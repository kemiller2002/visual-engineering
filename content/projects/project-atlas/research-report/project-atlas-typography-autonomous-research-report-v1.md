---
id: RR-VE-ATL-2026-3070
authors:
  - OpenAI
confidence: Moderate-High
date: 2026-07-19
llm_ingest: true
machine_readable: true
project: project-atlas
research_mode: autonomous hypothesis cycles
status: completed research phase
summary: >
  Multi-cycle investigation of typography as a constrained visual communication
  system. The work tested information-theoretic, geometric, crowding, spacing,
  contextual-inference, familiarity, and functional-aesthetic hypotheses. The
  strongest conclusion is that typography cannot be optimized with one global
  readability score. It is an adaptive, layered inference system whose active
  bottleneck changes with observer, environment, task, language context, and
  consequence of error. Glyph-level robustness remains measurable and important,
  but improvements transfer upward only when glyph recognition is the limiting
  factor. The proposed Atlas model therefore combines conditional confusion
  networks, degradation curves, language redundancy, eye-movement adaptation,
  and task-weighted error costs.
version: 1.0
purposes:
  - integrate
  - verify
audiences:
  - executive
  - practitioner
  - researcher
---

# Executive Summary

## What Was Accomplished

This research phase moved the Project Atlas typography work beyond the initial
claim that typography resembles a noisy communication channel.

Seven investigation cycles were completed:

1. Tested whether Shannon information theory can serve as a complete theory of
   typography.
2. Tested whether geometric distinctiveness predicts human letter recognition.
3. Tested whether crowding and visual-span size are the primary bottlenecks in
   reading.
4. Tested whether increased spacing, width, weight, and size improve reading in
   predictable directions.
5. Tested whether words and linguistic context function as error-correcting
   redundancy.
6. Tested whether familiarity and reader expertise are secondary biases or
   fundamental channel variables.
7. Tested whether aesthetic quality can be derived from communication efficiency.

The research repeatedly attempted to falsify the emerging model. Several
plausible claims failed, and those failures materially changed the framework.

## Major Discoveries

### 1. Typography is not one communication channel

It is a layered and adaptive system:

```text
Rendered signal
    ↓
Perceptual availability
    ↓
Glyph discrimination
    ↓
Letter-position recovery
    ↓
Word inference
    ↓
Sentence interpretation
    ↓
Structural navigation
    ↓
Decision and action
```

A design can succeed at one layer and fail at another.

### 2. Glyph robustness is measurable, but it is not sufficient

Human letter-confusion matrices can be converted into mutual information and
information-retention curves. Geometric image descriptors predict a meaningful
portion of human confusions near recognition limits.

However, better isolated-letter recognition does not reliably produce faster
continuous reading. The improvement transfers only when symbol recognition is
the dominant bottleneck.

### 3. Reading is active inference, not passive decoding

Readers combine uncertain visual evidence with:

- letter-frequency expectations
- orthographic legality
- word probability
- syntax
- semantics
- familiarity
- eye-movement resampling

The reader does not wait for every letter to become certain before understanding
a word or sentence.

### 4. Typography variables have conditional optima

Spacing, width, weight, and size do not behave as monotonic goods.

Examples:

- More letter spacing can reduce crowding but increase peripheral extent and
  weaken word grouping.
- Bolder text can improve stroke visibility until counters and apertures begin
  to close, yet several studies find no reading-speed gain across a wide range
  of boldness.
- Condensed text changes fixation behavior without necessarily reducing overall
  reading speed because readers compensate.
- Print size has a strong threshold effect, but above critical print size there
  is a broad fluent range rather than one ideal size.

### 5. The governing variable is the active bottleneck

Typography should not be optimized by maximizing every favorable feature.

The highest-value intervention is the one that addresses the weakest active
layer for a defined observer, task, and environment.

### 6. Error cost matters as much as average accuracy

Ordinary prose can tolerate substantial letter-level uncertainty because
language provides redundancy.

Serial numbers, medication names, passwords, safety instructions, unfamiliar
names, and short labels provide much less contextual correction. They require
stronger glyph-level distinction.

### 7. Familiarity is part of the system

Readers become perceptually tuned to conventional letter structures and familiar
typefaces. Familiarity can compensate for some geometric weakness and can make
an initially inferior typeface perform better after exposure.

This means a typeface cannot be represented only by its geometry.

### 8. Beauty is not reducible to efficiency

Processing fluency may contribute to preference, but typography also serves
expression, identity, historical reference, status, tension, novelty, and
deliberate friction.

The hypothesis that beautiful typography is simply efficient typography was
rejected.

## Overall Confidence

**Moderate to high** for the layered bottleneck model, conditional-optimum model,
contextual error-correction model, and distinction between component performance
and system performance.

**Moderate** for the proposed quantitative integration of confusion networks,
language priors, eye-movement cost, and consequence weighting. The components
are supported independently, but a unified predictive model has not yet been
validated.

## Largest Remaining Uncertainty

The largest unresolved question is not whether typography affects reading. It is:

> Under which conditions does a measurable glyph-level improvement transfer to
> word recognition, reading speed, comprehension, navigation, or action?

That transfer function is the highest-value target for the next research phase.

---

# Research Log

## Cycle 1: Can Shannon Information Theory Explain Typography?

### Objective

Determine whether typography can be modeled directly as an information channel
and whether information transmitted can serve as a global measure of quality.

### Hypothesis

A typographic system can be evaluated by the amount of information that survives
transmission from source to reader.

### Evidence Found

Claude Shannon's communication model provides rigorous concepts for:

- entropy
- conditional entropy
- mutual information
- redundancy
- noise
- channel capacity
- decoding error

Visual-span researchers have applied information-theoretic calculations to
letter recognition. Confusion matrices can be converted into bits transmitted,
giving a direct measure of how much uncertainty about a presented letter remains
after an observer responds.

