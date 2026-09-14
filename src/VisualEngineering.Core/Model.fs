namespace VisualEngineering.Core

open System

/// Who controls a managed file. The classification decides what the tool is allowed to do
/// to the file during `init` and `upgrade`.
type Ownership =
    /// Controlled by the tool. Replaced according to explicit version rules.
    | ToolOwned
    /// Derived from authoritative inputs. Regenerated whenever the inputs change.
    | Generated
    /// Controlled by the repository. Created once if absent, never modified afterwards.
    | UserOwned
    /// Managed by both. The tool owns a delimited region or a set of reserved keys.
    | Shared

[<RequireQualifiedAccess>]
module Ownership =

    let toString ownership =
        match ownership with
        | ToolOwned -> "tool-owned"
        | Generated -> "generated"
        | UserOwned -> "user-owned"
        | Shared -> "shared"

    let tryParse (value: string) =
        match value with
        | "tool-owned" -> Some ToolOwned
        | "generated" -> Some Generated
        | "user-owned" -> Some UserOwned
        | "shared" -> Some Shared
        | _ -> None

    /// True when the tool may rewrite the file (or its managed region) without asking.
    let isToolMaintained ownership =
        match ownership with
        | ToolOwned
        | Generated
        | Shared -> true
        | UserOwned -> false

/// A file recorded in the installation manifest. `Sha256` is the normalized hash of the
/// content the tool last wrote, which is how local modification is detected later.
type ManagedArtifact =
    { Path: RepoPath
      Ownership: Ownership
      Sha256: string option }

/// Machine readable installation record written to `.echelon/visual-engineering.json`.
/// Contains no secrets, no machine specific values and no semantically significant timestamps.
type InstallationManifest =
    { SchemaVersion: int
      Tool: string
      InstalledVersion: string
      ConfigurationVersion: int
      ContextVersion: string
      ContextDirectory: RepoPath
      ManagedArtifacts: ManagedArtifact list }

/// Repository configuration written to `.echelon/visual-engineering.config.json`.
/// `Extra` preserves keys the tool does not own so user edits survive rewrites.
type Configuration =
    { SchemaVersion: int
      ConfigurationVersion: int
      ContextDirectory: RepoPath
      AgentsFile: RepoPath option
      ManageGitignore: bool
      Extra: (string * JsonValue) list }

[<RequireQualifiedAccess>]
module Configuration =

    let defaultContextDirectory = RepoPath.create ".visual-engineering"
    let defaultAgentsFile = RepoPath.create "AGENTS.md"

    let defaults =
        { SchemaVersion = Versioning.ConfigurationSchemaVersion
          ConfigurationVersion = Versioning.CurrentConfigurationVersion
          ContextDirectory = defaultContextDirectory
          AgentsFile = Some defaultAgentsFile
          ManageGitignore = true
          Extra = [] }

/// Version of an installation found in a repository.
type InstalledVersion =
    { Version: string
      ConfigurationVersion: int
      ContextVersion: string }

type AvailableVersion =
    { Version: string
      ConfigurationVersion: int
      ContextVersion: string }

/// Something wrong with an installation. Reported by `verify`, explained by `doctor`.
type InstallationProblem =
    | ManifestUnreadable of path: RepoPath * reason: string
    | UnsupportedManifestSchema of path: RepoPath * found: int
    | UnsupportedConfigurationVersion of found: int
    | ConfigurationUnreadable of path: RepoPath * reason: string
    | MissingArtifact of path: RepoPath * ownership: Ownership
    | ModifiedArtifact of path: RepoPath * ownership: Ownership
    | StaleArtifact of path: RepoPath
    | MissingIntegration of path: RepoPath * integration: string
    | ModifiedManagedRegion of path: RepoPath * integration: string
    | UnmanagedConflict of path: RepoPath
    | ContextVersionMismatch of installed: string * available: string

[<RequireQualifiedAccess>]
module InstallationProblem =

    let describe problem =
        match problem with
        | ManifestUnreadable(path, reason) -> $"Installation manifest {RepoPath.value path} is unreadable: {reason}"
        | UnsupportedManifestSchema(path, found) ->
            $"Installation manifest {RepoPath.value path} uses unsupported schema version {found}"
        | UnsupportedConfigurationVersion found ->
            $"Configuration version {found} is not supported by this release"
        | ConfigurationUnreadable(path, reason) -> $"Configuration {RepoPath.value path} is unreadable: {reason}"
        | MissingArtifact(path, ownership) ->
            $"Required {Ownership.toString ownership} file {RepoPath.value path} is missing"
        | ModifiedArtifact(path, ownership) ->
            $"{Ownership.toString ownership} file {RepoPath.value path} was modified locally"
        | StaleArtifact path -> $"Generated file {RepoPath.value path} is stale"
        | MissingIntegration(path, integration) ->
            $"Integration '{integration}' is not registered in {RepoPath.value path}"
        | ModifiedManagedRegion(path, integration) ->
            $"The managed '{integration}' region in {RepoPath.value path} was edited locally"
        | UnmanagedConflict path -> $"{RepoPath.value path} already exists and is not managed by this tool"
        | ContextVersionMismatch(installed, available) ->
            $"Installed context {installed} is older than packaged context {available}"

