---
id: RR-VE-COMP-2026-23F3
authors:
  - OpenAI Research Agent
confidence: Medium-High
date: 2026-07-19
llm_ingest: true
machine_readable: true
project: composition-science
purpose: |
  Investigate visual density, crowding, and perceptual separation to determine
  whether Composition Science can produce quantitative, predictive spacing laws.
references:
  - Pelli et al. (2007), Crowding and eccentricity determine reading rate
  - Pelli and Tillman (2008), The uncrowded window of object recognition
  - Whitney and Levi (2011), Visual crowding: a fundamental limit on conscious perception
  - Herzog et al. (2015), Crowding, grouping, and object recognition
  - Rosenholtz, Li, and Nakano (2007), Measuring visual clutter
status: research-draft
summary: |
  Five iterative research cycles tested whether a universal minimum spacing exists
  for visual composition. The fixed-spacing hypothesis was rejected. The strongest
  quantitative regularity is that critical spacing for peripheral identification
  grows approximately in proportion to eccentricity, but the coefficient varies by
  target, observer, meridian, grouping, and display configuration. Global grouping
  can worsen or relieve crowding, and clutter cannot be predicted from element count
  alone. The resulting model treats perceptual separation as a relationship among
  visual angle, eccentricity, grouping compatibility, feature competition, task,
  viewing time, and observer capability. The report proposes a Perceptual Separation
  Envelope and a practical design-testing protocol rather than a universal pixel rule.
version: 1.0
purposes:
  - integrate
  - verify
audiences:
  - executive
  - practitioner
  - researcher
---

# Visual Density, Crowding, and Perceptual Separation

## Purpose

This document investigates one of the earliest and most important questions in
the Composition Science project:

> **How far apart must elements be before humans can perceive and identify them as separate?**

The goal was not to collect spacing recommendations. The goal was to determine
whether the question can be converted into a predictive law that transfers
across architecture, interfaces, typography, maps, dashboards, signage, and
other visual systems.

The investigation operated in repeated cycles. Each cycle identified the largest
remaining uncertainty, generated hypotheses, searched for evidence, attempted
falsification, and revised the emerging model.

------------------------------------------------------------------------

# Executive Summary

## What Was Accomplished

Five research cycles were completed:

1. Tested whether a universal minimum spacing exists.
2. Tested whether the Bouma relationship is a sufficient spacing law.
3. Tested whether crowding is strictly local and monotonic.
4. Tested whether visual density can be predicted from element count.
5. Tested whether the findings can be converted into practical composition rules.

## Major Discoveries

- There is **no universal spacing value** in pixels, millimeters, or multiples of
  element size that guarantees perceptual separation.
- The strongest quantitative baseline is an eccentricity-scaled critical spacing:
  objects farther from fixation generally require greater separation to be
  individually identified.
- The familiar approximation of critical center-to-center spacing near one-half
  the target's eccentricity is a useful starting point, not a universal constant.
- Critical spacing is affected by meridian, radial versus tangential arrangement,
  target type, observer, similarity, grouping, temporal exposure, and task.
- Crowding is not always worsened by adding more surrounding elements. Additional
  flankers can reorganize the display and reduce interference, a result sometimes
  called **uncrowding**.
- Visual clutter is not equivalent to element count. Search difficulty depends on
  feature congestion, local variability, target-background similarity, spatial
  distribution, and fixation-relative peripheral encoding.
- Increasing object size may improve visibility without resolving crowding because
  peripheral identification can remain spacing-limited.
- The correct unit for cross-medium spacing research is **visual angle**, not pixels.
- The most defensible result is a **Perceptual Separation Envelope**, not a single
  threshold.

## Overall Confidence

**Medium-high** for the broad model.

**High** confidence that fixed universal spacing rules are invalid.

**High** confidence that eccentricity is a primary predictor of peripheral critical
spacing.

**Medium** confidence in the proposed integrated predictive model because the
interactions among grouping, clutter, task, and observer characteristics are not yet
captured by one validated equation.

## Remaining Uncertainty

The largest unresolved problem is translating controlled laboratory crowding
measurements into complex, interactive, real-world compositions. Existing results
strongly constrain the form of a model, but do not yet provide a single coefficient
set that predicts performance across text, icons, charts, architecture, and dynamic
interfaces.

------------------------------------------------------------------------

# Key Findings

- Perceptual separation is fixation-relative.
- Critical spacing generally scales with eccentricity.
- Spacing should be expressed in visual angle and center-to-center terms unless
  another definition is explicitly justified.
- Grouping changes crowding and can reverse the expected effect of density.
- Similarity is both a grouping cue and a source of feature confusion.
- Clutter is a field property, not an object count.
- Size and spacing solve different perceptual limitations.
- Design rules must distinguish detection, localization, identification, comparison,
  and action.
- Accessibility cannot be reduced to scaling everything uniformly.
- A robust composition law must specify observer, task, fixation, exposure time,
  target, flankers, and performance criterion.

------------------------------------------------------------------------

# Research Log

## Cycle 1: Does a Universal Minimum Spacing Exist?

### Objective

Determine whether there is a fixed distance, ratio, or multiple of element size that
reliably separates visual elements.

### Hypothesis

**HYP-SEP-001:** A universal minimum spacing exists and can be expressed as a fixed
multiple of target size.

### Evidence That Would Support It

- Critical separation remains stable when target size, viewing distance, and retinal
  location change.
- A common edge-to-edge or center-to-center ratio predicts recognition across object
  categories.
- Similar thresholds appear across letters, symbols, faces, and simple shapes.

### Evidence Found

Crowding research consistently shows that an isolated object may be visible while its
identity becomes unavailable when neighboring objects are too close. This impairment
is distinct from ordinary acuity loss and simple masking.

