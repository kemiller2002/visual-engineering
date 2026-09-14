# Installation

> Applies to `@echelon-foundry/visual-engineering`. For the legacy `ve-context` mechanism see
> [Legacy compatibility](#legacy-compatibility).

## What `init` means

```bash
npx @echelon-foundry/visual-engineering init
```

`init` brings the current repository into a valid installed state for this capability. It is not
a file copy. Each run performs the same sequence:

1. detect the repository
2. inspect it
3. determine the current installation state
4. determine the desired state from the packaged context and the repository configuration
5. validate prerequisites and migration preconditions
6. calculate the changes
7. detect conflicts
8. install the capability and create required configuration and artifacts
9. register integrations
10. write the installation manifest
11. verify the result
12. report

Inspection never mutates anything, and nothing is written until the whole plan has been
calculated and validated.

## Idempotency

`init` is idempotent. Running it again when nothing needs to change reports
`Visual Engineering is already up to date. No changes required.` and writes nothing at all: no
file is rewritten, no timestamp is refreshed, no content changes.

This is enforced by tests: `init` runs twice against a clean repository and the full content
hash snapshot of the repository must be byte identical. Nothing the tool writes contains a
timestamp or any other volatile value.

## What it may create

| Path | Ownership |
| --- | --- |
| `.visual-engineering/AGENT-INSTRUCTIONS.md` | tool-owned |
| `.visual-engineering/UI-FOUNDATIONS.md` | tool-owned |
| `.visual-engineering/UI-DECISION-CHECKLIST.md` | tool-owned |
| `.visual-engineering/UI-ANTI-PATTERNS.md` | tool-owned |
| `.visual-engineering/RESEARCH-INDEX.md` | generated |
| `.visual-engineering/sources.json` | generated |
| `.visual-engineering/context.json` | generated |
| `.echelon/visual-engineering.json` | tool-owned |
| `.echelon/visual-engineering.config.json` | shared |
| `.gitignore` | shared (managed region only) |
| `AGENTS.md` | shared (managed region only) |

## What it will not do

- It will not modify any path not listed above.
- It will not modify content outside a managed region in `.gitignore` or `AGENTS.md`.
- It will not remove or reorder keys you added to `.echelon/visual-engineering.config.json`.
- It will not replace tool maintained content that was modified locally. That is a blocker,
  reported with exit code 5, until you restore the file or pass `--force`.
- It will not delete anything.
- It will not run repository scripts, download anything, or contact a network service.

## Conflicts

A conflict is content that the tool would otherwise write over but did not write itself:

| Situation | Behaviour |
| --- | --- |
| A tool-owned or generated file differs from both the desired content and the content recorded in the manifest | blocked: `LocallyModifiedFile` |
| The managed region inside a shared file differs from the region recorded in the manifest | blocked: `LocallyModifiedRegion` |
| `.echelon/visual-engineering.config.json` is not valid JSON | blocked: `ConflictingUnmanagedFile` |
| A file from a pre-Echelon `ve-context` installation differs from the packaged context | adopted and replaced; that mechanism owned the whole context directory |

Blocked runs change nothing. Resolve the conflict, or rerun with `--force` to accept
replacement of the tool maintained content. `--force` never touches user content outside a
managed region.

## Configuration

`init` writes `.echelon/visual-engineering.config.json`:

```json
{
  "schemaVersion": 1,
  "tool": "visual-engineering",
  "configurationVersion": 3,
  "contextDirectory": ".visual-engineering",
  "integrations": {
    "agentsFile": "AGENTS.md",
    "gitignore": true
  }
}
```

You may change:

- `contextDirectory` — where the context is installed. Must be a relative path inside the
  repository.
- `integrations.agentsFile` — which file receives the managed briefing region, or `null` to
  register nothing.
- `integrations.gitignore` — `false` to leave `.gitignore` alone.

Any other key you add is preserved across `init` and `upgrade`. The tool owns
`schemaVersion`, `tool`, `configurationVersion`, `contextDirectory` and `integrations`.

## Dry run and check

```bash
npx @echelon-foundry/visual-engineering init --dry-run          # report the plan, write nothing
npx @echelon-foundry/visual-engineering init --dry-run --json   # the same plan, machine readable
npx @echelon-foundry/visual-engineering init --check            # write nothing, exit 4 if changes are required
```

## Legacy compatibility

Two earlier distribution mechanisms remain supported and are unchanged by this package:

1. **npm adapter** `@kemiller2002/visual-engineering-context` with the `ve-context` executable
   (`sync`, `verify`, `status`, `show`). It installs the same context files into
   `.visual-engineering/`.
2. **GitHub Pages feed and immutable releases**, described in
   [`agent-context/README.md`](../agent-context/README.md).

A repository installed by either mechanism is recognised as configuration version 1. Running
`npx @echelon-foundry/visual-engineering upgrade` adopts it in place: the existing context files
are kept, an installation manifest is written, and the integrations are registered. Nothing is
deleted. See [upgrading.md](upgrading.md).
