---
id: RP-VE-PE-001
title: Perceptual Envelope and Adaptive Visual Engineering
research_area:
  - accessibility
  - typography
  - color
  - human-factors
discipline: Visual Engineering
author_agent: OpenAI
version: 1.0
confidence: medium-high
completion: program-design-complete
priority: critical
related_projects:
  - project-atlas
  - itten-color-contrasts
  - evaluation-measurement
related_documents:
  - project-atlas-typography-autonomous-research-report-v1.md
  - rep-ve-col-001-color-contrast-low-vision-and-color-vision-deficiency.md
supersedes: []
superseded_by: []
tags:
  - perceptual-envelope
  - disability
  - dyslexia
  - low-vision
  - color-vision-deficiency
  - personalization
  - semantic-redundancy
keywords:
  - visual crowding
  - individualized typography
  - CVD
  - variable fonts
  - accessibility modes
  - semantic channel survivability
purposes:
  - integrate
  - verify
  - execute
audiences:
  - practitioner
  - researcher
  - contributor
  - executive
---

# RP-VE-PE-001: Perceptual Envelope and Adaptive Visual Engineering

## Research State Snapshot

- **Theory version:** Visual Engineering pre-Perceptual-Envelope synthesis, September 2026.
- **Knowledge base version:** repository main as of 2026-09-22 plus sources listed in this package.
- **Highest confidence areas:** fixed dyslexia-specific fonts are not a generally effective intervention; color alone is not a safe semantic channel; typography and contrast effects depend on observer, task, and context; user variation is material.
- **Lowest confidence areas:** how stable an individual visual profile is across days, devices, fatigue, and task classes; how well simulated CVD predicts affected-user performance; whether a compact calibration can recover a useful profile.
- **Largest remaining unknown:** whether mechanism-aware visual calibration can outperform universal defaults and named accessibility modes while preserving semantic consistency.
- **Active research streams:** Project Atlas typography, color and low-vision research, Itten color experiments, evaluation and measurement.
- **Recently invalidated ideas:** universal dyslexia fonts, universal CVD-safe palettes, one contrast ratio as a usability threshold, monotonic "more spacing is better," universal light or dark mode.
- **Priority changes:** move from disability-label presets toward measured perceptual constraints and task-specific representation.

## Executive Summary

Visual Engineering should treat accessibility as a constrained visual communication problem, not as a collection of named display modes.

The proposed **Perceptual Envelope** is the region of visual configurations in which a defined observer can complete a defined task accurately, efficiently, and sustainably under a defined environment. The envelope may vary by task. Prose, identifiers, charts, warnings, and navigation do not have the same error-correction mechanisms or consequences.

The proposed **Semantic Channel Survivability** property measures whether meaning survives the loss or degradation of one or more visual channels such as hue, luminance, iconography, border, position, text, motion, or pattern.

The research program explicitly rejects two shortcuts:

1. diagnosis implies one visual preset;
2. standards conformance implies optimal human performance.

Standards remain mandatory conformance floors. A Perceptual Envelope is an additional performance model.

## Original Objective

Find new experiments around typography and color theory, investigate disability-specific needs including color-vision deficiency and dyslexia, and identify approaches that could materially differentiate Visual Engineering.

## Scope

Included:

- digital typography;
- color, contrast, semantic encoding, and categorical palettes;
- dyslexia and visual crowding;
- low vision and contrast sensitivity;
- congenital and acquired color-vision deficiency;
- user customization;
- task-specific rendering;
- sustained effort and compensation cost;
- automated screening plus affected-user validation.

Excluded from claims of completion:

- clinical diagnosis;
- medical treatment;
- claims that a simulation reproduces lived perception;
- claims that one personalized setting remains optimal indefinitely;
- claims that any single metric replaces WCAG conformance.

## Repository Context

Project Atlas already contains a layered typography model, conditional optima, a task-weighted error model, a compensation-cost model, and evidence that specialist dyslexia fonts do not consistently outperform conventional fonts.

REP-VE-COL-001 already concludes that WCAG ratios are conformance floors rather than universal human thresholds, that color-vision deficiency is primarily a discriminability and encoding problem, and that simulated CVD is a screening method rather than proof.

This program connects those streams and converts them into falsifiable experiments.

