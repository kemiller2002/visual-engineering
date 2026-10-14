---
id: DF-VE-ID-2026-5C1E
title: Canonical Identifiers for Every Published Document
date: 2026-10-14
status: accepted
work_item: WI-0020
author_agent: Anthropic Claude
purposes:
  - decide
  - reference
audiences:
  - contributor
  - researcher
---

# DF-VE-ID-2026-5C1E: Canonical Identifiers for Every Published Document

## Context

The research site published 115 documents with no canonical identifier. The
research publisher reported each one as a `missing-id` warning and catalogued
it with `id: null`. Its filename fallback only recognises the retired
`XX-2026-001` pattern, so it cannot infer current identifiers.

Three groups of documents lacked an identifier:

1. Research notes, reports, canonical syntheses, concept pages, and theme
   research that predate or were imported outside the identifier standard.
2. Governance, operating, and navigation documents (standards, protocols,
   agent guidance, context, platform architecture, READMEs, registry indexes).
   The Praxis identifier standard (`framework/standards/IDENTIFIERS.md`) has no
   prefix for them.
3. Praxis-installed governance documents and product tooling documentation.
   Praxis marks the governance documents `tool-owned` and replaces them on
   upgrade, so an identifier added here would be lost.

Allocation was not the cause. The standard already uses random hexadecimal
tokens with collision retry, so parallel branches cannot collide on a shared
counter. Timestamps or GUIDs would not have assigned identifiers to documents
that never received one.

## Decision

1. **Keep the standard's format and allocation.** Identifiers remain
   `<PREFIX>-<AREA>-<YYYY>-<TOKEN>` with a random four-character hexadecimal
   token, retried on collision, with the validator as uniqueness authority.
2. **Add two Visual Engineering prefixes** alongside the Praxis set:
   - `GV`: governance, operating, and navigation documents. `AGENTS.md`
     (`GV-START-001`) already used it.
   - `RR`: research reports and syntheses that are not a single evidence,
     hypothesis, experiment, theory, or decision record. This covers notes,
     reports, studies, canonical syntheses, audits, case studies, plans,
     frameworks, crosswalks, galleries, and evidence collections.
3. **Area codes:** `VE` for repository-wide documents, `VE-<PROJECT>` for
   project content (`VE-ATL` Project Atlas, `VE-COMP` Composition Science,
   `VE-PG` Product Genome, `VE-DL` Design Library, `VE-THM` themes).
   `YYYY` is the year the file was first committed.
4. **Backfilled documents keep their paths.** Renaming more than 100 files to
   start with their identifier would break inbound links and published URLs.
   The filename rule continues to apply to newly created artifacts.
5. **Praxis-owned governance documents and product tooling documentation are
   not published to the research site.** This matches the existing exclusion
   of `docs/**` and `README.md`. Adding the prefixes to the Praxis standard
   itself is an upstream change.
6. **A missing identifier fails CI.** `npm run research:ids -- --check` runs
   after research validation and fails if any published document lacks an
   identifier. `npm run research:ids -- --write` assigns them.

## Alternatives considered

- **Sequential numbers.** Unsafe across parallel branches without a lock; the
  standard already rejects this.
- **Timestamps.** Can still collide across branches within the same second,
  disclose creation time, and are no easier to read.
- **GUIDs.** Effectively collision-free, but 36 characters long and unreadable
  in filenames and citations. Four hexadecimal characters per prefix, area,
  and year give 65,536 identifiers with retry, which is sufficient.
- **Editing the Praxis-owned documents in place.** Praxis would overwrite the
  edits on upgrade.

## Consequences

- Every published document has a stable identifier, so catalog lookups by
  `id` no longer skip records.
- `scripts/artifact-ids.mjs` is the only allocator for backfill; it reads the
  publisher's diagnostics so publication rules stay in one place.
- The 12 Praxis governance documents under `framework/`, `BOOTSTRAP.md`,
  `CHANGELOG.md`, and the tool READMEs no longer appear on the research site.
  They remain in the repository and in the installed Praxis documentation.
