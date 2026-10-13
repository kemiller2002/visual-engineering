# Universal UI Conformance: Cross-Repository Rollout

Status: staged adoption, not a claim that any existing application already passes  
Normative rule source: [UI Layout and Interaction](../standards/UI-LAYOUT-INTERACTION.md)

## Scope

All currently maintained Echelon applications and sites with shipped UI are in scope, including documentation/catalogs and standalone demos. Every new UI created after adoption joins automatically. Libraries without rendered interfaces have a `not-applicable` assessment for UI behavior; libraries with examples/sites DO participate for those surfaces. Preserve product-specific meaning, brand identity, and architecture.

This work is NOT an instruction to bulk-edit all historical repositories or replace per-product layout choices with one rigid grid. The target inventory is a starting register, not conformance evidence or an exhaustive authorized exclusion list.

## Responsibility chain

- **Visual Engineering**: rule versions, severity, probes, evidence vocabulary, independent false-positive review.
- **AVL**: visual relationships/defaults, permitted application-specific profiles and deviation record.
- **Forma**: shared intrinsic control geometry and reusable composition patterns, catalog examples, documentation fixtures.
- **Folio**: print/PDF layout evidence and active site demos.
- **Forma Studio**: declarative alignment groups, design-time feedback and exported conformance metadata.
- **Consumers**: action semantics, observable transitions, app-specific runtime tests.
- **Dokimos**: collect rule IDs, measured deltas, versioned findings and regressions where integrated.
- **Praxis/Ordo/ROS**: schedule repairs and require evidence without bypassing repository-local workflow/approval gates.
- **Vitium**: record actual defects if teams elect to centralize defect reporting; do not claim integration before it exists.

## Staged rollout

### Stage 0: inventory and triage

1. Enumerate repositories and detect maintained UI surfaces: public sites, product screens, interactive docs, component demos, editor previews and rendered print examples.
2. In every candidate repository, verify its current status and label it `app-ui`, `site-ui`, `print-ui`, `service-no-ui`, `archived` or `unknown`. Record actual routes and commands; repo names are not sufficient proof.
3. Create a repository-owned issue/work item referencing the versioned canonical standard; keep its known deviations and exact execution plan in that repository.
4. Give each currently running UI a baseline: actionable controls (including demo pages), peer alignment groups, mobile clipping/overflow, page/print geometry, keyboard and state transitions.
5. Report `unassessed` honestly. An absence of observed findings is not a passed gate.

### Stage 1: shared primitives and pilot

1. Use Forma as first browser test bed; address shared intrinsic input/button/label geometry once, not by per-app margin hacks.
2. Add tested layout relationships to Forma's machine-operable patterns and catalog demos. Exercise every enabled demo action end-to-end or turn it into truthful noninteractive documentation.
3. Use Forma Studio as test of editor composition and generated HTML: aligned form rows and a real interaction path for previews.
4. Use Folio as print variant: alignment, pagination and clipping; separately validate interactive documentation actions.
5. Add a portable Playwright helper/reference suite with no production runtime dependency, verified on CI. Record which browsers, routes and fixtures it covers.

### Stage 2: active applications

Adopt via small product-owned PRs:
- pin an exact standard version/commit or package release;
- add the route/state/control inventory and applicable alignment groups;
- add working tests and a distinct `ui:conformance` command to local scripts or an equivalent native command;
- measure with browser geometry and actual outcomes; do not assert that a control works merely because it can be clicked;
- fix source shared components first, then local composition bugs;
- add traces/screenshots and versioned reports to CI artifacts;
- keep existing known debt as explicit remediation work without masking new defects.

No wholesale framework rewrite. Avoid copying live runtime implementations among applications; share the contract and reusable probes instead.

### Stage 3: enforce release gates

Require new/modified UI to pass relevant rules after each app's test harness is reliable. Migrate old known violations into explicitly owned backlog. Move the full baseline to blocking only after coverage and repairs are demonstrated. Mandatory errors: inert active actions, misleading completion, clipped critical controls, known overlapping controls, and inaccessible required actions. A failed check is not fixed by removing the test.

### Stage 4: prevent future drift

Integrate with Conditor/capability installation where appropriate and with Dokimos/Praxis when those integrations are implemented. CI should report rule ID, violated relationship, page, viewport, evidence, owner and remediation reference. Revision changes trigger revalidation. New UI surfaces should be discovered even if not in this initial list.

## Minimum per-application acceptance record

```yaml
standard: visual-engineering/UI-LAYOUT-INTERACTION@0.1.0
repo: owner/repository
revision: <actual-sha>
classification: app-ui # app-ui | site-ui | print-ui | service-no-ui | archived | unknown
surfaces: [{ route: "/example", states: ["initial", "error"] }]
viewportWidths: [320, 390, 768, 1280]
alignmentGroups: [{ id: "customer-row", role: "top-edge", targets: ["First name", "Last name"] }]
actions: [{ route: "/example", role: "button", name: "Save", outcome: "status or persisted data observed" }]
results: { passed: 0, failed: 0, unknown: 1, notApplicable: 0 }
exceptions: []
evidence: []
```

This is an illustrative *record shape*, not an already published executable schema. Apps must replace every placeholder with real evidence. Tests are responsible for asserting actual behavior; the record alone is not verification.

## Candidate inventory

`framework/rollouts/ui-conformance/targets.csv` records candidate repos and rollout priority. Entries are provisional until source routes are inspected; `unassessed` is not `failed`. Start with owned core platform demos and currently delivered apps/sites, then expand to every maintained UI, including UI discovered outside the initial list.

## PR acceptance checklist

- [ ] Source rule version pinned; affected surfaces and owners known
- [ ] Alignment groups declared; peer top alignment measured or justified
- [ ] No unexplained overflow/clipping at tested widths and states
- [ ] Every enabled demo action inventoried and has an asserted meaningful outcome
- [ ] Key product actions asserted end-to-end, including consequential failure/recovery
- [ ] Keyboard, accessible names and machine-observable completion verified
- [ ] Print checks included if the surface is a Folio output
- [ ] Existing debt, new findings, unknown scope and documented deviations distinguishable
- [ ] No release-gate weakening or unverified success claims

## Exceptions

An exception requires rule ID, affected surfaces/states, reason, alternative considered, owner, expiry/review trigger and evidence. Do not use a repository-wide broad skip or snapshot update as an exception. If many apps need the same deviation, investigate whether the shared rule or primitive is wrong.

## Success criteria

The rollout is complete only when *all maintained shipped UIs* are discovered and classified, active UI surfaces have passing applicable conformance evidence, residual deviations are owned and bounded, and CI prevents reintroduction of prior classes of violation. Having this document, issues or PRs alone is **not** completion.
