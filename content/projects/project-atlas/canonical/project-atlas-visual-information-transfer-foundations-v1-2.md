---
id: RR-VE-ATL-2026-A8E5
authors:
- OpenAI
confidence: Moderate
date: 2026-07-18
llm_ingest: true
machine_readable: true
project: project-atlas
purpose: |
  Establish a research foundation for treating typography and visual design as constrained communication systems. This document tests which concepts from information theory, signal detection, psychophysics, and symbol-recognition research can be applied directly, which are useful analogies, and which require new Atlas-specific models.
references:
- Shannon, C. E. (1948), A Mathematical Theory of Communication
- Legge et al. (1997-2007), visual span and reading research
- Mueller and Weidemann (2012), Alphabetic letter identification
- Liu et al. (2009), Using geometric moments to explain human letter recognition
- Yu et al. (2007), Effect of letter spacing on visual span and reading speed
- Chiu et al. (2023), The role of visual crowding in eye movements during reading
- Coates et al. (2021), Feature contingencies when reading letter strings
- Wiley et al. (2016), The effects of alphabet and expertise on letter perception
- Bernard et al. (2016), A new font designed for peripheral vision
status: research draft
summary: |
  Typography can be usefully modeled as a noisy visual communication channel, but Shannon information cannot be equated with comprehension or meaning. The strongest measurable foundation is a layered model: physical signal preservation, perceptual discrimination, symbolic recognition, linguistic interpretation, and task success. Existing vision research already measures letter recognition in bits through confusion matrices and visual-span profiles, supporting the idea that type systems can be evaluated by information retained under degradation. However, alphabet fitness cannot be reduced to maximum geometric distinctiveness because familiarity, frequency, context, word structure, spacing, and reading strategy provide redundancy and alter the cost of individual confusions. Atlas should therefore measure conditional communication performance: how much task-relevant uncertainty a visual system removes for a defined reader, environment, and task.
version: 1.0
purposes:
  - orient
  - integrate
  - reference
audiences:
  - practitioner
  - researcher
  - contributor
---

# Visual Information Transfer: Foundations for the Typography Genome

## Purpose

This document investigates the hypothesis that typography is best understood as visual information engineering.

The hypothesis is promising, but it must be made more precise. Information theory, perception science, reading research, and graphic design use the word *information* differently. Treating them as identical would produce a persuasive metaphor but a weak scientific model.

The central question is:

> Which parts of visual communication can be measured as transmission through noise, and where must Atlas add models of perception, meaning, context, and action?

------------------------------------------------------------------------

# Key Findings

- Shannon's communication model is directly useful for describing encoding, channel noise, decoding errors, redundancy, and capacity. It deliberately excludes semantic meaning, so it cannot by itself measure comprehension, persuasion, or usability.
- Letter-recognition research already converts confusion matrices into information transmitted in bits. The information-theory connection is therefore more than analogy at the perceptual-recognition layer.
- The most useful unit is conditional uncertainty reduction, not visual complexity alone. A symbol is informative when it helps a specific observer distinguish among relevant alternatives under specified conditions.
- Maximum pairwise geometric distance is not automatically optimal. Readers exploit familiarity, letter frequency, word probability, syntax, and context. A visually redundant alphabet may be efficient because language supplies error correction.
- Recognition failure is usually structured rather than random. Blur, low contrast, crowding, eccentricity, and feature migration create predictable confusion families.
- Spacing exposes a real tradeoff: increasing separation can reduce crowding but spread letters farther into lower-acuity peripheral vision and weaken word cohesion.
- Visual communication has several distinct bottlenecks. Signal preservation, glyph discrimination, word recognition, comprehension, navigation, and action can fail independently.
- An alphabet should be modeled as a weighted confusion network. Weights must include both perceptual confusion probability and real-world occurrence or consequence.
- Atlas should distinguish channel capacity from task capacity. A setting may transmit many recognizable characters yet still communicate poorly because hierarchy, language, or task structure is weak.
- Beauty cannot currently be derived from transmission efficiency. Functional robustness may contribute to fluency and preference, but aesthetic value also depends on culture, expectation, expression, novelty, and purpose.

------------------------------------------------------------------------

