---
id: EX-VE-TCG-2026-319C
title: Character-Grid Focus Order and Narrow-Screen Strategy User Tests
project: terminal-character-grid
status: preregistration-draft
priority: high
work_item: WI-0006
machinery_work_item: WI-0008
author_agent: Anthropic Claude
date: 2026-09-27
hypotheses:
  - HY-VE-TCG-2026-9151
  - HY-VE-TCG-2026-8750
related_documents:
  - ../hypothesis-registry/terminal-character-grid-hypotheses-v1.md
  - ../experiment-report/EX-VE-TCG-2026-5437--reference-workflow-validation.md
  - ../concept/CN-VE-TCG-2026-6CA0--terminal-character-grid-abstraction-boundary.md
purposes:
  - verify
audiences:
  - researcher
---

# EX-VE-TCG-2026-319C: Focus Order and Narrow-Screen Strategy User Tests

**Status: preregistration draft with executable pilot machinery
(`experiments/ex-ve-tcg-319c`, WI-0008, 2026-09-28). No participant has
been recruited and no data exist.** This document specifies the studies so that they can be run
and judged against criteria fixed in advance. It must not be cited as
evidence for either hypothesis.

## Questions

1. **HY-VE-TCG-2026-9151.** For keyboard-first data entry on a character
   grid, does focus order equal to row-major field order produce fewer
   navigation errors than column-major order, for both terminal-experienced
   and inexperienced users?
2. **HY-VE-TCG-2026-8750.** At 320 CSS px, does a contained, horizontally
   scrollable grid preserve task accuracy on a position-dependent screen
   (dense transaction rows) better than reflow, while reflow is better on a
   label/value screen?

## Stimuli

Both studies use the rendered reference screens from Forma's public
contracts (`patterns/character-grid-workflow.html`: Customer Inquiry,
Account Detail, Transaction History) with synthetic data only. The harness
that presents tasks and logs events is test code outside Forma; Forma
markup is not modified except for the declared conditions below.

### Study 1 conditions (HY-VE-TCG-2026-9151)

A two-column entry screen derived from Customer Inquiry: six fields in two
columns of three, labels to the left of each field.

| Condition | Focus order |
| --- | --- |
| R (row-major) | Row by row: left field, then right field, then next row. This is the family rule (CG-6). |
| C (column-major) | Down the left column, then down the right column. |

Condition C violates CG-6 and exists only as an experimental stimulus. It
is built by reordering source, not with positive `tabindex`, so both
conditions differ only in order. Visual placement is identical.

### Study 2 conditions (HY-VE-TCG-2026-8750)

| Screen | Contained (`data-ef-narrow="contained"`) | Reflow (`data-ef-narrow="reflow"`) |
| --- | --- | --- |
| Transaction History (position-dependent) | grid preserved, viewport scrolls horizontally | runs linearized; the table keeps its own contained scroll |
| Account Detail (label/value) | grid preserved | runs linearized in source order |

Viewport 320 CSS px wide; a second block at 1280 CSS px with 400 % zoom
(the SC 1.4.10 equivalent) if the pilot shows the conditions differ.

## Participants

Three strata, recruited separately:

- **T:** at least one year of regular use of a 3270, 5250, or text-mode
  business application.
- **N:** no regular terminal use.
- **S:** screen-reader users (Study 2 only, and Study 1 if recruited),
  using their own assistive technology.

Sample size is not fixed here because no variance estimate exists. A pilot
of six participants per stratum estimates variance; the confirmatory sample
per stratum is then set by a power analysis for the primary measure
(power 0.8, two-sided α 0.05) and recorded in this document **before**
confirmatory data collection. Pilot data are not pooled with confirmatory
data.

## Procedure

Within-subject. Condition order is counterbalanced (AB/BA for Study 1; a
2 × 2 Latin square over screen × strategy for Study 2), and data sets are
matched but not identical across conditions to limit memory effects.

- **Study 1 task:** enter a given record into the six fields and submit
  with Enter. Twelve trials per condition.
- **Study 2 tasks:** Transaction History: "find the amount and status of
  the transaction on DATE with DESCRIPTION"; Account Detail: "report the
  available balance and the overdraft limit". Eight trials per screen and
  strategy.

## Measures

| Study | Primary | Secondary |
| --- | --- | --- |
| 1 | Navigation errors per trial: a value entered in a field other than the one the task instruction was addressing, plus corrective Shift+Tab presses | Completion time; backtracks; subjective workload (NASA-TLX raw) |
| 2 | Task accuracy (correct answer) | Time to answer; horizontal scroll distance; focus stops; subjective preference |

Events are logged by the harness (keydown, focus, input, scroll), with
timestamps, as raw JSON per trial. No keystroke content beyond the
synthetic task data is stored.

## Analysis plan

Mixed-effects models with participant and item as random intercepts:
Poisson (or negative binomial if overdispersed) for error counts, logistic
for accuracy, and log-time for durations. Fixed effects: condition,
stratum, and their interaction. Effects are reported with 95 % confidence
intervals, per stratum and pooled.

## Falsification criteria (fixed in advance)

- **HY-VE-TCG-2026-9151 is falsified** if, in the confirmatory sample,
  column-major order does not produce more navigation errors than row-major
  order (the interval for the C − R difference includes zero or favors C)
  in either stratum T or stratum N.
