open System.Text.Json
open VisualEngineering.ThemeCatalog

type Options =
    { RepositoryRoot: string
      CatalogDirectory: string
      Json: bool }

let usage = "Usage: theme-catalog-validate [--repository-root DIR] [--catalog DIR] [--json]"

let rec parseArgs options args =
    match args with
    | [] -> Ok options
    | "--repository-root" :: value :: rest -> parseArgs { options with RepositoryRoot = value } rest
    | "--catalog" :: value :: rest -> parseArgs { options with CatalogDirectory = value } rest
    | "--json" :: rest -> parseArgs { options with Json = true } rest
    | unknown :: _ -> Error(sprintf "Unrecognized argument '%s'" unknown)

let severityName severity =
    match severity with
    | Severity.Error -> "error"
    | Severity.Notice -> "notice"

let textLine (d: Diagnostic) =
    let subject =
        [ d.ThemeId; d.Path ] |> List.choose id |> String.concat " "
    sprintf "%s %s %s: %s" (severityName d.Severity) (DiagnosticCode.name d.Code) subject d.Message

let jsonReport (diagnostics: Diagnostic list) =
    let findings =
        diagnostics
        |> List.map (fun d ->
            {| severity = severityName d.Severity
               code = DiagnosticCode.name d.Code
               themeId = Option.toObj d.ThemeId
               path = Option.toObj d.Path
               message = d.Message |})
    let report =
        {| validatorVersion = "1.0.0"
           valid = diagnostics |> List.exists Diagnostic.isError |> not
           errors = diagnostics |> List.filter Diagnostic.isError |> List.length
           notices = diagnostics |> List.filter (Diagnostic.isError >> not) |> List.length
           diagnostics = findings |}
    JsonSerializer.Serialize(report, JsonSerializerOptions(WriteIndented = true))

let summary (diagnostics: Diagnostic list) =
    let errors = diagnostics |> List.filter Diagnostic.isError |> List.length
    sprintf "theme catalog integrity: %d error(s), %d notice(s)" errors (diagnostics.Length - errors)

[<EntryPoint>]
let main argv =
    let defaults =
        { RepositoryRoot = "."
          CatalogDirectory = CatalogFiles.defaultCatalogDirectory
          Json = false }
    match parseArgs defaults (List.ofArray argv) with
    | Error message ->
        eprintfn "%s\n%s" message usage
        2
    | Ok options ->
        let diagnostics = CatalogFiles.validateDirectory options.RepositoryRoot options.CatalogDirectory
        if options.Json then
            printfn "%s" (jsonReport diagnostics)
        else
            diagnostics |> List.iter (textLine >> printfn "%s")
            printfn "%s" (summary diagnostics)
        if diagnostics |> List.exists Diagnostic.isError then 1 else 0
