# @echelon-foundry/visual-engineering

Echelon Foundry Visual Engineering repository initialization, verification, diagnostics, and upgrade tooling.

It installs the Visual Engineering UI research context into a repository so that people and
implementation agents design, build and review interfaces from current evidence instead of
from copied snapshots. The tool owns the whole lifecycle of that installation: it detects the
current state, installs, verifies, diagnoses and upgrades it, and records what it manages.

## Quick start

```bash
# Bring the repository into a valid installed state
npx @echelon-foundry/visual-engineering init

# Confirm what is installed
npx @echelon-foundry/visual-engineering status

# Validate the installation
npx @echelon-foundry/visual-engineering verify
```

`init` is safe to run repeatedly. Running it a second time when nothing needs to change reports
no changes and rewrites nothing.

## What it installs

| Path | Ownership | Purpose |
| --- | --- | --- |
| `.visual-engineering/AGENT-INSTRUCTIONS.md` | tool-owned | How an agent should use the briefing |
| `.visual-engineering/UI-FOUNDATIONS.md` | tool-owned | Evidence based UI foundations |
| `.visual-engineering/UI-DECISION-CHECKLIST.md` | tool-owned | Decision checklist |
| `.visual-engineering/UI-ANTI-PATTERNS.md` | tool-owned | Anti patterns |
| `.visual-engineering/RESEARCH-INDEX.md` | generated | Source linked index of current research |
| `.visual-engineering/sources.json` | generated | Provenance records |
| `.visual-engineering/context.json` | generated | Context manifest and integrity metadata |
| `.echelon/visual-engineering.json` | tool-owned | Installation manifest |
| `.echelon/visual-engineering.config.json` | shared | Repository configuration |
| `.gitignore` | shared | Managed region ignoring the context directory |
| `AGENTS.md` | shared | Managed region registering the briefing with agents |

Shared files are only partly the tool's: it owns a region delimited by
`echelon:visual-engineering` markers, or a set of reserved JSON keys, and copies everything else
through untouched. See [docs/ownership.md](docs/ownership.md).

## Supported environments

Binaries for six platforms ship in the package, and the launcher selects the right one:

| Platform | Architectures |
| --- | --- |
| Linux | x64, arm64 |
| macOS | x64 (Intel), arm64 (Apple silicon) |
| Windows | x64, arm64 |

Any other platform fails immediately with exit code 7.

### Prerequisites

- Node.js 20 or newer, only to launch the packaged executable.
- No .NET installation is required: the executable is self contained.
- No network access is required after the package is installed. The research context is
  carried inside the package.

## Commands

| Command | Changes the repository | Purpose |
| --- | --- | --- |
| `init` | yes | Bring the repository into a valid installed state |
| `status` | no | Report installation state |
| `verify` | no | Validate that the installation is correct |
| `upgrade` | yes | Move an existing installation to this release |
| `doctor` | no | Explain what is wrong and how to fix it |

Global options: `--help`, `--version`, `--repo <path>`, `--json`, `--verbose`.
`init` and `upgrade` also accept `--dry-run`, `--check` and `--force`.
`verify` and `doctor` also accept `--strict`.

Full reference: [docs/cli.md](docs/cli.md).

### init

```bash
npx @echelon-foundry/visual-engineering init
npx @echelon-foundry/visual-engineering init --dry-run
npx @echelon-foundry/visual-engineering init --check
```

`init` means *bring this repository into a valid installed state*, not *copy some files*. It
inspects the repository, determines the current installation state, calculates the changes,
detects conflicts, applies the changes, writes the installation manifest, and verifies the
result.

It may create or update the tool-owned, generated and shared paths listed above. It never
modifies user-owned files, never edits content outside a managed region, and refuses to replace
tool maintained content that was modified locally unless `--force` is given. See
[docs/installation.md](docs/installation.md).

### status

```bash
npx @echelon-foundry/visual-engineering status
npx @echelon-foundry/visual-engineering status --json
```

Reports the tool name, CLI version, installed version, configuration version, installation
state, artifact status, integration status, verification status and any available upgrade.
`status` never modifies the repository.

### verify

```bash
npx @echelon-foundry/visual-engineering verify
npx @echelon-foundry/visual-engineering verify --strict
```

Default mode asks whether the installation is internally consistent: every required file
present, nothing modified locally, the packaged context intact. An installation that is
consistent but older than this release still passes.

`--strict` additionally requires the installation to be exactly what this release would
produce: no stale files and no pending upgrade.

Exit code `0` means valid; a non-zero code means invalid.

### upgrade

```bash
npx @echelon-foundry/visual-engineering upgrade
npx @echelon-foundry/visual-engineering upgrade --dry-run
```

Upgrades run one configuration version at a time (`1 -> 2 -> 3`), each with its own
preconditions. The upgrade stops at the first precondition failure and reports exactly what
happened rather than leaving the repository half migrated. See
[docs/upgrading.md](docs/upgrading.md).

### doctor

```bash
npx @echelon-foundry/visual-engineering doctor
npx @echelon-foundry/visual-engineering doctor --json
```

`doctor` explains *why* something is wrong and how to fix it. Findings are classified as
`error`, `warning` or `information`; not every deviation is an error.

### Dry run

`init --dry-run` and `upgrade --dry-run` inspect the repository, calculate the full plan,
validate it, report what would change, and write nothing. Combine with `--json` for automation.

## Machine readable output

Every command accepts `--json`. With `--json`, stdout carries a single JSON document and
nothing else; diagnostics go to stderr. Every document shares one envelope:

