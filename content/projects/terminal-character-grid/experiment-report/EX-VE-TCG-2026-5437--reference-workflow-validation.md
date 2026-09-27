---
id: EX-VE-TCG-2026-5437
title: Terminal / Character-Grid Reference Workflow Validation
project: terminal-character-grid
date: 2026-09-27
status: complete
conclusion: supported-with-gaps
work_item: GH-20
author_agent: Anthropic Claude
related_documents:
  - ../concept/CN-VE-TCG-2026-6CA0--terminal-character-grid-abstraction-boundary.md
  - ../concept/CN-VE-TCG-2026-F9F1--sequential-reveal-behavior.md
purposes:
  - verify
  - reference
audiences:
  - researcher
  - practitioner
  - contributor
---

# EX-VE-TCG-2026-5437: Reference Workflow Validation

## Question

Is the `LAY-TERMINAL-CHARACTER-GRID` family sufficient to express a
nontrivial Customer Inquiry → Account Detail → Transaction History workflow
**without product-specific abstractions**, and which Forma capability gaps
does the exercise expose?

## Method

1. The three screens and their states were encoded as data in
   [`content/layouts/reference-workflows/customer-account-inquiry.json`](../../../layouts/reference-workflows/customer-account-inquiry.json)
   using only the family's vocabulary: eight run kinds (`heading`, `text`,
   `value`, `field`, `table`, `message`, `status`, `action`), a geometry, and
   reserved rows. Data are fictitious.
2. `scripts/character-grid-layout.mjs` checks every screen and every state
   against the family's relational rules: bounds, no row wrap, no collisions,
   protected/field overflow, source order = row-major order, labels that
   exist and precede their fields, invalid fields that reference a message,
   message severity, required regions, reserved message/action/status rows,
   table empty states, and that every application transition is triggered by
   an action the screen presents.
3. `scripts/test-character-grid-layout.mjs` shows each rule fails closed on a
   counterexample (12 tests).