Pelli and Tillman reported that the critical spacing required to avoid peripheral
crowding is broadly similar across different object types and scales primarily with
distance from fixation rather than target size. Whitney and Levi reviewed crowding as
a fundamental limitation on object recognition throughout much of the visual field.

Relevant sources:

- Pelli, D. G., Palomares, M., and Majaj, N. J. (2004). *Crowding is unlike ordinary
  masking: distinguishing feature integration from detection*. Journal of Vision.
- Pelli, D. G., and Tillman, K. A. (2008). *The uncrowded window of object recognition*.
  Nature Neuroscience. PMID: 18828191.
- Whitney, D., and Levi, D. M. (2011). *Visual crowding: a fundamental limit on
  conscious perception and object recognition*. Trends in Cognitive Sciences.
  PMID: 21420849. PMC3070834.
- Levi, D. M. (2008). *Crowding: an essential bottleneck for object recognition*.
  Vision Research. PMC2268888.

### Evidence Against

Critical spacing grows strongly with eccentricity. An element that is easily
identified near fixation may require far more separation when viewed peripherally.
This immediately breaks any universal pixel, millimeter, or target-size-only rule.

The literature also reports variation with target and flanker type, observer,
meridian, arrangement, and threshold criterion.

### Analysis

The initial hypothesis confounded two different limits:

1. **Resolution or visibility limit:** whether the target's features can be detected.
2. **Crowding or individuation limit:** whether the target can be identified separately
   from neighboring features.

Making an object larger can resolve the first problem while leaving the second largely
unchanged. Therefore, element size and element separation cannot be collapsed into one
universal ratio.

### Conclusion

**HYP-SEP-001 was rejected.**

No universal fixed spacing exists independent of fixation and viewing geometry.

### Confidence

High.

### Next Step

Test the strongest replacement hypothesis: critical spacing is proportional to
eccentricity.

---

## Cycle 2: Is the Bouma Relationship a Sufficient Law?

### Objective

Determine whether the relationship commonly called Bouma's law can serve as the
quantitative spacing law for Composition Science.

### Hypothesis

**HYP-SEP-002:** Critical center-to-center spacing is approximately half the target's
eccentricity and is otherwise stable.

A simplified expression is:

\[
s_c \approx b e
\]

Where:

- \(s_c\) is critical center-to-center spacing in degrees of visual angle.
- \(e\) is target eccentricity from fixation in degrees.
- \(b\) is commonly approximated near 0.4 to 0.5.

### Evidence That Would Support It

- Linear scaling explains most measured critical spacing.
- The proportionality remains reasonably stable across observers and stimuli.
- Additional variables offer only minor improvement.

### Evidence Found

A large 2023 study of 50 observers found that a two-parameter Bouma model explained
approximately 82% of variance across measured log crowding distances. Adding factors
for meridian, orientation, target kind, and observer increased cross-validated
explanation to approximately 94%.

Reading research also supports eccentricity-scaled crowding. Pelli and colleagues
reported that crowding and eccentricity predict reading rate through the size of the
uncrowded visual window.

Relevant sources:

- Kurzawski, J. W. et al. (2023). *The Bouma law accounts for crowding in 50
  observers*. Vision Research. PMID: 37540179.
- Pelli, D. G. et al. (2007). *Crowding and eccentricity determine reading rate*.
  Journal of Vision. PMID: 18217835.
- Pelli, D. G., and Tillman, K. A. (2008). *The uncrowded window of object
  recognition*. Nature Neuroscience. PMID: 18828191.
- Strasburger, H., Rentschler, I., and Jüttner, M. (2011). *Peripheral vision and
  pattern recognition: a review*. Journal of Vision. PMID: 22207654.

### Evidence Against

The simple coefficient is not invariant. Reviews have identified substantial
conceptual and empirical problems with treating “half the eccentricity” as an
unequivocal constant.

Variation arises from:

- radial versus tangential arrangement
- visual-field meridian
- inward versus outward flankers
- target and flanker similarity
- target category
- observer differences
- performance criterion
- dense versus sparse configurations
- temporal conditions
- attention and saccade preparation

Coates and colleagues specifically investigated the generality of the rule for
optotypes and found that critical spacing is modulated by several factors. Strasburger
argued that common statements of the law oversimplify both Bouma's original result and
later evidence.

Relevant sources:

- Strasburger, H. (2020). *Seven myths on crowding and peripheral vision*.
  i-Perception. PMC7238452.
- Coates, D. R. et al. (2021). *The generality of the critical spacing for crowded
  optotypes*. Vision Research. PMID: 34694326.
- Levi, D. M. (2008). *Crowding: an essential bottleneck for object recognition*.
  Vision Research. PMC2268888.

### Analysis

The Bouma relationship survives falsification as a **baseline scaling law**, but fails
as a complete universal law.

It captures the dominant geometric fact: peripheral spacing requirements expand with
distance from fixation. It does not capture how strongly a particular composition will
crowd.

The distinction is critical:

- **Law form:** critical spacing increases approximately linearly with eccentricity.
- **Universal coefficient claim:** the slope is always approximately 0.5.

The first is strongly supported. The second is not.

### Conclusion

**HYP-SEP-002 was revised rather than rejected.**

### Revised Hypothesis

**HYP-SEP-002R:** Critical spacing is approximately linear in eccentricity, with a
coefficient and intercept conditioned by observer, stimulus, configuration, task,
exposure, and performance criterion.

A more defensible form is:

\[
s_c = a + b(e) \cdot e
\]

where \(a\) captures a possible foveal or near-foveal floor and \(b\) is not assumed to
be constant across conditions.

### Confidence

High for the linear baseline; medium for any generalized coefficient.

### Next Step

Investigate whether spacing effects are strictly local and monotonic.

---

## Cycle 3: Is Crowding Strictly Local and Monotonic?