Roger Levy's noisy-channel model of sentence comprehension demonstrates that
language understanding can be modeled as inference over uncertain input. The
reader combines a prior probability over intended messages with a likelihood
model describing how the input may have been corrupted.

Rational eye-movement models similarly treat each fixation as an information
gathering decision under uncertainty.

### Evidence Against

Shannon explicitly excluded semantics from the engineering definition of
information.

A perfectly transmitted sentence can still be:

- incomprehensible
- ambiguous
- false
- irrelevant
- badly organized
- behaviorally ineffective

Character-level mutual information therefore cannot measure comprehension,
persuasion, trust, navigation, or task success.

No evidence supports collapsing all visual communication outcomes into one
information score.

### Sources

- Shannon, C. E. (1948), *A Mathematical Theory of Communication*
- Legge et al. (2007), *The Case for the Visual Span as a Sensory Bottleneck in
  Reading*
- Levy, R. (2008), *A Noisy-Channel Model of Rational Human Sentence
  Comprehension under Uncertain Input*
- Bicknell, K., and Levy, R. (2010), *A Rational Model of Eye Movement Control
  in Reading*

### Analysis

The information-theory analogy survives, but only when the represented
uncertainty is explicitly defined.

At the glyph layer, the alternatives are letters and the output is an observer's
response. Mutual information is directly applicable.

At the semantic layer, the alternatives are possible meanings or intended
messages. A different model is required.

At the task layer, uncertainty concerns actions and outcomes.

The word *information* is therefore not one quantity across the system. Each
layer has its own state space, priors, evidence, and loss function.

### Conclusion

Shannon theory is a valid quantitative foundation for symbol transmission but
not a complete theory of typography.

### Confidence

High

### Next Step

Test whether physical geometry can predict the human confusion channel well
enough to support scalable computational analysis.

---

## Cycle 2: Does Geometric Distinctiveness Predict Letter Recognition?

### Objective

Determine whether letter recognition can be predicted from measurable image
geometry rather than traditional type-anatomy labels.

### Hypothesis

Human letter confusions are largely determined by pairwise geometric similarity.
A sufficiently good image descriptor should predict which letters are confused.

### Evidence Found

Liu and colleagues found that low-order geometric moments explained a substantial
portion of human letter-confusion patterns near the acuity limit.

Geometric moments represent broad properties such as:

- area
- centroid
- orientation
- spread
- symmetry
- coarse distribution of visual mass

This supports the idea that recognition depends partly on global structure rather
than only on named anatomy such as serif, bowl, or terminal.

Image-descriptor studies of Chinese characters also found associations among
stroke count, size, type style, and legibility thresholds.

Human similarity matrices show stable clusters that often correspond to obvious
structural relationships.

### Evidence Against

Mueller and Weidemann showed that observed identification reflects at least:

- perceivability
- similarity
- response bias

Geometric similarity alone cannot explain why some symbols are selected more
often as guesses or why one direction of a confusion is stronger than the
reverse.

Reader expertise changes perceived similarity. Mirror-letter sensitivity, for
example, changes with reading acquisition.

Clear-view similarity ratings are not equivalent to actual confusion under
blur, crowding, or peripheral presentation.

Local diagnostic features can preserve categorical identity even when overall
shapes are similar.

### Sources

- Liu et al. (2009), *Using Geometric Moments to Explain Human Letter
  Recognition Near the Acuity Limit*
- Mueller and Weidemann (2012), *Alphabetic Letter Identification: Effects of
  Perceivability, Similarity, and Bias*
- Simpson et al. (2013), *A Letter Visual-Similarity Matrix for Latin-Based
  Alphabets*
- Wiley et al. (2016), *The Effects of Alphabet and Expertise on Letter
  Perception*
- Duñabeitia et al. (2013), *The Influence of Reading Expertise in
  Mirror-Letter Perception*

### Analysis

Geometry is neither irrelevant nor sufficient.

The strongest model is:

```text
Observed confusion
=
physical similarity
× feature survival
× target perceivability
× learned category structure
× response bias
× condition
```

The multiplication is conceptual. Some variables may combine additively or
interactively in an empirical model.

Traditional anatomy terms remain useful only when they identify a feature whose
presence, absence, or degradation changes confusion probability.

The scientifically relevant unit is not "open aperture" by itself. It is the
change in conditional discriminability produced by changing the aperture under
a defined condition.

### Conclusion

Geometric models are useful screening tools and can explain meaningful variance,
but no observer-independent glyph-distance metric is currently justified.

### Confidence

High

### Next Step

Investigate the dominant form of visual interference in connected text:
crowding and visual-span limits.

---

## Cycle 3: Is Crowding the Primary Bottleneck in Reading?

### Objective

Test whether visual crowding and the size of the visual span causally determine
reading speed.

### Hypothesis

Reading speed is primarily limited by the number of uncrowded letters available
during each fixation. Reducing crowding should enlarge the visual span and
increase reading speed.

### Evidence Found

Pelli and colleagues found that reading rate scales with an uncrowded span across
eccentricities.

Legge and colleagues reported strong relationships among crowding, visual-span
size, and reading speed.

Crowding is a clear perceptual phenomenon: a letter that is identifiable in
isolation can become unidentifiable when surrounded by nearby letters.

Research identifies several potential mechanisms:

- feature pooling
- feature substitution
- positional uncertainty
- feature migration
- source confusion

Critical spacing often scales with eccentricity, a relationship commonly
associated with Bouma's law.

### Evidence Against

Training studies reduced crowding without producing proportionate or consistent
improvements in visual span and reading speed.

He and colleagues explicitly challenged a simple causal chain from crowding to
visual span to reading.

