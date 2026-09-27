---
id: DF-VE-TCG-2026-DD05
title: Runtime Message Overflow Policy for Character-Grid Message Regions
project: terminal-character-grid
date: 2026-09-27
status: accepted
work_item: WI-0005
author_agent: Anthropic Claude
related_documents:
  - ../experiment-report/EX-VE-TCG-2026-5437--reference-workflow-validation.md
  - ../evidence-registry/terminal-character-grid-evidence-v1.md
  - ../concept/CN-VE-TCG-2026-6CA0--terminal-character-grid-abstraction-boundary.md
purposes:
  - decide
  - reference
audiences:
  - contributor
  - practitioner
---

# DF-VE-TCG-2026-DD05: Runtime Message Overflow Policy

## Context

EX-VE-TCG-2026-5437 finding F-3 (GAP-TCG-09): messages are produced by the
application at runtime, so static validation cannot guarantee that a message
fits its region, which in the reference workflow is one 78-cell row. The
family needed one declared policy instead of per-screen patches. The report
named two candidates: wrap into a reserved second row, or truncate the
visible line and expose the full text to assistive technology and on request.

Relevant evidence:

- EV-VE-TCG-2026-E852 (SC 1.4.4, 1.4.12): text must survive resizing and
  spacing overrides "without loss of content or functionality".
- EV-VE-TCG-2026-61DF (SC 4.1.3): status messages are programmatically
  determinable without focus; this covers screen-reader users only.
- EV-VE-TCG-2026-7B66 (SC 2.2.2): automatically moving text (a marquee)
  needs pause/stop/hide.
- EV-VE-TCG-2026-DE6C: a character grid can grow a row track to its content
  without breaking shared-column alignment. A browser probe on Forma pull
  request #47 (commit `14979f3`) placed a 300-character message in the
  one-row Transaction History message run: row 21 grew from 24px to 120px,
  the text ended 3px above row 22, and nothing was clipped.
- CN-VE-TCG-2026-6CA0 names validation messages as consequential
  information that users verify before acting.

## Decision

1. **A message is never truncated, clipped, or scrolled.** Sighted users get
   the same complete text that assistive technology gets.
2. **A message region declares a reserved height** of one or more rows
   (Forma: `data-ef-height`). Screens whose messages vary in length reserve
   two rows. Text wraps within the region's width.
3. **When a message exceeds its reserved rows, the region grows** and the
   rows below it move down. It never paints over the rows below. The
   resulting layout shift is accepted as the cost of complete content; it
   occurs only for messages the application failed to size.
4. **Message length is application content.** Applications should author
   messages to fit the reserved region (a message identifier plus one
   sentence). Longer explanations belong in help (for example PF1), not in
   the message region. This is guidance, not something the presentation
   layer enforces.
5. The message remains a live region (`role="status"` or `alert`) with a
   visible severity word, as before.

## Alternatives considered

- **Truncate with an ellipsis and expose the full text to assistive
  technology and on request.** Rejected: sighted users lose consequential
  text unless they know to ask for it, which contradicts decision 1 and
  SC 1.4.4/1.4.12's "without loss of content".
- **Clip to the region (the fixed-grid default).** Rejected: silent loss.
- **Horizontal marquee.** Rejected: motion that needs pause/stop/hide
  (SC 2.2.2) and slows verification.
- **Scrollable message region.** Rejected: hides text behind an interaction
  and adds a focus stop inside the grid.

## Consequences

- GAP-TCG-09 is closed as a family rule. The implementation obligation is
  that an over-long message grows its rows without overlap; Forma verifies
  this in its own tests.
- The reference workflow keeps one-row message regions because its messages
  are authored to fit; the rule applies when they do not.
- Row pitch is uniform only while content fits. Tests that assert uniform
  rows must use content that fits, and a separate test must cover growth.
