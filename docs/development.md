# Development

## Architecture

```text
npm / npx
    |
    v
tiny Node bootstrap (npm/bin/visual-engineering.js)
    |
    v
F# CLI (src/VisualEngineering.Cli)
    |
    v
F# core (src/VisualEngineering.Core)
    |
    +-- init / status / verify / upgrade / doctor
    +-- integration API (Api module)
    +-- tests
```

npm is distribution and invocation. F# is the application and the domain.

The Node launcher may only: detect the platform, locate the packaged executable, point it at
the packaged payload, forward arguments and standard streams, and return the exit code. It
decides nothing about what to install, what repository state means, whether an installation is
valid, what migrations are required, what configuration should exist, what is stale, or what
integrations are correct.

### Distribution shape

The executable ships in a per-platform package and the research context payload in the root
package, so the launcher passes the payload directory to the executable in
`VISUAL_ENGINEERING_PAYLOAD`. That is package layout, not a lifecycle decision.

The executable does not depend on being told: `Payload.candidateRoots` also looks for the root
package beside the platform package, for the staged `platforms/<rid>` layout, and for a payload
next to the executable. Running the binary directly is therefore a supported path, and the
packed artifact test exercises it with the launcher's hint stripped.

## Layout

| Path | Purpose |
| --- | --- |
| `src/VisualEngineering.Core/` | Domain and lifecycle. No CLI concerns, no dependencies beyond the base class library |
| `src/VisualEngineering.Cli/` | Argument parsing, rendering, exit codes. A thin adapter |
| `tests/VisualEngineering.Core.Tests/` | Lifecycle, migration, ownership, idempotency and JSON schema tests |
| `tests/VisualEngineering.Cli.Tests/` | Parser and help text tests |
| `npm/` | The root npm package: launcher, staged payload, README and licence |
| `npm/platforms/<rid>/` | One npm package per platform, each carrying a single executable |
| `scripts/build-tool-package.mjs` | Stages all seven packages from the F# build and the generated context |
| `scripts/test-tool-package.mjs` | Packs and exercises the real archives |

### Core module order

`Primitives` → `Json` → `Payload` → `Model` → `ManagedBlock` → `Desired` → `Inspection` →
`Migrations` → `Planning` → `Execution` → `Verification` → `Diagnostics` → `Api` →
`JsonOutput`.

The pipeline is deliberately one directional:

```text
inspect -> determine desired state -> calculate transition -> validate transition
        -> execute transition -> verify resulting state
```

Inspection never mutates. Planning never writes. Execution only applies a plan that carries no
blockers.

### Using the core without the CLI

`VisualEngineering.Core.Api` is the callable surface, and is the basis of a future typed
integration assembly:

```fsharp
open VisualEngineering.Core

match Api.openSession "/path/to/repo" with
| Error error -> eprintfn "%A" error
| Ok session ->
    let status = Api.getStatus session
    let report = Api.verify session true
    let outcome, updated = Api.initialize session PlanOptions.defaults false
    ignore (status, report, outcome, updated)
```

`Api.openSessionWith` binds an already loaded payload, which is how the tests run without any
packaged binary.

## Dependencies

The core and CLI take no NuGet dependencies: F# core library and the base class library only.
JSON is built with `System.Text.Json` writers over an explicit `JsonValue` model rather than
reflection based serialization, which keeps the published schemas visible in one file and keeps
the executable trim safe.

The test projects depend on xunit. That is a development dependency and is not distributed.

## Building

```bash
dotnet restore VisualEngineering.sln
dotnet build VisualEngineering.sln -c Release
dotnet test VisualEngineering.sln
```

.NET 8 SDK is required to build. It is not required to run the published package: the CLI is
published self contained, trimmed and single file for each supported runtime identifier.

## Building the npm package

The package payload is the generated context, so the research build runs first:

```bash
npm ci
npm run research:build     # writes dist/data/research-catalog.json
npm run context:build      # writes packages/visual-engineering-context/context/
npm run tool:build         # publishes the CLI and stages npm/
```

`npm run tool:build -- --rid linux-x64` stages a single runtime identifier, which is much
faster during development. The packed artifact test only needs the host platform.

Version flows from one place: `scripts/build-tool-package.mjs` takes `--version` (or
`VE_TOOL_VERSION`) and stamps it into both `dotnet publish -p:Version=` and `npm/package.json`.
The CLI reads its version from the assembly, so `--version` and the npm package version cannot
drift.

## Testing

```bash
dotnet test VisualEngineering.sln     # 80 unit and lifecycle tests
npm run tool:test-package             # packs and exercises the real .tgz archives
```

`npm run tool:test-package` is the authoritative check. It packs the package, installs the
archive into a clean temporary directory, and runs the full lifecycle against temporary
repositories, including idempotency, dry run, JSON validity, damage detection, a legacy
installation fixture and README/help agreement.

## Conventions

- Functional style: immutable data, explicit `Result` values, no exceptions as control flow.
- Illegal lifecycle states should not be representable. Prefer a new union case over a boolean.
- Anything visible through npm is a public interface: package name, executable name, package
  contents, help text, JSON schemas, exit codes.
- Document tested behaviour, not intended behaviour.