### Objective

Test the intuitive assumption that closer or more numerous neighboring elements always
produce more crowding.

### Hypothesis

**HYP-SEP-003:** Crowding is determined by nearby flankers inside a fixed integration
window, and adding more flankers can only worsen recognition.

### Evidence That Would Support It

- Performance decreases monotonically as flankers are added.
- Only target-neighbor distance predicts impairment.
- Global arrangement outside the nearest-neighbor region has little effect.

### Evidence Found

Classical sparse-display studies often support local spacing effects. Recognition
generally declines when target-flanker spacing is reduced, including for objects in
complex real-world scenes.

Relevant sources:

- Ringer, R. V. et al. (2021). *Investigating visual crowding of objects in complex
  real-world scenes*. Vision Research. PMC8822316.
- Sayim, B. et al. (2013). *Grouping and crowding affect target appearance over
  different spatial scales*. PLOS ONE.
- Whitney, D., and Levi, D. M. (2011). *Visual crowding*. PMC3070834.

### Evidence Against

A substantial body of research contradicts the monotonic local-window model.

Adding flankers can sometimes **improve** target recognition. Global configuration can
reorganize the flankers into a separate group, reducing target interference. This
phenomenon is often described as uncrowding.

Dense-display experiments also show that traditional sparse paradigms can generate
incorrect conclusions. Bornet and colleagues found that adding elements does not
necessarily strengthen crowding and proposed models in which the effective crowding
window changes with display structure. Herzog and colleagues reviewed findings that
challenge strictly local, feature-pooling accounts.

Relevant sources:

- Herzog, M. H. et al. (2015). *Crowding, grouping, and object recognition: a matter
  of appearance*. Journal of Vision. PMID: 26024452.
- Doerig, A. et al. (2019). *How do we explain global aspects of crowding?*
  PLOS Computational Biology.
- Bornet, A. et al. (2021). *Shrinking Bouma's window: how to model crowding in dense
  displays*. PLOS Computational Biology.
- Levi, D. M., and Carney, T. (2009). *Crowding in peripheral vision: why bigger is
  better*. Current Biology. PMC3045113.
- Herzog, M. H. et al. (2022). *Crowding: recent advances and perspectives*.
  Journal of Vision. PMC9680590.

### Analysis

The failed hypothesis assumed that crowding is caused by simple local accumulation.
The evidence instead points to an interaction between local interference and global
organization.

Additional elements can have at least three effects:

1. Add competing features and worsen identification.
2. Strengthen a flanker group distinct from the target and relieve crowding.
3. Change the apparent structure or segmentation of the entire display.

The same increase in numerical density can therefore improve or degrade performance.

This connects directly to the Gestalt findings from the previous phase. Grouping does
not merely organize the result after crowding occurs. It appears to help determine
which features interfere in the first place.

### Conclusion

**HYP-SEP-003 was rejected.**

Crowding is not reliably monotonic in element count and cannot be predicted from the
nearest target-flanker distance alone.

### Confidence

High.

### Next Step

Determine whether visual density can be measured independently of raw element count.

---

## Cycle 4: Is Visual Density Equivalent to Element Count?

### Objective

Identify measurable predictors of clutter and visual-search difficulty.

### Hypothesis

**HYP-DEN-001:** Visual density can be estimated from the number of elements per unit
area.

### Evidence That Would Support It

- Element count strongly predicts search time across displays.
- Feature identity and arrangement add little explanatory value.
- Equal-density displays create similar difficulty.

### Evidence Found

Increasing the number of distractors often increases visual-search time, and denser
real-world scenes frequently impair detection. Element count is therefore a useful
contributor.

### Evidence Against

Equal-count scenes can differ dramatically in search performance and perceived
complexity. The difficulty depends on whether target-relevant features are common,
variable, and locally congested.

Rosenholtz, Li, and Nakano proposed multiple image-based clutter measures:

- **Feature congestion:** local variability and competition in color, orientation, and
  luminance.
- **Subband entropy:** the information required to encode image structure across
  spatial-frequency bands.
- **Edge density:** the prevalence of edges or boundaries.

These measures correlate with search performance, but none is universally sufficient.
Later research shows that target location, regional clutter, task, scene semantics,
and foveated viewing matter.

A fixation-relative model can outperform a non-foveated clutter measure because the
same clutter has different effects depending on its retinal position.

Relevant sources:

- Rosenholtz, R., Li, Y., and Nakano, L. (2007). *Measuring visual clutter*.
  Journal of Vision.
- Henderson, J. M. et al. (2009). *The influence of clutter on real-world scene
  search*. Journal of Vision.
- van den Berg, R. et al. (2009). *A crowding model of visual clutter*. Journal of
  Vision.
- Asher, M. F. et al. (2013). *Regional effects of clutter on human target detection
  performance*. Journal of Vision.
- Nuthmann, A. (2017). *Fixation durations in scene viewing: modeling the effects of
  local scene content*. PMC5390002.
- Deza, A., and Eckstein, M. P. (2016). *Can peripheral representations improve
  clutter metrics on complex scenes?* arXiv:1608.04042.
- Rosenholtz, R. (2023). *Does your old clutter measure spark joy?* Journal of Vision.

### Analysis

Visual density is not one variable. It contains at least four distinct concepts:

1. **Numerical density:** number of elements per area.
2. **Feature density:** number and variability of colors, orientations, textures,
   edges, and spatial frequencies.
3. **Semantic density:** amount of distinct meaning or decisions represented.
4. **Action density:** number of plausible actions or response targets.

A scene can have high numerical density but low feature congestion if the elements form
regular, predictable groups. A scene can have low numerical density but high decision
density if every element demands a distinct interpretation.

### Conclusion

**HYP-DEN-001 was rejected.**

Element count contributes to clutter but is not a sufficient measure.