# Content

## 1. The Strong Version of the Hypothesis

A useful starting definition is:

> Typography is the design and arrangement of visible language so that intended distinctions, structures, meanings, and actions survive the conditions of perception.

This is stronger than defining typography as the arrangement of type, but more accurate than defining it only as information transfer.

It identifies four progressively higher outcomes:

```text
Distinction
    ↓
Structure
    ↓
Meaning
    ↓
Action
```

A system can succeed at an earlier layer and fail at a later one.

Examples:

- Every letter may be recognizable while the paragraph structure remains difficult to navigate.
- Every sentence may be understandable while the visual hierarchy obscures the primary action.
- A warning may be perfectly legible but semantically ambiguous.
- A display face may transmit words accurately while communicating the wrong institutional tone.

Typography is therefore not one channel. It is a stack of coupled channels.

## 2. What Shannon's Model Gives Us

Shannon's communication system contains:

```text
Information source
    ↓
Transmitter / encoder
    ↓
Channel
    ↓
Noise
    ↓
Receiver / decoder
    ↓
Destination
```

Mapped cautiously to typography:

```text
Intended linguistic content
    ↓
Writing and typographic encoding
    ↓
Print, display, environment, and visual field
    ↓
Blur, glare, crowding, distance, resolution, distraction
    ↓
Visual and cognitive decoding
    ↓
Reader interpretation and response
```

The mapping is valuable because it forces us to identify where errors enter rather than calling the final experience simply "readable" or "unreadable."

Shannon's model contributes several directly useful concepts:

### Entropy

Uncertainty among possible symbols or messages before observation.

### Conditional entropy

Uncertainty remaining after observing the received signal.

### Mutual information

How much observing the received signal reduces uncertainty about the transmitted symbol.

### Redundancy

Structure that allows a message to survive partial corruption.

### Channel capacity

The maximum rate at which information can be transmitted with arbitrarily low error under the model's assumptions.

### Noise

Any process that causes the received signal to differ from the transmitted signal.

These concepts can be used directly for controlled recognition experiments.

## 3. The Boundary of Shannon Information

Shannon explicitly separated the engineering problem of selecting and transmitting messages from the semantic question of what those messages mean.

That boundary is essential for Atlas.

A perfectly transmitted string can be:

- meaningless to the reader
- written in an unknown language
- logically contradictory
- misleading
- poorly structured
- irrelevant to the reader's task

Therefore:

```text
Symbol accuracy ≠ comprehension
Comprehension ≠ judgment
Judgment ≠ action
Action ≠ successful outcome
```

Atlas should not use a single "information transfer" score across all these layers.

Instead, it should model a chain of conditional success probabilities.

## 4. The Atlas Layered Channel Model

### Layer 1: Physical rendering

Question:

> Did the intended visual form appear in the medium?

Variables:

- pixel density
- rasterization
- ink spread
- stroke dropout
- display contrast
- illumination
- motion
- glare

Output metric examples:

- edge preservation
- stroke continuity
- modulation transfer
- rendered-to-source similarity

### Layer 2: Perceptual availability

Question:

> Was identity-bearing visual information available to the observer?

Variables:

- angular size
- retinal eccentricity
- contrast sensitivity
- blur
- exposure duration
- crowding
- masking
- fatigue

Output metric examples:

- feature detectability
- contrast threshold
- visual-span profile
- crowding distance

### Layer 3: Symbol discrimination

Question:

> Could the observer distinguish the intended glyph from relevant alternatives?

Variables:

- pairwise similarity
- feature distinctiveness
- response bias
- alphabet familiarity
- case
- neighboring glyphs

Output metric examples:

- confusion matrix
- identification accuracy
- mutual information
- minimum pairwise recognition margin

### Layer 4: Sequence recognition

Question:

> Could the observer recover the intended word or symbol sequence?

Variables:

- letter position
- spacing
- word frequency
- orthographic probability
- parafoveal information
- feature migration
- line position

Output metric examples:

- word accuracy
- fixation duration
- regression rate
- sequence edit distance
- reading speed

### Layer 5: Structural interpretation

Question:

> Could the observer infer grouping, hierarchy, order, and relationships?

Variables:

