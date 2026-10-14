---
id: RR-VE-COMP-2026-699F
slug: composition-science-research-library-v0-2-2
project: composition-science
purposes:
  - verify
  - reference
  - integrate
audiences:
  - practitioner
  - researcher
---

# Composition Science Research Library

**Version:** 0.2  
**Status:** Working evidence registry  
**Updated:** July 18, 2026

## Purpose

This library collects experimental evidence that may support, limit, revise, or reject proposed laws of composition.

The intended chain is:

> Published result → bounded finding → candidate law → practical design implication → later validation

This version begins the transition from a conceptual outline to a source-grounded evidence registry.

---

## Research Rules

1. **No principle without evidence.**
2. **Separate observation from interpretation.**
3. **Preserve experimental conditions.**
4. **Do not generalize beyond the population, stimulus, task, or setting studied.**
5. **Prefer relationships and conditional rules over arbitrary pixel prescriptions.**
6. **Record contradictory evidence rather than averaging it away.**
7. **Distinguish biological constraints, perceptual tendencies, learned conventions, and stylistic preferences.**
8. **A proposed law must make a testable prediction.**

---

# Evidence Confidence Scale

| Grade | Meaning |
|---|---|
| A | Meta-analysis, systematic review, or strong replicated evidence with usable quantitative results |
| B | Multiple controlled studies or a large, well-reported controlled study |
| C | Single controlled experiment with adequate reporting |
| D | Applied or observational evidence |
| E | Expert theory, historical analysis, or reasoned case study |
| F | Convention, anecdote, or unsupported assertion |

A high evidence grade does not automatically justify a broad design recommendation. Generalizability is assessed separately.

---

# Candidate Laws

## L-001: Law of Relative Separation

### Proposed relationship

Perceived separation depends more strongly on differences between within-group and between-group relationships than on an isolated absolute gap.

### Testable prediction

Holding element size, color, and alignment constant, grouping accuracy should improve as the ratio of between-group spacing to within-group spacing increases.

### Current status

**Provisional.** Supported conceptually by Gestalt proximity research, but a design-usable spacing-ratio threshold has not yet been extracted.

---

## L-002: Law of Peripheral Separation Cost

### Proposed relationship

The separation required for reliable object recognition increases as an object moves farther from fixation.

### Testable prediction

At larger eccentricities, otherwise-identical targets will require larger center-to-center separation from surrounding elements to maintain recognition accuracy.

### Current status

**Strongly supported within visual-crowding paradigms.**

### Important boundary

This is a law about recognition under crowding, not a direct CSS spacing formula.

---

## L-003: Law of Conditional Grouping Benefit

### Proposed relationship

Grouping can reduce perceptual or memory burden when the grouping corresponds to the current task, but can impair performance when it emphasizes irrelevant relationships.

### Testable prediction

Task-relevant grouped elements should improve performance relative to ungrouped displays, while task-irrelevant grouping may bias attention and increase errors.

### Current status

**Moderately supported.**

---

## L-004: Law of Search Competition

### Proposed relationship

Search cost increases when the target is not distinguished by an efficiently processed feature and must compete with increasing numbers of similar distractors.

### Testable prediction

Reaction time should remain comparatively stable across set size for efficient feature searches, but rise with set size for less efficient searches.

### Current status

**Strongly supported in visual-search research, with important qualifications.**

---

## L-005: Law of Structural Encoding

### Proposed relationship

Visual organization affects what is encoded into working memory, not merely how a display appears.

### Testable prediction

Displays with usable Gestalt organization should change later recall or change-detection performance even when the individual visual items remain the same.

### Current status

**Moderately supported.**

---

## L-006: Law of Cue Competition

### Proposed relationship

Grouping cues can reinforce or compete with one another. No cue should be treated as independently deterministic.

### Testable prediction

When proximity, similarity, connectedness, or enclosure indicate conflicting organizations, grouping judgments and performance should depend on cue strength, timing, and task.

