// Producer side build for the @echelon-foundry/visual-engineering npm package.
//
// This script only assembles the distributable: it stages the generated context payload,
// publishes the F# CLI for each supported runtime identifier, and stamps one authoritative
// version into both the executable and package.json. It makes no lifecycle decisions.

import { execFileSync } from "node:child_process";
import { cp, mkdir, readFile, rm, writeFile } from "node:fs/promises";
import { existsSync } from "node:fs";
import path from "node:path";
import { fileURLToPath } from "node:url";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const packageRoot = path.join(root, "npm");
const contextSource = path.join(root, "packages/visual-engineering-context/context");
const payloadDir = path.join(packageRoot, "payload");
const runtimesDir = path.join(packageRoot, "runtimes");
const cliProject = path.join(root, "src/VisualEngineering.Cli/VisualEngineering.Cli.fsproj");

const ALL_RUNTIME_IDENTIFIERS = [
  "linux-x64",
  "linux-arm64",
  "win-x64",
  "win-arm64",
  "osx-x64",
  "osx-arm64",
];

function argument(name, fallback) {
  const index = process.argv.indexOf(name);
  return index >= 0 && process.argv[index + 1] ? process.argv[index + 1] : fallback;
}

const version = argument("--version", process.env.VE_TOOL_VERSION || "0.0.0-dev");

if (!/^\d+\.\d+\.\d+(?:-[0-9A-Za-z.-]+)?$/.test(version)) {
  throw new Error(`Version must be semantic, received: ${version}`);
}

const runtimeIdentifiers = argument("--rid", ALL_RUNTIME_IDENTIFIERS.join(",")).split(",");

for (const identifier of runtimeIdentifiers) {
  if (!ALL_RUNTIME_IDENTIFIERS.includes(identifier)) {
    throw new Error(`Unsupported runtime identifier: ${identifier}`);
  }
}

if (!existsSync(path.join(contextSource, "context.json"))) {
  throw new Error(
    `Generated context is missing at ${contextSource}. Run "npm run research:build" then "npm run context:build" first.`
  );
}

await rm(payloadDir, { recursive: true, force: true });
await rm(runtimesDir, { recursive: true, force: true });
await mkdir(path.join(payloadDir, "context"), { recursive: true });
await cp(contextSource, path.join(payloadDir, "context"), { recursive: true });

for (const identifier of runtimeIdentifiers) {
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
      path.join(runtimesDir, identifier),
    ],
    { stdio: "inherit", cwd: root }
  );
}

const packageJsonPath = path.join(packageRoot, "package.json");
const packageJson = JSON.parse(await readFile(packageJsonPath, "utf8"));
packageJson.version = version;
await writeFile(packageJsonPath, `${JSON.stringify(packageJson, null, 2)}\n`);

// The root README is the authoritative package page. Copying it keeps the two from drifting.
await cp(path.join(root, "README.md"), path.join(packageRoot, "README.md"));

const context = JSON.parse(await readFile(path.join(payloadDir, "context/context.json"), "utf8"));

process.stdout.write(
  `${JSON.stringify(
    {
      package: packageJson.name,
      version,
      contextVersion: context.contextVersion,
      sourceCommit: context.sourceCommit,
      runtimeIdentifiers,
    },
    null,
    2
  )}\n`
);