/// The lifecycle state of a repository with respect to this capability.
/// Illegal combinations are not representable: a repository is in exactly one state.
type InstallationState =
    | NotInstalled
    | Installed of InstalledVersion
    | UpgradeRequired of InstalledVersion * AvailableVersion
    | Invalid of InstallationProblem list

[<RequireQualifiedAccess>]
module InstallationState =

    let toString state =
        match state with
        | NotInstalled -> "not-installed"
        | Installed _ -> "installed"
        | UpgradeRequired _ -> "upgrade-required"
        | Invalid _ -> "invalid"

/// A change the tool intends to make. Planning produces these; nothing else writes to disk.
type PlannedChange =
    | CreateDirectory of RepoPath
    | CreateFile of path: RepoPath * ownership: Ownership * content: string
    | UpdateManagedFile of path: RepoPath * ownership: Ownership * content: string
    | UpdateConfiguration of path: RepoPath * content: string
    | RegisterIntegration of integration: string * path: RepoPath * content: string
    | RunMigration of fromVersion: int * toVersion: int * description: string

[<RequireQualifiedAccess>]
module PlannedChange =

    let path change =
        match change with
        | CreateDirectory path -> Some path
        | CreateFile(path, _, _) -> Some path
        | UpdateManagedFile(path, _, _) -> Some path
        | UpdateConfiguration(path, _) -> Some path
        | RegisterIntegration(_, path, _) -> Some path
        | RunMigration _ -> None

    let kind change =
        match change with
        | CreateDirectory _ -> "create-directory"
        | CreateFile _ -> "create-file"
        | UpdateManagedFile _ -> "update-managed-file"
        | UpdateConfiguration _ -> "update-configuration"
        | RegisterIntegration _ -> "register-integration"
        | RunMigration _ -> "run-migration"

    let describe change =
        match change with
        | CreateDirectory path -> $"create directory {RepoPath.value path}"
        | CreateFile(path, ownership, _) -> $"create {Ownership.toString ownership} file {RepoPath.value path}"
        | UpdateManagedFile(path, ownership, _) -> $"update {Ownership.toString ownership} file {RepoPath.value path}"
        | UpdateConfiguration(path, _) -> $"update configuration {RepoPath.value path}"
        | RegisterIntegration(integration, path, _) ->
            $"register integration '{integration}' in {RepoPath.value path}"
        | RunMigration(fromVersion, toVersion, description) ->
            $"migrate configuration {fromVersion} -> {toVersion}: {description}"

/// A reason the tool refuses to execute a plan. Blockers are never worked around silently.
type PlanBlocker =
    | LocallyModifiedFile of path: RepoPath * ownership: Ownership
    | LocallyModifiedRegion of path: RepoPath * integration: string
    | ConflictingUnmanagedFile of path: RepoPath
    | MigrationPreconditionFailed of fromVersion: int * toVersion: int * reason: string
    | NoMigrationPath of fromVersion: int * toVersion: int
    | RepositoryNotWritable of reason: string

[<RequireQualifiedAccess>]
module PlanBlocker =

    let describe blocker =
        match blocker with
        | LocallyModifiedFile(path, ownership) ->
            $"{RepoPath.value path} is {Ownership.toString ownership} but was modified locally; rerun with --force to replace it"
        | LocallyModifiedRegion(path, integration) ->
            $"the managed '{integration}' region in {RepoPath.value path} was edited locally; rerun with --force to replace it"
        | ConflictingUnmanagedFile path ->
            $"{RepoPath.value path} already exists and is not recorded in any installation manifest; rerun with --force to replace it"
        | MigrationPreconditionFailed(fromVersion, toVersion, reason) ->
            $"migration {fromVersion} -> {toVersion} cannot start: {reason}"
        | NoMigrationPath(fromVersion, toVersion) ->
            $"no supported migration path from configuration version {fromVersion} to {toVersion}"
        | RepositoryNotWritable reason -> $"the repository cannot be modified: {reason}"

/// A file the plan deliberately leaves alone, with the reason.
type PreservedFile = { Path: RepoPath; Reason: string }

/// The complete, validated intent of an `init` or `upgrade`. A plan with blockers is never executed.
type Plan =
    { Changes: PlannedChange list
      Blockers: PlanBlocker list
      Preserved: PreservedFile list
      Migrations: (int * int) list
      TargetManifest: InstallationManifest }

[<RequireQualifiedAccess>]
module Plan =

    let isExecutable plan = List.isEmpty plan.Blockers
    let hasChanges plan = not (List.isEmpty plan.Changes)

/// Severity used by `doctor`. Not every deviation is an error.
[<RequireQualifiedAccess>]
type Severity =
    | Information
    | Warning
    | Error

[<RequireQualifiedAccess>]
module Severity =

    let toString severity =
        match severity with
        | Severity.Information -> "information"
        | Severity.Warning -> "warning"
        | Severity.Error -> "error"

    let rank severity =
        match severity with
        | Severity.Error -> 0
        | Severity.Warning -> 1
        | Severity.Information -> 2

/// A single `doctor` finding: what is wrong, why, and how to fix it.
type Diagnosis =
    { Severity: Severity
      Code: string
      Title: string
      Detail: string
      Remedy: string option }
