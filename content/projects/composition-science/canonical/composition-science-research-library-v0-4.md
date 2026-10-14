---
id: RR-VE-COMP-2026-0978
slug: composition-science-research-library-v0-4
project: composition-science
purposes:
  - verify
  - reference
  - integrate
audiences:
  - practitioner
  - researcher
entryPoint: true
entryPointOrder: 20
entryPointLabel: Evidence library
---

# Composition Science Research Library

**Version:** 0.4  
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


---

# Perceptual Separation Synthesis

## Why the first model must be conditional

The research collected so far rejects a simple rule such as:

> Elements separated by a particular number of pixels will be perceived as distinct.

At least four separate thresholds are involved:

1. **Detection** — Is a visual change present?
2. **Identification** — What object or symbol is it?
3. **Segmentation** — Which surrounding elements belong with it?
4. **Interpretation** — What does the resulting group mean?

A layout can pass the detection threshold while failing identification or segmentation. This distinction is essential for interface design because many current guidelines concentrate on size and contrast while ignoring the interference produced by nearby objects.

---

## PS-001 — Candidate Perceptual Separation Function

A provisional model:

```text
Reliable separation =
    spatial distance
  × spacing contrast
  × feature difference
  × boundary reinforcement
  × attentional support
  ÷ peripheral cost
  ÷ distractor competition
```

A more formal placeholder:

```text
Sᵣ = f(D, R, F, B, A, E, N, O)
```

Where:

- `D` = center-to-center distance
- `R` = ratio of between-group spacing to within-group spacing
- `F` = target–neighbor feature difference
- `B` = boundary cues such as enclosure, connectedness, or background
- `A` = attentional support
- `E` = eccentricity from fixation
- `N` = number and similarity of competing neighbors
- `O` = orientation and visual-field location

This is not yet a fitted equation. It is a causal inventory defining what our evidence model must preserve.

---

# New Candidate Laws

## L-007: Law of Recognition Beyond Visibility

### Proposed relationship

An element can be visible without being reliably identifiable.

### Testable prediction

Participants may accurately report that a peripheral target is present while failing to report its identity when similar flankers fall within critical spacing.

### Evidence status

**Strongly supported by crowding research.**

### Design consequence

Compliance with minimum size and contrast requirements does not guarantee that a control, badge, or status symbol is usable within a crowded composition.

---

## L-008: Law of Similarity-Weighted Interference

### Proposed relationship

Nearby objects interfere more strongly when they share relevant visual features with the target.

### Testable prediction

At equal spacing and eccentricity, target identification should improve as target–flanker similarity decreases, subject to task and feature dimensions.

### Evidence status

**Moderately to strongly supported.**

### Design consequence

Dissimilarity can function as separation. However, it should be used selectively because excessive differentiation can destroy meaningful grouping and increase visual noise.

---

## L-009: Law of Directional Crowding

### Proposed relationship

Critical separation varies by direction and visual-field location rather than forming a perfectly uniform radius around an object.

### Testable prediction

The same target and spacing will produce different recognition accuracy depending on radial versus tangential arrangement and visual-field meridian.

### Evidence status

**Strongly supported.**

### Design consequence

A single circular “safe zone” around an interface element is an approximation. Horizontal, vertical, radial, and tangential arrangements may not impose equal perceptual cost.

---

## L-010: Law of Attentional Relief

### Proposed relationship

Attention can reduce some crowding effects, but attentional intent does not uniformly eliminate critical-spacing limits.

### Testable prediction

Exogenous or involuntary cues to target location may reduce measured critical spacing under some conditions, while voluntary attention may improve performance without producing the same spacing reduction.

### Evidence status

**Moderately supported.**

### Design consequence

Making users search harder or “pay attention” is not a substitute for adequate composition. Salient cues may help, but they do not erase the biological cost of dense surrounding structure.

---

## L-011: Law of Temporal Cue Priority

### Proposed relationship

Grouping cues are not necessarily processed simultaneously. Proximity can dominate early organization, while similarity can exert greater influence with additional processing time.

### Testable prediction

Under brief exposure, proximity should determine grouping more often than conflicting similarity cues; with longer processing, similarity may gain influence.

### Evidence status

**Moderately supported.**

### Design consequence

