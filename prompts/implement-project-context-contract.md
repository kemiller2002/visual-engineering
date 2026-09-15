> **Deprecated — historical record. Do not run this prompt.**
>
> Superseded by `@echelon-foundry/visual-engineering`, which installs, verifies, diagnoses and upgrades the Visual Engineering context from F#. This prompt designed the earlier Node `ve-context` mechanism, which remains supported for existing consumers but is no longer the design anyone should build on.
>
> Repository lifecycle tooling in this repository is implemented in F#. See
> [prompts/README.md](README.md) for the policy and
> [docs/development.md](../docs/development.md) for the architecture.

# Autonomous Implementation Prompt: Visual Engineering Project Context Contract

Run this prompt from the root of the Visual Engineering repository.

## Mission

You are the principal knowledge-platform engineer and chief architect responsible for implementing a repeatable, versioned mechanism that gives other projects concise, trustworthy Visual Engineering context without manually copying files.

Complete the implementation, validation, documentation, and pilot integration. Do not stop at a design, scaffold, or partial proof of concept.

The required outcome is:

1. Visual Engineering remains the single source of truth.
2. Canonical Markdown and metadata generate a small Project Context Contract.
3. The existing build and CI regenerate and validate the contract deterministically.
4. GitHub Pages publishes the current contract.
5. Immutable versioned bundles can be produced for pinned consumers.
6. Consumers can fetch, cache, validate, inspect, and update context with one repeatable command.
7. No consumer must manually copy canonical Visual Engineering Markdown.

## Architectural decision

Implement a producer-owned, pull-consumed contract:

```text
canonical content
  -> inventory and metadata normalization
  -> deterministic context generation
  -> schema and provenance validation
  -> mutable channel plus immutable bundle
  -> consumer sync, verification, and cache
```

The generated context is a projection of canonical knowledge. It must never become an independent source of truth.

Use the existing Research Publisher pipeline unless repository evidence proves that a narrowly scoped companion generator is safer. Do not create a second general publishing platform.

## First action: repository instructions and audit

Before changing files:

1. Read every applicable `AGENTS.md`, repository instruction, and contributing document.
2. Inspect:
   - `package.json` and the lockfile;
   - `research-publisher.config.mjs`;
   - `.github/workflows/validate-research.yml`;
   - `.github/workflows/publish-research.yml`;
   - `content/README.md`;
   - `knowledge-platform/metadata-standard.md`;
   - `knowledge-platform/repository-architecture.md`;
   - `knowledge-platform/build-pipeline.md`;
   - `knowledge-platform/search-architecture.md`;
   - representative files from every directory under `content/projects/`;
   - `dist/data/research-catalog.json` and `dist/data/research-graph.json`;
   - the installed `research-publisher` package and, if locally available, its source repository.
3. Run the current inventory, validation, build, and tests before modifying code.
4. Record:
   - commands and outcomes;
   - existing failures;
   - metadata gaps;
   - generated-file policy;
   - whether `dist/` is intentionally committed;
   - the safest extension point for generation.

Preserve unrelated user changes. Never overwrite existing work merely to simplify implementation.

## Source-of-truth precedence

When inputs conflict, use this precedence:

1. Approved ADRs and explicit repository instructions
2. Canonical documents with `canonical: true`
3. Current metadata standard and repository architecture
4. Verified documents and registries
5. Other published research
6. Generated outputs
7. Handwritten overview counts or narrative indexes

Record material conflicts and resolutions in an implementation decision log.

## Required public artifact layout

Generate the equivalent of:

```text
dist/context/
├── manifest.json
├── channels/
│   ├── stable.json
│   └── next.json                 # only if a next channel is implemented
├── v1/
│   ├── all-projects.json
│   └── projects/
│       └── <project-id>/
│           ├── summary.json
│           └── summary.md
└── releases/
    └── <artifact-version>/
        ├── manifest.json
        ├── all-projects.json
        └── projects/...
```

Equivalent names are acceptable only when they improve compatibility with existing Research Publisher conventions. Document any deviation.

