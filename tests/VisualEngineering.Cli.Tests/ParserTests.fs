module VisualEngineering.Cli.Tests.ParserTests

open Xunit
open VisualEngineering.Cli

let private parse args = Parser.parse args

let private ok args =
    match parse args with
    | Ok command -> command
    | Error message -> failwithf "expected %A to parse, got: %s" args message

let private failure args =
    match parse args with
    | Ok command -> failwithf "expected %A to be rejected, got %A" args command
    | Error message -> message

[<Fact>]
let ``no arguments shows general help`` () = Assert.Equal(Help None, ok [])

[<Theory>]
[<InlineData("--help")>]
[<InlineData("-h")>]
let ``the help flag shows general help`` (flag: string) = Assert.Equal(Help None, ok [ flag ])

[<Theory>]
[<InlineData("--version")>]
[<InlineData("-V")>]
let ``the version flag requests the version`` (flag: string) =
    Assert.Equal(Command.Version, ok [ flag ])

[<Theory>]
[<InlineData("init")>]
[<InlineData("status")>]
[<InlineData("verify")>]
[<InlineData("upgrade")>]
[<InlineData("doctor")>]
[<InlineData("robustness")>]
let ``every documented command parses`` (name: string) =
    Assert.Equal(name, Command.name (ok [ name ]))

[<Theory>]
[<InlineData("init")>]
[<InlineData("status")>]
[<InlineData("verify")>]
[<InlineData("upgrade")>]
[<InlineData("doctor")>]
[<InlineData("robustness")>]
let ``command specific help is available both ways`` (name: string) =
    Assert.Equal(Help(Some name), ok [ name; "--help" ])
    Assert.Equal(Help(Some name), ok [ "help"; name ])

[<Fact>]
let ``init accepts its documented flags`` () =
    match ok [ "init"; "--dry-run"; "--json"; "--verbose"; "--force"; "--repo"; "/tmp/x" ] with
    | Init options ->
        Assert.True options.DryRun
        Assert.True options.Force
        Assert.True options.Common.Json
        Assert.True options.Common.Verbose
        Assert.Equal("/tmp/x", options.Common.Repository)
    | other -> failwithf "expected init, got %A" other

[<Fact>]
let ``verify accepts strict`` () =
    match ok [ "verify"; "--strict"; "--json" ] with
    | Verify options ->
        Assert.True options.Strict
        Assert.True options.Common.Json
    | other -> failwithf "expected verify, got %A" other

[<Fact>]
let ``upgrade accepts check`` () =
    match ok [ "upgrade"; "--check" ] with
    | Upgrade options -> Assert.True options.Check
    | other -> failwithf "expected upgrade, got %A" other

[<Fact>]
let ``robustness requires and accepts a manifest`` () =
    Assert.Contains("--manifest", failure [ "robustness" ])

    match ok [ "robustness"; "--manifest"; "visual-robustness.json"; "--json"; "--repo"; "/tmp/x" ] with
    | Robustness options ->
        Assert.Equal("visual-robustness.json", options.Manifest)
        Assert.True options.Common.Json
        Assert.Equal("/tmp/x", options.Common.Repository)
    | other -> failwithf "expected robustness, got %A" other

[<Fact>]
let ``robustness rejects lifecycle flags`` () =
    Assert.Contains("--force", failure [ "robustness"; "--manifest"; "x.json"; "--force" ])

[<Fact>]
let ``an unknown command is rejected`` () =
    Assert.Contains("unknown command", failure [ "install" ])

[<Fact>]
let ``an unknown option is rejected`` () =
    Assert.Contains("unknown option", failure [ "status"; "--wat" ])

[<Fact>]
let ``repo requires a value`` () =
    Assert.Contains("--repo requires a path", failure [ "status"; "--repo" ])

[<Theory>]
[<InlineData("status", "--dry-run")>]
[<InlineData("status", "--strict")>]
[<InlineData("verify", "--force")>]
[<InlineData("doctor", "--dry-run")>]
[<InlineData("init", "--strict")>]
let ``flags a command does not support are rejected rather than ignored`` (command: string) (flag: string) =
    Assert.Contains(flag, failure [ command; flag ])
