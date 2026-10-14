---
id: GV-VE-2026-4587
project: visual-engineering-knowledge-platform
purposes:
  - decide
  - apply
  - reference
audiences:
  - practitioner
  - contributor
---

# Repository Architecture

Generated: 2026-07-22T15:10:59+00:00

## Recommendation

Use a hybrid architecture:

- Concept-first for knowledge relationships
- Project-first for active research programs
- Artifact-type collections for generated indexes and registries
- Generated data layers for search, graph traversal, and website navigation

## Why This Fits The Evidence

The repository has 49 files but already spans multiple projects, document types, and maturity levels. A discipline-first tree would fragment shared concepts. A project-first tree would hide cross-project laws and evidence. An artifact-first tree would optimize storage at the expense of reasoning. A hybrid model keeps canonical concepts stable while allowing projects and derived artifacts to evolve independently.

## Target Structure

```text
content/
  concepts/
    perception/
    attention/
    hierarchy/
    wayfinding/
    color/
    typography/
  projects/
    composition-science/
      governance/
      canonical/
      research/
      evidence/
    project-atlas/
      charter/
      canonical/
      reports/
      evidence/
    product-genome/
      canonical/
      reports/
  registries/
    evidence/
    hypotheses/
    experiments/
    decisions/
  journals/
    research-journal/
data/
  catalog/
  graph/
  search/
  lineage/
site/
scripts/
```

## Canonical Rules

- Canonical concept pages own definitions and stable identifiers.
- Derived documents never duplicate canonical definitions; they cite them.
- Registries are generated, not edited by hand.
- Historical documents remain preserved under lineage-aware versioning.