A spatial structure that communicates the wrong grouping cannot safely be repaired only through color or shape similarity, especially in rapidly scanned interfaces.

---

## L-012: Law of Reinforced Structure

### Proposed relationship

Multiple compatible grouping cues can communicate structure more reliably than an isolated weak cue, but reinforcement is useful only when the cues encode the same intended relationship.

### Testable prediction

Compatible combinations of proximity, alignment, common region, connectivity, and color similarity should improve recovery of intended group structure relative to weaker isolated cues.

### Evidence status

**Moderately supported in applied grouping research.**

### Design consequence

A section boundary can be strengthened through a coordinated combination of spacing, alignment, heading treatment, and enclosure. Using all possible cues at maximum strength is unnecessary and may fragment the page.

---

# Additional Evidence Records

## VS-004 — Critical spacing is not universally one-half eccentricity

### Citation

Coates, D. R., Chin, J. M., & Chung, S. T. L. (2021). *The generality of the critical spacing for crowded optotypes.*

### Evidence grade

**B**

### Bounded finding

The familiar approximation that critical spacing is one-half of target eccentricity is a useful historical rule of thumb, not a universal constant. Even within optotype research, estimates vary with method, stimulus, and observer.

### Supports

- L-002: Peripheral Separation Cost
- L-009: Directional Crowding
- The research rule against universal pixel conversion

### Challenges

Any implementation rule that directly converts `0.5 × eccentricity` into a mandatory interface spacing value.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC8556556/

---

## VS-005 — Similarity and flanker complexity moderate crowding

### Citation

Bernard, J.-B., & Chung, S. T. L. (2011). *The dependence of crowding on flanker complexity and target–flanker similarity.*

### Evidence grade

**C**

### Research focus

Identification of crowded letters while manipulating:

- spatial complexity of flankers
- similarity between targets and flankers

### Bounded finding

Crowding strength is affected by the relationship between target and flankers, not only by geometric spacing. Similarity and complexity can alter identification performance.

### Supports

- L-008: Similarity-Weighted Interference
- PS-001 candidate function

### Design implication

Repeated controls with nearly identical visual form can interfere with rapid peripheral recognition. The system needs enough within-group consistency for recognition, but enough task-relevant differentiation to locate exceptional or urgent states.

### Source

https://jov.arvojournals.org/article.aspx?articleid=2120999

---

## VS-006 — Topological difference can alleviate crowding

### Citation

Xi, H., et al. (2020). *Topological difference between target and flankers alleviates crowding.*

### Evidence grade

**C**

### Bounded finding

Target–flanker differences in global or topological structure can reduce crowding, reinforcing the conclusion that feature relationship modifies interference.

### Supports

- L-008: Similarity-Weighted Interference

### Design implication

Shape difference may carry stronger recognition value than minor decorative differences. A critical status icon that differs only by subtle tone may remain crowded, while a meaningfully different silhouette may be more robust.

### Source

https://jov.arvojournals.org/article.aspx?articleid=2770795

---

## VS-007 — Involuntary attention can reduce critical spacing

### Citation

Bowen, J. D., et al. (2023). *Effects of involuntary and voluntary attention on critical spacing in crowding.*

### Evidence grade

**C**

### Bounded result

Directing involuntary attention toward the target reduced critical spacing relative to directing it elsewhere. Voluntary attention did not produce the same significant critical-spacing effect. Effects on response time and spacing were not strongly correlated across participants.

### Supports

- L-010: Attentional Relief

### Design implication

Animation, sudden onset, or another exogenous cue may temporarily help isolate an urgent signal. This is not equivalent to making an ordinary dense layout permanently legible.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC9987171/

---

## GP-004 — Proximity precedes similarity under cue conflict

### Citation

Ben-Av, M. B., Sagi, D., & Braun, J. (1992/1995 indexing). *Perceptual grouping by similarity and proximity.*

### Evidence grade

**C**

### Experimental structure

Observers judged horizontal or vertical organization while proximity and similarity supplied conflicting grouping cues.

### Bounded result

Proximity grouping was perceived faster. With increasing processing time, similarity could dominate the final grouping judgment.

### Supports

- L-011: Temporal Cue Priority
- L-001: Relative Separation

### Design implication

Spacing communicates before users have fully interpreted color, shape, or semantic labels. It is therefore one of the earliest structural claims made by a composition.