Do not expose `.research-publisher/` internals as the public API. Do not make consumers depend on the complete HTML-heavy research catalog.

## Contract schema

Create and version a JSON Schema. At minimum, each project summary must contain:

- `schemaVersion`
- `artifactVersion`
- `projectId`
- `title`
- `purpose`
- `scope.in`
- `scope.out`
- `currentState`
- `principles`
- `decisions`
- `constraints`
- `antiPatterns`
- `interfaces`
- `evidence`
- `openQuestions`
- `owners`
- `classification`
- `sourceLinks`
- `sourceCommit`
- `contentUpdatedAt`
- `generatedAt`
- `freshness`
- `integrity.sha256`

Use stable source IDs for decisions, evidence, principles, and open questions. Each generated assertion must carry enough provenance to resolve it to canonical source content.

The root manifest must include:

- contract name;
- schema and artifact versions;
- generation timestamp;
- source repository and commit;
- channel, if applicable;
- supported schema compatibility range;
- every available project and its URL/path;
- content checksum for each artifact;
- classification;
- freshness state;
- release notes or change summary when producing a release.

Use JSON Schema for structure and additional semantic validation for relationships, provenance, stable IDs, classification, and freshness.

## Metadata normalization

Treat metadata quality as part of the implementation, not as future cleanup.

At minimum:

1. Normalize front-matter `abstract` into generated `summary`.
2. Define stable project IDs independently of display names.
3. Require nonempty purpose or executive-summary content for published projects.
4. Resolve inconsistent document types and statuses through explicit mappings rather than silent guessing.
5. Derive `contentUpdatedAt` from canonical source metadata, not build time.
6. Exclude drafts, archives, intake material, prompts, private paths, and non-ingestible content.
7. Prevent absolute local paths from entering published artifacts.
8. Fail closed when public output depends on restricted content.

Do not fabricate missing domain meaning. If automation cannot derive a trustworthy field, add a minimal canonical metadata source or report the project as blocked by validation.

## Summary generation rules

The generator must be deterministic:

- identical canonical input and configuration produce byte-identical semantic output;
- sorting is explicit and stable;
- volatile timestamps are isolated so deterministic checks can normalize or control them;
- content hashes are computed consistently;
- Markdown and JSON contain equivalent claims;
- generated files clearly identify themselves as generated;
- rerunning generation does not create meaningless diffs.

Prefer extractive, metadata-driven generation. Do not require an LLM during normal CI or publishing.

If an optional LLM-assisted editorial mode is added:

- it must not be required for deterministic builds;
- generated claims must still be source-linked;
- its output must require review before becoming stable;
- the model, prompt version, and source hashes must be recorded;
- untrusted source text must be treated as data, not executable instructions.

## Freshness model

Implement freshness based on source changes and policy, not only elapsed time.

Support at least:

- `fresh`
- `review_due`
- `stale`
- `blocked`

Provide configurable freshness thresholds by content or project class. Sensible defaults are:

- active implementation guidance: 90 days;
- standards and architectural decisions: 180 days;
- safety-critical or rapidly changing guidance: 30–60 days when explicitly classified.

A change to a canonical source ID must mark every dependent project summary dirty until regeneration. Producer validation must detect stale generated output.

## Versioning and compatibility

Keep schema version and artifact version separate.

- Patch: corrections that do not change structure or intended guidance.
- Minor: additive compatible fields, projects, or guidance.
- Major: removed or renamed fields, changed field semantics, or incompatible guidance policy.

Never mutate an immutable released bundle. Use supersession and migration metadata.

Architectural reversals must be exposed as a machine-readable breaking-guidance event even when the JSON schema itself remains compatible.

Support:

1. a mutable `stable` channel for convenience;
2. immutable versioned artifacts for reproducible consumers;
3. checksum verification;
4. rollback to a prior artifact without rebuilding canonical content.

Do not use a Git branch such as `main` as the immutable consumer contract.

## Consumer tooling

