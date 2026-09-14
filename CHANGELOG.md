# Changelog

This file covers `@echelon-foundry/visual-engineering` and its six platform packages. The
research context published as `@kemiller2002/visual-engineering-context` has its own release
line and is not tracked here.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project
follows [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## 1.0.0

First public release.

### Added

- The `init`, `status`, `verify`, `upgrade` and `doctor` lifecycle commands, plus `--help` and
  `--version`, and `--dry-run`, `--check`, `--force`, `--json`, `--verbose`, `--strict` and
  `--repo` where each applies.
- An F# implementation: `VisualEngineering.Core` owns every lifecycle decision and is callable
  without simulating command line input; `VisualEngineering.Cli` is a thin adapter. Node is
  present only as the launcher that selects and starts the packaged executable.
- A typed installation state model (`NotInstalled`, `Installed`, `UpgradeRequired`, `Invalid`).
- A file ownership model (`tool-owned`, `generated`, `user-owned`, `shared`) recorded per path
  in the installation manifest with the hash of what the tool last wrote.
- An installation manifest at `.echelon/visual-engineering.json` and repository configuration
  at `.echelon/visual-engineering.config.json`, both schema versioned.
- Sequential migrations with preconditions, covering adoption of a pre-Echelon `ve-context`
  installation (configuration version 1) through to the current configuration version 3.
- Machine readable output on every command behind `--json`, sharing one versioned envelope.
- Stable exit codes 0 to 7, documented in `--help`, the README and `docs/cli.md`.
- Distribution as a small root package plus one optional dependency per platform, so an install
  downloads roughly 7 MB rather than every platform's executable.

### Compatibility

- The `@kemiller2002/visual-engineering-context` package, the GitHub Pages context feed and the
  immutable `ui-context-v*` releases are unchanged and remain supported.
- A repository previously set up by `ve-context sync` is detected as configuration version 1 and
  adopted in place by `upgrade`. Its files are kept and nothing is deleted.
