# EX-VE-TCG-2026-319C — Focus order and narrow-screen strategy

Executable pilot machinery for the preregistered user tests of
HY-VE-TCG-2026-9151 (row-major versus column-major focus order) and
HY-VE-TCG-2026-8750 (contained versus reflow at 320 CSS px). The
specification, including its operational definitions, is
`content/projects/terminal-character-grid/experiment-specification/EX-VE-TCG-2026-319C--focus-order-and-narrow-strategy-user-tests.md`.

**This directory contains no participant data.** Nothing produced here by
tests or by the scripted mechanics agent may be cited for either
hypothesis.

## What exists

| Part | File | Notes |
| --- | --- | --- |
| Configuration | `protocol/pilot-config.json` | Strata, conditions, trial counts, seed, power settings. |
| Frozen stimuli | `protocol/forma-snapshot/` | Forma `30aa31c`: CSS, the reference workflow markup, and the CharacterGrid conformance checker, with SHA-256 hashes in `manifest.json`. Sessions refuse to build if a hash differs. |
| Schedules | `src/design.mjs` | AB/BA for Study 1. A 4 × 4 Williams design over screen × strategy for Study 2. |
| Item pools | `src/datasets.mjs` | Fictitious records and accounts, fixed by seed. Item sets are crossed with conditions across participants. |
| Stimuli | `src/stimuli.mjs` | Study 1 is built from Forma's public contracts; R and C differ only in source order. Study 2 is Forma's own Account Detail and Transaction History, with only the data and `data-ef-narrow` substituted. |
| Session plan | `src/plan.mjs` | Every trial of one participant: stimulus, instruction, expected answer, and focus order. |
| Harness | `harness/` | Browser session runner outside Forma. Logs keydown, focus, input, scroll, submit, and answer events. Gates Study 2 on a 320 ± 2 px viewport. Downloads one JSON file of raw trial records. |
| Scoring | `src/scoring.mjs` | The preregistered measures as pure functions of the event log. |
| Pilot analysis | `src/analyze.mjs`, `src/power.mjs` | Tidy trial CSV, per-stratum paired differences, and a simulated paired-t sample-size table over candidate effects. |

Everything under `src/` is pure (inputs to values), apart from the two CLI
entry points' file I/O. Seed, stratum, and participant index regenerate
the exact plan a participant saw.

## Run a session

```bash
npm run tcg319c:session -- --stratum N --index 0     # writes experiments/ex-ve-tcg-319c/dist/N-000/
npx http-server experiments/ex-ve-tcg-319c/dist/N-000  # or any static server
```

Open `index.html`. The researcher records the assistive technology (no
identifiers), and the participant completes Study 1. The researcher then
resizes the viewport to 320 CSS px (the gate shows the measured width) and
the participant completes Study 2. At the end, download the trial records.
Participant indices count from 0 within each stratum and are not
identifiers.

## Analyze the pilot

```bash
npm run tcg319c:analyze -- --out experiments/ex-ve-tcg-319c/dist/analysis path/to/*-trials.json
```

This writes `trials.csv`, one row per trial for the preregistered
mixed-effects models, and `pilot-report.json`. The report holds per-stratum
paired differences (oriented so each hypothesis predicts a positive value),
their SD, and the confirmatory sample size for each candidate effect,
rounded up to a multiple of the counterbalancing cycle.

The sample-size table is a planning approximation: a two-sided paired
t test on per-participant differences, simulated with a fixed seed. The
confirmatory analysis remains the preregistered mixed-effects models, which
are not implemented here; no R or statistics runtime is in this repository
yet. Before confirmatory collection, the researcher records the smallest
effect of interest in the specification.

## Checks

```bash
npm run tcg319c:test                                   # 18 unit tests
CHROMIUM_PATH=/path/to/chrome npm run tcg319c:mechanics   # full session in a real browser
```

The mechanics check drives one complete session (56 trials) with a
deterministic scripted agent. It asserts that:

- typing in row-major order yields no navigation errors under R and a
  misentry under C wherever the orders disagree;
- DOM-read answers score correct;
- contained Transaction History at 320 px requires horizontal scrolling;
- Study 2 ran at 320 CSS px.

CI runs both commands (`.github/workflows/validate-research.yml`).

## Not supplied here

Recruitment, informed consent, the running organization's approval, the
session researcher, and the smallest effect of interest. WI-0009 is
blocked on these; its reason records the commands that resume it.
