module VisualEngineering.Core.Tests.Fixtures

open System
open System.IO
open System.Text
open VisualEngineering.Core

/// A disposable temporary directory so tests never touch the developer's working tree.
type TempDirectory() =
    let path =
        Path.Combine(Path.GetTempPath(), "ve-tests-" + Guid.NewGuid().ToString("N").Substring(0, 12))

    do Directory.CreateDirectory path |> ignore
    member _.Path = path

    interface IDisposable with
        member _.Dispose() =
            try
                if Directory.Exists path then
                    Directory.Delete(path, true)
            with _ ->
                ()

let private sha256 (text: string) =
    Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes text)
    |> Convert.ToHexString
    |> fun value -> value.ToLowerInvariant()

/// Files a real payload carries. Content is short but structurally identical to the shipped one.
let payloadFiles =
    [ "AGENT-INSTRUCTIONS.md", "# Agent Instructions\n\nBefore UI work, read the briefing.\n"
      "UI-FOUNDATIONS.md", "# UI Foundations\n\nEvidence based defaults.\n"
      "UI-DECISION-CHECKLIST.md", "# UI Decision Checklist\n\nAsk these questions.\n"
      "UI-ANTI-PATTERNS.md", "# UI Anti Patterns\n\nAvoid these.\n"
      "RESEARCH-INDEX.md", "# Research Index\n\nGenerated index.\n"
      "sources.json", "{\n  \"records\": []\n}\n" ]

/// Writes a payload tree and loads it, exactly as the packaged one is loaded at runtime.
let createPayload (root: string) (contextVersion: string) =
    let contextDirectory = Path.Combine(root, "context")
    Directory.CreateDirectory contextDirectory |> ignore

    let artifacts =
        [ for file, content in payloadFiles do
              File.WriteAllText(Path.Combine(contextDirectory, file), content, UTF8Encoding false)
              file, sha256 content ]

    let manifest =
        JObject
            [ "schemaVersion", JString "1.0"
              "contextVersion", JString contextVersion
              "sourceRepository", JString "https://github.com/kemiller2002/visual-engineering"
              "sourceCommit", JString "0123456789abcdef0123456789abcdef01234567"
              "generatedAt", JString "2026-01-01T00:00:00.000Z"
              "researchDocuments", JInt 42
              "profile", JString "ui-foundations"
              "classification", JString "public"
              "artifacts",
              JArray
                  [ for file, hash in artifacts -> JObject [ "file", JString file; "sha256", JString hash ] ] ]

    File.WriteAllText(
        Path.Combine(contextDirectory, "context.json"),
        Json.serialize manifest,
        UTF8Encoding false
    )

    match Payload.load root with
    | Ok payload -> payload
    | Error problems ->
        failwith (String.Join("; ", problems |> List.map PayloadProblem.describe))

/// A repository directory with a git marker, so inspection sees a normal project.
let createRepository (root: string) =
    Directory.CreateDirectory(Path.Combine(root, ".git")) |> ignore
    root

let session (payload: Payload) (repository: string) =
    match Api.openSessionWith payload repository with
    | Ok session -> session
    | Error _ -> failwith "the test repository could not be opened"

/// Stable snapshot of every file in a repository, used to prove idempotency.
let snapshot (root: string) =
    Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
    |> Seq.filter (fun file -> not (file.Contains(Path.DirectorySeparatorChar.ToString() + ".git" + Path.DirectorySeparatorChar.ToString())))
    |> Seq.map (fun file -> Path.GetRelativePath(root, file).Replace('\\', '/'), sha256 (File.ReadAllText file))
    |> Seq.sortBy fst
    |> List.ofSeq

let write (root: string) (relative: string) (content: string) =
    let target = Path.Combine(root, relative.Replace('/', Path.DirectorySeparatorChar))
    Paths.ensureParent target
    File.WriteAllText(target, content, UTF8Encoding false)

let read (root: string) (relative: string) =
    File.ReadAllText(Path.Combine(root, relative.Replace('/', Path.DirectorySeparatorChar)))

let exists (root: string) (relative: string) =
    File.Exists(Path.Combine(root, relative.Replace('/', Path.DirectorySeparatorChar)))

/// Recreates what `ve-context sync` produced before this tool existed: context files only.
let createLegacyInstallation (root: string) (contextVersion: string) =
    let payloadRoot = Path.Combine(root, "..", "legacy-payload-" + Guid.NewGuid().ToString("N").Substring(0, 8))
    Directory.CreateDirectory payloadRoot |> ignore
    let payload = createPayload payloadRoot contextVersion

    for artifact in payload.Artifacts do
        write root (".visual-engineering/" + RepoPath.value artifact.File) artifact.Content

    payload
