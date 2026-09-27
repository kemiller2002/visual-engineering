open System
open System.IO
open VisualEngineering.ThemeCatalog
open Fixtures

type TestCase = { Name: string; Check: unit -> Result<unit, string> }

let test name check = { Name = name; Check = check }

let codesOf severity (diagnostics: Diagnostic list) =
    diagnostics |> List.filter (fun d -> d.Severity = severity) |> List.map (fun d -> DiagnosticCode.name d.Code) |> Set.ofList

let describe (diagnostics: Diagnostic list) =
    diagnostics
    |> List.map (fun d -> sprintf "  %A %s %A %A: %s" d.Severity (DiagnosticCode.name d.Code) d.ThemeId d.Path d.Message)
    |> String.concat "\n"

/// The catalog's errors must be exactly these codes, and its notices exactly these codes.
let expect (errors: string list) (notices: string list) (catalog: Catalog) () =
    let diagnostics = validate catalog
    let actualErrors = codesOf Severity.Error diagnostics
    let actualNotices = codesOf Severity.Notice diagnostics
    if actualErrors = Set.ofList errors && actualNotices = Set.ofList notices then Ok()
    else
        Error(sprintf "expected errors %A and notices %A, got:\n%s" errors notices (describe diagnostics))

let expectErrors errors = expect errors []
let expectValid = expect [] []

let allOf (checks: (unit -> Result<unit, string>) list) () =
    checks |> List.fold (fun outcome check -> outcome |> Result.bind check) (Ok())

/// A second theme reusing THM-0001's exact colors under different palette key names.
let lightTwin id status tokens =
    specimen id "Light Twin" status "light" "cool"
        [ "ground", "#ffffff"; "type", "#111111"; "signal", "#0055AA"; "hairline", "#CCCCCCFF" ] tokens lightFingerprint

let withTwin id status tokens =
    valid
    |> withSpecimen (id + ".json") (Json(lightTwin id status tokens))
    |> editIndex (append [ "themes" ] (indexEntry id "Light Twin" status "light" "cool") >> set [ "nextId" ] (str "THM-0004"))

let twinTokens =
    [ "background", "ground"; "surface", "ground"; "text", "type"; "mutedText", "type"; "primaryAccent", "signal"
      "border", "hairline" ]

let rec findRepositoryRoot (directory: DirectoryInfo) =
    if File.Exists(Path.Combine(directory.FullName, "content", "themes", "index.json")) then Some directory.FullName
    else
        match directory.Parent with
        | null -> None
        | parent -> findRepositoryRoot parent