Fonts designed to reduce inter-letter similarity improved peripheral letter and
word recognition but did not consistently improve sentence-reading speed.

Temporal-processing training has improved rapid serial visual presentation
reading, showing that non-spatial bottlenecks can also matter.

Reading models must also account for eye-movement control, lexical access,
attention, temporal processing, language inference, and motor planning.

### Sources

- Pelli et al. (2007), *Crowding and Eccentricity Determine Reading Rate*
- Legge et al. (2007), *The Case for the Visual Span as a Sensory Bottleneck in
  Reading*
- He et al. (2017), *Linking Crowding, Visual Span, and Reading*
- He et al. (2013), *Sensory and Cognitive Influences on Training-Related
  Improvement in Crowded-Letter Recognition*
- Bernard et al. (2016), *A New Font Specifically Designed for Peripheral
  Vision*
- Chung (2021), *Training to Improve Temporal Processing of Letters Benefits
  Reading Speed*

### Analysis

Crowding is a real constraint, but "the primary bottleneck" is too strong as a
universal claim.

The causal relationship appears conditional:

```text
If crowding is the active bottleneck:
    reducing crowding can improve reading

If lexical, temporal, attentional, oculomotor, or semantic processing is limiting:
    reducing crowding may produce little transfer
```

This explains why component-level gains sometimes fail at the system level.

Visual span should be treated as a performance summary produced by multiple
mechanisms, not automatically as one independent causal variable.

### Conclusion

The hypothesis that crowding universally determines reading speed was rejected.
Crowding is one of several possible active bottlenecks.

### Confidence

Moderate to high

### Next Step

Test the practical design variables most often used to reduce crowding and
increase visibility: spacing, width, weight, and size.

---

## Cycle 4: Do More Space, Width, Weight, and Size Consistently Improve Reading?

### Objective

Determine whether common legibility interventions behave monotonically.

### Hypotheses

1. Increased letter spacing improves reading by reducing crowding.
2. Wider letters improve reading by increasing distinctive area.
3. Bolder strokes improve reading by increasing visibility.
4. Larger print improves reading continuously.

### Evidence Found

Print size has a clear lower threshold. Legge's review identified a broad fluent
range beginning near an x-height of approximately 0.2 degrees of visual angle
for normally sighted readers.

Increased spacing can reduce local crowding.

Narrow, highly condensed forms can alter eye-movement behavior and increase
fixation duration.

Some low-vision and dyslexic populations benefit from enlarged spacing under
specific conditions.

Recent cognitive and perceptual reviews generally warn against:

- extremely condensed fonts
- extreme weights
- narrow spacing
- closed counters
- excessive embellishment

### Evidence Against

Yu and colleagues found that increasing letter spacing creates competing effects.
It reduces crowding but pushes letters farther into peripheral vision, where
acuity and positional precision are worse.

Vinckier and colleagues found nonlinear deterioration when letters became
sufficiently separated, consistent with damage to multi-letter or bigram
processing.

Studies of boldness found that reading speed was often invariant across a broad
range and could decrease when internal spaces became too small.

Chung found that bolder print did not improve reading speed for people with
central vision loss.

Minakata found that an ultra-condensed font altered fixation and saccade
patterns, yet overall reading speed remained similar because readers adapted.

Above critical print size, larger type does not continuously increase maximum
reading speed. There is a wide fluent plateau.

Dyslexie and similar specialized fonts have repeatedly failed to produce
consistent superiority over conventional fonts when size and spacing are
controlled.

### Sources

- Legge and Bigelow (2011), *Does Print Size Matter for Reading?*
- Yu et al. (2007), *Effect of Letter Spacing on Visual Span and Reading Speed*
- Vinckier et al. (2011), *The Impact of Letter Spacing on Reading*
- Bernard et al. (2013), *The Effect of Letter-Stroke Boldness on Reading Speed*
- Chung (2018), *Bolder Print Does Not Increase Reading Speed in People with
  Central Vision Loss*
- Xiong et al. (2018), *Fonts Designed for Macular Degeneration*
- Minakata and Beier (2021), *The Effect of Font Width on Eye Movements During
  Reading*
- Duranovic et al. (2018), spacing and Dyslexie-font study

### Analysis

Each variable exhibits a useful range bounded by competing failure modes.

```text
Letter spacing:
crowding ← useful range → fragmentation and eccentricity cost

Weight:
stroke dropout ← useful range → counter closure and fusion

Width:
compressed features ← useful range → excessive line length and scan cost

Size:
subcritical recognition ← fluent plateau → layout and navigation cost
```

This is a general pattern of constrained optimization.

The correct question is not "Is more better?" It is:

> Which failure boundary is currently closer for this observer, task, and
> environment?

### Conclusion

All four monotonic hypotheses were rejected.

Typography variables should be modeled with condition-dependent response curves
and safe operating ranges.

### Confidence

High

### Next Step

Investigate how linguistic structure repairs uncertain visual input and changes
the required level of glyph distinction.

---

## Cycle 5: Does Language Function as Error-Correcting Redundancy?

### Objective

Determine whether word and sentence context systematically compensate for weak
or degraded letter signals.

### Hypothesis

Readers use higher-level linguistic probability to correct lower-level visual
uncertainty. Typography therefore operates within an error-correcting language
system.

### Evidence Found

The word-superiority effect has been replicated across a long experimental
history: under brief or degraded presentation, a letter can be identified more
accurately inside a familiar word than in an unrelated string or isolation.

Pseudoword and acronym superiority effects show that the benefit is not limited
to semantic familiarity. Orthographic legality, learned sequence structure, and
familiar multi-letter units also contribute.

Sentence-superiority work suggests that still-higher context can improve
post-cued letter identification.

Noisy-channel models of comprehension formally combine:

```text
P(intended message)
×
P(observed input | intended message)
```

