namespace VisualEngineering.Core

open System
open System.IO

type DiagnosticReport =
    { Findings: Diagnosis list
      /// True when nothing was classified as an error.
      Healthy: bool }

/// Explains why an installation is wrong and what to do about it.
/// Unlike `verify`, a deviation is not automatically an error.
[<RequireQualifiedAccess>]
module Diagnostics =

    let private finding severity code title detail remedy =
        { Severity = severity
          Code = code
          Title = title
          Detail = detail
          Remedy = remedy }

    let private repositoryWritable (root: string) =
        let probe = Path.Combine(root, Tool.EchelonDirectory, ".write-probe")

        try
            Directory.CreateDirectory(Path.GetDirectoryName probe) |> ignore
            File.WriteAllText(probe, "")
            File.Delete probe
            Ok()
        with error ->
            Error error.Message

    let private environmentFindings (repository: Repository) (payload: Payload) =
        let payloadProblems =
            match Payload.load payload.Root with
            | Error problems -> problems
            | Ok _ -> []

        [ if not (List.isEmpty payloadProblems) then
              finding
                  Severity.Error
                  "packaged-context-corrupt"
                  "The packaged Visual Engineering context failed its integrity check"
                  (String.Join("; ", payloadProblems |> List.map PayloadProblem.describe))
                  (Some $"Reinstall the package: npx --yes {Tool.PackageName}@latest doctor")

          if not repository.IsGitRepository then
              finding
                  Severity.Information
                  "not-a-git-repository"
                  "This directory is not a git repository"
                  $"{repository.Root} has no .git entry. The lifecycle commands still work, but ignore rules and review of managed files will not apply."
                  None

          match repositoryWritable repository.Root with
          | Ok() -> ()
          | Error reason ->
              finding
                  Severity.Error
                  "repository-not-writable"
                  "The repository cannot be modified"
                  reason
                  (Some "Check filesystem permissions for the repository root.") ]

    /// Problems that an artifact specific finding below already explains in detail.
    let private coveredByArtifactFinding (repository: Repository) problem =
        let artifactPaths =
            repository.Artifacts
            |> List.map (fun artifact -> RepoPath.value artifact.Desired.Path)
            |> Set.ofList

        match problem with
        | MissingArtifact(path, _)
        | ModifiedArtifact(path, _)
        | StaleArtifact path
        | MissingIntegration(path, _)
        | ModifiedManagedRegion(path, _)
        | ManifestUnreadable(path, _) -> artifactPaths.Contains(RepoPath.value path)
        | _ -> false

    let private installationFindings (repository: Repository) (payload: Payload) =
        [ match repository.State with
          | NotInstalled ->
              finding
                  Severity.Warning
                  "not-installed"
                  $"{Tool.DisplayName} is not installed in this repository"
                  $"No {RepoPath.value Desired.manifestPath} and no {RepoPath.value repository.Configuration.ContextDirectory} were found."
                  (Some $"npx {Tool.PackageName} init")
          | UpgradeRequired(installed, available) when installed.Version = "legacy" ->
              finding
                  Severity.Warning
                  "legacy-installation"
                  "A pre-Echelon `ve-context` installation was detected"
                  $"{RepoPath.value repository.Configuration.ContextDirectory} holds context {installed.ContextVersion} but there is no Echelon installation manifest. Packaged context is {available.ContextVersion}."
                  (Some $"npx {Tool.PackageName} upgrade")
          | UpgradeRequired(installed, available) ->
              finding
                  Severity.Warning
                  "upgrade-available"
                  "The installation is behind this release"
                  $"Installed {installed.Version} (configuration version {installed.ConfigurationVersion}, context {installed.ContextVersion}); this release provides {available.Version} (configuration version {available.ConfigurationVersion}, context {available.ContextVersion})."
                  (Some $"npx {Tool.PackageName} upgrade")
          | Invalid problems ->
              // Artifact level problems get their own finding below, with a specific remedy.
              for problem in problems |> List.filter (coveredByArtifactFinding repository >> not) do
                  finding
                      Severity.Error
                      "invalid-installation"
                      "The installation is not valid"
                      (InstallationProblem.describe problem)
                      (Some $"npx {Tool.PackageName} doctor --verbose, then npx {Tool.PackageName} init")
          | Installed version ->
              finding
                  Severity.Information
                  "installed"
                  "The installation matches this release"
                  $"Version {version.Version}, configuration version {version.ConfigurationVersion}, context {version.ContextVersion}."
                  None

          for artifact in repository.Artifacts do
              let path = RepoPath.value artifact.Desired.Path

              match artifact.Desired.Content, artifact.Status with
              | _, UpToDate -> ()
              | Region(_, integration, _), Absent ->
                  finding
                      Severity.Warning
                      "integration-missing"
                      $"Integration '{integration}' is not registered"
                      $"{path} has no managed '{ManagedBlock.Marker}' region."
                      (Some $"npx {Tool.PackageName} init")
              | Region(_, integration, _), LocallyModified ->
                  finding
                      Severity.Error
                      "managed-region-modified"
                      $"The managed '{integration}' region was edited"
                      $"The region inside {path} no longer matches what this tool wrote, so it cannot be updated safely."
                      (Some
                          $"Move your edits outside the managed markers, or accept replacement with: npx {Tool.PackageName} init --force")
              | _, Absent ->
                  finding
                      Severity.Error
                      "file-missing"
                      $"Required file {path} is missing"
                      $"{Ownership.toString artifact.Desired.Ownership} file {path} is declared by the installation but not present."
                      (Some $"npx {Tool.PackageName} init")
              | _, Unreadable reason ->
                  finding
                      Severity.Error
                      "file-unreadable"
                      $"{path} could not be interpreted"
                      reason
                      (Some $"Repair or delete {path}, then run npx {Tool.PackageName} init")
              | _, LocallyModified ->
                  finding
                      Severity.Error
                      "file-modified"
                      $"{path} was modified locally"
                      $"{path} is {Ownership.toString artifact.Desired.Ownership} but its content differs from what this tool wrote."
                      (Some
                          $"Restore the file, or accept replacement with: npx {Tool.PackageName} init --force")
              | _, Stale ->
                  finding
                      Severity.Warning
                      "file-stale"
                      $"{path} is behind the packaged context"
                      $"{path} matches the previously installed context, but the packaged context is {payload.ContextVersion}."
                      (Some $"npx {Tool.PackageName} upgrade")

          match repository.Manifest with
          | Some manifest when manifest.Tool <> Tool.Id ->
              finding
                  Severity.Error
                  "manifest-tool-mismatch"
                  "The installation manifest belongs to a different tool"
                  $"{RepoPath.value Desired.manifestPath} declares tool '{manifest.Tool}'."
                  (Some $"Remove {RepoPath.value Desired.manifestPath} and run npx {Tool.PackageName} init")
          | _ -> () ]

    let diagnose (repository: Repository) (payload: Payload) : DiagnosticReport =
        let findings =
            environmentFindings repository payload @ installationFindings repository payload
            |> List.sortBy (fun finding -> Severity.rank finding.Severity)

        { Findings = findings
          Healthy = findings |> List.forall (fun finding -> finding.Severity <> Severity.Error) }
