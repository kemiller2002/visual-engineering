# @echelon-foundry/visual-engineering

Echelon Foundry Visual Engineering repository initialization, verification, diagnostics, and upgrade tooling.

It installs the Visual Engineering UI research context into a repository so that people and
implementation agents design, build and review interfaces from current evidence instead of
from copied snapshots. The tool owns the whole lifecycle of that installation: it detects the
current state, installs, verifies, diagnoses and upgrades it, and records what it manages.

## First-class application polish

Visual Engineering treats application polish as an engineering discipline, not a final cosmetic pass. A polished application must provide evidence across visual precision, interaction, motion, state completeness, forms, feedback, content, responsiveness, accessibility, performance perception, resilience, data integrity, navigation, environment behavior, security UX, and fit-and-finish.

The normative standard is [framework/standards/APPLICATION-POLISH.md](framework/standards/APPLICATION-POLISH.md), the review procedure is [framework/protocols/APPLICATION-POLISH-REVIEW.md](framework/protocols/APPLICATION-POLISH-REVIEW.md), and the active research program is [research/frontier/application-polish-engineering.md](research/frontier/application-polish-engineering.md).

A zero-finding review is not evidence of polish by itself. Coverage of states, seams, adversarial fixtures, environments, exceptions, and unknowns is part of the claim.

## Requirements

- **Node.js 20 or newer.** Node is only used to start the packaged executable.
- **Nothing else.** The executable is self contained: no .NET runtime, no compiler, no global
  tooling. After installation nothing is downloaded — the research context travels inside the
  package — so it works on an offline or air-gapped machine.
- Linux, macOS or Windows on x64 or arm64. See
  [supported environments](#supported-environments).

## Installation

Pick whichever fits how you work. All three give you the same `visual-engineering` command.

### Run it without installing

```bash
npx @echelon-foundry/visual-engineering init
```

`npx` downloads the package on first use and caches it, so later runs start immediately. Best
for trying it out and for one-off runs. To pin a version rather than following the latest
release:

```bash
npx @echelon-foundry/visual-engineering@1.0.0 init
```

### Add it to a project (recommended for teams and CI)

```bash
npm install --save-dev @echelon-foundry/visual-engineering
```

Then run it through your package manager, which uses the exact version in your lockfile:

```bash
npx visual-engineering status
```

This is the reproducible option: everyone on the project, and every CI run, uses the same
version until you deliberately update it. Add a script if you run it often:

```json
{
  "scripts": {
    "ve:verify": "visual-engineering verify --strict"
  }
}
```

### Install it globally

```bash
npm install --global @echelon-foundry/visual-engineering
visual-engineering --version
```

Best if you work across many repositories. The command is then on your `PATH` everywhere.

## Quick start

From the root of the repository you want to set up:

```bash
npx @echelon-foundry/visual-engineering init
```

```text
Applied 13 change(s).
```

Confirm what you got:

```bash
npx @echelon-foundry/visual-engineering status
```

```text
Visual Engineering

  CLI version:           1.0.0
  Installed version:     1.0.0
  Configuration:         version 3 (valid)
  Context:               1.0.0
  Installation state:    installed
  Required artifacts:    valid
  Integration:           valid
  Verification:          passed
  Upgrade:               none

  Context directory:     .visual-engineering
  Manifest:              .echelon/visual-engineering.json
  Research documents:    97
```

Check it is intact at any time:

```bash
npx @echelon-foundry/visual-engineering verify
```

```text
Verification passed.
```

`init` is safe to run repeatedly. A second run when nothing needs to change reports
`Visual Engineering is already up to date. No changes required.` and rewrites nothing — not one
file, not one timestamp.

Want to see what it would do before it does anything?

```bash
npx @echelon-foundry/visual-engineering init --dry-run
```

### What just happened

`init` installed the Visual Engineering UI research briefing into `.visual-engineering/`,
recorded what it manages in `.echelon/visual-engineering.json`, added a managed block to your
`.gitignore` so the context is not committed, and registered a managed block in `AGENTS.md`
telling coding agents to read the briefing before doing UI work. Your own content in those two
files is untouched. The next section lists every path.

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

| Platform | Architectures |
| --- | --- |
| Linux | x64, arm64 |
| macOS | x64 (Intel), arm64 (Apple silicon) |
| Windows | x64, arm64 |

Any other platform fails immediately with exit code 7.

Each platform's executable ships in its own package, declared as an optional dependency of this
one and marked with the `os` and `cpu` it runs on. npm installs only the one your machine can
run, so an install downloads about 7 MB rather than all six executables:

| Package | Download |
| --- | --- |
| `@echelon-foundry/visual-engineering` | under 1 MB (launcher, research context, docs) |
| `@echelon-foundry/visual-engineering-<platform>` | about 7 MB (one executable) |

The six platform packages are `-linux-x64`, `-linux-arm64`, `-osx-x64`, `-osx-arm64`,
`-win-x64` and `-win-arm64`. You never name them directly; npm resolves the right one. If you
install with `--omit=optional`, add the one you need explicitly.

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
npm run tool:build                         # publish the F# CLI and stage all seven packages
npm run tool:build -- --rid linux-x64      # or stage one platform, much faster
npm run tool:pack                          # npm pack --dry-run, review the root contents
npm run tool:test-package                  # pack, install and exercise the real archives
```

### Release

Releases are produced by `.github/workflows/publish-visual-engineering-tool.yml`, which builds,
tests, packs, exercises the packed archive against temporary repositories on Linux, macOS and
Windows, and only then publishes. See [docs/releasing.md](docs/releasing.md).

## Troubleshooting

| Symptom | Cause | Fix |
| --- | --- | --- |
| `unsupported platform` (exit 7) | No binary for this platform/architecture | Use a supported platform from the table above |
| `the executable for ... is missing` (exit 6) | The platform package was not installed, usually from `--omit=optional` | Reinstall, or add `@echelon-foundry/visual-engineering-<platform>` explicitly |
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
- [CHANGELOG.md](CHANGELOG.md) — what changed in each release

## About this repository

This repository is also the Visual Engineering research knowledge base: the canonical research
lives in `content/`, the published site is generated by
[research-publisher](https://github.com/kemiller2002/research-publisher), and the operational
briefing in `agent-context/` is the human maintained source of the context this package ships.

## License

MIT. See [LICENSE](LICENSE).