### Source

https://pubmed.ncbi.nlm.nih.gov/7740775/

---

## HCI-001 — Multiple grouping cues can communicate intended structure

### Citation

Bae, J., et al. (2014/2015 indexing). *Reinforcing visual grouping cues to communicate complex informational structure.*

### Evidence grade

**C/D**

### Cues examined

- proximity
- color similarity
- common region
- connectivity
- alignment

### Method

A digital card-sorting approach was used to measure how effectively combinations of grouping cues communicated intended organization in real-world text and imagery.

### Bounded finding

Grouping cues can be combined to reinforce structure. Their effectiveness depends on cue selection and the intended organization rather than on simply adding more decoration.

### Supports

- L-012: Reinforced Structure
- L-006: Cue Competition

### Design implication

Composition should deliberately encode hierarchy using compatible cues. A design token system should represent relationships such as group, subgroup, exception, and boundary rather than only isolated spacing values.

### Source

https://pubmed.ncbi.nlm.nih.gov/26356911/

---

# Visual Angle Translation Reference

Pixels are not a biological unit. The same visual angle corresponds to different physical sizes at different viewing distances.

The table below converts angular size into approximate physical millimeters:

| Viewing distance | 0.1° | 0.25° | 0.5° | 1° | 2.5° |
|---:|---:|---:|---:|---:|---:|
| 12 in | 0.5 mm | 1.3 mm | 2.7 mm | 5.3 mm | 13.3 mm |
| 18 in | 0.8 mm | 2.0 mm | 4.0 mm | 8.0 mm | 20.0 mm |
| 24 in | 1.1 mm | 2.7 mm | 5.3 mm | 10.6 mm | 26.6 mm |
| 30 in | 1.3 mm | 3.3 mm | 6.6 mm | 13.3 mm | 33.3 mm |
| 36 in | 1.6 mm | 4.0 mm | 8.0 mm | 16.0 mm | 39.9 mm |

### Interpretation

At a 24-inch viewing distance:

- `0.1°` subtends approximately **1.1 mm**
- `0.5°` subtends approximately **5.3 mm**
- `1°` subtends approximately **10.6 mm**
- `2.5°` subtends approximately **26.6 mm**

These values describe apparent size or separation, not recommended UI dimensions.

### Formula

```text
physical size = 2 × viewing distance × tan(visual angle ÷ 2)
```

### Required implementation inputs

To translate a perceptual result into pixels, we would need:

- estimated viewing distance
- physical display dimensions
- display pixel dimensions
- device pixel ratio
- expected gaze location
- task and exposure duration

Without those inputs, a pixel value is only a convention.

---

# Emerging Composition Model

## Three spatial scales

### 1. Detail scale

Supports:

- letter identification
- icon recognition
- small status differences
- precise interaction

Primary constraints:

- acuity
- contrast
- crowding
- target size

### 2. Group scale

Supports:

- form-field units
- cards
- rows
- navigation clusters
- chart legends

Primary constraints:

- proximity
- similarity
- alignment
- enclosure
- connectedness

### 3. Page scale

Supports:

- dominant regions
- reading path
- balance
- global hierarchy
- page-level rhythm

Primary constraints:

- attentional capture
- global organization
- task sequence
- viewport and scrolling
- learned reading conventions

### Implication

A composition can succeed at one scale and fail at another.

For example:

- every icon may be identifiable at detail scale,
- controls may be ambiguously grouped at group scale,
- and the overall page may lack a dominant reading path at page scale.

A future composition score must therefore report scale-specific failures rather than one overall aesthetic number.

---

# First Proposed Design Diagnostic

## Separation Diagnostic v0.1

For every important target, record:

```yaml
target:
  role:
  expected_fixation:
  eccentricity_estimate:
  size:
  contrast:

neighbors:
  count:
  nearest_center_distance:
  feature_similarity:
  semantic_similarity:

grouping:
  within_group_gap:
  between_group_gap:
  spacing_ratio:
  alignment:
  common_region:
  connectedness:
  color_similarity:

task:
  detect:
  identify:
  compare:
  remember:
  act:

risk:
  peripheral_crowding:
  ambiguous_grouping:
  search_competition:
  false_emphasis:
```

