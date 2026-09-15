namespace VisualEngineering.Core

/// Options that change what a plan is allowed to contain.
type PlanOptions =
    { /// Replace locally modified tool maintained content instead of refusing to proceed.
      Force: bool }

[<RequireQualifiedAccess>]
module PlanOptions =
    let defaults = { Force = false }

/// Turns "what is" and "what should be" into an explicit, validated set of changes.
/// Nothing here touches the filesystem.
[<RequireQualifiedAccess>]
module Planning =

    let private targetConfiguration (repository: Repository) =
        { repository.Configuration with
            SchemaVersion = Versioning.ConfigurationSchemaVersion
            ConfigurationVersion = Versioning.CurrentConfigurationVersion
            AgentsFile =
                match repository.Configuration.AgentsFile with
                | Some file -> Some file
                | None when Files.tryReadText repository.Root Desired.configurationPath |> Option.isSome -> None
                | None -> Some Configuration.defaultAgentsFile }

    let private changeFor (repository: Repository) (artifact: InspectedArtifact) =
        let path = artifact.Desired.Path

        match artifact.Desired.Content with
        | Region(style, integration, body) ->
            let existing = Files.tryReadText repository.Root path
            Some(RegisterIntegration(integration, path, ManagedBlock.upsert style body existing))
        | NormalizedJson text ->
            match artifact.Status with
            | Absent -> Some(CreateFile(path, artifact.Desired.Ownership, text))
            | _ -> Some(UpdateConfiguration(path, text))
        | ExactText text ->
            match artifact.Status with
            | Absent -> Some(CreateFile(path, artifact.Desired.Ownership, text))
            | _ -> Some(UpdateManagedFile(path, artifact.Desired.Ownership, text))

    let private blockerFor (artifact: InspectedArtifact) =
        let path = artifact.Desired.Path

        match artifact.Desired.Content, artifact.Status with
        | Region(_, integration, _), LocallyModified -> Some(LocallyModifiedRegion(path, integration))
        | _, LocallyModified -> Some(LocallyModifiedFile(path, artifact.Desired.Ownership))
        | _, Unreadable _ -> Some(ConflictingUnmanagedFile path)
        | _ -> None

    let private preservedFor (artifact: InspectedArtifact) =
        match artifact.Desired.Ownership, artifact.Status with
        | UserOwned, status when status <> Absent ->
            Some
                { Path = artifact.Desired.Path
                  Reason = "user-owned files are never modified" }
        | Shared, status when status <> Absent ->
            Some
                { Path = artifact.Desired.Path
                  Reason = "content outside the managed region is preserved" }
        | _ -> None

    let private buildManifest (repository: Repository) (payload: Payload) (configuration: Configuration) =
        let desired = Desired.artifacts payload configuration

        { InstallationManifest.SchemaVersion = Versioning.ManifestSchemaVersion
          Tool = Tool.Id
          InstalledVersion = Versioning.cliVersion
          ConfigurationVersion = Versioning.CurrentConfigurationVersion
          ContextVersion = payload.ContextVersion
          ContextDirectory = configuration.ContextDirectory
          ManagedArtifacts =
            desired
            |> List.map (fun artifact ->
                { Path = artifact.Path
                  Ownership = artifact.Ownership
                  Sha256 = Some(Desired.recordedHash artifact) })
            |> List.sortBy (fun artifact -> RepoPath.value artifact.Path) }
        |> fun manifest ->
            ignore repository
            manifest

    let private currentConfigurationVersion (repository: Repository) =
        match repository.Manifest with
        | Some manifest -> Some manifest.ConfigurationVersion
        | None ->
            match repository.LegacyContextVersion with
            | Some _ -> Some Versioning.LegacyConfigurationVersion
            | None -> None

    let private build (repository: Repository) (payload: Payload) (options: PlanOptions) (migrations: Migration list) =
        let configuration = targetConfiguration repository
        let manifest = buildManifest repository payload configuration
        let manifestText = Desired.renderManifest manifest

        let outOfDate =
            repository.Artifacts |> List.filter (fun artifact -> artifact.Status <> UpToDate)

        let directoryChanges =
            Desired.directories configuration
            |> List.filter (fun directory -> not (Files.directoryExists repository.Root directory))
            |> List.map CreateDirectory

        let migrationChanges =
            migrations
            |> List.map (fun migration -> RunMigration(migration.From, migration.To, migration.Description))

        let artifactChanges = outOfDate |> List.choose (changeFor repository)

        let manifestChange =
            let current = Files.tryReadText repository.Root Desired.manifestPath

            if current = Some manifestText then
                []
            elif current.IsSome then
                [ UpdateManagedFile(Desired.manifestPath, ToolOwned, manifestText) ]
            else
                [ CreateFile(Desired.manifestPath, ToolOwned, manifestText) ]

        let blockers =
            if options.Force then
                []
            else
                outOfDate |> List.choose blockerFor

        { Changes = directoryChanges @ migrationChanges @ artifactChanges @ manifestChange
          Blockers = blockers @ Migrations.validate repository migrations
          Preserved = repository.Artifacts |> List.choose preservedFor
          Migrations = migrations |> List.map (fun migration -> migration.From, migration.To)
          TargetManifest = manifest }

    /// Plan that brings a repository into a valid installed state, whatever state it starts in.
    let createInitializationPlan (repository: Repository) (payload: Payload) (options: PlanOptions) : Plan =
        let migrations =
            match currentConfigurationVersion repository with
            | None -> Ok []
            | Some version -> Migrations.plan version Versioning.CurrentConfigurationVersion

        match migrations with
        | Error blocker ->
            { Changes = []
              Blockers = [ blocker ]
              Preserved = []
              TargetManifest = buildManifest repository payload (targetConfiguration repository)
              Migrations = [] }
        | Ok migrations -> build repository payload options migrations

    /// Plan that moves an existing installation to the current version.
    let planUpgrade (repository: Repository) (payload: Payload) (options: PlanOptions) : Plan =
        match currentConfigurationVersion repository with
        | None ->
            { Changes = []
              Blockers =
                [ MigrationPreconditionFailed(
                      0,
                      Versioning.CurrentConfigurationVersion,
                      $"{Tool.DisplayName} is not installed in this repository; run `{Tool.ExecutableName} init` first"
                  ) ]
              Preserved = []
              TargetManifest = buildManifest repository payload (targetConfiguration repository)
              Migrations = [] }
        | Some _ -> createInitializationPlan repository payload options
