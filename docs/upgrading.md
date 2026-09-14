# Upgrading and migration

```bash
npx @echelon-foundry/visual-engineering upgrade
npx @echelon-foundry/visual-engineering upgrade --dry-run
npx @echelon-foundry/visual-engineering upgrade --check
```

## Model

An installation carries a **configuration version**. Upgrades move it forward one version at a
time along a fixed chain; there are no N-to-N migrations.

| From | To | What it does | Precondition |
| --- | --- | --- | --- |
| 1 | 2 | Adopt a `ve-context` installation and record an Echelon installation manifest | `<contextDirectory>/context.json` exists and declares a `contextVersion` |
| 2 | 3 | Add repository configuration and register the agent briefing integration | `.echelon/visual-engineering.config.json` is readable |

Configuration version 1 is a pre-Echelon installation produced by `ve-context sync`: context
files present, no `.echelon/` manifest.

An upgrade from 1 to 3 runs `1 -> 2` then `2 -> 3` in order.

## Supported upgrade paths

| Starting state | Result |
| --- | --- |
| Not installed | `upgrade` refuses and directs you to `init` (exit 5). Nothing is written |
| Legacy `ve-context` installation (version 1) | Adopted in place and migrated to the current version |
| Configuration version 2 | Migrated to the current version |
| Current version, older context | Context files replaced with the packaged ones |
| Current version, current context | No changes |
| Configuration version newer than this release | Reported as invalid. The tool never downgrades an installation |

## Guarantees

These are the guarantees the test suite proves, and no more:

1. **Preconditions are checked before anything is written.** If any transition in the chain
   fails its precondition, the whole upgrade is refused and the repository is byte identical
   afterwards.
2. **User owned files are never modified.** Files outside the managed set are not read, written
   or deleted.
3. **User content in shared files survives.** Text outside a managed region, and configuration
   keys the tool does not own, are preserved exactly.
4. **Local modifications are detected before they would be replaced.** A tool maintained file or
   managed region whose content differs from what the tool recorded blocks the upgrade with
   exit code 5. `--force` is the explicit, non-interactive way to accept replacement.
5. **Nothing is deleted.** No migration in this release removes a file.
6. **Partial application is reported, never hidden.** Every write is staged and read back before
   any target is replaced. If a replacement still fails, the command reports how many changes
   were applied, which change failed and why, and exits 1.
7. **`--dry-run` writes nothing** and reports the same plan the real run would execute.

## Recovery

| Situation | What to do |
| --- | --- |
| Blocked by a locally modified file | Restore it, or rerun with `--force` |
| Blocked by an edit inside a managed region | Move the edit outside the markers, or rerun with `--force` |
| Blocked by a failed migration precondition | The message names the file and the reason. Repair that file, then rerun |
| Partially applied (exit 1) | Run `doctor` to see the resulting state, then `init` to converge |
| Installation manifest damaged | `doctor` reports it. Delete `.echelon/visual-engineering.json` and run `init`; your context files and shared files are untouched |

## Compatibility policy

- The command contract (`init`, `status`, `verify`, `upgrade`, `doctor`, `--help`, `--version`)
  is stable. New capability is added behind the same commands.
- The JSON envelope and per command schemas are versioned by `schemaVersion`. A breaking change
  increments it.
- Exit code meanings are stable.
- The installation manifest and configuration carry their own `schemaVersion`, and every
  configuration version this release supports has a migration path to the current one. That is
  covered by a test.
- The legacy `ve-context` mechanism remains supported. This package does not remove it, does not
  modify it, and does not require consumers to migrate.