### Confidence

High.

### Next Step

Translate the evidence into a predictive composition model and determine its practical
limits.

---

## Cycle 5: Can the Findings Become a Predictive Design Rule?

### Objective

Develop a model that can guide spacing decisions across media without pretending that
one fixed value applies universally.

### Hypothesis

**HYP-MOD-001:** Perceptual separation can be predicted from a small set of measurable
variables.

### Evidence That Would Support It

- A common equation form accommodates the strongest findings.
- Variables can be measured or approximated in real designs.
- The model produces falsifiable predictions.

### Evidence Found

The evidence consistently identifies the following variables:

- eccentricity from fixation
- angular spacing
- target size and feature visibility
- target-flanker similarity
- radial or tangential arrangement
- grouping and global configuration
- local feature congestion
- exposure time
- task type
- observer and visual condition

The Bouma relationship provides the geometric baseline. Clutter and grouping research
provide modifiers. Studies of older adults, glaucoma, amblyopia, dyslexia, and macular
degeneration show that observer capability cannot be ignored.

Relevant sources:

- Liu, R. et al. (2017). *Age-related changes in crowding and reading speed*.
  Scientific Reports. PMC5557829.
- Shamsi, F. et al. (2021). *Functional field of view determined by crowding, aging,
  or glaucoma*. Translational Vision Science & Technology. PMC8684310.
- Wallace, J. M. et al. (2017). *Object crowding in age-related macular degeneration*.
  Journal of Vision. PMC5283087.
- Chung, S. T. L. (2014). *Size or spacing: which limits letter recognition in people
  with age-related macular degeneration?* Vision Research. PMID: 25014400.
- Martelli, M. et al. (2009). *Crowding, reading, and developmental dyslexia*.
  Journal of Vision. PMID: 19757923.
- Tanriverdi, D. et al. (2024). *Assessing visual crowding in participants with
  preperimetric glaucoma*. Translational Vision Science & Technology. PMC11379081.

### Evidence Against

No integrated model found in this review predicts every crowding and clutter effect
across simple laboratory arrays and complex natural scenes.

Global uncrowding, task-dependent attention, semantic expectations, and eye movements
create nonlinearities that are difficult to reduce to a single closed-form equation.

Fixation is also dynamic. In normal viewing, people move their eyes, changing the
eccentricity of every element. A static model must therefore either assume a fixation
or predict gaze behavior.

### Analysis

A useful model can still be developed if it is treated as a **risk envelope** rather
than an exact universal threshold.

The model should predict the probability of successful individuation under specified
conditions, not declare elements universally separate or crowded.

### Conclusion

**HYP-MOD-001 remains provisionally supported.**

A predictive model is plausible, but it must be probabilistic, fixation-relative, and
conditioned on task and observer.

### Confidence

Medium.

### Next Step

Validate the proposed Perceptual Separation Envelope on controlled interface,
typographic, and architectural-signage examples.

------------------------------------------------------------------------

# Confirmed Findings

Only findings with strong, convergent support are included here.

## CF-001: Peripheral Identification Is Spacing-Limited

An object may be detectable yet not individually identifiable when neighboring
features are too close.

**Confidence:** High.

## CF-002: Critical Spacing Scales With Eccentricity

Peripheral critical spacing generally increases approximately linearly with distance
from fixation.

**Confidence:** High.

## CF-003: A Fixed Bouma Coefficient Is Not Universal

The slope varies with observer, meridian, configuration, target, and method.

**Confidence:** High.

## CF-004: Target Size and Critical Spacing Are Partly Dissociable

Making an object larger may improve feature visibility without proportionally reducing
the spacing needed to avoid crowding.

**Confidence:** High.

## CF-005: Global Grouping Modulates Local Crowding

The arrangement of surrounding elements can worsen or relieve target interference.

**Confidence:** High.

## CF-006: Density Is Not Equivalent to Element Count

Feature congestion, organization, similarity, and fixation-relative position matter.

**Confidence:** High.

## CF-007: Visual Angle Is the Correct Cross-Medium Unit

Pixel values do not generalize across display size and viewing distance.

**Confidence:** High.

## CF-008: The Task Defines the Required Separation

Detection, localization, identification, comparison, and action impose different
requirements.

**Confidence:** High.

## CF-009: Observer Capability Is a Model Variable

Age, visual-field loss, amblyopia, glaucoma, macular degeneration, reading development,
and individual differences can change the effective separation envelope.

**Confidence:** High.

------------------------------------------------------------------------

# Rejected Hypotheses

## RH-001: Universal Pixel Spacing

### Rejected Claim

A fixed number of pixels can guarantee perceptual separation.

### Why It Failed

Pixels have no stable perceptual meaning without display density, physical size,
viewing distance, fixation, and observer information.

### Confidence

High.

---

## RH-002: Universal Element-Size Ratio

### Rejected Claim

Spacing need only be a fixed multiple of target size.

### Why It Failed

Crowding is often more strongly tied to eccentricity than target size.

### Confidence

High.

---

## RH-003: More Elements Always Produce More Crowding

### Rejected Claim

Crowding increases monotonically with the number of nearby objects.

### Why It Failed

Global grouping and uncrowding demonstrate that additional flankers can improve target
recognition.

### Confidence

High.

---

## RH-004: Nearest-Neighbor Distance Fully Predicts Crowding

### Rejected Claim

Only the closest flanker matters.

### Why It Failed

Global configuration, grouping, remote elements, and dense-display structure can alter
performance.

### Confidence

High.

---

## RH-005: Clutter Equals Element Count

### Rejected Claim

Visual density is the number of objects per unit area.

### Why It Failed

Equal-count scenes differ in feature congestion, semantic organization, target
similarity, and search difficulty.

### Confidence

High.

---

