open System
open VisualEngineering.ThemeMeasure
open VisualEngineering.ThemeMeasure.ColorMath

let near expected actual tolerance name =
    if abs(expected-actual) > tolerance then failwithf "%s expected %f, got %f" name expected actual

[<EntryPoint>]
let main _ =
    let black = hexToRgb "#000000"
    let white = hexToRgb "#FFFFFF"
    near 21.0 (contrast black white) 0.0001 "black/white contrast"
    near 1.0 (contrast white white) 0.0001 "same-color contrast"
    near 0.0 (oklab black).L 0.000001 "OKLab black lightness"
    near 1.0 (oklab white).L 0.0001 "OKLab white lightness"
    near 0.0 (delta (oklab white) (oklab white)) 0.000001 "same-color OKLab distance"
    let red = oklch (oklab (hexToRgb "#FF0000"))
    if red.C <= 0.0 then failwith "red chroma should be positive"
    printfn "theme-measure production-math invariant tests passed"
    0