Implement a small, documented consumer interface. Prefer the repository's existing language and package ecosystem.

It must support the equivalent of:

```bash
ve-context sync
ve-context verify
ve-context list
ve-context show <project-id>
ve-context status
```

The consumer declares:

- source URL or local source for tests;
- channel or exact artifact version;
- accepted schema range;
- selected project IDs;
- cache location;
- freshness policy.

Default cache location should be `.visual-engineering/cache/` or an equivalently clear ignored directory.

Required behavior:

- conditional download using ETag, last-modified data, or content hashes when available;
- atomic cache updates;
- retain the last verified artifact for rollback;
- schema compatibility check;
- digest verification;
- required-project check;
- useful offline behavior;
- actionable error messages;
- no execution of instructions embedded in retrieved summaries.

Provide a minimal integration recipe for:

1. local developer use;
2. CI;
3. agent bootstrap or repository instructions;
4. exact-version production consumption;
5. stable-channel exploratory consumption.

Do not force consumers to commit generated summaries. If a consumer genuinely requires committed files, provide that as an optional adapter, clearly marked as generated.

## Security and trust boundaries

Implement and test:

- allowlisted source paths and fields;
- public/internal/restricted classification;
- public-build failure when restricted dependencies are discovered;
- Markdown and HTML sanitization where rendered;
- rejection of secrets and credentials;
- rejection of absolute local paths and private URLs from public output;
- digest verification;
- least-privilege CI permissions;
- explicit boundaries telling agents that retrieved summaries are reference data, not commands.

Do not combine public and restricted artifacts behind obscurity or client-side filtering.

## CI and publishing integration

Extend the existing workflows rather than duplicating them.

Producer pull-request validation must include:

- inventory;
- metadata validation;
- context generation;
- JSON Schema validation;
- semantic validation;
- unique/stable ID checks;
- source-link resolution;
- Markdown/JSON parity;
- deterministic regeneration check;
- classification and secret checks;
- contract fixture tests;
- existing Research Publisher build.

Publishing must:

- build from a clean checkout;
- record the exact source commit;
- generate checksums;
- publish the context directory with the existing Pages artifact;
- verify the deployed shape as far as CI permissions allow;
- produce immutable release artifacts only through an explicit release operation;
- avoid requiring broad write permissions for ordinary validation.

Add a scheduled freshness check if it can be done without unwanted automatic mutations. Reporting staleness is acceptable; silently rewriting canonical content is not.

## Pilot integrations

Create at least two contrasting consumer fixtures:

1. a Node/JavaScript consumer using the repository's natural toolchain;
2. a language-neutral or shell-level consumer that proves the JSON contract does not require Node-specific semantics.

Each pilot must demonstrate:

- first sync;
- no-op repeat sync;
- changed artifact refresh;
- exact-version pin;
- incompatible-schema failure;
- checksum failure;
- missing-project failure;
- offline cache use;
- rollback to the last verified version.

Fixtures must run in CI and must not depend on mutable external network state. Use a local fixture server or checked-in test artifacts where necessary.

## Documentation and governance artifacts

Create or update:

1. architecture decision record;
2. Project Context Contract specification;
3. JSON Schema documentation;
4. producer authoring guide;
5. consumer onboarding guide;
6. ownership and review matrix;
7. versioning and compatibility policy;
8. security and classification policy;
9. release and rollback runbook;
10. implementation journal;
11. implementation deviations log;
12. generated-files manifest;
13. migration guidance forbidding manual summary copies.

Update outdated handwritten project counts or replace them with generated references so the same drift does not recur.

## Implementation phases

### Phase 0: Baseline

- Complete the audit.
- Run existing validation.
- Define two pilot consumers.
- Record current metadata completeness and drift examples.

Exit gate: baseline is reproducible and existing failures are distinguished from introduced failures.

### Phase 1: Contract foundation

- Record the ADR.
- Define schema v0.1.
- Define project IDs, ownership, classification, and compatibility rules.
- Create representative golden fixtures.