## Current Understanding

### Working model

```text
Visual performance =
f(
  observer,
  task,
  content,
  consequence of error,
  typography,
  color,
  layout,
  device,
  environment,
  time
)
```

A disability label can be relevant to recruitment and subgroup analysis, but it is not assumed to identify the active bottleneck.

Candidate bottlenecks include:

- acuity;
- contrast sensitivity;
- visual crowding;
- glyph confusion;
- letter-position uncertainty;
- glare or photophobia;
- color discrimination;
- visual-field loss;
- tracking and fixation cost;
- lexical or language processing;
- working memory;
- navigation and hierarchy.

## Key Discoveries

### KD-1: Dyslexia-specific fonts are not a general solution

EV-VE-PE-001 reports a 2026 meta-analysis of 15 studies, 91 effect sizes, and 688 dyslexic readers. The overall effect of dyslexia-oriented fonts on reading speed or accuracy was negligible.

This does not imply that typography is irrelevant. It rejects a diagnosis-to-font shortcut.

### KD-2: Individual typography effects can be large

EV-VE-PE-002 reports substantial within-reader variation across fonts without a comprehension penalty. EV-VE-PE-006 reports that prediction from reader and font characteristics can improve selection relative to fixed defaults.

### KD-3: Mechanism can predict intervention benefit

EV-VE-PE-003 found that a subgroup of readers with dyslexia and elevated visual crowding benefited from expanded letter, word, and line spacing. This supports testing the mechanism directly instead of assuming the diagnosis identifies the treatment.

### KD-4: Current standards are intentionally incomplete as a performance model

EV-VE-PE-005 is the 10 September 2026 WCAG 3 Working Draft. Its text-contrast algorithm is still to be determined, it expects size and weight to matter, and it notes that a separate red/green CVD requirement may be needed if the final contrast method does not account for it.

Visual Engineering should continue using WCAG 2.2 for conformance while conducting additional performance research.

### KD-5: The same logic applies to color

REP-VE-COL-001 already rejects a universal CVD palette, a universal dark mode, and pairwise color checks as sufficient accessibility evidence. Meaning should survive the failure of any single nonessential visual channel.

## Evidence Registry

| ID | Evidence | Use | Limitation |
| --- | --- | --- | --- |
| EV-VE-PE-001 | Azzarello et al., 2026, *Does font improve reading in dyslexic children?* Annals of Dyslexia. PMID 42536336 | Reject universal dyslexia-font benefit | Child/student populations and included-study heterogeneity |
| EV-VE-PE-002 | Wallace et al., 2022, *Towards Individuated Reading Experiences* | Individual font effects and preference/performance separation | Font choice rather than continuous variable-font axes |
| EV-VE-PE-003 | *Optimizing text for an individual's visual system: The contribution of visual crowding to reading difficulties*, Cortex, 2018 | Mechanism-specific spacing response | Does not establish crowding as the cause of all dyslexia |
| EV-VE-PE-004 | REP-VE-COL-001 in this repository | Low vision, CVD, contrast, polarity, simulation limits | Research draft; affected-user experiments remain open |
| EV-VE-PE-005 | W3C WCAG 3.0 Working Draft, 2026-09-10 | Current standards direction and uncertainty | Working Draft, not a normative replacement for WCAG 2.2 |
| EV-VE-PE-006 | Cai et al., 2022, *Personalized Font Recommendations* | Feasibility of predicting individualized typography | Ranking among a limited font set |
| EV-VE-PE-007 | Project Atlas Typography Autonomous Research Report v1 | Conditional optima, compensation cost, active bottlenecks | Unified predictive model not yet validated |

## Hypothesis Registry

This REP creates HY-VE-PE-001 through HY-VE-PE-008. The canonical list is in the project hypothesis registry.

## Failed Assumptions

- "Dyslexia-friendly" font families are generally superior.
- More spacing is always better.
- A person's preferred font is necessarily their best-performing font.
- Passing a contrast ratio establishes usability for all low-vision users.
- A palette is CVD-safe because it passes one simulation.
- Light mode or dark mode is universally more accessible.
- One global visual accessibility mode can optimize heterogeneous users and tasks.

## Open Questions

