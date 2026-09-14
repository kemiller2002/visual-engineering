# CLI reference

```text
npx @echelon-foundry/visual-engineering <command> [options]
visual-engineering <command> [options]
```

The executable installed by the package is `visual-engineering`. It is the only executable the
package exposes.

## Global options

| Option | Meaning |
| --- | --- |
| `-h`, `--help` | Show help for the program, or for a command when given after it |
| `-V`, `--version` | Print the CLI version and exit |
| `--repo <path>` | Repository to operate on. Defaults to the working directory |
| `--json` | Emit one JSON document on stdout and nothing else |
| `--verbose` | Include per file detail in human readable output |

`visual-engineering help <command>` and `visual-engineering <command> --help` are equivalent.

An option a command does not accept is rejected with exit code 2 rather than ignored.

## Commands

### `init`

Brings the repository into a valid installed state. Idempotent.

| Option | Meaning |
| --- | --- |
| `--dry-run` | Calculate and report the plan, write nothing |
| `--check` | Write nothing; exit 4 when changes are required |
| `--force` | Replace locally modified tool maintained content |

### `status`

Reports installation state. Read only.

Reported fields: tool name, CLI version, installed version, configuration version, installed
and packaged context versions, installation state, artifact status, integration status,
verification status, available upgrade, context directory, manifest path, research document
count.

### `verify`

Validates that the capability is correctly installed. Read only.

| Option | Meaning |
| --- | --- |
| `--strict` | Also fail when the installation is behind this release |

Checks performed:

| Check | Mode | Fails when |
| --- | --- | --- |
| `installation` | default | nothing is installed |
| `manifest` | default | `.echelon/visual-engineering.json` is unreadable or uses an unsupported schema |
| `configuration` | default | `.echelon/visual-engineering.config.json` is unreadable |
| `required-files` | default | a required file is missing |
| `file-integrity` | default | tool maintained content was modified locally |
| `packaged-context` | default | the context inside the package fails its integrity check |
| `up-to-date` | strict | a managed file is behind the packaged context |
| `version-compatibility` | strict | an upgrade is pending, or the installation is invalid |

### `upgrade`

Moves an existing installation to the version this release provides.

| Option | Meaning |
| --- | --- |
| `--dry-run` | Calculate and report the plan, write nothing |
| `--check` | Write nothing; exit 4 when an upgrade is required |
| `--force` | Replace locally modified tool maintained content |

### `doctor`

Diagnoses problems and explains them. Read only.

| Option | Meaning |
| --- | --- |
| `--strict` | Treat warnings as failures |

Findings carry a `severity` (`error`, `warning`, `information`), a stable `code`, a `title`, a
`detail` and, where one exists, a `remedy`. Codes in this release:

`packaged-context-corrupt`, `not-a-git-repository`, `repository-not-writable`, `not-installed`,
`legacy-installation`, `upgrade-available`, `invalid-installation`, `installed`,
`integration-missing`, `managed-region-modified`, `file-missing`, `file-unreadable`,
`file-modified`, `file-stale`, `manifest-tool-mismatch`.

## Exit codes

| Code | Name | Meaning |
| --- | --- | --- |
| 0 | success | The command completed and the resulting state is valid |
| 1 | internal failure | An unexpected error, or a plan that was only partially applied |
| 2 | invalid arguments | Unknown command, unknown option, or an option the command rejects |
| 3 | verification failed | `verify` failed, `doctor` found a failing finding, or `status` found an invalid installation |
| 4 | changes required | `--check` found that changes are required |
| 5 | installation blocked | A conflict or a failed migration precondition stopped the plan |
| 6 | environment failure | The packaged context is missing or corrupt, or the repository cannot be opened |
| 7 | unsupported platform | No packaged executable for this platform and architecture |

Exit code 7 is produced by the Node launcher before the executable starts. All other codes come
from the F# implementation.

## JSON output

With `--json`, stdout carries exactly one JSON document and nothing else. Human readable
decoration is never mixed in. Errors that prevent a command from running are also emitted as
JSON when `--json` was requested.

### Envelope

Present on every document:

| Field | Type | Meaning |
| --- | --- | --- |
| `schemaVersion` | integer | Output schema version. Currently `1` |
| `tool` | string | `"visual-engineering"` |
| `package` | string | `"@echelon-foundry/visual-engineering"` |
| `command` | string | `init`, `status`, `verify`, `upgrade` or `doctor` |
| `cliVersion` | string | Version of the CLI, identical to the npm package version |
| `exitCode` | integer | The process exit code |

A breaking change to any schema below increments `schemaVersion`.

### `status --json`

Adds `displayName`, `installedVersion`, `configurationVersion`, `installedContextVersion`,
`packagedContextVersion`, `sourceCommit`, `researchDocuments`, `contextDirectory`,
`manifestPath`, `configurationPath`, `state`, `configurationValid`, `artifactsValid`,
`integrationsValid`, `verification`, `availableUpgrade`.

`state.status` is one of `not-installed`, `installed`, `upgrade-required`, `invalid`.

### `verify --json`

Adds `verification` and `problems`. `verification.checks[]` carries `name`, `passed`,
`strictOnly` and `detail` for every check, including checks that do not apply to the active
mode, so a consumer can see the full picture without rerunning.

### `doctor --json`

Adds `healthy` and `findings[]` with `severity`, `code`, `title`, `detail` and `remedy`.

### `init --json` and `upgrade --json`

Add `dryRun`, `plan`, `execution` and `verification`.

`plan` carries `changeCount`, `changes[]` (`kind`, `path`, `description`), `blockers[]`,
`migrations[]` (`from`, `to`) and `preserved[]` (`path`, `reason`).

`changes[].kind` is one of `create-directory`, `create-file`, `update-managed-file`,
`update-configuration`, `register-integration`, `run-migration`.

`execution.applied` is `false` for a dry run (`reason: "dry-run"`), for a blocked plan
(`reason: "blocked"`), and for a plan that failed part way through
(`reason: "partially-applied"`, with `appliedCount`, `failedChange` and `detail`).

## Examples

```bash
npx @echelon-foundry/visual-engineering init
npx @echelon-foundry/visual-engineering init --dry-run --json
npx @echelon-foundry/visual-engineering init --check
npx @echelon-foundry/visual-engineering status --json
npx @echelon-foundry/visual-engineering verify --strict
npx @echelon-foundry/visual-engineering upgrade --dry-run
npx @echelon-foundry/visual-engineering doctor --json
npx @echelon-foundry/visual-engineering --version
npx @echelon-foundry/visual-engineering init --help
```
