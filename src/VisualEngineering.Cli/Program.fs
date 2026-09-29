module VisualEngineering.Cli.Program

open System
open VisualEngineering.Core

/// stdout carries the answer; stderr carries diagnostics. With --json, stdout is JSON only.
let private out (text: string) = Console.Out.Write text
let private err (text: string) = Console.Error.Write text

let private emit (common: CommonOptions) (json: JsonValue) (human: unit -> string) =
    if common.Json then
        out (JsonOutput.serialize json)
    else
        out (human ())

let private planOptions (force: bool) : PlanOptions = { Force = force }

let private runStatus (session: Session) (options: StatusOptions) =
    let report = Api.getStatus session

    let code =
        match report.State with
        | Invalid _ -> ExitCode.VerificationFailed
        | _ -> ExitCode.Success

    emit options.Common (JsonOutput.status report code) (fun () -> Render.status report)
    code

let private runVerify (session: Session) (options: VerifyOptions) =
    let report = Api.verify session options.Strict

    let code =
        if report.Passed then
            ExitCode.Success
        else
            ExitCode.VerificationFailed

    emit options.Common (JsonOutput.verifyReport report code) (fun () ->
        Render.verification report options.Common.Verbose)

    code

let private runDoctor (session: Session) (options: DoctorOptions) =
    let report = Api.diagnose session

    let failing =
        report.Findings
        |> List.filter (fun finding ->
            finding.Severity = Severity.Error
            || (options.Strict && finding.Severity = Severity.Warning))

    let code =
        if List.isEmpty failing then
            ExitCode.Success
        else
            ExitCode.VerificationFailed

    emit options.Common (JsonOutput.doctorReport report code) (fun () ->
        Render.diagnostics report options.Strict options.Common.Verbose)

    code

let private runRobustness (options: RobustnessOptions) =
    match RepoPath.tryCreate options.Manifest with
    | Error message ->
        let code = ExitCode.UsageError

        if options.Common.Json then
            out (JsonOutput.serialize (JsonOutput.error "robustness" code [ message ]))
        else
            err ($"{Tool.ExecutableName}: {message}\n")

        code
    | Ok manifestPath ->
        if not (System.IO.Directory.Exists options.Common.Repository) then
            let code = ExitCode.EnvironmentError
            let message = $"{options.Common.Repository} is not a directory"

            if options.Common.Json then
                out (JsonOutput.serialize (JsonOutput.error "robustness" code [ message ]))
            else
                err ($"{Tool.ExecutableName}: {message}\n")

            code
        else
            match Files.tryReadText options.Common.Repository manifestPath with
            | None ->
                let code = ExitCode.EnvironmentError
                let message = $"robustness manifest {RepoPath.value manifestPath} was not found"

                if options.Common.Json then
                    out (JsonOutput.serialize (JsonOutput.error "robustness" code [ message ]))
                else
                    err ($"{Tool.ExecutableName}: {message}\n")

                code
            | Some manifestText ->
                match Api.evaluateRobustness manifestText with
                | Error messages ->
                    let code = ExitCode.VerificationFailed

                    if options.Common.Json then
                        out (JsonOutput.serialize (JsonOutput.error "robustness" code messages))
                    else
                        for message in messages do
                            err ($"{Tool.ExecutableName}: {message}\n")

                    code
                | Ok report ->
                    let code =
                        if report.Passed then
                            ExitCode.Success
                        else
                            ExitCode.VerificationFailed

                    emit options.Common (JsonOutput.robustnessReport report code) (fun () ->
                        Render.robustness report options.Common.Verbose)

                    code

let private runLifecycle
    (name: string)
    (session: Session)
    (common: CommonOptions)
    (dryRun: bool)
    (check: bool)
    (force: bool)
    (run: Session -> PlanOptions -> bool -> LifecycleOutcome * Session)
    =
    // --check and --dry-run both calculate the full plan and write nothing.
    let outcome, _ = run session (planOptions force) (dryRun || check)
    let code = Api.exitCodeFor outcome check

    emit common (JsonOutput.lifecycle name outcome (dryRun || check) code) (fun () ->
        Render.lifecycle name outcome dryRun check common.Verbose)

    code

let private dispatch (command: Command) =
    match command with
    | Help topic ->
        out (Help.forCommand topic)
        ExitCode.Success
    | Command.Version ->
        out (Versioning.cliVersion + "\n")
        ExitCode.Success
    | Robustness options -> runRobustness options
    | _ ->

    let common =
        Command.common command
        |> Option.defaultValue
            { Repository = Environment.CurrentDirectory
              Json = false
              Verbose = false }

    match Api.openSession common.Repository with
    | Error error ->
        let code = ExitCode.EnvironmentError

        if common.Json then
            let messages =
                match error with
                | PayloadUnavailable problems -> problems |> List.map PayloadProblem.describe
                | RepositoryUnavailable reason -> [ reason ]

            out (JsonOutput.serialize (JsonOutput.error (Command.name command) code messages))
        else
            err (Render.sessionError error)

        code
    | Ok session ->
        match command with
        | Status options -> runStatus session options
        | Verify options -> runVerify session options
        | Doctor options -> runDoctor session options
        | Init options ->
            runLifecycle "init" session options.Common options.DryRun options.Check options.Force Api.initialize
        | Upgrade options ->
            runLifecycle "upgrade" session options.Common options.DryRun options.Check options.Force Api.performUpgrade
        | Robustness _
        | Help _
        | Command.Version -> ExitCode.Success

[<EntryPoint>]
let main argv =
    try
        match Parser.parse (List.ofArray argv) with
        | Error message ->
            err ($"{Tool.ExecutableName}: {message}\n\n")
            err Help.general
            int ExitCode.UsageError
        | Ok command -> int (dispatch command)
    with error ->
        err ($"{Tool.ExecutableName}: internal failure: {error.Message}\n")
        int ExitCode.InternalError