- proximity
- alignment
- heading contrast
- indentation
- line length
- paragraph rhythm
- visual grouping

Output metric examples:

- target-location time
- hierarchy reconstruction
- grouping errors
- scan-path efficiency

### Layer 6: Semantic comprehension

Question:

> Did the reader construct the intended meaning?

Variables:

- vocabulary
- syntax
- domain knowledge
- ambiguity
- working memory
- reading fluency

Output metric examples:

- factual recall
- inference accuracy
- summarization quality
- delayed retention

### Layer 7: Task and action

Question:

> Did the communication support the intended decision or behavior?

Variables:

- action visibility
- perceived risk
- trust
- urgency
- competing goals
- feedback
- consequence of error

Output metric examples:

- task success
- time to action
- error severity
- abandonment
- recovery cost

This layered model prevents a high score at one level from concealing failure at another.

## 5. Recognition Can Be Measured in Bits

Letter-recognition experiments commonly present letters at multiple positions or under degraded conditions, record identification responses, and construct confusion matrices.

A confusion matrix provides:

```text
P(response = j | stimulus = i)
```

From that distribution, information transmitted can be estimated.

For an alphabet of 26 equally probable letters, the maximum uncertainty is:

```text
log2(26) ≈ 4.70 bits per letter
```

Perfect recognition transmits approximately 4.70 bits.

Complete inability to distinguish the letters transmits approximately 0 bits.

Partial recognition falls between those values.

This creates a useful recognition curve:

```text
Noise level
    ↓
Confusion matrix
    ↓
Mutual information
    ↓
Bits retained
```

The curve is more informative than a single accuracy score because it captures which mistakes occur and how rapidly uncertainty increases.

## 6. Why Accuracy Alone Is Not Enough

Two alphabets can have the same average accuracy but different error structures.

Example:

### System A

- Errors are distributed across many alternatives.
- A misread letter gives little useful information.

### System B

- Errors occur almost entirely within one predictable pair.
- Most of the alphabet remains reliably distinguished.

Both might score 90% accuracy, but System B preserves more usable structure and may be easier for language context to correct.

Accuracy also ignores asymmetry.

A degraded `c` may often be reported as `e`, while a degraded `e` may not be reported as `c` at the same rate. Differences in feature visibility, response bias, and familiarity can make confusion directional.

Atlas should preserve full conditional response distributions rather than collapsing them prematurely.

## 7. The Alphabet as a Weighted Confusion Network

Represent each glyph as a node.

Represent confusion as a directed edge:

```text
c ──0.18──> e
e ──0.07──> c
```

The network changes with:

- size
- blur
- contrast
- eccentricity
- spacing
- neighboring characters
- case
- reader population
- exposure duration

There is therefore no single permanent "distance" between glyphs.

There is a family of conditional distances:

```text
D(i, j | typeface, size, noise, context, observer, task)
```

A complete alphabet model should include:

- average separation
- weakest pair
- number of high-confusion clusters
- asymmetry
- degradation slope
- recovery through context
- frequency-weighted error cost

## 8. Why Frequency Must Weight Alphabet Fitness

Letters are not equally frequent.

Letter pairs are not equally frequent.

Words are not equally probable.

Confusing two rare symbols may have little practical consequence. Confusing two high-frequency symbols can affect large amounts of text.

A practical alphabet score should therefore weight pairwise confusion by exposure:

```text
Expected recognition cost
=
Σ P(symbol or sequence)
× P(confusion)
× consequence of confusion
```

For continuous reading, bigram and word frequency may matter more than isolated-letter frequency.

For safety signage, consequence may dominate frequency.

For passwords or serial numbers, every character may need nearly equal weight because linguistic context cannot repair the error.

This leads to a major principle:

> The optimal symbol system depends on the probability and cost structure of the messages it carries.

## 9. Language as Error-Correcting Redundancy

Natural language is highly redundant.

Readers use:

- lexical probability
- spelling constraints
- syntax
- semantics
- sentence context
- topic knowledge

to infer uncertain symbols.

The word:

```text
t_e
```

is often recoverable from context even when one letter is missing.

This does not mean the missing letter is unimportant. It means recognition occurs through combined bottom-up and top-down information.

