open System
open System.IO
open System.Text.Json
open VisualEngineering.ThemeMeasure
open VisualEngineering.ThemeMeasure.ColorMath

let round (n: int) (x: float) = Math.Round(x, n)
let prop (e: JsonElement) (name: string) = e.GetProperty(name)
let str (e: JsonElement) (name: string) = (prop e name).GetString()

let evaluate (path: string) =
    use doc = JsonDocument.Parse(File.ReadAllText path)
    let root = doc.RootElement
    let palette = prop root "palette"
    let tokens = prop root "semanticTokens"
    let colors =
        palette.EnumerateObject()
        |> Seq.map (fun p ->
            let rgb = hexToRgb (p.Value.GetString())
            let lab = oklab rgb
            p.Name, p.Value.GetString(), rgb, lab, oklch lab)
        |> Seq.toArray
    let colorMap = colors |> Seq.map(fun (n,_,r,_,_) -> n,r) |> Map.ofSeq
    let tokenPairs =
        [ "text","background"; "mutedText","background"; "primaryAccent","background";
          "text","surface"; "border","background"; "border","surface" ]
        |> List.choose(fun (fg,bg) ->
            if tokens.TryGetProperty(fg) |> fst && tokens.TryGetProperty(bg) |> fst then
                let fn: string = str tokens fg
                let bn: string = str tokens bg
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
    {| measurementVersion="1.0.0"; themeId=str root "id"; source=path; evidenceType="computed";
       contrastPairs=tokenPairs; colors=perceptual; pairwiseOklab=separations |}

[<EntryPoint>]
let main argv =
    if argv.Length = 0 then eprintfn "Usage: theme-measure <theme.json> [...]"; 2
    else
        let options = JsonSerializerOptions(WriteIndented=true)
        argv |> Array.map evaluate |> fun x -> JsonSerializer.Serialize(x, options) |> printfn "%s"
        0
