# Theme Accessibility and Data-Color Evaluation Protocol

Status: active
Related: #13, #14

## Principle

A theme is not accessible because its body text passes a contrast check. Evaluate the actual semantic relationships used by the interface.

## Required evaluations

### Text
Measure every foreground/background pair actually used for:
- body text
- muted/supporting text
- links
- button labels
- input text and placeholder text when meaningful
- status text
- text over accent/status fills

Record ratio and criterion. Never infer a pass from palette membership.

### Non-text and interaction
Evaluate boundaries and indicators required to perceive or operate the UI:
- input boundaries
- focus indicators
- selected state
- toggles/checks
- chart marks needed for interpretation
- status icons
- adjacent interactive states

A decorative border may be subtle. A boundary carrying necessary state must meet the applicable functional requirement.

### Color independence
For success, warning, error, info, selection, and chart categories, verify that color is not the only information carrier. Use combinations of:
- icon/shape
- text label
- pattern/texture where appropriate
- position
- stroke/dash treatment
- direct data label

### Color-vision-deficiency robustness
Evaluate at minimum simulated:
- protan-type red deficiency
- deutan-type green deficiency
- tritan-type blue/yellow deficiency
- achromatic/grayscale condition as an information-redundancy stress test

Simulation is a screening tool, not proof of lived accessibility.

For each semantic pair/category set record:
- distinguishable under simulation: yes/no/uncertain
- redundant non-color encoding: yes/no
- lightness separation
- failure notes

### Status palette
Every theme intended for applications SHOULD define or inherit:
- success
- warning
- error
- info
- neutral

Status colors MUST be evaluated against their surfaces and against one another. Do not assume red/green is sufficient.

### Data-series palette
For data visualization contexts record:
- maximum intended simultaneous series
- ordered vs categorical use
- pairwise perceptual separation
- CVD simulation results
- light/dark surface behavior
- direct-label support
- print/grayscale behavior

Prefer fewer reliably distinguishable series over nominally different colors that collapse perceptually.

## Evidence model

Record each result as:
- computed: deterministic calculation from defined colors
- simulated: algorithmic visual simulation
- observed: structured human review
- studied: controlled user evidence

Do not upgrade simulated evidence to observed/studied.

## Acceptance gates

A theme may be `evaluated` when its declared usage surfaces have completed computed checks and rendered review.

A theme may be `accepted` only for an explicit context. Example: accepted for marketing pages does not imply accepted for dense analytical dashboards.

## Controlled experiment requirements

When comparing variants:
1. state the intended independent variable;
2. measure OKLCH/lightness/chroma/hue differences;
3. report unintended differences as confounds;
4. use identical reference content/layout;
5. keep semantic role assignment stable unless role assignment is itself the experiment;
6. report each outcome independently;
7. preserve negative/null results.

This protocol intentionally favors falsifiable evidence over theme-ranking folklore.
