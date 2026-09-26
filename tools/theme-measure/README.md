# Theme Measure

Dependency-free F# measurement utility for Visual Engineering theme specimens.

It computes:
- WCAG 2.x relative-luminance contrast for declared semantic pairs;
- OKLCH coordinates for every palette color;
- pairwise Euclidean OKLab distances for comparative experiments.

It intentionally does **not** assign emotional meaning and does not claim CVD accessibility. Those require separate evidence layers.

## Run

```bash
dotnet run --project tools/theme-measure -- content/themes/specimens/THM-0001.json
```

Pass multiple specimen files to produce one JSON array. Generated output is evidence and should be stored separately from the authored hypothesis.

## Design constraints

- no external NuGet packages;
- deterministic math;
- source hex remains authoritative;
- computed, simulated, observed, and studied evidence remain distinct;
- changing formulas requires a measurement-version change and re-evaluation of derived evidence.
