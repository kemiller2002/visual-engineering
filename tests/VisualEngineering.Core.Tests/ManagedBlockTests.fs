module VisualEngineering.Core.Tests.ManagedBlockTests

open Xunit
open VisualEngineering.Core

[<Fact>]
let ``a managed region is appended without disturbing user content`` () =
    let existing = "# Project agents\n\nUse pnpm.\n"
    let result = ManagedBlock.upsert MarkdownComment "managed body" (Some existing)

    Assert.StartsWith("# Project agents\n\nUse pnpm.\n", result)
    Assert.Contains("managed body", result)

[<Fact>]
let ``updating a managed region preserves text before and after it`` () =
    let first = ManagedBlock.upsert MarkdownComment "one" (Some "before\n")
    let withTrailer = first + "\nafter\n"
    let second = ManagedBlock.upsert MarkdownComment "two" (Some withTrailer)

    Assert.StartsWith("before\n", second)
    Assert.EndsWith("after\n", second)
    Assert.Contains("two", second)
    Assert.DoesNotContain("one", second)

[<Fact>]
let ``upserting the same region twice is stable`` () =
    let once = ManagedBlock.upsert HashComment "ignored/" (Some "node_modules/\n")
    let twice = ManagedBlock.upsert HashComment "ignored/" (Some once)
    Assert.Equal(once, twice)

[<Fact>]
let ``an absent region is reported as absent`` () =
    Assert.True((ManagedBlock.tryExtract MarkdownComment "nothing here\n").IsNone)

[<Fact>]
let ``an extracted region returns exactly the body`` () =
    let text = ManagedBlock.upsert MarkdownComment "line one\nline two" None

    match ManagedBlock.tryExtract MarkdownComment text with
    | None -> failwith "expected a region"
    | Some block -> Assert.Equal("line one\nline two", block.Body)
