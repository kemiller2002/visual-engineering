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
