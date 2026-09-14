namespace VisualEngineering.Core

open System
open System.IO
open System.Reflection
open System.Security.Cryptography
open System.Text

/// Identity of the capability this package installs and maintains.
[<RequireQualifiedAccess>]
module Tool =

    /// Stable machine identifier. Used for the manifest file name and JSON payloads.
    [<Literal>]
    let Id = "visual-engineering"

    /// Human readable name used in help text and rendered reports.
    [<Literal>]
    let DisplayName = "Visual Engineering"

    /// npm package that distributes this tool.
    [<Literal>]
    let PackageName = "@echelon-foundry/visual-engineering"

    /// Executable name exposed by the npm `bin` mapping.
    [<Literal>]
    let ExecutableName = "visual-engineering"

    /// Shared Echelon Foundry root directory.
    [<Literal>]
    let EchelonDirectory = ".echelon"

/// Version constants. The CLI version is derived from the assembly, which the build
/// stamps from the single authoritative version passed to `dotnet publish`.
[<RequireQualifiedAccess>]
module Versioning =

    /// Schema version of every JSON document this tool emits or writes.
    [<Literal>]
    let OutputSchemaVersion = 1

    /// Schema version of the installation manifest.
    [<Literal>]
    let ManifestSchemaVersion = 1

    /// Schema version of the repository configuration file.
    [<Literal>]
    let ConfigurationSchemaVersion = 1

    /// Configuration version produced by this release.
    [<Literal>]
    let CurrentConfigurationVersion = 3

    /// Configuration version of a pre-Echelon `ve-context sync` installation.
    [<Literal>]
    let LegacyConfigurationVersion = 1

    /// Every configuration version this release can read and migrate.
    let supportedConfigurationVersions = [ 1; 2; 3 ]

    let private trimBuildMetadata (value: string) =
        match value.IndexOf '+' with
        | -1 -> value
        | index -> value.Substring(0, index)

    /// Version of this CLI, stamped at build time. Never hand maintained.
    let cliVersion =
        let assembly = Assembly.GetExecutingAssembly()

        match assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>() with
        | null ->
            match assembly.GetName().Version with
            | null -> "0.0.0"
            | version -> version.ToString()
        | attribute -> trimBuildMetadata attribute.InformationalVersion

/// Deterministic content hashing. All hashes are lowercase hex SHA-256 over UTF-8 bytes
/// with normalized line endings, so a checkout that applied CRLF translation still verifies.
[<RequireQualifiedAccess>]
module Hash =

    /// Normalizes CRLF and lone CR to LF so hashes are stable across platforms.
    let normalizeText (text: string) =
        text.Replace("\r\n", "\n").Replace("\r", "\n")

    let ofBytes (bytes: byte[]) =
        Convert.ToHexString(SHA256.HashData bytes).ToLowerInvariant()

    let ofText (text: string) =
        text |> normalizeText |> Encoding.UTF8.GetBytes |> ofBytes

/// A path inside a repository. Always relative, always forward slashed, never escaping the root.
[<Sealed>]
type RepoPath private (value: string) =
    member _.Value = value
    override _.ToString() = value
    override _.Equals other =
        match other with
        | :? RepoPath as o -> String.Equals(value, o.Value, StringComparison.Ordinal)
        | _ -> false
    override _.GetHashCode() = value.GetHashCode StringComparison.Ordinal

    interface IComparable with
        member _.CompareTo other =
            match other with
            | :? RepoPath as o -> String.CompareOrdinal(value, o.Value)
            | _ -> 1

    static member internal Make(value: string) = RepoPath value

[<RequireQualifiedAccess>]
module RepoPath =

    let private invalidSegments = set [ "."; ".." ]

    /// Parses a repository relative path, rejecting anything that could escape the repository.
    let tryCreate (raw: string) : Result<RepoPath, string> =
        if String.IsNullOrWhiteSpace raw then
            Error "path is empty"
        elif raw.Contains '\000' then
            Error "path contains a null character"
        else
            let normalized = raw.Replace('\\', '/').Trim()

            if normalized.StartsWith('/') then
                Error $"path must be relative: {raw}"
            elif normalized.Length > 1 && normalized[1] = ':' then
                Error $"path must be relative: {raw}"
            else
                let segments = normalized.Split('/', StringSplitOptions.RemoveEmptyEntries)

                if segments.Length = 0 then
                    Error $"path is empty: {raw}"
                elif segments |> Array.exists (fun segment -> invalidSegments.Contains segment) then
                    Error $"path must not contain relative segments: {raw}"
                elif segments |> Array.exists (fun segment -> segment.EndsWith ' ' || segment.EndsWith '.') then
                    Error $"path segment is not portable: {raw}"
                else
                    Ok(RepoPath.Make(String.Join('/', segments)))

    /// Parses a path that is already known to be valid. Intended for literals in this assembly.
    let create (raw: string) =
        match tryCreate raw with
        | Ok path -> path
        | Error message -> invalidArg (nameof raw) message

    let value (path: RepoPath) = path.Value

    let segments (path: RepoPath) = path.Value.Split '/'

    let fileName (path: RepoPath) = (segments path) |> Array.last

    /// Parent directory, or None when the path sits at the repository root.
    let parent (path: RepoPath) =
        let parts = segments path

        if parts.Length <= 1 then
            None
        else
            Some(RepoPath.Make(String.Join('/', parts[.. parts.Length - 2])))

    let append (path: RepoPath) (child: string) =
        create (path.Value + "/" + child)

    /// Resolves against a repository root and re-checks containment after normalization.
    let toAbsolute (root: string) (path: RepoPath) =
        let rootFull = Path.TrimEndingDirectorySeparator(Path.GetFullPath root)
        let combined = Path.GetFullPath(Path.Combine(rootFull, path.Value))

        let contained =
            combined.StartsWith(rootFull + string Path.DirectorySeparatorChar, StringComparison.Ordinal)

        if not contained then
            failwith $"resolved path escapes the repository root: {path.Value}"

        combined

    let startsWith (prefix: RepoPath) (path: RepoPath) =
        path.Value = prefix.Value || path.Value.StartsWith(prefix.Value + "/", StringComparison.Ordinal)

/// Thin, testable filesystem surface. Every read normalizes line endings for hashing purposes
/// but writes bytes exactly as produced, so output stays deterministic.
[<RequireQualifiedAccess>]
module Files =

    let exists (root: string) (path: RepoPath) =
        File.Exists(RepoPath.toAbsolute root path)

    let directoryExists (root: string) (path: RepoPath) =
        Directory.Exists(RepoPath.toAbsolute root path)

    let tryReadText (root: string) (path: RepoPath) =
        let absolute = RepoPath.toAbsolute root path

        if File.Exists absolute then
            Some(File.ReadAllText absolute)
        else
            None

    let tryHash (root: string) (path: RepoPath) =
        tryReadText root path |> Option.map Hash.ofText

    let writeText (root: string) (path: RepoPath) (content: string) =
        let absolute = RepoPath.toAbsolute root path
        let directory = Path.GetDirectoryName absolute

        if not (String.IsNullOrEmpty directory) then
            Directory.CreateDirectory directory |> ignore

        File.WriteAllText(absolute, content, UTF8Encoding false)

    let createDirectory (root: string) (path: RepoPath) =
        Directory.CreateDirectory(RepoPath.toAbsolute root path) |> ignore

/// Text helpers shared by content generation.
[<RequireQualifiedAccess>]
module Text =

    /// Joins lines with LF and guarantees a single trailing newline.
    let lines (values: string seq) =
        let body = String.Join('\n', values)
        if body.EndsWith '\n' then body else body + "\n"
