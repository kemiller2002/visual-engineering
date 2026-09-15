namespace VisualEngineering.Core

open System
open System.IO

/// One file shipped inside the npm package and installed into a repository.
type PayloadArtifact =
    { File: RepoPath
      /// SHA-256 over the exact published bytes, as recorded by the package build.
      PublishedSha256: string
      Content: string }

/// The capability payload carried by this release of the package: the Visual Engineering
/// agent briefing plus the generated research index and provenance records.
type Payload =
    { Root: string
      ContextVersion: string
      SourceCommit: string
      GeneratedAt: string
      ResearchDocuments: int
      Artifacts: PayloadArtifact list }

type PayloadProblem =
    | PayloadNotFound of searched: string list
    | PayloadManifestUnreadable of path: string * reason: string
    | PayloadArtifactMissing of file: string
    | PayloadArtifactCorrupt of file: string * expected: string * actual: string

[<RequireQualifiedAccess>]
module PayloadProblem =

    let describe problem =
        match problem with
        | PayloadNotFound searched ->
            "Packaged Visual Engineering context was not found. Searched: "
            + String.Join("; ", searched)
        | PayloadManifestUnreadable(path, reason) -> $"Packaged context manifest {path} is unreadable: {reason}"
        | PayloadArtifactMissing file -> $"Packaged context is missing {file}"
        | PayloadArtifactCorrupt(file, expected, actual) ->
            $"Packaged context file {file} failed its integrity check (expected {expected}, found {actual})"

