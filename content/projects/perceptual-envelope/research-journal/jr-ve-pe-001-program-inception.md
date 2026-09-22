---
id: JR-VE-PE-001
title: Perceptual Envelope Program Inception
project: perceptual-envelope
date: 2026-09-22
author_agent: OpenAI
status: complete
---

# JR-VE-PE-001: Perceptual Envelope Program Inception

## Starting question

What new typography and color experiments should Visual Engineering run, especially for dyslexia, color-vision deficiency, and other disabilities, and what direction could differentiate the methodology?

## Repository observations

The operational UI briefing had solid general accessibility rules but did not yet express disability-specific mechanisms or experimental pathways.

The deeper research base was stronger than the operational layer:

- Project Atlas already rejects one global readability score;
- typography research already models conditional optima and compensation;
- the color REP already rejects universal contrast, palette, and polarity claims;
- Itten experiments already examine CVD and aging redundancy.

The missing step was integration.

## External evidence checked

1. 2026 dyslexia-font meta-analysis: no reliable general speed/accuracy benefit.
2. individualized-font research: large within-reader differences with preserved comprehension.
3. visual-crowding research: spacing benefit in a measured high-crowding subgroup.
4. personalized-font recommendation research: prediction from reader/font features is plausible.
5. September 2026 WCAG 3 draft: contrast model remains under development and explicitly acknowledges size/weight and possible CVD-specific requirements.

## Synthesis

The common structure across typography, low vision, CVD, and dyslexia is not diagnosis. It is a constrained perception-and-task system.

This led to two candidate concepts:

- Perceptual Envelope;
- Semantic Channel Survivability.

## Decisions

- Do not create a canonical dyslexia mode.
- Do not create a canonical color-blind palette.
- Preserve WCAG 2.2 conformance as mandatory baseline.
- Investigate measured bottlenecks and individualized response surfaces.
- Separate preference, performance, and comfort.
- Treat simulation as fault injection.
- Use affected users for validation.
- Keep adaptive profiles semantic-neutral and privacy-preserving.

## Immediate experiments

- EX-VE-TYP-001
- EX-VE-TYP-002
- EX-VE-COL-006
- EX-VE-X-002

## Negative knowledge captured

The program explicitly records what should not be inferred:

- "dyslexia font has no average benefit" does not mean typography does not matter;
- "personalization can help" does not mean automatic adaptation is net beneficial;
- "CVD simulation finds a collision" does not mean an affected user will make that error;
- "simulation finds no collision" does not prove accessibility;
- "high contrast helps some low-vision tasks" does not mean maximum luminance is optimal;
- "user preference matters" does not mean preference equals performance.