Low-vision research found that removing spaces caused larger reading-speed
losses in low vision than normal vision, indicating that segmentation and
linguistic inference become more important when bottom-up visual quality is
reduced.

### Evidence Against

Context can produce confident misperception when prior expectations dominate
weak evidence.

Word-superiority effects depend on experimental design and have generated
methodological debate, particularly around post-cue procedures and response
alternatives.

Context provides little protection in:

- random strings
- account numbers
- medication names
- uncommon proper names
- passwords
- short labels
- unfamiliar technical notation
- safety codes

Language redundancy can therefore hide typography defects in prose while leaving
high-consequence low-context tasks vulnerable.

### Sources

- Grainger (2024), *Letters, Words, Sentences, and Reading*
- Jordan et al. (2024), Reicher-Wheeler paradigm review
- Coch et al. (2010), word and pseudoword superiority
- Laszlo and Federmeier (2007), acronym superiority
- Massol et al. (2025), word and sentence superiority
- Levy (2008), noisy-channel sentence comprehension
- Sass et al. (2006), linguistic inference and low-vision reading speed

### Analysis

Language is not merely downstream of typography. It feeds back into visual
interpretation.

The typography channel should therefore be modeled using Bayesian inference:

```text
Posterior intended symbol or word
∝
visual likelihood
×
linguistic prior
```

The strength of the prior changes by task.

For ordinary prose:

```text
high contextual redundancy
→ moderate glyph ambiguity may be recoverable
```

For identifiers:

```text
low contextual redundancy
→ glyph ambiguity directly raises error risk
```

A universal font ranking that ignores message distribution is therefore
fundamentally incomplete.

### Conclusion

Language behaves like an error-correcting layer, but it can both repair and
distort uncertain visual evidence.

### Confidence

High

### Next Step

Determine whether familiarity is just another prior or whether it changes
perceptual encoding itself.

---

## Cycle 6: Is Familiarity Merely Bias, or Does It Alter the Effective Channel?

### Objective

Determine whether typeface familiarity and reading expertise should be treated
as external preferences or as part of the perceptual system.

### Hypothesis

Familiarity changes priors but leaves bottom-up visual encoding unchanged.

### Evidence Found

Eye-tracking research found faster reading for familiar letter structures and
improvement with exposure to initially unfamiliar typefaces.

Expertise in a writing system changes letter-similarity judgments.

Reading acquisition changes sensitivity to mirror forms, suggesting that learned
categories reshape visual discrimination.

Familiarity effects are consistent with more efficient mappings among visual
features, letter identities, and lexical representations.

### Evidence Against

Some near-threshold geometric and crowding effects persist regardless of
expertise.

Exposure cannot rescue a signal that falls below perceptual availability.

A familiar typeface is not universally superior. Under severe degradation, a
more structurally robust unfamiliar typeface may eventually outperform it.

The evidence does not support reducing all familiarity effects to response bias.
Some changes appear perceptual or representational.

### Sources

- Nedeljković et al. (2020), *You Read Best What You Read Most*
- Wiley et al. (2016), alphabet expertise and letter perception
- Duñabeitia et al. (2013), mirror-letter perception
- Mueller and Weidemann (2012), perceivability, similarity, and bias

### Analysis

The original hypothesis was too narrow.

Familiarity likely acts at multiple layers:

```text
response prior
+
feature weighting
+
category boundary tuning
+
letter-to-word mapping efficiency
+
expectation about legal variation
```

The effective channel therefore includes observer history.

This creates a switching-cost problem. A theoretically superior redesign may
perform poorly at first because it invalidates learned perceptual models.

Mature alphabets and common typefaces may persist partly because replacement
requires retraining millions of observers, not because the existing forms are
globally optimal.

### Conclusion

Familiarity is both prior knowledge and learned perceptual tuning. It is a core
channel variable, not a nuisance to be removed from the model.

### Confidence

High

### Next Step

Test the tempting claim that efficient communication naturally produces beauty.

---

## Cycle 7: Is Typographic Beauty an Emergent Property of Efficient Transmission?

### Objective

Determine whether aesthetic quality can be explained primarily by perceptual
fluency or functional efficiency.

### Hypothesis

Typography appears beautiful when it transmits information efficiently under
human perceptual constraints.

### Evidence Found

Processing fluency research broadly suggests that easier perceptual processing
can increase positive affect and preference.

Typeface shape has been associated with pleasantness and emotional experience.

Coherent proportions, predictable rhythm, and clear differentiation can produce
both functional and aesthetic benefits.

Some features that improve robustness, such as balanced counters and controlled
stroke contrast, can also appear harmonious.

### Evidence Against

Expressive typography often works by increasing friction, ambiguity, novelty, or
tension.

Display typography may prioritize:

- identity
- historical association
- luxury signaling
- emotional tone
- memorability
- ceremonial weight
- disruption

A familiar, fluent typeface can be perceived as boring or inappropriate.

A difficult typeface can be aesthetically successful when the difficulty
supports the intended meaning.

Evidence connecting fluency with preference does not establish that beauty is
nothing more than efficiency.

### Sources

- Medved et al. (2023), typeface shape, reading speed, and pleasantness
- Processing-fluency literature summarized in perceptual and aesthetic research
- Typography and emotional-connotation studies

### Analysis

Functional fluency is one aesthetic input among several.

A better model separates:

```text
instrumental fitness
    accurate, fast, sustainable communication

expressive fitness
    appropriate identity, emotion, and cultural meaning
```

These objectives can align, conflict, or be intentionally traded.

A warning label and a film title should not be optimized with the same loss
function.

### Conclusion

The efficiency-equals-beauty hypothesis was rejected.

Beauty cannot be inferred from channel robustness alone.

### Confidence

High

### Next Step

Integrate the surviving findings into a predictive model and identify the
research needed to validate it.

