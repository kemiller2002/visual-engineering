namespace VisualEngineering.Core

/// A single supported configuration transition. Upgrades are always composed from these,
/// one version at a time, so an old installation walks the same path a recent one did.
type Migration =
    { From: int
      To: int
      Description: string
      /// What the transition is expected to change, for reporting and documentation.
      Changes: string list
      /// Checked before anything is written. A failure stops the whole upgrade.
      Precondition: Repository -> Result<unit, string> }

[<RequireQualifiedAccess>]
module Migrations =

    let private legacyContextReadable (repository: Repository) =
        let contextFile =
            RepoPath.append repository.Configuration.ContextDirectory Payload.ManifestFileName

        if repository.Manifest.IsSome then
            Ok()
        else
            match Files.tryReadText repository.Root contextFile with
            | None ->
                Error
                    $"{RepoPath.value contextFile} is missing, so there is no version 1 installation to migrate"
            | Some text ->
                match Json.tryParse text with
                | Error reason -> Error $"{RepoPath.value contextFile} is not readable: {reason}"
                | Ok document ->
                    use document = document

                    match Json.tryString "contextVersion" document.RootElement with
                    | None -> Error $"{RepoPath.value contextFile} does not declare a contextVersion"
                    | Some _ -> Ok()

    let private configurationReadable (repository: Repository) =
        match repository.ConfigurationProblem with
        | Some problem -> Error(InstallationProblem.describe problem)
        | None -> Ok()

    /// Every transition this release knows how to perform, in order.
    let all: Migration list =
        [ { From = 1
            To = 2
            Description = "Adopt a `ve-context` installation and record an Echelon installation manifest"
            Changes =
              [ $"write {RepoPath.value Desired.manifestPath}"
                "record ownership and content hashes for every installed context file" ]
            Precondition = legacyContextReadable }
          { From = 2
            To = 3
            Description = "Add repository configuration and register the agent briefing integration"
            Changes =
              [ $"write {RepoPath.value Desired.configurationPath}"
                "register the managed agent briefing region"
                "register the managed .gitignore region" ]
            Precondition = configurationReadable } ]

    /// The ordered transitions needed to move from one configuration version to another.
    let plan (fromVersion: int) (toVersion: int) : Result<Migration list, PlanBlocker> =
        if fromVersion = toVersion then
            Ok []
        elif fromVersion > toVersion then
            Error(NoMigrationPath(fromVersion, toVersion))
        else
            let rec walk current acc =
                if current = toVersion then
                    Ok(List.rev acc)
                else
                    match all |> List.tryFind (fun migration -> migration.From = current) with
                    | None -> Error(NoMigrationPath(fromVersion, toVersion))
                    | Some migration -> walk migration.To (migration :: acc)

            walk fromVersion []

    /// Runs every precondition in order and stops at the first failure.
    let validate (repository: Repository) (migrations: Migration list) : PlanBlocker list =
        migrations
        |> List.fold
            (fun blockers migration ->
                if not (List.isEmpty blockers) then
                    blockers
                else
                    match migration.Precondition repository with
                    | Ok() -> []
                    | Error reason -> [ MigrationPreconditionFailed(migration.From, migration.To, reason) ])
            []
