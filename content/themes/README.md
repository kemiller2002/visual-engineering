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

## Catalog integrity

Every change to the catalog is checked by the dependency-free F# validator in [`tools/theme-catalog-validate`](../../tools/theme-catalog-validate/README.md). CI runs it before any perceptual measurement, and any integrity error fails the build. The specimen is authoritative; `index.json` is a retrieval projection of the specimens and must agree with them.

The validator enforces:

1. **Identity.** Every specimen ID and every index ID is a unique `THM-NNNN`. A specimen file is named after the ID it authors (`specimens/THM-0017.json` authors `THM-0017`).
2. **Index completeness.** Every index entry resolves to a specimen, and every specimen has exactly one index entry.
3. **Paths.** An index entry's `path` and a specimen's optional `specimen.path` equal the specimen's actual repository-relative path, for example `content/themes/specimens/THM-0017.json`. Equivalent spellings such as `./content/...` are errors, not normalized.
4. **Index metadata.** The index `name`, `status`, `mode` and `contexts` (compared as a set) equal the specimen's. Index `temperature` equals the specimen's `facets.temperature` retrieval facet. An entry may not omit `contexts`, or omit `temperature` when the specimen authors the facet; a missing index facet silently hides the theme from retrieval. It is not compared with `perception.temperature`, which is a separately evidenced perceptual descriptor that may be `unknown`.
5. **Semantic token references.** Every semantic token, required or optional, names a key in the same specimen's palette.
6. **Next ID.** `nextId` is the highest existing numeric ID plus one. Existing IDs include specimens, specimen filenames and index entries. IDs need not be contiguous, so gaps are never refilled.
7. **Exact palette fingerprint.** Each stored `fingerprint` equals the canonical fingerprint of its palette (below).
8. **Theme references.** A `provenance.source` value that is exactly a `THM-NNNN` identifier must name an existing theme. URLs, document paths and prose that merely mention an ID are not references. `replaces`, when non-null, must be a `THM-NNNN` naming an existing theme.
9. **Self references.** A theme may not replace itself, be derived from itself, or be part of a `replaces` cycle.

Malformed JSON, and required values that are missing, null, empty or of the wrong type, fail validation. They are never coerced into defaults.

### Exact palette fingerprint

The canonical fingerprint is an exact identity of the palette's colors:

1. Take every palette value, ignoring its role-free key and every semantic role.
2. Normalize it by upper-casing the hex digits and writing a fully opaque `#RRGGBBFF` as `#RRGGBB`. Any other alpha is kept, because it is a different color.
3. Sort the normalized values ordinally and join them with `|`.

For example, `{ "ink": "#20211f", "canvas": "#F7F7F5" }` has the fingerprint `#20211F|#F7F7F5`. Two palettes with the same fingerprint have exactly the same colors.

### Palette duplicates versus theme duplicates

The same palette may support several `THM` records when their semantic mappings materially differ. Integrity validation therefore separates two findings:

- **Exact palette duplicate** (`duplicate-palette`, a notice). Different theme IDs share a canonical fingerprint. The finding is reported deterministically but is permitted doctrine and does not fail validation.
- **Exact theme duplicate** (`duplicate-theme-definition`, an error). Different non-deprecated theme IDs share a fingerprint, and every semantic token resolves to the same color in each. Resolved colors are compared, not palette key names, so renaming palette keys does not make a duplicate distinct. To keep such a record, deprecate the redundant one and point its successor's `replaces` at it.

### Exact identity is not perceptual similarity

Fingerprint equality is string identity over normalized hex values. Two palettes that differ by one hex digit have different fingerprints however similar they look. Perceptual near-duplicate detection, such as OKLab/OKLCH distance thresholds, is **not** an integrity failure in this layer. It belongs to perceptual measurement, which runs only after the catalog is structurally valid.

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
