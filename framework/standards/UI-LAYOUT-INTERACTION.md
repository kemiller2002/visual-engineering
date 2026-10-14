---
id: GV-VE-2026-B61F
---

# Universal UI Layout and Interaction Conformance

Version: 1.0.0  
Status: canonical and normative for newly changed Echelon UI; brownfield adoption is staged  
Canonical reference: `visual-engineering/UI-LAYOUT-INTERACTION@1.0.0` (evidence schema `framework/contracts/ui-conformance.schema.json`, `standardVersion: "1.0.0"`)  
Owner: Visual Engineering  
Applies to: active application interfaces, marketing and documentation sites, component demonstrations, and interactive preview environments. Folio print output adopts geometry/overflow provisions with print-specific evidence.

This is the single authoritative version of the universal layout and interaction contract. Application Visual Language describes visual relationships and design intent; Forma implements reusable screen presentation; Folio implements print presentation; applications own their behavior. Consumers pin this version and cite rule IDs; they do not restate or fork the rules.

This standard extends [APPLICATION-POLISH.md](APPLICATION-POLISH.md) and does not replace the Forma machine-operability contract, accessibility requirements, product semantics, or application-specific design profiles.

## Principle

An interface is not finished when peer elements are unintentionally misaligned, visible controls suggest behavior they do not provide, or demos claim functionality they cannot demonstrate. Every relevant layout relationship and human-visible action must have a verifiable contract. Visual similarity alone is not proof of correctness.

## Authority and ownership

- Visual Engineering owns observable quality invariants and the evidence threshold.
- Application Visual Language (AVL) owns bounded representation guidance. Application profiles own expressive values.
- Forma owns reusable presentation primitives, intrinsic geometry, tokens and semantic markup, **not** application domain behavior.
- Folio owns reusable print/paged-media geometry and semantic document primitives.
- The consumer application/Limen owns non-native interaction implementation and observable status.
- Ordo/application state owns permitted transitions and consequences.
- Playwright is the reference browser verifier; automation must remain operable through semantic paths.
- Dokimos may record findings; Praxis/ROS may require conformance evidence. Neither a static scan nor a screenshot by itself proves action completeness.

### Rule strength

**Required**: must be evidenced or explicitly excepted. **Recommended**: default with documented justification for material differences. **Contextual**: determined by explicit product/semantic structure. A machine assertion is justified only if the relationship it checks is declared, not guessed from CSS coincidence.

## Layout invariants

| ID | Strength | Contract |
| --- | --- | --- |
| ULI-L01 | Required | Declare meaningful alignment groups. Peer inputs, selects and other fields in a shared form row align at their top edges or declared baselines; different label lengths, help text or validation messages must not inadvertently stagger peer control edges. Do not assume unrelated fields must align. |
| ULI-L02 | Required | Equivalent controls in the same density/context have coherent intrinsic heights, padding, baseline alignment and geometry, or a documented semantic difference. |
| ULI-L03 | Required | Composition/parent owns cross-component layout; primitives own their internal geometry. Consumers must not patch recurring shared defects with per-page pixel offsets. |
| ULI-L04 | Required | Grouping and gaps express hierarchy using documented semantic roles/tokens; local values cannot obscure relationships or create inconsistent equivalent layouts. |
| ULI-L05 | Required | At supported widths, browser zoom, text scaling, long labels, translated and right-to-left text, and content or state changes, text does not overlap boundaries or escape its container, and there is no unintended clipping, overlap, horizontal page scrolling or offscreen critical action. Essential two-dimensional content may have a contained, labeled scroller. |
| ULI-L06 | Required | Navigation labels remain meaningful. Preserve each destination's readable accessible name; either preserve label integrity or recompose navigation rather than crop or overlap it. |
| ULI-L07 | Required | Icons and text inside controls are perceptually centered with a stable baseline; intrinsic button geometry does not vary inexplicably within a group. |
| ULI-L08 | Required | Empty, loading, validation, error, expanded, collapsed, flyout and modal states preserve valid geometry and focus; state changes must not create avoidable layout jump or trap controls. |
| ULI-L09 | Required | Mobile layouts are functional compositions, not merely compressed desktop layouts: responsive changes retain semantic order, grouping, comparisons, accessible navigation and action scope. Test at 320, 390, 768 and 1280 CSS pixels plus relevant content-driven breakpoints. |
| ULI-L10 | Required | Related buttons align to a shared contextual edge in a predictable order and wrap or recompose sensibly when space is constrained, respecting language direction and accessibility needs. |
| ULI-L11 | Contextual | Text areas, editors, tables, diagrams and complex panels can intentionally differ from ordinary fields when their visual relationships are specified and verified. |
| ULI-L12 | Required | Folio print/paged-media output additionally validates page edges, page breaks, clipping, table and content continuation, headers/footers, page numbers, and print/PDF output geometry at intended media sizes. Browser print preview and PDF rendering are verified separately. |
| ULI-L13 | Required | Spacing, sizing and alignment use shared design tokens and composition primitives. Arbitrary one-off positional corrections (negative margins, per-page pixel nudges) are not an acceptable repair for a shared or recurring defect. |

**Geometry tolerance.** For declared equal-top or equal-baseline groups in the same visual row, the default automated browser tolerance is `<= 1 CSS px` once fonts and layout stabilize. This is an engineering starting point, not a WCAG threshold. Browser/renderer effects or deliberate optical corrections may justify a documented per-group tolerance. Tests must compare actual bounding boxes after settling; never hardcode absolute viewport coordinates or guess which elements are peers.

## Action and demonstration invariants