Typography can therefore trade some symbol-level efficiency against language-level redundancy, but the trade is task dependent.

Context is weak or absent in:

- random identifiers
- medication labels
- account numbers
- passwords
- airport codes
- mathematical notation
- unfamiliar names
- short interface labels
- emergency signage

These tasks require stronger character-level distinction than ordinary prose.

## 10. Crowding as Channel Interference

Crowding is not merely reduced sharpness. A target can remain visible while becoming difficult to identify because neighboring features interfere.

Research describes several possible effects:

- feature pooling
- feature substitution
- positional uncertainty
- source confusion
- mislocalization of features between neighboring letters

This resembles inter-symbol interference in communication systems.

The analogy is useful because the problem is relational:

```text
Target signal
+
neighboring signals
→ corrupted feature assignment
```

But the perceptual mechanism should not be assumed to be identical to electronic interference.

The important design consequence is:

> More visible ink does not necessarily produce more recoverable information.

Adding weight, decoration, or density can increase energy while reducing discriminability.

## 11. Spacing Is a Bandwidth Allocation Problem

Increasing letter spacing can:

- reduce local crowding
- improve separation
- expose boundaries

But it can also:

- spread letters farther into peripheral vision
- increase line length
- weaken word grouping
- reduce skipping
- increase the number of fixations
- alter rhythm

This creates a competing-cost function:

```text
Total spacing cost
=
crowding cost
+
eccentricity cost
+
grouping cost
+
navigation cost
```

The optimal point depends on:

- central versus peripheral reading
- print size
- reader vision
- word length
- line width
- display constraints

Spacing should therefore be modeled as a conditional optimum rather than a monotonic good.

## 12. Recognition Curves and Robustness

For a glyph, pair, alphabet, or typeface, define a degradation parameter `n`.

Examples:

- Gaussian blur radius
- contrast reduction
- angular-size reduction
- added visual clutter
- eccentricity
- exposure-time reduction

Measure recognition information:

```text
I(n) = mutual information retained at noise level n
```

A robust system loses information slowly.

Candidate metrics:

### Half-information threshold

Noise level at which transmitted information falls to 50% of maximum.

### Failure slope

Rate at which information declines near the threshold.

### Minimum-pair threshold

Noise level at which the weakest important pair becomes unreliable.

### Area under robustness curve

Total retained information across a defined degradation range.

### Context recovery gain

Difference between isolated-symbol and word-context information.

### Population robustness spread

Variation in the curve across reader groups.

This gives a better answer than asking which font is universally most readable.

## 13. Alphabet Fitness Is Multi-Objective

A symbol system may need to balance:

- recognition accuracy
- robustness
- visual coherence
- learnability
- writing speed
- production cost
- spatial efficiency
- language compatibility
- cultural continuity
- emotional expression

Maximum geometric distinctiveness alone could create an alphabet that is:

- slow to learn
- visually chaotic
- difficult to write
- inefficient in space
- incompatible with reader expectations

Alphabet design is therefore a Pareto optimization problem.

There may be many non-dominated solutions rather than one universally optimal alphabet.

## 14. Familiarity Changes the Channel

Expertise alters perception.

Readers of an alphabet develop:

- tuned feature detectors
- expectations about legal forms
- sensitivity to conventional variations
- efficient mappings from shapes to identities
- stronger use of orthographic context

A familiar but geometrically imperfect symbol may outperform an unfamiliar but objectively distinct alternative.

This means the observer is part of the channel.

A fuller model is:

```text
Performance
=
f(signal, noise, symbol system, observer history, context, task)
```

The typography genome cannot be purely geometric.

## 15. Can Beauty Be Reduced to Efficiency?

The current evidence does not justify this claim.

Possible relationships include:

- fluent processing can increase preference
- familiar proportions can feel stable
- balanced differentiation can produce visual coherence
- robust forms can appear purposeful
- predictable rhythm can reduce effort

But beauty also reflects:

- cultural association
- historical reference
- novelty
- status
- identity
- expressive tension
- deliberate inefficiency
- context and expectation

Typography sometimes uses friction intentionally:

- ceremonial text
- luxury branding
- horror titles
- protest graphics
- editorial emphasis
- expressive poetry