## RH-006: Larger Objects Automatically Solve Crowding

### Rejected Claim

Increasing target size restores recognition in clutter.

### Why It Failed

Size can resolve acuity limits while spacing remains below the critical threshold.

### Confidence

High.

------------------------------------------------------------------------

# Emerging Patterns

## EP-001: Relational Rather Than Absolute Laws

The strongest findings are ratios or functions relating environmental demands to
human capability. This repeats earlier discoveries in human scale, wayfinding, and
Gestalt grouping.

Why it matters:

Composition Science should search for conditional relationships rather than ideal
numbers.

---

## EP-002: Externalized Cognition Has a Perceptual Bottleneck

Externalizing information only helps when the external structure can itself be
individuated. Adding more labels, indicators, borders, or controls can exceed the
observer's useful perceptual resolution.

Why it matters:

“Make information visible” is incomplete. Visible information must also remain
separable and interpretable.

---

## EP-003: Grouping and Separation Are Dual Operations

Grouping determines which elements become one unit. Crowding determines when distinct
features cannot be individually recovered.

Why it matters:

A composition must intentionally compress some elements into groups while preserving
separation among elements whose differences matter.

---

## EP-004: Every Compression Has an Error Cost

Grouping reduces cognitive load but may hide within-group differences. Crowding is an
extreme form of involuntary compression in which information is pooled or substituted
incorrectly.

Why it matters:

The design objective is not maximum grouping. It is **task-compatible compression**.

---

## EP-005: Fixation Creates a Moving Resolution Field

The visual field is not uniformly detailed. Each eye movement shifts the high-resolution
center and changes which objects are vulnerable to crowding.

Why it matters:

Composition guides both attention and the sequence of fixations. Layout quality cannot
be assessed solely as a static image.

---

## EP-006: Clutter Is Target-Relative

A background is not inherently cluttered. It is cluttered relative to what the observer
must find, identify, compare, or act upon.

Why it matters:

Generic “reduce clutter” advice should be replaced by target- and task-specific analysis.

---

## EP-007: Biological Limits Interact With Learned Structure

Peripheral integration is biologically constrained, while expertise, reading
conventions, familiarity, and expectations influence where people look and how they
group information.

Why it matters:

Universal laws and learned conventions must be modeled separately but allowed to
interact.

------------------------------------------------------------------------

# Proposed Models

## MODEL-COMP-001: Perceptual Separation Envelope

### Purpose

Estimate whether a target can be individually identified under specified viewing
conditions.

### Baseline

\[
s_{base} = a + b e
\]

Where:

- \(s_{base}\) = baseline critical center-to-center spacing in degrees.
- \(e\) = target eccentricity in degrees.
- \(a\) = foveal or near-foveal spacing floor.
- \(b\) = eccentricity-scaling coefficient.

### Modifiers

\[
s_{required} =
s_{base}
\cdot M_{similarity}
\cdot M_{configuration}
\cdot M_{clutter}
\cdot M_{task}
\cdot M_{time}
\cdot M_{observer}
\]

The multiplicative form is a research proposal, not yet a validated equation.

#### \(M_{similarity}\)

Higher when target and flankers share confusable features.

#### \(M_{configuration}\)

Captures radial/tangential arrangement, inward/outward asymmetry, regularity, and
global grouping. This modifier may be less than 1 when global organization produces
uncrowding.

#### \(M_{clutter}\)

Captures local feature congestion and target-background competition.

#### \(M_{task}\)

Higher for identification or comparison than for simple detection.

#### \(M_{time}\)

Higher under brief exposure, motion, divided attention, or rapid interaction.

#### \(M_{observer}\)

Captures individual variation and visual conditions.

### Predicted Outcome

\[
P(\text{correct individuation}) =
\sigma\left(
k \left[
\frac{s_{actual}}{s_{required}} - 1
\right]
\right)
\]

Where \(\sigma\) is a logistic function and \(k\) determines transition steepness.

### Assumptions

- Fixation is known or approximated.
- Spacing is measured center-to-center in visual angle.
- The target's isolated visibility is above threshold.
- Task and criterion are specified.
- The model predicts probability, not certainty.

### Confidence

Medium.

---

## MODEL-COMP-002: Separation Versus Grouping Matrix

| Relationship | Desired perceptual result | Design treatment |
|---|---|---|
| Same unit, same role | Strong grouping | close spacing, similarity, common region |
| Same unit, distinct roles | Group with internal differentiation | common region plus clear substructure |
| Different units, comparable role | Separation with shared category | larger spacing plus controlled similarity |
| Different units, different role | Strong separation | spacing, boundary, feature differentiation |
| Distinct items requiring comparison | Preserve individuation | adequate spacing, alignment, low clutter |
| Repeated texture or background | Allow compression | regularity and low semantic demand |

### Prediction

Design errors occur when perceptual treatment implies a relationship different from
the task relationship.

### Confidence

Medium-high.

---

## MODEL-COMP-003: Four-Density Taxonomy

### Numerical Density

Objects per unit area or solid angle.

### Feature Density

Local variation in color, orientation, luminance, spatial frequency, and boundaries.

### Semantic Density

Independent meanings, states, categories, or facts per region.

### Action Density

Independent possible actions or decisions per region.

### Prediction

Performance will be more accurately predicted by a weighted density vector than by
element count alone.

\[
D = [D_n, D_f, D_s, D_a]
\]

### Confidence

Medium-high.

---

## MODEL-COMP-004: Composition as Controlled Compression

### Theory

A successful composition compresses raw elements into perceptual units while
preserving every distinction required by the task.

### Objective Function

\[
Q =
\text{Cognitive Efficiency}
-
\lambda \cdot \text{Task-Relevant Information Loss}
\]

Where \(\lambda\) increases when errors are costly.

### Implications

