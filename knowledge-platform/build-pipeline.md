---
id: GV-VE-2026-81B4
project: visual-engineering-knowledge-platform
purposes:
  - decide
  - apply
  - reference
audiences:
  - practitioner
  - contributor
---

# Build Pipeline

Generated: 2026-07-22T15:10:59+00:00

## Pipeline

```text
Repository Intake
  -> Inventory Scan
  -> Metadata Normalization
  -> Duplicate Detection
  -> Concept Extraction
  -> Relationship Extraction
  -> Canonicality Scoring
  -> Registry Generation
  -> Search Index Build
  -> Graph Build
  -> Website Build
  -> Validation
  -> Publish
  -> Version Archive
```

## Stages

1. Inventory scan
   Emit machine-readable catalog and repository statistics.
2. Metadata normalization
   Validate front matter, controlled vocabularies, IDs, and dates.
3. Relationship extraction
   Generate concept, citation, lineage, and similarity edges.
4. Registry generation
   Materialize evidence, hypothesis, experiment, and decision indexes.
5. Search index build
   Produce lexical, semantic, and graph-aware indexes.
6. Site generation
   Build a static site from content and generated data.
7. Validation
   Check broken links, missing metadata, duplicate IDs, stale lineage, and empty canonical pages.
8. Publish and archive
   Publish the current site and preserve immutable build artifacts for historical reconstruction.
