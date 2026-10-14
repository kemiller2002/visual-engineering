---
id: RR-VE-ATL-2026-4D32
authors:
- Kevin Miller
- ChatGPT
confidence: Moderate
date: 2026-07-18
llm_ingest: true
machine_readable: true
project: project-atlas
purpose: |
  Compare how major disciplines define, measure, and apply color, identify
  genuine disagreements versus differences in scope, and reorganize the
  Project Atlas Color Genome around shared perceptual mechanisms rather than
  professional domains.
references:
- https://cie.co.at/publications/cie-2016-colour-appearance-model-colour-management-systems-ciecam16
- https://www.w3.org/TR/WCAG22/
- https://www.w3.org/WAI/WCAG21/Understanding/use-of-color.html
- https://pubmed.ncbi.nlm.nih.gov/23020642/
- https://pubmed.ncbi.nlm.nih.gov/21264737/
- https://pubmed.ncbi.nlm.nih.gov/21106682/
- https://pubmed.ncbi.nlm.nih.gov/32196068/
- https://pubmed.ncbi.nlm.nih.gov/14650846/
- https://cie.co.at/publications/effect-stimulus-size-colour-appearance
status: working-draft
summary: |
  Color disciplines appear to conflict partly because they optimize for
  different outcomes: physical measurement, perceptual prediction, search
  performance, accessibility, expression, spatial experience, or narrative.
  The comparison supports reorganizing Atlas by perceptual mechanism and
  functional outcome rather than by profession. Traditional design concepts
  remain useful, but they should be treated as applied heuristics built on
  lower-level mechanisms such as adaptation, contrast, grouping, salience,
  ecological familiarity, and learned meaning.
version: 0.1
purposes:
  - orient
  - integrate
  - reference
audiences:
  - practitioner
  - researcher
  - contributor
---

# Project Atlas: Comparative Color Framework

## Purpose

This document compares the major intellectual and professional traditions that
study or use color.

The goal is not to determine which discipline is correct. Most apparent
conflicts arise because different disciplines are answering different
questions.

For example:

- Physics asks what light is present.
- Colorimetry asks how a standard observer would match it.
- Vision science asks how the nervous system processes it.
- Psychophysics asks how perception or performance changes under controlled
  conditions.
- Accessibility asks whether information remains available to a broad range of
  users.
- Graphic design asks whether color communicates and organizes effectively.
- Fine art asks what color can make visible, expressive, or emotionally
  compelling.
- Architecture asks how light, material, scale, and duration shape lived space.
- Cinematography asks how color guides narrative interpretation over time.

These are related questions, but they are not interchangeable.

------------------------------------------------------------------------

# Key Findings

-   Most disciplinary disagreements are differences in **dependent variable**,
    not direct contradictions.
-   “Color” can refer to a spectrum, a tristimulus value, an appearance, a
    category, a compositional role, an emotional association, or a semantic
    signal.
-   Traditional color theory is strongest as a descriptive and generative
    vocabulary, but weaker as a universal causal theory.
-   Color science predicts matching and appearance more rigorously than design
    practice, but usually does not explain hierarchy, meaning, narrative, or
    composition by itself.
-   Psychophysics provides measurable behavioral evidence, but often uses
    simplified stimuli that may not transfer cleanly to rich compositions.
-   Accessibility standards provide robust minimum constraints, but compliance
    does not equal complete perceptual success.
-   Art, architecture, and cinematography preserve high-value ecological
    knowledge developed through repeated practice, but many claims remain
    under-specified or difficult to isolate experimentally.
-   Atlas should organize color primarily around mechanisms and outcomes, then
    map each discipline onto those mechanisms.

------------------------------------------------------------------------

# Content

# 1. The Central Comparison Problem

A discipline can appear wrong when it is merely incomplete outside its intended
scope.

A CIE color appearance model is not intended to explain whether a red button
feels urgent. A painter’s complementary palette is not intended to provide a
numerically uniform color-difference metric. WCAG contrast is not intended to
predict every condition of visual comfort or aesthetic hierarchy.

Therefore, Atlas should compare every claim using five questions:

1. What stimulus is being studied?
2. What observer or population is assumed?
3. What outcome is being measured?
4. Under what viewing conditions?
5. Over what spatial and temporal scale?

Without those five variables, cross-disciplinary comparison becomes unreliable.