[<RequireQualifiedAccess>]
module Payload =

    /// Directory inside the payload root holding installable files.
    [<Literal>]
    let ContextDirectoryName = "context"

    /// Manifest describing the payload, produced by the package build.
    [<Literal>]
    let ManifestFileName = "context.json"

    /// Environment variable that overrides payload discovery. Used by the build and tests.
    [<Literal>]
    let PayloadEnvironmentVariable = "VISUAL_ENGINEERING_PAYLOAD"

    /// Directory the executable was launched from.
    ///
    /// `AppContext.BaseDirectory` is the correct source for a single-file bundle, which is how
    /// this CLI ships; `Assembly.Location` is empty there. `Environment.ProcessPath` is the
    /// fallback for hosts that do not set a base directory.
    let private executableDirectory () =
        let fromBaseDirectory =
            match AppContext.BaseDirectory with
            | "" -> None
            | directory -> Some(Path.TrimEndingDirectorySeparator directory)

        let fromProcessPath =
            Environment.ProcessPath |> Option.ofObj |> Option.bind Paths.tryParent

        fromBaseDirectory |> Option.orElse fromProcessPath |> Option.defaultValue "."

    let private ascend (start: string) (depth: int) =
        let rec loop (current: string) remaining acc =
            if remaining < 0 || String.IsNullOrEmpty current then
                List.rev acc
            else
                let parent = Path.GetDirectoryName current

                loop (parent |> Option.ofObj |> Option.defaultValue "") (remaining - 1) (current :: acc)

        loop (Path.GetFullPath start) depth []

    /// Ordered payload locations. The first entry that contains a readable manifest wins.
    let candidateRoots () =
        let fromEnvironment =
            Environment.GetEnvironmentVariable PayloadEnvironmentVariable
            |> Option.ofObj
            |> Option.filter (String.IsNullOrWhiteSpace >> not)
            |> Option.map Path.GetFullPath
            |> Option.toList

        let exeDirectory = executableDirectory ()

        let packaged =
            [
              // Installed npm layout: the executable ships in
              // node_modules/@echelon-foundry/visual-engineering-<rid>/ and the payload in the
              // sibling node_modules/@echelon-foundry/visual-engineering/. The launcher passes
              // the payload location explicitly; these cover running the executable directly.
              Path.GetFullPath(Path.Combine(exeDirectory, "..", "visual-engineering", "payload"))
              Path.GetFullPath(
                  Path.Combine(exeDirectory, "..", "..", "..", "..", "visual-engineering", "payload")
              )
              // Staged layout: <package>/platforms/<rid>/<exe> with <package>/payload.
              Path.GetFullPath(Path.Combine(exeDirectory, "..", "..", "payload"))
              // Flat layout: the payload sits next to the executable.
              Path.GetFullPath(Path.Combine(exeDirectory, "payload")) ]

        // Developer checkout: `dotnet run` from anywhere inside the repository.
        let developer =
            ascend exeDirectory 8
            |> List.map (fun directory -> Path.Combine(directory, "npm", "payload"))

        fromEnvironment @ packaged @ developer

    let private manifestPath (root: string) =
        Path.Combine(root, ContextDirectoryName, ManifestFileName)

    /// Finds the payload root without reading its contents.
    let locate () : Result<string, PayloadProblem> =
        let searched = candidateRoots ()

        match searched |> List.tryFind (fun root -> File.Exists(manifestPath root)) with
        | Some root -> Ok root
        | None -> Error(PayloadNotFound searched)

    let private loadArtifacts (root: string) (files: (string * string) list) =
        files
        |> List.map (fun (file, expected) ->
            let absolute = Path.Combine(root, ContextDirectoryName, file)

            if not (File.Exists absolute) then
                Error(PayloadArtifactMissing file)
            else
                let bytes = File.ReadAllBytes absolute
                let actual = Hash.ofBytes bytes

                if actual <> expected then
                    Error(PayloadArtifactCorrupt(file, expected, actual))
                else
                    match RepoPath.tryCreate file with
                    | Error reason -> Error(PayloadManifestUnreadable(manifestPath root, reason))
                    | Ok path ->
                        Ok
                            { File = path
                              PublishedSha256 = expected
                              Content = Text.Encoding.UTF8.GetString bytes })

    /// Loads and integrity checks the payload. Never mutates anything.
    let load (root: string) : Result<Payload, PayloadProblem list> =
        let manifestFile = manifestPath root

        if not (File.Exists manifestFile) then
            Error [ PayloadNotFound [ root ] ]
        else
            match Json.tryParse (File.ReadAllText manifestFile) with
            | Error reason -> Error [ PayloadManifestUnreadable(manifestFile, reason) ]
            | Ok document ->
                use document = document
                let root' = document.RootElement

                let declared =
                    Json.arrayItems "artifacts" root'
                    |> List.choose (fun item ->
                        match Json.tryString "file" item, Json.tryString "sha256" item with
                        | Some file, Some sha -> Some(file, sha)
                        | _ -> None)

                // The manifest itself is installed alongside the artifacts it describes.
                let manifestBytes = File.ReadAllBytes manifestFile

                let manifestArtifact =
                    { File = RepoPath.create ManifestFileName
                      PublishedSha256 = Hash.ofBytes manifestBytes
                      Content = Text.Encoding.UTF8.GetString manifestBytes }

                if List.isEmpty declared then
                    Error [ PayloadManifestUnreadable(manifestFile, "no artifacts are declared") ]
                else
                    let loaded = loadArtifacts root declared
                    let problems = loaded |> List.choose (function Error problem -> Some problem | _ -> None)

                    if not (List.isEmpty problems) then
                        Error problems
                    else
                        let artifacts =
                            loaded |> List.choose (function Ok artifact -> Some artifact | _ -> None)

                        match Json.tryString "contextVersion" root' with
                        | None -> Error [ PayloadManifestUnreadable(manifestFile, "contextVersion is missing") ]
                        | Some contextVersion ->
                            Ok
                                { Root = root
                                  ContextVersion = contextVersion
                                  SourceCommit =
                                    Json.tryString "sourceCommit" root' |> Option.defaultValue "unknown"
                                  GeneratedAt = Json.tryString "generatedAt" root' |> Option.defaultValue ""
                                  ResearchDocuments = Json.tryInt "researchDocuments" root' |> Option.defaultValue 0
                                  Artifacts = manifestArtifact :: artifacts }

    /// Locates and loads in one step.
    let discover () : Result<Payload, PayloadProblem list> =
        match locate () with
        | Error problem -> Error [ problem ]
        | Ok root -> load root