---

# Confirmed Findings

The following findings are supported by multiple strong sources or well-established
experimental traditions.

## CF-001: Print size has a critical threshold and a broad fluent range

Reading speed deteriorates below a critical print size. Above that threshold,
normally sighted readers can maintain near-maximum speed over a broad range.

**Confidence:** High

## CF-002: Crowding impairs identification of otherwise visible letters

The presence and spacing of neighboring symbols can reduce recognition without
making the target disappear.

**Confidence:** High

## CF-003: Letter spacing has competing effects

Spacing can reduce crowding while increasing eccentricity, line length,
fixation demand, and word fragmentation.

**Confidence:** High

## CF-004: Human letter errors are structured and asymmetric

Confusions cluster around particular relationships and can be directional.
Average accuracy loses important information.

**Confidence:** High

## CF-005: Geometry predicts part, but not all, of letter confusion

Global image descriptors and feature relationships explain meaningful variance
in human recognition.

**Confidence:** High

## CF-006: Reader familiarity affects reading and letter perception

Typeface exposure and alphabet expertise alter performance and perceived
similarity.

**Confidence:** High

## CF-007: Context improves recovery of uncertain letters

Word, pseudoword, acronym, and sentence structure can improve letter
identification under constrained conditions.

**Confidence:** High

## CF-008: Component gains do not guarantee system gains

Improvements in isolated-letter or peripheral recognition may fail to improve
continuous reading speed.

**Confidence:** High

## CF-009: Extreme values are usually harmful

Extreme condensation, weight, spacing, embellishment, or insufficient size tend
to increase recognition or reading costs.

**Confidence:** Moderate to high

## CF-010: Task determines the required level of glyph distinction

Low-context and high-consequence tasks depend more heavily on character-level
legibility than ordinary prose.

**Confidence:** Moderate to high

---

# Rejected Hypotheses

## RH-001: Shannon information is a complete measure of typographic quality

Rejected because symbol transmission does not measure meaning, comprehension,
navigation, trust, or action.

## RH-002: A glyph has one intrinsic recognition distance from every other glyph

Rejected because confusability changes with typeface, scale, degradation,
neighbors, observer, and task.

## RH-003: Maximum alphabetic distinctiveness is universally optimal

Rejected because extreme distinctiveness can damage coherence, learnability,
spatial efficiency, writing efficiency, and familiarity.

## RH-004: Crowding is the universal causal bottleneck in reading

Rejected because reducing crowding does not always improve visual span or reading
speed, and temporal, lexical, attentional, and oculomotor constraints also matter.

## RH-005: More letter spacing always improves reading

Rejected because the benefit reverses when spacing creates peripheral,
navigation, or grouping costs.

## RH-006: Bolder type reliably improves reading speed

Rejected because reading speed is frequently invariant across useful boldness
ranges and can worsen when counters and apertures close.

## RH-007: Wider type necessarily produces faster reading

Rejected because readers adapt their eye movements and because width introduces
line-length and spatial-efficiency costs.

## RH-008: Larger print continuously improves reading

Rejected because performance reaches a fluent plateau above critical print size.

## RH-009: Specialized dyslexia fonts are generally superior

Rejected because controlled studies do not show consistent performance benefits
over conventional fonts once spacing, familiarity, and size are considered.

## RH-010: Better isolated-letter recognition automatically improves reading

Rejected by font and training studies showing weak or absent upward transfer.

## RH-011: Familiarity is merely response bias

Rejected because expertise appears to alter perceptual organization and category
sensitivity.

## RH-012: Beauty is efficient communication made visible

Rejected because expressive typography can succeed through deliberate friction
and because aesthetics includes cultural and semantic objectives beyond fluency.

---

# Open Questions

Ranked by expected importance.

## 1. What predicts upward transfer?

Which glyph-level improvements produce measurable gains in:

- word recognition
- reading speed
- comprehension
- navigation
- task success

This is the central unresolved problem.

## 2. Can active bottlenecks be diagnosed without extensive human testing?

Atlas needs a practical method for predicting whether a design is limited by:

- size
- contrast
- crowding
- glyph similarity
- temporal exposure
- eye movement
- lexical access
- hierarchy
- comprehension

## 3. Is there a stable cross-condition confusion backbone?

Some letter pairs may remain vulnerable across many fonts and degradation types,
while others are typeface-specific.

Identifying the stable backbone would separate alphabet constraints from design
defects.

## 4. How should human and computational observer data be combined?

The optimal proxy may be an ensemble of:

- geometric descriptors
- optical degradation
- neural classifiers
- language models
- human confusion data

The validation procedure remains unresolved.

## 5. What is the correct task-weighted loss function?

Average recognition is inappropriate when one rare error is catastrophic.

Atlas needs separate models for:

- prose
- interface labels
- safety signage
- medication names
- codes and identifiers
- low-vision reading
- peripheral monitoring

## 6. How do eye movements conceal typographic weakness?

Readers can compensate with longer fixations, regressions, and altered saccades
while maintaining average reading speed.

Speed alone may underestimate cost.

## 7. How should comfort and fatigue be measured?

Short laboratory tasks may miss cumulative effort, headache, abandonment, and
reduced persistence.

## 8. Which variable interactions are genuinely causal?

Examples:

- x-height × width
- weight × aperture
- spacing × eccentricity
- line length × line spacing
- familiarity × degradation
- context × character ambiguity

## 9. How transferable are Latin-alphabet results?

Writing systems differ in:

- symbol inventory
- visual density
- connectedness
- diacritics
- spatial arrangement
- morphology
- word-boundary marking

## 10. Can expressive fitness be modeled without reducing it to preference?

A framework is needed for emotional appropriateness, identity, historical
association, and intentional friction.

---

# Emerging Patterns

