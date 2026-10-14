---
id: RR-VE-ATL-2026-E878
authors: [OpenAI]
confidence: Moderate
date: 2026-07-19
llm_ingest: true
machine_readable: true
project: project-atlas
status: research draft
summary: >
  Audit of existing letter-similarity, letter-confusion, visual-span, and
  synthetic glyph datasets. The main finding is that these data classes answer
  different questions and must remain condition-aware.
version: 1.0
purposes:
  - verify
  - reproduce
audiences:
  - researcher
  - contributor
---

# Letter-Confusion Data Audit and Measurement Plan

## Purpose

The previous work established that letter recognition can be modeled as a noisy
communication problem. This document identifies what evidence already exists and
what Atlas can calculate without running new human studies.

> A matrix is only meaningful together with its stimulus, observer, task,
> presentation conditions, and response procedure.

## 1. Four Different Data Types

### Clear-view similarity ratings

Participants compare clearly rendered letter pairs and rate visual resemblance.

Useful for:

- conscious shape similarity
- baseline clustering
- cross-case and diacritic comparisons

Not direct evidence of:

- substitution errors
- blur robustness
- crowding robustness
- reading speed

### Identification-confusion matrices

Participants identify briefly shown, peripheral, low-contrast, small, or
crowded letters.

They estimate:

```text
P(response = j | target = i, condition = c)
```

These are the strongest data for modeling actual recognition failure.

### Visual-span profiles

Participants identify letters at positions around fixation, often in trigrams.
Results estimate how much symbol information is available across the visual
field.

### Computational glyph datasets

Rendered letters are distorted and classified by machines.

Useful for stimulus generation and proxy-model development, but they are not
human legibility evidence unless validated against human results.

## 2. Why These Data Must Not Be Pooled

```text
Similarity ratings      → What looks alike?
Confusion matrices      → What is mistaken for what?
Visual-span profiles    → What survives by retinal position?
Reading studies         → What transfers to continuous reading?
Synthetic datasets      → What can machines screen or predict?
```

Similarity, confusability, and reading performance overlap, but they are not the
same construct.

# 3. Candidate Source Audit

## DATA-VIT-001: Mueller and Weidemann Review

*Alphabetic letter identification: Effects of perceivability, similarity, and
bias* reviews more than a century of letter-identification research and more
than 70 relevant publications.

Its most important contribution is separating three influences:

- perceivability
- similarity
- response bias

**Atlas value:** High as a source map and conceptual framework.

**Limitation:** It is not one standardized raw dataset.

## DATA-VIT-002: Simpson et al. Latin Similarity Matrix

*A letter visual-similarity matrix for Latin-based alphabets* collected untimed
similarity ratings for clearly presented letters used across several European
languages.

Reported design:

- 52 uppercase forms
- 53 lowercase forms
- 1,326 unordered uppercase pairs
- 1,378 unordered lowercase pairs

**Measures:** conscious resemblance under clear viewing.

**Does not measure:** actual substitution probability under degradation.

**Atlas value:** Excellent baseline for comparing rated similarity with real
confusion.

## DATA-VIT-003: Bouma Lowercase Recognition Data

Bouma's studies on isolated lowercase recognition and parafoveal interference
are repeatedly reused in later confusion research.

**Atlas value:** High because they contain actual identification errors.

**Limitations:**

- older fonts and apparatus
- incomplete modern metadata
- raw machine-readable availability remains uncertain
- not suitable for ranking contemporary web fonts without replication

## DATA-VIT-004: Liu et al. Geometric-Moment Dataset

*Using geometric moments to explain human letter recognition near the acuity
limit* compares human confusion patterns with low-order geometric image
descriptors.

**Atlas value:** Extremely high. It directly tests whether computable geometry
can predict human confusion.

**Limitation:** Geometry explains only part of performance. Familiarity, bias,
context, and task remain relevant.

## DATA-VIT-005: Beckmann-Legge Visual-Span Data

Visual-span research uses confusion data to convert recognition performance into
information transmitted in bits across retinal positions.

