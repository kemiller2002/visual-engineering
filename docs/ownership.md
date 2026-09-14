# File ownership model

Every path the tool manages carries exactly one ownership classification. The classification
decides what the tool is allowed to do to it.

| Ownership | Definition | On `init` and `upgrade` |
| --- | --- | --- |
| `tool-owned` | Controlled by the tool | Replaced according to explicit version rules |
| `generated` | Derived from authoritative inputs | Regenerated whenever the inputs change |
| `user-owned` | Controlled by the repository | Created once if absent, never modified afterwards |
| `shared` | Managed by both | Only a delimited region or a set of reserved keys is rewritten |

The classification of every managed path is recorded in `.echelon/visual-engineering.json`
alongside the content hash of what the tool last wrote. That hash is how a later run tells
"the tool wrote this" apart from "somebody edited this".

## Current classification

| Path | Ownership | Notes |
| --- | --- | --- |
| `.visual-engineering/AGENT-INSTRUCTIONS.md` | tool-owned | Maintained in `agent-context/` upstream |
| `.visual-engineering/UI-FOUNDATIONS.md` | tool-owned | |
| `.visual-engineering/UI-DECISION-CHECKLIST.md` | tool-owned | |
| `.visual-engineering/UI-ANTI-PATTERNS.md` | tool-owned | |
| `.visual-engineering/RESEARCH-INDEX.md` | generated | Generated from the research catalog |
| `.visual-engineering/sources.json` | generated | |
| `.visual-engineering/context.json` | generated | |
| `.echelon/visual-engineering.json` | tool-owned | The installation manifest itself |
| `.echelon/visual-engineering.config.json` | shared | The tool owns five reserved keys |
| `.gitignore` | shared | The tool owns one `# BEGIN/# END echelon:visual-engineering` region |
| `AGENTS.md` | shared | The tool owns one `<!-- BEGIN/END echelon:visual-engineering -->` region |

`tool-owned` and `generated` behave identically when the tool writes them. They are
distinguished because they differ in provenance, and therefore in what it means for them to be
out of date: a `generated` file is stale when its inputs moved on, a `tool-owned` file is stale
when the release moved on.

## Shared files

Shared files are never rewritten wholesale.

- In `.gitignore` and `AGENTS.md` the tool owns only the text between its markers. Everything
  before and after is copied through byte for byte. If the region is absent, it is appended.
- In `.echelon/visual-engineering.config.json` the tool owns `schemaVersion`, `tool`,
  `configurationVersion`, `contextDirectory` and `integrations`. Every other key is preserved
  in the order you wrote it. The file is compared after JSON normalization, so reformatting is
  not mistaken for a change.

Editing inside a managed region is detected and blocks the run rather than being silently
overwritten. Move such edits outside the markers.

## Files created by `init` are not tool-owned forever

`init` creates `.echelon/visual-engineering.config.json` with defaults, but that file is
classified `shared` and not `tool-owned`: you are expected to edit its values, and those edits
survive every later run. `.gitignore` and `AGENTS.md` are likewise yours except for one region.

The context files under `.visual-engineering/` are the opposite: they are entirely the tool's,
and are expected to be gitignored. The managed `.gitignore` region does that for you.