## Pattern 1: Safe Operating Ranges

Many typography variables have two failure boundaries rather than one ideal
point.

```text
insufficient value ← viable range → excessive value
```

This pattern appears in:

- size
- spacing
- weight
- width
- contrast
- line length
- line spacing
- hierarchy strength

### Why It Matters

Design guidance should specify ranges, boundary conditions, and failure signals
rather than fixed universal values.

---

## Pattern 2: Local Improvement, Global Compensation

Readers compensate for weak typography through:

- longer fixations
- additional fixations
- regressions
- linguistic inference
- familiarity
- slower decision-making

### Why It Matters

Stable reading speed does not prove equivalent usability. A design may preserve
output by consuming more cognitive or oculomotor resources.

---

## Pattern 3: Redundancy Is Distributed Across Layers

Redundancy exists in:

- distinctive glyph features
- letter spacing
- word structure
- syntax
- semantics
- hierarchy
- layout
- repeated conventions

### Why It Matters

Removing redundancy from one layer increases dependence on the others. Designs
must be more locally explicit when higher-level context is weak.

---

## Pattern 4: The Observer Is Inside the System

Vision, age, expertise, language, familiarity, fatigue, and strategy change the
effective channel.

### Why It Matters

The same rendered typography is not one stimulus in functional terms. It creates
different evidence for different readers.

---

## Pattern 5: Performance Curves Matter More Than Rankings

Two typefaces may perform equally under ideal conditions but diverge rapidly as
noise increases.

### Why It Matters

Robustness across realistic degradation is more useful than a single laboratory
score.

---

## Pattern 6: Errors Have Unequal Consequences

A substitution in a predictable sentence may be harmless. A substitution in a
drug name or account number may be severe.

### Why It Matters

Typography evaluation needs decision theory as well as perception science.

---

## Pattern 7: Mature Forms Reflect Both Optimization and Lock-In

Conventional alphabets and common typefaces may combine:

- perceptual fitness
- production history
- cultural inheritance
- educational investment
- switching costs

### Why It Matters

Historical survival is evidence of viability, not proof of global optimality.

---

## Pattern 8: Typography Is a Control System, Not a Static Object

Readers sample, infer, verify, regress, and update.

### Why It Matters

A static screenshot cannot fully represent reading. The system unfolds over time.

---

# Proposed Models

## Model 1: Atlas Conditional Communication Model

```text
Performance =
f(
  rendered signal,
  degradation,
  observer,
  familiarity,
  language context,
  task,
  eye-movement strategy,
  error consequence
)
```

### Assumptions

- Performance is conditional rather than intrinsic.
- Different outcomes require different metrics.
- Variables interact.
- The reader adapts during the task.

---

## Model 2: Active Bottleneck Model

Overall performance is constrained by the weakest currently active process.

Candidate bottlenecks:

```text
rendering
perceptual availability
crowding
glyph discrimination
position coding
temporal processing
lexical inference
eye movement
working memory
semantic interpretation
navigation
action selection
```

### Prediction

Improving a non-limiting component produces little system gain.

### Assumption

The system may have multiple simultaneous bottlenecks rather than exactly one.

---

## Model 3: Conditional Confusion Network

Each symbol is a node. Directed edges represent substitution probability.

```text
edge(i → j) =
P(response j | target i, condition)
```

Network properties:

- incoming confusion
- outgoing confusion
- asymmetry
- clusters
- weakest consequential pair
- stability across degradation

### Assumption

The matrix must retain stimulus and observer metadata.

---

## Model 4: Robustness Curve

For degradation level `n`:

```text
R(n) = I(Target; Response | n) / H(Target)
```

Candidate summary measures:

- critical information threshold
- degradation slope
- area under the curve
- weakest-pair threshold
- population variance

### Assumption

The chosen degradation distribution must match the intended environment.

---

## Model 5: Bayesian Reading Model

```text
P(intended text | visual evidence)
∝
P(visual evidence | intended text)
×
P(intended text | language and context)
```

Typography changes the likelihood term.

Language, familiarity, and context change the prior.

### Prediction

Typography improvements matter more when priors are weak.

---

## Model 6: Task-Weighted Error Cost

```text
Expected loss =
Σ P(message element)
× P(confusion | condition)
× consequence(confusion)
× recovery cost
```

### Assumptions

- Error consequences can be estimated.
- Recovery and detection probabilities differ by task.
- Average accuracy is not a sufficient objective.

---

## Model 7: Compensation Cost Model

```text
Observed task success
=
baseline information
+
reader compensation
-
compensation cost
```

Compensation cost may appear as:

- longer fixation time
- regressions
- fatigue
- reduced secondary-task capacity
- lower persistence
- slower action
- greater error under distraction

### Prediction

Two settings with equal reading speed may differ in effort and robustness.

---

## Model 8: Dual Fitness Model

```text
Typographic fitness =
instrumental fitness
+
expressive fitness
-
contextual mismatch
```

Instrumental fitness includes accuracy, speed, sustainability, and accessibility.

Expressive fitness includes identity, emotion, historical meaning, and
memorability.

### Assumption

The relative weights are task-specific.

---

# Recommendations

## Priority 1: Build the Cross-Layer Transfer Dataset

### Work

Collect studies that report at least two of:

- glyph identification
- word recognition
- visual span
- eye movements
- reading speed
- comprehension
- preference
- fatigue

### Expected Value

Very high

### Effort

Moderate to high

### Reason

This addresses the largest remaining uncertainty and can reveal when local
typographic improvements matter.

---

## Priority 2: Acquire and Normalize Human Confusion Matrices

### Work

Obtain machine-readable data from:

- Simpson et al.
- Bouma-derived studies
- Liu et al.
- visual-span studies
- newer size-, script-, and condition-specific studies

### Expected Value

Very high

