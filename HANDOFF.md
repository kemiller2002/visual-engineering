# Visual Engineering handoff

## Objective

Bootstrap Visual Engineering as a greenfield Repository Operating System pilot.

## Current state

- ROS 3.1.1 greenfield profile installed on 2026-09-21.
- Project charter is a draft.
- No first vertical slice, evidence record, hypothesis, or experiment has been
  accepted.
- The operating system is under evaluation.

## Validation

Run:

```bash
./ros registry check
./ros validate
```

## Unresolved questions

1. What concrete communication problem and user should the first slice serve?
2. What baseline workflow will be used for comparison?
3. What data, privacy, safety, and accessibility constraints apply?
4. Which outcome would distinguish useful engineering from additional process?

## Next action

Complete `PROJECT-CHARTER.md`, choose the first bounded outcome, and record its
baseline and acceptance criteria in `context/CURRENT-STATE.md`.

## Terminal / Character-Grid family — 2026-09-27

Objective: GitHub #15 (#17, #18, #19, #20). Define a reusable
terminal/character-grid layout family with IBM 3270 as the first reference
profile, not the abstraction.

Completed on `claude/terminal-character-grid-ui-zz0q4q` (PR #21), one ROS work
item and commit per issue (`GH-17` to `GH-20`):

- `content/projects/terminal-character-grid/`: evidence registry
  (EVR-VE-TCG-001), abstraction boundary (CN-VE-TCG-2026-6CA0),
  SequentialReveal behavior (CN-VE-TCG-2026-F9F1), hypotheses
  (HYR-VE-TCG-001), REP, journal, catalog-location decision
  (DF-VE-TCG-2026-2DD5), and reference-workflow report (EX-VE-TCG-2026-5437).
- `content/layouts/`: the new layout catalog with `LAY-TERMINAL-CHARACTER-GRID`
  and the three-screen reference workflow fixture.
- `scripts/`: layout catalog validator, SequentialReveal reference model, and
  character-grid workflow validator, each with tests wired into
  `.github/workflows/validate-research.yml`.

Validation: `npm run layouts:validate|layouts:test|reveal:test|workflows:validate|workflows:test`,
`npm run research:inventory|research:validate|research:build`,
`npm run context:build|context:validate|context:test`, `./ros registry build`,
`./ros validate`.

Unresolved: DOS/BBS/TUI characteristics and 3270 OIA placement are unverified
assumptions (primary sources were blocked in the session); GAP-TCG-09 message
overflow policy needs a family decision; hypotheses are untested; backlog
item to back-fill Forma-verified families into `content/layouts`.

Next action: Forma #37 implementation, then user tests of HY-VE-TCG-2026-9151
and -8750 against rendered reference screens.

## Terminal / Character-Grid follow-up — 2026-09-27 (second session)

Objective: close the open items left by PR #21 and Forma #46.

Completed on `claude/terminal-character-grid-ui-4adh6x`, one ROS work item
and commit each:

- WI-0004: reachable-source evidence (x3270 OIA EV-VE-TCG-2026-1EB5, DOSBox
  text mode -D9CB, 5250 DDS per-row selection -8081, Forma cell rounding
  -C2ED and text-spacing tracks -DE6C); assumptions A-2 (partial), A-3
  (supported as implemented), A-5 (refined); family status `supported` on
  Forma conformance run 36304907987; gap resolutions recorded; new
  GAP-TCG-12.
- WI-0005: DF-VE-TCG-2026-DD05 runtime message overflow policy (GAP-TCG-09).
- WI-0006: EX-VE-TCG-2026-319C preregistration draft for HY-VE-TCG-2026-9151
  and -8750 (not run).

Forma counterpart: kemiller2002/forma pull request #47 (GAP-TCG-09, -10,
-11 and the defects found while fixing them).

Validation: `npm run layouts:validate|layouts:test|reveal:test|workflows:validate|workflows:test`,
`npm run research:validate` (no diagnostics for these records; the
committed `build-reports/*` are stale on main and were not refreshed),
`npm run context:build|context:validate|context:test`, `./ros registry build`,
`./ros validate`.

Unresolved: GAP-TCG-12 needs a decision (keep status on the last
application row as a stylistic choice, or model 24 application rows plus a
separate status line); GAP-TCG-06 (wide glyphs, RTL); BBS/ECMA-48 and curses
evidence (hosts still blocked); IBM primary documentation for the OIA and
5250 CUA list panels; WI-0003 back-fill needs per-family research (the VE
schema requires fields Forma's catalog does not hold), so it was not done
mechanically; family `verified` waits for Forma's promotion after a green
cross-engine run of #47 (blocked by red theme tests on Forma main).

Next action: run the EX-VE-TCG-2026-319C pilot; decide GAP-TCG-12.

## Terminal / Character-Grid follow-up — 2026-09-28 (third session)

Objective: decide GAP-TCG-12, the last open family decision left by the
second session.

Completed on `claude/terminal-character-grid-ui-kg0kec` under ROS work item
WI-0007:

- DF-VE-TCG-2026-1320: `rows × columns` is the application's presentation
  space. A profile may declare device status rows after it, and they hold
  status runs only. The 3270 profile declares one, matching the x3270 OIA
  (EV-VE-TCG-2026-1EB5). DOS declares none (EV-VE-TCG-2026-D9CB). 5250,
  BBS, and TUI are not declared because no source was read.
- Reference workflow: `geometry.statusRows: 1`, and status moved to row 25.
  The validator enforces bounds for status-only rows and counts density over
  application rows only. Two tests were added.
- Catalog entry, concept (A-3 and the characteristic table), and REP were
  updated.

Forma counterpart: the generic `data-ef-status-rows` reservation, on the
same branch name in kemiller2002/forma.

Unresolved: GAP-TCG-06 (wide glyphs, RTL). BBS/ECMA-48 and curses evidence.
IBM primary documentation for the OIA; reading it could change the 3270
profile's declaration but not the family rule. The EX-VE-TCG-2026-319C
pilot needs human participants. Promotion to family `verified` waits on
Forma's cross-engine conformance run.

Next action: run the EX-VE-TCG-2026-319C pilot with participants.
