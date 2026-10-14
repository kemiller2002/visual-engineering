---
id: EV-VE-ATL-2026-D4DA
title: "Project Atlas Color Evidence Registry v0.1"
project: project-atlas
version: 0.1
status: Working Draft
date: 2026-07-18
authors:
  - Kevin Miller
  - ChatGPT
purpose: |
  Establish the first source-grounded evidence registry for the Project
  Atlas Perceptual Color Genome. This document separates measured findings
  from interpretations and begins translating color science into bounded,
  testable composition hypotheses.
tags:
  - color
  - perception
  - attention
  - accessibility
  - colorimetry
  - composition
confidence: Preliminary
llm_ingest: true
machine_readable: true
references:
  - https://cie.co.at/publications/colorimetry-4th-edition
  - https://cie.co.at/publications/the-cie-2016-colour-appearance-model-colour-management-systems-ciecam16
  - https://www.w3.org/TR/WCAG22/
  - https://pubmed.ncbi.nlm.nih.gov/32196068/
  - https://pubmed.ncbi.nlm.nih.gov/21106682/
  - https://pubmed.ncbi.nlm.nih.gov/14650846/
  - https://pubmed.ncbi.nlm.nih.gov/23435629/
  - https://bottosson.github.io/posts/oklab/
purposes:
  - verify
  - reference
audiences:
  - researcher
---

# Project Atlas Color Evidence Registry v0.1

## Purpose

This document begins the empirical foundation of the Project Atlas
Perceptual Color Genome.

It does not attempt to teach conventional color theory or recommend
palettes. Its purpose is to extract bounded findings from color science,
vision science, psychophysics, accessibility standards, and visual-search
research, then translate those findings into candidate laws that can
eventually support composition analysis and generation.

The governing rule is:

> No color principle should be stronger than the evidence supporting it.

------------------------------------------------------------------------

# Scope of This Pass

This initial pass focuses on six foundational questions:

1. What does a numerical color value actually represent?
2. Why does the same color appear different under different conditions?
3. Which dimensions of color contribute to visual attention?
4. Is luminance categorically more important than chromatic contrast?
5. Can hue alone safely communicate structure or meaning?
6. Are perceptual color spaces sufficiently uniform to support direct
   design calculations?

The emotional and cultural meanings of color are included only where the
evidence can be bounded. Broad claims such as “blue is calming” are not
accepted at this stage.

------------------------------------------------------------------------

# Key Findings

1. **Color measurements are observer and condition models, not direct
   descriptions of subjective appearance.**
2. **Equal numerical changes do not guarantee equal perceptual changes.**
3. **Color appearance depends on illumination, adaptation, surroundings,
   display medium, and spatial context.**
4. **Luminance contrast is powerful, but chromatic contrast can independently
   support salience, segmentation, and search.**
5. **Adding chromatic contrast to weak luminance contrast can accelerate
   attentional selection under tested visual-search conditions.**
6. **Color cannot safely carry information alone because observers differ,
   viewing conditions vary, and hue distinctions can collapse without a
   lightness or shape difference.**
7. **Preference and emotion cannot be inferred from hue alone. Lightness,
   saturation, context, naturalness, experience, and population all alter
   the response.**
8. **Perceptually motivated spaces such as CIELAB and Oklab are useful models,
   not perfect maps of human experience.**

------------------------------------------------------------------------

# Foundational Distinctions

## Physical stimulus

A spectral power distribution reaching the eye.

## Colorimetric representation

A numerical compression of the stimulus based on a standard observer and
specified conditions.

## Perceptual appearance

The experienced attributes of the color, including lightness, brightness,
colorfulness, chroma, saturation, and hue.

## Functional role

What the color does in a composition, such as separating, grouping,
warning, attracting attention, encoding state, or shaping mood.

## Semantic interpretation

What the observer believes the color means based on context, convention,
culture, and prior experience.

These levels are related but not interchangeable.

------------------------------------------------------------------------

# Evidence

## CLR-EVD-001 — Standard color values model an observer

### Citation

Commission Internationale de l'Éclairage. *Colorimetry, 4th Edition.*

Source: https://cie.co.at/publications/colorimetry-4th-edition

### Evidence grade

**A — International technical standard and synthesis**

### Bounded finding

