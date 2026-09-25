# Color Combination Perception: Evidence Baseline

Status: research baseline
Updated: 2026-09-25
Related work: #13
Purpose: provide evidence-bounded perceptual metadata for the Theme Specimen Catalog.

## Critical distinction

A color may be **associated with** an emotion without **causing** that emotion in a viewer. Theme metadata MUST distinguish association, measured perceptual effect, preference, harmony, and inferred design intent.

## Evidence-backed dimensions

### Lightness and luminance contrast — high confidence for visibility

Light-dark contrast is fundamental to legibility and boundary discrimination. Theme evaluation MUST measure actual foreground/background pairs, not infer readability from hue names. WCAG requires 4.5:1 for normal text and 3:1 for large text under its contrast criterion, and meaningful non-text boundaries commonly require 3:1. Color alone must not encode critical state.

Design implication: record luminance contrast independently from hue contrast. A vivid complementary pair can still be a poor text pair.

### Saturation/chroma and lightness — moderate-to-high confidence for affective association

Across the color-emotion literature, saturation/chroma and lightness are important predictors. More saturated colors tend toward greater activation/arousal associations. Lighter colors tend toward more positive associations, while darker colors tend toward more negative associations. These are population-level associations, not guaranteed emotional effects.

Design implication: index `activation` and `valenceAssociation` as evidence-qualified descriptors rather than promises about user emotion.

### Warm/cool — high confidence as a perceptual dimension, moderate confidence for affective meaning

Warm/cool judgments are reliably perceptible. Recent perceptual work places the warm-cool dimension roughly along an orange-red to green-blue direction and links it with perceived saturation. Warm/cool should therefore be stored as a perceptual facet, while semantic interpretations remain contextual.

### Broad hue-emotion associations — moderate confidence, context-sensitive

A 2025 systematic review of 132 peer-reviewed articles (42,266 participants, 64 countries) reports recurring associations:
- light colors: positive affect
- dark colors: negative affect
- red: high-arousal, empowering positive and negative affect
- yellow/orange: positive, high-arousal affect
- blue/green/green-blue/white: positive, lower-arousal affect
- pink: positive affect
- purple: empowering affect
- grey: negative, lower-arousal affect
- black: negative, higher-arousal affect

Cross-national research also finds substantial common structure, but language, geography, culture, age, context, and personal experience modify associations.

Design implication: these labels can support discovery (for example `positive + low-arousal`) but MUST NOT be treated as deterministic psychology.

### Combination harmony and preference — distinct constructs

Pair harmony, pair preference, and foreground-color preference are not interchangeable. Controlled experiments found that pair harmony and overall pair preference generally increased with hue similarity. Overall preference also depended strongly on preferences for the component colors and lightness contrast. Conversely, preference for a *figural* color against its background increased with hue contrast.

Design implication: a combination can be harmonious without being maximally salient, and a highly salient figure/background combination can be less harmonious as a pair. Store these separately.

### Classical harmony rules — low confidence as universal prescriptions

Recent large perceptual studies challenge the assumption that classical harmonic templates automatically produce preferred results. Harmonized images are not necessarily preferred, and some archetypal harmonic palettes can be rated less harmonious than random palettes.

Design implication: do not label analogous, complementary, triadic, etc. as intrinsically good. Record the geometric relationship as a descriptive feature and evaluate actual specimens.

### Figure-ground and depth — moderate confidence, context-dependent

Experimental evidence supports an advancing tendency for red in figure-ground judgments. Blue recession is less robust and can depend on monocular/binocular conditions. Hue contrast can increase figural salience.

Design implication: `advancing/receding` must be an evidence-qualified contextual observation, never a universal semantic token rule.

### Ecological and learned association — moderate confidence

Color preference partly reflects associations with similarly colored objects and experiences. This provides a mechanism for both shared and individual/cultural differences.

Design implication: provenance and target population matter. Avoid universal claims such as “blue = trust.”

## Combination-level descriptors for THM records

Each evaluated theme SHOULD support these independent descriptors:

- `perceptual.temperature`: warm | cool | neutral | mixed
- `perceptual.lightnessRange`: numeric/derived
- `perceptual.chromaRange`: numeric/derived
- `perceptual.luminanceContrast`: measured by semantic pair
- `perceptual.hueSeparation`: measured/derived
- `perceptual.figureGroundSalience`: low | moderate | high | unknown
- `perceptual.harmony`: measured | predicted | unknown
- `perceptual.activationAssociation`: low | moderate | high | mixed | unknown
- `perceptual.valenceAssociation`: positive | neutral | negative | mixed | unknown
- `perceptual.weightAssociation`: light | neutral | heavy | mixed | unknown
- `perceptual.confidence`: high | moderate | low | unknown
- `perceptual.population`: population/context to which evidence applies
- `perceptual.evidence`: claim/source identifiers
- `perceptual.caveats`: contextual limitations

Do not collapse these into a single mood score.

## Combination families worth indexing

These are descriptive families to test, not prescriptions:

1. **Light + cool + moderate chroma** — candidate for positive/lower-activation perception.
2. **Light + warm + higher chroma** — candidate for positive/higher-activation perception.
3. **Dark + warm/high-chroma accent** — candidate for high activation and strong figure salience; text/accessibility must be evaluated separately.
4. **Dark + cool** — potentially subdued/low-lightness but hue associations can pull positive; demonstrates why valence cannot be inferred from hue alone.
5. **Near-hue / analogous** — candidate for greater pair harmony; requires sufficient lightness contrast for hierarchy and readability.
6. **Large hue-separation figure/ground** — candidate for increased figure salience; not necessarily greater pair harmony.
7. **Achromatic + chromatic accent** — useful experimental control for isolating accent effects and salience.
8. **Low-chroma / muted** — candidate for lower activation; hierarchy must come from lightness, typography, spacing, and structure.
9. **High-chroma multi-accent** — candidate for higher activation and attentional competition; test complexity and visual search.
10. **Accessibility-first high-luminance-contrast** — perception goal is discriminability first; emotional association remains secondary.

## Catalog policy

A THM record MUST distinguish:
- measured properties,
- experimentally supported associations,
- inferred associations,
- designer intent,
- user-study observations.

A theme MUST NOT claim to make users calm, trusting, productive, creative, hungry, or similar without direct context-specific evidence.

## Research sources

Primary evidence baseline:
- Elliot & Maier (2014), Annual Review of Psychology, color psychology review.
- Jonauskaite et al. (2020), Psychological Science, 4,598 participants across 30 nations.
- Jonauskaite et al. systematic review (2025), 132 studies, 42,266 participants, 64 countries.
- Schloss & Palmer (2011), Attention, Perception, & Psychophysics, aesthetic response to color combinations.
- Ou et al. (2004), Color Research & Application, emotion/preference for two-color combinations.
- Ou & Luo (2006), Color Research & Application, two-color harmony model.
- Forni et al. (2026), iScience, data-driven color pairing preferences and natural hue statistics.
- Tan, Echevarria & Gingold (2025), IEEE TVCG, perceptual studies of palette harmonization.
- W3C WCAG 2.2 guidance for use of color and luminance contrast.

This baseline should be extended with controlled theme-level experiments rather than converted directly into universal design rules.
