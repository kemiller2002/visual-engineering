namespace VisualEngineering.Core

open System
open System.IO

/// How a managed path on disk compares to the desired state.
type ArtifactStatus =
    /// The file, or the managed region inside it, is not present.
    | Absent
    /// On disk content already equals the desired content.
    | UpToDate
    /// On disk content equals what the tool last wrote, but the desired content has moved on.
    | Stale
    /// On disk content is neither the desired content nor what the tool last wrote.
    | LocallyModified
    /// The file exists but could not be interpreted.
    | Unreadable of reason: string

[<RequireQualifiedAccess>]
module ArtifactStatus =

    let toString status =
        match status with
        | Absent -> "absent"
        | UpToDate -> "up-to-date"
        | Stale -> "stale"
        | LocallyModified -> "locally-modified"
        | Unreadable _ -> "unreadable"

type InspectedArtifact =
    { Desired: DesiredArtifact
      Status: ArtifactStatus
      /// Hash the tool recorded for this path, when an installation manifest exists.
      RecordedHash: string option
      /// True when the path belongs to a pre-Echelon `ve-context` installation being adopted.
      AdoptedFromLegacy: bool }

/// Everything read from a repository. Inspection never writes.
type Repository =
    { Root: string
      IsGitRepository: bool
      Manifest: InstallationManifest option
      ManifestProblem: InstallationProblem option
      Configuration: Configuration
      ConfigurationProblem: InstallationProblem option
      /// Context version of a pre-Echelon `ve-context sync` installation, when one is present.
      LegacyContextVersion: string option
      Artifacts: InspectedArtifact list
      Problems: InstallationProblem list
      State: InstallationState }