- Dense art may tolerate ambiguity that a medical dashboard cannot.
- Repetition can compress background structure.
- Borders and similarity should reflect semantic relationships.
- Critical exceptions must resist group compression.
- High-stakes interfaces require more separation and redundancy.

### Confidence

Medium-high.

------------------------------------------------------------------------

# Candidate Laws

## LAW-COMP-025: Eccentricity-Scaled Separation Law

### Hypothesis

The spacing required for individual identification generally increases with retinal
eccentricity.

### Prediction

At equal physical spacing, identification accuracy will decline as the target moves
farther from fixation.

### Supporting Evidence

Bouma-type scaling across letters, optotypes, objects, reading, and large observer
samples.

### Counter Evidence

The slope is not universal; foveal crowding and global configuration require
extensions.

### Confidence

High.

---

## LAW-COMP-026: Visual-Angle Invariance Law

### Hypothesis

Spacing predictions transfer across media only when physical dimensions are converted
to visual angle.

### Prediction

Two displays with equal pixel spacing but different viewing geometry will produce
different separation performance, while comparable angular spacing will transfer more
reliably.

### Supporting Evidence

Crowding is defined in retinal and angular coordinates.

### Counter Evidence

Eye movements, accommodation, and device interaction can alter effective viewing
conditions.

### Confidence

High.

---

## LAW-COMP-027: Configuration Modulation Law

### Hypothesis

Critical spacing is modified by the organization of surrounding elements, not only
their nearest distance.

### Prediction

Displays with identical target-neighbor spacing can produce different recognition
accuracy when the global configuration changes.

### Supporting Evidence

Grouping, uncrowding, dense-display, and global configuration studies.

### Counter Evidence

The magnitude and direction of modulation are not yet predictable in every display.

### Confidence

High.

---

## LAW-COMP-028: Task-Conditional Separation Law

### Hypothesis

Required separation depends on what the observer must do with the target.

### Prediction

Spacing sufficient for detection will be insufficient for identification,
discrimination, comparison, or precise action.

### Supporting Evidence

Dissociations between visibility, acuity, crowding, and object recognition.

### Counter Evidence

Specific task multipliers remain unknown.

### Confidence

High.

---

## LAW-COMP-029: Feature-Competition Law

### Hypothesis

Crowding and search difficulty increase when nearby elements compete within target-
relevant feature dimensions.

### Prediction

Target-flanker similarity in relevant features will require greater spacing or stronger
structural separation.

### Supporting Evidence

Feature congestion, target-distractor similarity, and crowding studies.

### Counter Evidence

Similarity can also strengthen grouping that separates flankers from the target.

### Confidence

Medium-high.

---

## LAW-COMP-030: Structured-Density Law

### Hypothesis

The perceptual cost of density depends on organization, not count alone.

### Prediction

A regular, grouped high-count display can outperform a lower-count but irregular,
feature-congested display.

### Supporting Evidence

Clutter metrics, grouping, and uncrowding evidence.

### Counter Evidence

Strong regularity may hide exceptional items or produce texture-level compression.

### Confidence

High.

---

## LAW-COMP-031: Separation Accessibility Law

### Hypothesis

Spacing that succeeds for a median observer cannot be assumed to succeed for all
observers.

### Prediction

Older users, users with field loss, and users with crowding-sensitive visual
conditions will show reduced identification accuracy under the same layout.

### Supporting Evidence

Research involving aging, glaucoma, macular degeneration, amblyopia, dyslexia, and
individual variability.

### Counter Evidence

Normal aging effects vary by task and exposure time; age alone is not a reliable
individual predictor.

### Confidence

High.

---

## LAW-COMP-032: Fixation-Sequence Law

### Hypothesis

Composition quality depends partly on whether the layout supports an efficient sequence
of fixations that repeatedly brings task-critical elements into an uncrowded central
window.

### Prediction

Layouts requiring peripheral identification of crowded targets will produce more eye
movements, longer search, or more errors.

### Supporting Evidence

Visual-span, reading-rate, peripheral-vision, and search literature.

### Counter Evidence

Expertise and predictive context can partially compensate.

### Confidence

Medium-high.

------------------------------------------------------------------------

# Observations

## OBS-025

### Observation

The same object spacing can be adequate at fixation and inadequate in the periphery.

### Interpretation

Spacing is a property of the observer-layout relationship, not the layout alone.

### Confidence

High.

---

## OBS-026

### Observation

Increasing size does not reliably eliminate crowding.

### Interpretation

Visibility and individuation are distinct requirements.

### Confidence

High.

---

## OBS-027

### Observation

Additional flankers sometimes reduce crowding.

### Interpretation

Global grouping can restructure which features interfere.

### Confidence

High.

---

## OBS-028

### Observation

High element count can remain manageable when the display is regular and strongly
organized.

### Interpretation

Perceived units and feature congestion matter more than raw count.

### Confidence

High.

---

## OBS-029

### Observation

Clutter effects depend on target location and current fixation.

### Interpretation

A clutter map should be foveated and task-relative.

### Confidence

Medium-high.

---

## OBS-030

### Observation

Published “optimal spacing” values frequently omit fixation, visual angle, task, and
criterion.

### Interpretation

Many practical spacing recommendations cannot be treated as perceptual laws.

### Confidence

High.

------------------------------------------------------------------------

# Evidence

## EVD-025

### Citation

Kurzawski et al. (2023), *The Bouma law accounts for crowding in 50 observers*.

### Summary

Linear eccentricity scaling explained most variance, while additional observer,
meridian, orientation, and target factors improved prediction.

### Supports

- LAW-COMP-025
- MODEL-COMP-001

### Challenges

- Any universal fixed coefficient.

---

## EVD-026

### Citation

Pelli and Tillman (2008), *The uncrowded window of object recognition*.

### Summary

