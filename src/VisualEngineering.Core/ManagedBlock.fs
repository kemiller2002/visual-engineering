namespace VisualEngineering.Core

open System

/// Comment syntax used to delimit the tool owned region inside a shared file.
type BlockStyle =
    | MarkdownComment
    | HashComment

/// The tool owned region found inside a shared file.
type ExtractedBlock = { Body: string; StartLine: int; EndLine: int }

/// Shared files are never rewritten wholesale. The tool owns only the region between its
/// markers, everything outside is user content and is copied through untouched.
[<RequireQualifiedAccess>]
module ManagedBlock =

    [<Literal>]
    let Marker = "echelon:" + Tool.Id

    let private beginMarker style =
        match style with
        | MarkdownComment -> $"<!-- BEGIN {Marker} -->"
        | HashComment -> $"# BEGIN {Marker}"

    let private endMarker style =
        match style with
        | MarkdownComment -> $"<!-- END {Marker} -->"
        | HashComment -> $"# END {Marker}"

    let private splitLines (text: string) =
        (Hash.normalizeText text).Split '\n' |> List.ofArray

    /// Locates the managed region. Returns None when the file has no region yet.
    let tryExtract style (text: string) =
        let lines = splitLines text
        let openMarker = beginMarker style
        let closeMarker = endMarker style

        let indexOf marker =
            lines |> List.tryFindIndex (fun line -> line.Trim() = marker)

        match indexOf openMarker, indexOf closeMarker with
        | Some start, Some finish when finish > start ->
            let body = lines |> List.skip (start + 1) |> List.take (finish - start - 1)

            Some
                { Body = String.Join('\n', body)
                  StartLine = start
                  EndLine = finish }
        | _ -> None

    /// Renders a complete managed region including its markers.
    let render style (body: string) =
        let normalized = (Hash.normalizeText body).TrimEnd '\n'
        String.Join('\n', [ beginMarker style; normalized; endMarker style ])

    /// Produces the full new content of a shared file with the managed region set to `body`.
    /// User content outside the region is preserved exactly.
    let upsert style (body: string) (existing: string option) =
        let block = render style body

        match existing with
        | None -> block + "\n"
        | Some text ->
            let lines = splitLines text

            match tryExtract style text with
            | Some found ->
                let before = lines |> List.take found.StartLine
                let after = lines |> List.skip (found.EndLine + 1)
                Text.lines (before @ [ block ] @ after)
            | None ->
                let trimmed = (Hash.normalizeText text).TrimEnd '\n'

                if trimmed = "" then
                    block + "\n"
                else
                    Text.lines [ trimmed; ""; block ]

    /// Hash of the region body, used to detect edits inside a region the tool owns.
    let bodyHash (body: string) = Hash.ofText body
