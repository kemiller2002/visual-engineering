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

const executable = path.join(
  __dirname,
  "..",
  "runtimes",
  runtimeIdentifier,
  process.platform === "win32" ? "visual-engineering.exe" : "visual-engineering"
);

if (!existsSync(executable)) {
  process.stderr.write(
    `visual-engineering: this package does not contain an executable for ${runtimeIdentifier} ` +
      `(expected ${executable}). Reinstall the package.\n`
  );
  process.exit(EXIT_PACKAGING_FAILURE);
}

const result = spawnSync(executable, process.argv.slice(2), { stdio: "inherit" });

if (result.error) {
  process.stderr.write(`visual-engineering: ${result.error.message}\n`);
  process.exit(EXIT_INTERNAL_FAILURE);
}

process.exit(result.status === null ? EXIT_INTERNAL_FAILURE : result.status);
