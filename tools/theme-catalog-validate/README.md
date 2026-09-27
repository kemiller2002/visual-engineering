# Theme Catalog Validate

Dependency-free F# structural integrity validator for the Visual Engineering theme catalog (`content/themes`). The invariants and their doctrine are documented in [`content/themes/README.md`](../../content/themes/README.md#catalog-integrity).

It checks identity, index/specimen consistency, exact palette fingerprints, semantic token references, and theme references. It does **not** measure perceptual similarity; that belongs to [`theme-measure`](../theme-measure/README.md) and runs after integrity passes.

## Run

```bash
dotnet run --project tools/theme-catalog-validate -- --repository-root .
dotnet run --project tools/theme-catalog-validate -- --repository-root . --json
```

Options:

- `--repository-root DIR`: the repository that index and specimen paths are relative to. Defaults to `.`.
- `--catalog DIR`: the catalog directory relative to the root. Defaults to `content/themes`.
- `--json`: emit one machine-readable report instead of text lines.

Exit codes: `0` when there are no errors (notices allowed), `1` when there is at least one error, `2` for invalid arguments.

## Diagnostics

Each finding carries a severity, a stable code, the theme ID and path when known, and a human-readable message. Key on the code, never on the message. Output is sorted by theme ID, code, path and message, so identical input always produces identical output.

| Code | Severity | Meaning |
|---|---|---|
| `malformed-json` | error | A file is not valid JSON. |
| `invalid-field` | error | A required value is missing, null, empty, or of the wrong type, or an enumerated value is unknown. |
| `index-missing` | error | `index.json` does not exist. |
| `invalid-theme-id` | error | An authored ID is not `THM-NNNN`. |
| `invalid-specimen-filename` | error | A specimen file is not named `THM-NNNN.json`. |
| `invalid-palette-color` | error | A palette value is not `#RRGGBB` or `#RRGGBBAA`. |
| `duplicate-theme-id` | error | More than one specimen authors the same ID. |
| `duplicate-index-id` | error | The index lists an ID more than once. |
| `filename-id-mismatch` | error | A specimen's filename and authored ID differ. |
| `index-specimen-missing` | error | An index entry has no specimen. |
| `specimen-index-missing` | error | A specimen has no index entry. |
| `index-path-mismatch` | error | An index `path` is not the specimen's actual path. |
| `index-metadata-mismatch` | error | Index `name`, `status`, `mode`, `temperature` or `contexts` disagree with the specimen, or the index omits a facet the specimen authors. |
| `semantic-token-missing-palette-key` | error | A semantic token names a palette key that does not exist. |
| `next-id-mismatch` | error | `nextId` is not the highest existing ID + 1. |
| `fingerprint-mismatch` | error | A stored fingerprint is not the canonical palette fingerprint. |
| `duplicate-palette` | notice | Different theme IDs share an exact palette. |
| `duplicate-theme-definition` | error | Different non-deprecated theme IDs share an exact palette and resolved semantic mapping. |
| `invalid-theme-reference` | error | A THM reference names a missing theme, or `replaces` is not a THM ID. |
| `self-reference` | error | A theme replaces, or is derived from, itself. |
| `replaces-cycle` | error | `replaces` relationships form a cycle. |
| `specimen-path-mismatch` | error | An authored `specimen.path` is not the specimen's actual path. |

Specimens that fail structural parsing are reported once and excluded from cross-file checks. Their filename ID still counts as present, so an unusable specimen is not also reported as missing from the index.

## Layout

- `tools/theme-catalog-lib`: domain types (`Domain.fs`), exact fingerprints (`Fingerprint.fs`), JSON parsing (`CatalogParsing.fs`), pure cross-file validation (`CatalogIntegrity.fs`), and the file-system boundary (`CatalogFiles.fs`).
- `tools/theme-catalog-validate`: this command-line adapter.
- `tools/theme-catalog-tests`: dependency-free tests that validate miniature temporary catalogs through the same entry point the command uses, and validate the real catalog.

```bash
dotnet run --project tools/theme-catalog-tests
```

## Design constraints

- no external NuGet packages;
- parsing, validation, and output are separate; validation is a pure function of the files read;
- malformed input fails, never coerced;
- exact identity only. Perceptual near-duplicate thresholds are intentionally out of scope.
