---
id: JR-VE-PE-002
title: Provisional Operational Adoption of Perceptual Robustness
project: perceptual-envelope
date: 2026-09-22
author_agent: OpenAI
status: complete
---

# JR-VE-PE-002: Provisional Operational Adoption of Perceptual Robustness

## Decision

Visual Engineering will use Perceptual Envelope, Semantic Channel Survivability,
and Perceptual Failure Boundary as **provisional engineering defaults now**.

Human validation remains valuable, but it is not a prerequisite for using these
models to improve architecture, implementation, review, and automated checks.

This is a deliberate engineering decision under uncertainty. It is not a claim
that the models are clinically validated or universally predictive of human
performance.

## Why move forward

The current evidence is strong enough to reject several weak universal
assumptions, including diagnosis-to-preset mapping, color-only semantics, and the
idea that conformance thresholds alone establish robust human performance.

Waiting for a complete human research program would leave implementations
dependent on less explicit and less testable assumptions.

The preferred posture is therefore:

1. make the current assumptions explicit;
2. encode them in architecture and tooling;
3. keep them versioned and replaceable;
4. gather evidence when practical;
5. revise thresholds or models without requiring products to undo semantic
   architecture.

## Operational model

### Perceptual Envelope

Visual parameters are treated as controllable variables whose useful range may
depend on observer, task, content, environment, and error consequence.

Architecture should preserve the ability to adapt presentation without changing
semantic meaning or legal actions.

### Semantic Channel Survivability

Consequential meaning should survive loss of fragile presentation channels.

The initial Visual Engineering policy requires:

- programmatic semantics for every declared semantic state;
- at least one surviving visible channel for informational states;
- at least two surviving independent visible channels for important states;
- at least two surviving independent visible channels for critical states;
- an explicit text label or icon-shape cue for critical states;
- every important and critical state to survive each single-channel dropout;
- every explicitly declared product degradation scenario to satisfy the same
  state policy after degradation.

These values are engineering defaults, not immutable laws.

### Perceptual Failure Boundary

The Perceptual Failure Boundary is the smallest channel-loss combination that
causes a semantic state to violate the current policy.

Interpretation:

- boundary 0: the baseline state is already invalid;
- boundary 1: one channel loss can break meaning;
- boundary 2: every single-channel loss survives, but some two-channel loss
  breaks meaning;
- higher boundaries indicate additional modeled semantic robustness.

The tool preserves all minimal failure sets rather than reporting only a scalar.

## Executable capability

Visual Engineering now supports:

```bash
visual-engineering robustness --manifest visual-robustness.json
```

The manifest is repository-authored and machine-readable. It declares semantic
states, criticality, channels, programmatic semantics, and optional degradation
scenarios.

The analyzer reports:

- baseline validity;
- single-channel survivability;
- scenario results;
- Perceptual Failure Boundary;
- minimal failure sets;
- human-readable findings;
- equivalent JSON output for CI and agents.

The analyzer is deliberately semantic. It does not claim to simulate dyslexia,
low vision, color-vision deficiency, migraine, or any other human condition.

## Validation of the engineering implementation

The implementation head before this decision record passed:

- F# build with 0 warnings and 0 errors;
- 103/103 .NET tests;
- research inventory, research validation, site build, agent-context build and
  validation, and npm-content inspection;
- ROS validation;
- Limen strict verification;
- packed Visual Engineering artifact testing across Windows, macOS, and Ubuntu
  on Node 20 and Node 24.

The tests challenged two assumptions during implementation:

1. a robustness command cannot be treated like commands that are valid with no
   required arguments because it intentionally requires a manifest;
2. removal of text and icon from an *important* state does not automatically
   violate the chosen policy if two other independent channels remain. Explicit
   surviving text/icon is currently a stricter requirement only for *critical*
   states.

The tests were changed to reflect the stated policy rather than silently
strengthening it.

## What this decision does not establish

This adoption does not establish that:

- the channel model perfectly predicts human perception;
- all channels have equal perceptual strength;
- channel independence is binary in real perception;
- a boundary of two has the same human meaning across all tasks;
- the initial important/critical thresholds are optimal;
- the same policy should apply forever;
- disability-specific simulation can replace affected-user testing.

## Revision triggers

The operational policy should be revised when any of the following occurs:

- human evidence contradicts a threshold or channel assumption;
- product evidence shows systematic false positives or false negatives;
- a new channel class is repeatedly needed;
- two channels currently modeled as independent prove strongly coupled;
- task criticality requires a stronger policy;
- WCAG or another applicable normative standard imposes a stronger requirement;
- the model creates implementation cost without corresponding semantic
  robustness.

Revision should normally change policy or channel semantics, not remove the
underlying capability to declare state meaning explicitly.

## Engineering consequence

Visual Engineering is no longer only a research briefing and checklist for this
area. It now has an executable representation of semantic visual robustness.

Products can begin adopting robustness manifests immediately. Future validation
improves the model rather than blocking its use.