**Atlas value:** Provides an established bridge among confusion matrices,
information theory, visual position, and reading.

**Risk:** A generic percent-correct-to-bits conversion may hide condition-specific
confusion structure. Atlas should prefer full matrices when available.

## DATA-VIT-006: Yu and Kwon Cross-Matrix Comparison

*Defining letter similarity* compares four matrices collected with different
fonts and conditions.

**Atlas value:** Supports testing which confusion relationships are stable and
which are condition-specific.

**Limitation:** The accessible source is a conference record; original matrices
must be traced to source studies.

## DATA-VIT-007: UCI Letter Recognition

The UCI dataset contains 20,000 distorted uppercase stimuli based on 20 fonts
and represented by 16 numerical image features.

**Atlas value:** Reproducible pipeline and proxy-model benchmark.

**Limitation:** Machine-classification data, not human responses.

## DATA-VIT-008: Typography-MNIST

A large rendered corpus spanning many fonts, scripts, and glyphs.

**Atlas value:** Broad geometry corpus and computational screening.

**Limitation:** No human confusion probabilities.

# 4. Core Finding

There is no universal letter-confusion matrix.

The correct object is:

```text
M(i, j | typeface, size, contrast, eccentricity, spacing,
       exposure, observer, language, task)
```

not:

```text
M(i, j)
```

Glyph distance is conditional.

# 5. Standard Atlas Dataset Schema

```yaml
dataset_id:
source:
  citation:
  doi:
  publication_status:
  license:
  raw_data_location:

stimulus:
  script:
  alphabet:
  case:
  typeface:
  font_version:
  weight:
  nominal_size:
  measured_x_height:
  rendering_medium:
  contrast:
  polarity:

presentation:
  task_type:
  exposure_ms:
  masking:
  retinal_eccentricity_deg:
  flanker_configuration:
  letter_spacing:
  viewing_distance:
  visual_angle:

observer:
  sample_size:
  age_range:
  vision_criteria:
  language:
  alphabet_expertise:
  reading_level:

response:
  response_set:
  forced_choice:
  response_time_available:
  confidence_available:

data:
  unit: count | probability | rating | accuracy | bits
  matrix_orientation:
  missing_cells:
  repeated_trials:
  aggregation_level:

interpretation:
  valid_uses:
  invalid_uses:
  known_biases:
  confidence:
```

Missing metadata must remain explicit rather than being silently assumed.

# 6. First Metrics

## MET-VIT-011: Directed Confusion

```text
P(response_j | target_i)
```

Preserves directional errors.

## MET-VIT-012: Symmetric Pair Confusion

```text
C_sym(i,j) = [P(j|i) + P(i|j)] / 2
```

Useful for clustering only. It must not replace the directed source matrix.

## MET-VIT-013: Confusion Asymmetry

```text
A(i,j) = |P(j|i) - P(i|j)|
```

High asymmetry may reveal response bias or unequal feature visibility.

## MET-VIT-014: Response Bias

How often a symbol is selected independent of whether it was shown.

A commonly guessed symbol can appear deceptively legible if only diagonal
accuracy is reported.

## MET-VIT-015: Mutual Information

```text
I(Target; Response)
```

Measures how much the response reduces uncertainty about the target.

## MET-VIT-016: Normalized Information Retention

```text
I(Target; Response) / H(Target)
```

Allows comparison across alphabets and target distributions.

## MET-VIT-017: Weakest Consequential Pair

```text
confusion probability
× occurrence probability
× consequence of error
```

This is more useful for real systems than average accuracy alone.

## MET-VIT-018: Matrix Stability

Compare conditions using:

- rank correlation
- Jensen-Shannon divergence
- Hellinger distance
- nearest-neighbor overlap
- confusion-cluster preservation

No single comparison should be treated as definitive.

# 7. First Reanalysis

## Study A: Clear Similarity Versus Actual Confusion

Compare the Simpson clear-view ratings with one or more degraded identification
matrices.

Questions:

