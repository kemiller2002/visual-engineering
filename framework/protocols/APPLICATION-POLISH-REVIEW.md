# Application Polish Review Protocol

Status: normative
Established: 2026-09-26

## Purpose

Provide a repeatable, evidence-producing review of whether an application behaves like a finished product across states, transitions, environments and adversarial conditions.

## Procedure

### 1. Declare scope
Record routes/surfaces, supported viewport classes, browsers/platforms, input modalities, accessibility assumptions, integrations and critical user journeys.

### 2. Build the state inventory
For every material surface identify reachable initial, loading, partial, populated, empty, stale, offline/degraded, permission, validation, error, retry, saving/saved and unsaved states. Mark unreachable states explicitly rather than omitting them.

### 3. Build the seam inventory
Enumerate state transitions and boundary changes. Rank seams by user impact, frequency, irreversibility, data risk and uncertainty.

### 4. Exercise adversarial fixtures
Apply pathological content, boundary quantities, network/dependency faults, rapid/repeated actions, viewport/zoom changes, input modality changes, session changes and interrupted operations appropriate to the product.

### 5. Inspect the sixteen polish dimensions
Evaluate visual precision, interaction, motion, state completeness, forms, feedback, content, responsiveness, accessibility, performance perception, resilience, data integrity, navigation, environment behavior, security UX and fit-and-finish.

### 6. Capture evidence
For every required claim retain the strongest practical evidence: assertions, traces, measurements, screenshots/comparisons, accessibility results and bounded manual observations. Associate evidence with the state/seam/environment it demonstrates.

### 7. Record negative knowledge
Explicitly record untested browsers/devices, unreachable fixtures, missing instrumentation, uncertain behavior and assumptions. Unknown is not pass.

### 8. Classify findings
Classify by user consequence rather than cosmetic appearance alone: critical (unsafe/data loss/core task impossible), high (major task/recovery/accessibility failure), medium (material friction/inconsistency), low (minor perceptual or textual defect). Record the violated invariant and reproduction state/seam.

### 9. Re-test transitions
After fixes, test both the defect state and adjacent transitions. A local visual fix is insufficient if it creates a new seam failure.

### 10. Gate
Produce a polish evidence summary containing scope, coverage, findings, resolved findings, accepted exceptions, unknowns and release disposition. Zero findings without coverage evidence is incomplete.

## Required review questions

Does every action visibly and truthfully respond? Can interruption leave contradictory state? Can failure destroy recoverable work? Does persistence truth match presentation? Can content break geometry? Can modality or viewport changes strand the user? Can authentication/session changes preserve safe context? Can browser history/refresh reconstruct an intelligible state? Are reduced motion, zoom and keyboard paths intentional? What did we not test?

## Continuous use

Run focused polish review when new states, transitions, components or environments are introduced. Run the full gate before a release claim of finished/polished quality. Feed recurring defect classes back into Forma components, Visual Engineering standards and automated probes so the same defect class becomes harder to reintroduce.
