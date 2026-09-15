---
project: visual-engineering
purposes:
  - orient
  - apply
audiences:
  - practitioner
  - contributor
---

# Distributing Visual Engineering UI Context

The files in this directory are the maintained operational synthesis. The package build combines them with a generated, source-linked index of current research.

## Recommended consumer setup: the Echelon Foundry lifecycle tool

```bash
npx @echelon-foundry/visual-engineering init
```

This is the canonical public interface. It installs the context, writes an installation
manifest, registers the agent integration, and supports `status`, `verify`, `upgrade` and
`doctor`. See the [repository README](../README.md), [docs/installation.md](../docs/installation.md)
and [docs/cli.md](../docs/cli.md).

The channels below remain supported for existing consumers and are unchanged. They are legacy
compatibility, not equal alternatives: a repository set up with any of them is detected and
migrated in place by `npx @echelon-foundry/visual-engineering upgrade`.

## Legacy compatibility: GitHub Pages

Add this to the consumer repository's `AGENTS.md`:

```md
## Visual Engineering UI research

Before designing, implementing, or reviewing UI:

1. Refresh the current Visual Engineering context using the repository's documented context command.
2. Read `.visual-engineering/UI-FOUNDATIONS.md`.
3. Read `.visual-engineering/UI-DECISION-CHECKLIST.md`.
4. Read `.visual-engineering/UI-ANTI-PATTERNS.md`.
5. Consult `.visual-engineering/RESEARCH-INDEX.md` for provenance and deeper evidence.
6. Inspect the product and its existing design system.
7. Apply the research as decision criteria, not as a visual style.
8. Report the context version, source commit, principles applied, verification performed, and justified deviations.

Do not manually copy Visual Engineering research into this repository.
```

Add `.visual-engineering/` to the consumer's `.gitignore`.

Retrieve and verify the current Pages bundle:

```bash
VE_CONTEXT_TMP="$(mktemp -d)"
curl -fsSL \
  https://kemiller2002.github.io/Visual-Engineering/context/visual-engineering-context-latest.tar.gz \
  -o "$VE_CONTEXT_TMP/visual-engineering-context-latest.tar.gz"
curl -fsSL \
  https://kemiller2002.github.io/Visual-Engineering/context/SHA256SUMS \
  -o "$VE_CONTEXT_TMP/SHA256SUMS"
(cd "$VE_CONTEXT_TMP" && shasum -a 256 -c SHA256SUMS)
mkdir -p .visual-engineering
tar -xzf "$VE_CONTEXT_TMP/visual-engineering-context-latest.tar.gz" \
  --strip-components=1 \
  -C .visual-engineering
```

Pages exposes:

- `/context/manifest.json`: discovery and current version
- `/context/latest/`: individually downloadable current artifacts
- `/context/visual-engineering-context-latest.tar.gz`: verified current bundle
- `/context/SHA256SUMS`: bundle checksum

## Legacy compatibility: immutable GitHub Releases

For consequential work, pin a release rather than following `latest`. Create one by:

1. Running the `Release immutable UI agent context` workflow with a semantic version, or
2. Pushing a tag such as `ui-context-v1.2.0`.

The workflow publishes:

- `visual-engineering-context-<version>.tar.gz`
- `SHA256SUMS`
- `context.json`

The archive includes `AGENT-INSTRUCTIONS.md`, the three operational UI documents,
the research index, provenance records, integrity metadata, and `release.json`.

Each semantic version is publish-once: the workflow refuses to modify an existing tag
or GitHub Release.

Releases are available from:

```text
https://github.com/kemiller2002/Visual-Engineering/releases
```

The Pages feed is mutable and always current. GitHub Release assets are immutable version pins.

## Legacy compatibility: the `ve-context` npm adapter

Node projects may still use:

```bash
npm exec --yes --package=@kemiller2002/visual-engineering-context@latest -- ve-context sync
```

This adapter predates `@echelon-foundry/visual-engineering` and is retained so existing
consumers keep working.

## Producer commands

```bash
npm run research:build
npm run context:build
npm run context:validate
npm run context:test
npm run context:pack

# Stage, inspect and exercise the @echelon-foundry/visual-engineering package
npm run tool:build
npm run tool:pack
npm run tool:test-package
```

Publishing is performed by `.github/workflows/publish-ui-context.yml`. Configure:

- Repository secret `NPM_TOKEN`: npm automation or granular access token authorized to publish the package.
- Optional repository variable `VE_CONTEXT_NPM_PACKAGE`: alternate scoped package name.

Without `NPM_TOKEN`, the workflow still builds and validates but skips registry publication.

`@echelon-foundry/visual-engineering` is published by
`.github/workflows/publish-visual-engineering-tool.yml` from a `visual-engineering-v*` tag, using
the same `NPM_TOKEN` secret. See [docs/releasing.md](../docs/releasing.md).
