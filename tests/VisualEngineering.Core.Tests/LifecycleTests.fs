module VisualEngineering.Core.Tests.LifecycleTests

open Xunit
open VisualEngineering.Core
open VisualEngineering.Core.Tests.Fixtures

let private newRepository (temp: TempDirectory) name =
    let root = System.IO.Path.Combine(temp.Path, name)
    System.IO.Directory.CreateDirectory root |> ignore
    createRepository root

let private initialize payload repository =
    let session = session payload repository
    Api.initialize session PlanOptions.defaults false

[<Fact>]
let ``an untouched repository reports NotInstalled`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"

    Assert.Equal(NotInstalled, (session payload repository).Repository.State)

[<Fact>]
let ``a dry run calculates a full plan and writes nothing`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    let before = snapshot repository

    let outcome, _ = Api.initialize (session payload repository) PlanOptions.defaults true

    Assert.NotEmpty outcome.Plan.Changes
    Assert.Empty outcome.Plan.Blockers
    Assert.True outcome.Execution.IsNone
    Assert.Equal<(string * string) list>(before, snapshot repository)

[<Fact>]
let ``init installs the capability and verification passes`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"

    let outcome, session = initialize payload repository

    Assert.Equal(Some(Executed outcome.Plan.Changes), outcome.Execution)
    Assert.True (Api.verify session true).Passed

    Assert.True(exists repository ".echelon/visual-engineering.json")
    Assert.True(exists repository ".echelon/visual-engineering.config.json")
    Assert.True(exists repository ".visual-engineering/UI-FOUNDATIONS.md")
    Assert.True(exists repository "AGENTS.md")
    Assert.Contains(".visual-engineering/", read repository ".gitignore")

[<Fact>]
let ``init is idempotent`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"

    initialize payload repository |> ignore
    let afterFirst = snapshot repository

    let outcome, _ = initialize payload repository

    Assert.Empty outcome.Plan.Changes
    Assert.Equal<(string * string) list>(afterFirst, snapshot repository)

[<Fact>]
let ``init preserves user content in shared files`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    write repository "AGENTS.md" "# House rules\n\nUse pnpm.\n"
    write repository ".gitignore" "node_modules/\n"

    initialize payload repository |> ignore

    Assert.Contains("Use pnpm.", read repository "AGENTS.md")
    Assert.Contains("node_modules/", read repository ".gitignore")
    Assert.Contains(ManagedBlock.Marker, read repository "AGENTS.md")

[<Fact>]
let ``a deleted file is reported and repaired`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    initialize payload repository |> ignore

    System.IO.File.Delete(System.IO.Path.Combine(repository, ".visual-engineering", "UI-FOUNDATIONS.md"))

    let damaged = session payload repository
    let report = Api.verify damaged false
    Assert.False report.Passed

    let diagnosis = Api.diagnose damaged
    Assert.False diagnosis.Healthy

    Assert.Contains(
        diagnosis.Findings,
        fun finding -> finding.Code = "file-missing" && finding.Severity = Severity.Error
    )

    let outcome, repaired = initialize payload repository
    Assert.NotEmpty outcome.Plan.Changes
    Assert.True (Api.verify repaired true).Passed

[<Fact>]
let ``a locally modified tool owned file blocks init until forced`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    initialize payload repository |> ignore

    write repository ".visual-engineering/UI-FOUNDATIONS.md" "hand edited\n"

    let blockedOutcome, _ = initialize payload repository
    Assert.NotEmpty blockedOutcome.Plan.Blockers
    Assert.Equal(ExitCode.InstallationBlocked, Api.exitCodeFor blockedOutcome false)
    Assert.Equal("hand edited\n", read repository ".visual-engineering/UI-FOUNDATIONS.md")

    let forced, forcedSession = Api.initialize (session payload repository) { Force = true } false
    Assert.Empty forced.Plan.Blockers
    Assert.True (Api.verify forcedSession true).Passed

[<Fact>]
let ``an edit inside a managed region is detected as a shared file conflict`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    initialize payload repository |> ignore

    let agents = read repository "AGENTS.md"
    write repository "AGENTS.md" (agents.Replace("Inspect the product", "INSPECT the product"))

    let outcome, _ = initialize payload repository

    Assert.Contains(
        outcome.Plan.Blockers,
        function
        | LocallyModifiedRegion(path, integration) ->
            RepoPath.value path = "AGENTS.md" && integration = Desired.AgentsIntegration
        | _ -> false
    )

[<Fact>]
let ``check reports required changes without writing`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    let before = snapshot repository

    let outcome, _ = Api.initialize (session payload repository) PlanOptions.defaults true

    Assert.Equal(ExitCode.ChangesRequired, Api.exitCodeFor outcome true)
    Assert.Equal<(string * string) list>(before, snapshot repository)

[<Fact>]
let ``status is read only`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    initialize payload repository |> ignore
    let before = snapshot repository

    let session = session payload repository
    Api.getStatus session |> ignore
    Api.verify session true |> ignore
    Api.diagnose session |> ignore

    Assert.Equal<(string * string) list>(before, snapshot repository)

[<Fact>]
let ``a user chosen context directory is honoured`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"

    write
        repository
        ".echelon/visual-engineering.config.json"
        (Json.serialize (
            JObject
                [ "schemaVersion", JInt 1
                  "tool", JString "visual-engineering"
                  "configurationVersion", JInt 3
                  "contextDirectory", JString "docs/ve-context"
                  "integrations", JObject [ "agentsFile", JNull; "gitignore", JBool false ]
                  "teamNote", JString "kept by the tool" ]
         ))

    let _, session = initialize payload repository

    Assert.True(exists repository "docs/ve-context/UI-FOUNDATIONS.md")
    Assert.False(exists repository "AGENTS.md")
    Assert.False(exists repository ".gitignore")
    Assert.Contains("kept by the tool", read repository ".echelon/visual-engineering.config.json")
    Assert.True (Api.verify session true).Passed
