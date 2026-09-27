namespace VisualEngineering.ThemeCatalog

open System.Text.RegularExpressions

/// A permanent THM-NNNN theme identifier. Construct only through ThemeId.tryParse / ThemeId.next.
type ThemeId =
    private
    | ThemeId of int

    member this.Number = let (ThemeId n) = this in n
    override this.ToString() = sprintf "THM-%04d" this.Number

module ThemeId =
    // \z, not $: in .NET, $ also matches before a trailing newline.
    let private pattern = Regex(@"^THM-([0-9]{4})\z", RegexOptions.CultureInvariant)

    /// Parses a string that is, in its entirety, a THM-NNNN identifier.
    let tryParse (value: string) : ThemeId option =
        let m = pattern.Match value
        if m.Success then Some(ThemeId(int m.Groups[1].Value)) else None

    let number (id: ThemeId) = id.Number
    let format (id: ThemeId) = id.ToString()

    /// The identifier after the highest existing one. IDs need not be contiguous. After THM-9999
    /// the result no longer fits THM-NNNN and is rejected wherever it is parsed.
    let next (ids: ThemeId seq) : ThemeId =
        ids |> Seq.map number |> Seq.fold max 0 |> (+) 1 |> ThemeId

[<RequireQualifiedAccess>]
type Severity =
    | Error
    | Notice

type DiagnosticCode =
    | MalformedJson
    | InvalidField
    | IndexMissing
    | InvalidThemeId
    | InvalidSpecimenFilename
    | InvalidPaletteColor
    | DuplicateThemeId
    | DuplicateIndexId
    | FilenameIdMismatch
    | IndexSpecimenMissing
    | SpecimenIndexMissing
    | IndexPathMismatch
    | IndexMetadataMismatch
    | SemanticTokenMissingPaletteKey
    | NextIdMismatch
    | FingerprintMismatch
    | DuplicatePalette
    | DuplicateThemeDefinition
    | InvalidThemeReference
    | SelfReference
    | ReplacesCycle
    | SpecimenPathMismatch

module DiagnosticCode =
    /// Stable, kebab-case diagnostic code. Consumers should key on this, never on message text.
    let name code =
        match code with
        | MalformedJson -> "malformed-json"
        | InvalidField -> "invalid-field"
        | IndexMissing -> "index-missing"
        | InvalidThemeId -> "invalid-theme-id"
        | InvalidSpecimenFilename -> "invalid-specimen-filename"
        | InvalidPaletteColor -> "invalid-palette-color"
        | DuplicateThemeId -> "duplicate-theme-id"
        | DuplicateIndexId -> "duplicate-index-id"
        | FilenameIdMismatch -> "filename-id-mismatch"
        | IndexSpecimenMissing -> "index-specimen-missing"
        | SpecimenIndexMissing -> "specimen-index-missing"
        | IndexPathMismatch -> "index-path-mismatch"
        | IndexMetadataMismatch -> "index-metadata-mismatch"
        | SemanticTokenMissingPaletteKey -> "semantic-token-missing-palette-key"
        | NextIdMismatch -> "next-id-mismatch"
        | FingerprintMismatch -> "fingerprint-mismatch"
        | DuplicatePalette -> "duplicate-palette"
        | DuplicateThemeDefinition -> "duplicate-theme-definition"
        | InvalidThemeReference -> "invalid-theme-reference"
        | SelfReference -> "self-reference"
        | ReplacesCycle -> "replaces-cycle"
        | SpecimenPathMismatch -> "specimen-path-mismatch"

type Diagnostic =
    { Severity: Severity
      Code: DiagnosticCode
      ThemeId: string option
      Path: string option
      Message: string }

module Diagnostic =
    let error code themeId path message =
        { Severity = Severity.Error; Code = code; ThemeId = themeId; Path = path; Message = message }

    let notice code themeId path message =
        { Severity = Severity.Notice; Code = code; ThemeId = themeId; Path = path; Message = message }

    let isError diagnostic = diagnostic.Severity = Severity.Error

    /// Total, deterministic ordering independent of file-system enumeration order.
    let sortKey d =
        (Option.defaultValue "" d.ThemeId, DiagnosticCode.name d.Code, Option.defaultValue "" d.Path, d.Message)

type ThemeStatus =
    | Draft
    | Evaluated
    | Accepted
    | Deprecated

module ThemeStatus =
    let tryParse value =
        match value with
        | "draft" -> Some Draft
        | "evaluated" -> Some Evaluated
        | "accepted" -> Some Accepted
        | "deprecated" -> Some Deprecated
        | _ -> None

    let name status =
        match status with
        | Draft -> "draft"
        | Evaluated -> "evaluated"
        | Accepted -> "accepted"
        | Deprecated -> "deprecated"

/// A normalized palette color. The authored hex remains the source; this is its canonical spelling.
type CanonicalHex = CanonicalHex of string

/// A parsed, structurally usable specimen. Cross-file invariants are checked separately.
type Specimen =
    { Id: ThemeId
      /// Repository-relative, forward-slash path of the file the specimen was read from.
      Path: string
      Name: string
      Status: ThemeStatus
      Mode: string
      Palette: Map<string, CanonicalHex>
      SemanticTokens: Map<string, string>
      Contexts: Set<string>
      Temperature: string option
      ProvenanceSource: string option
      Replaces: string option
      SpecimenPath: string option
      Fingerprint: string }

type IndexEntry =
    { Id: ThemeId
      Position: int
      Name: string
      Status: string
      Mode: string
      Temperature: string option
      Contexts: Set<string> option
      Path: string }

type CatalogIndex =
    { Path: string
      NextId: string
      Entries: IndexEntry list }

/// A specimen file whose content could not be used, retained so its identity is not reported missing.
type UnusableSpecimen = { Path: string; FileId: ThemeId option }

type SourceFile = { Path: string; Text: string }

/// Raw catalog inputs as read from disk. Everything downstream is a pure function of this value.
type CatalogSources =
    { IndexPath: string
      Index: SourceFile option
      Specimens: SourceFile list }
