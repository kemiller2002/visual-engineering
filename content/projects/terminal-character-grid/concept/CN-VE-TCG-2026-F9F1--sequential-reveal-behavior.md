---
id: CN-VE-TCG-2026-F9F1
title: SequentialReveal Presentation Behavior
project: terminal-character-grid
version: 1.0
status: evaluated
confidence: medium
work_item: GH-19
author_agent: Anthropic Claude
date: 2026-09-27
related_documents:
  - CN-VE-TCG-2026-6CA0--terminal-character-grid-abstraction-boundary.md
  - ../evidence-registry/terminal-character-grid-evidence-v1.md
  - ../hypothesis-registry/terminal-character-grid-hypotheses-v1.md
purposes:
  - reference
  - apply
audiences:
  - practitioner
  - contributor
  - researcher
tags:
  - motion
  - reduced-motion
  - character-grid
  - accessibility
---

# CN-VE-TCG-2026-F9F1: SequentialReveal Presentation Behavior

## Classification

SequentialReveal is a **new Echelon presentation behavior** (category 4 in
CN-VE-TCG-2026-6CA0). It is **not** authentic IBM 3270 behavior: the 3270
transfers and applies whole data streams to its buffer (EV-VE-TCG-2026-CA91,
-DFA5). The "typing" association plausibly comes from slow serial and BBS
links, which remains unverified background. The behavior is defined without
reference to any terminal profile and may be used on any character grid.

## Definition

SequentialReveal is an optional, purely **visual** staging of text that is
**already present** in the document. Cells of a designated region become
visible one after another in row-major order. Nothing about the content, its
semantics, its operability, or its availability to assistive technology
depends on the reveal.

## Rules

### Order

- **R-1 Left to right.** Within a row, cells are revealed in ascending column.
- **R-2 Top to bottom.** A row begins only after the previous row completes.
- **R-3 Row-major.** Together, reveal order equals the grid's row-major order,
  which is also its source, reading, and focus order. Reveal never runs in an
  order that contradicts reading order.

### Timing

- **R-4 Character timing** (`charMs`) is the delay per non-blank cell.
- **R-5 Line timing** (`lineMs`) is an additional delay after each row.
- **R-6 Blank cells** may cost zero (`skipBlank`, default on) so layout
  whitespace does not slow the reveal.
- **R-7 Duration cap** (`maxDurationMs`). If the raw duration exceeds the cap,
  all delays are compressed proportionally so the reveal still completes
  within the cap and order is preserved. The default cap (1500 ms) is a
  design default, not an evidence-derived value; it is well under the
  five-second threshold of WCAG 2.2.2 (EV-VE-TCG-2026-7B66).
- Suggested defaults: `charMs` 12, `lineMs` 40, `maxDurationMs` 1500. These
  are starting points for HY-VE-TCG-2026-A94C, not doctrine.

### Modes

- **R-8 Immediate (static) mode** presents everything at once. It is the
  **default** state of any content that could be revealed; animation is
  opt-in per region.
- **R-9 Reduced motion.** When `prefers-reduced-motion: reduce` matches
  (EV-VE-TCG-2026-93E7), presentation is immediate. No partial or "gentler"
  reveal is substituted. The user's preference cannot be overridden by the
  author or the profile.
- **R-10** Zero timing or a zero cap also resolves to immediate.

### What is never staged

- **R-11 Exclusions.** The following are visible from time zero and are never
  part of a reveal:
  - editable fields and their current values;
  - action affordances (Enter, PF keys, named actions);
  - message, validation, error, warning, and system-status regions;
  - consequential values the user must verify (amounts, balances, dates of
    effect, account or record identifiers, statuses);
  - focus indicators.
- **R-12 Operability from time zero.** Fields and actions are operable
  before, during, and after a reveal. Focus is never delayed or withheld.

### Interruption

- **R-13** Any user input — key press, pointer press, focus change, scroll of
  the containing region, or an explicit "show all" action — **completes** the
  reveal immediately.
- **R-14** Interruption never hides or rewinds content.
- **R-15** The interrupting input is **not consumed**. A key press that
  completes the reveal is still delivered to its target (a typed character
  lands in the focused field; Enter still submits).

