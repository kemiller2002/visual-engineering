/// Miniature on-disk catalogs. Every edit clones its input, so fixtures compose without sharing state.
module Fixtures

open System
open System.Collections.Generic
open System.IO
open System.Text.Json.Nodes
open VisualEngineering.ThemeCatalog

/// Unwraps a possibly-null JSON API result; a fixture that navigates to nothing is a test bug.
let private nonNull context node =
    match Option.ofObj node with
    | Some value -> value
    | None -> failwithf "fixture %s is null or missing" context

let str (value: string) : JsonNode = (JsonValue.Create value |> nonNull "string value") :> JsonNode
let num (value: int) : JsonNode = (JsonValue.Create value |> nonNull "number value") :> JsonNode
let arr (items: JsonNode list) : JsonNode = JsonArray(Array.ofList items)
let obj (members: (string * JsonNode) list) : JsonNode =
    JsonObject(members |> List.map KeyValuePair) :> JsonNode

let private child (node: JsonNode) (key: string) : JsonNode =
    match node with
    | :? JsonArray as items -> items[int key] |> nonNull key
    | _ -> node[key] |> nonNull key

let private cloned (change: JsonNode -> unit) (node: JsonNode) : JsonNode =
    let copy = node.DeepClone()
    change copy
    copy

let private parentOf (path: string list) (root: JsonNode) = path |> List.take (path.Length - 1) |> List.fold child root

/// Sets (or adds) the member or array element at path. Values are cloned so they can be reused.
let set (path: string list) (value: JsonNode) : JsonNode -> JsonNode =
    cloned (fun root ->
        match parentOf path root with
        | :? JsonArray as items -> items[int (List.last path)] <- value.DeepClone()
        | parent -> parent[List.last path] <- value.DeepClone())

/// Sets the member at path to JSON null.
let setNull (path: string list) : JsonNode -> JsonNode =
    cloned (fun root -> (parentOf path root)[List.last path] <- null)

let remove (path: string list) : JsonNode -> JsonNode =
    cloned (fun root -> (parentOf path root).AsObject().Remove(List.last path) |> ignore)

let append (path: string list) (value: JsonNode) : JsonNode -> JsonNode =
    cloned (fun root -> (List.fold child root path).AsArray().Add(value.DeepClone()))

let removeAt (path: string list) (position: int) : JsonNode -> JsonNode =
    cloned (fun root -> (List.fold child root path).AsArray().RemoveAt position)

type Content =
    | Json of JsonNode
    | Raw of string

type Catalog =
    { Index: Content option
      /// File name (e.g. THM-0001.json) to content.
      Specimens: (string * Content) list }

let specimenPath (fileName: string) = "content/themes/specimens/" + fileName

let specimen id name status mode temperature (palette: (string * string) list) (tokens: (string * string) list) fingerprint =
    obj [ "id", str id
          "name", str name
          "status", str status
          "mode", str mode
          "palette", obj (palette |> List.map (fun (k, v) -> k, str v))
          "semanticTokens", obj (tokens |> List.map (fun (k, v) -> k, str v))
          "contexts", arr [ str "application"; str "dashboard" ]
          "tags", arr [ str "fixture" ]
          "facets", obj [ "temperature", str temperature ]
          "provenance", obj [ "kind", str "designed" ]
          "accessibility", obj [ "status", str "not-evaluated"; "contrastPairs", arr [] ]
          "specimen", obj [ "surfaces", arr [ str "reference UI" ]; "path", str (specimenPath (id + ".json")) ]
          "fingerprint", str fingerprint ]

let indexEntry id name status mode temperature =
    obj [ "id", str id
          "name", str name
          "status", str status
          "mode", str mode
          "temperature", str temperature
          "contexts", arr [ str "application"; str "dashboard" ]
          "path", str (specimenPath (id + ".json")) ]

let lightPalette = [ "paper", "#FFFFFF"; "ink", "#111111"; "accent", "#0055AA"; "line", "#CCCCCC" ]
let lightTokens =
    [ "background", "paper"; "surface", "paper"; "text", "ink"; "mutedText", "ink"; "primaryAccent", "accent"; "border", "line" ]
let lightFingerprint = "#0055AA|#111111|#CCCCCC|#FFFFFF"

let darkPalette = [ "night", "#101418"; "snow", "#F0F0F0"; "glow", "#FFB000"; "rule", "#303840" ]
let darkTokens =
    [ "background", "night"; "surface", "night"; "text", "snow"; "mutedText", "snow"; "primaryAccent", "glow"
      "border", "rule"; "success", "glow" ]
let darkFingerprint = "#101418|#303840|#F0F0F0|#FFB000"

let light = specimen "THM-0001" "Base Light" "evaluated" "light" "cool" lightPalette lightTokens lightFingerprint
let dark =
    specimen "THM-0002" "Base Dark" "draft" "dark" "warm" darkPalette darkTokens darkFingerprint
    |> set [ "provenance" ] (obj [ "kind", str "derived"; "source", str "THM-0001" ])

let index =
    obj [ "catalogVersion", str "1.0.0"
          "schemaVersion", str "1.0.0"
          "nextId", str "THM-0003"
          "themes",
          arr [ indexEntry "THM-0001" "Base Light" "evaluated" "light" "cool"
                indexEntry "THM-0002" "Base Dark" "draft" "dark" "warm" ] ]

/// A valid two-theme catalog: THM-0001 (light) and THM-0002 (dark, derived from THM-0001).
let valid: Catalog =
    { Index = Some(Json index)
      Specimens = [ "THM-0001.json", Json light; "THM-0002.json", Json dark ] }

let editIndex change (catalog: Catalog) : Catalog =
    { catalog with
        Index =
            catalog.Index
            |> Option.map (function
                | Json node -> Json(change node)
                | raw -> raw) }

let editSpecimen fileName change (catalog: Catalog) : Catalog =
    { catalog with
        Specimens =
            catalog.Specimens
            |> List.map (fun (name, file) ->
                match file with
                | Json node when name = fileName -> name, Json(change node)
                | other -> name, other) }

let withSpecimen fileName file (catalog: Catalog) : Catalog =
    { catalog with Specimens = (catalog.Specimens |> List.filter (fst >> (<>) fileName)) @ [ fileName, file ] }

let withoutSpecimen fileName (catalog: Catalog) : Catalog =
    { catalog with Specimens = catalog.Specimens |> List.filter (fst >> (<>) fileName) }

/// Writes the catalog under a fresh temporary repository root, validates it with the production
/// entry point, and removes the directory.
let validate (catalog: Catalog) : Diagnostic list =
    let root = Path.Combine(Path.GetTempPath(), "theme-catalog-tests-" + Guid.NewGuid().ToString("N"))
    let specimens = Path.Combine(root, "content", "themes", "specimens")
    Directory.CreateDirectory specimens |> ignore
    try
        let text content =
            match content with
            | Json node -> node.ToJsonString()
            | Raw raw -> raw
        catalog.Index
        |> Option.iter (fun content -> File.WriteAllText(Path.Combine(root, "content", "themes", "index.json"), text content))
        catalog.Specimens |> List.iter (fun (name, content) -> File.WriteAllText(Path.Combine(specimens, name), text content))
        CatalogFiles.validateDirectory root CatalogFiles.defaultCatalogDirectory
    finally
        Directory.Delete(root, true)