### Effort

Moderate

### Reason

These matrices are the foundation for validating computational proxies.

---

## Priority 3: Build a Controlled Degradation Benchmark

### Work

Render a representative set of open-source typefaces across:

- angular size
- blur
- contrast
- erosion
- dilation
- crowding
- eccentricity approximations
- display resolution

### Expected Value

High

### Effort

Moderate

### Reason

It creates reproducible robustness curves without immediately requiring new
human experiments.

---

## Priority 4: Separate Task Families

Create distinct evaluation profiles for:

1. Long-form prose
2. Interface navigation
3. Short labels
4. Codes and identifiers
5. Safety-critical text
6. Low-vision reading
7. Peripheral monitoring
8. Display and expressive typography

### Expected Value

High

### Effort

Low to moderate

### Reason

This prevents universal rankings built from incompatible objectives.

---

## Priority 5: Measure Compensation, Not Just Speed

### Work

Prefer studies and tests that include:

- fixation duration
- regressions
- pupil response
- secondary-task cost
- subjective effort
- persistence
- delayed comprehension

### Expected Value

High

### Effort

High

### Reason

Readers can preserve speed by consuming additional resources.

---

## Priority 6: Test Stable Versus Conditional Confusions

### Work

Compare confusion-network topology across fonts, sizes, and degradation types.

### Expected Value

High

### Effort

Moderate

### Reason

This can identify universal alphabet vulnerabilities and design-specific defects.

---

## Priority 7: Extend Beyond Latin

Begin with scripts that create useful contrasts:

- Cyrillic for shared and divergent letter forms
- Arabic for connected forms and context-sensitive glyphs
- Devanagari for headline structure and complex clusters
- Chinese for larger symbol inventories and stroke density
- Hangul for compositional blocks

### Expected Value

High

### Effort

High

### Reason

A universal visual-communication theory cannot be inferred from Latin alone.

---

## Priority 8: Delay a Universal Typography Score

### Recommendation

Do not produce one global Atlas readability score yet.

Build a profile:

```yaml
glyph_robustness:
word_recovery:
reading_efficiency:
navigation:
adaptability:
task_error_risk:
expressive_fit:
confidence:
```

### Expected Value

Very high

### Effort

Low

### Reason

A single score would hide the most important discoveries from this research.

---

# Bibliography

## Academic

### Information Theory and Noisy-Channel Models

- Shannon, C. E. (1948). *A Mathematical Theory of Communication*. Bell System
  Technical Journal, 27, 379–423 and 623–656.
- Levy, R. (2008). *A Noisy-Channel Model of Rational Human Sentence
  Comprehension under Uncertain Input*. EMNLP.
- Bicknell, K., and Levy, R. (2010). *A Rational Model of Eye Movement Control
  in Reading*. ACL.

### Letter Recognition and Similarity

- Mueller, S. T., and Weidemann, C. T. (2012). *Alphabetic Letter
  Identification: Effects of Perceivability, Similarity, and Bias*. Acta
  Psychologica, 139(1), 19–37.
- Simpson, I. C., Mousikou, P., Montoya, J. M., and Defior, S. (2013).
  *A Letter Visual-Similarity Matrix for Latin-Based Alphabets*. Behavior
  Research Methods, 45, 431–439.
- Liu, L., Klein, S. A., Xue, F., Zhang, J., and Yu, C. (2009). *Using
  Geometric Moments to Explain Human Letter Recognition Near the Acuity Limit*.
  Journal of Vision, 9(1).
- Wiley, R. W., Wilson, C., and Rapp, B. (2016). *The Effects of Alphabet and
  Expertise on Letter Perception*. Journal of Experimental Psychology: Human
  Perception and Performance.
- Duñabeitia, J. A. et al. (2013). *The Influence of Reading Expertise in
  Mirror-Letter Perception*. Frontiers in Psychology.

### Print Size, Visual Span, and Crowding

- Legge, G. E., and Bigelow, C. A. (2011). *Does Print Size Matter for Reading?
  A Review of Findings from Vision Science and Typography*. Journal of Vision.
- Legge, G. E. et al. (2007). *The Case for the Visual Span as a Sensory
  Bottleneck in Reading*. Journal of Vision.
- Pelli, D. G. et al. (2007). *Crowding and Eccentricity Determine Reading
  Rate*. Journal of Vision.
- Strasburger, H., Rentschler, I., and Jüttner, M. (2011). *Peripheral Vision
  and Pattern Recognition: A Review*. Journal of Vision.
- Nandy, A. S., and Tjan, B. S. (2007). *The Nature of Letter Crowding as
  Revealed by First- and Second-Order Classification Images*. Journal of Vision.
- Song, S., Levi, D. M., and Pelli, D. G. (2014). *A Double Dissociation of
  the Acuity and Crowding Limits to Letter Identification*. Journal of Vision.
- Kurzawski, J. W. et al. (2023). *The Bouma Law Accounts for Crowding in 50
  Observers*. Journal of Vision.
- He, Y. et al. (2017). *Linking Crowding, Visual Span, and Reading*. Journal
  of Vision.

### Spacing, Width, Weight, and Typeface

- Yu, D., Cheung, S. H., Legge, G. E., and Chung, S. T. L. (2007). *Effect
  of Letter Spacing on Visual Span and Reading Speed*. Journal of Vision.
- Vinckier, F. et al. (2011). *The Impact of Letter Spacing on Reading: A Test
  of the Bigram Coding Hypothesis*. Journal of Vision.
- Bernard, J. B. et al. (2013). *The Effect of Letter-Stroke Boldness on
  Reading Speed in Central and Peripheral Vision*. Vision Research.
- Chung, S. T. L. (2018). *Bolder Print Does Not Increase Reading Speed in
  People with Central Vision Loss*. Vision Research.