| ID | Strength | Contract |
| --- | --- | --- |
| ULI-A01 | Required | Every enabled, visible action control has a truthful semantic action or navigates to a meaningful destination. No empty/no-op handlers, decorative buttons, `javascript:` links or fake operations. |
| ULI-A02 | Required | An action has a machine-observable outcome: navigation, native state, DOM state, visible information, validated submission, download, or legitimate external operation. Click success alone is insufficient. |
| ULI-A03 | Required | Every shipped demo button and control performs the documented behavior with real local/native state or a genuinely wired operation. If an unavailable backend is simulated, clearly label the simulation and never present it as a completed real operation. |
| ULI-A04 | Required | Disabled controls express a real constraint and a way to understand it. A labeled disabled-state specimen may be intentionally disabled; unfinished functionality is not a legitimate reason to ship a prominent disabled button. |
| ULI-A05 | Required | Every interactive demo scenario supplies initial state, invocation, expected observable result, and a repeat/reset strategy (or documented immutable terminal state). |
| ULI-A06 | Required | Buttons, links, switches, disclosure elements, menu items, gestures and custom widgets use correct roles/names and offer human/machine parity. Follow Forma machine-operability rules. No action relies exclusively on coordinates, hover, animation or implementation-specific selectors; drag operations that perform meaningful edits have a semantic alternative. The final state is observable without fixed sleeps. |
| ULI-A07 | Required | Loading, success, partial success, error, cancellation, reset, retry and recovery behave honestly and never lie about persistence or completion; a missing backend never yields a falsely successful operation. Side-effecting retries and repeated rapid activation require idempotency/reconciliation as appropriate. |
| ULI-A08 | Required | All actionable surfaces remain operable by keyboard and required input alternatives; touch target and focus behavior satisfy existing accessibility standards. |
| ULI-A09 | Required | Unimplemented actions are removed, clearly presented as noninteractive documentation, or gated from shipping with visible honest explanation. Do not make a disabled placeholder the default design. |
| ULI-A10 | Required | Each important workflow has at least one end-to-end action assertion, including a failure/recovery case where relevant. Demo catalogs exercise **every** enabled example action rather than only a sample. |

For browser-native controls, native semantics and browser effects are legitimate action outcomes. Do not add JavaScript merely to satisfy an artificial counter or feedback requirement. For an external link, verify a meaningful, valid destination without claiming that the external service completed an operation.

## Conformance evidence

For each released UI surface, retain:
1. Identified version/commit, app, route/page and applicable rule IDs; unknown scope must remain unknown.
2. Declared peer-alignment groups and corresponding measured coordinates/deltas across relevant viewports; `not-applicable` with rationale for pages without side-by-side controls.
3. An action inventory for each demo page and representative critical app workflows: role, accessible name, action owner, initial state, invocation and expected outcome.
4. Browser assertions that measure geometric invariants and assert *outcomes*, not only clicks. Capture Playwright traces/screenshots on failure.
5. Accessibility/mobile/state adversarial coverage: 320/390/768/1280, zoom/text growth, long labels, expanded, disabled, loading, failure and reduced motion as relevant.
6. Exceptions: rule ID, route, affected state, product reason, owner, evidence, expiry/re-review trigger. No blanket global suppression.
7. Residual findings separated into new-change failures, pre-existing debt and unknown coverage.

Do not claim "all buttons tested" unless every in-scope demo action was enumerated and exercised. Static HTML scans can flag suspicious `href="#"`, buttons lacking names, or placeholder text; they cannot prove handlers are inert. Runtime behavior needs app-specific state assertions.

## Enforcement and rollout

- **New and modified UI:** enforce required applicable rules immediately in the changed surface, once a stable runner exists.
- **Existing UI:** inventory first, capture a baseline, and create bounded repair work. Baseline debt remains visible; do not convert untested work to green.
- **Pilot:** Forma catalog/marketing, Forma Studio and Folio site/print. Verify rule usefulness and false positives before enabling shared blocking policy.
- **Application waves:** active Echelon applications/sites, then remaining maintained UIs. Non-UI service repositories record `not-applicable`; historical/archived repositories are not automatically rewritten.
- **Release gate:** block unresolved *new critical* actionable/geometry defects; expand to the full mandatory contract after baseline remediation and signed conformance evidence. No unexplained opt-outs.
- **Discovery:** every newly shipped UI joins automatically; inventory is a tracking aid, not a whitelist.

## Failure classification

- `FAIL`: reproducible violation against a declared rule and surface.
- `PASS`: observed assertion on the declared surface/environment.
- `UNKNOWN`: missing evidence, unsupported renderer or untested route/state.
- `NOT_APPLICABLE`: documented absence of the relevant phenomenon.
- `EXCEPTION` (approved exception): owned, bounded, justified deviation with rule ID, surface, owner, rationale and review trigger; not an invisible pass.

Lack of evidence is `UNKNOWN`, never `PASS`.

## Versioning

This standard follows semantic versioning. A new required rule, a stricter tolerance or a changed status vocabulary is a major change; a new recommended rule or clarification is minor; editorial fixes are patches. The evidence schema's `standardVersion` changes with the major/minor version of this document.

| Version | Date | Change |
| --- | --- | --- |
| 1.0.0 | 2026-10-13 | First canonical release. Adds ULI-L13 (tokens and primitives over one-off offsets); makes ULI-L10 required (button groups align and wrap sensibly); extends ULI-L01, L05, L09, L12, A06 and A07 for help text, translation/RTL, functional mobile layout, print continuation, hover/animation/drag alternatives and honest reset/retry; adds the `demo-ui` classification. |
| 0.1.0 | 2026-10-13 | Rollout baseline draft. |

See the rollout protocol and its candidate inventory in `framework/protocols/UI-CONFORMANCE-ROLLOUT.md`.
