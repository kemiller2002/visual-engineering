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