1. How stable is an individual's envelope across sessions?
2. Can a short calibration approximate a longer psychophysical profile?
3. Do task-specific profiles outperform one personal global profile?
4. Does personalization reduce fatigue as well as improve speed or accuracy?
5. Which visual variables interact strongly enough that one-at-a-time optimization fails?
6. Which CVD simulation metrics correlate with affected-user discrimination?
7. How much semantic redundancy is enough before added cues become clutter?
8. Can design-system tokens express perceptual constraints without encoding medical labels?
9. Can a component expose adaptive parameters without causing layout instability?
10. Can Visual Engineering predict when personalization is unnecessary?

## Recommended Next Research

Priority order:

1. EX-VE-TYP-001 Individual Readability Envelope.
2. EX-VE-TYP-002 Crowding-Responsive Typography.
3. EX-VE-COL-006 CVD Minimax Palette and Semantic Survivability.
4. EX-VE-X-002 Accessibility Without Modes.
5. Add sustained-reading compensation and fatigue experiments after the first four establish measurement infrastructure.

## Research Backlog

- task-specific typography for identifiers and codes;
- preference versus measured performance;
- eye-movement compensation;
- visual stress and repetitive-pattern energy;
- low-luminance and photophobia interactions;
- non-Latin writing systems;
- outdoor mobile and glare conditions;
- aging plus CVD;
- combined impairments;
- calibration persistence and privacy;
- component-level adaptive contracts.

## Suggested Specialized Research Agents

- vision science / psychophysics;
- typography and variable-font engineering;
- color science and CVD modeling;
- accessibility standards;
- HCI experimentation;
- statistics and sequential experimental design;
- assistive technology;
- design-system architecture;
- ethics and human-subject research.

## Parallel Research Opportunities

The typography and color experiments can proceed independently through computational preflight. Human-subject execution can share participant characterization, telemetry schema, consent language, randomization infrastructure, and analysis code.

## Risks

- overfitting to one participant population;
- turning accessibility into hidden profiling;
- treating diagnosis as ground truth for mechanism;
- optimizing speed while worsening comprehension or fatigue;
- allowing personalization to change semantic meaning;
- excessive configuration burden;
- simulation confidence exceeding validation;
- inaccessible calibration itself;
- device-specific results being generalized too broadly.

## Cross-Discipline Opportunities

- state-directed engineering for legal visual transitions and explicit unknowns;
- decision theory for task-weighted error costs;
- information theory for symbol confusion;
- control systems for adaptive calibration;
- psychophysics for thresholds and response curves;
- robust optimization for minimax palette construction;
- human factors for sustained workload and interruption;
- privacy engineering for local-only perceptual profiles.

## Knowledge Relationships

```text
Project Atlas typography
        |
        v
active bottleneck ----> Perceptual Envelope <---- low vision / contrast
        |                       |
        v                       v
task-weighted error      adaptive presentation
        |                       |
        +---- semantic channel survivability ---- color / CVD
```

## Theory Impact Assessment

### Affected Theory Records

- Project Atlas conditional communication model;
- active bottleneck model;
- compensation cost model;
- task-weighted error model;
- REP-VE-COL-001 layered color accessibility model.

### Affected Engineering Principles

- typography should be task-sensitive;
- accessibility validation must include degraded conditions;
- color must not be the sole semantic carrier;
- user control must be preserved.

### New Principle Candidates

**PE-P1. Mechanism before mode.**  
Do not select a visual intervention solely from a disability label when a measurable perceptual bottleneck can be tested.

**PE-P2. Performance before preference.**  
Preference is useful evidence but cannot stand in for accuracy, comprehension, time, fatigue, or abandonment.

**PE-P3. Meaning survives channel loss.**  
Consequential semantics should remain identifiable after loss of any single nonessential visual channel.

**PE-P4. Optimize the task, not the decoration.**  
Typography and color may legitimately vary by task when semantics and layout stability are preserved.

**PE-P5. Compliance is a floor, not a human-performance proof.**

### Deprecated Principles

None of the current accessibility principles are removed. Universal preset interpretations are explicitly deprecated.

### Confidence Changes

- increased confidence that universal dyslexia-font claims should be rejected;
- increased confidence that personalization is worth testing;
- unchanged medium confidence that a compact predictive profile can replace lengthy calibration;
- unchanged medium confidence in simulation-only CVD prediction.

