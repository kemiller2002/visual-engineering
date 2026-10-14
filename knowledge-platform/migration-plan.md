---
id: GV-VE-2026-9D7C
project: visual-engineering-knowledge-platform
purposes:
  - orient
  - decide
  - apply
audiences:
  - contributor
---

# Migration Plan

Generated: 2026-07-22T15:10:59+00:00

## Principles

- No big-bang rewrite
- Preserve original filenames in intake history
- Promote canonical artifacts first
- Generate indexes before moving large volumes of files

## Phase A: Stabilize Intake

- Freeze `input-documents` as an intake area.
- Generate repository manifest and quality reports on every run.
- Add metadata to files currently missing front matter.

## Phase B: Establish Canonical Layer

- Promote governance, ontology, genome, methodology, and library documents into `content/projects/*/canonical`.
- Create concept pages for the highest-frequency concepts first.

## Phase C: Resolve Duplicates And Lineage

- Merge exact duplicates:
- Composition_Science_Research_Library_v0.2.md
- Project_Atlas_Visual_Information_Transfer_Foundations_v1.md
- composition-science-research-library-v0-2-2.md
- project-atlas-visual-information-transfer-foundations-v1-2.md

## Phase D: Move Derived Research

- Move derived reports and case studies into project collections:
- composition-science-research-library-v0-1.md
- composition-science-research-library-v0-4.md
- composition-science-phase-2-visual-hierarchy-and-wayfinding.md
- composition-science-phase-3-evidence-review-attention-wayfinding.md
- composition-science-architecture-human-scale-and-proportion-research.md
- composition-science-repository-governance-specification-v1.md
- intuitive-is-just-familiar-research-report.md
- component-library-foundations-research-report.md
- project-atlas-letter-confusion-data-audit-v1.md
- project-atlas-color-vocabulary-and-mechanism-taxonomy.md
- project-atlas-perceptual-color-genome.md
- project-atlas-project-charter-v0-1.md
- project-atlas-research-methodology-v0-2.md
- project-atlas-applied-case-study-001-account-settings-form.md
- project-atlas-ribbon-comparative-study-v0-1.md
- project-atlas-evidence-gallery-v0-1.md

## Phase E: Normalize Names

- Rename inconsistent intake files into slug and ID-backed forms:
- project-atlas-color-evidence-registry-v0-1.md
- product-genome-autonomous-research-run-01.md
- rep-atlas-0002-relational-legibility-envelope.md
- architecture-as-a-foundation-for-composition-science.md
- atlas-0001-proximity-and-relative-separation-v0-1.md
- composition-science-atlas-research-framework-v1.md
- composition-science-visual-density-crowding-and-perceptual-separation.md
- intuitive-is-just-familiar-predictive-fit-rep-v2.md
- project-atlas-comparative-color-framework-v0-1.md
- gn-100-perception-autonomous-research-report-v1.md
- composition-science-markdown-template-v1.md
- project-atlas-rosetta-stone-cross-system-design-mechanism-map-v0-1.md
- project-atlas-foundational-documents-and-ribbon-evidence-collection-001.md
- df-atlas-color-005-perceptual-measurement-layer.md
- project-atlas-masters-vs-modern-science-01-chevreul-v0-1.md
- rp-atlas-color-004-munsell-vs-modern-perceptual-color-spaces.md

## Phase F: Generate Registries And Site

- Build evidence, hypothesis, experiment, and decision registries from metadata and document sections.
- Publish the generated website only after validation gates pass.

## Exit Condition

The repository exits migration mode when canonical concepts, registries, and search indexes are generated automatically and intake documents can be promoted without manual navigation edits.
