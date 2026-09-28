---
id: DF-VE-TCG-2026-1320
title: Device Status Line Placement Outside the Application Rows
project: terminal-character-grid
date: 2026-09-28
status: accepted
work_item: WI-0007
author_agent: Anthropic Claude
related_documents:
  - ../evidence-registry/terminal-character-grid-evidence-v1.md
  - ../concept/CN-VE-TCG-2026-6CA0--terminal-character-grid-abstraction-boundary.md
  - ../experiment-report/EX-VE-TCG-2026-5437--reference-workflow-validation.md
  - DF-VE-TCG-2026-DD05--runtime-message-overflow-policy.md
purposes:
  - decide
  - reference
audiences:
  - contributor
  - practitioner
---

# DF-VE-TCG-2026-1320: Device Status Line Placement

## Context

GAP-TCG-12 (found by WI-0004): EV-VE-TCG-2026-1EB5 shows that x3270 draws
the Operator Information Area (OIA) on a terminal row *beyond* the emulated
model's rows and never inside them. It omits the OIA rather than overwrite
an application row when no spare row exists. A Model 2 screen is therefore
24 application rows plus a separate status line.

The reference workflow (RW-TCG-CUSTOMER-ACCOUNT-INQUIRY) and Forma's 3270
reference screens instead put system status on application row 24 of a
24 × 80 grid. That leaves the application 23 rows and presents a device
indicator as if the application had drawn it.

The two options recorded with the gap were:

1. keep status on the last application row and call it a stylistic choice;
2. model 24 application rows plus a separate status line.

Relevant evidence:

- EV-VE-TCG-2026-1EB5 (x3270): the OIA lies outside the presentation space,
  optionally below a ruled line, and carries keyboard-lock/system indicators
  and the cursor position. IBM display-station documentation is unread, so
  this is *supported as implemented* by a maintained emulator. It is not
  device-verified.
- EV-VE-TCG-2026-D9CB (DOSBox): PC text mode has no device status line.
  Applications draw status on their own rows.
- 5250, BBS/ECMA-48, and curses/TUI: no reachable source about a device
  status line was read (A-2 remains partial).

## Decision

1. **`rows × columns` is the application's presentation space.** The family's
   geometry counts only rows the application can write.
2. **A profile may declare device status rows** (`statusRows`, a small
   integer, default 0). They follow the last application row, outside
   the presentation space. Status is the only run kind they may hold:
   device or system state such as input inhibited, waiting, insert mode,
   or the cursor position. Messages, actions, fields, and application text
   may not occupy them.
3. **Row-major order still governs.** Status rows are numbered after the
   application rows (row 25 on a 24-row screen), so source, reading, and
   focus order are unchanged. The status line never takes focus.
4. **Application-drawn status stays legal.** A screen with `statusRows` 0 may
   still place a status run on an application reserved row. This covers the
   DOS/TUI convention (EV-VE-TCG-2026-D9CB). The choice belongs to the
   profile, not to an individual screen.
5. **Profile declarations, bounded by evidence:**

   | Profile | `statusRows` | Basis |
   |---|---|---|
   | IBM 3270 (Model 2, 24 × 80; Model 3, 32 × 80) | 1 | EV-VE-TCG-2026-1EB5, supported as implemented |
   | DOS text mode | 0 | EV-VE-TCG-2026-D9CB |
   | IBM 5250, BBS/ANSI, curses/TUI | not declared | No source read; authors choose, and the choice is unverified |

6. **A visual separator between the application rows and the status rows is
   optional and stylistic.** x3270 draws it only on request. It must not be
   the only cue: the status run is still a `status` live region.
7. **Density and capacity** are measured over the application rows only.

## Alternatives considered

- **Keep status on application row 24 and call it stylistic.** Rejected for
  the 3270 profile. The evidence places the status line outside the
  application's rows, and the reference profile's purpose is to show the
  family's relationships faithfully where evidence exists. Calling it style
  would hide a known inaccuracy and cost the application a row. The option
  remains available to profiles without a device status line (decision 4).
- **A status region outside the grid, inside the same viewport.** Rejected.
  A separate element does not share the grid's column tracks, so when
  text-spacing overrides widen the columns (GAP-TCG-11, EV-VE-TCG-2026-DE6C)
  the status line's columns would stop aligning with the rows above. Extra
  rows in the same grid keep one set of tracks.
- **Make `rows` include the status line (a 25-row grid).** Rejected. This
  confuses presentation-space size with display size. Every 24 × 80
  application would have to declare 25, and bounds checks could no longer
  keep application runs out of the status row.

## Consequences

- GAP-TCG-12 is closed as a family rule. Forma must add a generic way to
  reserve status rows after the application rows. The 3270 reference
  profile uses it. No 3270-specific logic may enter CharacterGrid.
- The reference workflow declares `statusRows: 1` and moves `sys` to row 25.
  Row 24 becomes free for application content. The screens do not use it,
  so their content is unchanged.
- The workflow validator enforces decision 2: only status runs may occupy
  status rows. It also enforces decision 7: density is counted over
  application rows.
- Assumption A-3 is unchanged in strength. It is supported as implemented
  and not device-verified. Reading IBM 3278/3279 or 3174 documentation
  could still change the 3270 profile's `statusRows`, but not the family
  rule.
