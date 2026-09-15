namespace VisualEngineering.Core

/// One named check performed by `verify`.
type VerificationCheck =
    { Name: string
      Passed: bool
      /// Only checks that are part of the active mode can fail the command.
      StrictOnly: bool
      Detail: string }

type VerificationReport =
    { Passed: bool
      Strict: bool
      Checks: VerificationCheck list
      Problems: InstallationProblem list }

/// Validates that the capability is correctly installed. Reads only.
///
/// Default mode asks: is this installation internally consistent?
/// Strict mode also asks: is it the installation this release would produce?
[<RequireQualifiedAccess>]
module Verification =

    /// Statuses that mean the installation is broken regardless of version.
    let private isBroken status =
        match status with
        | Absent
        | LocallyModified
        | Unreadable _ -> true
        | UpToDate
        | Stale -> false

    let private check name strictOnly passed detail =
        { Name = name
          Passed = passed
          StrictOnly = strictOnly
          Detail = detail }

    let evaluate (repository: Repository) (payload: Payload) (strict: bool) : VerificationReport =
        let broken =
            repository.Artifacts |> List.filter (fun artifact -> isBroken artifact.Status)

        let missing =
            broken |> List.filter (fun artifact -> artifact.Status = Absent)

        let modified =
            broken |> List.filter (fun artifact -> artifact.Status = LocallyModified)

        let stale =
            repository.Artifacts |> List.filter (fun artifact -> artifact.Status = Stale)

        let installed =
            match repository.State with
            | NotInstalled -> false
            | _ -> true

        let payloadProblems = Payload.load payload.Root |> function Error problems -> problems | Ok _ -> []

        let checks =
            [ check
                  "installation"
                  false
                  installed
                  (if installed then
                       "an installation was detected"
                   else
                       $"{Tool.DisplayName} is not installed in this repository")

              check
                  "manifest"
                  false
                  repository.ManifestProblem.IsNone
                  (match repository.ManifestProblem with
                   | Some problem -> InstallationProblem.describe problem
                   | None -> $"{RepoPath.value Desired.manifestPath} is readable or not yet required")

              check
                  "configuration"
                  false
                  repository.ConfigurationProblem.IsNone
                  (match repository.ConfigurationProblem with
                   | Some problem -> InstallationProblem.describe problem
                   | None -> "configuration is readable")

              check
                  "required-files"
                  false
                  (List.isEmpty missing)
                  (if List.isEmpty missing then
                       "all required files are present"
                   else
                       "missing: "
                       + System.String.Join(", ", missing |> List.map (fun a -> RepoPath.value a.Desired.Path)))

              check
                  "file-integrity"
                  false
                  (List.isEmpty modified)
                  (if List.isEmpty modified then
                       "no tool maintained content was modified locally"
                   else
                       "locally modified: "
                       + System.String.Join(", ", modified |> List.map (fun a -> RepoPath.value a.Desired.Path)))

              check
                  "packaged-context"
                  false
                  (List.isEmpty payloadProblems)
                  (if List.isEmpty payloadProblems then
                       $"packaged context {payload.ContextVersion} passed its integrity check"
                   else
                       System.String.Join("; ", payloadProblems |> List.map PayloadProblem.describe))

              check
                  "up-to-date"
                  true
                  (List.isEmpty stale)
                  (if List.isEmpty stale then
                       "every managed file matches the packaged context"
                   else
                       "stale: "
                       + System.String.Join(", ", stale |> List.map (fun a -> RepoPath.value a.Desired.Path)))

              check
                  "version-compatibility"
                  true
                  (match repository.State with
                   | Installed _ -> true
                   | _ -> false)
                  (match repository.State with
                   | Installed version ->
                       $"installed version {version.Version} matches this release"
                   | UpgradeRequired(installed, available) ->
                       $"installed version {installed.Version} is behind {available.Version}"
                   | NotInstalled -> $"{Tool.DisplayName} is not installed"
                   | Invalid problems ->
                       System.String.Join("; ", problems |> List.map InstallationProblem.describe)) ]

        let applicable =
            checks |> List.filter (fun check -> strict || not check.StrictOnly)

        { Passed = applicable |> List.forall (fun check -> check.Passed)
          Strict = strict
          Checks = checks
          Problems = repository.Problems }
