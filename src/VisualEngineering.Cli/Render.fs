namespace VisualEngineering.Cli

open System
open VisualEngineering.Core

/// Human readable output. Concise by default; detail only behind --verbose.
[<RequireQualifiedAccess>]
module Render =

    let private field (label: string) (value: string) = "  " + label.PadRight 23 + value

    let private bullet (text: string) = "  - " + text

    let status (report: StatusReport) =
        let installed =
            report.InstalledVersion
            |> Option.filter (String.IsNullOrWhiteSpace >> not)
            |> Option.defaultValue "not installed"

        let configuration =
            match report.ConfigurationVersion, report.ConfigurationValid with
            | Some version, true -> $"version {version} (valid)"
            | Some version, false -> $"version {version} (unreadable)"
            | None, _ -> "not present"

        let context =
            match report.InstalledContextVersion with
            | Some version when version = report.PackagedContextVersion -> version
            | Some version -> $"{version} (packaged {report.PackagedContextVersion})"
            | None -> $"not installed (packaged {report.PackagedContextVersion})"

        let upgrade =
            match report.AvailableUpgrade with
            | Some available -> $"{available.Version} available"
            | None -> "none"

        Text.lines
            [ report.DisplayName
              ""
              field "CLI version:" report.CliVersion
              field "Installed version:" installed
              field "Configuration:" configuration
              field "Context:" context
              field "Installation state:" (InstallationState.toString report.State)
              field "Required artifacts:" (if report.ArtifactsValid then "valid" else "invalid")
              field "Integration:" (if report.IntegrationsValid then "valid" else "not registered")
              field "Verification:" (if report.Verification.Passed then "passed" else "failed")
              field "Upgrade:" upgrade
              ""
              field "Context directory:" (RepoPath.value report.ContextDirectory)
              field "Manifest:" (RepoPath.value report.ManifestPath)
              field "Research documents:" (string report.ResearchDocuments) ]

    let verification (report: VerificationReport) (verbose: bool) =
        let applicable =
            report.Checks
            |> List.filter (fun check -> report.Strict || not check.StrictOnly)

        let shown =
            if verbose then
                applicable
            else
                applicable |> List.filter (fun check -> not check.Passed)

        let header =
            if report.Passed then
                if report.Strict then
                    "Verification passed (strict)."
                else
                    "Verification passed."
            else
                "Verification failed."

        Text.lines (
            [ header ]
            @ [ for check in shown ->
                    let mark = if check.Passed then "ok  " else "FAIL"
                    $"  {mark} {check.Name}: {check.Detail}" ]
        )

    let diagnostics (report: DiagnosticReport) (strict: bool) (verbose: bool) =
        let shown =
            if verbose then
                report.Findings
            else
                report.Findings
                |> List.filter (fun finding -> finding.Severity <> Severity.Information)

        let failing =
            report.Findings
            |> List.filter (fun finding ->
                finding.Severity = Severity.Error || (strict && finding.Severity = Severity.Warning))

        let header =
            if List.isEmpty failing then
                if List.isEmpty shown then
                    $"{Tool.DisplayName} is healthy."
                else
                    $"{Tool.DisplayName} is healthy, with notes."
            else
                $"{Tool.DisplayName} reported {List.length failing} problem(s)."

        let body =
            [ for finding in shown do
                  let label = (Severity.toString finding.Severity).ToUpperInvariant()
                  $"  [{label}] {finding.Title} ({finding.Code})"
                  $"      {finding.Detail}"

                  match finding.Remedy with
                  | Some remedy -> $"      fix: {remedy}"
                  | None -> () ]

        Text.lines ([ header ] @ body)

    let robustness (report: RobustnessReport) (verbose: bool) =
        let headline =
            if report.Passed then
                $"Perceptual robustness passed for {report.StateCount} semantic state(s)."
            else
                $"Perceptual robustness failed for one or more of {report.StateCount} semantic state(s)."

        let stateLines =
            [ for state in report.States do
                  let mark =
                      if state.BaselineValid
                         && (state.Criticality = Informational || state.SingleChannelSurvivable)
                         && (state.ScenarioResults |> List.forall _.Survives) then
                          "ok"
                      else
                          "FAIL"

                  let boundary =
                      match state.FailureBoundary with
                      | Some value -> string value
                      | None -> "unbounded"

                  yield
                      $"  {mark.PadRight 4} {state.StateId}: {SemanticCriticality.toString state.Criticality}, failure boundary {boundary}"

                  if verbose || mark = "FAIL" then
                      for finding in state.Findings do
                          yield $"       - {finding}" ]

        Text.lines ([ headline ] @ stateLines)

    let lifecycle (command: string) (outcome: LifecycleOutcome) (dryRun: bool) (check: bool) (verbose: bool) =
        let plan = outcome.Plan

        let changeLines =
            if verbose || dryRun || check then
                [ for change in plan.Changes -> bullet (PlannedChange.describe change) ]
            else
                []

        // Migrations already appear in the change list; summarise them only when it is hidden.
        let migrationLines =
            if List.isEmpty changeLines then
                [ for fromVersion, toVersion in plan.Migrations ->
                      bullet $"migrated configuration {fromVersion} -> {toVersion}" ]
            else
                []

        let preservedLines =
            if verbose || dryRun then
                [ for preserved in plan.Preserved ->
                      bullet $"preserved {RepoPath.value preserved.Path}: {preserved.Reason}" ]
            else
                []

        if not (Plan.isExecutable plan) then
            Text.lines (
                [ $"{command} cannot continue." ]
                @ [ for blocker in plan.Blockers -> bullet (PlanBlocker.describe blocker) ]
            )
        else

        let headline =
            match outcome.Execution, List.isEmpty plan.Changes with
            | _, true -> $"{Tool.DisplayName} is already up to date. No changes required."
            | None, false when check -> $"{List.length plan.Changes} change(s) required."
            | None, false -> $"{List.length plan.Changes} change(s) would be applied. Nothing was written."
            | Some(Executed applied), _ -> $"Applied {List.length applied} change(s)."
            | Some(Aborted _), _ -> $"{command} was aborted."
            | Some(PartiallyApplied(applied, failed, reason)), _ ->
                $"{command} failed after {List.length applied} change(s) at '{PlannedChange.describe failed}': {reason}"

        let verificationLines =
            match outcome.Verification with
            | Some report when not report.Passed -> [ ""; verification report verbose |> fun text -> text.TrimEnd '\n' ]
            | Some report when verbose -> [ ""; verification report verbose |> fun text -> text.TrimEnd '\n' ]
            | _ -> []

        Text.lines ([ headline ] @ migrationLines @ changeLines @ preservedLines @ verificationLines)

    let sessionError (error: SessionError) =
        match error with
        | PayloadUnavailable problems ->
            Text.lines (
                [ "The packaged Visual Engineering context could not be loaded." ]
                @ [ for problem in problems -> bullet (PayloadProblem.describe problem) ]
                @ [ ""
                    $"Reinstall the package: npx --yes {Tool.PackageName}@latest doctor" ]
            )
        | RepositoryUnavailable reason -> Text.lines [ $"The repository could not be opened: {reason}" ]