### Current status

**Supported at a conceptual level; quantitative synthesis remains incomplete.**

---

# Evidence Registry

## VS-001 — Crowding distance scales with eccentricity

### Citation

Kurzawski, J. W., Burchell, A., Thapa, D., et al. (2023). *The Bouma law accounts for crowding in 50 observers*. Journal of Vision, 23(8):6.  
DOI: `10.1167/jov.23.8.6`

### Evidence grade

**B**

### Study scope

- 50 observers
- 13 crowding distances measured twice per observer
- Eccentricities: 0°, 5°, and 10°
- Four cardinal meridians
- Radial and tangential crowding orientations
- Sloan and Pelli fonts
- Foveal acuity also measured twice

### Quantitative result

- A two-parameter Bouma model explained **82%** of variance in the 13 × 50 log crowding-distance measurements using cross-validation.
- An enhanced model including meridian, orientation, target kind, and observer explained **94%**.
- Reported crowding-distance ratios included:
  - horizontal : vertical = **0.62**
  - lower : upper = **0.79**
  - right : left = **0.78**
  - tangential : radial = **0.55**
  - Sloan font : Pelli font = **0.78**

### Bounded finding

Critical spacing grows systematically with eccentricity, but location, orientation, target type, and observer materially modify the result.

### Supports

- L-002: Law of Peripheral Separation Cost
- L-006: Law of Cue Competition, indirectly through moderator effects

### Does not establish

- A universal coefficient for all objects
- A direct pixel-spacing recommendation
- That crowding is identical for reading, icons, controls, faces, and arbitrary interface objects

### Design implication

Small, tightly clustered interface signals placed away from the normal fixation path may become difficult to identify even when each signal is individually large enough to see. Peripheral importance requires greater isolation, stronger differentiation, relocation into the gaze path, or an intentional eye movement.

### Source

https://doi.org/10.1167/jov.23.8.6

---

## VS-002 — Crowding limits recognition rather than simple detection

### Citation

Pelli, D. G., & Tillman, K. A. (2008). *The uncrowded window of object recognition*. Nature Neuroscience, 11, 1129–1135.  
Related open review: *Crowding: A cortical constraint on object recognition.*

### Evidence grade

**A/B conceptual synthesis**

### Bounded finding

Objects that can be detected and identified in isolation may become unrecognizable when nearby flankers fall within a critical spacing region. The spacing is commonly described center-to-center and varies by visual-field location and direction.

### Supports

- L-002: Law of Peripheral Separation Cost

### Important distinction

Visibility is not equivalent to recognizability.

An icon, label, or status indicator may be technically visible while its identity is unreliable in clutter.

### Design implication

Minimum contrast and font-size rules are insufficient by themselves. Critical information also needs adequate isolation from competing forms.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC3624758/

---

## VS-003 — Crowding and eccentricity constrain reading rate

### Citation

Pelli, D. G., Tillman, K. A., Freeman, J., Su, M., Berger, T. D., & Majaj, N. J. (2007). *Crowding and eccentricity determine reading rate*. Journal of Vision, 7(2):20.  
DOI: `10.1167/7.2.20`

### Evidence grade

**B**

### Bounded finding

Reading is constrained by the number of letters that can be recognized during a fixation. Crowding helps explain why high-detail reading depends on repeated eye movements rather than a continuous high-resolution sweep across text.

### Supports

- L-002: Law of Peripheral Separation Cost
- The broader claim that a whole page is not available in equal detail simultaneously

### Design implication

A page should support a sequence of useful fixations. Peripheral layout can indicate structure and destination, but detailed comprehension requires bringing content into central inspection.

### Source

https://doi.org/10.1167/7.2.20

---

## GP-001 — Gestalt similarity can improve visual working memory

### Citation

Peterson, D. J., & Berryhill, M. E. (2013). *The Gestalt principle of similarity benefits visual working memory*. Psychonomic Bulletin & Review, 20, 1282–1289.

