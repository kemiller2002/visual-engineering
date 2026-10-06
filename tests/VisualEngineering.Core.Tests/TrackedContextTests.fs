module VisualEngineering.Core.Tests.TrackedContextTests

// The installed context is required content: `verify` fails without it and the managed agent
// briefing tells agents to read it. These tests use a real git repository, because the
// failure they guard against only exists in a clean checkout of a committed installation.

open System.Diagnostics
open System.IO
open Xunit
open VisualEngineering.Core
open VisualEngineering.Core.Tests.Fixtures

type private GitResult = { ExitCode: int; Output: string }

let private git (directory: string) (arguments: string list) =
    let start =
        ProcessStartInfo(
            "git",
            [ "-c"; "user.name=Visual Engineering Tests"; "-c"; "user.email=tests@example.invalid" ]
            @ [ "-c"; "core.autocrlf=false"; "-c"; "commit.gpgsign=false" ]
            @ arguments,
            WorkingDirectory = directory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        )

    match Process.Start start with
    | null -> failwith "git could not be started"
    | proc ->
        use proc = proc
        let output = proc.StandardOutput.ReadToEnd() + proc.StandardError.ReadToEnd()
        proc.WaitForExit()
        { ExitCode = proc.ExitCode; Output = output }

let private gitOk directory arguments =
    let result = git directory arguments
    Assert.True(result.ExitCode = 0, sprintf "git %s failed: %s" (String.concat " " arguments) result.Output)
    result.Output

let private newGitRepository (temp: TempDirectory) name =
    let root = Path.Combine(temp.Path, name)
    Directory.CreateDirectory root |> ignore
    gitOk root [ "init"; "-q" ] |> ignore
    root

let private isIgnored (repository: string) (relative: string) =
    (git repository [ "check-ignore"; "-q"; "--no-index"; relative ]).ExitCode = 0

let private contextFiles (payload: Payload) =
    payload.Artifacts
    |> List.map (fun artifact -> ".visual-engineering/" + RepoPath.value artifact.File)

/// Commits everything git does not ignore, clones the result, and opens the clone.
let private cleanCheckout (temp: TempDirectory) (payload: Payload) (repository: string) =
    gitOk repository [ "add"; "-A" ] |> ignore
    gitOk repository [ "commit"; "-q"; "-m"; "install visual engineering" ] |> ignore
    let clone = Path.Combine(temp.Path, "clone")
    gitOk temp.Path [ "clone"; "-q"; repository; clone ] |> ignore
    session payload clone

/// The managed `.gitignore` region 1.0.0 wrote: it ignored the required context directory.
let private ignoringRegionBody =
    String.concat
        "\n"
        [ "# Installed Visual Engineering context. Refresh it with:"
          $"#   npx {Tool.PackageName} init"
          ".visual-engineering/" ]

/// Rewrites an installation so it is exactly what 1.0.0 left behind: the ignoring region on
/// disk, and that region's hash recorded in the manifest as what the tool last wrote.
let private regressToIgnoringRegion (repository: string) =
    let current = read repository ".gitignore"
    let region = ManagedBlock.tryExtract HashComment current |> Option.get
    let installedHash = ManagedBlock.bodyHash region.Body
    write repository ".gitignore" (ManagedBlock.upsert HashComment ignoringRegionBody (Some current))

    read repository ".echelon/visual-engineering.json"
    |> fun manifest -> manifest.Replace(installedHash, ManagedBlock.bodyHash ignoringRegionBody)
    |> write repository ".echelon/visual-engineering.json"

[<Fact>]
let ``init leaves every required context file trackable by git`` () =
    use temp = new TempDirectory()
    let payload = createPayload (Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newGitRepository temp "repo"

    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore

    let ignored = contextFiles payload |> List.filter (isIgnored repository)
    Assert.True(List.isEmpty ignored, sprintf "required context files are gitignored: %A" ignored)

[<Fact>]
let ``a clean checkout of a committed installation verifies`` () =
    use temp = new TempDirectory()
    let payload = createPayload (Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newGitRepository temp "repo"
    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore

    let report = Api.verify (cleanCheckout temp payload repository) false

    let failed =
        report.Checks
        |> List.filter (fun check -> not check.Passed && not check.StrictOnly)
        |> List.map (fun check -> check.Name + ": " + check.Detail)

    Assert.True(report.Passed, sprintf "clean checkout failed verification: %A" failed)

[<Fact>]
let ``init keeps the context trackable when another rule ignores dot directories`` () =
    use temp = new TempDirectory()
    let payload = createPayload (Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newGitRepository temp "repo"
    write repository ".gitignore" ".*\n!.gitignore\n!.echelon/\n"

    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore

    let ignored = contextFiles payload |> List.filter (isIgnored repository)
    Assert.True(List.isEmpty ignored, sprintf "required context files are gitignored: %A" ignored)

[<Fact>]
let ``upgrade replaces the region that ignored the context in an earlier release`` () =
    use temp = new TempDirectory()
    let payload = createPayload (Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newGitRepository temp "repo"
    write repository ".gitignore" "node_modules/\n"
    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore
    regressToIgnoringRegion repository
    Assert.True(isIgnored repository ".visual-engineering/UI-FOUNDATIONS.md", "the regressed region should ignore the context")

    let outcome, upgraded = Api.performUpgrade (session payload repository) PlanOptions.defaults false

    Assert.Empty outcome.Plan.Blockers
    Assert.True (Api.verify upgraded true).Passed
    Assert.Contains("node_modules/", read repository ".gitignore")
    Assert.Empty(contextFiles payload |> List.filter (isIgnored repository))
    Assert.True (Api.verify (cleanCheckout temp payload repository) false).Passed

[<Fact>]
let ``default verify reports the ignoring region as stale and strict verify fails on it`` () =
    use temp = new TempDirectory()
    let payload = createPayload (Path.Combine(temp.Path, "payload")) "1.0.0"
    let repository = newGitRepository temp "repo"
    Api.initialize (session payload repository) PlanOptions.defaults false |> ignore
    regressToIgnoringRegion repository

    let installed = session payload repository

    Assert.True (Api.verify installed false).Passed
    Assert.False (Api.verify installed true).Passed

    let gitignore =
        installed.Repository.Artifacts
        |> List.find (fun artifact -> RepoPath.value artifact.Desired.Path = ".gitignore")

    Assert.Equal(Stale, gitignore.Status)