### Predictions Created

- PRED-PE-001: within-person typography response variance will be large enough to identify a usable envelope for a meaningful subset of participants.
- PRED-PE-002: measured crowding will predict spacing benefit better than dyslexia diagnosis alone.
- PRED-PE-003: semantic redundancy will reduce status-identification errors under CVD and forced-color degradation.
- PRED-PE-004: calibrated profiles will outperform fixed named modes on at least one performance dimension without reducing comprehension.
- PRED-PE-005: preference and best measured configuration will diverge for a meaningful subset of users.

### Predictions Invalidated

None yet. The four priority experiments are designed to falsify these predictions.

### Required Theory Registry Updates

Promote Perceptual Envelope and Semantic Channel Survivability to candidate Visual Engineering theory records only after experiment evidence.

## Repository Updates

This branch adds:

- this REP;
- a hypothesis registry;
- an evidence registry;
- four executable experiment specifications;
- a research journal entry;
- operational guidance updates;
- global experiment and hypothesis registry links.

## Website Updates

When the research publisher exposes this project, surface:

- the distinction between compliance and performance;
- the Perceptual Envelope model;
- the four experiment cards;
- explicit confidence and status;
- negative findings against universal dyslexia fonts and universal palettes;
- no marketing claim that personalization is proven until experiments complete.

## AI Consumption Notes

Agents must distinguish:

- **established evidence** from experiment proposals;
- **diagnosis** from measured mechanism;
- **simulation** from affected-user validation;
- **preference** from performance;
- **conformance** from usability;
- **personalization** from semantic mutation.

No agent may introduce a "dyslexia mode" or "color-blind mode" as an evidence-backed universal solution without project-specific evidence.

## Handoff Instructions

1. Run the computational preflight for each experiment before recruiting participants.
2. Preregister primary outcomes, stopping rules, exclusions, and analysis.
3. Keep participant-level raw data separate from the public repository.
4. Record only de-identified aggregate results and approved derived data.
5. Preserve failures and null results.
6. Update the hypothesis registry after every experiment.
7. Produce a new RP artifact when an experiment completes.

## Research Journal

See JR-VE-PE-001.

## Research Quality Metrics

- Primary sources reviewed: 4 external primary/meta-analytic sources plus existing repository primary-source syntheses.
- Independent sources: multiple research groups and W3C.
- Counterexamples reviewed: universal dyslexia font, preference-as-performance, universal color palette, universal contrast threshold.
- Competing viewpoints reviewed: universal defaults versus personalization; diagnosis-based versus mechanism-based intervention.
- Hypotheses tested in this package: 0 experimentally; 8 created for execution.
- Failed hypotheses: 0 new experimental failures; several prior assumptions rejected by external evidence.
- Research completeness: program design complete, empirical program incomplete.
- Confidence gain: moderate on program direction.
- Open questions reduced: classification of the next experiments is substantially sharper; empirical unknowns remain.

## Research Debt

- affected-user CVD validation;
- repeated-session typography calibration;
- low-vision plus dyslexia comorbidity;
- non-Latin scripts;
- visual stress measures;
- calibration privacy model;
- power analyses based on pilot variance;
- stable open dataset for CVD category-confusion performance.

## Appendix

### Operational definition: Perceptual Envelope

A Perceptual Envelope is a bounded set of visual parameter combinations under which a specified observer can perform a specified task within prespecified accuracy, comprehension, time, effort, and error-cost limits under specified environmental conditions.

### Operational definition: Semantic Channel Survivability

Semantic Channel Survivability is the ability of a UI state or distinction to remain correctly identifiable when one or more nonessential presentation channels are removed, degraded, remapped, or unavailable.

## Completion Checklist

- [x] Research objective captured.
- [x] Repository context reconstructed.
- [x] Evidence separated from inference.
- [x] Falsifiable hypotheses created.
- [x] Four priority experiments specified.
- [x] Theory impact documented.
- [x] Research debt recorded.
- [x] Operational agent guidance updated.
- [x] Handoff is executable without prior conversation.
- [ ] Human-subject experiments executed.
- [ ] Priority hypotheses resolved.
