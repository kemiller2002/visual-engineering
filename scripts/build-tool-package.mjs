// Producer side build for the @echelon-foundry/visual-engineering npm packages.
//
// This script only assembles the distributables: it stages the generated context payload,
// publishes the F# CLI for each supported runtime identifier into its own platform package,
// and stamps one authoritative version into every package.json and into the executables.
// It makes no lifecycle decisions.
//
// Layout produced:
//
//   npm/                                  @echelon-foundry/visual-engineering
//     bin/ payload/ README.md LICENSE     launcher, context payload, docs
//   npm/platforms/<rid>/                  @echelon-foundry/visual-engineering-<rid>
//     visual-engineering[.exe] LICENSE    one executable, declared os/cpu
//
// The root package declares each platform package as an optionalDependency, so npm
// downloads only the executable the installing machine can run.

import { execFileSync } from "node:child_process";
import { cp, mkdir, readFile, rm, writeFile } from "node:fs/promises";
import { existsSync } from "node:fs";
import path from "node:path";
import { fileURLToPath } from "node:url";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const packageRoot = path.join(root, "npm");
const contextSource = path.join(root, "packages/visual-engineering-context/context");
const payloadDir = path.join(packageRoot, "payload");
const platformsDir = path.join(packageRoot, "platforms");
const cliProject = path.join(root, "src/VisualEngineering.Cli/VisualEngineering.Cli.fsproj");

// Runtime identifier -> the npm `os` and `cpu` values npm matches against the install host.
const PLATFORMS = {
  "linux-x64": { os: "linux", cpu: "x64" },
  "linux-arm64": { os: "linux", cpu: "arm64" },
  "win-x64": { os: "win32", cpu: "x64" },
  "win-arm64": { os: "win32", cpu: "arm64" },
  "osx-x64": { os: "darwin", cpu: "x64" },
  "osx-arm64": { os: "darwin", cpu: "arm64" },
};

function argument(name, fallback) {
  const index = process.argv.indexOf(name);
  return index >= 0 && process.argv[index + 1] ? process.argv[index + 1] : fallback;
}

const version = argument("--version", process.env.VE_TOOL_VERSION || "0.0.0-dev");

if (!/^\d+\.\d+\.\d+(?:-[0-9A-Za-z.-]+)?$/.test(version)) {
  throw new Error(`Version must be semantic, received: ${version}`);
}

const runtimeIdentifiers = argument("--rid", Object.keys(PLATFORMS).join(",")).split(",");

for (const identifier of runtimeIdentifiers) {
  if (!PLATFORMS[identifier]) {
    throw new Error(`Unsupported runtime identifier: ${identifier}`);
  }
}

if (!existsSync(path.join(contextSource, "context.json"))) {
  throw new Error(
    `Generated context is missing at ${contextSource}. Run "npm run research:build" then "npm run context:build" first.`
  );
}

await rm(payloadDir, { recursive: true, force: true });
await rm(platformsDir, { recursive: true, force: true });
await mkdir(path.join(payloadDir, "context"), { recursive: true });
await cp(contextSource, path.join(payloadDir, "context"), { recursive: true });

const packageJsonPath = path.join(packageRoot, "package.json");
const packageJson = JSON.parse(await readFile(packageJsonPath, "utf8"));
const license = await readFile(path.join(root, "LICENSE"));

for (const identifier of runtimeIdentifiers) {
  const target = path.join(platformsDir, identifier);

  execFileSync(
    "dotnet",
    [
      "publish",
      cliProject,
      "-c",
      "Release",
      "-r",
      identifier,
      `-p:Version=${version}`,
      "-o",
      target,
    ],
    { stdio: "inherit", cwd: root }
  );

  const executable = identifier.startsWith("win-")
    ? "visual-engineering.exe"
    : "visual-engineering";

  if (!existsSync(path.join(target, executable))) {
    throw new Error(`dotnet publish did not produce ${executable} for ${identifier}`);
  }

  await writeFile(
    path.join(target, "package.json"),
    `${JSON.stringify(
      {
        name: `${packageJson.name}-${identifier}`,
        version,
        description: `${identifier} executable for ${packageJson.name}.`,
        license: packageJson.license,
        repository: packageJson.repository,
        bugs: packageJson.bugs,
        os: [PLATFORMS[identifier].os],
        cpu: [PLATFORMS[identifier].cpu],
        files: [executable, "LICENSE"],
        publishConfig: packageJson.publishConfig,
      },
      null,
      2
    )}\n`
  );

  await writeFile(path.join(target, "LICENSE"), license);
}

// Every platform package is optional: npm skips the ones whose os/cpu do not match, so an
// install downloads one executable rather than six.
packageJson.version = version;
packageJson.optionalDependencies = Object.fromEntries(
  Object.keys(PLATFORMS).map((identifier) => [`${packageJson.name}-${identifier}`, version])
);
await writeFile(packageJsonPath, `${JSON.stringify(packageJson, null, 2)}\n`);

// The root README is the authoritative package page, and the root LICENSE the authoritative
// licence. Copying both keeps the published copies from drifting from the repository.
await cp(path.join(root, "README.md"), path.join(packageRoot, "README.md"));
await cp(path.join(root, "LICENSE"), path.join(packageRoot, "LICENSE"));
await cp(path.join(root, "CHANGELOG.md"), path.join(packageRoot, "CHANGELOG.md"));

const context = JSON.parse(await readFile(path.join(payloadDir, "context/context.json"), "utf8"));

process.stdout.write(
  `${JSON.stringify(
    {
      package: packageJson.name,
      version,
      contextVersion: context.contextVersion,
      sourceCommit: context.sourceCommit,
      platformPackages: runtimeIdentifiers.map(
        (identifier) => `${packageJson.name}-${identifier}`
      ),
    },
    null,
    2
  )}\n`
);
