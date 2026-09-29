# Application Polish Requirements

Status: required for user-facing application work
Established: 2026-09-26

- VE-POLISH-001: Application polish SHALL be treated as an engineering requirement throughout implementation, not a final cosmetic activity.
- VE-POLISH-002: Every material user-facing surface SHALL identify applicable polish dimensions from the Application Polish Standard.
- VE-POLISH-003: Reachable non-happy states SHALL be explicitly represented or declared not applicable with rationale.
- VE-POLISH-004: Material state transitions and environmental seams SHALL be inventoried and high-risk seams SHALL be tested.
- VE-POLISH-005: User-facing work SHALL exercise applicable adversarial fixtures covering content, quantity, dependency, viewport, input, accessibility preference, authentication and persistence boundaries.
- VE-POLISH-006: Evidence SHALL distinguish passed, failed, untested and not-applicable coverage. Unknown SHALL NOT become pass.
- VE-POLISH-007: A zero-finding review SHALL NOT satisfy the release gate without coverage evidence.
- VE-POLISH-008: Critical findings SHALL block a polished-release claim. High findings SHALL be resolved or explicitly dispositioned.
- VE-POLISH-009: Persistence indicators SHALL truthfully distinguish local, pending, persisted, failed and conflicting states where those distinctions exist.
- VE-POLISH-010: Recoverable failures SHALL preserve valid user work unless a documented domain constraint prevents preservation.
- VE-POLISH-011: Responsive, zoom, keyboard, focus, reduced-motion and forced-color behavior SHALL be intentionally verified where applicable.
- VE-POLISH-012: Agents performing UI work SHALL report polish verification, material unknowns and justified deviations in handoff.
- VE-POLISH-013: Recurrent defect classes SHOULD move upstream into Forma primitives, Visual Engineering rules, fixtures or automated probes.
- VE-POLISH-014: Integrated polish assessment SHALL consume evidence from specialized accessibility, failure, security and quality systems without duplicating ownership.
- VE-POLISH-015: Polish evidence SHOULD conform to `schemas/application-polish-evidence.schema.json`.
- VE-POLISH-016: The reusable adversarial corpus SHALL remain versioned and extendable; discovered production defect classes SHOULD refine it.