A more defensible hypothesis is:

> Processing fluency is one contributor to aesthetic response, not its complete cause.

## 16. A Revised Definition of Visual Information

Atlas should avoid treating visual information as the amount of detail in an image.

A dense pattern can contain many physical variations while conveying little useful information.

A single red octagon can carry substantial task-relevant information for a trained driver.

A useful Atlas definition is:

> Visual information is the reduction of task-relevant uncertainty produced in an observer by a visual signal under defined conditions.

This definition requires five explicit elements:

1. Observer
2. Alternatives or uncertainty
3. Visual signal
4. Conditions
5. Task

Without those elements, claims about "more information" are ambiguous.

## 17. Proposed Atlas Communication Model

```text
Intent
    ↓
Semantic formulation
    ↓
Symbolic encoding
    ↓
Typographic and compositional encoding
    ↓
Physical rendering
    ↓
Environmental and perceptual noise
    ↓
Feature extraction
    ↓
Symbol recognition
    ↓
Sequence and structural reconstruction
    ↓
Semantic interpretation
    ↓
Decision and action
    ↓
Outcome
```

Feedback can occur at several levels:

- eye movements resample the signal
- regressions revisit uncertain text
- context repairs symbol errors
- interface feedback confirms or rejects action
- learning changes later perception

The model is therefore dynamic, not a one-way pipeline.

------------------------------------------------------------------------

# Observations

## OBS-VIT-001

### Observation

Visual-span research expresses letter-recognition performance as information transmitted in bits derived from confusion patterns across letter positions.

### Interpretation

The application of information theory to typography is not merely metaphorical at the perceptual-recognition layer.

### Confidence

High

## OBS-VIT-002

### Observation

Shannon's original framework explicitly brackets meaning from the engineering problem of communication.

### Interpretation

Atlas must not equate character transmission with comprehension, interpretation, or successful action.

### Confidence

High

## OBS-VIT-003

### Observation

Letter confusion changes under size, blur, contrast, eccentricity, crowding, and spacing.

### Interpretation

Glyph distance is conditional rather than an intrinsic scalar property.

### Confidence

High

## OBS-VIT-004

### Observation

Increased letter spacing can reduce crowding without necessarily improving overall reading speed.

### Interpretation

Reducing one source of noise can create costs elsewhere in the reading system.

### Confidence

High

## OBS-VIT-005

### Observation

Geometric models using low-order image moments explain a meaningful portion, but not all, of human letter-confusion patterns.

### Interpretation

Computational proxies can narrow the design space, but human recognition cannot yet be replaced by simple geometry alone.

### Confidence

Moderate

## OBS-VIT-006

### Observation

Fonts designed to reduce inter-letter similarity can improve some peripheral letter-recognition measures without consistently improving every reading outcome.

### Interpretation

Improving component-level transmission does not guarantee system-level performance.

### Confidence

Moderate to high

## OBS-VIT-007

### Observation

Expertise and alphabet familiarity alter perceived letter similarity.

### Interpretation

Recognition-space geometry is partly learned.

### Confidence

High

------------------------------------------------------------------------

# Evidence

## EVD-VIT-001

### Citation

Shannon, C. E. (1948). *A Mathematical Theory of Communication*. Bell System Technical Journal, 27, 379–423 and 623–656.

### Summary

Defines entropy, redundancy, channel capacity, noise, encoding, and decoding while separating the technical communication problem from semantic meaning.

### Supports

- LAW-VIT-001
- LAW-VIT-002
- LAW-VIT-009

### Challenges

- Claims equating information fidelity with understanding

## EVD-VIT-002

### Citation

Legge, G. E. et al. Visual-span studies, including *The Case for the Visual Span as a Sensory Bottleneck in Reading*.

### Summary

Measures letter recognition across positions and expresses visual-span size using information transmitted in bits. Links visual span to reading performance while retaining important qualifications.

### Supports

- LAW-VIT-003
- LAW-VIT-004
- LAW-VIT-006

### Challenges

- A single-font ranking detached from viewing position and task

## EVD-VIT-003

### Citation

