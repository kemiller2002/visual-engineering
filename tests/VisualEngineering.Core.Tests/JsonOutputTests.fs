module VisualEngineering.Core.Tests.JsonOutputTests

open System.Text.Json
open Xunit
open VisualEngineering.Core
open VisualEngineering.Core.Tests.Fixtures

let private parse (text: string) = JsonDocument.Parse text

let private envelope (text: string) (command: string) =
    use document = parse text
    let root = document.RootElement
    Assert.Equal(Versioning.OutputSchemaVersion, Json.tryInt "schemaVersion" root |> Option.defaultValue -1)
    Assert.Equal(Some Tool.Id, Json.tryString "tool" root)
    Assert.Equal(Some Tool.PackageName, Json.tryString "package" root)
    Assert.Equal(Some command, Json.tryString "command" root)
    Assert.True((Json.tryString "cliVersion" root).IsSome)
    Assert.True((Json.tryInt "exitCode" root).IsSome)

let private prepared () =
    let temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = System.IO.Path.Combine(temp.Path, "repo")
    System.IO.Directory.CreateDirectory repository |> ignore
    createRepository repository |> ignore
    temp, payload, repository

[<Fact>]
let ``every command emits a valid, consistently shaped document`` () =
    let temp, payload, repository = prepared ()
    use temp = temp

    let before = session payload repository
    let initOutcome, installed = Api.initialize before PlanOptions.defaults false

    envelope (JsonOutput.serialize (JsonOutput.status (Api.getStatus installed) ExitCode.Success)) "status"
    envelope (JsonOutput.serialize (JsonOutput.verifyReport (Api.verify installed true) ExitCode.Success)) "verify"
    envelope (JsonOutput.serialize (JsonOutput.doctorReport (Api.diagnose installed) ExitCode.Success)) "doctor"
    envelope (JsonOutput.serialize (JsonOutput.lifecycle "init" initOutcome false ExitCode.Success)) "init"

    let upgradeOutcome, _ = Api.performUpgrade installed PlanOptions.defaults true
    envelope (JsonOutput.serialize (JsonOutput.lifecycle "upgrade" upgradeOutcome true ExitCode.Success)) "upgrade"

[<Fact>]
let ``a dry run document lists the planned changes`` () =
    let temp, payload, repository = prepared ()
    use temp = temp

    let outcome, _ = Api.initialize (session payload repository) PlanOptions.defaults true
    let text = JsonOutput.serialize (JsonOutput.lifecycle "init" outcome true ExitCode.Success)

    use document = parse text
    let root = document.RootElement
    let plan = (Json.tryProperty "plan" root).Value

    Assert.True(Json.tryBool "dryRun" root = Some true)
    Assert.True((Json.tryInt "changeCount" plan |> Option.defaultValue 0) > 0)
    Assert.NotEmpty(Json.arrayItems "changes" plan)
    Assert.Empty(Json.arrayItems "blockers" plan)

[<Fact>]
let ``an error document reports the failure`` () =
    let text =
        JsonOutput.serialize (JsonOutput.error "status" ExitCode.EnvironmentError [ "payload missing" ])

    envelope text "status"
    use document = parse text
    Assert.Equal(Some true, Json.tryBool "error" document.RootElement)
    Assert.Single(Json.arrayItems "messages" document.RootElement) |> ignore

[<Fact>]
let ``exit codes keep their documented values`` () =
    Assert.Equal(0, int ExitCode.Success)
    Assert.Equal(1, int ExitCode.InternalError)
    Assert.Equal(2, int ExitCode.UsageError)
    Assert.Equal(3, int ExitCode.VerificationFailed)
    Assert.Equal(4, int ExitCode.ChangesRequired)
    Assert.Equal(5, int ExitCode.InstallationBlocked)
    Assert.Equal(6, int ExitCode.EnvironmentError)
    Assert.Equal(7, int ExitCode.UnsupportedPlatform)
