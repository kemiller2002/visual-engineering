---
purposes:
  - reference
  - integrate
  - apply
audiences:
  - practitioner
  - researcher
  - contributor
---

# Perceptual Failure Boundary

A **Perceptual Failure Boundary** is the smallest defined degradation of a visual
representation that makes a required semantic distinction fail the current
Visual Engineering robustness policy.

The first operational implementation models degradation as loss of independent
semantic channels rather than as a simulated person.

Examples of channels include:

- hue;
- luminance;
- text labels;
- icon shape;
- border or shape;
- pattern;
- position;
- motion.

## Operational status

This concept is **provisionally operational**.

Visual Engineering will use it for engineering decisions now even though the
relationship between the channel model and human performance has not been fully
validated. The model must remain versioned, reversible, and open to revision as
human evidence becomes available.

The purpose is to make better decisions under uncertainty, not to convert a
working theory into a clinical claim.

## Initial calculation

For each semantic state:

1. declare its criticality;
2. declare every independent visible channel carrying the state;
3. require programmatic semantics;
4. remove channels in combinations;
5. find the smallest combination that causes the state to violate its policy;
6. report that combination size as the failure boundary;
7. preserve all minimal failure sets for diagnosis.

A boundary of zero means the design is already invalid before degradation.

A boundary of one means a single channel loss can break meaning.

A boundary of two means every single-channel loss survives, but at least one
two-channel loss breaks meaning.

## Initial policy

The current operational policy is intentionally conservative:

- informational states require at least one surviving visible channel;
- important states require at least two surviving independent visible channels;
- critical states require at least two surviving independent visible channels;
- critical states must retain an explicit text or icon cue;
- every declared semantic state requires programmatic semantics;
- important and critical states must survive every single-channel dropout;
- product-specific degradation scenarios may impose stricter requirements.

This policy is an engineering default, not a universal law of perception.

## Why this matters

Conventional accessibility checks frequently ask whether an interface passes a
threshold under its intended presentation.

Perceptual Failure Boundary asks a different question:

**How much can the representation degrade before meaning breaks?**

That makes robustness measurable before human testing is available and creates a
common language for comparing alternative representations.

## Executable form

The Visual Engineering CLI can evaluate a repository-authored robustness
manifest:

```bash
visual-engineering robustness --manifest visual-robustness.json
```

The analyzer calculates baseline validity, single-channel survivability,
declared degradation scenarios, failure boundaries, and minimal failure sets.

The analyzer does not claim to reproduce dyslexia, low vision, color-vision
deficiency, migraine, or any other human condition.

## Related concepts

- Perceptual Envelope
- Semantic Channel Survivability
- task-weighted error cost
- semantic projection
- accessibility and human factors
