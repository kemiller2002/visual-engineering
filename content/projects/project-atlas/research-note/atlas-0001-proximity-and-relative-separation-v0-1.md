---
id: RR-VE-ATL-2026-E994
title: "ATLAS-0001: Proximity and Relative Separation"
project: project-atlas
version: "0.1"
status: "Evidence Review — Working Draft"
date: "2026-07-18"
authors:
  - Kevin Miller
  - ChatGPT
purpose: |
  Establish the first formal Project Atlas principle entry by synthesizing
  existing research on proximity-based perceptual grouping. This document
  distinguishes direct findings from interpretation, preserves boundaries
  and contradictions, and identifies measurable variables for later use in
  composition analysis and implementation.
tags:
  - proximity
  - grouping
  - gestalt
  - spatial-organization
  - relative-separation
  - visual-perception
  - composition
confidence: "Moderate to High, bounded to perceptual grouping"
llm_ingest: true
machine_readable: true
references:
  - "Kubovy, M., & Wagemans, J. (1995). Grouping by proximity and multistability in dot lattices: A quantitative Gestalt theory."
  - "Wagemans, J., et al. (2012). A Century of Gestalt Psychology in Visual Perception I."
  - "Claessens, P. M. E., & Wagemans, J. (2008). Proximity, collinearity, and orientation priors in zigzag lattices."
  - "Ben-Av, M. B., Sagi, D., & Braun, J. Perceptual grouping by similarity and proximity."
  - "Johansson, R. C. G., et al. (2024). Serial processing of proximity groups and similarity groups in perceptual organization."
  - "Beck, D. M., & Palmer, S. E. (2002). Top-down influences on perceptual grouping."
  - "Li, J., et al. (2018). Evidence for the beneficial effect of perceptual grouping on visual working memory."
purposes:
  - integrate
  - verify
audiences:
  - practitioner
  - researcher
---

# ATLAS-0001: Proximity and Relative Separation

## Purpose

This document asks a narrower and more useful question than whether proximity
"is real":

> What does existing research allow Project Atlas to claim about how spatial
> distance influences perceptual grouping, and what does it not yet justify?

Proximity is being used as the first reference implementation of the Atlas
method. The objective is to demonstrate how published evidence becomes bounded
observations, causal hypotheses, candidate laws, measurable variables, design
implications, and open questions.

---

# Key Findings

1. **Closer elements are more likely to be grouped, but grouping is not governed
   by a universal hard distance threshold.**

2. **Relative distances among competing organizations matter more than an
   isolated absolute gap.**

3. **Grouping is probabilistic and can be multistable.** A display may support
   more than one plausible organization, with perception shifting between them.

4. **Proximity competes with other cues**, including similarity, alignment,
   collinearity, connectedness, common region, orientation, and prior
   expectations.

5. **Proximity may influence early organization faster than similarity under
   some cue-conflict conditions**, but later processing can alter the final
   grouping judgment.

6. **Perceptual grouping can affect attention and visual working memory**, but
   benefits are conditional. Grouping can improve processing of included items
   while disadvantaging items excluded by the organization.

7. **The current literature does not justify a universal CSS spacing token,
   pixel value, or single within-group/between-group ratio.**

---

# Scope and Terminology

## Proximity

The tendency for spatially nearer elements to be perceived as belonging
together.

## Relative separation

The relationship between distances supporting one candidate grouping and the
distances supporting competing groupings.

## Candidate organization

Any plausible perceptual grouping supported by the geometry or other visual
cues in a composition.

## Multistability

A condition in which more than one organization is perceptually plausible and
the perceived grouping may alternate.

## Grouping judgment

A report of which elements appear to belong together. This is not equivalent to
task performance, comprehension, memory, or interaction success.

---

# Evidence Summary

## EVD-PROX-001 — Quantitative proximity and multistability in dot lattices

### Citation

Kubovy, M., & Wagemans, J. (1995). *Grouping by proximity and
multistability in dot lattices: A quantitative Gestalt theory*.
Psychological Science, 6(4), 225–234.

DOI: `10.1111/j.1467-9280.1995.tb00597.x`

### Evidence grade

**B**

### Research contribution

The work developed a quantitative account of grouping in dot lattices where
multiple orientations could support different organizations.