Critical spacing is broadly object-independent and proportional to eccentricity,
defining an uncrowded recognition window.

### Supports

- LAW-COMP-025
- LAW-COMP-032

### Challenges

- Element-size-only spacing rules.

---

## EVD-027

### Citation

Bornet et al. (2021), *Shrinking Bouma's window*.

### Summary

Dense displays violate assumptions derived from sparse flanker paradigms; additional
elements may not increase crowding.

### Supports

- LAW-COMP-027
- LAW-COMP-030

### Challenges

- Strictly local monotonic crowding models.

---

## EVD-028

### Citation

Herzog et al. (2015), *Crowding, grouping, and object recognition*.

### Summary

Global grouping and configuration are central to crowding and object recognition.

### Supports

- LAW-COMP-027
- MODEL-COMP-004

### Challenges

- Pure local pooling explanations.

---

## EVD-029

### Citation

Rosenholtz, Li, and Nakano (2007), *Measuring visual clutter*.

### Summary

Image-based feature congestion and entropy measures predict aspects of clutter and
search difficulty better than element count alone.

### Supports

- LAW-COMP-029
- LAW-COMP-030
- MODEL-COMP-003

### Challenges

- Numerical-density-only models.

---

## EVD-030

### Citation

Liu et al. (2017), *Age-related changes in crowding and reading speed*.

### Summary

Older adults showed an enlarged crowding zone and reduced visual span in the reported
task.

### Supports

- LAW-COMP-031

### Challenges

- One-layout-fits-all assumptions.

------------------------------------------------------------------------

# Open Questions

Ranked by importance.

## 1. How Can Laboratory Critical Spacing Be Translated to Real Interfaces?

**Importance:** Very high  
**Uncertainty:** High

Controlled studies usually use brief fixation and simplified targets. Real interfaces
allow eye movements, prediction, scrolling, and repeated exposure.

## 2. Can Grouping Modifiers Be Quantified Reliably?

**Importance:** Very high  
**Uncertainty:** High

Global configuration can cause crowding or uncrowding, but no simple general-purpose
coefficient was identified.

## 3. What Fixation Should a Design Model Assume?

**Importance:** High  
**Uncertainty:** High

Possible approaches include predicted fixation, observed eye tracking, task-defined
fixation, or worst-case peripheral analysis.

## 4. How Should Semantic and Action Density Be Measured?

**Importance:** High  
**Uncertainty:** High

Feature-density metrics exist, but semantic and action density need operational
definitions.

## 5. What Performance Criterion Defines “Separate”?

**Importance:** High  
**Uncertainty:** Medium

Possible criteria include 75%, 90%, or 95% correct identification, acceptable search
time, or error-cost thresholds.

## 6. How Do Motion and Animation Change Critical Separation?

**Importance:** Medium-high  
**Uncertainty:** High

Common fate may strengthen grouping while movement also consumes attention and changes
retinal position.

## 7. How Do Color, Contrast, and Similarity Interact With Spacing?

**Importance:** Medium-high  
**Uncertainty:** Medium-high

Feature differentiation may reduce confusion, but similarity can either worsen
competition or improve group segregation.

## 8. What Safety Factors Should Be Used for Inclusive Design?

**Importance:** High  
**Uncertainty:** Medium-high

Median thresholds are inadequate for high-stakes or accessibility-sensitive systems.

------------------------------------------------------------------------

# Recommendations

## Priority 1: Build a Visual-Angle Spacing Calculator

### Description

Create a tool that converts display dimensions, viewing distance, fixation, and target
position into angular size, eccentricity, and spacing.

### Expected Value

Very high.

### Effort

Low to medium.

### Why

It prevents future research from reverting to meaningless pixel-only rules.

---

## Priority 2: Create a Controlled Interface Test Set

### Description

Construct small interface examples that independently vary:

- eccentricity
- spacing
- similarity
- grouping
- density
- target type
- exposure time

### Expected Value

Very high.

### Effort

Medium.

### Why

It begins translating laboratory findings into composition-specific evidence without
requiring a large human experiment initially. Published parameter ranges can be used
to generate candidate boundaries.

---

## Priority 3: Develop a Fixation-Aware Composition Analyzer

### Description

Estimate likely fixation points and overlay eccentricity-scaled crowding risk zones.

### Expected Value

High.

### Effort

High.

### Why

This could become a practical implementation of the Perceptual Separation Envelope.

---

## Priority 4: Separate Spacing Rules by Task

### Description

Create distinct profiles for:

- detection
- localization
- identification
- reading
- comparison
- target selection
- error-critical action

### Expected Value

High.

### Effort

Medium.

### Why

Most design advice fails by treating all visual tasks as equivalent.

---

## Priority 5: Create an Accessibility Safety-Factor Framework

### Description

Define conservative modifiers for older adults and users with visual impairments.

### Expected Value

High.

### Effort

Medium to high.

### Why

Observer variability is too large to treat median performance as universal.

---

## Priority 6: Investigate Dynamic Composition

### Description

Study how eye movements, animation, progressive disclosure, scrolling, and transitions
change crowding and separation.

### Expected Value

Medium-high.

### Effort

High.

### Why

Static composition models cannot fully explain interactive media.

------------------------------------------------------------------------

# Practical Interim Rules

These are not universal laws. They are defensible interim practices derived from the
evidence.

1. Express spacing in visual angle whenever results must transfer across viewing
   conditions.
2. Evaluate peripheral elements at their expected eccentricity from likely fixation.
3. Do not assume that increasing target size solves identification in clutter.
4. Use spacing and feature differentiation together when individual identity matters.
5. Treat regular grouping as a way to reduce clutter, but isolate exceptions that must
   remain visible.
6. Avoid placing visually similar task-critical items close together in the periphery.
7. Use greater safety margins for brief exposure, divided attention, motion, aging,
   and visual impairment.