- Minakata, K., and Beier, S. (2021). *The Effect of Font Width on Eye
  Movements During Reading*. Applied Ergonomics.
- Bernard, J. B. et al. (2016). *A New Font, Specifically Designed for
  Peripheral Vision, Improves Peripheral Letter and Word Recognition, but Not
  Eye-Mediated Reading Performance*. PLOS ONE.
- Mansfield, J. S. et al. (1996). *Psychophysics of Reading XV: Font Effects in
  Normal and Low Vision*. Investigative Ophthalmology & Visual Science.
- Dobres, J. et al. (2016). *Utilising Psychophysical Techniques to Investigate
  the Effects of Age, Typeface Design, Size and Display Polarity on Glance
  Legibility*. Ergonomics.
- Xiong, Y. Z. et al. (2018). *Fonts Designed for Macular Degeneration: Impact
  on Reading*. Investigative Ophthalmology & Visual Science.
- Galiano, A. R. et al. (2023). *Luciole, a New Font for People with Low
  Vision*. Applied Ergonomics.

### Context, Words, and Reading

- Grainger, J. (2024). *Letters, Words, Sentences, and Reading*. Psychonomic
  Bulletin & Review.
- Coch, D. et al. (2010). *Word and Pseudoword Superiority Effects Reflected in
  the ERP Waveform*. Brain Research.
- Laszlo, S., and Federmeier, K. D. (2007). *The Acronym Superiority Effect*.
  Psychonomic Bulletin & Review.
- Starrfelt, R. et al. (2013). *Don't Words Come Easy? A Psychophysical
  Exploration of Word Superiority*. Frontiers in Human Neuroscience.
- Jordan, T. R. et al. (2024). *The Reicher-Wheeler Paradigm in Word
  Recognition Research*. Behavior Research Methods.
- Massol, S. et al. (2025). *Word Superiority and Sentence Superiority Effects
  in Post-Cued Letter Identification*. Attention, Perception, & Psychophysics.
- Sass, S. M. et al. (2006). *Low-Vision Reading Speed: Influences of
  Linguistic Inference and Aging*. Optometry and Vision Science.

### Familiarity and Typeface Experience

- Nedeljković, U. et al. (2020). *You Read Best What You Read Most: An
  Eye-Tracking Study*. Frontiers in Psychology.

### Dyslexia and Specialized Fonts

- Duranovic, M. et al. (2018). *Influence of Increased Letter Spacing and Font
  Type on the Reading Ability of Dyslexic Children*. Annals of Dyslexia.
- Kuster, S. M. et al. Research comparing Dyslexie and conventional typefaces.
- Gori, S., and Facoetti, A. (2015). *The Intriguing Case of Crowding and
  Developmental Dyslexia*. Journal of Vision.

### Aesthetics and Emotion

- Medved, T. et al. (2023). *Influence of Letter Shape on Readers' Emotional
  Experience, Reading Fluency, and Text Comprehension*. Frontiers in Psychology.

## Books

- Legge, G. E. (2007). *Psychophysics of Reading in Normal and Low Vision*.
  Lawrence Erlbaum Associates.
- Tracy, W. (1986). *Letters of Credit: A View of Type Design*. David R.
  Godine.
- Bigelow, C., and Day, D. (1983). *Digital Typography*. Scientific American.
- Bringhurst, R. *The Elements of Typographic Style*. Included as historical and
  professional context, not as experimental evidence.

## Industry

- Google Fonts Knowledge. Typeface and optical-size documentation.
- Microsoft typography and accessibility documentation.
- Adobe typography and font-rendering documentation.
- Type Network and foundry technical documentation for optical sizes and
  production practices.

## Patents

No patent evidence materially changed the conclusions in this phase. Patent
research is lower priority than acquiring human confusion datasets and should
focus later on:

- screen font hinting
- subpixel rendering
- adaptive text display
- optical-size interpolation
- low-vision typeface systems

## Standards

- W3C Web Content Accessibility Guidelines, text spacing and reflow criteria.
- ISO 9241 ergonomics standards for visual displays and human-system interaction.
- DIN and related signage-legibility standards should be reviewed in the
  task-specific phase.
- U.S. accessibility and highway-sign standards should be reviewed when
  evaluating safety-critical typography.

## Historical

- Bouma, H. (1971). *Visual Recognition of Isolated Lower-Case Letters*.
  Vision Research.
- Cattell, J. M. (1886). Early demonstrations related to word-superiority
  effects.
- Reicher, G. M. (1969), and Wheeler, D. D. (1970). Foundational
  word-superiority paradigm studies.
- Historical typefounders' optical-size practices, to be investigated as
  empirical craft knowledge rather than assumed proof.

## Other

- UCI Machine Learning Repository, *Letter Recognition* dataset.
- Magre, N., and Brown, N. (2022), *Typography-MNIST*.
- Synthetic glyph corpora and OCR benchmarks, classified as computational
  resources rather than human evidence.

---

# Final Research Position

The initial claim was:

> Typography is the engineering of visual symbols to maximize accurate
> information transfer under human perceptual constraints.

That definition is directionally correct but incomplete.

The revised definition is:

> **Typography is the adaptive design of visible language so that
> task-relevant distinctions, structures, meanings, and actions survive the
> combined constraints of rendering, perception, learned inference, navigation,
> and context.**

The most important shift is from optimizing symbols in isolation to diagnosing
the active bottleneck in a layered, adaptive system.

The Typography Genome should therefore not become a catalog of fonts or even a
catalog of letter features.

It should become a conditional model that predicts:

- where information will be lost
- which layer will fail first
- how the reader will compensate
- whether compensation is sustainable
- which errors matter for the task
- which design change has the highest expected benefit

That is the point where Atlas begins moving from descriptive typography toward a
predictive science of visible language.