### Direct finding

Grouping preference changes systematically with the relative distances that
support competing lattice organizations.

### Interpretation

This supports a **probabilistic competition model** rather than a binary rule.
Proximity contributes evidence toward one organization, but alternative
organizations remain possible when their spatial support is similar.

### Supports

- LAW-PROX-001: Relative Separation
- LAW-PROX-002: Probabilistic Grouping
- LAW-PROX-003: Competing Organization

### Challenges

- Any universal threshold stating that a particular gap always creates a group.
- Any model treating grouping as a deterministic nearest-neighbor assignment.

### Generalizability limits

- Dot lattices are highly controlled and abstract.
- Grouping reports do not directly measure interface task performance.
- UI elements carry semantics, enclosure, typography, and interaction roles
  absent from simple dot patterns.

### Source

https://doi.org/10.1111/j.1467-9280.1995.tb00597.x

---

## EVD-PROX-002 — Century review of quantitative Gestalt research

### Citation

Wagemans, J., Elder, J. H., Kubovy, M., Palmer, S. E., Peterson, M. A.,
Singh, M., & von der Heydt, R. (2012). *A Century of Gestalt Psychology
in Visual Perception I: Perceptual Grouping and Figure-Ground Organization*.
Psychological Bulletin, 138(6), 1172–1217.

DOI: `10.1037/a0029333`

### Evidence grade

**A**

### Direct finding

The review documents the transition from qualitative Gestalt principles toward
psychophysical and computational models in which grouping strength can be
quantified and multiple cues can interact.

### Interpretation

Proximity is best treated as one source of organization within a broader cue
integration system. Its existence is well established, but its design
translation remains conditional.

### Supports

- LAW-PROX-001: Relative Separation
- LAW-PROX-002: Probabilistic Grouping
- LAW-PROX-004: Cue Competition

### Generalizability limits

A broad review establishes the state of perceptual science but does not supply
a ready-made spacing specification for realistic interfaces.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC3482144/

---

## EVD-PROX-003 — Orientation and collinearity modify distance effects

### Citation

Claessens, P. M. E., & Wagemans, J. (2008). *Proximity, collinearity,
and orientation priors in zigzag lattices*. Perception.

PubMed ID: `19146265`

### Evidence grade

**C**

### Direct finding

Perceptual grouping in zigzag lattices was not explained by distance alone.
Cardinal and oblique organizations differed in their sensitivity to proximity
and discollinearity.

### Interpretation

The same geometric distance can carry different grouping strength depending on
orientation and structural alignment. Distance is therefore not an independent
or context-free variable.

### Supports

- LAW-PROX-004: Cue Competition
- LAW-PROX-005: Orientation-Conditioned Proximity

### Challenges

- A one-dimensional spacing model.
- Equal treatment of horizontal, vertical, diagonal, radial, and irregular
  arrangements.

### Source

https://pubmed.ncbi.nlm.nih.gov/19146265/

---

## EVD-PROX-004 — Proximity and similarity can have different temporal priority

### Citation

Ben-Av, M. B., Sagi, D., & Braun, J. *Perceptual grouping by similarity
and proximity*.

PubMed ID: `7740775`

### Evidence grade

**C**

### Direct finding

Under cue conflict, proximity-based organization appeared earlier, while
similarity could exert greater influence with additional processing time.

### Interpretation

Spatial organization may make an early structural claim before color, shape,
or semantic interpretation fully influences the percept.

### Supports

- LAW-PROX-004: Cue Competition
- LAW-PROX-006: Temporal Cue Priority

### Design implication

A layout whose spacing communicates the wrong structure should not rely on
color alone to repair that structure, especially in brief, peripheral, or
rapidly scanned presentations.

### Source

https://pubmed.ncbi.nlm.nih.gov/7740775/

---

## EVD-PROX-005 — Sequential processing of proximity and similarity

### Citation

Johansson, R. C. G., et al. (2024). *Serial processing of proximity
groups and similarity groups in perceptual organization*.

### Evidence grade

**C**

### Direct finding

The experiments indicated sequential rather than fully parallel processing
under the tested conditions, with a temporal advantage for proximity grouping.

### Interpretation

