---
id: EX-VE-TYP-002
title: Crowding-Responsive Typography
project: perceptual-envelope
status: preregistration-draft
priority: critical
hypotheses:
  - HY-VE-PE-001
---

# EX-VE-TYP-002: Crowding-Responsive Typography

## Question

Does directly measured visual crowding predict benefit from spacing interventions better than dyslexia diagnosis alone?

## Rival models

### Model A: diagnosis-only

```text
spacing benefit ~ dyslexia diagnosis
```

### Model B: mechanism-aware

```text
spacing benefit ~ measured crowding + diagnosis + interaction terms
```

The experiment is successful only if the rival models are compared prospectively.

## Participant characterization

Record separately:

- dyslexia diagnosis, if present;
- age;
- corrected visual acuity;
- reading proficiency;
- measured crowding;
- baseline reading speed and accuracy;
- relevant visual conditions.

Do not use diagnosis as a proxy for crowding.

## Crowding measure

Use a psychophysical task with isolated versus flanked character recognition and a preregistered threshold or spacing metric. Preserve raw condition metadata.

## Typography manipulation

Compare a bounded set of:

- letter spacing;
- word spacing;
- line spacing;

including a baseline, modest expansion, and settings near the point where grouping is expected to degrade.

Avoid assuming "more spacing" is better.

## Outcomes

- reading speed;
- word/character errors;
- comprehension;
- line-loss or rereading indicators;
- subjective effort.

## Falsification

HY-VE-PE-001 is supported only if measured crowding materially improves prediction of spacing benefit over diagnosis-only baselines on held-out participants or preregistered cross-validation.

It is weakened if diagnosis predicts equally well, crowding is unstable, or the spacing manipulation does not interact with crowding.

## Important negative result

If expanded spacing helps high-crowding readers but harms low-crowding readers, that is strong evidence for Perceptual Envelope theory and against a universal accessibility preset.

## Ethics

This is human-subject research. Obtain appropriate review/consent before execution. The public repository stores protocol, analysis code, and de-identified aggregates, not raw identifiable data.
