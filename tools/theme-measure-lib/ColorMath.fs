namespace VisualEngineering.ThemeMeasure

open System

type Rgb = { R: float; G: float; B: float }
type Oklab = { L: float; A: float; B: float }
type Oklch = { L: float; C: float; H: float }

module ColorMath =
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

    let delta (a: Oklab) (b: Oklab) = sqrt ((a.L-b.L)**2.0 + (a.A-b.A)**2.0 + (a.B-b.B)**2.0)
