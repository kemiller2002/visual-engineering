open System

let linear c = if c <= 0.04045 then c / 12.92 else Math.Pow((c + 0.055) / 1.055, 2.4)
let lum (r,g,b) = 0.2126*linear r + 0.7152*linear g + 0.0722*linear b
let contrast a b =
    let x,y=lum a,lum b
    (max x y + 0.05)/(min x y + 0.05)
let near expected actual tolerance name =
    if abs(expected-actual) > tolerance then failwithf "%s expected %f, got %f" name expected actual

[<EntryPoint>]
let main _ =
    near 21.0 (contrast (0.,0.,0.) (1.,1.,1.)) 0.0001 "black/white contrast"
    near 1.0 (contrast (1.,1.,1.) (1.,1.,1.)) 0.0001 "same-color contrast"
    near 0.0 (linear 0.0) 0.000001 "linear black"
    near 1.0 (linear 1.0) 0.000001 "linear white"
    printfn "theme-measure invariant tests passed"
    0
