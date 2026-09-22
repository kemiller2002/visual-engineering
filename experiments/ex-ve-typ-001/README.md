# EX-VE-TYP-001 — Individual Readability Envelope

This directory contains the executable Phase 0 machinery for the first
Perceptual Envelope experiment.

It does **not** contain human-subject results. Synthetic runs validate mechanics,
balancing, deterministic scheduling, and analysis plumbing only.

## What exists

- a deterministic 24-condition, space-filling typography design;
- four task classes with balanced incomplete crossover;
- a four-cohort task-order design;
- an eight-trial repeatability session;
- pilot-only envelope thresholds;
- a synthetic cohort simulator;
- trial-record and configuration contracts;
- versioned task stimuli;
- automated tests.

## Run the preflight

```bash
dotnet run --project experiments/ex-ve-typ-001/src/ExVeTyp001.Runner/ExVeTyp001.Runner.fsproj -- preflight
```

The command exits non-zero when a design invariant fails.

## Generate a participant schedule

Primary session:

```bash
dotnet run --project experiments/ex-ve-typ-001/src/ExVeTyp001.Runner/ExVeTyp001.Runner.fsproj -- schedule 0 1
```

Repeatability session:

```bash
dotnet run --project experiments/ex-ve-typ-001/src/ExVeTyp001.Runner/ExVeTyp001.Runner.fsproj -- schedule 0 2
```

The participant index is a zero-based cohort index, not identifying information.

## Run the synthetic mechanics check

```bash
dotnet run --project experiments/ex-ve-typ-001/src/ExVeTyp001.Runner/ExVeTyp001.Runner.fsproj -- simulate 64
```

The simulator deliberately gives each synthetic participant a different latent
typography profile and adds task-specific shifts and noise. Its purpose is to
exercise the design and analysis paths. It is prohibited from resolving
HY-VE-PE-002, HY-VE-PE-003, or HY-VE-PE-006.

## Design

A full factorial across font size, weight, width, optical size, letter spacing,
word spacing, line height, line length, polarity, and contrast would be too large
for a useful within-participant session.

Instead the pilot uses 24 deterministic Halton-sequence conditions across the
safe preregistered bounds. Each participant sees every condition once, split
six-per-task across:

1. continuous prose;
2. interface labels;
3. identifier recognition;
4. dense comparison.

Condition-to-task assignment rotates by participant cohort. Across four adjacent
cohorts, every typography condition is exposed exactly once to every task class.
Task block order is also balanced across four cohorts.

Session two repeats eight condition/task pairs with different stimuli.

## Important boundaries

- WCAG 2.2 conformance remains a floor.
- Pilot thresholds are **not** the final main-study thresholds.
- Preference, comfort, and performance are recorded separately.
- No diagnosis is required by the data model.
- Raw participant data must not be committed to this repository.
- The eventual browser runner must use monotonic timing and must not record
  keystrokes, free text, or other unnecessary personal data.
- Human execution requires appropriate consent/review for the setting in which it
  is run.

## Next implementation step

Build the browser presentation/collection runner against these contracts, then run
a small instrumented pilot to estimate within-participant variance and choose the
main-study sample size by simulation rather than convention.
