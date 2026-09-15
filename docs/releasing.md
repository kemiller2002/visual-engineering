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

Prerequisite 1 is already satisfied, and this is worth knowing before debugging: the scope holds
seven published packages, all with `kevin.m.miller` as maintainer.

```text
@echelon-foundry/communication-engineering     0.1.0
@echelon-foundry/repository-operating-system   3.0.0
@echelon-foundry/research-publisher            0.1.1
@echelon-foundry/ros-worker-daemon             0.1.1
@echelon-foundry/sde                           1.1.1
@echelon-foundry/typescript-wasm-kernel        0.4.1
@echelon-foundry/visual-engineering-context    0.2.0
```

The org exists, the account is a member, and it has published into the scope seven times. So a
404 on publishing `@echelon-foundry/visual-engineering-*` is **not** a missing scope and **not**
a missing membership. It narrows to prerequisite 3: the token in `NPM_TOKEN` does not cover the
seven new package names.

That is the expected failure for a granular token whose permissions were selected per package,
because the seven packages did not exist when the token was created and so could not be
selected. Fix it with either:

- an **automation token**, which is account-wide; or
- a **granular token** whose permission is set on the **`@echelon-foundry` scope** rather than on
  a list of individual packages.

After the first successful release the seven packages exist and a per-package granular token
becomes possible, but a scope-level one keeps working without being reissued.

Without `NPM_TOKEN` the workflow still builds, tests, packs and exercises the archives, then
warns that publication was skipped.

#### What the preflight checks, and what it cannot

Before publishing anything, the publish job runs `scripts/preflight-npm-publish.mjs`. It reads
the staged `npm/package.json`, derives the seven packages and the release version from it, and
refuses the release if:

- any platform package is pinned to a version other than the release version, meaning the
  staged package is internally inconsistent;
- `npm whoami` fails, meaning `NPM_TOKEN` is missing, expired or revoked;
- any of the seven versions already exists on the registry, which cannot be republished.

It runs every check before reporting, so one run lists everything that is wrong.

It **cannot** confirm that the token is allowed to publish. npm exposes no endpoint for that:

- `npm access list packages @echelon-foundry` returns each package's own public access setting,
  not the caller's permission. It answers the same for an anonymous caller as for an authorised
  one, so a pass means nothing about write access.
- `npm org ls echelon-foundry` exits 0 with empty output when unauthenticated.
- A publish authorisation failure comes back as 404, indistinguishable from a missing package.

The only true test of publish permission is a publish. So the preflight reports how many of the
seven packages are new to the scope -- a first publish needs scope-level write access, because a
granular token cannot be scoped to packages that do not exist yet -- and the publish steps are
written to fail legibly: if a publish fails partway, the error names exactly which versions
reached the registry. Those versions are permanently taken, and the release must move to a new
version rather than retry the same one.

Run it locally against the staged package at any time:

```bash
npm run tool:build -- --version <version>
npm run tool:preflight
```

Without npm credentials the authentication check fails and the rest still runs, which is the
quickest way to confirm a version is still free before tagging.

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
cover the packages being published. For `@echelon-foundry` the first two are already
established above, so only the third is in question.

The same failure has occurred on every run of the legacy
`.github/workflows/publish-ui-context.yml`, and `@kemiller2002/visual-engineering-context` has
never reached the registry despite the workflow running. That one is a separate problem, not the
same one: `@kemiller2002` is a different scope from `@echelon-foundry`, so a token fixed for one
does not necessarily cover the other. If both release paths are wanted, the token needs write
access to both scopes.

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
npm run tool:preflight
```

Then push the tag:

```bash
git tag visual-engineering-v<version>
git push origin visual-engineering-v<version>
```

The tag is the release trigger and the version comes from it. If the registry prerequisites are
not met, the workflow builds, tests, and then stops at the preflight or the first publish, and
the tag refers to a release that does not exist. Nothing is published in that case, so the
version number is still free and the same tag can be re-run once the registry side is fixed --
but only as long as no package actually reached the registry. Confirm the
[registry prerequisites](#registry-prerequisites) before tagging.

### Publishing order

The workflow publishes the six platform packages first, then the root package. The root
declares each platform package as an `optionalDependency` pinned to the exact release version,
so publishing the root first would briefly reference packages that do not exist.