- **HY-VE-TCG-2026-8750 is falsified** if reflow's accuracy on Transaction
  History is equal to or better than contained (interval for contained −
  reflow includes zero or favors reflow), or if reflow is not better than
  contained on Account Detail.
- A null or adverse result is recorded in the hypothesis registry and in an
  experiment report. The family rules are not silently changed.

## Consequences of outcomes

- 9151 supported: CG-6 stays a rule with user evidence behind A-1.
  Falsified: the family records that focus order must follow the screen's
  reading structure, which may be column-major, and Forma's CG-6 becomes a
  default rather than a rule.
- 8750 supported: the per-screen guidance "contained for
  position-dependent screens, reflow for label/value screens" becomes
  evidence-backed. Falsified: the guidance is withdrawn and both strategies
  are re-examined, especially for stratum S.

## Operational definitions (amendment, 2026-09-28, before any data)

These definitions make the measures above computable. They were fixed with
the pilot machinery (`experiments/ex-ve-tcg-319c/src/scoring.mjs`) before any
participant was recruited, and they change only by a dated amendment made
before the data they would apply to exist.

- **Visit.** One uninterrupted stay of focus on one element, together with
  the input events made during it.
- **Misentry (Study 1).** During a visit to field F the value changed and
  ended as text of at least two characters that is not a prefix of F's
  target, but begins with the first two characters of another field's
  target. A typo inside the right field is not a navigation error. Item
  generation guarantees that no two targets in a record share their first
  two characters.
- **Corrective Shift+Tab.** Every Shift+Tab keydown during a Study 1
  trial. The task never requires backward navigation.
- **Navigation errors (primary, Study 1).** Misentries plus corrective
  Shift+Tab presses.
- **Backtrack.** Any focus move to a field earlier in the condition's focus
  order than the previously focused field, by any means.
- **Completion time.** From the first keydown to the submit event.
- **Correct (primary, Study 2).** Every requested part matches the item's
  answer after removing spaces and thousands separators and ignoring case.
  A trial without an answer is incorrect.
- **Time to answer.** From stimulus display to answer submission.
- **Scroll distance.** The sum of absolute horizontal (and, separately,
  vertical) position changes over every scroll container and the window.
- **Focus stops.** Focus events on elements inside the stimulus.

Procedure details the draft left open, fixed the same way:

- The first field has focus when a Study 1 trial starts, as on a terminal.
  It is first in both orders.
- The Study 1 record card mirrors the screen's 2 × 3 layout, so it favors
  neither order.
- Study 1 precedes Study 2 for every participant.
- There are no practice trials. Whether the pilot shows a need for them is
  a pilot question, and adding them is an amendment.
- The Study 2 screen × strategy counterbalancing uses a 4 × 4 Williams
  design. It balances position and first-order carryover and satisfies the
  draft's "2 × 2 Latin square over screen × strategy".
- Pilot size stays at six per stratum. Six is not a multiple of four, so
  Study 2 sequences are used 2, 2, 1, 1 times. Confirmatory sizes are
  rounded up to a multiple of the sequence count (2 for Study 1, 4 for
  Study 2).
- Study 2 does not begin until the harness measures the viewport at
  320 ± 2 CSS px.
- Stimuli are frozen: Forma commit `30aa31c` (CSS, reference workflow
  markup, and conformance checker) with SHA-256 hashes in
  `experiments/ex-ve-tcg-319c/protocol/forma-snapshot/manifest.json`.
  Condition C is the only stimulus that breaks a Forma rule (CG-6), and
  that break is the manipulation.

**Open before confirmatory collection:** the smallest effect of interest
for each contrast. Until it is set, the power module reports sample sizes
over a grid of candidate effects (`protocol/pilot-config.json`).

## Pilot readiness (2026-09-28)

Ready (WI-0008): stimuli, schedules, item pools, session harness, event
logging, scoring, pilot analysis, and power planning. There are 18 unit
tests and one browser mechanics check. The mechanics check is a scripted
agent, not a participant, and its output can never bear on either
hypothesis. It confirms that:

- a row-major typist makes no navigation errors under R and exactly four
  misentries per trial under C;
- Study 2 answers are scored correctly;
- horizontal scrolling is measured where the grid needs it.

Not ready, and outside what a repository can supply (tracked as WI-0009,
blocked):

- recruitment of strata T, N, and S;
- informed consent;
- the approval route the running organization requires;
- a researcher to run the sessions;
- the smallest-effect-of-interest decision.

## Ethics and data

Informed consent; participants may stop at any time; synthetic data only; no
recording of screens or voices unless separately consented; results stored
without direct identifiers. The study needs the approval route the running
organization requires; this draft does not constitute approval.

## Threats to validity

- Learning effects across conditions (mitigated by counterbalancing and
  matched data sets).
- The reference screens are a single domain (banking).
- Stratum T participants' expectations may come from one platform (3270 or
  5250) and not generalize.
- Browser, font, and assistive-technology versions vary; record them per
  session.

## Required output

An experiment report (`EX-VE-TCG-…` report) with the pilot variance
estimate and power analysis, the confirmatory results per stratum, raw
event logs, and an update of HY-VE-TCG-2026-9151 and -8750 in
HYR-VE-TCG-001 (supported, falsified, or inconclusive).