------------------------------------------------------------------------

# 2. Discipline Comparison Matrix

| Discipline | Primary Question | Typical Unit or Evidence | Main Strength | Main Blind Spot |
|---|---|---|---|---|
| Physics and optics | What radiation is emitted, transmitted, reflected, or absorbed? | Spectral power distribution, wavelength, radiance, reflectance | Objective characterization of the stimulus | Does not by itself explain experienced color |
| Colorimetry | How would a standard observer match or quantify the stimulus? | XYZ, chromaticity, Lab, color difference | Reproducible measurement and color management | Standard observers and simplified conditions do not capture all individual or contextual variation |
| Color appearance science | How will color attributes change with viewing conditions? | CIECAM16 correlates, adaptation parameters | Explicitly models context and adaptation | Still approximates complex scenes, materials, and observers |
| Neuroscience | How does the visual system encode and transform color information? | Neural response, imaging, electrophysiology | Mechanistic explanation across processing stages | Often does not translate directly into design rules |
| Psychophysics | What can observers detect, discriminate, search for, match, or prefer? | Thresholds, response time, accuracy, ratings | Quantitative behavior tied to controlled variables | Laboratory stimuli can be ecologically thin |
| Accessibility and human factors | Can diverse users perceive and correctly interpret critical information? | Contrast thresholds, error rates, task completion, standards | Strong emphasis on robustness and inclusion | Minimum compliance can be mistaken for complete usability |
| Graphic and information design | Does color organize, distinguish, encode, and emphasize? | Visual hierarchy, grouping, communication success | Directly addresses compositional function | Often relies on inherited heuristics and taste |
| Interface design | Does color support interaction, status, navigation, and readability? | Task performance, usability testing, design tokens | Connects color to state and action | May reduce color to token systems and static contrast checks |
| Fine art | What perceptual and expressive effects can color produce? | Studio practice, critique, historical precedent | Rich knowledge of interaction, balance, atmosphere, and expression | Causal claims are often informal or difficult to isolate |
| Architecture and environmental design | How do color, light, material, scale, and time shape space? | Material samples, daylight studies, occupancy response | Treats color as embodied and environmental | Quantitative color control is difficult in changing real environments |
| Cinematography and photography | How does color guide attention, continuity, mood, and narrative over time? | Grading, exposure, scene comparison, audience response | Integrates color with light, motion, sequence, and story | Narrative and cultural factors complicate causal attribution |
| Branding and marketing | What meanings and recognitions become associated with a color system? | Recall, recognition, preference, conversion | Strong focus on learned meaning and consistency | Frequently overstates universal color psychology |
| Data visualization and cartography | Can color encode quantity, category, order, uncertainty, and emphasis? | Accuracy, search time, perceptual ordering | Forces explicit mapping between color and information structure | Display and audience constraints can be oversimplified |

------------------------------------------------------------------------

# 3. What Each Discipline Means by “Color”

## 3.1 Physics

Color is not the object of measurement in the subjective sense.

Physics measures:

- electromagnetic radiation
- spectral composition
- reflectance
- absorption
- emission
- transmission
- scattering

The physical stimulus constrains perception but does not uniquely determine it.
Different spectra can produce the same colorimetric match for a defined observer,
and the same physical surface can appear different under different illumination.

### Atlas translation

Physics supplies the **input layer**, not the complete perceptual outcome.

------------------------------------------------------------------------

## 3.2 Colorimetry

Colorimetry compresses a spectral stimulus into values based on standardized
observer functions and defined conditions.

It provides the common language required for:

- display calibration
- printing
- manufacturing
- color matching
- color difference
- reproduction

CIECAM16 explicitly exists because tristimulus values alone do not predict
appearance across viewing conditions.

### Atlas translation

Colorimetry supplies measurement infrastructure, but every number must retain
its observer, illuminant, medium, and adaptation assumptions.

------------------------------------------------------------------------

## 3.3 Neuroscience

Neuroscience treats color as a distributed process rather than a single
property.

Relevant mechanisms include:

- cone responses
- post-receptoral opponent channels
- receptive-field organization
- adaptation
- cortical transformation
- interactions with shape, attention, memory, and object knowledge

The strength of this discipline is causal mechanism. Its weakness for Atlas is
that neural descriptions do not automatically resolve the level at which a
designer should intervene.

### Atlas translation

