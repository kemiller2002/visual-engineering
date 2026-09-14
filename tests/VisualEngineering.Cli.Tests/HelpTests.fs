module VisualEngineering.Cli.Tests.HelpTests

open Xunit
open VisualEngineering.Core
open VisualEngineering.Cli

let private commands = [ "init"; "status"; "verify"; "upgrade"; "doctor" ]

[<Fact>]
let ``general help lists every command`` () =
    for command in commands do
        Assert.Contains(command, Help.general)

[<Fact>]
let ``general help documents the package and executable names`` () =
    Assert.Contains(Tool.PackageName, Help.general)
    Assert.Contains(Tool.ExecutableName, Help.general)

[<Fact>]
let ``general help documents every exit code`` () =
    for code in
        [ ExitCode.Success
          ExitCode.InternalError
          ExitCode.UsageError
          ExitCode.VerificationFailed
          ExitCode.ChangesRequired
          ExitCode.InstallationBlocked
          ExitCode.EnvironmentError
          ExitCode.UnsupportedPlatform ] do
        Assert.Contains($"  {int code}  ", Help.general)

[<Theory>]
[<InlineData("init")>]
[<InlineData("status")>]
[<InlineData("verify")>]
[<InlineData("upgrade")>]
[<InlineData("doctor")>]
let ``command help names the command, its side effects and an example`` (command: string) =
    let text = Help.forCommand (Some command)
    Assert.Contains($"{Tool.ExecutableName} {command}", text)
    Assert.Contains("SIDE EFFECTS", text)
    Assert.Contains("EXAMPLES", text)
    Assert.Contains($"npx {Tool.PackageName} {command}", text)

[<Fact>]
let ``read only commands say so`` () =
    for command in [ "status"; "verify"; "doctor" ] do
        Assert.Contains("never modifies the repository", Help.forCommand (Some command))

[<Fact>]
let ``commands that change the repository document dry run and check`` () =
    for command in [ "init"; "upgrade" ] do
        let text = Help.forCommand (Some command)
        Assert.Contains("--dry-run", text)
        Assert.Contains("--check", text)
        Assert.Contains("--force", text)

[<Fact>]
let ``init help states that it is idempotent`` () =
    Assert.Contains("idempotent", Help.init)
