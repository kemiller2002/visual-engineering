---
id: GV-VE-2026-061F
project: visual-engineering-knowledge-platform
purposes:
  - decide
  - apply
  - reference
audiences:
  - practitioner
  - contributor
---

# Website Architecture

Generated: 2026-07-22T15:10:59+00:00

## Recommendation

Use Astro as the website framework and treat the website as a generated presentation layer over repository data products.

## Why Astro Wins

- Content collections provide a typed model for Markdown, JSON, and generated data products: <https://docs.astro.build/en/guides/content-collections/>
- Astro can stay mostly static while still supporting interactive graph and search islands.
- It is flexible enough to host canonical concept pages, generated registries, timelines, and custom visualization pages without forcing a docs-only information model.

## Why The Others Lose For This Repository

- Quartz is strong for note-native graphs and backlinks, but its Obsidian-first model is too opinionated for a multi-registry scientific platform.
- MkDocs is efficient for docs portals, but its core search and navigation model is too shallow for concept, graph, and registry-heavy needs: <https://www.mkdocs.org/user-guide/configuration/>
- Docusaurus is mature for documentation versioning and hosted search, but its primary abstraction is versioned docs rather than a knowledge graph platform: <https://docusaurus.io/docs/versioning>
- VitePress is elegant for documentation with local search, but still docs-centric: <https://vitepress.dev/reference/default-theme-search>
- Hugo and Eleventy are capable but would require more custom data plumbing for typed relationships and interactive knowledge views: <https://gohugo.io/methods/site/sections/> and <https://www.11ty.dev/>
- Next.js is viable if server features are required later, but it introduces unnecessary application weight for a mostly static scientific archive: <https://nextjs.org/docs/app/glossary>

## Required Site Sections

- Home
- Repository Health
- Recent Research
- Concepts
- Projects
- Evidence Registry
- Hypothesis Registry
- Experiment Registry
- Decision Records
- Timelines
- Knowledge Graph
- Reading Paths
- Search

## Rendering Model

- Canonical content pages are generated from Markdown.
- Registry pages are generated from normalized data.
- Graph and timeline views are generated from `data/graph` and `data/lineage`.
- Search UI queries a prebuilt lexical index plus a vector service or local semantic index.