Neuroscience explains why mechanisms are plausible, but psychophysics and
applied testing are usually required to estimate usable design bounds.

------------------------------------------------------------------------

## 3.4 Psychophysics

Psychophysics asks what observers can do.

Typical tasks include:

- detect a difference
- discriminate two stimuli
- match an appearance
- locate a target
- identify a category
- rate preference
- judge naturalness
- remember a color

Visual-search studies show that both luminance and chromatic contrast can
contribute to salience, while adaptation studies show that salience changes
with the surrounding distribution and recent exposure.

### Atlas translation

Psychophysics is one of the strongest bridges between mechanism and design, but
Atlas must record task, visual angle, eccentricity, background, duration, and
population before generalizing.

------------------------------------------------------------------------

## 3.5 Accessibility

Accessibility treats color as a reliability problem.

The relevant question is not merely whether a typical observer can notice a
difference. It is whether critical information remains perceivable and
interpretable across varied visual abilities and conditions.

WCAG requires minimum luminance contrast in defined cases and prohibits using
color as the only visual means of conveying certain information. This reflects
a broader principle: information should survive the failure or weakening of
one perceptual channel.

### Atlas translation

Accessibility constraints are not a separate moral overlay on design. They
reveal the fragility of single-channel encoding.

------------------------------------------------------------------------

## 3.6 Graphic and Interface Design

Design usually treats color functionally:

- establish hierarchy
- group related elements
- distinguish states
- create emphasis
- support navigation
- reinforce brand identity
- create atmosphere

This is closer to Atlas’s intended application than pure colorimetry, but design
terminology often collapses distinct mechanisms. “Contrast” may refer to
luminance, hue, saturation, area, semantic opposition, or overall visual
difference.

### Atlas translation

Design claims should be decomposed into measurable perceptual variables and
explicit functional outcomes.

------------------------------------------------------------------------

## 3.7 Fine Art

Fine art contains extensive practical knowledge of:

- simultaneous contrast
- optical mixture
- warm-cool relationships
- atmospheric depth
- local versus perceived color
- chromatic balance
- color area
- expressive exaggeration
- palette limitation

The evidence often comes from stable recurrence across artists and periods
rather than controlled experiments.

### Atlas translation

Studio traditions should be treated as a source of high-value hypotheses and
case evidence, not dismissed because they are not laboratory studies.

------------------------------------------------------------------------

## 3.8 Architecture

Architecture adds variables that flat-screen design often ignores:

- changing daylight
- artificial light spectra
- reflectance over large surfaces
- material texture
- gloss
- distance
- peripheral exposure
- movement through space
- prolonged occupancy
- color contamination between surfaces

CIE research on stimulus size notes that large color fields can differ in
appearance from standard small-field measurements and may not be fully predicted
by standard observers or conventional appearance models.

### Atlas translation

Color area and embodied exposure must become first-class variables in the Color
Genome.

------------------------------------------------------------------------

## 3.9 Cinematography

Cinematography treats color as a temporal and narrative system.

A color can function through:

- anticipation
- recurrence
- transformation
- character association
- location coding
- emotional progression
- contrast across cuts
- adaptation within a sequence

### Atlas translation

The meaning and salience of a color depend partly on its history within the
composition, not only its local contrast.

------------------------------------------------------------------------

# 4. Mechanism-by-Discipline Comparison

## 4.1 Simultaneous Contrast

| Discipline | Treatment |
|---|---|
| Physics | The target spectrum may remain unchanged |
| Colorimetry | Basic tristimulus values remain fixed under fixed measurement conditions |
| Appearance science | Surround and adaptation parameters alter predicted appearance |
| Neuroscience | Context-sensitive receptive fields and opponent processing contribute |
| Psychophysics | Measured through matches, induction, discrimination, and constancy tasks |
| Fine art | Used deliberately to intensify, neutralize, shift, or animate color |
| UI design | Often encountered indirectly when tokens change appearance on different surfaces |
| Architecture | Amplified by large fields, reflected light, materials, and changing illumination |

### Synthesis

Fine art’s claim that neighboring colors change one another is strongly
compatible with appearance science and psychophysics. The disagreement is
mostly one of vocabulary and explanatory depth.

### Atlas mechanism

**Contextual color construction**

------------------------------------------------------------------------

## 4.2 Color Harmony

