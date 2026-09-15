namespace VisualEngineering.Core

open System.IO

/// Stable process exit codes. Documented in docs/cli.md and covered by tests.
type ExitCode =
    | Success = 0
    | InternalError = 1
    | UsageError = 2
    | VerificationFailed = 3
    | ChangesRequired = 4
    | InstallationBlocked = 5
    | EnvironmentError = 6
    | UnsupportedPlatform = 7

/// A loaded payload bound to a repository. Created once per command.
type Session =
    { Root: string
      Payload: Payload
      Repository: Repository }

type SessionError =
    | PayloadUnavailable of PayloadProblem list
    | RepositoryUnavailable of reason: string

type StatusReport =
    { Tool: string
      DisplayName: string
      Package: string
      CliVersion: string
      InstalledVersion: string option
      ConfigurationVersion: int option
      InstalledContextVersion: string option
      PackagedContextVersion: string
      SourceCommit: string
      ResearchDocuments: int
      ContextDirectory: RepoPath
      ManifestPath: RepoPath
      ConfigurationPath: RepoPath
      State: InstallationState
      ConfigurationValid: bool
      ArtifactsValid: bool
      IntegrationsValid: bool
      Verification: VerificationReport
      AvailableUpgrade: AvailableVersion option }

/// Result of a command that may change the repository.
type LifecycleOutcome =
    { Plan: Plan
      /// None when the command ran as a dry run or as a check.
      Execution: ExecutionResult option
      /// Verification of the resulting state, when the plan was executed.
      Verification: VerificationReport option }

/// The callable surface of this capability.
///
/// Everything the CLI does is available here without simulating command line input, so the
/// same core can back an integration assembly, a service host, or another Echelon tool.
[<RequireQualifiedAccess>]
module Api =

    /// Binds an already loaded payload to a repository. Callers that host this core
    /// themselves - tests, integration assemblies, other Echelon tools - use this.
    let openSessionWith (payload: Payload) (root: string) : Result<Session, SessionError> =
        if not (Directory.Exists root) then
            Error(RepositoryUnavailable $"{root} is not a directory")
        else
            let full = Path.TrimEndingDirectorySeparator(Path.GetFullPath root)

            Ok
                { Root = full
                  Payload = payload
                  Repository = Inspection.inspect full payload }

    /// Loads the packaged payload and inspects a repository.
    let openSession (root: string) : Result<Session, SessionError> =
        if not (Directory.Exists root) then
            Error(RepositoryUnavailable $"{root} is not a directory")
        else
            match Payload.discover () with
            | Error problems -> Error(PayloadUnavailable problems)
            | Ok payload ->
                let full = Path.TrimEndingDirectorySeparator(Path.GetFullPath root)

                Ok
                    { Root = full
                      Payload = payload
                      Repository = Inspection.inspect full payload }

    /// Re-reads the repository, for example after applying a plan.
    let refresh (session: Session) =
        { session with
            Repository = Inspection.inspect session.Root session.Payload }

    let inspectRepository (root: string) (payload: Payload) = Inspection.inspect root payload

    let getInstallationState (session: Session) = session.Repository.State

    let createInitializationPlan (session: Session) (options: PlanOptions) =
        Planning.createInitializationPlan session.Repository session.Payload options

    let planUpgrade (session: Session) (options: PlanOptions) =
        Planning.planUpgrade session.Repository session.Payload options

    let verify (session: Session) (strict: bool) =
        Verification.evaluate session.Repository session.Payload strict

    let diagnose (session: Session) =
        Diagnostics.diagnose session.Repository session.Payload

    let getStatus (session: Session) : StatusReport =
        let repository = session.Repository
        let report = Verification.evaluate repository session.Payload false

        let artifactsValid =
            repository.Artifacts
            |> List.filter (fun artifact ->
                match artifact.Desired.Content with
                | Region _ -> false
                | _ -> true)
            |> List.forall (fun artifact ->
                match artifact.Status with
                | UpToDate
                | Stale -> true
                | _ -> false)

        let integrationsValid =
            repository.Artifacts
            |> List.filter (fun artifact ->
                match artifact.Desired.Content with
                | Region _ -> true
                | _ -> false)
            |> List.forall (fun artifact -> artifact.Status = UpToDate)

        { Tool = Tool.Id
          DisplayName = Tool.DisplayName
          Package = Tool.PackageName
          CliVersion = Versioning.cliVersion
          InstalledVersion =
            match repository.State with
            | Installed version -> Some version.Version
            | UpgradeRequired(installed, _) -> Some installed.Version
            | NotInstalled
            | Invalid _ -> repository.Manifest |> Option.map (fun manifest -> manifest.InstalledVersion)
          ConfigurationVersion =
            repository.Manifest
            |> Option.map (fun manifest -> manifest.ConfigurationVersion)
            |> Option.orElse (
                repository.LegacyContextVersion
                |> Option.map (fun _ -> Versioning.LegacyConfigurationVersion)
            )
          InstalledContextVersion =
            repository.Manifest
            |> Option.map (fun manifest -> manifest.ContextVersion)
            |> Option.orElse repository.LegacyContextVersion
          PackagedContextVersion = session.Payload.ContextVersion
          SourceCommit = session.Payload.SourceCommit
          ResearchDocuments = session.Payload.ResearchDocuments
          ContextDirectory = repository.Configuration.ContextDirectory
          ManifestPath = Desired.manifestPath
          ConfigurationPath = Desired.configurationPath
          State = repository.State
          ConfigurationValid = repository.ConfigurationProblem.IsNone
          ArtifactsValid = artifactsValid
          IntegrationsValid = integrationsValid
          Verification = report
          AvailableUpgrade =
            match repository.State with
            | UpgradeRequired(_, available) -> Some available
            | _ -> None }

    let private run (session: Session) (plan: Plan) (dryRun: bool) : LifecycleOutcome * Session =
        if dryRun || not (Plan.isExecutable plan) then
            { Plan = plan
              Execution = (if Plan.isExecutable plan then None else Some(Aborted plan.Blockers))
              Verification = None },
            session
        else
            let execution = Execution.apply session.Root plan
            let refreshed = refresh session

            { Plan = plan
              Execution = Some execution
              Verification = Some(Verification.evaluate refreshed.Repository refreshed.Payload false) },
            refreshed

    /// Brings the repository into a valid installed state. Idempotent.
    let initialize (session: Session) (options: PlanOptions) (dryRun: bool) =
        run session (createInitializationPlan session options) dryRun

    /// Moves an existing installation to the version this release provides.
    let performUpgrade (session: Session) (options: PlanOptions) (dryRun: bool) =
        run session (planUpgrade session options) dryRun

    /// Exit code for a lifecycle outcome.
    let exitCodeFor (outcome: LifecycleOutcome) (check: bool) =
        if not (Plan.isExecutable outcome.Plan) then
            ExitCode.InstallationBlocked
        elif check && Plan.hasChanges outcome.Plan then
            ExitCode.ChangesRequired
        else
            match outcome.Execution with
            | Some(PartiallyApplied _) -> ExitCode.InternalError
            | Some(Aborted _) -> ExitCode.InstallationBlocked
            | _ ->
                match outcome.Verification with
                | Some report when not report.Passed -> ExitCode.VerificationFailed
                | _ -> ExitCode.Success
