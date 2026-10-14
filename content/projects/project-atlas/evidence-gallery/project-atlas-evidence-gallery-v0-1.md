---
id: RR-VE-ATL-2026-E112
project: project-atlas
purposes:
  - verify
  - reference
audiences:
  - practitioner
  - researcher
---

# Project Atlas – Evidence Gallery v0.1

## Purpose

The Evidence Gallery is a curated collection of real-world compositions that are
analyzed using Atlas principles. It complements scientific literature by showing
how those principles appear in practice.

The gallery does **not** prove a principle. Instead, it provides observable
examples that can be explained by the current Atlas model.

---

# Gallery Entry Format

Each entry should contain:

- Gallery ID
- Source
- Domain
- Screenshot (when licensing permits)
- Atlas principles involved
- Observed strengths
- Observed weaknesses
- Competing organizations
- Confidence
- Supporting literature
- Open questions

---

# Evidence Gallery 0001

## GOV.UK Forms

**Domain:** Government web forms

### Atlas Principles

- ATLAS-0001 Relative Separation
- Candidate: Hierarchical Spacing Differentiation

### Observations

Positive evidence:

- Strong separation between sections.
- Consistent internal spacing.
- Large section headings.
- Minimal competing organizations.
- Reinforcing cues (spacing + headings).

### Atlas Interpretation

The layout uses multiple compatible grouping cues instead of relying on
whitespace alone.

Confidence: High

Research to connect:

- Gestalt proximity
- Common region
- Visual hierarchy
- Form usability

---

# Evidence Gallery 0002

## Apple Settings

**Domain:** Mobile OS

### Atlas Principles

- Relative Separation
- Hierarchical grouping
- Cue reinforcement

### Observations

Positive evidence:

- Clear section boundaries.
- Repeating spacing rhythm.
- Typography reinforces grouping.
- Controls naturally cluster.

Atlas predicts these reduce ambiguity during rapid scanning.

Confidence: Moderate–High

---

# Evidence Gallery 0003

## Uniform-Spacing Forms (Negative Pattern)

**Domain:** Generic web applications

### Pattern

Many criticized forms use nearly identical spacing for:

- labels
- fields
- sections
- actions

### Atlas Diagnosis

The layout contains multiple semantic levels but communicates nearly one spacing
level.

Result:

- Flattened hierarchy.
- Increased competing organizations.
- Users depend on headings rather than geometry.

Confidence: Moderate

---

# Emerging Research Direction

One unexpected observation emerged from comparing modern design systems.

Most systems define spacing tokens such as:

- 4
- 8
- 12
- 16
- 24
- 32
- 48

They describe **what** values to use but rarely explain **why** those values
produce reliable perceptual organization.

Atlas should investigate the underlying mechanisms rather than treating the
tokens themselves as universal rules.

---

# New Candidate Research Questions

1. Why do independent design systems converge on similar spacing scales?
2. Are spacing scales driven by biology, convention, implementation, or all
   three?
3. Which spacing relationships remain stable across web, mobile, print,
   dashboards, and architecture?
4. Can Atlas derive spacing recommendations from perceptual mechanisms rather
   than fixed design tokens?

---

# Next Collection Priorities

1. GOV.UK Design System
2. Apple Human Interface Guidelines
3. Material Design
4. IBM Carbon Design System
5. Microsoft Fluent
6. NASA cockpit/HMI guidance
7. Medical interfaces
8. Financial trading platforms
9. Information graphics
10. Museum and wayfinding systems

---

# Atlas Insight

The first comparisons suggest that mature design systems independently converge
on many of the same structural patterns:

- stronger spacing between groups than within groups;
- multiple reinforcing grouping cues;
- nested hierarchy;
- consistent spacing scales.

Atlas's role is to explain *why* these convergent patterns work, determine
their limits, and identify the perceptual mechanisms behind them rather than
simply documenting best practices.