| Tradition | Main Explanation |
|---|---|
| Classical and instructional color theory | Geometric relationships on a hue circle |
| Art practice | Balance among hue, value, saturation, area, and expressive intent |
| Psychophysics | Preference and harmony judgments vary with hue similarity, lightness contrast, and component preference |
| Ecological accounts | Preference reflects familiar or natural color statistics |
| Neuroscience | Identifies correlates of pleasant or harmonious judgments, but does not establish a single universal rule |
| Cultural and semantic accounts | Compatibility depends on learned associations and object meaning |

Research distinguishes harmony from preference. In controlled studies, pair
harmony and pair preference overlap but are not identical. Preference is also
influenced by the desirability of the individual colors and by lightness
contrast.

### Synthesis

Complementary, analogous, and triadic structures may be useful palette
generators, but they are not sufficient causal explanations of harmony.

### Atlas mechanism set

- perceptual similarity
- controlled contrast
- component preference
- ecological familiarity
- semantic compatibility
- area balance
- processing fluency

------------------------------------------------------------------------

## 4.3 Attention and Salience

| Discipline | Main Model |
|---|---|
| Neuroscience | Competition and selection across feature-sensitive systems |
| Psychophysics | Search time, accuracy, pop-out, and adaptation |
| Graphic design | Emphasis and focal point |
| UI design | Accent, notification, warning, call to action |
| Cinematography | Guided gaze through light, color, motion, and narrative relevance |
| Architecture | Wayfinding, landmarking, boundary definition |

Controlled evidence indicates that luminance and chromatic contrast can both
contribute to attentional salience. Adaptation changes the relative salience of
features, meaning an accent is not a fixed property of a color.

### Synthesis

“Use a bright color to attract attention” is under-specified.

A stronger statement is:

> A feature attracts attention when it differs sufficiently from the current
> perceptual distribution in a task-relevant dimension, subject to adaptation,
> scale, position, and competition.

### Atlas mechanism

**Distribution-relative feature contrast**

------------------------------------------------------------------------

## 4.4 Meaning and Emotion

| Discipline | Typical Position |
|---|---|
| Marketing | Colors possess recognizable emotional or behavioral associations |
| Cross-cultural psychology | Some associations recur, but magnitude and meaning vary |
| Art | Meaning emerges from context, history, material, and composition |
| Cinematography | Meaning is constructed through recurrence and narrative |
| UI design | Meaning is partly conventional: red error, green success, blue link |
| Neuroscience | Can study affective responses but does not validate simple hue-to-emotion tables |

### Synthesis

A hue is not an emotion.

Emotional and semantic effects should be modeled as interactions among:

- hue
- lightness
- chroma
- area
- material
- context
- object
- culture
- learned convention
- narrative history
- individual experience

### Atlas mechanism

**Learned and contextual color semantics**

------------------------------------------------------------------------

## 4.5 Accessibility and Redundancy

| Discipline | Core Concern |
|---|---|
| Accessibility | Information must remain available across visual differences |
| Information design | Categories and states must be distinguishable |
| UI design | Users must recognize controls, focus, status, and errors |
| Gestalt psychology | Multiple congruent cues strengthen grouping |
| Human factors | Critical signals require reliability under degraded conditions |

### Synthesis

The accessibility rule against hue-only information is a special case of a
broader compositional law:

> Important structure should be represented by more than one independent,
> compatible cue when failure carries meaningful cost.

### Atlas mechanism

**Reinforced encoding**

------------------------------------------------------------------------

# 5. True Disagreements Versus False Disagreements

## False disagreement: “Color is physical” versus “color is perceptual”

Both claims can be valid at different levels.

- A surface has measurable spectral properties.
- Experienced color is produced by an observer interacting with that stimulus
  under particular conditions.

The error is treating either level as the whole phenomenon.

------------------------------------------------------------------------

## False disagreement: “Luminance matters more” versus “color attracts attention”

Both can be true under different tasks.

- Luminance is critical for text contrast and many boundary judgments.
- Chromatic contrast can contribute independently to search and segmentation.
- The balance changes with scale, spatial frequency, adaptation, and task.

------------------------------------------------------------------------

## True disagreement: Universal hue psychology

The strong claim that a hue has a stable, general emotional meaning is not
well supported.

The evidence instead favors conditional associations.

------------------------------------------------------------------------

## True disagreement: Fixed color-wheel harmony as universal law