[<RequireQualifiedAccess>]
module Inspection =

    [<Literal>]
    let private LegacyVersionLabel = "legacy"

    let private readManifest (root: string) =
        match Files.tryReadText root Desired.manifestPath with
        | None -> None, None
        | Some text ->
            match Json.tryParse text with
            | Error reason -> None, Some(ManifestUnreadable(Desired.manifestPath, reason))
            | Ok document ->
                use document = document
                let element = document.RootElement

                match Json.tryInt "schemaVersion" element with
                | Some schema when schema <> Versioning.ManifestSchemaVersion ->
                    None, Some(UnsupportedManifestSchema(Desired.manifestPath, schema))
                | _ ->
                    let artifacts =
                        Json.arrayItems "managedArtifacts" element
                        |> List.choose (fun item ->
                            match Json.tryString "path" item with
                            | None -> None
                            | Some raw ->
                                match RepoPath.tryCreate raw with
                                | Error _ -> None
                                | Ok path ->
                                    Some
                                        { Path = path
                                          Ownership =
                                            Json.tryString "ownership" item
                                            |> Option.bind Ownership.tryParse
                                            |> Option.defaultValue ToolOwned
                                          Sha256 = Json.tryString "sha256" item })

                    let contextDirectory =
                        Json.tryString "contextDirectory" element
                        |> Option.bind (RepoPath.tryCreate >> Result.toOption)
                        |> Option.defaultValue Configuration.defaultContextDirectory

                    Some
                        { InstallationManifest.SchemaVersion =
                            Json.tryInt "schemaVersion" element
                            |> Option.defaultValue Versioning.ManifestSchemaVersion
                          Tool = Json.tryString "tool" element |> Option.defaultValue Tool.Id
                          InstalledVersion = Json.tryString "installedVersion" element |> Option.defaultValue ""
                          ConfigurationVersion =
                            Json.tryInt "configurationVersion" element
                            |> Option.defaultValue Versioning.LegacyConfigurationVersion
                          ContextVersion = Json.tryString "contextVersion" element |> Option.defaultValue ""
                          ContextDirectory = contextDirectory
                          ManagedArtifacts = artifacts },
                    None

    let private readConfiguration (root: string) (fallbackContextDirectory: RepoPath) =
        match Files.tryReadText root Desired.configurationPath with
        | None ->
            { Configuration.defaults with
                ContextDirectory = fallbackContextDirectory },
            None
        | Some text ->
            match Json.tryParse text with
            | Error reason ->
                { Configuration.defaults with
                    ContextDirectory = fallbackContextDirectory },
                Some(ConfigurationUnreadable(Desired.configurationPath, reason))
            | Ok document ->
                use document = document
                let element = document.RootElement

                let integrations = Json.tryProperty "integrations" element

                let agentsFile =
                    integrations
                    |> Option.bind (Json.tryString "agentsFile")
                    |> Option.bind (RepoPath.tryCreate >> Result.toOption)

                let ownedKeys =
                    set [ "schemaVersion"; "tool"; "configurationVersion"; "contextDirectory"; "integrations" ]

                let extras =
                    match Json.ofElement element with
                    | JObject fields -> fields |> List.filter (fun (key, _) -> not (ownedKeys.Contains key))
                    | _ -> []

                { Configuration.SchemaVersion =
                    Json.tryInt "schemaVersion" element
                    |> Option.defaultValue Versioning.ConfigurationSchemaVersion
                  ConfigurationVersion =
                    Json.tryInt "configurationVersion" element
                    |> Option.defaultValue Versioning.LegacyConfigurationVersion
                  ContextDirectory =
                    Json.tryString "contextDirectory" element
                    |> Option.bind (RepoPath.tryCreate >> Result.toOption)
                    |> Option.defaultValue fallbackContextDirectory
                  AgentsFile = agentsFile
                  ManageGitignore =
                    integrations
                    |> Option.bind (Json.tryBool "gitignore")
                    |> Option.defaultValue true
                  Extra = extras },
                None

    [<Literal>]
    let private UnknownVersion = "unknown"

    /// A pre-Echelon installation is recognised by the presence of the context manifest the
    /// previous mechanism wrote. A damaged manifest still counts as an installation: reporting
    /// it as "not installed" would hide a repository that needs repair.
    let private legacyContextVersion (root: string) (contextDirectory: RepoPath) =
        let contextFile = RepoPath.append contextDirectory Payload.ManifestFileName

        match Files.tryReadText root contextFile with
        | None -> None
        | Some text ->
            match Json.tryParse text with
            | Error _ -> Some UnknownVersion
            | Ok document ->
                use document = document
                Json.tryString "contextVersion" document.RootElement
                |> Option.orElse (Some UnknownVersion)

    /// Current hash of the content the tool owns at this path, or None when it is absent.
    let private actualHash (root: string) (artifact: DesiredArtifact) =
        match artifact.Content with
        | ExactText _ -> Files.tryReadText root artifact.Path |> Option.map Hash.ofText |> Ok
        | NormalizedJson _ ->
            match Files.tryReadText root artifact.Path with
            | None -> Ok None
            | Some text ->
                match Desired.normalizeJson text with
                | None -> Error "the file is not valid JSON"
                | Some normalized -> Ok(Some(Hash.ofText normalized))
        | Region(style, _, _) ->
            match Files.tryReadText root artifact.Path with
            | None -> Ok None
            | Some text ->
                ManagedBlock.tryExtract style text
                |> Option.map (fun block -> ManagedBlock.bodyHash block.Body)
                |> Ok

    let private inspectArtifact
        (root: string)
        (recorded: Map<string, string>)
        (legacyPaths: Set<string>)
        (artifact: DesiredArtifact)
        =
        let key = RepoPath.value artifact.Path
        let recordedHash = recorded.TryFind key
        let adopted = recordedHash.IsNone && legacyPaths.Contains key
        let desiredHash = Desired.recordedHash artifact

        let status =
            match actualHash root artifact with
            | Error reason -> Unreadable reason
            | Ok None -> Absent
            | Ok(Some actual) when actual = desiredHash -> UpToDate
            | Ok(Some actual) when recordedHash = Some actual -> Stale
            | Ok(Some _) when adopted -> Stale
            | Ok(Some _) -> LocallyModified

        { Desired = artifact
          Status = status
          RecordedHash = recordedHash
          AdoptedFromLegacy = adopted }

    let private problemOf (artifact: InspectedArtifact) =
        let path = artifact.Desired.Path

        match artifact.Desired.Content, artifact.Status with
        | _, UpToDate -> None
        | Region(_, integration, _), Absent -> Some(MissingIntegration(path, integration))
        | Region(_, integration, _), LocallyModified -> Some(ModifiedManagedRegion(path, integration))
        | Region(_, integration, _), Stale -> Some(MissingIntegration(path, integration))
        | _, Absent -> Some(MissingArtifact(path, artifact.Desired.Ownership))
        | _, Unreadable reason -> Some(ManifestUnreadable(path, reason))
        | _, Stale -> Some(StaleArtifact path)
        | _, LocallyModified -> Some(ModifiedArtifact(path, artifact.Desired.Ownership))

    let private isGitRepository (root: string) =
        Directory.Exists(Path.Combine(root, ".git")) || File.Exists(Path.Combine(root, ".git"))

    /// Reads everything the lifecycle commands need to know about a repository.
    let inspect (root: string) (payload: Payload) : Repository =
        let root = Path.TrimEndingDirectorySeparator(Path.GetFullPath root)
        let manifest, manifestProblem = readManifest root

        let fallbackContextDirectory =
            manifest
            |> Option.map (fun manifest -> manifest.ContextDirectory)
            |> Option.defaultValue Configuration.defaultContextDirectory

        let configuration, configurationProblem = readConfiguration root fallbackContextDirectory

        let legacyVersion =
            if manifest.IsSome then
                None
            else
                legacyContextVersion root configuration.ContextDirectory

        let recorded =
            manifest
            |> Option.map (fun manifest ->
                manifest.ManagedArtifacts
                |> List.choose (fun artifact ->
                    artifact.Sha256 |> Option.map (fun hash -> RepoPath.value artifact.Path, hash))
                |> Map.ofList)
            |> Option.defaultValue Map.empty

        // A pre-Echelon installation owned the whole context directory, so adopting its
        // files is not treated as overwriting user content.
        let legacyPaths =
            match legacyVersion with
            | None -> Set.empty
            | Some _ ->
                payload.Artifacts
                |> List.map (fun artifact ->
                    RepoPath.value configuration.ContextDirectory + "/" + RepoPath.value artifact.File)
                |> Set.ofList

        let desired = Desired.artifacts payload configuration

        let artifacts =
            desired |> List.map (inspectArtifact root recorded legacyPaths)

        let artifactProblems = artifacts |> List.choose problemOf

        let available: AvailableVersion =
            { Version = Versioning.cliVersion
              ConfigurationVersion = Versioning.CurrentConfigurationVersion
              ContextVersion = payload.ContextVersion }

        let state =
            match manifestProblem, configurationProblem with
            | Some problem, _ -> Invalid [ problem ]
            | _, Some problem -> Invalid [ problem ]
            | None, None ->
                match manifest, legacyVersion with
                | None, None -> NotInstalled
                | None, Some contextVersion ->
                    UpgradeRequired(
                        { InstalledVersion.Version = LegacyVersionLabel
                          ConfigurationVersion = Versioning.LegacyConfigurationVersion
                          ContextVersion = contextVersion },
                        available
                    )
                | Some manifest, _ ->
                    let installed: InstalledVersion =
                        { Version = manifest.InstalledVersion
                          ConfigurationVersion = manifest.ConfigurationVersion
                          ContextVersion = manifest.ContextVersion }

                    if not (List.contains manifest.ConfigurationVersion Versioning.supportedConfigurationVersions) then
                        Invalid [ UnsupportedConfigurationVersion manifest.ConfigurationVersion ]
                    elif
                        manifest.ConfigurationVersion < Versioning.CurrentConfigurationVersion
                        || manifest.ContextVersion <> payload.ContextVersion
                        || manifest.InstalledVersion <> Versioning.cliVersion
                    then
                        UpgradeRequired(installed, available)
                    elif not (List.isEmpty artifactProblems) then
                        Invalid artifactProblems
                    else
                        Installed installed

        { Root = root
          IsGitRepository = isGitRepository root
          Manifest = manifest
          ManifestProblem = manifestProblem
          Configuration = configuration
          ConfigurationProblem = configurationProblem
          LegacyContextVersion = legacyVersion
          Artifacts = artifacts
          Problems =
            (manifestProblem |> Option.toList)
            @ (configurationProblem |> Option.toList)
            @ artifactProblems
          State = state }
