namespace VisualEngineering.Core

/// Machine readable command output.
///
/// Every document shares one envelope (`schemaVersion`, `tool`, `package`, `command`,
/// `cliVersion`, `exitCode`) so agents and CI can parse any command the same way.
/// The schemas are versioned: a breaking change increments `schemaVersion`.
[<RequireQualifiedAccess>]
module JsonOutput =

    let private envelope (command: string) (exitCode: ExitCode) (fields: (string * JsonValue) list) =
        JObject(
            [ "schemaVersion", JInt Versioning.OutputSchemaVersion
              "tool", JString Tool.Id
              "package", JString Tool.PackageName
              "command", JString command
              "cliVersion", JString Versioning.cliVersion
              "exitCode", JInt(int exitCode) ]
            @ fields
        )

    let private installedVersion (version: InstalledVersion) =
        JObject
            [ "version", JString version.Version
              "configurationVersion", JInt version.ConfigurationVersion
              "contextVersion", JString version.ContextVersion ]

    let private availableVersion (version: AvailableVersion) =
        JObject
            [ "version", JString version.Version
              "configurationVersion", JInt version.ConfigurationVersion
              "contextVersion", JString version.ContextVersion ]

    let private state (value: InstallationState) =
        let details =
            match value with
            | NotInstalled -> []
            | Installed version -> [ "installed", installedVersion version ]
            | UpgradeRequired(installed, available) ->
                [ "installed", installedVersion installed
                  "available", availableVersion available ]
            | Invalid problems ->
                [ "problems", JArray [ for problem in problems -> JString(InstallationProblem.describe problem) ] ]

        JObject([ "status", JString(InstallationState.toString value) ] @ details)

    let private verification (report: VerificationReport) =
        JObject
            [ "passed", JBool report.Passed
              "strict", JBool report.Strict
              "checks",
              JArray
                  [ for check in report.Checks ->
                        JObject
                            [ "name", JString check.Name
                              "passed", JBool check.Passed
                              "strictOnly", JBool check.StrictOnly
                              "detail", JString check.Detail ] ] ]

    let private change (value: PlannedChange) =
        JObject
            [ "kind", JString(PlannedChange.kind value)
              "path", (value |> PlannedChange.path |> Option.map RepoPath.value |> Json.ofStringOption)
              "description", JString(PlannedChange.describe value) ]

    let private plan (value: Plan) =
        JObject
            [ "changeCount", JInt(List.length value.Changes)
              "changes", JArray [ for item in value.Changes -> change item ]
              "blockers", JArray [ for blocker in value.Blockers -> JString(PlanBlocker.describe blocker) ]
              "migrations",
              JArray
                  [ for fromVersion, toVersion in value.Migrations ->
                        JObject [ "from", JInt fromVersion; "to", JInt toVersion ] ]
              "preserved",
              JArray
                  [ for preserved in value.Preserved ->
                        JObject
                            [ "path", JString(RepoPath.value preserved.Path)
                              "reason", JString preserved.Reason ] ] ]

    let private execution (result: ExecutionResult option) =
        match result with
        | None -> JObject [ "applied", JBool false; "reason", JString "dry-run" ]
        | Some(Executed applied) -> JObject [ "applied", JBool true; "changeCount", JInt(List.length applied) ]
        | Some(Aborted blockers) ->
            JObject
                [ "applied", JBool false
                  "reason", JString "blocked"
                  "blockers", JArray [ for blocker in blockers -> JString(PlanBlocker.describe blocker) ] ]
        | Some(PartiallyApplied(applied, failed, reason)) ->
            JObject
                [ "applied", JBool false
                  "reason", JString "partially-applied"
                  "appliedCount", JInt(List.length applied)
                  "failedChange", JString(PlannedChange.describe failed)
                  "detail", JString reason ]

    let status (report: StatusReport) (exitCode: ExitCode) =
        envelope
            "status"
            exitCode
            [ "displayName", JString report.DisplayName
              "installedVersion", Json.ofStringOption report.InstalledVersion
              "configurationVersion",
              (match report.ConfigurationVersion with
               | Some version -> JInt version
               | None -> JNull)
              "installedContextVersion", Json.ofStringOption report.InstalledContextVersion
              "packagedContextVersion", JString report.PackagedContextVersion
              "sourceCommit", JString report.SourceCommit
              "researchDocuments", JInt report.ResearchDocuments
              "contextDirectory", JString(RepoPath.value report.ContextDirectory)
              "manifestPath", JString(RepoPath.value report.ManifestPath)
              "configurationPath", JString(RepoPath.value report.ConfigurationPath)
              "state", state report.State
              "configurationValid", JBool report.ConfigurationValid
              "artifactsValid", JBool report.ArtifactsValid
              "integrationsValid", JBool report.IntegrationsValid
              "verification", verification report.Verification
              "availableUpgrade",
              (match report.AvailableUpgrade with
               | Some version -> availableVersion version
               | None -> JNull) ]

    let verifyReport (report: VerificationReport) (exitCode: ExitCode) =
        envelope
            "verify"
            exitCode
            [ "verification", verification report
              "problems", JArray [ for problem in report.Problems -> JString(InstallationProblem.describe problem) ] ]

    let doctorReport (report: DiagnosticReport) (exitCode: ExitCode) =
        envelope
            "doctor"
            exitCode
            [ "healthy", JBool report.Healthy
              "findings",
              JArray
                  [ for finding in report.Findings ->
                        JObject
                            [ "severity", JString(Severity.toString finding.Severity)
                              "code", JString finding.Code
                              "title", JString finding.Title
                              "detail", JString finding.Detail
                              "remedy", Json.ofStringOption finding.Remedy ] ] ]

    let lifecycle (command: string) (outcome: LifecycleOutcome) (dryRun: bool) (exitCode: ExitCode) =
        envelope
            command
            exitCode
            [ "dryRun", JBool dryRun
              "plan", plan outcome.Plan
              "execution", execution outcome.Execution
              "verification",
              (match outcome.Verification with
               | Some report -> verification report
               | None -> JNull) ]

    let error (command: string) (exitCode: ExitCode) (messages: string list) =
        envelope command exitCode [ "error", JBool true; "messages", Json.ofStrings messages ]

    let serialize = Json.serialize