### Evidence grade

**C**

### Experimental theme

The study tested whether similarity-based grouping influences visual working-memory performance.

### Bounded finding

Grouping by visual similarity can improve working-memory performance under tested conditions. The benefit indicates that organization affects storage or effective encoding, not merely subjective appearance.

### Supports

- L-003: Law of Conditional Grouping Benefit
- L-005: Law of Structural Encoding

### Does not establish

- That all similarity improves memory
- That decorative consistency always reduces cognitive load
- That similarity should override task-relevant distinctions

### Design implication

Consistent visual treatment can help users encode related information as a unit, but only when the similarity represents a meaningful relationship.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC3806891/

---

## GP-002 — Grouping can help relevant items and hinder irrelevant ones

### Citation

Prieto, A., et al. (2022). *Does perceptual grouping improve visuospatial working memory?*

### Evidence grade

**C**

### Experimental structure

- Experiment 1 tested effects of similarity grouping on change detection.
- Experiment 2 examined whether explicit task instructions could override the grouping bias.

### Bounded result

Similarity grouping improved change-detection accuracy for probes that had been grouped, but hindered detection for non-grouped probes in some conditions. Explicit instructions to ignore irrelevant grouped items could override the automatic bias.

### Supports

- L-003: Law of Conditional Grouping Benefit
- L-005: Law of Structural Encoding
- L-006: Law of Cue Competition

### Strong implication

Grouping is not free cognitive compression. It prioritizes some relationships over others.

### Design implication

A border, shared background, repeated color, or proximity pattern is an assertion about what belongs together. Incorrect grouping can make the interface worse even if it appears orderly.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC9090850/

---

## GP-003 — Proximity may receive a temporal advantage over similarity

### Citation

Johansson, R. C. G., et al. (2024). *Serial processing of proximity groups and similarity groups in perceptual organization.*

### Evidence grade

**C**

### Experimental structure

- Experiment 1 compared grouping judgments produced by proximity and contrast similarity.
- Experiment 2 used redundant signals to investigate the processing architecture.

### Bounded finding

The experiments indicated sequential organization of grouping operations and a temporal processing advantage for proximity grouping under the tested conditions.

### Supports

- L-001: Law of Relative Separation
- L-006: Law of Cue Competition

### Design implication

Spatial relationship may be interpreted earlier than some similarity cues. Color or style should not be expected to repair a spatial organization that communicates the wrong grouping.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC11093805/

---

## AT-001 — Search cost depends on the target–distractor relationship

### Citation

Wolfe, J. M., Palmer, E. M., & Horowitz, T. S. (2010). *Reaction time distributions constrain models of visual search*. Vision Research, 50(14), 1304–1311.

### Evidence grade

**B**

### Dataset

Approximately **112,000 trials** across:

- efficient color-feature search
- inefficient search for a `2` among `5`s
- intermediate color × orientation conjunction search

### Bounded finding

Different target–distractor relationships produce meaningfully different reaction-time distributions. Mean reaction time alone is insufficient to explain search behavior.

### Supports

- L-004: Law of Search Competition

### Design implication

Making a target “different” is not a binary condition. The feature used, distractor similarity, and number of competing items determine whether search remains efficient.

### Source

https://pubmed.ncbi.nlm.nih.gov/19895828/

---

## AT-002 — Global organization can capture attention

### Citation

Marini, F., et al. (2016). *Gestalt perceptual organization of visual stimuli captures attention.*

### Evidence grade

**C**

### Bounded finding

Global configurations formed through perceptual organization can capture attention even when top-down attention is directed toward local elements.

### Supports

- L-005: Law of Structural Encoding
- A future candidate law concerning global-before-local attentional effects

### Design implication

The page-level composition is not a neutral container around components. Large-scale grouping and shape can compete with or redirect attention away from local controls.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC5005981/

---

# Initial Cross-Study Observations

