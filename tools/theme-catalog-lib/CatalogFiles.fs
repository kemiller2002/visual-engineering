namespace VisualEngineering.ThemeCatalog

open System
open System.IO

/// File-system boundary. Reads the catalog once; all validation is a pure function of the result.
module CatalogFiles =
    let defaultCatalogDirectory = "content/themes"

    /// Reads <catalog>/index.json and every <catalog>/specimens/THM-*.json. Paths are recorded
    /// repository-relative with forward slashes, the form index and specimen paths are authored in.
    let read (repositoryRoot: string) (catalogDirectory: string) : CatalogSources =
        let relative (path: string) = Path.GetRelativePath(repositoryRoot, path).Replace('\\', '/')
        let catalog = Path.Combine(repositoryRoot, catalogDirectory)
        let indexFile = Path.Combine(catalog, "index.json")
        let specimenDirectory = Path.Combine(catalog, "specimens")
        let load path = { Path = relative path; Text = File.ReadAllText path }
        { IndexPath = relative indexFile
          Index = if File.Exists indexFile then Some(load indexFile) else None
          Specimens =
            if Directory.Exists specimenDirectory then
                Directory.GetFiles(specimenDirectory, "THM-*.json")
                |> Array.sortWith (fun a b -> String.CompareOrdinal(a, b))
                |> Array.map load
                |> Array.toList
            else
                [] }

    let validateDirectory (repositoryRoot: string) (catalogDirectory: string) : Diagnostic list =
        read repositoryRoot catalogDirectory |> CatalogIntegrity.validate
