---
project: visual-engineering
purposes:
  - apply
  - verify
audiences:
  - practitioner
  - contributor
---

# Application Polish

Polish is the systematic elimination of observable evidence that an application is unfinished. Treat it as engineering work throughout implementation, not a cosmetic pass at the end.

For each material user-facing surface, reason across visual precision, interaction, motion, state completeness, forms, feedback, content, responsiveness, accessibility, performance perception, resilience, data integrity, navigation, environment behavior, security UX, and fit-and-finish.

Prioritize seams such as loading to loaded, empty to populated, valid to invalid, editing to saving to saved, online to offline to recovered, authenticated to expired, wide to narrow, pointer to keyboard, ordinary to pathological content, overlay lifecycle, and optimistic action to rejection.

Use adversarial cases including extreme text, Unicode and RTL, numeric and collection boundaries, broken dependencies, slow or offline behavior, rapid repeated input, zoom and text scaling, constrained viewports, reduced motion, forced colors, expired sessions, stale or conflicting data and interrupted persistence.

A zero-finding review is not proof of polish. Report what was tested, what evidence exists, what failed, what was not applicable, and what remains unknown. Unknown is not pass.

Recoverable failures should preserve valid work. Persistence indicators must tell the truth. Recurrent defects should be moved upstream into shared components, rules, fixtures or automated probes so they become structurally harder to repeat.