CIE colorimetry defines standard observers, illuminants, viewing conditions,
tristimulus calculations, chromaticity coordinates, color spaces, and color
difference methods.

The standard observer is a population model. It does not assert that all
individual observers experience every stimulus identically.

### Interpretation

A color token is never simply “the color.” It is a value inside a defined
measurement system.

### Supports

- CLR-LAW-001: Conditional Color Identity
- CLR-LAW-002: Measurement Is Not Appearance

### Generalizability limits

Colorimetric equality is strongest under matched viewing conditions and does
not fully model individual observer variation, adaptation, material appearance,
or complex spatial context.

------------------------------------------------------------------------

## CLR-EVD-002 — Tristimulus matches can arise from different spectra

### Citation

Commission Internationale de l'Éclairage. *Colorimetry — Part 3:
CIE Tristimulus Values.*

Source:
https://cie.co.at/publications/colorimetry-part-3-cie-tristimulus-values-1

### Evidence grade

**A — International standard**

### Bounded finding

Different spectral distributions can generate equal tristimulus values and
therefore match for a defined observer under defined conditions. This is the
basis of metamerism.

### Interpretation

Two objects or displays can appear to match in one environment and diverge in
another because the physical spectra are not identical.

### Composition implication

Cross-device and print-to-screen color consistency cannot be guaranteed by
matching a single set of nominal RGB or Lab coordinates without controlling
the rendering and viewing environment.

### Supports

- CLR-LAW-001: Conditional Color Identity
- CLR-LAW-003: Medium-Dependent Equivalence

------------------------------------------------------------------------

## CLR-EVD-003 — Appearance requires viewing-condition models

### Citation

Commission Internationale de l'Éclairage. *The CIE 2016 Colour Appearance
Model for Colour Management Systems: CIECAM16.*

Source:
https://cie.co.at/publications/the-cie-2016-colour-appearance-model-colour-management-systems-ciecam16

### Evidence grade

**A — CIE technical model**

### Bounded finding

CIECAM16 transforms tristimulus values into perceptual attribute correlates
using viewing-condition-specific parameters.

### Interpretation

The need for a color appearance model is evidence against treating XYZ, RGB,
or hex values as sufficient descriptions of appearance.

### Composition implication

Light mode, dark mode, projected display, printed output, and physical material
should not be assumed to preserve the same apparent hierarchy merely because
the nominal palette is reused.

### Supports

- CLR-LAW-001: Conditional Color Identity
- CLR-LAW-004: Appearance Adaptation

------------------------------------------------------------------------

## CLR-EVD-004 — CIELAB explicitly separates lightness, chroma, and hue

### Citation

Commission Internationale de l'Éclairage. *Colorimetry — Part 4:
CIE 1976 L*a*b* Colour Space.*

Source:
https://cie.co.at/publications/colorimetry-part-4-cie-1976-lab-colour-space-1

### Evidence grade

**A — International standard**

### Bounded finding

CIELAB defines coordinates corresponding to lightness and opponent chromatic
dimensions and provides methods for calculating color differences.

### Interpretation

Hue, chroma, and lightness must be treated as separable variables. “Changing
the color” is analytically inadequate because the change may occur primarily
along one dimension.

### Composition implication

Atlas should record at minimum:

- lightness difference
- chroma difference
- hue difference
- background and adaptation conditions
- spatial extent
- medium and gamut

### Supports

- CLR-LAW-005: Multidimensional Color Difference

------------------------------------------------------------------------

## CLR-EVD-005 — Luminance and chromatic contrast jointly affect salience

### Citation

Hardman, A. et al. (2020). *Chromaticity- and luminance-driven attentional
salience in visual search.*

PubMed: https://pubmed.ncbi.nlm.nih.gov/32196068/

### Evidence grade

**C — Controlled visual-search and electrophysiological study**

### Bounded finding

When large chromaticity contrast was added to targets with low luminance
contrast, the latency of an electrophysiological marker of attentional
selection was reduced. The findings indicate that both luminance and
chromaticity can contribute to attentional salience.

### Interpretation

The common design claim that luminance always dominates hue is too broad.
Luminance is highly important, but chromatic contrast can add salience,
particularly when luminance contrast is weak.

### Composition implication

A more defensible model is:

```text
attentional salience =
  f(luminance contrast,
    chromatic contrast,
    target size,
    background,
    distractor distribution,
    adaptation,
    task)
```

### Supports

- CLR-LAW-006: Combined Contrast Salience
- CLR-LAW-007: Task-Conditional Color Priority

### Challenges

- The absolute claim that hierarchy is always carried more strongly by
  luminance than hue.

------------------------------------------------------------------------

## CLR-EVD-006 — Salience adapts to the color distribution

### Citation

McDermott, K. C., Malkoc, G., Mulligan, J. B., & Webster, M. A. (2010).
*Adaptation and visual salience.*

PubMed: https://pubmed.ncbi.nlm.nih.gov/21106682/

### Evidence grade

**C — Controlled psychophysical experiments**

### Bounded finding

Adaptation altered visual-search salience along chromatic and luminance axes.
Comparable adaptation effects occurred for multiple color directions and for
color distributions resembling natural environments.

### Interpretation

Salience is relative to recent visual experience. A vivid color repeatedly
used throughout a composition or product may lose some of its exceptional
status.

### Composition implication

Accent color is a limited resource. Repeated exposure changes the visual
baseline against which novelty is detected.

### Supports

- CLR-LAW-008: Adaptive Salience
- CLR-LAW-009: Accent Dilution

------------------------------------------------------------------------

## CLR-EVD-007 — Local context contributes to perceived color

### Citation

Hurlbert, A. (2004). *Color contrast: a contributory mechanism to color
constancy.*

PubMed: https://pubmed.ncbi.nlm.nih.gov/14650846/

### Evidence grade

**B/C — Review and synthesis of psychophysical and physiological evidence**

### Bounded finding

Local chromatic contrast contributes to color constancy. Texture differences
can weaken chromatic contrast induction, while tested relative-motion and
relative-depth differences did not produce the same weakening. The reviewed
evidence places important contrast mechanisms at early stages of visual
processing.

### Interpretation

Surrounding colors do not merely decorate a target color. They participate in
constructing its appearance.

### Composition implication

Color tokens cannot be validated only in a palette sheet. They must be tested
inside the intended spatial and textural context.

### Supports

- CLR-LAW-010: Contextual Color Construction
- CLR-LAW-011: Palette Non-Independence

------------------------------------------------------------------------

## CLR-EVD-008 — Web contrast standards measure relative luminance

### Citation

World Wide Web Consortium. *Web Content Accessibility Guidelines 2.2.*

Source: https://www.w3.org/TR/WCAG22/

### Evidence grade

**A for the normative standard; mixed for universal perceptual prediction**

### Bounded finding

WCAG defines contrast ratio from the relative luminance of the lighter and
darker colors:

```text
(L1 + 0.05) / (L2 + 0.05)
```

The ratio ranges from 1:1 to 21:1. WCAG also prohibits using color as the only
visual means of conveying information in specified contexts.

### Interpretation

WCAG contrast is a luminance-based compliance model. It does not quantify every
aspect of readability, spatial context, font rendering, adaptation, or
chromatic differentiation.

### Composition implication

Passing contrast is a necessary floor in many contexts, not proof of complete
legibility or hierarchy.

### Supports

- CLR-LAW-012: Compliance Is Not Perception
- CLR-LAW-013: Redundant State Encoding

------------------------------------------------------------------------

## CLR-EVD-009 — Hue preference interacts with lightness and saturation

### Citation

Skelton, A. E., Catchpole, G., Abbott, J. T., Bosten, J. M., &
Franklin, A. (2017 indexing varies by source). *Color preferences in infants
and adults are different.*

PubMed: https://pubmed.ncbi.nlm.nih.gov/23435629/

### Evidence grade

**C — Controlled comparative study**

### Bounded finding

Adults in the tested population commonly preferred blues and least preferred
greenish yellows, but hue preference interacted with lightness and saturation.
Infant preferences differed from adult preferences.

### Interpretation

Preference is not a stable lookup table from hue to emotion. Development,
experience, saturation, and lightness alter the result.

### Composition implication

Brand and emotional color claims should be represented as population- and
context-dependent probabilities rather than universal mappings.

### Supports

- CLR-LAW-014: Conditional Color Preference

### Challenges

- Universal “color psychology” charts.
- Hue-only emotional classifications.

