module VisualEngineering.Core.Tests.PrimitiveTests

open Xunit
open VisualEngineering.Core

[<Theory>]
[<InlineData("../escape")>]
[<InlineData("a/../../b")>]
[<InlineData("/absolute")>]
[<InlineData("C:/windows")>]
[<InlineData("")>]
[<InlineData("   ")>]
let ``repository paths reject traversal and absolute forms`` (raw: string) =
    match RepoPath.tryCreate raw with
    | Ok path -> failwithf "expected %s to be rejected, got %s" raw (RepoPath.value path)
    | Error _ -> ()

[<Theory>]
[<InlineData(".visual-engineering/context.json", ".visual-engineering/context.json")>]
[<InlineData(".echelon\\visual-engineering.json", ".echelon/visual-engineering.json")>]
[<InlineData("a//b///c", "a/b/c")>]
let ``repository paths normalize separators`` (raw: string) (expected: string) =
    Assert.Equal(expected, RepoPath.value (RepoPath.create raw))

[<Fact>]
let ``resolving a repository path stays inside the root`` () =
    use temp = new Fixtures.TempDirectory()
    let resolved = RepoPath.toAbsolute temp.Path (RepoPath.create "a/b.txt")
    Assert.StartsWith(temp.Path, resolved)

[<Fact>]
let ``hashing ignores line ending translation`` () =
    Assert.Equal(Hash.ofText "a\nb\n", Hash.ofText "a\r\nb\r\n")

[<Fact>]
let ``payload integrity failure is reported rather than thrown`` () =
    use temp = new Fixtures.TempDirectory()
    Fixtures.createPayload temp.Path "1.0.0" |> ignore

    System.IO.File.WriteAllText(
        System.IO.Path.Combine(temp.Path, "context", "UI-FOUNDATIONS.md"),
        "tampered"
    )

    match Payload.load temp.Path with
    | Ok _ -> failwith "expected the payload to fail its integrity check"
    | Error problems ->
        Assert.Contains(
            problems,
            function
            | PayloadArtifactCorrupt(file, _, _) -> file = "UI-FOUNDATIONS.md"
            | _ -> false
        )

[<Fact>]
let ``every supported configuration version has a migration path to the current one`` () =
    for version in Versioning.supportedConfigurationVersions do
        match Migrations.plan version Versioning.CurrentConfigurationVersion with
        | Ok _ -> ()
        | Error blocker -> failwith (PlanBlocker.describe blocker)

[<Fact>]
let ``migrations form an unbroken sequential chain`` () =
    let sorted = Migrations.all |> List.sortBy (fun migration -> migration.From)

    sorted
    |> List.iteri (fun index migration ->
        Assert.Equal(index + 1, migration.From)
        Assert.Equal(migration.From + 1, migration.To))

    Assert.Equal(Versioning.CurrentConfigurationVersion, (List.last sorted).To)
