---
id: GV-VE-2026-630E
project: visual-engineering-knowledge-platform
purposes:
  - decide
  - apply
  - reference
audiences:
  - practitioner
  - contributor
---

# Search Architecture

Generated: 2026-07-22T15:10:59+00:00

## Recommendation

Use hybrid retrieval:

- Metadata filters for precision
- BM25 or equivalent lexical search for exact terminology
- Embedding search for semantic recall
- Graph traversal for concept-aware expansion and citation paths

## Retrieval Stack

1. Normalize metadata and chunk canonical summaries plus section-level document chunks.
2. Build a lexical index over full text, titles, headings, IDs, and concept labels.
3. Build a vector index over canonical summaries and semantically chunked sections.
4. Build a graph index over concepts, documents, evidence, and lineage edges.
5. Fuse results with confidence, authority, and recency weighting.

## Query Modes

- Concept search
- Document search
- Evidence search
- Timeline search
- Relationship search
- Project-scoped search

## Ranking Factors

- Title and heading match
- Canonical status
- Authority score
- Confidence score
- Shared concept count
- Citation and backlink count
- Version freshness without hiding superseded history

## Why Not Single-Mode Search

Lexical search alone misses conceptual paraphrases. Vector search alone hides exact IDs, terminology, and structured filters. Graph traversal alone cannot serve as the first-pass retriever. The repository needs all three because it is a research corpus rather than a simple doc site.

## Current Product Research

- Quartz documents built-in full-text search, graph view, wikilinks, transclusions, and backlinks: <https://quartz.jzhao.xyz/>
- VitePress documents local full-text search with an in-browser index and Algolia support: <https://vitepress.dev/reference/default-theme-search>
- Docusaurus documents official Algolia DocSearch support and contextual search across versions: <https://docusaurus.io/docs/search>