Cue strength is partly time-dependent. A final unlimited-viewing judgment may
not describe the organization available during the first moments of perception.

### Supports

- LAW-PROX-006: Temporal Cue Priority
- LAW-PROX-004: Cue Competition

### Generalizability limits

- The specific stimulus and task constrain the result.
- The study does not establish that proximity always precedes every other cue.
- Interface scanning includes learned expectations and semantic information.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC11093805/

---

## EVD-PROX-006 — Grouping can be influenced by top-down probability

### Citation

Beck, D. M., & Palmer, S. E. (2002). *Top-down influences on perceptual
grouping*. Journal of Experimental Psychology: Human Perception and
Performance.

PubMed ID: `12421056`

### Evidence grade

**C**

### Direct finding

Grouping was sensitive to the probability that a target pair would be
organized by a particular cue when sufficient processing time was available.
Effects differed in speed across grouping factors.

### Interpretation

Perceptual grouping is not purely bottom-up. Learned expectation, task
probability, and processing time can modify which organization dominates.

### Supports

- LAW-PROX-004: Cue Competition
- LAW-PROX-007: Expectation-Conditioned Grouping

### Challenges

- The claim that proximity operates as an inflexible reflex.
- Designs that assume geometric organization can be evaluated independently of
  user goals and learned conventions.

### Source

https://pubmed.ncbi.nlm.nih.gov/12421056/

---

## EVD-PROX-007 — Grouping can benefit visual working memory

### Citation

Li, J., et al. (2018). *Evidence for the beneficial effect of perceptual
grouping on visual working memory*. Scientific Reports.

### Evidence grade

**B**

### Direct finding

The work included an empirical study and a meta-analytic study examining
whether perceptual grouping improves visual working-memory performance.

### Interpretation

Grouping can alter effective encoding or maintenance, not merely visual
appearance. However, the memory benefit depends on what features and
relationships the grouping makes available.

### Supports

- LAW-PROX-008: Conditional Encoding Benefit
- A broader Structural Encoding law

### Generalizability limits

- This evidence concerns perceptual grouping broadly, not proximity alone.
- Memory benefit does not automatically imply faster navigation or better
  interaction.
- Grouping may privilege some information at the expense of other information.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC6138684/

---

## EVD-PROX-008 — Proximity grouping extends beyond explicit visual reports

### Citation

Overvliet, K. E., et al. (2013). *Grouping by Proximity in Haptic
Contour Detection*.

### Evidence grade

**C**

### Direct finding

Closer proximity among contour elements increased detection rates and reduced
exploration time in a haptic contour-detection task.

### Interpretation

Proximity-based organization may reflect a broader perceptual strategy rather
than a convention unique to visual graphic design.

### Supports

- A cross-modal proximity hypothesis
- The possibility that proximity is a general organization mechanism

### Generalizability limits

- Haptic exploration is sequential and differs from visual perception.
- Cross-modal similarity does not prove identical neural or computational
  mechanisms.
- This cannot be translated directly into visual spacing guidance.

### Source

https://pmc.ncbi.nlm.nih.gov/articles/PMC3676406/

---

# Observations

## OBS-PROX-001 — Relative spacing carries more information than isolated spacing

### Observation

Controlled grouping studies compare distances supporting multiple candidate
organizations rather than treating a single gap as meaningful in isolation.

### Interpretation

A spacing token such as `16px` has no inherent grouping meaning. Its effect
depends on surrounding element sizes, internal gaps, competing gaps, alignment,
and other cues.

### Confidence

**High**

---

## OBS-PROX-002 — Grouping is continuous and probabilistic

### Observation

Dot-lattice research describes systematic changes in grouping probability as
relative distances change.

### Interpretation

The strongest useful output is a probability or confidence distribution over
candidate organizations, not a binary grouped/not-grouped label.

### Confidence

**High within controlled lattice paradigms; moderate for realistic interfaces**

---

## OBS-PROX-003 — Ambiguity is an expected state, not measurement noise

### Observation

Displays can support multiple plausible organizations.

### Interpretation

A composition may genuinely be multistable. Different users, tasks, exposure
times, or moments may produce different grouping reports without any observer
being "wrong."

### Confidence

**High**

---

## OBS-PROX-004 — Spatial organization can precede stylistic interpretation