- Do pairs that look alike become pairs that are actually confused?
- Which pairs are more confusable than ratings predict?
- Which pairs look similar but remain categorically distinct?
- Does the relationship change with blur, crowding, or eccentricity?

The residuals are especially valuable.

### High confusion, low rated similarity

Possible causes:

- degradation-specific feature loss
- crowding
- response bias
- mirror or positional errors
- rendering artifacts

### High rated similarity, low confusion

Possible causes:

- diagnostic local features
- learned category boundaries
- task differences
- strong conventional familiarity

## Study B: Stable Confusion Backbone

Classify pair relationships as:

```text
Structural
  stable across many conditions

Conditional
  activated by a particular degradation

Typeface-specific
  caused by local design decisions
```

This separates alphabet-level constraints from typeface-level failures.

## Study C: Component-to-Reading Transfer

Collect studies reporting at least two levels:

- letter recognition
- word recognition
- visual span
- reading speed
- eye movements
- comprehension

This will reveal when improved glyph information transfers to reading and when
another bottleneck dominates.

# 8. Refined Candidate Laws

## LAW-VIT-012: Matrix Conditionality

A confusion matrix describes a symbol system under a condition, not a permanent
alphabet map.

**Confidence:** High

## LAW-VIT-013: Rating-Error Separation

Perceived similarity under clear viewing and actual substitution under degraded
viewing are distinct constructs.

**Confidence:** High

## LAW-VIT-014: Bias Decomposition

Observed identification reflects perceivability, similarity, and response bias.

**Confidence:** High

## LAW-VIT-015: Residual Value

Errors unexplained by geometric or clear-view similarity identify missing
perceptual, learned, contextual, or procedural variables.

**Confidence:** Moderate to high

## LAW-VIT-016: Stable-Backbone Hypothesis

Some confusions will remain stable across conditions because they arise from
shared alphabet structure; others will be typeface- or condition-specific.

**Confidence:** Moderate

## LAW-VIT-017: Proxy Validation

A computational legibility model is useful only insofar as it predicts human
confusion under matched conditions.

**Confidence:** High

# 9. Risks

- Older matrices may require manual extraction.
- Matrix orientation can be transcribed incorrectly.
- Aggregated data may hide observer variation.
- Historical font versions may be unrecoverable.
- Point size without x-height or visual angle is insufficient.
- Forced-choice tasks can create artificial substitutions.
- Acuity-threshold results may not generalize to ordinary body text.
- Clear-view ratings may conceal degradation failures.
- Machine observers may learn synthetic artifacts rather than human features.

# 10. Immediate Next Actions

1. Obtain a machine-readable Simpson similarity matrix.
2. Locate complete Bouma- and Liu-derived confusion matrices.
3. Verify access to Beckmann-Legge conversion or response data.
4. Create CSV and JSON forms of the Atlas schema.
5. Build an import validator for orientation, totals, missing cells, and metadata.
6. Calculate directed confusion, asymmetry, mutual information, and graph
   clusters for the first available matrix.
7. Compare at least two matrices before deriving typography laws.
8. Add language frequency and consequence weighting only after preserving the
   unweighted perceptual structure.
9. Keep synthetic corpora classified as computational evidence.
10. Record inaccessible data as a limitation rather than filling gaps by
    assumption.

# References

- Mueller, S. T., & Weidemann, C. T. (2012). *Alphabetic letter
  identification: Effects of perceivability, similarity, and bias*.
- Simpson, I. C., Mousikou, P., Montoya, J. M., & Defior, S. (2013).
  *A letter visual-similarity matrix for Latin-based alphabets*.
- Bouma, H. (1971). *Visual recognition of isolated lower-case letters*.
- Liu, L. et al. (2009). *Using geometric moments to explain human letter
  recognition near the acuity limit*.
- Yu, D. et al. (2007). *Effect of letter spacing on visual span and reading
  speed*.
- UCI Machine Learning Repository. *Letter Recognition*.
- Magre, N., & Brown, N. (2022). *Typography-MNIST*.

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 1.0 | 2026-07-19 | OpenAI | Initial dataset audit, schema, metrics, and reanalysis plan. |
