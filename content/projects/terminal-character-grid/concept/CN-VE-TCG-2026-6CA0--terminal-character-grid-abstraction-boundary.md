---
id: CN-VE-TCG-2026-6CA0
title: Terminal / Character-Grid Abstraction Boundary
project: terminal-character-grid
version: 1.1
status: evaluated
confidence: medium
work_item: GH-17
author_agent: Anthropic Claude
date: 2026-09-27
related_documents:
  - ../research-execution-package/RP-VE-TCG-2026-DD75--terminal-character-grid-family.md
  - ../evidence-registry/terminal-character-grid-evidence-v1.md
  - ../hypothesis-registry/terminal-character-grid-hypotheses-v1.md
purposes:
  - reference
  - verify
audiences:
  - researcher
  - practitioner
  - contributor
tags:
  - terminal
  - character-grid
  - keyboard-first
  - ibm-3270
  - layout-family
---

# CN-VE-TCG-2026-6CA0: Terminal / Character-Grid Abstraction Boundary

## Question

What is invariant across IBM 3270, IBM 5250, DOS text-mode, BBS, terminal/TUI,
and modern keyboard-first character-grid interfaces, and where does the
abstraction stop?

The answer is used by the layout catalog entry (GH-18), the SequentialReveal
behavior (GH-19), the reference workflow (GH-20), and Forma's CharacterGrid
contracts (Forma #37).

## Confidence

**Medium.** The 3270 structure is supported by the architecture reference and
a maintained emulator (EV-VE-TCG-2026-CA91, -732A, -E645, -DFA5, -21B3, -D0F7,
-3E25). 5250 is supported by one emulator (EV-VE-TCG-2026-2966). DOS, BBS, and
modern TUI characteristics are **not** supported by sources opened in this
work; they appear only as assumptions below. No user study has been run, so
every claim about task performance is a hypothesis
(HYR-VE-TCG-001), not a finding.

## The model in one paragraph

A character-grid interface is a **fixed rectangle of R rows × C columns of
equal-width cells**. Every visible element occupies a **contiguous run of
cells** identified by a **(row, column, length)** triple. Runs are either
**protected** (the operator cannot change them) or **editable** (fields).
Because a coordinate can be linearized as `row × C + column`, the grid has a
single canonical **row-major order**, and that order simultaneously defines
storage/transmission order, the default traversal order of fields, and the
most defensible reading order. **Status** and **action affordances** occupy
reserved runs, usually at the bottom rows. Everything else — colors, glow,
typefaces, blinking cursors, character-by-character reveal — is presentation.

## Invariants

Each invariant is stated as a relationship, followed by its support.

| # | Invariant | Support | Scope |
| --- | --- | --- | --- |
| I-1 | **Fixed grid.** The surface has explicit integer dimensions R × C and every cell has equal advance width. | EV-VE-TCG-2026-CA91, -21B3, -D0F7, -2966 | 3270, 5250 verified; others assumed |
| I-2 | **Coordinate placement.** Every element has an explicit (row, column) origin and a length in cells. Position is data, not the output of a flow layout. | EV-VE-TCG-2026-CA91, -732A | 3270 verified |
| I-3 | **Row-major linearization.** `address = row × C + column` gives a total order over cells. | EV-VE-TCG-2026-CA91 (Figure 1-1) | 3270 verified |
| I-4 | **Protected vs editable is a property of a run**, not of a separate widget type. Protected runs cannot receive input. | EV-VE-TCG-2026-E645 | 3270 verified; 5250 consistent |
| I-5 | **Explicit field extent.** A field's capacity is a fixed number of cells known before input; input beyond it is impossible, not truncated after the fact. | EV-VE-TCG-2026-732A (extent to next attribute) | 3270 verified |
| I-6 | **Non-overlap.** Two runs cannot occupy the same cell. (In the 3270 this is structural: a cell holds one character or one attribute.) | EV-VE-TCG-2026-CA91, -732A | 3270 verified |
| I-7 | **Deterministic traversal derived from position.** Tab/BackTab move to the next/previous editable field in row-major order. | EV-VE-TCG-2026-3E25 | 3270 verified in emulator |
| I-8 | **Reserved status region.** The system reports lock/wait/error state in a region distinct from application fields. | EV-VE-TCG-2026-DFA5 (input inhibited), -2966 (indicators, message line) | 3270 state verified, placement assumed; 5250 verified |
| I-9 | **Named action keys.** A small vocabulary of named actions (Enter, Clear, PF1–PF24, PA1–PA3 for 3270; F1–F24, Help, Roll, Field Exit for 5250) submits or modifies the screen. | EV-VE-TCG-2026-DFA5, -2966 | Vocabulary is **profile-specific** |
| I-10 | **Keyboard-first.** Every operation is reachable from the keyboard; pointer input is optional. | EV-VE-TCG-2026-DFA5 (selector pen is optional) | 3270 verified |
| I-11 | **Density.** A screen commonly uses most of its cells; whitespace is expressed as blank cells, not proportional spacing. | Inferred from I-1/I-2; no density measurement taken | Assumption A-4 |

## What is **not** invariant

| Property | Why it is excluded from the generic abstraction |
| --- | --- |
| A visible cell reserved for each field boundary | A 3270 encoding cost (EV-VE-TCG-2026-732A). 5250 and web fields do not require it. A profile may *render* a boundary glyph; the model does not reserve a cell. |
| Field wrap across rows and from the last cell to the first | Architecturally allowed in 3270 (EV-VE-TCG-2026-732A) but unusual in practice and incompatible with native text inputs. The generic model **forbids** row-wrapping runs (see Boundary rule B-3). |
| Wraparound Tab from last field to first | Authentic 3270 (EV-VE-TCG-2026-3E25), but on the web Tab must be able to leave the terminal region. Wraparound is non-native behavior and belongs to the application/Limen if wanted. |
| PF-key semantics (e.g., PF3 = Exit, PF7/PF8 = page) | Application conventions, not architecture. The 3270 defines only that the key was pressed (AID). |
| Colors (green-on-black, the 7-color 3279 palette) | Extended attributes are optional (EV-VE-TCG-2026-C4C6). Base emphasis is intensity/highlighting and was device-dependent (EV-VE-TCG-2026-E645). |
| A blinking block cursor | Stylistic. Focus must be visible (WCAG 2.4.7) but its shape is presentation. |
| Character-by-character reveal | **Not** authentic 3270 behavior. See the classification table and the SequentialReveal definition (GH-19). |
| 24 × 80 specifically | The 3270 default only (EV-VE-TCG-2026-21B3); 32 × 80, 43 × 80, 27 × 132 (3270) and 27 × 132 (5250) exist. Geometry is a parameter. |

## Four-way classification

Every characteristic considered for the family is classified as exactly one
of the following. Implementations must preserve the category when describing
themselves; in particular, a category-3 or -4 behavior must never be described
as "authentic 3270".

| Characteristic | 1. Actual 3270 | 2. Generalized terminal | 3. Stylistic choice | 4. New Echelon behavior |
| --- | :---: | :---: | :---: | :---: |
| Fixed R × C grid | ✓ (source) | ✓ | | |
| Row-major address order | ✓ (source) | ✓ | | |
| Protected / unprotected runs | ✓ (source) | ✓ | | |
| Numeric-only fields | ✓ (source) | ✓ (as input constraint) | | |
| Nondisplay (masked) fields | ✓ (source) | ✓ | | |
| Mandatory entry / mandatory fill | ✓ (source, extended attr.) | ✓ (as `required`) | | |
| Field attribute occupies a blank cell | ✓ (source) | | | |
| Tab/BackTab by position | ✓ (emulator) | ✓ | | |
| Wraparound Tab | ✓ (emulator) | | | Application option only |
| Enter / Clear / PF / PA as submissions | ✓ (source) | ✓ (as named actions) | | |
| Reset as local unlock | ✓ (emulator) | ✓ (as local action) | | |
| Keyboard-lock / input-inhibited status | ✓ (source) | ✓ | | |
| Status row below application rows (OIA) | ✓ (emulator, EV-VE-TCG-2026-1EB5) | ✓ (reserved status region) | Status on an application row (GAP-TCG-12) | |
| Action-key legend row (e.g. "PF3=Exit") | | ✓ (application convention) | | |
| Green/amber phosphor palette | | | ✓ | |
| Scanlines, glow, bezel | | | ✓ | |
| Blinking block cursor | | | ✓ | |
| Uppercase-only text | | | ✓ (device/codepage history, not required) | |
| Character-by-character SequentialReveal | ✗ | Partially: evokes slow serial/BBS links (unverified) | ✓ when used for mood | ✓ as a defined, reduced-motion-safe behavior (GH-19) |
| Contained horizontal scroll at narrow widths | | | | ✓ (web adaptation) |
| Native HTML inputs for fields | | | | ✓ (web adaptation) |
| Visible labels programmatically associated to fields | | | | ✓ (accessibility requirement) |
| Status messages exposed as live regions | | | | ✓ (WCAG 4.1.3) |

## Web translation of the invariants

These are the relationships a web implementation must preserve. They are the
input to Forma's CharacterGrid contract; how Forma names them is Forma's
decision.

1. **Grid geometry is declared**, not inferred: an explicit row count and
   column count. The cell width is `1ch` of a monospaced face, so the grid
   scales with text size (supports WCAG 1.4.4) instead of with viewport width.
2. **Placement is declared** per element as (row, column, length), 1-based to
   match terminal documentation. The rendered position must equal the
   declared position.
3. **Source order is row-major order.** Authors write elements in ascending
   `row × C + column`. This makes DOM order, reading order, focus order, and
   visual order the same story (EV-VE-TCG-2026-E22E, -FEFD) **without** `tabindex`
   values. Row-major traversal (I-7) is then provided natively by the browser.
4. **Protected runs are text, not controls.** A protected value is rendered as
   non-interactive text (with an associated label where it is a value). It
   must not be an `input` with `readonly` unless it is genuinely a form value
   the application reads back, because a read-only input is focusable and is
   announced as an edit control. The layout catalog records this decision.
5. **Editable runs are native form controls** (`input`, `select` where a
   profile has a choice field) with a programmatic label, `maxlength` equal to
   the field length, and native `required`, `disabled`, `readonly`, and
   `aria-invalid` states.
6. **Status is a live region** (`role="status"` for information,
   `role="alert"` only for errors requiring immediate attention), with a text
   prefix or glyph so severity is not conveyed by color alone (EV-VE-TCG-2026-61DF,
   -E9AC).
7. **Action keys are real buttons** with visible names ("PF3 Exit"), so they
   are operable without a physical function key. Mapping physical keys
   (F3, Enter, Escape) to those actions is application/Limen behavior.
8. **Narrow screens keep the grid** inside a contained, keyboard-reachable
   horizontal scroller by default, and never create page-level horizontal
   overflow. A linearized (reflowed) presentation is a separately declared
   alternative for screens whose positions carry no meaning (see B-6 and
   HY-VE-TCG-2026-8750).

## Boundary rules

- **B-1 Ownership.** Visual Engineering owns the relationships above and
  their evidence. Forma owns semantic HTML/CSS contracts that express them.
  The consuming application/Limen owns key mapping (F-keys → actions),
  Enter/Clear/Reset processing, screen transitions, runtime reveal
  orchestration, and application state. Ordo/the application domain owns
  legal domain-state transitions. No layer may acquire another's authority to
  make a demo easier.
- **B-2 No collision.** Two runs may not share a cell. A layout with overlap is
  invalid, not "last writer wins".
- **B-3 No row wrap.** A run must satisfy `column + length − 1 ≤ C`. Multi-row
  content is expressed as multiple runs. (Excludes 3270 field wrap; see table.)
- **B-4 In bounds.** `1 ≤ row ≤ R` and `1 ≤ column ≤ C`. Out-of-bounds content
  is invalid; it is not clipped silently.
- **B-5 Overflow is truncation-proof.** Protected text longer than its declared
  length is an authoring error to be caught by validation, not by visual
  clipping, because clipping silently hides data. Editable input cannot exceed
  its length (`maxlength`).
- **B-6 Narrow-screen strategy is a declared choice.** `contained` (preserve
  grid, scroll inside a focusable region) or `reflow` (linearize in source
  order). WCAG 1.4.10's exception applies only where two-dimensional layout is
  required for usage or meaning (EV-VE-TCG-2026-62B1); the author must be able to justify
  `contained`.
- **B-7 Motion is never authority.** No information, state, or action may
  depend on an animation having completed (GH-19).
- **B-8 Profile ≠ primitive.** A profile (3270, 5250, DOS, BBS, modern) may set
  geometry, palette, typeface, cursor shape, boundary glyphs, and action
  vocabulary. It may not add placement, ordering, or state semantics that the
  generic grid lacks. If a profile needs one, the generic abstraction has a gap.

## Assumptions

| ID | Assumption | Risk if false | How to test |
| --- | --- | --- | --- |
| A-1 | Users of modern keyboard-first grid applications expect row-major field order. | Focus order feels wrong for column-oriented forms. | HY-VE-TCG-2026-9151 |
| A-2 | DOS text-mode, BBS, and modern TUIs share I-1…I-7. | The family over-generalizes from mainframe terminals. | Verify with primary sources (ECMA-48, VGA text mode documentation, a curses reference); record as evidence. **Partially verified:** DOS text mode (EV-VE-TCG-2026-D9CB); BBS and curses still unverified. |
| A-3 | The 3270 OIA is a line below the application rows. | Status placement in the 3270 profile is inaccurate. | Obtain IBM 3278/3279 or 3174 documentation. **Supported as implemented** by x3270 (EV-VE-TCG-2026-1EB5); the 3270 reference screens place status on an application row, recorded as GAP-TCG-12. |
| A-4 | Dense, cell-aligned presentation supports expert scanning. | Density harms novices without benefiting experts. | HY-VE-TCG-2026-3D56 |
| A-5 | `1ch` of a monospaced face is a stable cell width across engines and zoom levels. | Grid misalignment at 200 %. | Browser verification in Forma (#44). **Refined:** stable for layout, but glyph advances exceed `1ch` by accumulating sub-pixel amounts (EV-VE-TCG-2026-C2ED). |

## Unknowns

- Whether screen-reader users are better served by row-major reading of a
  dense account-detail screen or by a label/value list. Only user testing can
  settle this.
- Whether contained horizontal scrolling at 320 px is usable for the
  reference workflow, or whether `reflow` is always better. Not tested.
- How CJK or other wide-glyph scripts should occupy cells (one glyph = two
  cells in many terminals). Not researched; localization of the grid is an
  open gap recorded in the catalog entry.

## Risks

- **Aesthetic capture:** teams may copy the look (green text, scanlines) and
  skip the relationships. Mitigation: the catalog entry is relational and the
  anti-pattern list names it.
- **Authenticity inflation:** describing SequentialReveal or wraparound Tab as
  "how the 3270 works". Mitigation: the four-way classification above.
- **Accessibility regression through fidelity:** reproducing autoskip,
  wraparound, or uppercase-only text can harm users. Mitigation: those are
  profile options or excluded.
