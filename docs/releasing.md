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
5. stage the root package and the six platform packages
6. verify the root package contents with `npm pack --dry-run`
7. run `npm pack` on the root package and on the host's platform package
8. install the resulting archives into a clean temporary directory and exercise them against
   temporary repositories on Linux, macOS and Windows
9. publish the six platform packages, then the root package

The order in step 9 matters: the root package declares each platform package as an
`optionalDependency` pinned to the exact release version, so those must be on the registry
first.

A local developer machine is not the release process. The only supported publish path is the
workflow.

### Version

One value flows everywhere:

```bash
node scripts/build-tool-package.mjs --version 1.4.0
```

It is stamped into `dotnet publish -p:Version=`, into `npm/package.json`, and into every
`npm/platforms/<rid>/package.json`. The CLI reads its version from the assembly, so
`visual-engineering --version` always equals the published npm version. The packed artifact test
asserts this, and that each optional dependency is pinned to that same version.

### Registry prerequisites

Publishing needs three things to be true on the npm side. The workflow cannot create any of
them, and a publish that is missing one fails at the final step with an unhelpful error.

1. **The `@echelon-foundry` scope exists and this account can publish to it.** For an
   organisation scope, the org must exist on npm and the publishing account must be a member
   with publish rights.
2. **The repository secret `NPM_TOKEN` is set**, to an npm automation token or a granular
   access token.
3. **That token is authorised for these packages.** A granular token is limited to the packages
   or scopes selected when it was created. It must cover
   `@echelon-foundry/visual-engineering` *and* `@echelon-foundry/visual-engineering-*`, since
   the release publishes seven packages.

Without `NPM_TOKEN` the workflow still builds, tests, packs and exercises the archives, then
warns that publication was skipped.

#### Diagnosing a failed publish

npm reports authorisation failures on publish as **404, not 403**, so it does not distinguish
"this package does not exist" from "you may not publish here":

```text
npm error code E404
npm error 404 Not Found - PUT https://registry.npmjs.org/@scope%2fname
npm error 404  The requested resource '@scope/name@1.0.0' could not be found
npm error 404  or you do not have permission to access it.
```

Reaching that error means authentication succeeded and authorisation did not. Check, in order:
the scope exists; the token's account is a member of it; and the token's package permissions
cover the packages being published. A first publish into a scope the account does not own fails
exactly this way.

The same failure has occurred on every run of the legacy
`.github/workflows/publish-ui-context.yml`, which is why
`@kemiller2002/visual-engineering-context` has never reached the registry despite the workflow
running. Fixing the registry side fixes both release paths.

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

The tag is the release trigger and the version comes from it, so pushing a tag whose registry
prerequisites are not yet met burns that version number: the workflow will build and test, fail
at publish, and the tag will then refer to a release that does not exist. Confirm the
[registry prerequisites](#registry-prerequisites) before tagging.

### Publishing order

The workflow publishes the six platform packages first, then the root package. The root
declares each platform package as an `optionalDependency` pinned to the exact release version,
so publishing the root first would briefly reference packages that do not exist.