Hue geometry alone does not account for component preference, lightness,
chroma, area, ecological naturalness, individual variation, or semantic
context.

Color-wheel relationships remain useful compositional tools, but their status
should be downgraded from universal laws to bounded heuristics.

------------------------------------------------------------------------

## True disagreement: Numerical equality as perceptual equality

Raw numerical equality or equal coordinate distance does not guarantee equal
appearance or equal perceptual difference across contexts.

This disagreement is resolved in favor of condition-dependent perceptual
modeling.

------------------------------------------------------------------------

# 6. Revised Atlas Architecture

The comparison supports moving Project Atlas from a catalog of design domains
to a layered science of perception and communication.

```text
Project Atlas
│
├── Physical Environment
│   ├── Light
│   ├── Sound
│   ├── Material
│   ├── Scale
│   └── Time
│
├── Perceptual Systems
│   ├── Vision
│   │   ├── Color
│   │   ├── Luminance
│   │   ├── Form
│   │   ├── Depth
│   │   ├── Motion
│   │   └── Spatial frequency
│   ├── Audition
│   ├── Touch
│   └── Multisensory integration
│
├── Organizing Mechanisms
│   ├── Contrast
│   ├── Similarity
│   ├── Grouping
│   ├── Segmentation
│   ├── Hierarchy
│   ├── Rhythm
│   ├── Balance
│   ├── Adaptation
│   └── Prediction
│
├── Cognitive Systems
│   ├── Attention
│   ├── Memory
│   ├── Categorization
│   ├── Meaning
│   ├── Emotion
│   ├── Learning
│   └── Decision making
│
├── Communication Functions
│   ├── Identification
│   ├── Navigation
│   ├── State
│   ├── Priority
│   ├── Relationship
│   ├── Narrative
│   └── Expression
│
└── Applied Domains
    ├── Graphic design
    ├── Interface design
    ├── Typography
    ├── Architecture
    ├── Fine art
    ├── Cinematography
    ├── Data visualization
    ├── Industrial design
    └── Environmental design
```

## Implication

Color is no longer the top-level object of study.

It is one perceptual channel participating in larger mechanisms such as:

- contrast
- grouping
- hierarchy
- attention
- memory
- semantics
- emotion
- navigation

This structure allows findings from one domain to transfer to another without
pretending the applications are identical.

------------------------------------------------------------------------

# 7. Proposed Color Comparison Record

Every future comparison should use the following schema:

```yaml
comparison_id:
topic:
claim_a:
discipline_a:
claim_b:
discipline_b:
stimulus_difference:
observer_difference:
task_difference:
outcome_difference:
viewing_condition_difference:
apparent_conflict:
conflict_type:
  - terminology
  - scope
  - measurement
  - population
  - genuine-empirical
shared_mechanism:
evidence_for_a:
evidence_for_b:
synthesis:
atlas_implication:
confidence:
open_questions:
```

------------------------------------------------------------------------

# Observations

## CLR-CMP-OBS-001

### Observation

Professional disciplines often use the same word for different measurable
constructs.

### Interpretation

Atlas requires controlled vocabulary that distinguishes physical contrast,
luminance contrast, chromatic contrast, semantic contrast, and compositional
contrast.

### Confidence

High

------------------------------------------------------------------------

## CLR-CMP-OBS-002

### Observation

Traditional design theory often preserves valid effects while giving an
incomplete causal explanation.

### Interpretation

Atlas should not discard traditional knowledge. It should decompose it into
testable mechanisms and retain the practical rule when it remains useful.

### Confidence

Moderate to high

------------------------------------------------------------------------

## CLR-CMP-OBS-003

### Observation

The strongest transferable laws occur below the level of professional domain.

### Interpretation

Mechanisms such as adaptation, grouping, salience, cue redundancy, and context
transfer more reliably than domain-specific prescriptions.

### Confidence

High

------------------------------------------------------------------------

## CLR-CMP-OBS-004

### Observation

Color meaning behaves more like learned language than like a fixed sensory
property.

### Interpretation

Semantic color systems should be modeled historically and culturally, with
conventions becoming stronger through repeated consistent use.

### Confidence

Moderate

------------------------------------------------------------------------

## CLR-CMP-OBS-005

### Observation

Architecture and cinematography reveal variables underrepresented in interface
color research: scale, duration, movement, changing illumination, and sequence.

