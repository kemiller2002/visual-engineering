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

# Semantic Channel Survivability

**Semantic Channel Survivability** is the ability of a UI state or distinction to remain correctly identifiable when one or more nonessential presentation channels are removed, degraded, remapped, or unavailable.

Candidate visual channels include:

- hue;
- luminance;
- text labels;
- icon geometry;
- border or shape;
- pattern or line style;
- position;
- motion.

Programmatic semantics remain a separate required channel for assistive technology.

## Operational principle

Semantic Channel Survivability is **provisionally operational**.

Consequential meaning should not depend on a single fragile visual channel.
Important and critical states should survive loss of any one declared visible
channel. Critical states should retain an explicit text or icon cue as well as
programmatic semantics.

Testing should deliberately remove or degrade channels rather than only inspect
the full design. The executable robustness analyzer implements this rule now;
future human evidence may tune the thresholds without changing the underlying
semantic model.

## Related Documents

- RP-VE-PE-001
- EX-VE-COL-006
- REP-VE-COL-001

- Perceptual Failure Boundary
