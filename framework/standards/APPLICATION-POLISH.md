# Application Polish Standard

Status: normative
Established: 2026-09-26

## Principle

Application polish is a first-class Visual Engineering concern. It SHALL be designed, specified, tested and evidenced throughout delivery. It SHALL NOT be deferred to an informal final visual pass.

## Required polish dimensions

Applicable products SHALL address: visual precision; interaction; motion; state completeness; forms; feedback; content; responsiveness; accessibility; perceived and actual interaction performance; resilience; data integrity; navigation; supported environment behavior; security UX; and fit-and-finish.

A dimension MAY be declared not applicable only with recorded rationale.

## Core invariants

1. Every applicable interactive element SHALL define pointer, touch, keyboard, focus, selected/active, disabled, loading and error behavior as relevant.
2. Every asynchronous operation SHALL expose an intentional presentation state and SHALL prevent ambiguous duplicate operations where duplication is unsafe.
3. Recoverable failure SHALL preserve valid user work unless an explicit domain constraint makes preservation impossible.
4. Presentation SHALL be a deterministic projection of known application state wherever practicable; contradictory visual states are defects.
5. Supported viewport and zoom ranges SHALL not produce unintended clipping, overlap or horizontal overflow.
6. Motion SHALL be interruptible or safely completed without leaving illegal visual/application state, and reduced-motion preferences SHALL be respected.
7. Saving/saved/syncing/error indicators SHALL accurately represent persistence state. Optimistic UI SHALL define rollback/reconciliation behavior.
8. Keyboard navigation SHALL preserve logical focus order and visible focus. Focus SHALL be intentionally placed/restored across overlays and navigation.
9. Empty, loading, partial, stale, degraded, offline, unauthorized, forbidden, validation-failure, recoverable-failure and terminal-failure states SHALL be intentionally handled when reachable.
10. User-facing text SHALL use consistent terminology and formatting and SHALL not expose placeholder or implementation language.
11. Navigation SHALL support applicable deep-link, refresh, browser-history and state-restoration behavior.
12. Security and permission failures SHALL fail safely while preserving context and actionable recovery where permitted.
13. A claim of polish SHALL identify tested environments, states, seams, adversarial fixtures, exceptions and retained evidence.
14. Zero observed defects SHALL NOT by itself satisfy the polish gate.

## Seam coverage

Teams SHALL enumerate material state transitions and environmental seams. High-risk seams SHALL receive explicit scenarios. At minimum consider loading/loaded, empty/populated, valid/invalid, edit/save/failure, online/offline/recovery, authenticated/session-expired, responsive breakpoint transitions, input-modality transitions, overlay lifecycle, and optimistic/rejected operations.

## Adversarial fixtures

Products SHALL use representative pathological inputs rather than only ideal fixtures. The reusable corpus SHOULD cover extreme text length, Unicode/RTL, numeric boundaries, collection-size boundaries, broken media, slow/failing dependencies, repeated/rapid input, zoom/text scaling, constrained viewports, expired sessions, stale/conflicting data, interruption and recovery.

## Evidence model

Polish evidence SHOULD combine deterministic assertions, accessibility checks, interaction traces, screenshots or visual comparisons where useful, performance measurements where relevant, and explicit manual observations for qualities that cannot yet be reliably automated.

Evidence MUST identify scope. Untested state or environment coverage MUST remain visible as unknown rather than silently passing.

## Release gate

A releasable application SHALL have no unresolved critical polish defect; SHALL have dispositioned material high-severity findings; SHALL have evidence for required dimensions and high-risk seams; SHALL record unsupported/untested scope; and SHALL record any accepted exception with rationale and owner.

The gate evaluates evidence and known gaps, not subjective confidence alone.

## Relationship to other Echelon systems

This standard does not duplicate component, accessibility, security, failure, architecture or code-quality ownership. It consumes those systems' evidence and evaluates the integrated user-observable result. Forma supplies capable primitives; Visual Engineering defines observable quality; Aegis contributes failure behavior; other assurance systems contribute their respective evidence; Ordo/ROS can trace obligations, work and release evidence.