### Observation

Several cue-conflict studies report an early temporal advantage for proximity
relative to similarity.

### Interpretation

Spacing may establish a first-pass structure that later cues reinforce, modify,
or overturn.

### Confidence

**Moderate**

---

## OBS-PROX-005 — Proximity does not operate alone

### Observation

Orientation, collinearity, similarity, region, connectivity, expectation, and
task can modify grouping.

### Interpretation

A proximity metric must be embedded in a cue-competition model.

### Confidence

**High**

---

## OBS-PROX-006 — Better grouping is conditional on intended task structure

### Observation

Grouping can improve encoding and attention for information included in the
group while disadvantaging excluded or competing information.

### Interpretation

Organization is not free cognitive compression. Every grouping cue expresses a
claim about which relationships matter.

### Confidence

**Moderate to high**

---

## OBS-PROX-007 — Existing research does not validate universal interface bands

### Observation

The strongest quantitative studies use dots, lattices, contours, or tightly
controlled symbols rather than complete production interfaces.

### Interpretation

It would be premature to publish rules such as:

- internal gap must be exactly half the external gap;
- 8 pixels means related and 24 pixels means unrelated;
- a ratio above a fixed value guarantees grouping.

### Confidence

**High**

---

# Candidate Laws

## LAW-PROX-001 — Law of Relative Separation

### Hypothesis

The probability that elements are perceived as a group rises as distances
within that candidate group become smaller relative to distances supporting
competing organizations.

### Prediction

Holding element size and other cues constant, increasing the ratio of a
competing gap to the intended within-group gap should increase reports of the
intended organization.

### Supporting evidence

- EVD-PROX-001
- EVD-PROX-002

### Counter evidence

No evidence found that rejects the general relationship. However, orientation,
similarity, semantic expectation, and other cues can reduce or override its
effect.

### Confidence

**High as a perceptual relationship; low for any universal numeric threshold**

---

## LAW-PROX-002 — Law of Probabilistic Grouping

### Hypothesis

Perceptual grouping represents a distribution over competing organizations
rather than a deterministic classification.

### Prediction

As two candidate organizations approach equal support, grouping judgments
should become more variable and confidence should decline.

### Supporting evidence

- EVD-PROX-001
- EVD-PROX-002

### Counter evidence

None identified at the level of the general claim.

### Confidence

**High**

---

## LAW-PROX-003 — Law of Competing Organization

### Hypothesis

The perceptual force of a spacing relationship cannot be evaluated without
representing the alternative organizations available in the same composition.

### Prediction

Two displays with the same intended within-group gap can produce different
grouping judgments when competing row, column, alignment, or boundary
relationships differ.

### Supporting evidence

- EVD-PROX-001
- EVD-PROX-003

### Confidence

**High**

---

## LAW-PROX-004 — Law of Cue Competition

### Hypothesis

Proximity combines with and competes against similarity, alignment,
collinearity, enclosure, connectedness, motion, semantics, and expectation.

### Prediction

Conflicting cues will shift grouping probability as their relative strengths,
timing, and task relevance change.

### Supporting evidence

- EVD-PROX-002
- EVD-PROX-003
- EVD-PROX-004
- EVD-PROX-005
- EVD-PROX-006

### Confidence

**High conceptually; quantitative weights remain unknown**

---

## LAW-PROX-005 — Law of Orientation-Conditioned Proximity

### Hypothesis

Equal distances do not necessarily carry equal grouping strength across
different orientations and alignment structures.

### Prediction

The same spacing relationship should produce different grouping probabilities
when rotated or when collinearity changes.

### Supporting evidence

- EVD-PROX-003

### Confidence

**Moderate**

---

## LAW-PROX-006 — Law of Temporal Cue Priority

### Hypothesis

Grouping cues do not necessarily become available at the same time. Proximity
may dominate initial organization under some conflicts before slower cues alter
the final percept.

### Prediction

Brief exposure should produce stronger proximity-consistent reports than
longer exposure in selected proximity-versus-similarity conflicts.

### Supporting evidence

- EVD-PROX-004
- EVD-PROX-005

### Confidence

**Moderate**

---

## LAW-PROX-007 — Law of Expectation-Conditioned Grouping