**Limitation.** This validates the relational grammar only. It does not
render anything, measure legibility, or involve users. Rendering, focus, 320/390 px
containment, 200 % text, forced colors, and reduced motion are verified in
Forma (#44). All task-performance questions remain hypotheses (HYR-VE-TCG-001).

## Screens

Geometry 24 × 80 (3270 profile). Reserved rows: 21 message, 22–23 actions,
24 system status. Content rows: 1–20.

### Screen 1 — Customer Inquiry (`CINQ`)

- Protected context: screen code, title, date, system, operator.
- Six editable fields with preceding labels: customer number (10, numeric),
  last name (20), first name (15), date of birth (10, with a format hint),
  postal code (10), account number (14).
- Actions: Enter=Search, Clear=Erase, Reset=Unlock, PF1=Help, PF3=Exit.
- States: default; `validation` (date of birth invalid, described by both the
  hint and the message); `no-criteria` (error); `not-found` (warning);
  `busy` (system status "X SYSTEM WAITING FOR HOST"); `maximum-input` (every
  field filled to capacity).

### Screen 2 — Account Detail (`ACCD`)

- Protected only: no editable fields, so focus goes straight to actions.
- Grouping by protected headings `ACCOUNT` and `BALANCES`; label/value pairs
  aligned to shared columns (20 and 55); amounts end-aligned in 15-cell runs.
- Missing content rendered as explicit text ("not on file").
- Actions: Enter=Refresh, PF3=Return, PF5=Transactions, PF12=Inquiry, PF1=Help.
- States: `restricted` (warning); `extreme-values` (30-cell name, negative
  15-cell amounts, 40-cell address); `missing-values`.

### Screen 3 — Transaction History (`TRNH`)

- Protected account; two required editable date fields (From, To).
- A repeated-row table at row 5: header plus 12 visible records; columns Date
  (10), Description (27), Amount (12, end), Balance (12, end), Status (8:
  POSTED, PENDING, RETURNED, REVERSED).
- Paging position ("Rows 1-12 of 37", "More: +") as protected text.
- Actions: Enter=Apply dates, PF3=Return, PF7=Back, PF8=Forward,
  PF12=Inquiry, Reset=Unlock, PF1=Help.
- States: `empty` (explicit empty-state line), `error` (service failure,
  status "X SYSTEM INPUT INHIBITED"), `validation` (From after To),
  `last-page`.

## Results

`node scripts/character-grid-layout.mjs --report` on the committed fixture:
**workflow valid (15 screen states)**. Measured occupancy (cells inside
declared runs, including reserved table and message cells, not only visible
glyphs):

| Screen state | Occupied cells | Share of 1920 |
| --- | ---: | ---: |
| CINQ (all 6 states) | 630 | 32.8 % |
| ACCD (all 4 states) | 761 | 39.6 % |
| TRNH default / validation | 1387 | 72.2 % |
| TRNH empty | 1383 | 72.0 % |
| TRNH error / last-page | 1388 | 72.3 % |

Derived focus orders (row-major, fields then actions because actions occupy
the bottom rows):

- CINQ: f-custno › f-last › f-first › f-dob › f-postal › f-acct › Enter ›
  Clear › Reset › PF1 › PF3
- ACCD: Enter › PF3 › PF5 › PF12 › PF1
- TRNH: f-from › f-to › Enter › PF3 › PF7 › PF8 › PF12 › Reset › PF1

The first validation run failed: the `TRNH#error` message was 79 cells in a
78-cell message run. The validator caught it as an authoring error, which is
the intended behavior, and it exposed finding F-3.

## Sufficiency

| Requirement from #20 | Family construct | Sufficient? |
| --- | --- | --- |
| Multiple editable fields | `field` with label, length, input mode | Yes |
| Validation | `field.invalid` + `describedBy` → `message` (severity `validation`) | Yes |
| Commands/actions | `action` (key + label) in reserved rows | Yes |
| Status messages | `message` (5 severities) + `status` | Yes |
| Keyboard traversal | Derived from row-major source order | Yes |
| Protected/static data | `value` / `text` / `heading` | Yes |
| Meaningful grouping | Protected headings, blank rows, shared columns | Yes |
| Transitions | Application-owned transition table referencing presented actions | Yes, outside the family by design |
| Dense repeated rows | `table` with cell-measured columns | Yes, but see GAP-TCG-07 |
| Dates, descriptions, amounts, statuses | Table columns with lengths and alignment | Yes |
| Empty state | `table.emptyText` | Yes |
| Error state | `message` severity `error` + `status` | Yes |
| Navigation | PF7/PF8 actions + protected position text | Yes |

**Conclusion: supported with gaps.** No screen needed a concept outside the
eight generic run kinds. The only 3270-specific content is *data* — action key
names (PF3), screen codes, and the status wording — so the same structure
would serve a 5250 profile (F3, Roll Up) or a modern profile (named actions)
by changing data, not structure. No one-off concepts were added to make the
screens work; the findings below are recorded instead.

## Findings and capability gaps

- **F-1 / GAP-TCG-07 — Positioned repeated-row table.** Dense rows need a
  table whose columns are measured in cells, whose numeric columns end-align
  within fixed runs, and which keeps header-to-cell association. Forma has
  tables, but none placed on a character grid.
- **F-2 / GAP-TCG-08 — Field ↔ message association.** A field must be able
  to reference the grid's message region and a hint at the same time
  (`aria-describedby` with several IDs). The first model allowed only one
  reference; it was generalized to a list because native HTML already
  supports it.
- **F-3 / GAP-TCG-09 — Runtime message length.** Messages are produced by
  the application at runtime, so static validation cannot guarantee they fit
  one 78-cell row. The family needs a declared overflow policy for message
  regions (for example: wrap into a second reserved row, or show a truncated
  line with the full text available to assistive technology and on request).
  This is a family-level rule to decide, not something to patch per screen.
- **F-4 — Initial cursor position is application behavior.** The 3270 sets
  the cursor per write. On the web, which field receives initial focus (for
  example, the first invalid field) is decided by the application/Limen; the
  native `autofocus` attribute is the only markup Forma needs to permit. No
  new primitive.
- **F-5 — Input inhibition is application state.** The `busy` and `error`
  states show "X SYSTEM" in the status region. Whether fields are also
  disabled while inhibited is application behavior; Forma only needs to
  present `disabled` natively.
- **F-6 / GAP-TCG-10 — Row selection idiom (not exercised).** Many terminal
  applications select a record by typing into a per-row selection column.
  The reference workflow deliberately navigates by PF keys instead, so the
  family has no verified rule for per-row fields (focus order within dense
  rows, labelling each row's field). Recorded as open.
- **F-7 — Screen without fields.** ACCD has no editable fields, so the first
  Tab reaches an action. That is correct under the family rules; whether
  users expect focus on the message or title first is untested.

## SequentialReveal in the workflow

Only the `CINQ` title is designated for reveal, on first display in a
session, following CN-VE-TCG-2026-F9F1. Fields, actions, messages, status,
and every value on ACCD and TRNH are excluded, because those screens exist to
verify consequential data.

## Next

- Forma implements the primitives and closes or records GAP-TCG-01…09.
- Decide the message overflow policy (GAP-TCG-09) in the family entry.
- Test HY-VE-TCG-2026-9151 and -8750 with users once Forma renders the
  reference screens.