Mueller, S. T., and Weidemann, C. T. (2012). *Alphabetic letter identification: Effects of perceivability, similarity, and bias*.

### Summary

Shows that identification performance reflects perceivability, similarity, and response bias rather than visual similarity alone.

### Supports

- LAW-VIT-004
- LAW-VIT-005
- LAW-VIT-008

### Challenges

- Purely geometric models of legibility

## EVD-VIT-004

### Citation

Liu, L. et al. (2009). *Using geometric moments to explain human letter recognition near the acuity limit*.

### Summary

Uses a large confusion dataset and finds that low-order geometric moments account for a substantial portion of human letter confusions for English and Chinese characters.

### Supports

- LAW-VIT-004
- LAW-VIT-007

### Challenges

- The claim that named anatomy categories are the only useful feature representation

## EVD-VIT-005

### Citation

Yu, D. et al. (2007). *Effect of letter spacing on visual span and reading speed*.

### Summary

Finds competing effects of spacing: reduced crowding versus greater peripheral extent. Reading and visual-span performance do not increase monotonically with spacing.

### Supports

- LAW-VIT-006
- LAW-VIT-010

### Challenges

- "More spacing is always more readable"

## EVD-VIT-006

### Citation

Chiu, T. Y. et al. (2023). *The role of visual crowding in eye movements during reading*.

### Summary

Examines how crowding affects natural reading, including parafoveal processing and saccade targeting, rather than treating it only as isolated-letter loss.

### Supports

- LAW-VIT-003
- LAW-VIT-006

### Challenges

- Models that limit crowding to local glyph recognition

## EVD-VIT-007

### Citation

Coates, D. R. et al. (2021). *Feature contingencies when reading letter strings*.

### Summary

Investigates distinctive features and possible feature mislocalization between crowded letters.

### Supports

- LAW-VIT-004
- LAW-VIT-005

### Challenges

- Static whole-outline models

## EVD-VIT-008

### Citation

Wiley, R. W. et al. (2016). *The effects of alphabet and expertise on letter perception*.

### Summary

Shows that alphabet expertise affects similarity judgments and perceptual organization.

### Supports

- LAW-VIT-008

### Challenges

- Observer-independent alphabet geometry

## EVD-VIT-009

### Citation

Bernard, J. B. et al. (2016). *A new font, specifically designed for peripheral vision, improves peripheral letter and word recognition, but not eye-mediated reading performance*.

### Summary

Reducing inter-letter similarity improved several recognition measures, but improvements did not transfer uniformly to reading performance.

### Supports

- LAW-VIT-002
- LAW-VIT-003
- LAW-VIT-011

### Challenges

- The assumption that better glyph recognition automatically produces faster reading

------------------------------------------------------------------------

# Candidate Laws

## LAW-VIT-001: Layer Separation

### Hypothesis

Reliable visual communication requires separate measurement of physical signal, perception, recognition, structure, meaning, and action.

### Prediction

Interventions that improve one layer will sometimes leave later layers unchanged or make them worse.

### Supporting Evidence

- EVD-VIT-001
- EVD-VIT-009

### Counter Evidence

None identified against the general layered claim; the specific boundaries require refinement.

### Confidence

High

## LAW-VIT-002: Semantic Non-Equivalence

### Hypothesis

The amount of visually transmitted symbol information does not determine comprehension or usefulness.

### Prediction

Two settings with equal character-recognition information can produce different comprehension and task performance.

### Supporting Evidence

- EVD-VIT-001
- EVD-VIT-009

### Counter Evidence

Strong correlations may appear in threshold-limited conditions, but correlation would not establish equivalence.

### Confidence

High

## LAW-VIT-003: Bottleneck Dominance

### Hypothesis

Overall communication performance is constrained most strongly by the weakest active layer.

### Prediction

Improving a non-limiting layer will produce little system-level benefit until the dominant bottleneck is addressed.

### Supporting Evidence

- EVD-VIT-002
- EVD-VIT-006
- EVD-VIT-009

### Counter Evidence

Multiple weak layers may interact rather than form one clean bottleneck.

### Confidence

Moderate

## LAW-VIT-004: Conditional Distinctiveness

### Hypothesis