This diagnostic does not yet produce a valid numeric score. Its purpose is to prevent us from collapsing different perceptual problems into the word “clutter.”

---

# Updated Research Priorities

## Priority 1 — Extract usable proximity functions

We still need experiments reporting:

- exact spacing values
- element dimensions
- exposure times
- grouping-choice percentages
- proximity–similarity conflicts
- response-time changes

The goal is to determine whether practical spacing-ratio bands can be justified.

## Priority 2 — Build a target–flanker similarity taxonomy

Candidate dimensions:

- luminance
- color
- orientation
- spatial frequency
- shape
- topology
- semantic category
- motion

The goal is to identify which differences most reliably preserve recognition in dense interfaces.

## Priority 3 — Translate crowding into realistic interface tasks

Laboratory optotypes are foundational but not sufficient. We need studies involving:

- icons
- text labels
- menus
- status indicators
- charts
- dashboards
- touch controls
- mixed text-and-symbol displays

## Priority 4 — Age and visual impairment

The framework must eventually distinguish baseline recommendations from inclusive recommendations for:

- aging vision
- low vision
- amblyopia
- dyslexia and reading differences
- motor or attentional limitations

---

# Change Log

## Version 0.3

- Added a conditional perceptual-separation model.
- Added six candidate laws concerning recognition, similarity, direction, attention, processing order, and cue reinforcement.
- Added five evidence records.
- Added visual-angle conversion data across common viewing distances.
- Introduced detail, group, and page scales.
- Added the first structured separation diagnostic.
- Identified the need for realistic UI-stimulus research before issuing implementation standards.


---

# Quantifying Proximity

## Objective

This research round asked a narrow question:

> Can published experiments justify practical weak, moderate, and strong spacing-ratio bands for visual grouping?

## Current answer

**Not yet as universal thresholds.**

The strongest quantitative tradition does not describe proximity grouping as an abrupt category boundary. It describes the relative probability of alternative organizations as a smooth function of relative distance.

This is a more powerful result than a fixed threshold, but it changes what our design system should attempt to model.

---

# Core Finding: Proximity Is Probabilistic

## The dot-lattice paradigm

Kubovy, Wagemans, Holcombe, and later researchers used regular dot lattices that could be seen as parallel rows in more than one orientation.

The method typically:

1. presents a lattice briefly,
2. varies the relative distances between dots along possible grouping directions,
3. asks observers which orientation they perceived,
4. models the probability of each reported organization.

The 1998 reanalysis reported that the **relative strength of grouping decreases approximately exponentially with relative inter-dot distance**.

This finding was robust across:

- different lattice configurations,
- spatial scale transformations,
- presentation-time transformations,
- and grouping of already-grouped dot pairs.

The configural properties tested, including angular separation and pattern symmetry, did not explain the result as strongly as relative distance.

### Primary source

Kubovy, M., Holcombe, A. O., & Wagemans, J. (1998). *On the lawfulness of grouping by proximity*. Cognitive Psychology, 35(1), 71–98.

https://doi.org/10.1006/cogp.1997.0673

Supporting institutional record:

https://lirias.kuleuven.be/209847

---

# The Pure Distance Law

## Conceptual form

For a possible organization along vector `v`, grouping attraction is modeled as decreasing exponentially with relative distance:

```text
attraction(v) ∝ exp(-α × relative_distance(v))
```

The probability of perceiving an organization is its attraction relative to the attraction of all available organizations:

```text
P(v) = attraction(v) / Σ attraction(all candidate directions)
```

Where:

- `relative_distance(v)` is distance along a possible grouping direction relative to a reference or shortest distance,
- `α` is an attraction or distance-sensitivity parameter,
- larger `α` means a stronger preference for the nearest organization.

This means grouping depends on **competition among plausible organizations**, not merely the isolated distance between two objects.

## Equivalent two-choice interpretation

For two competing orientations with distances `a` and `b`, choice probability can be represented in logistic form:

```text
log odds(group by a rather than b)
    ∝ α × relative distance difference
```

The exact parameter is observer- and condition-dependent.

## Consequence

There is no universal ratio such as:

```text
between-gap / within-gap ≥ 2
```

that guarantees grouping for every observer, task, stimulus, and environment.

Instead:

> Increasing the relative distance of one organization continuously reduces its probability relative to competing organizations.