### Replay and restart

- **R-16** A reveal may start when new screen content is presented (for
  example, after a screen transition) or when the user explicitly requests a
  replay.
- **R-17** A reveal never loops or repeats automatically, and does not re-run
  when the same content is re-rendered (refresh, re-validation, returning to
  a screen with unchanged content).
- **R-18** Replay restarts from row 1, column 1 with the same deterministic
  schedule. Content that changes during a reveal restarts nothing; the new
  content is presented immediately.

### Semantics and assistive technology

- **R-19** The full text is in the DOM, in source order, from the start. The
  reveal is implemented as a visual mask (clip, width, or opacity) over
  already-rendered text, never by inserting characters over time.
- **R-20** Screen readers therefore read the complete content immediately.
  Revealed regions are **not** live regions; the behavior must not generate
  per-character or per-line announcements.
- **R-21** Content hidden by the reveal mask remains selectable and findable
  (browser find-in-page) where the platform allows; at minimum it must not be
  removed from the accessibility tree (`display: none`, `visibility: hidden`,
  `aria-hidden`, or `inert` must not be used for staging).
- **R-22 Motion is never semantic authority.** No state, meaning, ordering of
  domain events, or availability of an action may be inferred from reveal
  progress. A reveal completing is not an application event.

## Where SequentialReveal is inappropriate

Do not use it:

- for errors, alerts, validation messages, or anything time-critical;
- on screens whose purpose is verification of consequential values
  (payments, postings, confirmations);
- on screens users visit repeatedly in a work session (the cost compounds;
  see HY-VE-TCG-2026-A94C);
- on long or dense content where the cap would compress it into flicker;
- during data refresh, polling, or re-render of unchanged content;
- as a loading indicator or to disguise latency — it would misrepresent
  system state;
- when the user has requested reduced motion (it resolves to immediate).

Plausible uses: a sign-on banner, a screen title, or decorative framing text
on first display of a session.

## Accessibility summary

| Concern | Rule | Evidence |
| --- | --- | --- |
| Vestibular / motion sensitivity | Immediate under reduced motion (R-9) | EV-VE-TCG-2026-93E7, -3BDE |
| Moving content > 5 s | Capped well below 5 s (R-7); interruptible (R-13) | EV-VE-TCG-2026-7B66 |
| Screen readers | Full DOM text immediately, no live announcements (R-19, R-20) | EV-VE-TCG-2026-61DF |
| Keyboard | Operable from time zero; interrupting key delivered (R-12, R-15) | — |
| Reading order | Reveal order = reading order (R-3) | EV-VE-TCG-2026-FEFD |
| Cognitive load | Not used for consequential or repeated content | HY-VE-TCG-2026-A94C (untested) |

## Ownership

| Responsibility | Owner |
| --- | --- |
| This definition and its tests | Visual Engineering |
| Static default, reduced-motion override, row-major visual mask, timing tokens/custom properties, a "complete" presentation state | Forma (presentation only) |
| Deciding when a reveal starts, detecting interruption, marking the region complete, replay requests | Consuming application / Limen |
| Screen transitions and application state | Consuming application / Limen |
| Domain state | Ordo / application domain |

A CSS-only presentation can satisfy R-1 through R-12 and R-19 through R-21
(for example, one stepped clip animation per row, delayed by the preceding
rows' durations, disabled under reduced motion). Interruption (R-13 to R-15)
and replay (R-16 to R-18) require observing user input and therefore belong
to the runtime layer, which completes the reveal by switching the region to
its static state.

## Executable reference model

`scripts/sequential-reveal.mjs` computes, as pure data, the time at which each
cell becomes visible for given content, options, and exclusions.
`scripts/test-sequential-reveal.mjs` checks row-major order, character and
line timing, blank skipping, reduced-motion and static modes, the duration
cap, interruption, exclusions, deterministic replay, and time-independent
semantic text. Run with `npm run reveal:test`. Implementations must be
observably equivalent to this model.

## Open questions

- Does any reveal duration improve perceived quality enough to justify its
  verification cost (HY-VE-TCG-2026-A94C)?
- Is a cursor-following caret during reveal distracting for low-vision users
  using magnification, who may be viewing a different part of the grid?