Symbol distinctiveness is a conditional relationship between alternatives, observer, context, and degradation rather than a fixed property of a glyph.

### Prediction

Pairwise confusion rankings will change across viewing conditions and reader populations.

### Supporting Evidence

- EVD-VIT-003
- EVD-VIT-004
- EVD-VIT-007
- EVD-VIT-008

### Counter Evidence

Some confusion families may remain stable across wide condition ranges.

### Confidence

High

## LAW-VIT-005: Structured Error

### Hypothesis

Visual recognition noise produces patterned, asymmetric errors rather than uniform random substitution.

### Prediction

Full confusion matrices will provide more predictive value than overall accuracy.

### Supporting Evidence

- EVD-VIT-003
- EVD-VIT-004
- EVD-VIT-007

### Counter Evidence

At extreme degradation, responses may approach chance and lose structure.

### Confidence

High

## LAW-VIT-006: Interference Tradeoff

### Hypothesis

Reducing local interference can increase other costs such as peripheral extent, grouping loss, or navigation effort.

### Prediction

Spacing and separation interventions will have non-monotonic performance curves.

### Supporting Evidence

- EVD-VIT-002
- EVD-VIT-005
- EVD-VIT-006

### Counter Evidence

Specific impaired populations or tasks may benefit across a larger monotonic range.

### Confidence

High

## LAW-VIT-007: Degradation Robustness

### Hypothesis

A useful measure of symbol-system quality is the rate at which task-relevant information is lost as controlled noise increases.

### Prediction

Area under the information-retention curve will distinguish systems that have similar performance under ideal conditions.

### Supporting Evidence

- EVD-VIT-002
- EVD-VIT-004

### Counter Evidence

The choice and distribution of noise conditions may dominate the resulting ranking.

### Confidence

Moderate

## LAW-VIT-008: Learned Channel

### Hypothesis

The observer's perceptual history changes the effective communication channel.

### Prediction

Expert and novice confusion networks will differ even when physical stimuli are identical.

### Supporting Evidence

- EVD-VIT-003
- EVD-VIT-008

### Counter Evidence

Some low-level effects may remain largely independent of expertise.

### Confidence

High

## LAW-VIT-009: Task-Weighted Information

### Hypothesis

Visual information should be measured as reduction of task-relevant uncertainty rather than physical detail or unweighted symbol accuracy.

### Prediction

Frequency- and consequence-weighted metrics will predict real-world errors better than equal-weight alphabet scores.

### Supporting Evidence

- EVD-VIT-001
- theoretical consequence of confusion and language distributions

### Counter Evidence

Direct empirical validation across tasks is still needed.

### Confidence

Moderate

## LAW-VIT-010: Redundancy Allocation

### Hypothesis

Effective visual systems distribute redundancy across glyph shape, spacing, word structure, hierarchy, and context.

### Prediction

Removing redundancy at one layer will increase reliance on other layers and disproportionately harm contexts where those layers are absent.

### Supporting Evidence

- EVD-VIT-002
- EVD-VIT-005

### Counter Evidence

The relative contribution of each redundancy source remains uncertain.

### Confidence

Moderate

## LAW-VIT-011: Local-to-System Non-Transfer

### Hypothesis

Improvement in isolated symbol recognition does not guarantee improvement in continuous reading or task performance.

### Prediction

Some fonts optimized for pairwise distinctiveness will improve acuity or peripheral recognition without increasing reading speed.

### Supporting Evidence

- EVD-VIT-009

### Counter Evidence

Other optimizations may transfer when the improved component is the dominant bottleneck.

### Confidence

High

------------------------------------------------------------------------

# Proposed Metrics

## MET-VIT-001: Recognition Information

Mutual information between presented and reported symbols.

Unit: bits

## MET-VIT-002: Information Retention Ratio

```text
observed recognition information / maximum possible information
```

Unit: proportion

## MET-VIT-003: Robustness Area

Area under the information-retention curve across a specified degradation distribution.

Unit: condition-weighted bits

## MET-VIT-004: Critical Information Threshold

Noise level at which recognition information falls below a task-defined minimum.

## MET-VIT-005: Weakest-Pair Margin

Performance of the most consequential high-confusion pair.

## MET-VIT-006: Context Recovery Gain