------------------------------------------------------------------------

## CLR-EVD-010 — Naturalness affects preference for color compositions

### Citation

Nascimento, S. M. C. et al. (2021). *Preference for color compositions
perceived as natural.*

PubMed: https://pubmed.ncbi.nlm.nih.gov/33965779/

### Evidence grade

**C — Controlled image experiments**

### Bounded finding

Manipulating image color gamuts changed perceived naturalness and preference.
The study supports a relationship between ecological plausibility and the
evaluation of color compositions.

### Interpretation

People may prefer some palettes not because of geometric relationships on a
color wheel, but because the palette resembles learned regularities in natural
scenes.

### Composition implication

“Color harmony” may partly emerge from statistical familiarity, semantic
coherence, and ecological expectation rather than hue-angle geometry alone.

### Supports

- CLR-LAW-015: Ecological Color Coherence

### Challenges

- The assumption that fixed complementary, analogous, or triadic geometry is a
  complete explanation for harmony.

------------------------------------------------------------------------

## CLR-EVD-011 — Oklab is a practical perceptual model, not settled ground truth

### Citation

Ottosson, B. (2020). *A perceptual color space for image processing.*

Source: https://bottosson.github.io/posts/oklab/

### Evidence grade

**E/C — Engineering model supported by comparative datasets and tests, but
not a formal international standard**

### Bounded finding

Oklab was designed to improve practical prediction of perceived lightness,
chroma, and hue while remaining computationally simple for image processing
and interpolation.

### Interpretation

Oklab and OKLCH are useful working spaces for design systems because their
dimensions are easier to manipulate than raw RGB. They should not be treated
as perfectly perceptually uniform.

### Composition implication

Atlas may use OKLCH for implementation and token generation while retaining
CIE color-difference methods and empirical validation for research claims.

### Supports

- CLR-LAW-005: Multidimensional Color Difference
- CLR-LAW-016: Model-Bounded Uniformity

------------------------------------------------------------------------

# Observations

## CLR-OBS-001 — A palette is a system of relationships

### Observation

The evidence does not support evaluating a color independently of its
background, adaptation state, medium, or neighboring colors.

### Interpretation

A palette entry should be stored as a node with conditional relationships, not
as an isolated hex value.

### Confidence

**High**

------------------------------------------------------------------------

## CLR-OBS-002 — Luminance is foundational but not sufficient

### Observation

Luminance supports boundary detection, contrast measurement, and readability,
but chromatic contrast can independently alter attention and segmentation.

### Interpretation

Atlas should reject both extremes:

- hue is enough
- only luminance matters

### Confidence

**Moderate to high**

------------------------------------------------------------------------

## CLR-OBS-003 — Accent power depends on rarity

### Observation

Adaptation and visual-search research indicate that salience depends on the
distribution of features in the surrounding field and recent experience.

### Interpretation

An accent color used everywhere stops functioning as an accent.

### Confidence

**Moderate**

------------------------------------------------------------------------

## CLR-OBS-004 — Accessibility and hierarchy are different questions

### Observation

A color pair can satisfy a luminance contrast rule while still failing as a
complete state, grouping, or attentional system.

### Interpretation

Atlas must distinguish:

1. detectability
2. readability
3. identifiability
4. grouping
5. state interpretation
6. attentional priority

### Confidence

**High**

------------------------------------------------------------------------

## CLR-OBS-005 — Harmony may be ecological as well as geometric

### Observation

Preference can depend on naturalness and familiar environmental color
statistics.

### Interpretation

Color-wheel geometry is at best one contributor to perceived harmony.

### Confidence

**Moderate**

------------------------------------------------------------------------

# Candidate Laws

## CLR-LAW-001 — Law of Conditional Color Identity

### Hypothesis

The perceived identity of a color depends on the stimulus, observer,
illumination, adaptation, surrounding field, spatial scale, and medium.

### Prediction

A fixed colorimetric or RGB value will receive different appearance matches or
attribute ratings when one or more contextual variables change.

### Supporting evidence

- CLR-EVD-001
- CLR-EVD-002
- CLR-EVD-003
- CLR-EVD-007

### Confidence

**High**

------------------------------------------------------------------------

## CLR-LAW-005 — Law of Multidimensional Color Difference