```json
{
  "schemaVersion": 1,
  "tool": "visual-engineering",
  "package": "@echelon-foundry/visual-engineering",
  "command": "status",
  "cliVersion": "1.0.0",
  "exitCode": 0
}
```

Schemas are versioned: a breaking change increments `schemaVersion`. Documented in
[docs/cli.md](docs/cli.md#json-output).

## Exit codes

| Code | Meaning |
| --- | --- |
| 0 | Success |
| 1 | Internal failure |
| 2 | Invalid arguments |
| 3 | Verification failed |
| 4 | Changes are required (`--check`) |
| 5 | Installation blocked (conflict or failed migration precondition) |
| 6 | Environment or packaging failure |
| 7 | Unsupported platform |

## CI usage

```yaml
- name: Verify Visual Engineering context
  run: npx --yes @echelon-foundry/visual-engineering@latest verify --strict
```

`verify --strict` exits non-zero when the installation is missing, damaged, or behind the
release being used, which makes it a drift gate. To fail a build when `init` would change
something without writing anything:

```bash
npx @echelon-foundry/visual-engineering init --check   # exit 4 when changes are required
```

## Agent usage

Every command is non-interactive and never prompts. For agents and scripts:

```bash
npx @echelon-foundry/visual-engineering status --json
npx @echelon-foundry/visual-engineering init --dry-run --json
npx @echelon-foundry/visual-engineering doctor --json
```

Destructive replacement of locally modified content is never assumed: it requires the explicit
`--force` flag. Parse `exitCode` from the JSON document or read the process exit code; both
carry the same value.

## Configuration and installation manifest

- Configuration: `.echelon/visual-engineering.config.json` (shared; the tool owns
  `schemaVersion`, `tool`, `configurationVersion`, `contextDirectory` and `integrations`, and
  preserves any other key you add).
- Installation manifest: `.echelon/visual-engineering.json` (tool-owned). It records the
  installed version, configuration version, context version and the ownership and content hash
  of every managed path. It contains no secrets, no machine specific values, and no timestamps.

`.echelon/` is the shared Echelon Foundry root. Each Echelon tool owns one manifest inside it
and they coexist without conflicting.

## Compatibility

This package is additive. The previous distribution channels are unchanged and still supported:

- **Recommended:** `npx @echelon-foundry/visual-engineering <command>`.
- **Supported (legacy compatibility):** the `@kemiller2002/visual-engineering-context` npm
  package (`ve-context sync|verify|status|show`), the GitHub Pages context feed, and the
  immutable `ui-context-v*` GitHub Releases. See
  [packages/visual-engineering-context/README.md](packages/visual-engineering-context/README.md)
  and [agent-context/README.md](agent-context/README.md).

A repository installed by `ve-context sync` is detected as configuration version 1 and is
migrated in place by `upgrade`, preserving its files.

## Development

```bash
dotnet restore VisualEngineering.sln
dotnet build VisualEngineering.sln -c Release
dotnet test VisualEngineering.sln
```

The implementation is F#. `src/VisualEngineering.Core` owns every lifecycle decision and is
callable without simulating command line input; `src/VisualEngineering.Cli` is a thin adapter.
The Node launcher contains no lifecycle logic. See
[docs/development.md](docs/development.md).

### Testing

```bash
dotnet test VisualEngineering.sln          # unit and lifecycle tests
npm run tool:test-package                  # tests the actual packed npm artifact
```

### Packaging

```bash
npm ci
npm run research:build                     # generate the research catalog
npm run context:build                      # generate the context payload
npm run tool:build                         # publish the F# CLI and stage the npm package
npm run tool:pack                          # npm pack --dry-run, review the contents
npm run tool:test-package                  # pack, install and exercise the real archive
```

### Release

Releases are produced by `.github/workflows/publish-visual-engineering-tool.yml`, which builds,
tests, packs, exercises the packed archive against temporary repositories on Linux, macOS and
Windows, and only then publishes. See [docs/releasing.md](docs/releasing.md).

## Troubleshooting

| Symptom | Cause | Fix |
| --- | --- | --- |
| `unsupported platform` (exit 7) | No binary for this platform/architecture | Use a supported platform from the table above |
| `this package does not contain an executable for ...` (exit 6) | Incomplete install | Reinstall the package |
| `... was modified locally` (exit 5) | Tool maintained content was edited | Restore the file, or rerun with `--force` |
| `the managed '...' region ... was edited locally` (exit 5) | Edits inside the managed markers | Move edits outside the markers, or rerun with `--force` |
| `verify` fails only with `--strict` | The installation is behind this release | `npx @echelon-foundry/visual-engineering upgrade` |

Run `npx @echelon-foundry/visual-engineering doctor --verbose` for an explanation of any state.

## Documentation

- [docs/installation.md](docs/installation.md) — initialization semantics in detail
- [docs/cli.md](docs/cli.md) — command, option, JSON and exit code reference
- [docs/upgrading.md](docs/upgrading.md) — migration model and guarantees
- [docs/ownership.md](docs/ownership.md) — file ownership model
- [docs/development.md](docs/development.md) — architecture and contributor workflow
- [docs/releasing.md](docs/releasing.md) — release and publishing process

## About this repository

This repository is also the Visual Engineering research knowledge base: the canonical research
lives in `content/`, the published site is generated by
[research-publisher](https://github.com/kemiller2002/research-publisher), and the operational
briefing in `agent-context/` is the human maintained source of the context this package ships.

## License

MIT. See [LICENSE](LICENSE).