---

# New Candidate Law

## L-013: Law of Probabilistic Proximity

### Proposed relationship

Grouping strength changes continuously with relative spatial distance and with the strengths of competing organizations.

### Prediction

As the distance along one possible organization increases relative to another, the probability of perceiving the longer-distance organization should decline approximately monotonically and often exponentially.

### Evidence status

**Strong within regular dot-lattice paradigms.**

### Boundary

The functional form has not yet been validated as a universal model for cards, form fields, typography, or mixed interface objects.

---

# New Evidence Records

## GP-005 — Relative grouping strength follows a decreasing exponential

### Citation

Kubovy, M., Holcombe, A. O., & Wagemans, J. (1998). *On the lawfulness of grouping by proximity*. Cognitive Psychology, 35(1), 71–98.

### Evidence grade

**B**

### Experimental lineage

The authors reanalyzed multistable dot-lattice data and tested the quantitative relationship between inter-dot distance and reported grouping orientation.

### Quantitative form

Relative grouping strength was approximated by a decreasing exponential function of relative distance.

### Robustness reported

- spatial scale transformation: retained
- temporal scale transformation: retained
- grouping of paired dots as higher-order units: retained
- pattern symmetry and angular separation: comparatively unimportant in the tested configurations

### Supports

- L-001: Relative Separation
- L-013: Probabilistic Proximity

### Challenges

- fixed universal spacing thresholds
- explanations based only on overall pattern symmetry
- treating proximity as an all-or-nothing law

### Composition implication

A layout should be evaluated by the relative strength of all plausible groupings. Increasing a section gap weakens competing cross-section organizations gradually rather than activating a categorical separator.

### Source

https://doi.org/10.1006/cogp.1997.0673

---

## GP-006 — Distance ratios from 1.0 to 1.5 systematically alter grouping

### Citation

Claessens, P. M. E., & Wagemans, J. (2005). *Perceptual grouping in Gabor lattices: Proximity and alignment*. Perception & Psychophysics.

### Evidence grade

**C**

### Experimental geometry

The lattice space included five levels of the distance ratio:

```text
|b| / |a| = 1.0 through 1.5
```

with three inter-vector angle levels per distance ratio.

### Exposure tradition

Related lattice experiments commonly presented stimuli for approximately **300 ms** before observers reported the perceived orientation.

### Findings

- Distance ratio had a highly significant effect within each observer.
- Alignment also had a highly significant effect.
- Both were reported at `p < .0001`.
- Proximity was the stronger of the two principal effects.
- Alignment still modified grouping and therefore cannot be ignored.

### Supports

- L-013: Probabilistic Proximity
- L-006: Cue Competition
- L-012: Reinforced Structure

### Composition implication

A 1.0–1.5 ratio manipulation is perceptually consequential in controlled lattice displays, but it cannot be directly relabeled as a UI rule such as “1.5 means strong grouping.”

### Source

https://doi.org/10.3758/BF03193649

---

## GP-007 — Central and peripheral grouping require different models

### Citation

Bleumers, L., De Graef, P., Verfaillie, K., & Wagemans, J. (2008). *Eccentric grouping by proximity in multistable dot lattices*. Vision Research, 48(4), 495–504.

### Evidence grade

**C**

### Conditions

Dot lattices appeared:

- centrally,
- to the right of fixation with the closest border at **3°**,
- or to the right of fixation with the closest border at **15°**.

### Finding

The Pure Distance Law adequately predicted central grouping but did not capture peripheral responses as well, even when peripheral lattices were scaled.

A model allowing occasional random responses fit the eccentric data better. One proposed explanation was failure to shift attention successfully from fixation to the peripheral pattern.

The effect of relative inter-dot distance could be stronger in peripheral vision at larger scales, but the difference disappeared at the smallest tested scale.

### Supports

- L-002: Peripheral Separation Cost
- L-010: Attentional Relief
- L-013: Probabilistic Proximity

### Challenges

A single proximity curve applied uniformly across the viewport.

### Composition implication

The same spacing ratio may not communicate grouping equally at the center and edge of a display. Expected fixation location belongs in the layout model.

### Source

https://doi.org/10.1016/j.visres.2007.11.016

---

## GP-008 — Cardinal orientation alters proximity grouping

