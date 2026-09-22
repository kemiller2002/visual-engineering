---
id: EX-VE-TYP-001
title: Individual Readability Envelope
project: perceptual-envelope
status: preregistration-draft
priority: critical
hypotheses:
  - HY-VE-PE-002
  - HY-VE-PE-003
  - HY-VE-PE-006
---

# EX-VE-TYP-001: Individual Readability Envelope

## Question

Can Visual Engineering recover a repeatable, task-sensitive region of typography settings in which an individual reads accurately, quickly, comprehends correctly, and reports acceptable effort?

## Independent variables

Prefer variable-font axes where available, plus CSS controls:

- optical size;
- width;
- weight;
- font size / x-height;
- letter spacing;
- word spacing;
- line height;
- line length;
- polarity;
- contrast reserve.

Do not vary all dimensions in one exhaustive factorial. Use staged screening followed by adaptive search.

## Task classes

1. continuous prose;
2. short interface labels;
3. low-context identifiers and codes;
4. dense comparison/table scanning.

## Outcomes

Primary:

- accuracy;
- comprehension;
- completion or reading time.

Secondary:

- confidence;
- subjective effort;
- preference;
- regression/correction behavior where measurable;
- abandonment;
- fatigue over repeated blocks.

Preference must not replace performance.

## Design

Within-participant repeated-measures design.

### Phase 0: computational preflight

- verify font-axis ranges;
- reject settings that violate layout or WCAG conformance;
- generate balanced condition sets;
- verify randomization and counterbalancing;
- simulate stopping rules;
- ensure task text is matched for difficulty.

### Phase 1: coarse map

Sample the safe ranges broadly enough to detect non-monotonic response.

### Phase 2: adaptive search

Concentrate trials near promising regions and near detected failure boundaries.

### Phase 3: repeatability

Repeat a subset on a separate session/day and a second device class where feasible.

## Analysis

Estimate a participant-level response surface rather than selecting only a single winner.

Candidate envelope rule:

```text
condition is inside envelope
iff
  accuracy >= task threshold
  and comprehension >= task threshold
  and time <= task threshold
  and effort <= task threshold
```

Thresholds must be preregistered by task and must not be invented after observing the data.

Use hierarchical models to estimate population effects without erasing individual response curves.

## Falsification

HY-VE-PE-002 is weakened if within-person differences are small relative to measurement noise or if personal optima fail to repeat.

HY-VE-PE-003 is weakened if task-specific optima do not materially differ.

HY-VE-PE-006 is weakened if preference reliably identifies best measured performance.

## Sampling

Do not choose the final sample size from convention. Run a pilot to estimate within-participant variance, then preregister a simulation-based power analysis around the smallest practically meaningful difference.

Recruitment should include, but not collapse together:

- typical readers;
- readers with diagnosed dyslexia;
- low-vision readers when the task and display protocol are appropriate.

## Safety and ethics

- calibration itself must be accessible;
- participants may stop at any time;
- avoid painful luminance or deliberately extreme visual stress;
- collect the minimum personal information needed;
- keep raw participant data out of the public repository.

## Success criterion

The experiment succeeds as research even if personalization fails. A useful result is a well-bounded null showing that a universal default performs equivalently within practical tolerances.