### Hypothesis

When sufficient processing time is available, learned probabilities and task
expectations can alter grouping.

### Prediction

Repeated exposure to a task in which one organization is more likely should
bias later grouping or target-search performance toward that organization.

### Supporting evidence

- EVD-PROX-006

### Confidence

**Moderate**

---

## LAW-PROX-008 — Law of Conditional Encoding Benefit

### Hypothesis

Perceptual grouping improves encoding or memory when the grouping corresponds to
task-relevant structure, but can impair access to information outside that
structure.

### Prediction

Memory and change-detection performance should improve for grouped,
task-relevant items while remaining unchanged or worsening for items excluded
by the grouping.

### Supporting evidence

- EVD-PROX-007

### Confidence

**Moderate to high for perceptual grouping generally; proximity-specific
evidence remains incomplete**

---

# Provisional Mechanism Model

The earlier causal chain:

```text
reduced distance
    → automatic grouping
    → reduced search area
    → reduced working-memory demand
    → faster recognition
```

is too simple.

A better provisional model is:

```text
geometry and visual cues
    ↓
evidence for multiple candidate organizations
    ↓
time-dependent cue integration
    ↓
task- and expectation-conditioned grouping probability
    ↓
allocation of attention and encoding priority
    ↓
effects on search, memory, comprehension, and action
```

This model deliberately separates perceptual grouping from downstream outcomes.
Grouping may influence search or memory, but it does not guarantee improvement.

---

# Candidate Computational Representation

For each candidate organization `G`:

```text
CueEvidence(G) =
    wp × relative_proximity
  + wa × alignment
  + wc × collinearity
  + ws × similarity
  + wr × common_region
  + wn × connectedness
  + wm × common_motion
  + we × semantic_expectation
  + wt × task_relevance
```

Then:

```text
P(G) = exp(CueEvidence(G)) / Σ exp(CueEvidence(all candidate organizations))
```

## Status

This is a conceptual architecture, not a fitted model.

## What remains unknown

- Valid weights for UI-like stimuli
- Nonlinear cue interactions
- Temporal changes in weights
- Observer-specific priors
- Effects of reading direction and culture
- Effects of device, distance, and viewport
- Whether task performance is predicted by subjective grouping probability

---

# Measurement Model

## MET-PROX-001 — Geometric relationship record

```yaml
elements:
  width:
  height:
  shape:
  count:

distance:
  center_to_center_within:
  edge_to_edge_within:
  center_to_center_between:
  edge_to_edge_between:
  nearest_neighbor:
  repeated_distance_consistency:

ratios:
  between_to_within_center:
  between_to_within_edge:
  element_size_to_gap:
  horizontal_to_vertical:

candidate_organizations:
  rows:
  columns:
  sections:
  nested_groups:
  diagonals:
  semantic_units:
```

---

## MET-PROX-002 — Competing cue record

```yaml
alignment:
  strength:
  supports:

similarity:
  color:
  shape:
  size:
  typography:
  texture:
  supports:

boundaries:
  common_region:
  connectedness:
  divider:
  background:
  supports:

semantics:
  heading_association:
  label_value_relationship:
  learned_component_pattern:
  task_relationship:
  supports:
```

---

## MET-PROX-003 — Viewing and task record

```yaml
viewing:
  exposure_duration:
  viewing_distance:
  expected_fixation:
  eccentricity:
  viewport:
  scrolling:
  motion:

task:
  detect:
  identify:
  group:
  compare:
  read:
  remember:
  navigate:
  act:

outcomes:
  grouping_choice:
  grouping_confidence:
  reaction_time:
  accuracy:
  first_fixation:
  fixation_count:
  recall:
  task_completion:
  error_rate:
```

---

# Composition Implications

## Supported implications

### 1. Measure relationships, not isolated tokens

Record internal and external spacing together. A gap has meaning only relative
to elements and alternatives around it.

### 2. Map plausible wrong groupings

A composition review should identify not only the intended grouping but every
credible competing organization.

### 3. Treat spacing as an early structural cue

When spacing and color disagree, spacing may control the first percept even if
color later changes the judgment.

### 4. Reinforce important structure with compatible cues

Spacing, alignment, headings, boundaries, and similarity should agree when
misgrouping has meaningful consequences.

