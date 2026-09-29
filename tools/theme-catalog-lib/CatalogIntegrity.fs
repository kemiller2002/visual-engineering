namespace VisualEngineering.ThemeCatalog

open System

/// Structural integrity of the theme catalog: identity, index consistency, exact palette identity,
/// and references. Deliberately excludes perceptual similarity, which is not an integrity property.
module CatalogIntegrity =

    /// Structurally usable inputs plus the findings that made anything else unusable.
    type ParsedCatalog =
        { Index: CatalogIndex option
          Specimens: Specimen list
          Unusable: UnusableSpecimen list
          Structural: Diagnostic list }

    let private idText = ThemeId.format >> Some
    let private ordinal (a: string) (b: string) = String.CompareOrdinal(a, b)
    let private joinSorted (items: string seq) = items |> Seq.sortWith ordinal |> String.concat ", "
    let private groupsOfMany key items =
        items |> List.groupBy key |> List.filter (snd >> List.length >> (<) 1)

    let parse (sources: CatalogSources) : ParsedCatalog =
        let index, indexFindings =
            match sources.Index with
            | None ->
                None, [ Diagnostic.error IndexMissing None (Some sources.IndexPath) "catalog index file does not exist" ]
            | Some file ->
                match CatalogParsing.parseIndex file with
                | Ok index -> Some index, []
                | Error findings -> None, findings
        let filenameFindings =
            sources.Specimens
            |> List.filter (fun file -> CatalogParsing.fileThemeId file.Path |> Option.isNone)
            |> List.map (fun file ->
                Diagnostic.error InvalidSpecimenFilename None (Some file.Path) "specimen filenames must be THM-NNNN.json")
        let parsed = sources.Specimens |> List.map (fun file -> file, CatalogParsing.parseSpecimen file)
        { Index = index
          Specimens = parsed |> List.choose (snd >> Result.toOption)
          Unusable =
            parsed
            |> List.filter (snd >> Result.isError)
            |> List.map (fun (file, _) -> { Path = file.Path; FileId = CatalogParsing.fileThemeId file.Path })
          Structural =
            indexFindings
            @ filenameFindings
            @ (parsed |> List.collect (fun (_, result) ->
                match result with
                | Error findings -> findings
                | Ok _ -> [])) }

    /// Theme IDs that exist as specimen files, including files that failed structural parsing,
    /// so a malformed specimen is reported as malformed rather than also as missing.
    let private presentIds (catalog: ParsedCatalog) =
        Set.union
            (catalog.Specimens |> List.map (fun s -> s.Id) |> Set.ofList)
            (catalog.Unusable |> List.choose (fun u -> u.FileId) |> Set.ofList)

    let private specimensWithId (catalog: ParsedCatalog) id =
        catalog.Specimens |> List.filter (fun s -> s.Id = id)

    let duplicateSpecimenIds (catalog: ParsedCatalog) =
        catalog.Specimens
        |> groupsOfMany (fun s -> s.Id)
        |> List.map (fun (id, group) ->
            Diagnostic.error DuplicateThemeId (idText id) None
                (sprintf "is authored by %d specimens: %s" group.Length (group |> List.map (fun s -> s.Path) |> joinSorted)))

    let duplicateIndexIds (catalog: ParsedCatalog) =
        catalog.Index
        |> Option.map (fun index ->
            index.Entries
            |> groupsOfMany (fun e -> e.Id)
            |> List.map (fun (id, group) ->
                Diagnostic.error DuplicateIndexId (idText id) (Some index.Path)
                    (sprintf "appears %d times in the index (themes[%s])" group.Length
                        (group |> List.map (fun e -> string e.Position) |> String.concat ", "))))
        |> Option.defaultValue []

    let filenameIdentity (catalog: ParsedCatalog) =
        catalog.Specimens
        |> List.choose (fun s ->
            match CatalogParsing.fileThemeId s.Path with
            | Some fileId when fileId <> s.Id ->
                Some(Diagnostic.error FilenameIdMismatch (idText s.Id) (Some s.Path)
                    (sprintf "file is named %s.json but authors id %s" (ThemeId.format fileId) (ThemeId.format s.Id)))
            | _ -> None)

    let specimenPaths (catalog: ParsedCatalog) =
        catalog.Specimens
        |> List.choose (fun s ->
            match s.SpecimenPath with
            | Some authored when authored <> s.Path ->
                Some(Diagnostic.error SpecimenPathMismatch (idText s.Id) (Some s.Path)
                    (sprintf "specimen.path is '%s' but the specimen is at '%s'" authored s.Path))
            | _ -> None)

    let indexCompleteness (catalog: ParsedCatalog) =
        catalog.Index
        |> Option.map (fun index ->
            let present = presentIds catalog
            let indexed = index.Entries |> List.map (fun e -> e.Id) |> Set.ofList
            let missingSpecimens =
                index.Entries
                |> List.filter (fun e -> not (present.Contains e.Id))
                |> List.map (fun e ->
                    Diagnostic.error IndexSpecimenMissing (idText e.Id) (Some index.Path)
                        (sprintf "index entry themes[%d] has no specimen (indexed path '%s')" e.Position e.Path))
            let unindexed =
                catalog.Specimens
                |> List.filter (fun s -> not (indexed.Contains s.Id))
                |> List.map (fun s ->
                    Diagnostic.error SpecimenIndexMissing (idText s.Id) (Some s.Path) "specimen has no index entry")
            missingSpecimens @ unindexed)
        |> Option.defaultValue []

    let indexPaths (catalog: ParsedCatalog) =
        catalog.Index
        |> Option.map (fun index ->
            index.Entries
            |> List.choose (fun e ->
                match specimensWithId catalog e.Id with
                | [] -> None
                | specimens when specimens |> List.exists (fun s -> s.Path = e.Path) -> None
                | specimens ->
                    Some(Diagnostic.error IndexPathMismatch (idText e.Id) (Some index.Path)
                        (sprintf "index path is '%s' but the specimen is at '%s'" e.Path
                            (specimens |> List.map (fun s -> s.Path) |> joinSorted)))))
        |> Option.defaultValue []

    /// The specimen is authoritative; the index is a retrieval projection of it, so an index entry
    /// that omits a facet the specimen authors is as wrong as one that contradicts it. Index temperature
    /// projects the specimen's facets.temperature retrieval facet, not perception.temperature,
    /// which is a separately evidenced perceptual descriptor.
    let indexMetadata (catalog: ParsedCatalog) =
        catalog.Index
        |> Option.map (fun index ->
            index.Entries
            |> List.collect (fun e ->
                match specimensWithId catalog e.Id with
                | [ s ] ->
                    let mismatch field indexed authored =
                        Diagnostic.error IndexMetadataMismatch (idText e.Id) (Some index.Path)
                            (sprintf "index %s is %s but the specimen authors %s" field indexed authored)
                    let quote = sprintf "'%s'"
                    let setText values = values |> Set.toList |> joinSorted |> sprintf "[%s]"
                    [ if e.Name <> s.Name then mismatch "name" (quote e.Name) (quote s.Name)
                      if e.Status <> ThemeStatus.name s.Status then
                          mismatch "status" (quote e.Status) (quote (ThemeStatus.name s.Status))
                      if e.Mode <> s.Mode then mismatch "mode" (quote e.Mode) (quote s.Mode)
                      match e.Temperature, s.Temperature with
                      | Some indexed, Some authored when indexed <> authored ->
                          mismatch "temperature" (quote indexed) (sprintf "facets.temperature %s" (quote authored))
                      | Some indexed, None -> mismatch "temperature" (quote indexed) "no facets.temperature"
                      | None, Some authored ->
                          mismatch "temperature" "absent" (sprintf "facets.temperature %s" (quote authored))
                      | _ -> ()
                      match e.Contexts with
                      | Some indexed when indexed <> s.Contexts ->
                          mismatch "contexts" (setText indexed) (setText s.Contexts)
                      | None -> mismatch "contexts" "absent" (setText s.Contexts)
                      | _ -> () ]
                | _ -> []))
        |> Option.defaultValue []

    /// Every semantic token, required or optional, must name a key of the same specimen's palette.
    let semanticTokenReferences (catalog: ParsedCatalog) =
        catalog.Specimens
        |> List.collect (fun s ->
            s.SemanticTokens
            |> Map.toList
            |> List.filter (fun (_, key) -> not (s.Palette.ContainsKey key))
            |> List.map (fun (token, key) ->
                Diagnostic.error SemanticTokenMissingPaletteKey (idText s.Id) (Some s.Path)
                    (sprintf "semanticTokens.%s references palette key '%s', which does not exist" token key)))

    let nextId (catalog: ParsedCatalog) =
        catalog.Index
        |> Option.map (fun index ->
            let fileIds =
                (catalog.Specimens |> List.map (fun s -> s.Path)) @ (catalog.Unusable |> List.map (fun u -> u.Path))
                |> List.choose CatalogParsing.fileThemeId
            let existing =
                Set.unionMany
                    [ presentIds catalog
                      Set.ofList fileIds
                      index.Entries |> List.map (fun e -> e.Id) |> Set.ofList ]
            let expected = ThemeId.next existing |> ThemeId.format
            if index.NextId = expected then []
            else
                [ Diagnostic.error NextIdMismatch None (Some index.Path)
                      (sprintf "nextId is '%s' but must be %s (highest existing ID + 1)" index.NextId expected) ])
        |> Option.defaultValue []

    let fingerprints (catalog: ParsedCatalog) =
        catalog.Specimens
        |> List.choose (fun s ->
            let canonical = Fingerprint.ofPalette s.Palette
            if s.Fingerprint = canonical then None
            else
                Some(Diagnostic.error FingerprintMismatch (idText s.Id) (Some s.Path)
                    (sprintf "stored fingerprint '%s' does not equal the canonical palette fingerprint '%s'" s.Fingerprint canonical)))

    /// Same exact palette is permitted doctrine when the semantic mapping materially differs, and is
    /// reported as a notice. Same palette AND same resolved mapping under two non-deprecated IDs is
    /// the same theme recorded twice, which is an error. Deprecation is the documented relationship
    /// that lets an identical definition persist under a retired ID. Only distinct theme IDs are
    /// compared; two files authoring one ID are reported as duplicate-theme-id instead.
    let duplicates (catalog: ParsedCatalog) =
        catalog.Specimens
        |> List.distinctBy (fun s -> s.Id)
        |> groupsOfMany (fun s -> Fingerprint.ofPalette s.Palette)
        |> List.collect (fun (fingerprint, group) ->
            let ordered = group |> List.sortBy (fun s -> s.Id)
            let ids = ordered |> List.map (fun s -> ThemeId.format s.Id) |> String.concat ", "
            let palette =
                Diagnostic.notice DuplicatePalette (idText ordered.Head.Id) None
                    (sprintf "%s share the exact palette %s; permitted only while their semantic mappings materially differ" ids fingerprint)
            let definitions =
                ordered
                |> groupsOfMany (fun s -> Fingerprint.resolvedMapping s.Palette s.SemanticTokens)
                |> List.choose (fun (_, same) ->
                    match same |> List.filter (fun s -> s.Status <> Deprecated) with
                    | first :: _ :: _ as active ->
                        Some(Diagnostic.error DuplicateThemeDefinition (idText first.Id) None
                            (sprintf "%s have identical palettes and identical semantic mappings; deprecate the redundant record and point its successor's replaces at it"
                                (active |> List.map (fun s -> ThemeId.format s.Id) |> String.concat ", ")))
                    | _ -> None)
            palette :: definitions)

    /// Only values that are, in their entirety, THM-NNNN identifiers are theme references.
    /// Provenance URLs, document paths, and prose are not interpreted.
    let references (catalog: ParsedCatalog) =
        let known = presentIds catalog
        catalog.Specimens
        |> List.collect (fun s ->
            let check field (target: ThemeId) =
                if target = s.Id then
                    [ Diagnostic.error SelfReference (idText s.Id) (Some s.Path) (sprintf "%s references the theme itself" field) ]
                elif known.Contains target then []
                else
                    [ Diagnostic.error InvalidThemeReference (idText s.Id) (Some s.Path)
                          (sprintf "%s references %s, which does not exist" field (ThemeId.format target)) ]
            let source =
                s.ProvenanceSource
                |> Option.bind ThemeId.tryParse
                |> Option.map (check "provenance.source")
                |> Option.defaultValue []
            let replaces =
                match s.Replaces with
                | None -> []
                | Some text ->
                    match ThemeId.tryParse text with
                    | Some target -> check "replaces" target
                    | None ->
                        [ Diagnostic.error InvalidThemeReference (idText s.Id) (Some s.Path)
                              (sprintf "replaces '%s' is not a THM-NNNN identifier" text) ]
            source @ replaces)

    /// A replacement chain that returns to its start makes every member its own successor.
    let replacementCycles (catalog: ParsedCatalog) =
        let graph =
            catalog.Specimens
            |> List.choose (fun s ->
                s.Replaces |> Option.bind ThemeId.tryParse |> Option.filter ((<>) s.Id) |> Option.map (fun t -> s.Id, t))
            |> Map.ofList
        let rec cycleFrom start path current =
            match Map.tryFind current graph with
            | Some next when next = start -> Some(List.rev (current :: path))
            | Some next when List.contains next path || next = current -> None
            | Some next -> cycleFrom start (current :: path) next
            | None -> None
        graph
        |> Map.toList
        |> List.choose (fun (start, _) -> cycleFrom start [] start)
        |> List.distinctBy Set.ofList
        |> List.map (fun cycle ->
            let members = cycle |> List.sort
            Diagnostic.error ReplacesCycle (idText members.Head) None
                (sprintf "replaces relationships form a cycle: %s"
                    ((cycle @ [ cycle.Head ]) |> List.map ThemeId.format |> String.concat " -> ")))

    let private checks : (ParsedCatalog -> Diagnostic list) list =
        [ duplicateSpecimenIds
          duplicateIndexIds
          filenameIdentity
          specimenPaths
          indexCompleteness
          indexPaths
          indexMetadata
          semanticTokenReferences
          nextId
          fingerprints
          duplicates
          references
          replacementCycles ]

    /// Every finding for the catalog, in a total deterministic order.
    let validateParsed (catalog: ParsedCatalog) : Diagnostic list =
        catalog.Structural @ (checks |> List.collect (fun check -> check catalog))
        |> List.distinct
        |> List.sortBy Diagnostic.sortKey

    let validate: CatalogSources -> Diagnostic list = parse >> validateParsed
