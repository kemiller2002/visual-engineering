---
id: GV-VE-2026-3ADC
project: visual-engineering-knowledge-platform
purposes:
  - reference
audiences:
  - contributor
---

# Metadata Standard

Generated: 2026-07-22T15:10:59+00:00

## Canonical Front Matter

```yaml
---
id: DOC-0001
title: Example Title
abstract: One-paragraph summary focused on evidence and scope.
authors:
  - Kevin Miller
created: 2026-07-21
updated: 2026-07-21
project: Composition Science
discipline: Composition Science
research_area:
  - perception
  - hierarchy
document_type: research-report
status: research-draft
evidence_level: B
confidence: medium
canonical: false
concepts:
  - perception
  - hierarchy
supersedes: []
superseded_by: []
related_documents: []
related_concepts: []
tags: []
keywords: []
source_stage: intake
reading_time_minutes: 12
machine_readable: true
llm_ingest: true
---
```

## Required Fields

- `id`
- `title`
- `abstract`
- `created`
- `updated`
- `project`
- `document_type`
- `status`
- `canonical`
- `concepts`

## Controlled Vocabularies

- Status: `intake`, `draft`, `working-draft`, `research-draft`, `verified`, `approved`, `superseded`, `archived`
- Document type: `governance`, `ontology`, `knowledge-model`, `research-report`, `phase-report`, `evidence-registry`, `case-study`, `comparative-study`, `journal-entry`, `decision-record`, `experiment-report`
- Confidence: `low`, `medium`, `high`
- Evidence level: `A`, `B`, `C`, `D`, `E`

## Standardization Notes

- Use stable IDs instead of filename-only identity.
- Separate `created` from `updated`; do not overload `date`.
- Prefer arrays for concepts, tags, and relationships.
- Keep filenames human-readable but treat metadata as the source of truth.