### Interpretation

A complete Color Genome must include spatial extent and temporal history as
core variables.

### Confidence

Moderate to high

------------------------------------------------------------------------

# Evidence

## CLR-CMP-EVD-001

### Citation

Commission Internationale de l'Éclairage. *The CIE 2016 Colour Appearance
Model for Colour Management Systems: CIECAM16.*

### Summary

CIECAM16 uses viewing-condition-specific parameters to transform tristimulus
values into perceptual attribute correlates, demonstrating that appearance
cannot be inferred from stimulus coordinates alone.

### Supports

-   CLR-CMP-LAW-001
-   CLR-CMP-LAW-002

### Challenges

-   Claims that a fixed digital token has a stable perceptual identity across
    environments.

------------------------------------------------------------------------

## CLR-CMP-EVD-002

### Citation

Schloss, K. B., & Palmer, S. E. *Aesthetic response to color combinations.*

### Summary

Pair preference and harmony are related but distinct. Both increased with hue
similarity in the reported experiments, while preference depended more strongly
on component preference and lightness contrast.

### Supports

-   CLR-CMP-LAW-003

### Challenges

-   Single-factor accounts of harmony.
-   Treating harmony and preference as synonyms.

------------------------------------------------------------------------

## CLR-CMP-EVD-003

### Citation

McDermott, K. C. et al. *Adaptation and visual salience.*

### Summary

Adaptation to color distributions changed subsequent search performance,
showing that salience depends on recent visual context and feature distribution.

### Supports

-   CLR-CMP-LAW-004

### Challenges

-   Treating accent strength as an intrinsic property of a hue.

------------------------------------------------------------------------

## CLR-CMP-EVD-004

### Citation

World Wide Web Consortium. *Web Content Accessibility Guidelines 2.2* and
*Understanding Success Criterion 1.4.1: Use of Color.*

### Summary

WCAG defines luminance-based contrast constraints for specified content and
requires that color not be the sole visual means of communicating certain
information.

### Supports

-   CLR-CMP-LAW-005

### Challenges

-   Hue-only encoding of critical information.
-   Treating contrast compliance as a complete design evaluation.

------------------------------------------------------------------------

## CLR-CMP-EVD-005

### Citation

Commission Internationale de l'Éclairage. *Effect of Stimulus Size on Colour
Appearance.*

### Summary

The CIE report states that large color stimuli can produce appearance effects
not predicted by standard observers or CIECAM02, supporting the importance of
visual extent.

### Supports

-   CLR-CMP-LAW-006

### Challenges

-   Directly transferring small-swatch measurements to architecture or
    immersive fields.

------------------------------------------------------------------------

# Candidate Laws

## CLR-CMP-LAW-001 — Law of Layered Color Description

### Hypothesis

No single disciplinary description of color is complete because physical,
colorimetric, perceptual, functional, and semantic descriptions operate at
different explanatory levels.

### Prediction

Cross-disciplinary disputes will often dissolve when claims are assigned to
their correct explanatory layer and dependent variable.

### Supporting Evidence

- CLR-CMP-EVD-001
- Comparative disciplinary analysis

### Counter Evidence

Some disputes remain genuine after variables and outcomes are aligned.

### Confidence

High

------------------------------------------------------------------------

## CLR-CMP-LAW-002 — Law of Conditional Transfer

### Hypothesis

A color finding transfers between disciplines only to the degree that observer,
stimulus, task, scale, medium, and viewing conditions remain equivalent.

### Prediction

Rules derived from small laboratory patches will lose predictive accuracy when
applied directly to immersive architecture, complex interfaces, or narrative
sequences without adjustment.

### Supporting Evidence

- CLR-CMP-EVD-001
- CLR-CMP-EVD-005

### Counter Evidence

Some low-level mechanisms remain directionally stable across broad conditions.

### Confidence

High

------------------------------------------------------------------------

## CLR-CMP-LAW-003 — Law of Composite Harmony

### Hypothesis

Perceived color harmony is produced by multiple interacting variables rather
than hue geometry alone.

### Prediction

Hue relationships will fail to predict harmony consistently when component
preference, lightness, chroma, area, naturalness, or semantic context is
manipulated.

### Supporting Evidence

- CLR-CMP-EVD-002
- Palmer review of visual aesthetics

### Counter Evidence