### Hypothesis

Color difference cannot be adequately represented as unweighted distance in
device RGB coordinates.

### Prediction

Pairs with equal RGB distance will not produce equal perceived difference,
while perceptually motivated color-difference models will improve prediction
under their validated conditions.

### Supporting evidence

- CLR-EVD-004
- CLR-EVD-011

### Confidence

**High for the inadequacy of raw RGB distance; moderate for any one replacement
model**

------------------------------------------------------------------------

## CLR-LAW-006 — Law of Combined Contrast Salience

### Hypothesis

Attentional salience can be produced by luminance contrast, chromatic contrast,
or their interaction.

### Prediction

Adding task-relevant chromatic contrast to a low-luminance-contrast target will
improve selection speed or accuracy under at least some visual-search
conditions.

### Supporting evidence

- CLR-EVD-005
- CLR-EVD-006

### Counter evidence and boundary

The relative contribution will vary with target size, spatial frequency,
eccentricity, background, color direction, adaptation, and task. This law does
not imply that hue can replace adequate text luminance contrast.

### Confidence

**Moderate to high**

------------------------------------------------------------------------

## CLR-LAW-008 — Law of Adaptive Salience

### Hypothesis

The salience of a color feature decreases as the visual system adapts to that
feature distribution.

### Prediction

A color initially detected rapidly as an outlier will lose search advantage as
the same or similar color becomes frequent or repeatedly viewed.

### Supporting evidence

- CLR-EVD-006

### Confidence

**Moderate**

------------------------------------------------------------------------

## CLR-LAW-010 — Law of Contextual Color Construction

### Hypothesis

The apparent color of an element is partly constructed from local and global
context rather than determined solely by the element's physical or numerical
value.

### Prediction

Holding the target value constant while changing surrounding color, texture,
or adaptation will alter appearance judgments.

### Supporting evidence

- CLR-EVD-003
- CLR-EVD-007

### Confidence

**High**

------------------------------------------------------------------------

## CLR-LAW-012 — Law of Compliance–Perception Separation

### Hypothesis

Passing a formal contrast threshold does not guarantee successful recognition,
grouping, state interpretation, or attentional priority.

### Prediction

Some displays that pass a contrast requirement will still show performance
differences when typography, crowding, adaptation, glare, chromatic confusion,
or competing hierarchy is manipulated.

### Supporting evidence

- CLR-EVD-008
- Existing Project Atlas crowding and recognition evidence

### Confidence

**High as a conceptual boundary; quantitative UI thresholds remain to be
derived**

------------------------------------------------------------------------

## CLR-LAW-013 — Law of Redundant State Encoding

### Hypothesis

Critical information encoded by color is more robust when reinforced by an
independent cue such as shape, text, position, pattern, or iconography.

### Prediction

State-identification accuracy across varied observers and viewing conditions
will be higher for redundant encodings than for hue-only encoding.

### Supporting evidence

- CLR-EVD-008
- Existing Project Atlas Reinforced Structure and Cue Competition laws

### Confidence

**High as an accessibility principle; implementation effectiveness depends on
cue compatibility**

------------------------------------------------------------------------

## CLR-LAW-014 — Law of Conditional Color Preference

### Hypothesis

Color preference is a function of hue, lightness, saturation, context,
development, experience, and population rather than hue alone.

### Prediction

Preference rankings will change when lightness, saturation, surrounding colors,
image semantics, population, or task changes while hue is held constant.

### Supporting evidence

- CLR-EVD-009
- CLR-EVD-010

### Confidence

**Moderate to high**

------------------------------------------------------------------------

## CLR-LAW-015 — Law of Ecological Color Coherence

### Hypothesis

Color combinations that preserve familiar environmental or semantic
relationships will often be judged more natural and may be preferred over
equally structured but ecologically implausible combinations.

### Prediction

Rotating or remapping an image gamut while preserving spatial structure will
reduce naturalness and preference when the transformation violates learned
color regularities.

### Supporting evidence

- CLR-EVD-010

### Confidence

**Moderate**

------------------------------------------------------------------------

## CLR-LAW-016 — Law of Model-Bounded Uniformity

### Hypothesis

Every perceptual color space is an approximation whose uniformity depends on
the dataset, task, gamut, adaptation, and color-difference scale used for its
construction or evaluation.

