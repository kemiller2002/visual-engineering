---
id: EX-VE-COL-006
title: CVD Minimax Palette and Semantic Survivability
project: perceptual-envelope
status: preregistration-draft
priority: critical
hypotheses:
  - HY-VE-PE-004
  - HY-VE-PE-007
---

# EX-VE-COL-006: CVD Minimax Palette and Semantic Survivability

## Question

Can a palette optimized for worst-case category separation across multiple observer models reduce categorical confusion while semantic redundancy preserves meaning when color fails?

## Phase A: computational minimax search

Define candidate categorical colors in a bounded gamut.

For each candidate palette:

1. evaluate normal-vision perceptual separation;
2. transform through multiple severity levels of protan, deutan, and tritan simulation;
3. calculate pairwise separability in a versioned perceptual space;
4. enforce WCAG contrast requirements for the actual foreground/background use;
5. penalize semantic-token collisions;
6. maximize the worst-case separation rather than the average.

Illustrative objective:

```text
maximize
  min(observer_model, severity, color_pair)
    discriminability(color_pair)
subject to
  conformance constraints
  gamut constraints
  semantic-role constraints
```

No simulated score is labeled accessible.

## Phase B: semantic channel dropout

For states such as success, warning, failure, selected, disabled, and unknown, create conditions that selectively remove or degrade channels:

- hue removed / grayscale;
- protan simulation;
- deutan simulation;
- tritan simulation;
- forced colors;
- icon removed;
- text label removed;
- border or pattern removed.

Measure whether participants can still identify the state.

## Redundant channels

Candidate channels:

- text label;
- icon geometry;
- border/shape;
- position;
- pattern or line style;
- color;
- programmatic semantics.

The aim is not maximum cue count. The aim is the smallest independent set that maintains performance without excessive clutter.

## Human validation

Recruit affected participants across CVD types and severity where feasible. Compare:

- current palette;
- minimax palette;
- minimax palette plus redundant semantics.

Measure accuracy, time, confidence, and confusion matrix.

## Falsification

HY-VE-PE-007 is weakened if the optimized palette does not improve held-out category discrimination or if gains exist only in simulation.

HY-VE-PE-004 is weakened if semantic channel survivability scores do not predict actual state-identification robustness.

## Required output

- palette-generation algorithm and version;
- all simulation model versions;
- category-confusion matrices;
- state-recognition results;
- explicit list of conditions where color remains insufficient;
- no universal "CVD-safe palette" claim unless affected-user evidence supports it.
