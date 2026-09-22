---
id: JR-VE-TYP-001-P0
title: EX-VE-TYP-001 Phase 0 Computational Preflight Execution
project: perceptual-envelope
date: 2026-09-22
author_agent: OpenAI
status: complete
---

# EX-VE-TYP-001 Phase 0 Computational Preflight Execution

## Objective

Execute the computational preflight for EX-VE-TYP-001, Individual Readability
Envelope, far enough to validate the experimental mechanics before any
human-subject exposure.

This record contains engineering and protocol validation only. It contains no
human performance data and does not resolve HY-VE-PE-002, HY-VE-PE-003, or
HY-VE-PE-006.

## Implemented

The executable experiment package is under `experiments/ex-ve-typ-001/`.

It now contains:

- a dependency-light F# domain and analysis core;
- 24 deterministic, space-filling typography conditions;
- bounded font size, weight, width, optical size, letter spacing, word spacing,
  line height, line length, polarity, and contrast variables;
- four task classes: prose, interface labels, identifiers, and dense comparison;
- a four-cohort condition-to-task crossover;
- a four-cohort task-order counterbalance;
- a 24-trial primary schedule;
- an eight-trial repeatability schedule;
- deterministic participant scheduling;
- pilot-only envelope thresholds;
- a synthetic mechanics simulator;
- a JSON trial schema;
- an explicit data-minimization policy;
- original task stimuli;
- automated tests;
- a command-line preflight/schedule/simulation runner.

## Validation Result

GitHub Actions validation on the final implementation head before this
completion record:

| Gate | Result |
| --- | --- |
| F# build | PASS, 0 warnings, 0 errors |
| EX-VE-TYP-001 tests | PASS, 11/11 |
| Existing VisualEngineering.Core tests | PASS, 43/43 |
| Existing VisualEngineering.Cli tests | PASS, 37/37 |
| Total .NET tests | PASS, 91/91 |
| Research validation | PASS, workflow run 68 |
| ROS validation | PASS, workflow run 103 |
| Limen strict verification | PASS, workflow run 102 |
| Visual Engineering tool/package workflow | PASS, workflow run 50 |
| Packed artifact matrix | PASS on Ubuntu, macOS, and Windows with Node 20 and 24 |

The CI workflow IDs for this validated head were:

- Build Visual Engineering tool: 35704559229
- Validate research: 35704559277
- ROS validation: 35704559233
- Limen verify: 35704559361

## Important Failure Found During Preflight

The first implementation of the repeatability scheduler intended to use a new
stimulus in session two but did not guarantee it. A test exposed a real collision:
one repeated dense-comparison condition received `DENSE-S03` in both sessions.

That is a protocol defect, not a cosmetic test failure. Reusing the same
condition/task/stimulus combination could confound repeatability with recognition
or memory.

The scheduler was changed so each repeatability probe:

1. preserves the original typography condition;
2. preserves the original task class;
3. selects a session-two stimulus;
4. explicitly compares it with the session-one stimulus;
5. deterministically advances to a different stimulus if a collision occurs.

The regression test was expanded across 16 participant cohort indices. It passes.

## Assumptions Challenged

### Assumption: a session-number offset was sufficient to prevent stimulus reuse

**Rejected.** Deterministic modular scheduling can still collide when the
condition's original within-task position differs from its repeatability
position. Freshness must be an explicit invariant, not an emergent property of
an offset.

### Assumption: a full typography factorial is required to map a response surface

**Not supported for Phase 0.** A 24-point space-filling design provides broad
coverage while keeping the primary participant burden to 24 trials. Human pilot
data is still required to determine whether this is dense enough for useful
response-surface estimation.

### Assumption: one global visual optimum is sufficient

**Not tested.** The design deliberately rotates the same condition across four
task classes so this assumption can be falsified later. Synthetic behavior is
not evidence.

## What Phase 0 Proves

Phase 0 supports the following engineering claims:

- the condition generator is deterministic;
- the declared pilot bounds are machine-checkable;
- every condition reaches every task across four adjacent cohorts;
- every task occupies every serial block position across four adjacent cohorts;
- every participant receives six primary conditions per task;
- repeatability probes retain condition/task identity and change stimulus;
- envelope classification keeps accuracy, comprehension, duration, and effort
  as independent gates;
- preference remains a separate outcome from measured speed;
- the synthetic mechanics path is deterministic and bounded;
- the experiment integrates without breaking the Visual Engineering tool,
  research publisher, ROS, or Limen gates.

## What Phase 0 Does Not Prove

It does not show that:

- individualized typography improves human performance;
- a stable personal readability envelope exists;
- task-specific visual optima differ in real users;
- user preference diverges from performance in the target population;
- the provisional pilot thresholds are scientifically appropriate;
- 24 conditions are sufficient for the final response surface;
- any disability group has one characteristic response.

Those remain empirical questions.

## Human-Study Boundary

Before human pilot execution, the next phase must provide an accessible browser
presentation/collection runner with:

- monotonic trial timing;
- deterministic schedule import;
- exact typography application and verification;
- response capture without free text;
- pause/withdraw behavior;
- device and viewport metadata;
- no diagnosis requirement;
- local validation against the trial-record schema;
- export that keeps raw participant data out of the public repository;
- consent/review appropriate to the execution setting.

A small instrumented pilot should then estimate within-participant variance and
measurement reliability. Final thresholds, smallest practically meaningful
differences, exclusion rules, stopping rules, and main-study sample size must be
frozen only after that pilot and before the confirmatory run.

## Status

**Phase 0: complete.**

**Human execution: not started.**

**Hypotheses resolved: none.**
