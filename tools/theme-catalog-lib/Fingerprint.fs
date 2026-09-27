namespace VisualEngineering.ThemeCatalog

open System
open System.Text.RegularExpressions

/// Exact palette identity. This is string identity over normalized colors, not perceptual similarity.
module Fingerprint =
    let private hexPattern = Regex("^#([0-9A-Fa-f]{6})([0-9A-Fa-f]{2})?$", RegexOptions.CultureInvariant)

    /// Normalizes an authored palette color: upper-case hex digits, and a fully opaque
    /// #RRGGBBFF collapsed to #RRGGBB so one color has exactly one spelling.
    /// Any other alpha is retained because it denotes a different color.
    let normalizeHex (value: string) : CanonicalHex option =
        match value with
        | null -> None
        | text ->
            let m = hexPattern.Match text
            if not m.Success then None
            else
                let rgb = m.Groups[1].Value.ToUpperInvariant()
                let alpha = m.Groups[2].Value.ToUpperInvariant()
                match alpha with
                | "" | "FF" -> Some(CanonicalHex("#" + rgb))
                | a -> Some(CanonicalHex("#" + rgb + a))

    let hexText (CanonicalHex hex) = hex

    /// Canonical exact-palette fingerprint: every normalized palette value, independent of its
    /// role-free key and of any semantic role, sorted ordinally and joined with "|".
    let ofPalette (palette: Map<string, CanonicalHex>) : string =
        palette
        |> Map.toList
        |> List.map (snd >> hexText)
        |> List.sortWith (fun a b -> String.CompareOrdinal(a, b))
        |> String.concat "|"

    /// Resolves each semantic token to the color it names. Two themes with the same resolved
    /// mapping present the same colors in the same roles, whatever their palette keys are called.
    /// Tokens naming a missing palette key resolve to None and are reported separately.
    let resolvedMapping (palette: Map<string, CanonicalHex>) (tokens: Map<string, string>) =
        tokens |> Map.map (fun _ key -> palette |> Map.tryFind key |> Option.map hexText)