## OBS-001 — Separation is a relationship, not a token

The research reviewed so far does not support a universal spacing token that guarantees visual separation. Separation depends on:

- position relative to fixation
- target–flanker similarity
- orientation
- visual-field meridian
- task
- attentional state
- grouping cues
- observer differences

**Confidence:** High

---

## OBS-002 — Visibility, recognition, grouping, and comprehension are different thresholds

A target may be:

1. detectable,
2. identifiable,
3. correctly grouped,
4. correctly interpreted,
5. remembered,

and fail at any later stage despite passing an earlier one.

**Confidence:** High

---

## OBS-003 — Grouping changes processing priorities

Grouping does not merely make a layout look organized. It can improve encoding of grouped information while impairing access to information excluded from that grouping.

**Confidence:** Moderate to high

---

## OBS-004 — Peripheral design should communicate structure before detail

Because detailed recognition deteriorates away from fixation, peripheral interface regions are better suited to:

- large state changes
- strong landmarks
- broad grouping
- motion or salient alerts
- navigational structure

They are less reliable for:

- tightly packed labels
- subtle status differences
- small badges
- dense icon clusters

**Confidence:** Moderate to high

---

## OBS-005 — More visual consistency is not always better

Similarity can support grouping, but excessive similarity can make a search target harder to distinguish. A successful composition must balance:

- grouping within a meaningful unit
- differentiation between competing units
- emphasis for the current task

**Confidence:** Moderate

---

# Practical Translation Framework

Every scientific finding should be translated through four stages.

## 1. Laboratory claim

What was directly demonstrated?

## 2. Perceptual mechanism

What process might explain it?

## 3. Composition principle

What conditional relationship may transfer across domains?

## 4. Implementation hypothesis

What UI treatment should be tested?

Example:

```yaml
laboratory_claim:
  Critical spacing rises with eccentricity.

composition_principle:
  Peripheral recognition requires greater isolation.

implementation_hypothesis:
  Status signals positioned outside the dominant gaze path should
  use greater spacing, size, or contrast than centrally inspected labels.

validation_needed:
  Compare dashboard error rates using centrally and peripherally placed
  status indicators under controlled spacing conditions.
```

---

# Immediate Research Backlog

## Track A — Perceptual Separation

1. Extract coefficients and moderators from major crowding studies.
2. Separate foveal crowding from peripheral crowding.
3. Compare letters, symbols, faces, and interface-like stimuli.
4. Record center-to-center spacing rather than edge-only spacing.
5. Translate visual angle into device-specific examples without turning them into universal rules.

## Track B — Grouping Strength

1. Find experiments that directly compare proximity ratios.
2. Extract conditions where similarity overrides proximity.
3. Compare common region, connectedness, and enclosure.
4. Investigate cue conflict and cue redundancy.
5. Identify effects on accuracy, reaction time, memory, and confidence.

## Track C — Search and Hierarchy

1. Extract visual-search slopes by target and distractor type.
2. Study how salience changes first fixation.
3. Compare local target salience with global compositional dominance.
4. Investigate expertise and learned search strategies.
5. Connect visual hierarchy to task completion rather than preference alone.

---

# Research Extraction Template

## Identifier

## Citation

## Evidence grade

## Research question

## Participants

## Stimuli

## Task

## Viewing conditions

## Independent variables

## Dependent variables

## Quantitative results

## Authors' interpretation

## Bounded finding

## Candidate laws supported

## Candidate laws challenged

## Generalizability limits

## Composition implication

## UI hypothesis

## Replication status

## Source

---

# Change Log

## Version 0.2

- Added six candidate laws.
- Added eight source-grounded evidence records.
- Added quantitative crowding results from a 50-observer study.
- Added distinctions among visibility, recognition, grouping, comprehension, and memory.
- Added the conditional nature of grouping benefits.
- Added an immediate research backlog.
- Reframed design guidance as implementation hypotheses requiring later validation.
