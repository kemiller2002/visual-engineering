# Releasing

There are two independent release paths in this repository. Both remain supported.

## 1. `@echelon-foundry/visual-engineering` (recommended)

Workflow: `.github/workflows/publish-visual-engineering-tool.yml`

Trigger: push of a tag matching `visual-engineering-v*`, or manual dispatch with a semantic
version.

The workflow does all of the following before anything is published:

1. restore and build the F# solution
2. run the unit and lifecycle tests
3. build the research catalog and the context payload
4. publish the CLI for every supported runtime identifier
5. stage the npm package
6. verify the package contents with `npm pack --dry-run`
7. run `npm pack`
8. install the resulting archive into a clean temporary directory and exercise it against
   temporary repositories on Linux, macOS and Windows
9. publish to npm

A local developer machine is not the release process. The only supported publish path is the
workflow.

### Version

One value flows everywhere:

```bash
node scripts/build-tool-package.mjs --version 1.4.0
```

It is stamped into `dotnet publish -p:Version=` and into `npm/package.json`. The CLI reads its
version from the assembly, so `visual-engineering --version` always equals the published npm
version. The packed artifact test asserts this.

### Credentials

- Repository secret `NPM_TOKEN`: an npm automation or granular token allowed to publish
  `@echelon-foundry/visual-engineering`.

Without it the workflow still builds, tests, packs and exercises the archive, and then warns
that publication was skipped.

## 2. `@kemiller2002/visual-engineering-context` (legacy compatibility)

Unchanged. Two workflows cover it:

- `.github/workflows/publish-ui-context.yml` publishes the mutable `latest` package on every
  qualifying push to `main`.
- `.github/workflows/release-ui-context.yml` publishes an immutable GitHub Release from a
  `ui-context-v*` tag.

These are the mechanisms existing consumers use. Do not remove them.

## Release checklist

```bash
npm ci
npm run research:validate
npm run research:build
npm run context:build
npm run context:validate
npm run context:test
dotnet test VisualEngineering.sln
npm run tool:build -- --version <version>
npm run tool:pack
npm run tool:test-package
```

Then push the tag:

```bash
git tag visual-engineering-v<version>
git push origin visual-engineering-v<version>
```