Difference between isolated-symbol and contextual recognition information.

## MET-VIT-007: Frequency-Weighted Confusion Cost

Pairwise error probability weighted by symbol or sequence occurrence.

## MET-VIT-008: Consequence-Weighted Confusion Cost

Pairwise error probability weighted by the real-world severity of substitution.

## MET-VIT-009: Layer Transfer Ratio

System-level improvement divided by component-level improvement.

A low ratio indicates that local gains did not transfer.

## MET-VIT-010: Adaptation Spread

Variation in performance across observer groups, devices, environments, or user overrides.

------------------------------------------------------------------------

# Research Program

## Phase 1: Reanalyze Existing Confusion Data

Use published confusion matrices where available.

Calculate:

- mutual information
- entropy remaining
- asymmetric pairwise errors
- graph clusters
- weakest-pair margins
- frequency-weighted cost

This requires no new human experiment.

## Phase 2: Computational Degradation

Render open-source typefaces under controlled transformations:

- blur
- contrast reduction
- downsampling
- stroke erosion
- ink spread
- peripheral-vision approximations
- neighboring-letter crowding

Use multiple machine observers as proxies, while explicitly treating them as hypothesis generators rather than human replacements.

## Phase 3: Language-Aware Modeling

Weight glyph and pair confusions using:

- letter frequency
- bigram frequency
- word frequency
- task-specific vocabularies
- consequence matrices

Compare ordinary prose against low-context tasks such as serial numbers and medical labels.

## Phase 4: Cross-Layer Transfer Review

Collect studies that separately report:

- letter recognition
- word recognition
- reading speed
- eye movements
- comprehension
- task completion

Estimate when component improvements transfer and when they do not.

## Phase 5: Atlas Visual Channel Simulator

Build a system that accepts:

```yaml
observer:
  vision_model:
  familiarity:
  language:
task:
  type:
  vocabulary:
  error_consequence:
environment:
  distance:
  contrast:
  glare:
  display:
typography:
  typeface:
  size:
  spacing:
  weight:
  line_length:
```

and returns predicted risk rather than a universal readability score.

------------------------------------------------------------------------

# Open Questions

- Which published letter-confusion datasets provide complete trial-level or matrix data suitable for reanalysis?
- Which degradation transformations best approximate human perceptual noise without falsely claiming biological fidelity?
- Can a small set of geometric or neural features predict confusion-network changes across multiple fonts?
- How much practical predictive improvement comes from frequency weighting?
- What is the best metric for a task where one rare error is catastrophic?
- How should Atlas combine mutual information with response time?
- Can hierarchy and grouping be represented with confusion-style uncertainty models?
- What forms of visual redundancy improve comprehension without adding clutter?
- Does perceptual fluency predict aesthetic preference after controlling for familiarity?
- Where does deliberate visual friction improve attention, memory, or meaning?
- Are mature alphabets near local optima, or primarily locked in by cultural and educational switching costs?
- How should handwriting and production efficiency enter alphabet fitness?
- Can visual channel capacity be estimated for complex layouts, or only for narrowly defined symbol tasks?

------------------------------------------------------------------------

# Next Actions

- Obtain complete published letter-confusion matrices and visual-span datasets.
- Build a standard schema for stimulus, condition, response, and observer metadata.
- Calculate information-theoretic metrics from at least two existing datasets.
- Create a degradation benchmark for a small representative typeface set.
- Separate prose, interface-label, serial-number, and safety-critical task models.
- Add visual-information nodes to the Project Atlas genome map.
- Review research on processing fluency and aesthetics without assuming functional efficiency explains beauty.
- Investigate sign and icon comprehension as a parallel symbol system.

------------------------------------------------------------------------

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 1.0 | 2026-07-18 | OpenAI | Initial research foundation connecting typography, information theory, visual perception, and task performance. |

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
11. Do not use Shannon information as a synonym for meaning or comprehension.
12. State the observer, task, alternatives, and viewing conditions for every visual-information claim.
13. Preserve full confusion structures where possible rather than reporting only accuracy.
14. Treat computational observers as screening tools unless validated against human data.
15. Distinguish component-level improvements from system-level outcomes.
