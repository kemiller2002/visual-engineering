---
id: GV-VE-2026-CF0E
project: visual-engineering-knowledge-platform
purposes:
  - decide
  - verify
audiences:
  - executive
  - contributor
---

# Risk Assessment

Generated: 2026-07-22T15:10:59+00:00

## Rejected Alternatives

- Discipline-first folders as the primary architecture
- Project-only organization without shared concept ownership
- Docs-only static site generators as the full platform model
- Manual curation of registries and backlinks

## Architectural Tradeoffs

- A hybrid concept-plus-project model is more complex than a plain docs tree, but it preserves cross-project reasoning.
- Hybrid retrieval requires more infrastructure than local full-text search, but single-mode search will fail at scale.
- Typed metadata adds authoring overhead, but the repository is already large enough that untyped growth would create long-term entropy.

## Scalability Risks

- Flat intake directories become unmanageable well before 10,000 documents.
- Semantic indexing costs increase sharply if chunking and canonical summaries are not normalized.
- Graph density can become noisy without controlled relationship vocabularies.

## Maintenance Costs

- Metadata linting and schema evolution must be treated as first-class engineering work.
- Canonical concept ownership requires editorial governance.
- Search quality tuning needs periodic evaluation against real queries.

## Technical Debt To Avoid

- Hard-coding navigation in website templates
- Allowing duplicate status vocabularies
- Embedding relationships only in prose
- Using filenames as the only durable identifiers

## Future Migration Concerns

- If Astro no longer fits, keep generated data products framework-agnostic so the site can be replatformed without reclassifying the corpus.
- Preserve immutable manifests and graph exports so future researchers can reconstruct repository state independent of the website stack.