### Prediction

A color space that performs well for interpolation or moderate sRGB differences
will show systematic errors in at least some other hue, chroma, adaptation, or
suprathreshold conditions.

### Supporting evidence

- CLR-EVD-004
- CLR-EVD-011
- The continuing development of revised CIE color-difference and appearance
  models

### Confidence

**High**

------------------------------------------------------------------------

# Provisional Color Relationship Model

```text
Observed Color Outcome =
    Physical Stimulus
  × Observer Sensitivity
  × Adaptation State
  × Viewing Conditions
  × Spatial Context
  × Temporal Context
  × Task Relevance
  × Learned Meaning
```

A more explicit placeholder:

```text
Cₒ = f(S, O, I, A, B, X, M, T, R, K)
```

Where:

- `S` = spectral or encoded stimulus
- `O` = observer characteristics
- `I` = illumination
- `A` = adaptation state
- `B` = background and neighboring colors
- `X` = spatial extent and position
- `M` = medium, material, and display
- `T` = temporal exposure and change
- `R` = task relevance
- `K` = learned semantic and cultural knowledge

This is a causal inventory, not a fitted equation.

------------------------------------------------------------------------

# Data Schema for Future Evidence Extraction

```yaml
evidence_id:
citation:
year:
discipline:
evidence_grade:
genome_nodes:
population:
observer_characteristics:
sample_size:
stimulus_medium:
display_or_material:
illuminant:
adaptation:
viewing_distance:
visual_angle:
background:
spatial_context:
task:
independent_variables:
dependent_variables:
color_space:
color_difference_formula:
quantitative_results:
effect_size:
statistical_significance:
authors_conclusion:
bounded_finding:
atlas_interpretation:
candidate_laws_supported:
candidate_laws_challenged:
generalizability_limits:
replication_status:
source:
```

------------------------------------------------------------------------

# Open Questions

1. Under which spatial-frequency and target-size conditions does luminance
   dominate chromatic contrast?
2. How does chromatic contrast interact with peripheral crowding?
3. What is the best practical perceptual space for UI token generation versus
   scientific color-difference prediction?
4. How quickly does repeated accent use reduce salience?
5. How should color hierarchy be modeled in mixed light and dark surfaces?
6. How do age, lens yellowing, color-vision deficiency, glare, and low vision
   alter usable chromatic separations?
7. Can palette harmony be predicted from natural-image statistics,
   semantic relationships, or processing fluency?
8. When do multiple colors cease to form hierarchy and begin to create
   competition?
9. How does colored area affect apparent lightness, chroma, and dominance?
10. Which findings transfer from isolated laboratory patches to complex
    interface, editorial, architectural, and artistic compositions?

------------------------------------------------------------------------

# Next Actions

1. Build a dedicated evidence track for luminance versus chromatic contrast.
2. Extract quantitative visual-search results by target size, eccentricity,
   and distractor set size.
3. Build an accessibility track covering normal vision, color-vision
   deficiency, aging, glare, and low vision.
4. Compare CIELAB, CIEDE2000, CAM16-UCS, Oklab, and OKLCH by intended use.
5. Build a contextual appearance track covering simultaneous contrast,
   adaptation, induction, and color constancy.
6. Build a preference and emotion track that explicitly records population,
   object context, saturation, lightness, and semantic meaning.
7. Link these laws to existing Atlas laws concerning similarity, cue
   competition, search competition, reinforced structure, and recognition
   beyond visibility.

------------------------------------------------------------------------

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 0.1 | 2026-07-18 | Kevin Miller and ChatGPT | Initial source-grounded color evidence registry with eleven evidence records, five observations, nine candidate laws, and a research schema. |

------------------------------------------------------------------------

# Agent Instructions

When creating or modifying this document:

1. Separate observation from interpretation.
2. Never strengthen a conclusion beyond the available evidence.
3. Preserve contradictory findings.
4. Prefer measurable variables over subjective descriptions.
5. Reference candidate laws and genome nodes whenever possible.
6. Use stable IDs for observations, evidence, laws, experiments, metrics, and
   case studies.
7. Record assumptions explicitly.
8. Record confidence explicitly.
9. Keep the YAML header valid.
10. Do not delete revision history; append to it.
