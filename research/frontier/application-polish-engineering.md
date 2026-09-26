# Application Polish Engineering

Status: active research frontier
Date established: 2026-09-26

## Research question

What observable properties distinguish a merely functional application from a deliberately finished application, and how can those properties be specified, exercised, evidenced, and release-gated rather than left to subjective final review?

## Working definition

Application polish is the systematic elimination of observable evidence that an application is unfinished.

Polish is not decoration. It includes visual precision, interaction quality, motion, state completeness, forms, feedback, content, responsive behavior, accessibility, performance perception, resilience, data integrity, navigation, environment/browser behavior, security UX, and fit-and-finish.

## Central hypothesis

Polish can be treated as an engineering discipline when observable quality is represented as explicit invariants over states, transitions, environments, inputs, and presentation. A release can therefore make bounded, evidence-backed polish claims instead of relying on a final subjective inspection.

A second hypothesis is that defects cluster at seams: state transitions, environmental changes, exceptional content, interrupted operations, and boundaries between subsystems. Seam testing should therefore find a disproportionate share of user-visible defects.

## Research taxonomy

1. Visual precision: alignment, spacing rhythm, typography, icon geometry, borders, radii, shadows, optical centering, wrapping, truncation, image treatment, component proportions.
2. Interaction quality: hover, press, focus, selected and disabled behavior; targets; pointer/touch/keyboard behavior; double activation; drag thresholds; scrolling; overlays; escape and outside-click behavior.
3. Motion: duration, easing or physics, interruption, reversal, continuity, layout stability, and reduced-motion behavior.
4. State completeness: initial, loading, partial, populated, empty, zero-result, stale, offline, degraded, unauthorized, forbidden, validation-error, recoverable-error, fatal-error, success, retrying, saving, saved, and unsaved states.
5. Forms: labels, instructions, defaults, validation timing, error recovery, paste/autofill, password managers, mobile keyboards, submit/cancel semantics, dirty-state protection, destructive confirmation, duplicate-submission prevention.
6. Feedback: acknowledgement, progress, background status, success and failure feedback proportional to the operation.
7. Content: terminology, capitalization, punctuation, localization, formatting, pluralization, empty-state language, error language, action labels, removal of placeholder/developer text.
8. Responsive behavior: narrow and wide viewports, phones, tablets, landscape, zoom, text enlargement, virtual keyboards, safe areas, touch/pointer changes and pathological content.
9. Accessibility: semantics, keyboard operation, tab order, visible focus, assistive technology, labels, headings, landmarks, contrast, zoom/reflow, reduced motion, error identification, targets, forced colors/high contrast.
10. Performance perception: startup, first useful interaction, layout stability, input latency, motion smoothness, progressive rendering, caching, safe optimism and unnecessary waiting indicators.
11. Resilience: slow/intermittent network, timeouts, malformed responses, expired authentication, stale data, duplicates, refresh during work, multiple tabs, history traversal, retry, restart and partial failure.
12. Data integrity: persistence truthfulness, concurrency, conflict handling, idempotency, optimistic rollback, refresh consistency, timestamps and local/pending/persisted distinctions.
13. Navigation: deep links, refreshability, meaningful URLs, history, restoration of scroll/selection/filter state, modal/flyout history and dead-end prevention.
14. Environment behavior: supported browsers, iOS Safari, install/PWA where applicable, printing, downloads, clipboard, file selection, locale/timezone and autofill.
15. Security UX: session expiry without unnecessary work loss, permission failure recovery, hostile-content rendering, sensitive-data treatment, proportional friction and safe errors/logs.
16. Fit and finish: favicon/title/metadata, startup and terminal error surfaces, 404/500, print, selection, clipboard, filenames, tooltips, shortcuts and first/last moments.

## Seam model

Priority seams include loading -> loaded, empty -> first item, valid -> invalid, editing -> saving -> saved, online -> offline -> online, authenticated -> expired, wide -> narrow, pointer -> keyboard, ordinary -> pathological content, closed -> opening -> open -> closing, and optimistic -> rejected.

The research program should compare seam-focused testing with conventional page/component inspection and measure defect yield, severity and recurrence.

## Adversarial corpus

Maintain reusable fixtures for one-character and very long strings; Unicode, emoji and RTL; zero/negative/huge numbers; zero and very large collections; broken media; slow and failed dependencies; duplicate and rapid input; 200-400% zoom; small/landscape viewports; virtual keyboards; expired sessions; stale/conflicting data; refresh during mutation; offline recovery; and unsupported or degraded capabilities.

## Evidence questions

For each claimed polished surface: what was exercised, which states and seams were covered, under which environments and input classes, what objective assertions passed, what visual/behavioral evidence was retained, what remains untested, and what exceptions were accepted?

Zero findings is not evidence of polish unless coverage evidence exists.

## Cross-system implications

Forma should expose components with complete deterministic states and interaction contracts. Visual Engineering owns observable polish principles and evidence. Percepta can constrain intended UI outcomes. Aegis can supply failure semantics/presentation inputs. Security and quality systems contribute evidence without being duplicated here. Ordo/ROS can trace polish obligations and evidence to work and release decisions.

## Open research

Research should quantify seam defect density; determine useful invariant classes; develop coverage metrics that resist gaming; distinguish objective defects from preference; investigate perceptual thresholds for alignment, latency and motion; study state-projection architectures; establish confidence rules for browser/device sampling; and test whether adversarial fixture suites predict production polish defects.