### 5. Do not maximize every cue

Over-reinforcement can fragment a layout, create excessive enclosure, or
flatten hierarchy by making every boundary equally strong.

### 6. Distinguish perceptual success from task success

A layout can be grouped as intended yet remain difficult to read, compare,
remember, or operate.

---

## Unsupported implications

The current evidence does **not** support:

- one universal proximity ratio for all layouts;
- a universal pixel distance for grouping;
- the claim that more whitespace is always better;
- the claim that proximity always overrides similarity;
- the claim that perceptual grouping automatically lowers cognitive load;
- the claim that a visually neat composition is operationally effective.

---

# Initial Atlas Design Diagnostic

## Proximity Diagnostic v0.1

For each intended group:

1. What elements are intended to belong together?
2. What are the within-group center and edge distances?
3. What are the nearest competing distances?
4. Which alternative row, column, section, or semantic groupings are possible?
5. Which cues reinforce the intended organization?
6. Which cues contradict it?
7. Is the task performed during brief scanning or prolonged inspection?
8. Does the organization privilege the information users actually need?
9. Is the important relationship central or peripheral to expected fixation?
10. What observable outcome would demonstrate that the grouping works?

---

# Evidence Sufficiency

## Core perceptual claim

**Rating: A**

There is strong, replicated evidence that relative proximity influences visual
grouping.

## Quantitative interface translation

**Rating: D**

There is not yet enough direct evidence to define universal spacing bands for
realistic interfaces.

## Temporal interaction with similarity

**Rating: C**

Controlled studies support temporal differences, but generalization remains
bounded.

## Effects on task performance

**Rating: C**

Evidence indicates effects on attention and working memory, but interface-level
task outcomes require more synthesis.

## Need for original human experiments

**Current priority: Low**

Existing research is sufficient to build and refine the conceptual model.
Original experiments should be deferred until the literature review identifies
a narrow, consequential gap that cannot be answered from published data.

---

# Open Questions

1. Which published studies provide recoverable psychometric functions relating
   spacing ratios to grouping probability?

2. How stable are proximity functions across dots, letters, icons, words,
   controls, cards, and mixed-content regions?

3. Should UI spacing be normalized by element size, center distance, edge gap,
   visual angle, or a combination?

4. How do nested groups alter the strength of local proximity?

5. When do semantic expectations override conflicting geometry?

6. How does reading direction change grouping preference?

7. How does responsive reflow affect learned grouping across viewport sizes?

8. Do grouping judgments predict task time, error rate, and memory in realistic
   interfaces?

9. Can computational vision estimate candidate organizations reliably enough to
   support an automated composition diagnostic?

10. What evidence distinguishes beneficial whitespace from empty space that
    weakens continuity, comparison, or information density?

---

# Next Actions

1. Retrieve and extract quantitative details from the Kubovy and Wagemans
   dot-lattice studies.

2. Build a source table of proximity-versus-similarity cue-conflict experiments.

3. Separate evidence into:
   - grouping judgment,
   - visual search,
   - attention,
   - working memory,
   - task performance.

4. Identify studies using text, symbols, diagrams, or interface-like stimuli.

5. Create a machine-readable evidence JSON schema matching this document.

6. Apply the Proximity Diagnostic to several existing layouts as case studies,
   without treating those case studies as scientific proof.

---

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 0.1 | 2026-07-18 | Kevin Miller and ChatGPT | First formal Atlas principle review. Synthesized proximity evidence, created candidate laws, measurement records, a provisional mechanism model, design boundaries, and a targeted research backlog. |

---

# Agent Instructions

When creating or modifying this document:

1. Separate direct observation from interpretation.
2. Never strengthen a conclusion beyond the available evidence.
3. Preserve contradictory findings and moderator variables.
4. Prefer measurable relationships over subjective descriptions.
5. Reference stable IDs for observations, evidence, laws, and metrics.
6. Do not convert laboratory distances directly into pixel recommendations.
7. Do not treat perceptual grouping as synonymous with usability.
8. Record the stimulus, task, population, viewing conditions, and outcome.
9. Retain prior revisions and append changes to the revision history.
10. Mark computational equations as provisional until fitted and validated.
