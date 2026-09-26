# Theme Specimen Catalog

This catalog records reusable color combinations as evidence-bearing theme specimens.

## Identity

Every theme receives a permanent `THM-NNNN` identifier. Names and mappings may evolve; IDs do not. A theme is not identified by its display name.

## Model

A specimen deliberately separates:

1. **Palette** — normalized source colors with no UI meaning.
2. **Semantic mapping** — assignment of palette colors to background, surface, text, accent, border, and status roles.
3. **Evaluation** — measured contrast and other evidence for the mapping in a stated context.
4. **Specimen** — representative UI/document surfaces used to inspect the mapping.
5. **Index metadata** — tags and facets used to retrieve themes by properties and intended use.

The same palette may therefore support multiple `THM` records when its semantic mapping materially differs.

## Required fields

Each specimen records a stable ID, name, status, mode, palette, semantic tokens, intended contexts, tags, provenance, accessibility evaluation, specimen coverage, and a normalized fingerprint.

Fingerprints are derived from normalized palette values sorted independently of semantic role. They are duplicate-detection aids, not identity. Near-duplicate detection should use a perceptual color-difference metric rather than string similarity.

## Retrieval facets

Index at minimum by mode, temperature, contrast character, chroma character, intended context, accessibility status, provenance, and tags. Consumers should be able to ask questions such as `dark + warm + high-contrast + dashboard`.

## Accessibility

A theme is not declared accessible merely because its palette contains high-contrast colors. Contrast is evaluated on actual semantic foreground/background pairs. Store measured ratios and the criterion used. Color must not be the sole carrier of critical state.

## Specimen coverage

Before a theme is promoted beyond draft, inspect at least: body/headings, links, primary/secondary buttons, inputs and focus states, cards/surfaces, tables, status messaging, and disabled states. Document/print themes should additionally cover page background, body text, headings, rules, tables, figures/callouts, and notes.

## Lifecycle

`draft -> evaluated -> accepted -> deprecated`

Draft themes may be captured quickly. Evaluated themes have measurements. Accepted themes have sufficient evidence for reuse. Deprecation preserves the ID and points to a replacement when one exists.

## Integration direction

The catalog is a Visual Engineering evidence asset. Forma may consume semantic UI mappings; Folio may consume document/print mappings; Limen applications may select accepted mappings at runtime. Consumers should reference the `THM` ID plus catalog/schema version rather than copying unnamed palettes.