let tests =
    [ test "valid catalog passes" (expectValid valid)

      test "real repository catalog passes" (fun () ->
          match findRepositoryRoot (DirectoryInfo AppContext.BaseDirectory) with
          | None -> Error "could not locate the repository catalog"
          | Some root ->
              match CatalogFiles.validateDirectory root CatalogFiles.defaultCatalogDirectory with
              | [] -> Ok()
              | diagnostics -> Error(describe diagnostics))

      test "validation output is deterministic and sorted" (fun () ->
          let broken =
              valid
              |> editSpecimen "THM-0001.json" (set [ "fingerprint" ] (str "stale") >> set [ "name" ] (str "Renamed"))
              |> editIndex (set [ "nextId" ] (str "THM-0009"))
          let first = validate broken
          let second = validate broken
          if first <> second then Error "two runs disagree"
          elif first <> List.sortBy Diagnostic.sortKey first then Error "diagnostics are not in canonical order"
          elif first.Length < 3 then Error(describe first)
          else Ok())

      test "duplicate specimen ID fails"
          (valid
           |> withSpecimen "THM-0003.json"
               (Json(light |> set [ "palette"; "accent" ] (str "#AA0000") |> set [ "fingerprint" ] (str "#111111|#AA0000|#CCCCCC|#FFFFFF")))
           |> editIndex (set [ "nextId" ] (str "THM-0004"))
           |> expectErrors [ "duplicate-theme-id"; "filename-id-mismatch"; "specimen-path-mismatch" ])

      test "duplicate index ID fails"
          (valid
           |> editIndex (append [ "themes" ] (indexEntry "THM-0001" "Base Light" "evaluated" "light" "cool"))
           |> expectErrors [ "duplicate-index-id" ])

      test "filename and authored ID mismatch fails"
          (valid
           |> withoutSpecimen "THM-0002.json"
           |> withSpecimen "THM-0003.json" (Json dark)
           |> expectErrors [ "filename-id-mismatch"; "index-path-mismatch"; "specimen-path-mismatch"; "next-id-mismatch" ])

      test "index entry without a specimen fails"
          (valid
           |> editIndex (append [ "themes" ] (indexEntry "THM-0003" "Ghost" "draft" "light" "cool") >> set [ "nextId" ] (str "THM-0004"))
           |> expectErrors [ "index-specimen-missing" ])

      test "specimen without an index entry fails"
          (valid |> editIndex (removeAt [ "themes" ] 1) |> expectErrors [ "specimen-index-missing" ])

      test "incorrect index path fails"
          (valid
           |> editIndex (set [ "themes"; "1"; "path" ] (str "content/themes/specimens/THM-0001.json"))
           |> expectErrors [ "index-path-mismatch" ])

      test "equivalent but non-canonical index path is not normalized into a pass"
          (valid
           |> editIndex (set [ "themes"; "1"; "path" ] (str "./content/themes/specimens/THM-0002.json"))
           |> expectErrors [ "index-path-mismatch" ])

      test "index name mismatch fails"
          (valid |> editIndex (set [ "themes"; "0"; "name" ] (str "Other")) |> expectErrors [ "index-metadata-mismatch" ])

      test "index status mismatch fails"
          (valid |> editIndex (set [ "themes"; "0"; "status" ] (str "accepted")) |> expectErrors [ "index-metadata-mismatch" ])

      test "index mode mismatch fails"
          (valid |> editIndex (set [ "themes"; "0"; "mode" ] (str "dark")) |> expectErrors [ "index-metadata-mismatch" ])

      test "index temperature must match the specimen temperature facet"
          (valid |> editIndex (set [ "themes"; "0"; "temperature" ] (str "warm")) |> expectErrors [ "index-metadata-mismatch" ])

      test "index temperature without a specimen facet fails"
          (valid |> editSpecimen "THM-0001.json" (remove [ "facets" ]) |> expectErrors [ "index-metadata-mismatch" ])

      test "perception temperature is not the index temperature facet"
          (valid
           |> editSpecimen "THM-0001.json" (set [ "perception" ] (obj [ "temperature", str "unknown" ]))
           |> expectValid)

      test "index contexts must match specimen contexts as a set"
          (valid
           |> editIndex (set [ "themes"; "0"; "contexts" ] (arr [ str "print" ]))
           |> expectErrors [ "index-metadata-mismatch" ])

      test "index contexts in a different order are consistent"
          (valid
           |> editIndex (set [ "themes"; "0"; "contexts" ] (arr [ str "dashboard"; str "application" ]))
           |> expectValid)

      test "required semantic token referencing a missing palette key fails"
          (valid
           |> editSpecimen "THM-0001.json" (set [ "semanticTokens"; "primaryAccent" ] (str "signal"))
           |> expectErrors [ "semantic-token-missing-palette-key" ])

      test "optional semantic token referencing a missing palette key fails"
          (valid
           |> editSpecimen "THM-0002.json" (set [ "semanticTokens"; "success" ] (str "green"))
           |> expectErrors [ "semantic-token-missing-palette-key" ])

      test "incorrect nextId fails"
          (valid |> editIndex (set [ "nextId" ] (str "THM-0002")) |> expectErrors [ "next-id-mismatch" ])

      test "nextId above the expected value fails"
          (valid |> editIndex (set [ "nextId" ] (str "THM-0004")) |> expectErrors [ "next-id-mismatch" ])

      test "nextId follows the highest ID when IDs are not contiguous"
          (let catalog =
              valid
              |> withoutSpecimen "THM-0002.json"
              |> withSpecimen "THM-0007.json"
                  (Json(dark |> set [ "id" ] (str "THM-0007") |> set [ "specimen"; "path" ] (str (specimenPath "THM-0007.json"))))
              |> editIndex (set [ "themes"; "1" ] (indexEntry "THM-0007" "Base Dark" "draft" "dark" "warm"))
           allOf
               [ catalog |> editIndex (set [ "nextId" ] (str "THM-0008")) |> expectValid
                 catalog |> editIndex (set [ "nextId" ] (str "THM-0003")) |> expectErrors [ "next-id-mismatch" ] ])

      test "stored fingerprint mismatch fails"
          (valid
           |> editSpecimen "THM-0001.json" (set [ "fingerprint" ] (str "#FFFFFF|#CCCCCC|#111111|#0055AA"))
           |> expectErrors [ "fingerprint-mismatch" ])

      test "fingerprint normalizes hex case and opaque alpha"
          (valid
           |> editSpecimen "THM-0001.json" (set [ "palette"; "paper" ] (str "#ffffffFF") >> set [ "palette"; "accent" ] (str "#0055aa"))
           |> expectValid)

      test "fingerprint keeps non-opaque alpha as a distinct color"
          (valid
           |> editSpecimen "THM-0001.json" (set [ "palette"; "paper" ] (str "#FFFFFF80"))
           |> expectErrors [ "fingerprint-mismatch" ])

      test "same palette and same semantic mapping is a duplicate theme definition"
          (withTwin "THM-0003" "draft" twinTokens |> expect [ "duplicate-theme-definition" ] [ "duplicate-palette" ])

      test "same palette with a materially different semantic mapping is permitted and reported"
          (withTwin "THM-0003" "draft"
               [ "background", "type"; "surface", "type"; "text", "ground"; "mutedText", "hairline"; "primaryAccent", "signal"
                 "border", "hairline" ]
           |> expect [] [ "duplicate-palette" ])

      test "an identical definition retained under a deprecated ID is not an active duplicate"
          (withTwin "THM-0003" "deprecated" twinTokens |> expect [] [ "duplicate-palette" ])

      test "index entry omitting temperature the specimen authors fails"
          (valid |> editIndex (remove [ "themes"; "0"; "temperature" ]) |> expectErrors [ "index-metadata-mismatch" ])

      test "index entry omitting contexts fails"
          (valid |> editIndex (remove [ "themes"; "0"; "contexts" ]) |> expectErrors [ "index-metadata-mismatch" ])

      test "index entry may omit temperature when the specimen has no temperature facet"
          (valid
           |> editSpecimen "THM-0001.json" (remove [ "facets" ])
           |> editIndex (remove [ "themes"; "0"; "temperature" ])
           |> expectValid)

      test "specimen id with a trailing newline is not a THM identifier"
          (valid |> editSpecimen "THM-0002.json" (set [ "id" ] (str "THM-0002\n")) |> expectErrors [ "invalid-theme-id" ])

      test "replaces with a trailing newline is not a THM identifier"
          (valid |> editSpecimen "THM-0002.json" (set [ "replaces" ] (str "THM-0001\n")) |> expectErrors [ "invalid-theme-reference" ])

      test "palette color with a trailing newline is malformed"
          (valid |> editSpecimen "THM-0002.json" (set [ "palette"; "glow" ] (str "#FFB000\n")) |> expectErrors [ "invalid-palette-color" ])

      test "invalid provenance THM reference fails"
          (valid
           |> editSpecimen "THM-0002.json" (set [ "provenance"; "source" ] (str "THM-0099"))
           |> expectErrors [ "invalid-theme-reference" ])

      test "non-THM provenance sources are not treated as theme references"
          (valid
           |> editSpecimen "THM-0001.json" (set [ "provenance"; "source" ] (str "https://example.com/palettes/THM-0099"))
           |> editSpecimen "THM-0002.json" (set [ "provenance"; "source" ] (str "Adapted from THM-0099 notes"))
           |> expectValid)

      test "replaces referencing a missing theme fails"
          (valid |> editSpecimen "THM-0002.json" (set [ "replaces" ] (str "THM-0099")) |> expectErrors [ "invalid-theme-reference" ])

      test "replaces that is not a THM identifier fails"
          (valid |> editSpecimen "THM-0002.json" (set [ "replaces" ] (str "THM-99")) |> expectErrors [ "invalid-theme-reference" ])

      test "replaces an existing theme and null replaces are valid"
          (valid
           |> editSpecimen "THM-0002.json" (set [ "replaces" ] (str "THM-0001"))
           |> editSpecimen "THM-0001.json" (setNull [ "replaces" ])
           |> expectValid)

      test "a theme replacing itself fails"
          (valid |> editSpecimen "THM-0002.json" (set [ "replaces" ] (str "THM-0002")) |> expectErrors [ "self-reference" ])

      test "a theme derived from itself fails"
          (valid
           |> editSpecimen "THM-0002.json" (set [ "provenance"; "source" ] (str "THM-0002"))
           |> expectErrors [ "self-reference" ])

      test "a replacement cycle fails"
          (valid
           |> editSpecimen "THM-0001.json" (set [ "replaces" ] (str "THM-0002"))
           |> editSpecimen "THM-0002.json" (set [ "replaces" ] (str "THM-0001"))
           |> expectErrors [ "replaces-cycle" ])

      test "incorrect specimen.path fails"
          (valid
           |> editSpecimen "THM-0002.json" (set [ "specimen"; "path" ] (str "content/themes/specimens/THM-0001.json"))
           |> expectErrors [ "specimen-path-mismatch" ])

      test "null required specimen string fails cleanly"
          (valid |> editSpecimen "THM-0002.json" (setNull [ "name" ]) |> expectErrors [ "invalid-field" ])

      test "empty required specimen string fails cleanly"
          (valid |> editSpecimen "THM-0002.json" (set [ "mode" ] (str "  ")) |> expectErrors [ "invalid-field" ])

      test "non-string specimen id fails cleanly"
          (valid |> editSpecimen "THM-0002.json" (set [ "id" ] (num 2)) |> expectErrors [ "invalid-field" ])

      test "missing required semantic token fails cleanly"
          (valid |> editSpecimen "THM-0002.json" (remove [ "semanticTokens"; "text" ]) |> expectErrors [ "invalid-field" ])

      test "malformed palette color fails cleanly"
          (valid |> editSpecimen "THM-0002.json" (set [ "palette"; "glow" ] (str "amber")) |> expectErrors [ "invalid-palette-color" ])

      test "null index string fails cleanly"
          (valid |> editIndex (setNull [ "themes"; "0"; "name" ]) |> expectErrors [ "invalid-field" ])

      test "malformed specimen JSON fails without being reported missing"
          (valid |> withSpecimen "THM-0002.json" (Raw "{ \"id\": \"THM-0002\", ") |> expectErrors [ "malformed-json" ])

      test "malformed index JSON fails"
          ({ valid with Index = Some(Raw "{ \"nextId\": ") } |> expectErrors [ "malformed-json" ])

      test "index without nextId and themes fails cleanly"
          ({ valid with Index = Some(Json(obj [])) } |> expectErrors [ "invalid-field" ])

      test "missing index fails" ({ valid with Index = None } |> expectErrors [ "index-missing" ])

      test "specimen filename that is not THM-NNNN fails"
          (valid |> withSpecimen "THM-2.json" (Json dark) |> expect [ "invalid-specimen-filename"; "duplicate-theme-id"; "specimen-path-mismatch" ] []) ]

[<EntryPoint>]
let main _ =
    let failures =
        tests
        |> List.choose (fun t ->
            let outcome =
                try
                    t.Check()
                with ex ->
                    Error(sprintf "threw %s: %s" (ex.GetType().Name) ex.Message)
            match outcome with
            | Ok() -> None
            | Error message -> Some(t.Name, message))
    failures |> List.iter (fun (name, message) -> eprintfn "FAIL %s\n%s" name message)
    printfn "theme-catalog integrity tests: %d passed, %d failed" (tests.Length - failures.Length) failures.Length
    if failures.IsEmpty then 0 else 1