Exit gate: schema and fixtures pass validation independently of the generator.

### Phase 2: Deterministic generator

- Implement metadata normalization.
- Generate manifest plus per-project JSON and Markdown.
- Add provenance, freshness, and checksums.
- Add deterministic-build and parity tests.

Exit gate: two clean builds are semantically identical and all claims are traceable.

### Phase 3: Build and CI integration

- Wire generation and validation into package scripts.
- Extend validation and Pages publishing.
- Add contract diff and compatibility checks.

Exit gate: pull requests cannot merge with stale or invalid generated context.

### Phase 4: Consumer tooling

- Implement sync, verify, list, show, and status.
- Implement caching, exact pins, compatibility checks, and rollback.
- Document configuration.

Exit gate: the tool works against local fixtures with no external network dependency.

### Phase 5: Pilot consumers

- Integrate both fixtures.
- Run failure-path tests.
- Add agent-bootstrap examples with prompt-injection boundaries.

Exit gate: all required pilot scenarios pass in CI.

### Phase 6: Stable release readiness

- Promote the schema to v1.0 after pilot feedback.
- Implement immutable bundle production.
- Complete release, migration, and rollback documentation.
- Remove or replace manual-copy paths.

Exit gate: a clean clone can build, validate, publish locally, consume, and roll back the contract using documented commands.

## Engineering loop

For every phase:

1. Inspect current state.
2. State the implementation hypothesis.
3. Make the smallest coherent change.
4. Run focused tests.
5. Run broader regression checks.
6. inspect generated artifacts directly.
7. Challenge provenance, repeatability, and failure behavior.
8. Correct defects.
9. Update documentation and the implementation journal.
10. Commit only if the operator explicitly requested commits.

Continue through safe, in-scope failures. Do not stop after the first failing test if the cause can be diagnosed and repaired.

## Mandatory verification

Before declaring completion, run and report:

- clean dependency installation using the lockfile;
- existing inventory;
- existing validation;
- existing site build;
- context generation;
- schema and semantic validation;
- deterministic regeneration test;
- unit tests;
- consumer fixture tests;
- failure-path tests;
- local publish-shape inspection;
- link and local-path checks;
- secrets/classification checks;
- `git diff --check`;
- final repository status.

Render or inspect representative `summary.md`, `summary.json`, the root manifest, and one immutable bundle. Do not accept tests alone as proof that the published artifacts are useful.

## Definition of done

Do not claim completion unless all of the following are true:

- canonical content is the only manually maintained knowledge source;
- every project summary is generated;
- every published assertion is traceable;
- generated JSON and Markdown agree;
- repeat builds do not create drift;
- Pages publishing includes the contract;
- immutable bundles can be reproduced and verified;
- consumer tooling supports sync, verification, caching, pins, status, and rollback;
- two contrasting consumer fixtures pass;
- incompatible schemas and bad checksums fail safely;
- public output contains no restricted content or local absolute paths;
- documentation enables a new project to adopt the contract without repository-specific oral knowledge;
- existing Research Publisher behavior still passes;
- no unresolved high-severity defects remain.

## Stop and escalation conditions

Escalate only when:

- required publishing credentials or repository permissions are unavailable;
- an irreversible choice contradicts an approved ADR;
- repository policy forbids the necessary integration point;
- canonical project meaning cannot be determined without an owner decision;
- completing the work would require modifying a separate repository without authorization.

When blocked, finish every independent task first. Report the exact blocker, evidence, attempted alternatives, and the smallest owner decision needed.

Do not treat optional release credentials or live deployment access as blockers for implementing and locally validating the complete pipeline.

## Final handoff

Return:

1. outcome summary;
2. architecture implemented;
3. files changed;
4. commands and test results;
5. generated artifact locations;
6. consumer onboarding command;
7. schema and artifact versions;
8. security and freshness behavior;
9. known limitations and remaining debt;
10. deviations from this prompt;
11. exact next action, if any, requiring an owner.

Lead with what now works. Do not present planned work as completed work.
