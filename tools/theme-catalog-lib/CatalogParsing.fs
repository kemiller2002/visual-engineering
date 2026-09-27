namespace VisualEngineering.ThemeCatalog

open System
open System.IO
open System.Text.Json

/// Applicative validation that accumulates every structural finding instead of stopping at the first.
module Validation =
    type Validated<'a> = Result<'a, Diagnostic list>

    let map2 f (a: Validated<'a>) (b: Validated<'b>) : Validated<'c> =
        match a, b with
        | Ok x, Ok y -> Ok(f x y)
        | Error e1, Error e2 -> Error(e1 @ e2)
        | Error e, Ok _
        | Ok _, Error e -> Error e

    let (<!>) f x = Result.map f x
    let (<*>) f x = map2 (<|) f x

    let sequence (items: Validated<'a> list) : Validated<'a list> =
        List.foldBack (map2 (fun x xs -> x :: xs)) items (Ok [])

/// Parses authored JSON into typed catalog values. Null, missing, or mistyped required
/// values are reported as diagnostics; they are never coerced into defaults.
module CatalogParsing =
    open Validation

    type private Context = { Path: string; ThemeId: string option }

    let private invalid ctx code field message =
        Error [ Diagnostic.error code ctx.ThemeId (Some ctx.Path) (sprintf "%s %s" field message) ]

    let private kindName (kind: JsonValueKind) =
        match kind with
        | JsonValueKind.Null -> "null"
        | JsonValueKind.String -> "a string"
        | JsonValueKind.Number -> "a number"
        | JsonValueKind.True
        | JsonValueKind.False -> "a boolean"
        | JsonValueKind.Array -> "an array"
        | JsonValueKind.Object -> "an object"
        | _ -> "undefined"

    let private tryProperty (name: string) (element: JsonElement) =
        match element.TryGetProperty name with
        | true, value -> Some value
        | _ -> None

    let private join parent name = if String.IsNullOrEmpty parent then name else parent + "." + name

    let private nonEmptyString ctx field (value: JsonElement) : Validated<string> =
        match value.ValueKind with
        | JsonValueKind.String ->
            match value.GetString() with
            | null -> invalid ctx InvalidField field "must be a non-empty string"
            | text when String.IsNullOrWhiteSpace text -> invalid ctx InvalidField field "must be a non-empty string"
            | text -> Ok text
        | kind -> invalid ctx InvalidField field (sprintf "must be a non-empty string, not %s" (kindName kind))

    let private anyString ctx field (value: JsonElement) : Validated<string> =
        match value.ValueKind with
        | JsonValueKind.String ->
            match value.GetString() with
            | null -> invalid ctx InvalidField field "must be a string"
            | text -> Ok text
        | kind -> invalid ctx InvalidField field (sprintf "must be a string, not %s" (kindName kind))

    let private requiredString ctx parent name element =
        match tryProperty name element with
        | Some value -> nonEmptyString ctx (join parent name) value
        | None -> invalid ctx InvalidField (join parent name) "is required"

    /// Absent is None; present must be a string (null is not a string).
    let private optionalString ctx parent name element : Validated<string option> =
        match tryProperty name element with
        | None -> Ok None
        | Some value -> anyString ctx (join parent name) value |> Result.map Some

    /// Absent or null is None; otherwise must be a string.
    let private nullableString ctx parent name element : Validated<string option> =
        match tryProperty name element with
        | None -> Ok None
        | Some value when value.ValueKind = JsonValueKind.Null -> Ok None
        | Some value -> anyString ctx (join parent name) value |> Result.map Some

    let private objectValue ctx field (value: JsonElement) : Validated<JsonElement> =
        match value.ValueKind with
        | JsonValueKind.Object -> Ok value
        | kind -> invalid ctx InvalidField field (sprintf "must be an object, not %s" (kindName kind))

    let private requiredObject ctx parent name element =
        match tryProperty name element with
        | Some value -> objectValue ctx (join parent name) value
        | None -> invalid ctx InvalidField (join parent name) "is required"

    let private optionalObject ctx parent name element : Validated<JsonElement option> =
        match tryProperty name element with
        | None -> Ok None
        | Some value -> objectValue ctx (join parent name) value |> Result.map Some

    let private stringArray ctx field (value: JsonElement) : Validated<string list> =
        match value.ValueKind with
        | JsonValueKind.Array ->
            value.EnumerateArray()
            |> Seq.mapi (fun i item -> nonEmptyString ctx (sprintf "%s[%d]" field i) item)
            |> Seq.toList
            |> sequence
        | kind -> invalid ctx InvalidField field (sprintf "must be an array, not %s" (kindName kind))

    /// Object of non-empty strings. Duplicate member names are rejected rather than silently
    /// resolved to one value.
    let private stringMap ctx field (value: JsonElement) : Validated<Map<string, string>> =
        let members = value.EnumerateObject() |> Seq.map (fun p -> p.Name, p.Value) |> Seq.toList
        let duplicates =
            members
            |> List.countBy fst
            |> List.filter (snd >> (<) 1)
            |> List.map (fun (name, _) ->
                Diagnostic.error InvalidField ctx.ThemeId (Some ctx.Path) (sprintf "%s.%s is declared more than once" field name))
        let values =
            members
            |> List.map (fun (name, v) -> nonEmptyString ctx (join field name) v |> Result.map (fun s -> name, s))
            |> sequence
        match duplicates, values with
        | [], Ok pairs -> Ok(Map.ofList pairs)
        | dups, Ok _ -> Error dups
        | dups, Error errors -> Error(dups @ errors)

    let private parseDocument ctx (text: string) (parse: JsonElement -> Validated<'a>) : Validated<'a> =
        try
            use document = JsonDocument.Parse text
            match document.RootElement.ValueKind with
            | JsonValueKind.Object -> parse document.RootElement
            | kind -> invalid ctx InvalidField "document root" (sprintf "must be an object, not %s" (kindName kind))
        with :? JsonException as ex ->
            Error [ Diagnostic.error MalformedJson ctx.ThemeId (Some ctx.Path) (sprintf "is not valid JSON: %s" ex.Message) ]

    let private themeId ctx field (text: string) : Validated<ThemeId> =
        match ThemeId.tryParse text with
        | Some id -> Ok id
        | None -> invalid ctx InvalidThemeId field (sprintf "'%s' is not a THM-NNNN identifier" text)

    let private oneOf ctx field (allowed: string list) (text: string) : Validated<string> =
        if List.contains text allowed then Ok text
        else invalid ctx InvalidField field (sprintf "'%s' is not one of %s" text (String.Join(", ", allowed)))

    let private modes = [ "light"; "dark"; "mixed"; "print" ]

    let private requiredTokens = [ "background"; "surface"; "text"; "mutedText"; "primaryAccent"; "border" ]

    let private palette ctx root : Validated<Map<string, CanonicalHex>> =
        requiredObject ctx "" "palette" root
        |> Result.bind (stringMap ctx "palette")
        |> Result.bind (fun authored ->
            authored
            |> Map.toList
            |> List.map (fun (key, hex) ->
                match Fingerprint.normalizeHex hex with
                | Some canonical -> Ok(key, canonical)
                | None -> invalid ctx InvalidPaletteColor ("palette." + key) (sprintf "'%s' is not #RRGGBB or #RRGGBBAA" hex))
            |> sequence
            |> Result.map Map.ofList)

    let private semanticTokens ctx root : Validated<Map<string, string>> =
        requiredObject ctx "" "semanticTokens" root
        |> Result.bind (stringMap ctx "semanticTokens")
        |> Result.bind (fun tokens ->
            match requiredTokens |> List.filter (fun t -> not (Map.containsKey t tokens)) with
            | [] -> Ok tokens
            | missing ->
                missing
                |> List.map (fun t -> Diagnostic.error InvalidField ctx.ThemeId (Some ctx.Path) (sprintf "semanticTokens.%s is required" t))
                |> Error)

    let private contexts ctx root : Validated<Set<string>> =
        match tryProperty "contexts" root with
        | Some value -> stringArray ctx "contexts" value |> Result.map Set.ofList
        | None -> invalid ctx InvalidField "contexts" "is required"

    let private fileStem (path: string) =
        match Path.GetFileNameWithoutExtension path with
        | null -> ""
        | stem -> stem

    /// The filename's own THM identifier, when the filename is THM-NNNN.json.
    let fileThemeId (path: string) = ThemeId.tryParse (fileStem path)

    let parseSpecimen (file: SourceFile) : Validated<Specimen> =
        let ctx = { Path = file.Path; ThemeId = Some(fileStem file.Path) }
        parseDocument ctx file.Text (fun root ->
            let make id name status mode palette tokens contexts temperature source replaces specimenPath fingerprint : Specimen =
                { Id = id
                  Path = file.Path
                  Name = name
                  Status = status
                  Mode = mode
                  Palette = palette
                  SemanticTokens = tokens
                  Contexts = contexts
                  Temperature = temperature
                  ProvenanceSource = source
                  Replaces = replaces
                  SpecimenPath = specimenPath
                  Fingerprint = fingerprint }
            let status =
                requiredString ctx "" "status" root
                |> Result.bind (fun s ->
                    match ThemeStatus.tryParse s with
                    | Some status -> Ok status
                    | None -> invalid ctx InvalidField "status" (sprintf "'%s' is not one of draft, evaluated, accepted, deprecated" s))
            let temperature =
                optionalObject ctx "" "facets" root
                |> Result.bind (function
                    | Some facets -> optionalString ctx "facets" "temperature" facets
                    | None -> Ok None)
            let source =
                requiredObject ctx "" "provenance" root
                |> Result.bind (optionalString ctx "provenance" "source")
            let specimenPath =
                requiredObject ctx "" "specimen" root
                |> Result.bind (optionalString ctx "specimen" "path")
            make
            <!> (requiredString ctx "" "id" root |> Result.bind (themeId ctx "id"))
            <*> requiredString ctx "" "name" root
            <*> status
            <*> (requiredString ctx "" "mode" root |> Result.bind (oneOf ctx "mode" modes))
            <*> palette ctx root
            <*> semanticTokens ctx root
            <*> contexts ctx root
            <*> temperature
            <*> source
            <*> nullableString ctx "" "replaces" root
            <*> specimenPath
            <*> requiredString ctx "" "fingerprint" root)

    let private indexEntry (ctx: Context) position (element: JsonElement) : Validated<IndexEntry> =
        let field = sprintf "themes[%d]" position
        let entryCtx =
            { ctx with
                ThemeId =
                    tryProperty "id" element
                    |> Option.filter (fun v -> v.ValueKind = JsonValueKind.String)
                    |> Option.bind (fun v -> Option.ofObj (v.GetString())) }
        objectValue entryCtx field element
        |> Result.bind (fun entry ->
            let make id name status mode temperature contexts path : IndexEntry =
                { Id = id
                  Position = position
                  Name = name
                  Status = status
                  Mode = mode
                  Temperature = temperature
                  Contexts = contexts
                  Path = path }
            let contexts =
                match tryProperty "contexts" entry with
                | Some value -> stringArray entryCtx (field + ".contexts") value |> Result.map (Set.ofList >> Some)
                | None -> Ok None
            make
            <!> (requiredString entryCtx field "id" entry |> Result.bind (themeId entryCtx (field + ".id")))
            <*> requiredString entryCtx field "name" entry
            <*> requiredString entryCtx field "status" entry
            <*> requiredString entryCtx field "mode" entry
            <*> optionalString entryCtx field "temperature" entry
            <*> contexts
            <*> requiredString entryCtx field "path" entry)

    let parseIndex (file: SourceFile) : Validated<CatalogIndex> =
        let ctx = { Path = file.Path; ThemeId = None }
        parseDocument ctx file.Text (fun root ->
            let entries =
                match tryProperty "themes" root with
                | Some themes when themes.ValueKind = JsonValueKind.Array ->
                    themes.EnumerateArray() |> Seq.mapi (indexEntry ctx) |> Seq.toList |> sequence
                | Some themes -> invalid ctx InvalidField "themes" (sprintf "must be an array, not %s" (kindName themes.ValueKind))
                | None -> invalid ctx InvalidField "themes" "is required"
            (fun nextId entries -> ({ Path = file.Path; NextId = nextId; Entries = entries }: CatalogIndex))
            <!> requiredString ctx "" "nextId" root
            <*> entries)