### Citation

Claessens, P. M. E., & Wagemans, J. (2008/2009). *Proximity, collinearity, and orientation priors in zigzag lattices*.

### Evidence grade

**C**

### Bounded finding

Grouping along cardinal axes was less affected by distance but more affected by discollinearity than grouping along oblique orientations.

### Supports

- L-009: Directional Crowding
- L-006: Cue Competition
- L-013: Probabilistic Proximity

### Design implication

Horizontal, vertical, and oblique arrangements cannot be assumed to have identical grouping strength. Learned reading structure and intrinsic orientation biases may coexist with low-level proximity effects.

### Source

https://pubmed.ncbi.nlm.nih.gov/19146265/

---

## GP-009 — Proximity and similarity can combine additively

### Citation

Kubovy, M., & van den Berg, M. (2008). *The whole is equal to the sum of its parts: A probabilistic model of grouping by proximity and similarity in regular patterns*. Psychological Review, 115(1), 131–154.

### Evidence grade

**B**

### Bounded finding

A probabilistic model accounted for combined grouping by proximity and similarity in regular patterns. Attraction functions were approximately linear in a transformed space, supporting quantitative integration rather than a winner-takes-all account.

### Supports

- L-006: Cue Competition
- L-012: Reinforced Structure
- L-013: Probabilistic Proximity

### Design implication

Spacing and similarity can be treated as partially separable evidence for organization. This supports a future model in which spatial, color, shape, and enclosure cues contribute weighted evidence to a grouping prediction.

### Source

https://doi.org/10.1037/0033-295X.115.1.131

---

# Why We Cannot Publish Universal Ratio Bands Yet

## 1. The fitted sensitivity parameter varies

The attraction constant `α` represents sensitivity to proximity. It varies among observers and conditions.

A ratio cannot be interpreted without the curve parameter.

## 2. Grouping is competitive

A gap does not possess an independent grouping strength. Its effect depends on all other plausible distances and cues.

## 3. Orientation matters

Vertical, horizontal, radial, tangential, cardinal, and oblique organizations may receive different weights.

## 4. Peripheral presentation changes performance

A model that works centrally may require attention-failure or other terms in peripheral vision.

## 5. Stimulus class matters

The evidence comes mainly from:

- identical dots,
- Gabor patches,
- regular lattices,
- brief forced-choice judgments.

A form, dashboard, article, photograph, or navigation bar contains semantic and learned structure absent from these experiments.

## 6. The practical outcome differs

Laboratory grouping reports do not directly measure:

- task completion,
- comprehension,
- error recovery,
- clicking behavior,
- reading order,
- long-duration use.

---

# Provisional Strength Language

Until interface-specific calibration exists, use qualitative language tied to measurable comparisons rather than pretending to have validated bands.

## Ambiguous proximity

Two or more organizations have similar spatial support.

```text
within-group and competing gaps are approximately equal
```

Expected result:

- multistability,
- inconsistent grouping,
- heavier reliance on alignment, similarity, enclosure, or semantics.

## Biased proximity

One organization has a shorter repeated distance than its competitors.

```text
preferred gap < competing gap
```

Expected result:

- increased probability of the shorter-distance grouping,
- but not guaranteed grouping.

## Strongly biased proximity

One organization is substantially shorter and repeated consistently while other cues agree.

Expected result:

- high probability of the intended grouping,
- subject to orientation, eccentricity, stimulus type, and observer.

These are **descriptive categories**, not validated numerical bands.

---

# A Better Metric Than “Spacing Ratio”

## Proximity evidence

For candidate group `G`:

```text
PE(G) = -α × normalized_distance(G)
```

## Cue evidence

```text
CE(G) =
    wₚ × proximity
  + wₛ × similarity
  + wₐ × alignment
  + wᵣ × common_region
  + w𝚌 × connectedness
  + wₘ × motion
  + wₑ × semantic_expectation
```

## Predicted organization

```text
P(G) = exp(CE(G)) / Σ exp(CE(all candidate groups))
```

This softmax-style representation is provisional, but it captures the key empirical result:

> Grouping is a probability distribution over competing organizations.

No weights have yet been validated for interface composition.

---

# Layout Measurement Protocol v0.1

For a repeated layout, measure:

