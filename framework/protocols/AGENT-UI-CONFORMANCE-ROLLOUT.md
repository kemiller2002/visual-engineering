# Agent Execution Brief: Universal UI Conformance Rollout

**Mission:** Deliver the governed universal layout and real-action contract across every maintained Echelon application, marketing site, component demo and interactive document preview. Start from [the standard](../standards/UI-LAYOUT-INTERACTION.md), [the rollout procedure](UI-CONFORMANCE-ROLLOUT.md) and [the candidate inventory](../rollouts/ui-conformance/targets.csv). Parent tracking issue: https://github.com/kemiller2002/visual-engineering/issues/47.

## Coordinator instructions

You are the lead agent. Use subagents for isolated, owned repositories, with code review and verification independent of implementation. Do not claim execution unless artifacts, checks and linked commits exist. Do not invent passed tests, no-UI classifications or completed work items.

### Step 0: respect repository authority

1. Work from the user's local `dev` workspace if available; do not assume your current directory is correct. Confirm each repository's remote and clean working tree before editing.
2. For every repository, read `AGENTS.md`, current state/context, requirements, applicable capability manifests and the canonical validation commands. Use its actual Ordo/ROS/Praxis work-item lifecycle **before** meaningful edits. Do not fabricate state or modify installed lifecycle internals.
3. Fix governance issues in standard PRs before merging, through each repository's real work-item lifecycle (Visual Engineering #46 attributes its commits to WI-0019 by Git-evidenced post-hoc reconciliation). Never bypass validation.
4. Adopt the canonical rule by pinned version/commit after it passes governance. Do not copy divergent standards into consuming repositories.
5. Prefer a repository-owned branch and PR. Respect other agents' uncommitted files, migration windows and repository-level concurrency constraints.

### Step 1: discover the real inventory

- Read every row in `targets.csv`; for each inspect actual UI route/site/build commands and confirm whether it is maintained.
- Search the complete owned repository list for missing maintained UIs. New active UIs must be added; a name-based list cannot establish completeness.
- Classify each as `app-ui`, `site-ui`, `demo-ui`, `print-ui`, `service-no-ui`, `archived` or `unknown`. Document evidence and ownership; don't assume a repository is an app just because its name sounds like one.
- Update corresponding issue with discovered routes, initial violations and baseline evidence. Keep `unknown` when source or runtime can't be inspected.

### Step 2: fix sources first

- **Forma:** audit 166 component pages / 717 current example specimens; clearly distinguish live demos from static semantic examples. An enabled live demo action must produce a repeatable, observed result. Align peers via composition-level primitives, not ad hoc top margins; keep Forma zero-runtime.
- **Forma Studio:** use Forma-owned geometry, express layout relationships in the authoring model and exported HTML; verify editor and demo behavior.
- **Folio:** test print/PDF page geometry separately from website action behavior; no paper layout should silently clip or lose headings.
- When a defect arises from a shared library, create one upstream repair and tests, then upgrade consuming applications by a pinned release. Never patch the same intrinsic component offset in a dozen repositories.

### Step 3: prove each application

For every actual UI surface:
- Inventory every visible enabled **demo** button and its semantic meaning; record action owner, initial state, expected observable result and reset/replay strategy.
- Sample all critical product workflows, with at least one valid and one relevant failure/recovery route. A click or screenshot is not functional evidence.
- Declare meaningful alignment groups, with edges and responsive semantics; do not force alignment between unrelated elements.
- Test at 320/390/768/1280 CSS pixels plus content-specific breakpoints; zoom, long text, error/loading state, keyboard, touch and reduced motion where applicable.
- Use Playwright semantic roles/names and actual state assertions; do not use hardcoded coordinate actions, transient selector IDs or fixed sleeps as the canonical interaction path.
- Classify each asserted check as `pass`, `fail`, `unknown`, `not-applicable` or owned `exception`. Include the exact revision, browser/environment, rules, traces, measurements and code/test links.
- Integrate into existing repo CI **after** local conformance runner and fixtures are reliable. Block newly introduced critical failures immediately; track previously existing debt explicitly until it is repaired.
- Keep each product's branding, task priorities, language and domain legality unchanged.

### Step 4: govern release and report honestly

- Stage PRs independently; do not merge failing or unvalidated work and do not weaken existing governance gates.
- For each repository issue, report: confirmed UI status; routes; alignment action counts; tested widths; findings; fixes; tests executed; test outcomes; unknowns; residual exceptions; PR URL; merge/release status.
- Update the parent issue every five finished repository assessments, with exact links and a numerator/denominator separated by `assessed`, `passing`, `failed`, `no-ui`, `unknown` and `merged`.
- Never count a tracker issue, draft PR, green static scan or CI job that hasn't finished as an application pass.
- Completion is **not** all 40 issues created. Completion is all actually maintained UIs discovered and verified under CI with owned bounded exceptions.

## Prioritization

1. **Wave 1:** Visual Engineering/AVL governance, Forma, Forma Studio, Folio. Complete proven reusable probes before scaling.
2. **Wave 2:** public/customer-visible sites and active product workflows; prioritize inert buttons, clipped controls, misleading completion and broken mobile layouts.
3. **Wave 3:** remaining maintained UIs and triage candidates; classify service-only, historical or non-applicable repositories with evidence.

Use horizontal slices: inventory → one shared primitive fix → end-to-end behavior and geometry test → evidence → PR → validated merge. Batch mechanical audit work where independent, but keep domain decisions and state assertions owned by each application.

## Required result per repo

```text
Repository:
Classification and maintained routes:
Standard version:
Agent/work-item ID:
Observed baseline defects:
Declared peer groups / demo action count:
Assertions and viewport coverage:
Tests run and exact results:
Pass / fail / unknown / N/A / exception totals:
Upstream shared repairs:
PR and validation URLs:
Remaining debt, risks and owners:
```

If CI or access prevents work, state the blocker and retain `unknown` status; don't mark it complete or quietly skip the repository.
