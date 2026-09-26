open System
open System.IO
open System.Text.Json

type Rgb = { R: float; G: float; B: float }
type Oklab = { L: float; A: float; B: float }
type Oklch = { L: float; C: float; H: float }

let hexToRgb (hex: string) =
    let h = hex.TrimStart('#')
    if h.Length <> 6 then invalidArg "hex" "Expected #RRGGBB"
    let p i = Convert.ToInt32(h.Substring(i, 2), 16) |> float |> fun v -> v / 255.0
    { R = p 0; G = p 2; B = p 4 }

let linear c = if c <= 0.04045 then c / 12.92 else Math.Pow((c + 0.055) / 1.055, 2.4)
let luminance rgb = 0.2126 * linear rgb.R + 0.7152 * linear rgb.G + 0.0722 * linear rgb.B
let contrast a b =
    let x, y = luminance a, luminance b
    (max x y + 0.05) / (min x y + 0.05)

let oklab rgb =
    let r,g,b = linear rgb.R, linear rgb.G, linear rgb.B
    let l = 0.4122214708*r + 0.5363325363*g + 0.0514459929*b
    let m = 0.2119034982*r + 0.6806995451*g + 0.1073969566*b
    let s = 0.0883024619*r + 0.2817188376*g + 0.6299787005*b
    let l',m',s' = Math.Cbrt l, Math.Cbrt m, Math.Cbrt s
    { L = 0.2104542553*l' + 0.7936177850*m' - 0.0040720468*s'
      A = 1.9779984951*l' - 2.4285922050*m' + 0.4505937099*s'
      B = 0.0259040371*l' + 0.7827717662*m' - 0.8086757660*s' }

let oklch lab =
    let c = sqrt (lab.A*lab.A + lab.B*lab.B)
    let raw = Math.Atan2(lab.B, lab.A) * 180.0 / Math.PI
    { L=lab.L; C=c; H=if raw < 0.0 then raw + 360.0 else raw }

let delta a b = sqrt ((a.L-b.L)**2.0 + (a.A-b.A)**2.0 + (a.B-b.B)**2.0)
let round n x = Math.Round(x, n)

let prop (e:JsonElement) name = e.GetProperty(name)
let str (e:JsonElement) name = (prop e name).GetString()

let evaluate path =
    use doc = JsonDocument.Parse(File.ReadAllText path)
    let root = doc.RootElement
    let palette = prop root "palette"
    let tokens = prop root "semanticTokens"
    let colors =
        palette.EnumerateObject()
        |> Seq.map (fun p ->
            let rgb = hexToRgb (p.Value.GetString())
            let lab = oklab rgb
            let lch = oklch lab
            p.Name, p.Value.GetString(), rgb, lab, lch)
        |> Seq.toArray
    let colorMap = colors |> Seq.map(fun (n,_,r,_,_) -> n,r) |> Map.ofSeq
    let tokenPairs =
        [ "text","background"; "mutedText","background"; "primaryAccent","background";
          "text","surface"; "border","background"; "border","surface" ]
        |> List.choose(fun (fg,bg) ->
            if tokens.TryGetProperty(fg) |> fst && tokens.TryGetProperty(bg) |> fst then
                let fn,bn = str tokens fg, str tokens bg
                Some {| foregroundToken=fg; backgroundToken=bg; foreground=fn; background=bn;
                        ratio=round 2 (contrast colorMap[fn] colorMap[bn]) |}
            else None)
    let perceptual =
        colors |> Array.map(fun (n,h,_,_,c) ->
            {| name=n; hex=h; oklch={| l=round 4 c.L; c=round 4 c.C; h=round 2 c.H |} |})
    let separations =
        [| for i in 0..colors.Length-1 do
             for j in i+1..colors.Length-1 do
               let n1,_,_,l1,_ = colors[i]
               let n2,_,_,l2,_ = colors[j]
               yield {| a=n1; b=n2; deltaOklab=round 4 (delta l1 l2) |} |]
    {| themeId=str root "id"; source=path; evidenceType="computed";
       contrastPairs=tokenPairs; colors=perceptual; pairwiseOklab=separations |}

[<EntryPoint>]
let main argv =
    if argv.Length = 0 then
        eprintfn "Usage: dotnet run --project tools/theme-measure -- <theme.json> [theme.json ...]"
        2
    else
        let options = JsonSerializerOptions(WriteIndented=true)
        argv |> Array.map evaluate |> fun x -> JsonSerializer.Serialize(x, options) |> printfn "%s"
        0
