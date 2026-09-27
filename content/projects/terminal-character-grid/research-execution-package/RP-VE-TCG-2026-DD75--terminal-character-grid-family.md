---
id: RP-VE-TCG-2026-DD75
title: Terminal / Character-Grid Layout Family
research_area:
  - layout
  - interaction
  - accessibility
discipline: Visual Engineering
author_agent: Anthropic Claude
version: 1.0
confidence: medium
completion: in-progress
priority: high
work_items:
  - GH-15
  - GH-17
  - GH-18
  - GH-19
  - GH-20
related_projects:
  - composition-science
  - perceptual-envelope
supersedes: []
superseded_by: []
tags:
  - terminal
  - character-grid
  - keyboard-first
  - layout-family
purposes:
  - integrate
  - apply
audiences:
  - researcher
  - practitioner
  - contributor
---

# RP-VE-TCG-2026-DD75: Terminal / Character-Grid Layout Family

## Research question

Can one relational abstraction describe keyboard-first character-grid
interfaces (IBM 3270, IBM 5250, DOS text mode, BBS, terminal/TUI, and modern
grid applications) well enough that a design system can implement it once and
express each style as a profile — without becoming a disguised 3270 component
and without sacrificing accessibility?

## Scope

In scope: grid geometry, placement, protected/editable runs, traversal, status,
action affordances, density, narrow-screen strategy, accessibility, and an
optional SequentialReveal presentation behavior.

Out of scope: host data streams, emulation, key-to-action processing, screen
transitions, and domain state. Those belong to the consuming application,
Limen, and Ordo (CN-VE-TCG-2026-6CA0, rule B-1).

## Method

1. Establish invariants from primary sources and maintained implementations;
   separate verified facts from background assumptions (GH-17).
2. Express the family as a relational layout-catalog entry (GH-18).
3. Define SequentialReveal independently of any profile (GH-19).
4. Validate against a three-screen reference workflow encoded as data and
   checked mechanically for the relational rules (GH-20).
5. Hand the verified relationships and capability gaps to Forma (#37).

## Artifacts

| Work item | Artifact |
| --- | --- |
| GH-17 | [CN-VE-TCG-2026-6CA0](../concept/CN-VE-TCG-2026-6CA0--terminal-character-grid-abstraction-boundary.md), [EVR-VE-TCG-001](../evidence-registry/terminal-character-grid-evidence-v1.md), [HYR-VE-TCG-001](../hypothesis-registry/terminal-character-grid-hypotheses-v1.md), [JR-VE-TCG-2026-2E0E](../research-journal/JR-VE-TCG-2026-2E0E--evidence-and-abstraction-boundary.md) |
| GH-18 | [`LAY-TERMINAL-CHARACTER-GRID`](../../../layouts/families/LAY-TERMINAL-CHARACTER-GRID.json) in the new [layout catalog](../../../layouts/README.md); [DF-VE-TCG-2026-2DD5](../decision-record/DF-VE-TCG-2026-2DD5--visual-engineering-layout-catalog-location.md) |
| GH-19 | SequentialReveal behavior definition |
| GH-20 | Reference workflow validation report |

## Status

GH-17 complete as of 2026-09-27. Later rows are updated by their own work
items.

## Evidence standard

Claims cite `EV-VE-TCG-2026-*`. Background that was not verified in an opened
source is labeled as an assumption. No user study has been run; all
performance claims are hypotheses (`HY-VE-TCG-2026-*`).
