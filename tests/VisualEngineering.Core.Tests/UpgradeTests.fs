module VisualEngineering.Core.Tests.UpgradeTests

open Xunit
open VisualEngineering.Core
open VisualEngineering.Core.Tests.Fixtures

let private newRepository (temp: TempDirectory) name =
    let root = System.IO.Path.Combine(temp.Path, name)
    System.IO.Directory.CreateDirectory root |> ignore
    createRepository root

[<Fact>]
let ``upgrade refuses to run when nothing is installed`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"

    let outcome, _ = Api.performUpgrade (session payload repository) PlanOptions.defaults false

    Assert.NotEmpty outcome.Plan.Blockers
    Assert.Equal(ExitCode.InstallationBlocked, Api.exitCodeFor outcome false)
    Assert.False(exists repository ".echelon/visual-engineering.json")

[<Fact>]
let ``a legacy ve-context installation is detected as configuration version 1`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "2.0.0"
    let repository = newRepository temp "repo"
    createLegacyInstallation repository "1.0.0" |> ignore

    match (session payload repository).Repository.State with
    | UpgradeRequired(installed, available) ->
        Assert.Equal(Versioning.LegacyConfigurationVersion, installed.ConfigurationVersion)
        Assert.Equal("1.0.0", installed.ContextVersion)
        Assert.Equal("2.0.0", available.ContextVersion)
    | state -> failwithf "expected an upgrade to be required, got %s" (InstallationState.toString state)

[<Fact>]
let ``upgrading several versions runs every transition in order`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "2.0.0"
    let repository = newRepository temp "repo"
    createLegacyInstallation repository "1.0.0" |> ignore

    let outcome, upgraded = Api.performUpgrade (session payload repository) PlanOptions.defaults false

    Assert.Equal<(int * int) list>([ 1, 2; 2, 3 ], outcome.Plan.Migrations)
    Assert.True (Api.verify upgraded true).Passed
    Assert.Contains("\"configurationVersion\": 3", read repository ".echelon/visual-engineering.json")

[<Fact>]
let ``upgrade preserves user owned content`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "2.0.0"
    let repository = newRepository temp "repo"
    createLegacyInstallation repository "1.0.0" |> ignore
    write repository "AGENTS.md" "# House rules\n\nNever force push.\n"
    write repository ".gitignore" "node_modules/\ncoverage/\n"
    write repository "docs/notes.md" "team notes\n"

    Api.performUpgrade (session payload repository) PlanOptions.defaults false |> ignore

    Assert.Contains("Never force push.", read repository "AGENTS.md")
    Assert.Contains("coverage/", read repository ".gitignore")
    Assert.Equal("team notes\n", read repository "docs/notes.md")

[<Fact>]
let ``upgrade from the current version makes no changes`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore
    let before = snapshot repository

    let outcome, _ = Api.performUpgrade (session payload repository) PlanOptions.defaults false

    Assert.Empty outcome.Plan.Changes
    Assert.Equal<(string * string) list>(before, snapshot repository)

[<Fact>]
let ``a newer context version is installed by upgrade`` () =
    use temp = new TempDirectory()
    let first = createPayload (System.IO.Path.Combine(temp.Path, "payload-1")) "1.0.0"
    let repository = newRepository temp "repo"
    Api.initialize (session first repository) PlanOptions.defaults false |> ignore

    let second = createPayload (System.IO.Path.Combine(temp.Path, "payload-2")) "2.0.0"

    match (session second repository).Repository.State with
    | UpgradeRequired _ -> ()
    | state -> failwithf "expected an upgrade to be required, got %s" (InstallationState.toString state)

    let _, upgraded = Api.performUpgrade (session second repository) PlanOptions.defaults false

    Assert.True (Api.verify upgraded true).Passed
    Assert.Contains("\"contextVersion\": \"2.0.0\"", read repository ".echelon/visual-engineering.json")
    Assert.Contains("2.0.0", read repository ".visual-engineering/context.json")

[<Fact>]
let ``a failed migration precondition stops the upgrade before anything is written`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "2.0.0"
    let repository = newRepository temp "repo"
    createLegacyInstallation repository "1.0.0" |> ignore
    write repository ".visual-engineering/context.json" "{ this is not json"
    let before = snapshot repository

    let outcome, _ = Api.performUpgrade (session payload repository) PlanOptions.defaults false

    Assert.Contains(
        outcome.Plan.Blockers,
        function
        | MigrationPreconditionFailed(1, 2, _) -> true
        | _ -> false
    )

    Assert.Equal<(string * string) list>(before, snapshot repository)

[<Fact>]
let ``an installation from a newer release is reported as invalid rather than downgraded`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore

    let manifest = read repository ".echelon/visual-engineering.json"

    write
        repository
        ".echelon/visual-engineering.json"
        (manifest.Replace("\"configurationVersion\": 3", "\"configurationVersion\": 99"))

    match (session payload repository).Repository.State with
    | Invalid problems ->
        Assert.Contains(
            problems,
            function
            | UnsupportedConfigurationVersion 99 -> true
            | _ -> false
        )
    | state -> failwithf "expected an invalid installation, got %s" (InstallationState.toString state)

[<Fact>]
let ``an unreadable installation manifest is reported rather than overwritten`` () =
    use temp = new TempDirectory()
    let payload = createPayload (System.IO.Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newRepository temp "repo"
    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore
    write repository ".echelon/visual-engineering.json" "{ broken"

    let damaged = session payload repository

    match damaged.Repository.State with
    | Invalid problems ->
        Assert.Contains(
            problems,
            function
            | ManifestUnreadable _ -> true
            | _ -> false
        )
    | state -> failwithf "expected an invalid installation, got %s" (InstallationState.toString state)

    Assert.False (Api.verify damaged false).Passed
