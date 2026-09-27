---
id: DF-VE-TCG-2026-2DD5
title: Visual Engineering Layout Catalog Location and Contract
project: terminal-character-grid
date: 2026-09-27
status: accepted
work_item: GH-18
author_agent: Anthropic Claude
purposes:
  - decide
  - reference
audiences:
  - contributor
  - researcher
---

# DF-VE-TCG-2026-2DD5: Layout Catalog Location and Contract

## Context

Issue #18 asks for the Terminal / Character-Grid family to be added "to the
Visual Engineering layout catalog using the repository's catalog contract".
At base commit `c1e7c32` Visual Engineering had no layout catalog. The only
`LAY-*` catalog was Forma's `catalog/layouts.json`, which describes Forma's
*verified implementations* and cites Visual Engineering Composition Science as
its source. Visual Engineering's only structured catalog was the theme
specimen catalog (`content/themes/`), which pairs an `index.json`, a schema,
and per-record JSON files.

## Decision

Create `content/layouts/` using the theme catalog's structure (index, schema,
per-family JSON record, README contract) and a Node validator with tests.
Field names are a superset of Forma's family record so Forma can consume the
relationships without translation. Visual Engineering records use the same
support-state vocabulary; a Visual Engineering record is `observed` until an
implementation's conformance run is recorded.

## Alternatives considered

- **Edit Forma's `catalog/layouts.json` from Visual Engineering.** Rejected:
  that file records Forma verification, and it would blur the Visual
  Engineering / Forma ownership boundary.
- **Markdown-only catalog entry.** Rejected: the reference workflow (GH-20)
  must be checked mechanically against the family's relational rules, which
  requires machine-readable records.

## Consequences

- Existing Forma-verified families are not back-filled here. A follow-up is
  recorded in the ROS backlog.
- The research-publisher only indexes Markdown; JSON records are validated by
  `npm run layouts:validate` instead.
