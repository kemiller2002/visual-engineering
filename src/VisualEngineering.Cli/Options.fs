namespace VisualEngineering.Cli

/// Options every command accepts.
type CommonOptions =
    { /// Repository to operate on. Defaults to the current working directory.
      Repository: string
      Json: bool
      Verbose: bool }

type InitOptions =
    { Common: CommonOptions
      DryRun: bool
      Check: bool
      Force: bool }

type StatusOptions = { Common: CommonOptions }

type VerifyOptions = { Common: CommonOptions; Strict: bool }

type UpgradeOptions =
    { Common: CommonOptions
      DryRun: bool
      Check: bool
      Force: bool }

type DoctorOptions = { Common: CommonOptions; Strict: bool }

type RobustnessOptions =
    { Common: CommonOptions
      Manifest: string }

/// The command the user asked for. Parsing produces exactly one of these, so an
/// unsupported combination of flags cannot reach the lifecycle code.
type Command =
    | Init of InitOptions
    | Status of StatusOptions
    | Verify of VerifyOptions
    | Upgrade of UpgradeOptions
    | Doctor of DoctorOptions
    | Robustness of RobustnessOptions
    | Help of topic: string option
    | Version

[<RequireQualifiedAccess>]
module Command =

    let name command =
        match command with
        | Init _ -> "init"
        | Status _ -> "status"
        | Verify _ -> "verify"
        | Upgrade _ -> "upgrade"
        | Doctor _ -> "doctor"
        | Robustness _ -> "robustness"
        | Help _ -> "help"
        | Version -> "version"

    let common command =
        match command with
        | Init options -> Some options.Common
        | Status options -> Some options.Common
        | Verify options -> Some options.Common
        | Upgrade options -> Some options.Common
        | Doctor options -> Some options.Common
        | Robustness options -> Some options.Common
        | Help _
        | Version -> None
