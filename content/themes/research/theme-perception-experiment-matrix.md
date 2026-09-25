# Theme Perception Experiment Matrix

Status: active
Related: #13

## Goal

Turn the theme catalog into a reusable perceptual evidence set, not a palette gallery.

## Independent outcomes

Never combine these into one "quality" score:

1. readability/discriminability
2. figure-ground salience
3. visual hierarchy
4. pair/palette harmony
5. aesthetic preference
6. activation association
7. valence association
8. semantic/status discrimination
9. visual fatigue/attention competition
10. context fit

## Current matrix

| Theme | Experimental dimension |
| --- | --- |
| THM-0001 Quiet Fjord | light + cool + restrained |
| THM-0002 Ember Paper | light + warm + energetic |
| THM-0003 Night Ember | dark + warm + salient accent |
| THM-0004 Deep Current | dark + cool |
| THM-0005 Heather Field | near-hue / muted harmony |
| THM-0006 Signal Neutral | achromatic + single accent control |
| THM-0007 Verdant Signal | green / natural cool |
| THM-0008 Cobalt Citrus | blue-orange dual accent |
| THM-0009 Plum Gold | dark purple-gold association |
| THM-0010 Mono Graphite | achromatic control |
| THM-0011 Coral Tide | warm-cool complementary candidate |
| THM-0012 Electric Night | dark + multiple high-chroma accents |

## Evaluation gates

### Gate 1 — computed
- semantic text contrast
- non-text boundary contrast
- lightness range
- chroma range
- hue separation
- duplicate/near-duplicate distance

### Gate 2 — rendered specimen
Render identical reference surfaces for every theme:
- prose + headings + links
- primary/secondary/destructive actions
- form controls, focus, disabled
- cards and elevation
- table
- status messages
- small data visualization
- dense dashboard
- print/document sample when applicable

### Gate 3 — perceptual review
Record outcomes independently:
- hierarchy clarity
- foreground salience
- harmony
- attentional competition
- semantic confusion
- fatigue concerns

### Gate 4 — contextual evidence
Where a theme is intended for a real product/context, record target population and task. Do not generalize a contextual result to all applications.

## Pairwise experiments

Prefer controlled pairs that alter one dimension:
- warm vs cool with similar lightness/chroma
- high vs low chroma with similar hue/lightness
- light vs dark mode using comparable semantic relationships
- analogous vs separated hue while holding lightness contrast
- one accent vs two competing accents
- subtle vs functional 3:1 boundaries
- chromatic vs achromatic control

## Status policy

- `draft`: hypothesis only
- `evaluated`: computed checks plus rendered review complete
- `accepted`: useful for a stated context with evidence
- `deprecated`: retained for history but no longer recommended for that context

Acceptance is contextual. There is no globally "best" theme.

## Next expansion

After THM-0012, prioritize:
- controlled one-variable variants of existing themes
- status palettes for success/warning/error/info
- color-vision-deficiency robustness
- data-series palettes
- print mappings
- reduced-chroma/fatigue variants
- high-contrast functional boundary variants

Do not add more decorative combinations until these controlled variants begin producing comparative evidence.
