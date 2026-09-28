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

## EX-VE-TCG-2026-319C pilot machinery — 2026-09-28 (third session, continued)

Objective: carry out the recommended next experiment, the pilot of
EX-VE-TCG-2026-319C. Running it needs people, so this session built
everything up to the first participant and formally blocked the rest.

Completed under WI-0008, on `claude/terminal-character-grid-ui-kg0kec`
restarted from main after #23 merged:

- `experiments/ex-ve-tcg-319c/` contains:
  - config and frozen Forma stimuli (`30aa31c`, hash-verified);
  - Williams and AB/BA schedules;
  - fictitious item pools;
  - stimulus generation (Study 1 R/C differ only in source order; Study 2
    substitutes data into Forma's own screens);
  - session plans and the browser harness with a 320 px viewport gate;
  - scoring, pilot analysis, and simulated paired-t power planning.
- 18 unit tests and a browser mechanics check (scripted agent, 56 trials),
  both in CI. The t quantiles match tables. Sample sizes match standard
  paired-t values (d = 0.5 gives n = 34; d = 0.8 gives n = 15).
- The specification gained a dated operational-definitions amendment
  (before data) and a pilot-readiness section. The experiments registry,
  hypothesis registry, and REP were updated.
- `playwright-core` 1.63.0 was added as a dev dependency (no bundled
  browsers).

Blocked: WI-0009 (run the pilot). It needs strata T/N/S participants,
consent, an approval route, a session researcher, and the smallest effect
of interest. The resume commands are in its ROS block reason and in the
experiment README.

Next action: a researcher decides the smallest effects of interest,
obtains approval, and runs six participants per stratum with
`npm run tcg319c:session`. Then run `npm run tcg319c:analyze`.

## EX-VE-TCG-2026-319C pilot forgone — 2026-09-28

Decision (project owner): participants cannot currently be recruited, so
the pilot is forgone. WI-0009 is abandoned, and the decision is recorded
under WI-0010 in the specification (status `not-run`, section "Decision
not to run"), the hypothesis registry, the REP, and the experiment
README.

HY-VE-TCG-2026-9151 and -8750 remain untested. CG-6 remains a rule on its
existing evidence (EV-VE-TCG-2026-3E25 and WCAG focus order). The
contained-versus-reflow guidance stays hypothesis-grade. The machinery in
`experiments/ex-ve-tcg-319c` stays runnable, and CI keeps testing it.

Next action: none for this experiment until participants are available.
When they are, capture a new work item and follow the specification's
"Decision not to run" section.
