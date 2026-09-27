---
id: JR-VE-TCG-2026-2E0E
title: Terminal / Character-Grid Evidence and Abstraction Boundary
project: terminal-character-grid
date: 2026-09-27
author_agent: Anthropic Claude
status: complete
work_item: GH-17
---

# JR-VE-TCG-2026-2E0E: Evidence and Abstraction Boundary

## Starting question

GitHub issue #17: what is invariant across terminal/character-grid interfaces,
with IBM 3270 as the first reference profile but not the abstraction?

## Repository observations

- Visual Engineering had no terminal, character-grid, or 3270 records.
- Visual Engineering had no structured layout catalog of its own. Forma's
  `catalog/layouts.json` cites "Visual Engineering Composition Science" as its
  source and holds the `LAY-*` families. GH-18 must therefore decide where a
  Visual Engineering layout catalog entry lives.
- `agent-context/UI-FOUNDATIONS.md` already requires keyboard, reading, focus,
  and visual order to agree; the terminal family inherits that rule.

## Source access log

| Attempt | Result |
| --- | --- |
| `ibm.com` CICS 3270 field attributes page | Blocked by session egress policy |
| `x3270.miraheze.org` protocol wiki | Blocked |
| `bitsavers.org` GA23-0059 PDF | Blocked |
| `w3.org` WCAG 2.2 and Media Queries 5 | Blocked |
| `github.com/Open3270/Open3270` IBM GA23-0059-07 scans | Retrieved (chapters 1–4, 5, 6; chapters 7–9 and 10+ not downloaded) |
| `github.com/pmattes/x3270` source | Retrieved |
| `github.com/tn5250/tn5250` source | Retrieved |
| `github.com/w3c/wcag` | Retrieved |
| `github.com/w3c/csswg-drafts` `mediaqueries-5/Overview.bs` | Retrieved |

The IBM scans contain no text layer. Pages were rendered and read visually;
Chapter 2 and 3 passages were located with OCR (tesseract) and then read.
Quotations in EVR-VE-TCG-001 were checked against the page images for
Chapters 1 and 3; the Chapter 2 screen-size quotation is from OCR output and
is marked as such in the registry entry.

## What changed my view

1. **Reset is not an AID.** I began assuming Enter, Clear, Reset, and PF keys
   were one family of "submit" keys. Table 3-4 lists no Reset AID, and x3270's
   `Reset_action` is local. The family therefore distinguishes *submitting*
   actions (Enter, PF, PA, Clear) from *local* actions (Reset), and Clear is
   both (local erase + transmission).
2. **Traversal order is derived, not declared.** x3270's `next_unprotected`
   walks buffer addresses. That makes "source order = row-major order" the
   faithful web translation, and argues against `tabindex`.
3. **SequentialReveal has no 3270 basis in the sources opened.** The 3270
   transfers and applies whole data streams. The typing effect is plausibly a
   slow-serial/BBS association, which remains unverified.
4. **Field wrap and wraparound Tab are authentic but excluded** from the
   generic abstraction because they conflict with native inputs and with Tab
   leaving the region.

## Unverified and deferred

- 3270 OIA placement, DOS text mode, BBS/ECMA-48, and curses/TUI
  characteristics (EVR-VE-TCG-001, "Unverified background").
- Chapters 7–9 of GA23-0059-07 (keyboard functions) were not read.

## Outputs

CN-VE-TCG-2026-6CA0, EVR-VE-TCG-001, HYR-VE-TCG-001, RP-VE-TCG-2026-DD75.
