#!/usr/bin/env node
"use strict";

// Minimal launcher. It resolves the packaged executable for this platform and hands over.
// It contains no lifecycle logic: what to install, what repository state means, what is
// valid and what must be migrated are decided by the F# implementation it launches.

const { spawnSync } = require("node:child_process");
const { existsSync } = require("node:fs");
const path = require("node:path");

const EXIT_INTERNAL_FAILURE = 1;
const EXIT_PACKAGING_FAILURE = 6;
const EXIT_UNSUPPORTED_PLATFORM = 7;

const RUNTIME_IDENTIFIERS = {
  "win32-x64": "win-x64",
  "win32-arm64": "win-arm64",
  "linux-x64": "linux-x64",
  "linux-arm64": "linux-arm64",
  "darwin-x64": "osx-x64",
  "darwin-arm64": "osx-arm64",
};

const key = `${process.platform}-${process.arch}`;
const runtimeIdentifier = RUNTIME_IDENTIFIERS[key];

if (!runtimeIdentifier) {
  process.stderr.write(
    `visual-engineering: unsupported platform ${key}. ` +
      `Supported: ${Object.keys(RUNTIME_IDENTIFIERS).join(", ")}.\n`
  );
  process.exit(EXIT_UNSUPPORTED_PLATFORM);
}

const binaryName =
  process.platform === "win32" ? "visual-engineering.exe" : "visual-engineering";
const platformPackage = `@echelon-foundry/visual-engineering-${runtimeIdentifier}`;

function resolveExecutable() {
  // Installed layout: one optional dependency per platform, so an install downloads only
  // the executable this machine can run.
  try {
    const manifest = require.resolve(`${platformPackage}/package.json`);
    const candidate = path.join(path.dirname(manifest), binaryName);
    if (existsSync(candidate)) return candidate;
  } catch {
    // Not installed. Fall through to the staged layout.
  }

  // Staged layout: a locally built package keeps every platform beside the launcher.
  const staged = path.join(__dirname, "..", "platforms", runtimeIdentifier, binaryName);
  return existsSync(staged) ? staged : null;
}

const executable = resolveExecutable();

if (!executable) {
  process.stderr.write(
    `visual-engineering: the executable for ${runtimeIdentifier} is missing. ` +
      `It ships in ${platformPackage}, which npm installs automatically on this platform. ` +
      `Reinstall the package, and if you install with --no-optional or --omit=optional, ` +
      `add ${platformPackage} explicitly.\n`
  );
  process.exit(EXIT_PACKAGING_FAILURE);
}

// The executable ships in the platform package and the context payload in this one, so tell
// the executable where the payload is. This is package layout, not a lifecycle decision: what
// the payload contains and what to do with it are decided by the F# implementation.
const payload = path.join(__dirname, "..", "payload");
const env = existsSync(payload)
  ? { ...process.env, VISUAL_ENGINEERING_PAYLOAD: payload }
  : process.env;

const result = spawnSync(executable, process.argv.slice(2), { stdio: "inherit", env });

if (result.error) {
  process.stderr.write(`visual-engineering: ${result.error.message}\n`);
  process.exit(EXIT_INTERNAL_FAILURE);
}

process.exit(result.status === null ? EXIT_INTERNAL_FAILURE : result.status);