```yaml
geometry:
  element_width:
  element_height:
  center_to_center_within:
  edge_to_edge_within:
  center_to_center_between:
  edge_to_edge_between:
  horizontal_ratio:
  vertical_ratio:

competition:
  plausible_row_grouping:
  plausible_column_grouping:
  plausible_section_grouping:
  cross_boundary_alignment:

reinforcement:
  color_similarity:
  shape_similarity:
  typography_similarity:
  common_region:
  connectedness:
  heading_association:
  semantic_relationship:

viewing:
  expected_fixation:
  eccentricity:
  viewing_distance:
  display_density:
  exposure_duration:

outcome:
  reported_grouping:
  grouping_confidence:
  first_fixation:
  task_time:
  errors:
```

Both center-to-center and edge-to-edge distances should be retained. Dot-lattice studies typically emphasize center distances, while interface designers often discuss empty edge gaps. Converting between them requires knowing element dimensions.

---

# Proposed Experiment: UI Proximity Calibration

## Research question

How does the ratio of between-group to within-group spacing affect grouping judgments and task performance for realistic interface elements?

## Stimuli

Create repeated rows of:

- label–value pairs,
- form fields,
- action buttons,
- navigation items,
- dashboard metrics,
- text cards.

## Independent variables

### Spacing ratio

```text
1.00
1.10
1.20
1.35
1.50
1.75
2.00
2.50
3.00
4.00
```

These are sampling points, not expected thresholds.

### Cue condition

- spacing only
- spacing + alignment
- spacing + common region
- spacing + similarity
- conflicting similarity
- conflicting alignment

### Exposure

- 100 ms
- 300 ms
- 1 second
- unlimited

### Location

- central
- moderate eccentricity
- peripheral

## Dependent variables

- grouping choice
- confidence
- response time
- task accuracy
- first fixation
- number of fixations
- recall of group membership

## Analysis

Fit a hierarchical multinomial or logistic model:

```text
grouping probability
    ~ log(spacing ratio)
    + cue condition
    + exposure
    + eccentricity
    + stimulus type
    + interactions
    + participant random effects
```

## Required result before publishing bands

A practical band should be published only if:

1. the probability curve is stable across several UI stimulus classes,
2. uncertainty intervals are reported,
3. task outcomes agree with subjective grouping,
4. the result replicates,
5. accessibility-related observer variation is measured.

---

# Updated Findings

## OBS-006 — Grouping has no natural hard boundary in current evidence

The best-supported model is continuous and probabilistic.

**Confidence:** High within dot-lattice research.

## OBS-007 — Relative distance is more transferable than absolute distance

Scale transformations often preserve the grouping relationship better than fixed physical measurements.

**Confidence:** Moderate to high.

## OBS-008 — A ratio is insufficient without competing alternatives

The same within/between ratio can produce different judgments when alignment, orientation, similarity, or other candidate organizations differ.

**Confidence:** High.

## OBS-009 — “Whitespace” is an incomplete construct

Empty space has no independent meaning. Its compositional effect comes from how it changes the probability of competing organizations, expected eye movements, and object isolation.

**Confidence:** Moderate to high.

## OBS-010 — UI spacing tokens should encode relational roles

A future token system should distinguish:

- internal item spacing,
- subgroup spacing,
- section spacing,
- competing-axis spacing,
- peripheral isolation,
- exceptional emphasis.

A single geometric sequence of generic spacing values does not capture the perceptual mechanism.

**Confidence:** Provisional design inference.

---

# Decision for the Framework

We will **not** currently define:

```text
weak grouping = ratio X
moderate grouping = ratio Y
strong grouping = ratio Z
```

We will define a probabilistic model and collect the data needed to calibrate it for interface stimuli.

This avoids manufacturing false precision while preserving a clear path to quantitative implementation.

---

# Change Log

## Version 0.4

- Added the Pure Distance Law and its probabilistic interpretation.
- Added five quantitative proximity evidence records.
- Recorded experimental distance ratios from 1.0 to 1.5 in Gabor lattices.
- Distinguished continuous grouping curves from hard thresholds.
- Added a provisional multi-cue grouping model.
- Added a layout measurement protocol.
- Designed a UI proximity-calibration experiment.
- Rejected universal weak/moderate/strong ratio bands until interface-specific data exists.