8. Measure success using the required task, not subjective neatness.
9. Test the whole configuration. Nearest-neighbor spacing alone is insufficient.
10. Preserve a clear path of fixations so critical elements can enter central vision.

------------------------------------------------------------------------

# Next Actions

- Build the visual-angle calculator.
- Define an experiment schema for spacing and crowding evidence.
- Create the first controlled interface stimulus set.
- Map all existing Composition Science laws to the proposed processing chain.
- Begin a literature review of visual search, saccade planning, and fixation prediction.
- Establish a standard evidence record containing fixation, eccentricity, angular size,
  spacing definition, task, exposure, criterion, and observer population.

------------------------------------------------------------------------

# Bibliography

## Academic

- Astle, A. T. et al. (2014). *The effect of aging on crowded letter recognition in
  the peripheral visual field*. Investigative Ophthalmology & Visual Science.
  PMID: 24985476. PMC4132554.
- Bornet, A. et al. (2021). *Shrinking Bouma's window: how to model crowding in dense
  displays*. PLOS Computational Biology. DOI: 10.1371/journal.pcbi.1009187.
- Coates, D. R. et al. (2021). *The generality of the critical spacing for crowded
  optotypes*. Vision Research. PMID: 34694326.
- Chung, S. T. L. (2014). *Size or spacing: which limits letter recognition in people
  with age-related macular degeneration?* Vision Research. PMID: 25014400.
- Doerig, A. et al. (2019). *How do we explain global aspects of crowding?*
  PLOS Computational Biology. DOI: 10.1371/journal.pcbi.1006580.
- Henderson, J. M. et al. (2009). *The influence of clutter on real-world scene
  search*. Journal of Vision.
- Herzog, M. H. et al. (2015). *Crowding, grouping, and object recognition: a matter
  of appearance*. Journal of Vision. PMID: 26024452.
- Herzog, M. H. et al. (2022). *Crowding: recent advances and perspectives*.
  Journal of Vision. PMC9680590.
- Kurzawski, J. W. et al. (2023). *The Bouma law accounts for crowding in 50
  observers*. Vision Research. PMID: 37540179.
- Levi, D. M. (2008). *Crowding: an essential bottleneck for object recognition*.
  Vision Research. PMC2268888.
- Levi, D. M., and Carney, T. (2009). *Crowding in peripheral vision: why bigger is
  better*. Current Biology. PMC3045113.
- Liu, R. et al. (2017). *Age-related changes in crowding and reading speed*.
  Scientific Reports. PMC5557829.
- Martelli, M. et al. (2009). *Crowding, reading, and developmental dyslexia*.
  Journal of Vision. PMID: 19757923.
- Pelli, D. G. et al. (2007). *Crowding and eccentricity determine reading rate*.
  Journal of Vision. PMID: 18217835.
- Pelli, D. G., and Tillman, K. A. (2008). *The uncrowded window of object recognition*.
  Nature Neuroscience. PMID: 18828191.
- Ringer, R. V. et al. (2021). *Investigating visual crowding of objects in complex
  real-world scenes*. Vision Research. PMC8822316.
- Rosenholtz, R., Li, Y., and Nakano, L. (2007). *Measuring visual clutter*.
  Journal of Vision.
- Shamsi, F. et al. (2021). *Functional field of view determined by crowding, aging,
  or glaucoma*. Translational Vision Science & Technology. PMC8684310.
- Strasburger, H. (2020). *Seven myths on crowding and peripheral vision*.
  i-Perception. PMC7238452.
- Strasburger, H., Rentschler, I., and Jüttner, M. (2011). *Peripheral vision and
  pattern recognition: a review*. Journal of Vision. PMID: 22207654.
- van den Berg, R. et al. (2009). *A crowding model of visual clutter*.
  Journal of Vision.
- Wallace, J. M. et al. (2017). *Object crowding in age-related macular degeneration*.
  Journal of Vision. PMC5283087.
- Whitney, D., and Levi, D. M. (2011). *Visual crowding: a fundamental limit on
  conscious perception and object recognition*. Trends in Cognitive Sciences.
  PMC3070834.

## Books

- No book was treated as load-bearing evidence in this phase.

## Industry

- No industry source was treated as primary evidence in this phase.

## Patents

- No relevant patent evidence was required for this phase.

## Standards

- Existing interface spacing and target-size standards were not used as evidence for
  perceptual separation because they generally address operability or accessibility
  outcomes rather than the underlying crowding mechanism. They should be compared in
  a later applied-design phase.

## Historical

- Bouma, H. (1970). *Interaction effects in parafoveal letter recognition*.
  Nature. This study is historically important, but later work shows that simplified
  statements of “Bouma's law” exceed what a single coefficient can support.

## Other

- Deza, A., and Eckstein, M. P. (2016). *Can peripheral representations improve
  clutter metrics on complex scenes?* arXiv:1608.04042.
- Rosenholtz, R. (2023). *Does your old clutter measure spark joy?*
  Journal of Vision conference material.

------------------------------------------------------------------------

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 1.0 | 2026-07-19 | OpenAI Research Agent | Initial autonomous research report on visual density, crowding, and perceptual separation |

------------------------------------------------------------------------

# Agent Instructions

When creating or modifying this document:

1. Separate observation from interpretation.
2. Never strengthen a conclusion beyond the available evidence.
3. Preserve contradictory findings.
4. Prefer measurable variables over subjective descriptions.
5. Reference candidate laws and genome nodes whenever possible.
6. Use stable IDs for observations, evidence, laws, experiments, metrics, and case studies.
7. Record assumptions explicitly.
8. Record confidence explicitly.
9. Keep the YAML header valid.
10. Do not delete revision history; append to it.

This template is the standard for all Composition Science project documents.