Hue geometry may remain a useful predictor within constrained palettes and
tasks.

### Confidence

Moderate to high

------------------------------------------------------------------------

## CLR-CMP-LAW-004 — Law of Distribution-Relative Accent

### Hypothesis

The attentional strength of a color is determined by its difference from the
current feature distribution and adaptation state, not by hue identity alone.

### Prediction

Repeated use of an accent color will reduce its relative search advantage and
hierarchical exclusivity.

### Supporting Evidence

- CLR-CMP-EVD-003

### Counter Evidence

Learned semantic importance may sustain attention despite visual repetition.

### Confidence

Moderate

------------------------------------------------------------------------

## CLR-CMP-LAW-005 — Law of Reinforced Critical Encoding

### Hypothesis

Critical information becomes more robust when color is paired with at least one
independent compatible cue.

### Prediction

Color-plus-shape, color-plus-text, or color-plus-position systems will produce
more reliable identification across observers and degraded viewing conditions
than hue-only systems.

### Supporting Evidence

- CLR-CMP-EVD-004

### Counter Evidence

Redundant cues can interfere when their meanings conflict.

### Confidence

High

------------------------------------------------------------------------

## CLR-CMP-LAW-006 — Law of Spatial-Extent Dependence

### Hypothesis

The perceived appearance and compositional dominance of a color depend partly
on its visual angle and occupied area.

### Prediction

A color selected from a small swatch will not maintain identical apparent
lightness, chroma, comfort, or dominance when expanded to a large surface.

### Supporting Evidence

- CLR-CMP-EVD-005

### Counter Evidence

The magnitude and direction of the effect vary by color and viewing condition.

### Confidence

Moderate to high

------------------------------------------------------------------------

# Open Questions

-   Which traditional color-theory rules survive when hue, lightness, chroma,
    area, and context are independently manipulated?
-   Can artistic studio knowledge be systematically encoded as case evidence
    without pretending it is controlled experimental evidence?
-   Which perceptual mechanisms transfer most reliably across UI, print,
    architecture, and film?
-   How should Atlas represent temporal color meaning built through narrative
    repetition?
-   Can semantic color conventions be modeled as learned probabilities?
-   How should ecological naturalness be separated from cultural familiarity?
-   What visual-angle thresholds materially alter perceived color appearance?
-   Which forms of redundant encoding reinforce one another and which create
    cue conflict?
-   Can compositional harmony be predicted as processing fluency, controlled
    contrast, or statistical familiarity?
-   What evidence would justify expanding Atlas beyond vision into audition,
    touch, and multisensory composition?

------------------------------------------------------------------------

# Next Actions

-   Build a controlled vocabulary for all uses of the term contrast.
-   Compare traditional harmony models against psychophysical harmony and
    preference research.
-   Compare CIELAB, CAM16-UCS, Oklab, and OKLCH by intended use rather than
    declaring one universally superior.
-   Create a painter-to-science comparison series beginning with Albers,
    Chevreul, Munsell, Itten, and modern appearance science.
-   Create an architecture-specific color track covering stimulus size,
    material, daylight, reflected color, and duration.
-   Create a cinematography track covering sequence, adaptation, recurrence,
    and narrative association.
-   Link the Color Genome to the existing Atlas mechanisms for grouping,
    hierarchy, crowding, similarity, and reinforced structure.
-   Begin populating the comparison schema with one record per major dispute.

------------------------------------------------------------------------

# Revision History

  Version   Date         Author                         Summary
  --------- ------------ ------------------------------ -----------------------------------------------
  0.1       2026-07-18   Kevin Miller and ChatGPT       Initial comparative framework, disciplinary matrix, revised Atlas architecture, observations, evidence records, and candidate laws.

------------------------------------------------------------------------

# Agent Instructions

When creating or modifying this document:

1.  Separate observation from interpretation.
2.  Never strengthen a conclusion beyond the available evidence.
3.  Preserve contradictory findings.
4.  Prefer measurable variables over subjective descriptions.
5.  Reference candidate laws and genome nodes whenever possible.
6.  Use stable IDs for observations, evidence, laws, experiments,
    metrics, and case studies.
7.  Record assumptions explicitly.
8.  Record confidence explicitly.
9.  Keep the YAML header valid.
10. Do not delete revision history; append to it.

This template is the standard for all Composition Science project
documents.
