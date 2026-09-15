namespace VisualEngineering.Core

open System

/// How the desired content of a managed path is expressed.
type DesiredContent =
    /// The whole file is the tool's. Its content must match exactly.
    | ExactText of string
    /// Only a delimited region belongs to the tool. Everything else is user content.
    | Region of style: BlockStyle * integration: string * body: string
    /// A JSON document the tool owns structurally. Compared after normalization so that
    /// reformatting by a user or an editor is not mistaken for a semantic change.
    | NormalizedJson of text: string

/// One path the installation is expected to contain, with its ownership and desired content.
type DesiredArtifact =
    { Path: RepoPath
      Ownership: Ownership
      Content: DesiredContent }

/// Derives the desired repository state from the packaged payload and the repository
/// configuration. This is a pure function: it inspects nothing and writes nothing.
[<RequireQualifiedAccess>]
module Desired =

    let manifestPath =
        RepoPath.create (Tool.EchelonDirectory + "/" + Tool.Id + ".json")

    let configurationPath =
        RepoPath.create (Tool.EchelonDirectory + "/" + Tool.Id + ".config.json")

    let gitignorePath = RepoPath.create ".gitignore"

    [<Literal>]
    let AgentsIntegration = "agents-briefing"

    [<Literal>]
    let GitignoreIntegration = "gitignore"

    /// Payload files whose content is generated from research inputs rather than maintained by hand.
    let private generatedFiles =
        set [ "RESEARCH-INDEX.md"; "sources.json"; "context.json" ]

    let ownershipOfPayloadFile (file: RepoPath) =
        if generatedFiles.Contains(RepoPath.fileName file) then Generated else ToolOwned

    /// Body of the managed region registered in the repository's agent instructions file.
    let agentsRegionBody (contextDirectory: RepoPath) =
        let directory = RepoPath.value contextDirectory

        Text.lines
            [ "## Visual Engineering UI research"
              ""
              $"Managed by `npx {Tool.PackageName}`. Do not edit inside this block."
              ""
              "Before designing, implementing, or reviewing UI:"
              ""
              $"1. Run `npx {Tool.PackageName} verify` and stop if it reports a failure."
              $"2. Read `{directory}/AGENT-INSTRUCTIONS.md`."
              $"3. Read `{directory}/UI-FOUNDATIONS.md`."
              $"4. Read `{directory}/UI-DECISION-CHECKLIST.md`."
              $"5. Read `{directory}/UI-ANTI-PATTERNS.md`."
              $"6. Consult `{directory}/RESEARCH-INDEX.md` for provenance and deeper evidence."
              "7. Inspect the product and its existing design system."
              "8. Apply the research as decision criteria, not as a visual style."
              "9. Report the context version, source commit, principles applied, verification"
              "   performed, and justified deviations."
              ""
              "Do not copy Visual Engineering research into this repository by hand." ]
        |> fun text -> text.TrimEnd '\n'

    /// Body of the managed region added to the repository's `.gitignore`.
    let gitignoreRegionBody (contextDirectory: RepoPath) =
        String.Join(
            '\n',
            [ "# Installed Visual Engineering context. Refresh it with:"
              $"#   npx {Tool.PackageName} init"
              RepoPath.value contextDirectory + "/" ]
        )

    /// Renders the repository configuration, preserving keys the tool does not own.
    let renderConfiguration (configuration: Configuration) =
        let integrations =
            JObject
                [ "agentsFile", (configuration.AgentsFile |> Option.map RepoPath.value |> Json.ofStringOption)
                  "gitignore", JBool configuration.ManageGitignore ]

        let owned =
            [ "schemaVersion", JInt configuration.SchemaVersion
              "tool", JString Tool.Id
              "configurationVersion", JInt configuration.ConfigurationVersion
              "contextDirectory", JString(RepoPath.value configuration.ContextDirectory)
              "integrations", integrations ]

        let extras =
            configuration.Extra
            |> List.filter (fun (key, _) -> owned |> List.forall (fun (owned, _) -> owned <> key))

        Json.serialize (JObject(owned @ extras))

    /// Renders the installation manifest.
    let renderManifest (manifest: InstallationManifest) =
        let artifacts =
            manifest.ManagedArtifacts
            |> List.sortBy (fun artifact -> RepoPath.value artifact.Path)
            |> List.map (fun artifact ->
                JObject
                    [ "path", JString(RepoPath.value artifact.Path)
                      "ownership", JString(Ownership.toString artifact.Ownership)
                      "sha256", Json.ofStringOption artifact.Sha256 ])

        Json.serialize (
            JObject
                [ "schemaVersion", JInt manifest.SchemaVersion
                  "tool", JString manifest.Tool
                  "installedVersion", JString manifest.InstalledVersion
                  "configurationVersion", JInt manifest.ConfigurationVersion
                  "contextVersion", JString manifest.ContextVersion
                  "contextDirectory", JString(RepoPath.value manifest.ContextDirectory)
                  "managedArtifacts", JArray artifacts ]
        )

    /// Every managed path other than the installation manifest, which is derived from these.
    let artifacts (payload: Payload) (configuration: Configuration) : DesiredArtifact list =
        let contextArtifacts =
            payload.Artifacts
            |> List.map (fun artifact ->
                let path =
                    RepoPath.create (
                        RepoPath.value configuration.ContextDirectory
                        + "/"
                        + RepoPath.value artifact.File
                    )

                { Path = path
                  Ownership = ownershipOfPayloadFile artifact.File
                  Content = ExactText artifact.Content })
            |> List.sortBy (fun artifact -> RepoPath.value artifact.Path)

        let configurationArtifact =
            { Path = configurationPath
              Ownership = Shared
              Content = NormalizedJson(renderConfiguration configuration) }

        let agentsArtifact =
            configuration.AgentsFile
            |> Option.map (fun file ->
                { Path = file
                  Ownership = Shared
                  Content =
                    Region(
                        MarkdownComment,
                        AgentsIntegration,
                        agentsRegionBody configuration.ContextDirectory
                    ) })
            |> Option.toList

        let gitignoreArtifact =
            if configuration.ManageGitignore then
                [ { Path = gitignorePath
                    Ownership = Shared
                    Content =
                      Region(
                          HashComment,
                          GitignoreIntegration,
                          gitignoreRegionBody configuration.ContextDirectory
                      ) } ]
            else
                []

        contextArtifacts @ [ configurationArtifact ] @ agentsArtifact @ gitignoreArtifact

    /// Directories the installation requires.
    let directories (configuration: Configuration) =
        [ configuration.ContextDirectory
          RepoPath.create Tool.EchelonDirectory ]

    /// Hash the tool records for a desired artifact. For a whole file it is the file hash;
    /// for a shared file it is the hash of the region body only.
    let recordedHash (artifact: DesiredArtifact) =
        match artifact.Content with
        | ExactText text -> Hash.ofText text
        | NormalizedJson text -> Hash.ofText text
        | Region(_, _, body) -> ManagedBlock.bodyHash body

    /// Reformats a JSON document so that only semantic differences remain.
    let normalizeJson (text: string) =
        match Json.tryParse text with
        | Error _ -> None
        | Ok document ->
            use document = document
            Some(Json.serialize (Json.ofElement document.RootElement))
