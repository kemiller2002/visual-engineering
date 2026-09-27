---
project: visual-engineering
purposes:
  - reference
  - apply
audiences:
  - contributor
  - practitioner
---

# Layout Family Catalog

This catalog records layout families as **relationships that an
implementation must preserve**, with the evidence, assumptions, and
verification requirements behind them. A family is not a screenshot, a
template, or a component.

## Identity

Every family has a permanent `LAY-*` identifier. The identifier space is shared
with Forma's `catalog/layouts.json`, which records Forma's verified
implementation of families. Visual Engineering owns the relationships and
evidence; Forma owns the presentation contract and its verification.

## Files

- `index.json` — catalog version, support states, and the family list.
- `layout-family.schema.json` — the required structure of a family record.
- `families/<ID>.json` — one record per family.

Validate with `npm run layouts:validate`; the validator's own tests run with
`npm run layouts:test`.

## Record contract

A record must state:

1. **Intended tasks** and, where useful, tasks it is not for.
2. **Semantic regions** and whether each is required.
3. **Authoritative source order** and its consequence for reading and focus.
4. **Recognition path** — what a user should identify first, second, and so on.
5. **Deliberate-verification regions** — where the user must check before acting.
6. **Composition** — spatial relationships, alignment, density, landmarks.
7. **Navigation** — keyboard, focus, visual, and reading order, and decision
   points.
8. **Preserved relationships** — what must survive every recomposition.
9. **Responsive strategy** — wide, narrow, and zoom behavior, and a statement
   that page-level horizontal overflow is prohibited.
10. **Content stress** — long, missing, extreme, and localized content.
11. **Accessibility expectations.**
12. **Required Forma primitives and known Forma capability gaps.**
13. **Assumptions** and how each will be tested.
14. **Verification requirements**, each tagged static, DOM, browser, or review.
15. **Evidence and hypotheses** by ID. Every cited ID must exist in a registry.

## Support states

`observed` → `partial` → `supported` → `verified`. A Visual Engineering record
is `observed` when the relationships are evidence-backed but no conforming
implementation has been verified. `verified` requires a recorded conformance
run of an implementation (Forma records these in its own catalog).

## Relationship to Forma

Forma's `catalog/layouts.json` family fields (`id`, `name`, `tasks`,
`recognitionPath`, `verificationRegions`, `composition`,
`preservedRelationships`, `narrowRecomposition`, `status`, `evidence`, `gaps`)
are a subset of this record. Forma should consume the record's relationships,
not copy its prose.

## Current families

| ID | Name | Status |
| --- | --- | --- |
| LAY-TERMINAL-CHARACTER-GRID | Terminal / character grid | observed |

Families already verified in Forma (`LAY-FORM-SECTIONED` and others) predate
this catalog and have not been back-filled here. See decision
DF-VE-TCG-2026-2DD5.
