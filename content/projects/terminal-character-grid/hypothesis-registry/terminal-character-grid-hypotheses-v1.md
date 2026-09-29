---
id: HYR-VE-TCG-001
title: Terminal / Character-Grid Hypothesis Registry
version: 1.0
project: terminal-character-grid
status: active
work_item: GH-17
purposes:
  - verify
  - apply
audiences:
  - researcher
  - contributor
---

# Terminal / Character-Grid Hypothesis Registry

None of these hypotheses has been tested. Initial confidence is a judgment
about plausibility from the evidence in EVR-VE-TCG-001, not a measured result.

| ID | Hypothesis | Initial confidence | Falsification approach |
| --- | --- | --- | --- |
| HY-VE-TCG-2026-9151 | For keyboard-first data entry on a character grid, focus order equal to row-major field order produces fewer navigation errors than a column-major or author-declared order, for both terminal-experienced and inexperienced users. | Medium (supported for 3270 practice by EV-VE-TCG-2026-3E25; untested for web users) | Counterbalanced task study: same screen, row-major vs column-major order; count mis-entries and backtracks. Falsified if column-major is not worse on a two-column form. |
| HY-VE-TCG-2026-8750 | At 320 CSS px, a contained horizontally scrollable grid preserves task accuracy on position-dependent screens (dense transaction rows) better than a reflowed linear presentation, while reflow is better on label/value screens. | Low-medium | Within-subject task at 320 px and 400 % zoom; accuracy, time, and scroll distance. Falsified if reflow is equal or better on the dense-row screen. |
| HY-VE-TCG-2026-A94C | A character-by-character SequentialReveal increases perceived "terminal character" but increases time-to-first-correct-verification compared with immediate presentation. | Medium for the cost; low for the benefit | A/B with immediate presentation; measure first-verification time and a perception questionnaire. Falsified if reveal has no measurable cost. |
| HY-VE-TCG-2026-2A59 | Explicit visible field boundaries (brackets, underscore runs, or outlines) improve discrimination of editable from protected runs over color alone, including under forced colors and CVD simulation. | Medium-high (consistent with EV-VE-TCG-2026-C4C6 and -E9AC) | Identification task with boundaries vs color-only; forced-colors condition. Falsified if color-only is equivalent in all conditions. |
| HY-VE-TCG-2026-3D56 | A fixed-position status region improves detection of validation errors compared with inline-only messages on dense screens, provided the field is also marked invalid. | Low-medium | Error-detection task with status-only, inline-only, and both. Falsified if status region adds no detection benefit when inline markers are present. |

## Test plans

HY-VE-TCG-2026-9151 and HY-VE-TCG-2026-8750 have a preregistration draft,
[EX-VE-TCG-2026-319C](../experiment-specification/EX-VE-TCG-2026-319C--focus-order-and-narrow-strategy-user-tests.md).
It has not been run; both remain untested. Its pilot machinery (stimuli,
harness, scoring, and power planning) is ready in `experiments/ex-ve-tcg-319c`
(WI-0008). On 2026-09-28 the project owner decided to forgo the pilot
because participants cannot currently be recruited (WI-0009 abandoned).
Both hypotheses remain untested, and the specification can be run later
unchanged.

## Rules

- Hypotheses are not doctrine. The catalog entry may cite them only as open
  questions.
- Null and adverse results stay in this registry.
- A result from a reference-profile demo (e.g. the 3270 profile) is not
  evidence about other profiles unless the relationship tested is generic.
