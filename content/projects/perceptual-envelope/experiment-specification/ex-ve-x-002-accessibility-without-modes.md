---
id: EX-VE-X-002
title: Accessibility Without Modes
project: perceptual-envelope
status: preregistration-draft
priority: critical
hypotheses:
  - HY-VE-PE-005
  - HY-VE-PE-006
---

# EX-VE-X-002: Accessibility Without Modes

## Question

Does a calibrated mechanism-aware visual profile outperform conventional named accessibility modes while preserving semantic consistency and user control?

## Conditions

A. strong universal default;

B. conventional named presets, for example large text, high contrast, dyslexia-oriented typography, or color-vision palette where such presets are normally offered;

C. user-selected manual controls;

D. calibrated Perceptual Envelope profile.

Do not intentionally construct poor presets to make condition D win.

## Calibration

Calibration may estimate:

- useful typography ranges;
- preferred polarity and luminance range;
- spacing sensitivity;
- color-category discrimination;
- tolerance for density;
- motion reduction preferences.

Calibration must expose uncertainty and must allow manual override.

## Tasks

Use at least:

- continuous reading;
- identifier recognition;
- semantic status recognition;
- dense comparison;
- navigation and recovery from error.

## Outcomes

Primary:

- task accuracy;
- comprehension where applicable;
- completion time.

Secondary:

- fatigue;
- confidence;
- preference;
- mode switching;
- manual overrides;
- abandonment;
- calibration burden.

## Semantic invariants

The experiment fails implementation review if personalization changes the meaning of:

- success;
- warning;
- error;
- unknown;
- selected;
- disabled;
- legal/illegal action;
- completion;
- uncertainty.

Presentation may adapt. Semantics may not.

## Privacy

Prefer a local device profile containing visual parameters rather than medical labels.

Do not require users to disclose a diagnosis to obtain an adaptation.

Profiles should be:

- inspectable;
- editable;
- resettable;
- portable only by explicit user action;
- unnecessary for baseline accessibility.

## Falsification

HY-VE-PE-005 is weakened if calibrated profiles do not outperform a strong universal default or manual controls by a practically meaningful amount, if calibration burden exceeds the benefit, or if profile instability creates errors.

A null result may imply that good universal defaults plus direct user controls are preferable to automatic adaptation.

## Differentiation test

This experiment is the direct test of whether Visual Engineering should become an adaptive visual system or remain a rigorous evidence-based design method without personalization.
