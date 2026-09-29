# Application Polish Automation

Application polish automation is evidence collection, not a substitute for product judgment.

## Coverage profiles

`registries/application-polish-profiles.json` defines reusable baselines for informational sites, interactive applications, transactional applications, and agent interfaces. Profiles can inherit another profile. The gate unions inherited dimensions and fixtures.

Run:

```
npm run polish:gate -- path/to/evidence.json --profile transactional-application
```

A missing required dimension or fixture produces `incomplete`. It does not fabricate a failure and it never silently passes.

## Probe contracts

`registries/application-polish-probes.json` defines bounded claims for automation. Each probe states what it can contribute evidence for and, critically, what it does not prove.

Initial contracts cover document overflow, visible focus, zoom/reflow, reduced motion, rapid repeated actions, persistence truth, offline recovery, and session expiry.

Probe results may be recorded in the evidence document as `passed`, `failed`, `unsupported`, or `not-run`. A probe result is supporting evidence. The corresponding state, seam, environment, or fixture remains the gate's coverage unit so a tool cannot expand its own authority merely by reporting success.

## Design rule

Automate observations that are objective and bounded. Keep semantic quality, task appropriateness, perceptual judgment, and product-specific correctness subject to explicit assertions or human review.

When a recurring defect becomes objectively detectable, add or refine a probe contract and preserve its limitations.

## Motion evidence

`scripts/application-polish-motion.mjs` evaluates the optional `motion` section of polish evidence, and `scripts/application-polish-gate.mjs` folds its result into the disposition. `examples/application-polish-motion-evidence.json` shows inertial, cadence, direct (drag phase and determinate progress), post-release settling and perceptual entries. It is deliberately incomplete because boundary coverage is unknown: `npm run polish:motion-example` exits 3, and CI asserts that. `npm run polish:test` covers the motion rules.

## Motion probes

`scripts/application-polish-motion-probes.mjs` runs reusable adversarial motion probes against any web surface. It is framework-neutral: a JSON configuration (`schemas/application-polish-motion-probes.schema.json`) names selectors, declarative actions and semantic readers, and it does not assume any particular design system. Each result is `pass`, `fail`, `not-applicable` or `unknown`. It records the evidence, the exercised environment (browser, version, viewport, reduced-motion and forced-colors state) and the `state` it covers. A probe that cannot observe its target reports `unknown`, never `pass`.

```
npm run polish:motion-probes -- config.json report.json --evidence polish-evidence.json --write polish-evidence.json
```

`--evidence` folds the results into the matching `motion.entries[*].checks` through `applyMotionProbeResults`. When results cover several environments, they combine to the most severe status (failed > unknown > passed > not-applicable). The Application Polish gate then evaluates the merged evidence.

Apply each probe where its phenomenon exists:

| Probe | Motion check | Apply when |
| --- | --- | --- |
| `motion-semantic-authority` | `semanticAuthority` | Any animated state change: selection, open/close, toggle, status. |
| `motion-rapid-repeat` | `rapidRepeat` | Press feedback, selection indicators, toggles, anything users can activate quickly. |
| `motion-interruption` | `interruption` | Any inertial or gravity transition that can be retargeted mid-flight. |
| `motion-reduced-substitution` | `reducedMotion` | Every surface that animates spatially or repeatedly. |
| `motion-focus` | `focus` | Dialogs, popovers, menus, flyouts, route/view transitions, collapse/expand. |
| `motion-direct-lag` | `directManipulation` | Drag, resize handles, scrubbers, custom sliders and other pointer-driven mappings. |
| `motion-progress-bounds` | `progressBounds` | Determinate progress bars, meters and completion indicators. |
| `motion-cadence-stops` | `cadenceStops` | Spinners, skeleton shimmer, pulsing activity and other indeterminate indicators. |
| `motion-rejected-drop` | `boundaries` | Reorder, drag-and-drop and any drop target that can refuse. |
| `motion-boundary` | `boundaries` | Resize, split panes, collapse/expand, snap points. |
| `motion-progressive-fallback` | `progressiveFallback` | View Transitions, anchor positioning, scroll-driven animation, intrinsic-size interpolation and other progressive motion features. |
| `motion-concurrent-composition` | `concurrentComposition` | Elements that combine press, hover, selection, entry or drag effects. |
| `motion-semantic-cue` | `semanticIndependence` | Any state change whose meaning must not depend on motion alone. |

`examples/motion-probes/` holds a paired fixture. `?variant=good` satisfies each invariant and `?variant=bad` violates it, and `npm run polish:motion-probes:test` proves every probe passes the former and fails the latter in a real Chromium.

Limitations: the probes are engineering evidence for specific, observable invariants. They do not prove universal human comfort, perceived smoothness or performance. Canvas or script-driven frame loops are invisible to `document.getAnimations()`, and semantic truth that the DOM does not expose still needs application or domain evidence.

