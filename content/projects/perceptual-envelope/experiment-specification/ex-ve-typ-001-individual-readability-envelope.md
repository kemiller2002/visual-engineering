---
id: EX-VE-TYP-001
title: Individual Readability Envelope
project: perceptual-envelope
status: phase-0-complete
priority: critical
implementation:
  phase_0: experiments/ex-ve-typ-001
  human_execution: not-started
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


## Phase 0 Implementation Record — 2026-09-22

The computational preflight is implemented under
`experiments/ex-ve-typ-001/`.

### Coarse-map design

The pilot uses 24 deterministic space-filling conditions generated from separate
prime-base Halton sequences rather than an exhaustive factorial. The current
safe pilot bounds are:

| Variable | Pilot range |
| --- | --- |
| Font size | 16–22 px |
| Weight | 350–650 |
| Width | 90–110% |
| Optical size | 14–22 pt |
| Letter spacing | -0.01–0.06 em |
| Word spacing | 0–0.12 em |
| Line height | 1.35–1.75 |
| Line length | 48–76 ch |
| Contrast ratio | 7:1–12:1 |
| Polarity | dark-on-light / light-on-dark |

These bounds are deliberately conservative. Phase 0 is intended to validate
experimental mechanics before exposing participants to wider or more demanding
conditions.

### Crossover structure

Each participant receives 24 primary trials, six in each task class. A
participant-specific rotation assigns every typography condition to exactly one
task for that session. Across four adjacent cohort indices, every condition is
tested exactly once in every task class.

Task block order is separately balanced across the same four cohort indices so
every task occupies every serial position once.

The repeatability session contains eight condition/task probes from session one
with different stimuli.

### Pilot-only envelope thresholds

The current implementation uses provisional thresholds only to test the
classification path:

- continuous prose and dense comparison: accuracy >= 0.95 and comprehension >=
  0.80;
- labels and identifiers: accuracy >= 0.98;
- all task classes: duration <= 1.10x the participant/task median and effort <=
  4/7.

These are **not the preregistered thresholds for the main human study**. The
instrumented pilot must estimate measurement variance and practical effect sizes
before final thresholds and sample size are frozen.

### Synthetic dry-run boundary

The F# simulator creates heterogeneous latent typography profiles, task-specific
optimum shifts, and deterministic noise. It exists solely to test schedule,
analysis, and data-path behavior.

Synthetic results:

- are not evidence for HY-VE-PE-002, HY-VE-PE-003, or HY-VE-PE-006;
- may not be reported as human performance;
- may not be used to claim that personalization works.

### Data contract

The trial record intentionally excludes names, email addresses, diagnosis,
free-text responses, and other unnecessary personal data. A study-assigned
pseudonym is the only participant key expected by the protocol.

Raw participant data must not be committed to the public repository.

### Phase 0 exit gate

Phase 0 is complete when all of the following pass:

1. all 24 generated conditions remain within the declared pilot bounds;
2. condition IDs are unique;
3. the four-cohort condition/task crossover is exact;
4. task serial positions balance across four cohorts;
5. schedule generation is deterministic for a participant and seed;
6. the repeatability session preserves condition/task pairs while changing
   stimuli;
7. synthetic observations remain within schema bounds;
8. the repository's F# build and automated tests pass;
9. research, ROS, and Limen repository gates pass.

Completion of this gate authorizes implementation of the browser
presentation/collection runner. It does not authorize a scientific conclusion.


## Phase 0 Completion Validation

Phase 0 completed on 2026-09-22.

- F# build: 0 warnings, 0 errors.
- EX-VE-TYP-001 tests: 11/11 passed.
- Existing Visual Engineering tests: 80/80 passed.
- Research validation: passed.
- ROS validation: passed.
- Limen strict verification: passed.
- Visual Engineering package matrix: passed on Ubuntu, macOS, and Windows with
  Node 20 and Node 24.
- A repeatability-stimulus collision discovered by testing was corrected and
  retained as negative knowledge in JR-VE-TYP-001-P0.

This completes the computational preflight only. No human-subject result or
hypothesis conclusion is implied.
