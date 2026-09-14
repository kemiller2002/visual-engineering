// Tests the actual packed npm artifact, not the source tree.
//
// It packs @echelon-foundry/visual-engineering, inspects the archive contents, installs the
// archive into a clean directory, and exercises the published command contract against
// temporary repositories: help, version, JSON output, dry run, idempotency, damage detection,
// forced repair, a legacy installation fixture, and agreement between help text and README.

import { execFileSync } from "node:child_process";
import { createHash } from "node:crypto";
import { gunzipSync } from "node:zlib";
import { existsSync, readdirSync, readFileSync, rmSync, statSync } from "node:fs";
import { mkdtemp, rm } from "node:fs/promises";
import { mkdirSync, writeFileSync } from "node:fs";
import os from "node:os";
import path from "node:path";
import { fileURLToPath } from "node:url";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const packageRoot = path.join(root, "npm");
const packageJson = JSON.parse(readFileSync(path.join(packageRoot, "package.json"), "utf8"));
const readme = readFileSync(path.join(root, "README.md"), "utf8");
const commands = ["init", "status", "verify", "upgrade", "doctor"];

const failures = [];
let checks = 0;

function check(description, condition, detail) {
  checks += 1;
  if (!condition) {
    failures.push(detail ? `${description}\n      ${detail}` : description);
  }
}

// npm is a .cmd shim on Windows, which execFileSync cannot launch and which recent Node
// refuses to spawn without a shell. Running npm's own JS entry point with the current Node
// works identically on every platform and keeps shell quoting out of the picture.
// Lists the regular files inside a gzipped tar, without shelling out to `tar`, whose
// availability and flavour vary by platform.
function listArchive(file) {
  const buffer = gunzipSync(readFileSync(file));
  const names = [];
  let offset = 0;
  let longName = null;

  while (offset + 512 <= buffer.length) {
    const header = buffer.subarray(offset, offset + 512);
    // Two consecutive zero blocks terminate the archive.
    if (header.every((byte) => byte === 0)) break;

    const field = (start, length) => {
      const raw = header.subarray(start, start + length).toString("utf8");
      const end = raw.indexOf("\0");
      return end === -1 ? raw : raw.slice(0, end);
    };

    const name = field(0, 100);
    const prefix = field(345, 155);
    const typeFlag = String.fromCharCode(header[156]);
    const size = parseInt(field(124, 12).trim() || "0", 8);
    offset += 512 + Math.ceil(size / 512) * 512;

    if (typeFlag === "L") {
      // GNU long name: the real path is this entry's payload.
      longName = buffer
        .subarray(offset - Math.ceil(size / 512) * 512, offset - Math.ceil(size / 512) * 512 + size)
        .toString("utf8")
        .replace(/\0+$/, "");
      continue;
    }

    const resolved = longName ?? (prefix ? `${prefix}/${name}` : name);
    longName = null;

    // Regular files only; skip directories, pax headers and everything else.
    if (typeFlag === "0" || typeFlag === "\0" || header[156] === 0) names.push(resolved);
  }

  return names;
}

function resolveNpm() {
  const fromEnvironment = process.env.npm_execpath;
  if (fromEnvironment && fromEnvironment.endsWith(".js") && existsSync(fromEnvironment)) {
    return [process.execPath, fromEnvironment];
  }

  const nodeDirectory = path.dirname(process.execPath);
  const candidates = [
    // Windows layout: npm sits beside node.exe.
    path.join(nodeDirectory, "node_modules", "npm", "bin", "npm-cli.js"),
    // POSIX layout: npm sits under the installation prefix.
    path.join(nodeDirectory, "..", "lib", "node_modules", "npm", "bin", "npm-cli.js"),
  ];

  for (const candidate of candidates) {
    if (existsSync(candidate)) return [process.execPath, candidate];
  }

  // Last resort: rely on PATH resolution. Works on POSIX; on Windows it is expected to fail
  // loudly rather than silently doing something else.
  return [process.platform === "win32" ? "npm.cmd" : "npm"];
}

const [npmCommand, ...npmPrefixArgs] = resolveNpm();

function npm(args, cwd) {
  return execFileSync(npmCommand, [...npmPrefixArgs, ...args], { cwd, encoding: "utf8" });
}

function run(cli, args, cwd) {
  const result = execFileSync(process.execPath, [cli, ...args], {
    cwd,
    encoding: "utf8",
    env: { ...process.env, NO_COLOR: "1" },
    // The CLI signals outcomes through exit codes, so a non-zero code is data, not a crash.
    stdio: ["ignore", "pipe", "pipe"],
  });
  return { status: 0, stdout: result, stderr: "" };
}

function tryRun(cli, args, cwd) {
  try {
    return run(cli, args, cwd);
  } catch (error) {
    return {
      status: typeof error.status === "number" ? error.status : 1,
      stdout: error.stdout ?? "",
      stderr: error.stderr ?? "",
    };
  }
}

function snapshot(directory) {
  const entries = [];
  const walk = (current) => {
    for (const entry of readdirSync(current, { withFileTypes: true })) {
      if (entry.name === ".git") continue;
      const full = path.join(current, entry.name);
      if (entry.isDirectory()) walk(full);
      else
        entries.push([
          path.relative(directory, full).split(path.sep).join("/"),
          createHash("sha256").update(readFileSync(full)).digest("hex"),
        ]);
    }
  };
  walk(directory);
  return entries.sort(([a], [b]) => a.localeCompare(b));
}

function parseJson(description, text) {
  try {
    return JSON.parse(text);
  } catch (error) {
    check(description, false, `stdout was not valid JSON: ${error.message}`);
    return null;
  }
}

const workspace = await mkdtemp(path.join(os.tmpdir(), "ve-package-test-"));

try {
  // ---------------------------------------------------------------- pack
  if (!existsSync(path.join(packageRoot, "payload", "context", "context.json"))) {
    throw new Error('npm/payload is missing. Run "npm run tool:build" first.');
  }

  const packOutput = npm(["pack", "--pack-destination", workspace, "--silent"], packageRoot);
  const archive = path.join(workspace, packOutput.trim().split("\n").pop().trim());
  check("npm pack produced an archive", existsSync(archive), archive);

  // ------------------------------------------------------- package contents
  const contents = listArchive(archive)
    .map((entry) => entry.replace(/^package\//, ""))
    .filter((entry) => !entry.endsWith("/"));

  const hostRuntime = {
    "linux-x64": "linux-x64",
    "linux-arm64": "linux-arm64",
    "darwin-x64": "osx-x64",
    "darwin-arm64": "osx-arm64",
    "win32-x64": "win-x64",
    "win32-arm64": "win-arm64",
  }[`${process.platform}-${process.arch}`];

  check("the host platform is supported", Boolean(hostRuntime), `${process.platform}-${process.arch}`);

  for (const required of [
    "package.json",
    "README.md",
    "bin/visual-engineering.js",
    "payload/context/context.json",
    "payload/context/UI-FOUNDATIONS.md",
  ]) {
    check(`the package contains ${required}`, contents.includes(required));
  }

  check(
    `the package contains an executable for ${hostRuntime}`,
    contents.some((entry) => entry.startsWith(`runtimes/${hostRuntime}/visual-engineering`)),
    contents.filter((entry) => entry.startsWith("runtimes/")).join(", ")
  );

  const forbidden = contents.filter((entry) =>
    /(^src\/|^tests\/|^scripts\/|^content\/|^dist\/|node_modules\/|\.pdb$|\.tgz$|\.fs$|\.fsproj$|\.env|secret|token|coverage\/)/i.test(
      entry
    )
  );
  check("the package publishes nothing unnecessary or sensitive", forbidden.length === 0, forbidden.join(", "));

  // ------------------------------------------------------------- install
  const installRoot = path.join(workspace, "consumer");
  mkdirSync(installRoot, { recursive: true });
  writeFileSync(
    path.join(installRoot, "package.json"),
    JSON.stringify({ name: "package-under-test", version: "1.0.0", private: true }, null, 2)
  );
  npm(["install", "--no-audit", "--no-fund", "--silent", archive], installRoot);

  const cli = path.join(
    installRoot,
    "node_modules",
    "@echelon-foundry",
    "visual-engineering",
    "bin",
    "visual-engineering.js"
  );
  check("the installed package exposes its launcher", existsSync(cli), cli);
  const binDirectory = path.join(installRoot, "node_modules", ".bin");
  check(
    "the bin mapping is installed",
    // npm writes a shell shim on POSIX and a .cmd shim on Windows.
    ["visual-engineering", "visual-engineering.cmd"].some((shim) =>
      existsSync(path.join(binDirectory, shim))
    ),
    existsSync(binDirectory) ? readdirSync(binDirectory).join(", ") : `${binDirectory} is missing`
  );

  // ------------------------------------------------------------ metadata
  const version = tryRun(cli, ["--version"], installRoot);
  check("--version exits 0", version.status === 0, version.stderr);
  check(
    "--version matches the npm package version",
    version.stdout.trim() === packageJson.version,
    `${version.stdout.trim()} !== ${packageJson.version}`
  );

  const help = tryRun(cli, ["--help"], installRoot);
  check("--help exits 0", help.status === 0, help.stderr);
  for (const command of commands) {
    check(`--help lists ${command}`, help.stdout.includes(command));
    const commandHelp = tryRun(cli, [command, "--help"], installRoot);
    check(`${command} --help exits 0`, commandHelp.status === 0, commandHelp.stderr);
    check(`${command} --help documents side effects`, commandHelp.stdout.includes("SIDE EFFECTS"));
    check(
      `${command} --help agrees with README on the package name`,
      commandHelp.stdout.includes(packageJson.name)
    );
  }

  check("no arguments prints help", tryRun(cli, [], installRoot).status === 0);
  check("an unknown command exits 2", tryRun(cli, ["frobnicate"], installRoot).status === 2);
  check("an unknown option exits 2", tryRun(cli, ["status", "--nope"], installRoot).status === 2);

  // --------------------------------------------- README and help agreement
  for (const command of commands) {
    check(`README documents ${command}`, readme.includes(`visual-engineering ${command}`));
  }
  for (const code of [0, 1, 2, 3, 4, 5, 6, 7]) {
    check(`README documents exit code ${code}`, new RegExp(`\\|\\s*${code}\\s*\\|`).test(readme));
    check(`help documents exit code ${code}`, new RegExp(`\\s${code}\\s{2}`).test(help.stdout));
  }
  check("README shows the quick start init command", readme.includes(`npx ${packageJson.name} init`));
  check("README shows the quick start status command", readme.includes(`npx ${packageJson.name} status`));
  check("README shows the quick start verify command", readme.includes(`npx ${packageJson.name} verify`));

  // ------------------------------------------------------ clean repository
  const repository = path.join(workspace, "repo");
  mkdirSync(path.join(repository, ".git"), { recursive: true });

  const emptyStatus = tryRun(cli, ["status", "--json"], repository);
  check("status on a clean repository exits 0", emptyStatus.status === 0, emptyStatus.stderr);
  const emptyStatusJson = parseJson("status --json emits valid JSON", emptyStatus.stdout);
  check(
    "status reports not-installed",
    emptyStatusJson?.state?.status === "not-installed",
    JSON.stringify(emptyStatusJson?.state)
  );
  check("status --json writes nothing else to stdout", emptyStatus.stdout.trimStart().startsWith("{"));
  check("the JSON envelope is complete", ["schemaVersion", "tool", "package", "command", "cliVersion", "exitCode"].every(
    (field) => emptyStatusJson && field in emptyStatusJson
  ));

  const beforeDryRun = snapshot(repository);
  const dryRun = tryRun(cli, ["init", "--dry-run", "--json"], repository);
  check("init --dry-run exits 0", dryRun.status === 0, dryRun.stderr);
  const dryRunJson = parseJson("init --dry-run --json emits valid JSON", dryRun.stdout);
  check("the dry run reports a plan", (dryRunJson?.plan?.changeCount ?? 0) > 0);
  check("the dry run applies nothing", dryRunJson?.execution?.applied === false);
  check(
    "the dry run changed no files",
    JSON.stringify(beforeDryRun) === JSON.stringify(snapshot(repository))
  );

  const checkRun = tryRun(cli, ["init", "--check"], repository);
  check("init --check exits 4 when changes are required", checkRun.status === 4, String(checkRun.status));
  check(
    "init --check changed no files",
    JSON.stringify(beforeDryRun) === JSON.stringify(snapshot(repository))
  );

  // ------------------------------------------------------------------ init
  const firstInit = tryRun(cli, ["init"], repository);
  check("init exits 0", firstInit.status === 0, firstInit.stderr);
  for (const installed of [
    ".echelon/visual-engineering.json",
    ".echelon/visual-engineering.config.json",
    ".visual-engineering/UI-FOUNDATIONS.md",
    ".visual-engineering/RESEARCH-INDEX.md",
    "AGENTS.md",
    ".gitignore",
  ]) {
    check(`init created ${installed}`, existsSync(path.join(repository, installed)));
  }

  const afterFirstInit = snapshot(repository);
  const secondInit = tryRun(cli, ["init"], repository);
  check("a second init exits 0", secondInit.status === 0, secondInit.stderr);
  check("a second init reports no changes", secondInit.stdout.includes("No changes required"), secondInit.stdout);
  check(
    "a second init changed nothing",
    JSON.stringify(afterFirstInit) === JSON.stringify(snapshot(repository))
  );
  check("init --check exits 0 once installed", tryRun(cli, ["init", "--check"], repository).status === 0);

  // -------------------------------------------------- read only commands
  for (const [command, args] of [
    ["status", ["status"]],
    ["verify", ["verify"]],
    ["verify --strict", ["verify", "--strict"]],
    ["doctor", ["doctor"]],
    ["upgrade", ["upgrade"]],
  ]) {
    const result = tryRun(cli, args, repository);
    check(`${command} exits 0 on a healthy installation`, result.status === 0, result.stderr || result.stdout);
  }

  for (const command of ["status", "verify", "doctor"]) {
    const before = snapshot(repository);
    tryRun(cli, [command, "--json"], repository);
    check(`${command} did not modify the repository`, JSON.stringify(before) === JSON.stringify(snapshot(repository)));
    const json = parseJson(`${command} --json emits valid JSON`, tryRun(cli, [command, "--json"], repository).stdout);
    check(`${command} --json carries the envelope`, json?.schemaVersion === 1 && json?.command === command);
  }

  // ------------------------------------------------------------- damage
  rmSync(path.join(repository, ".visual-engineering", "UI-FOUNDATIONS.md"));
  check("verify fails on a missing file", tryRun(cli, ["verify"], repository).status === 3);
  const damagedDoctor = tryRun(cli, ["doctor", "--json"], repository);
  check("doctor fails on a missing file", damagedDoctor.status === 3);
  const damagedDoctorJson = parseJson("doctor --json emits valid JSON when unhealthy", damagedDoctor.stdout);
  check(
    "doctor identifies the missing file",
    (damagedDoctorJson?.findings ?? []).some(
      (finding) => finding.code === "file-missing" && finding.severity === "error" && finding.remedy
    ),
    JSON.stringify(damagedDoctorJson?.findings?.map((f) => f.code))
  );
  check("init repairs the damage", tryRun(cli, ["init"], repository).status === 0);
  check("verify passes after repair", tryRun(cli, ["verify", "--strict"], repository).status === 0);

  writeFileSync(path.join(repository, ".visual-engineering", "UI-FOUNDATIONS.md"), "hand edited\n");
  const blocked = tryRun(cli, ["init"], repository);
  check("a locally modified tool owned file blocks init with exit 5", blocked.status === 5, String(blocked.status));
  check(
    "the blocked run did not replace the local edit",
    readFileSync(path.join(repository, ".visual-engineering", "UI-FOUNDATIONS.md"), "utf8") === "hand edited\n"
  );
  check("--force replaces it", tryRun(cli, ["init", "--force"], repository).status === 0);
  check("verify passes after a forced repair", tryRun(cli, ["verify", "--strict"], repository).status === 0);

  // -------------------------------------------------------- legacy fixture
  const legacy = path.join(workspace, "legacy");
  mkdirSync(path.join(legacy, ".git"), { recursive: true });
  mkdirSync(path.join(legacy, ".visual-engineering"), { recursive: true });

  const payloadContext = path.join(packageRoot, "payload", "context");
  for (const file of readdirSync(payloadContext)) {
    if (statSync(path.join(payloadContext, file)).isFile()) {
      writeFileSync(
        path.join(legacy, ".visual-engineering", file),
        readFileSync(path.join(payloadContext, file))
      );
    }
  }
  const legacyContext = JSON.parse(
    readFileSync(path.join(legacy, ".visual-engineering", "context.json"), "utf8")
  );
  legacyContext.contextVersion = "0.0.0-legacy";
  writeFileSync(
    path.join(legacy, ".visual-engineering", "context.json"),
    `${JSON.stringify(legacyContext, null, 2)}\n`
  );
  writeFileSync(path.join(legacy, "AGENTS.md"), "# House rules\n\nNever force push.\n");
  writeFileSync(path.join(legacy, ".gitignore"), "node_modules/\ncoverage/\n");

  const legacyStatus = parseJson(
    "status --json on a legacy installation emits valid JSON",
    tryRun(cli, ["status", "--json"], legacy).stdout
  );
  check(
    "a legacy installation is detected as upgrade-required at configuration version 1",
    legacyStatus?.state?.status === "upgrade-required" && legacyStatus?.configurationVersion === 1,
    JSON.stringify(legacyStatus?.state)
  );

  const beforeUpgrade = snapshot(legacy);
  check("upgrade --dry-run exits 0", tryRun(cli, ["upgrade", "--dry-run"], legacy).status === 0);
  check(
    "upgrade --dry-run changed nothing",
    JSON.stringify(beforeUpgrade) === JSON.stringify(snapshot(legacy))
  );

  const upgrade = tryRun(cli, ["upgrade", "--json"], legacy);
  check("upgrade exits 0", upgrade.status === 0, upgrade.stderr);
  const upgradeJson = parseJson("upgrade --json emits valid JSON", upgrade.stdout);
  check(
    "upgrade ran both migrations in order",
    JSON.stringify(upgradeJson?.plan?.migrations) === JSON.stringify([{ from: 1, to: 2 }, { from: 2, to: 3 }]),
    JSON.stringify(upgradeJson?.plan?.migrations)
  );
  check(
    "upgrade preserved user content in AGENTS.md",
    readFileSync(path.join(legacy, "AGENTS.md"), "utf8").includes("Never force push.")
  );
  check(
    "upgrade preserved user content in .gitignore",
    readFileSync(path.join(legacy, ".gitignore"), "utf8").includes("coverage/")
  );
  check("the upgraded installation verifies strictly", tryRun(cli, ["verify", "--strict"], legacy).status === 0);
  check("upgrade is idempotent", tryRun(cli, ["upgrade", "--check"], legacy).status === 0);

  // ------------------------------------------------- environment failures
  const missingRepo = tryRun(cli, ["status", "--repo", path.join(workspace, "does-not-exist")], installRoot);
  check("a missing repository exits 6", missingRepo.status === 6, String(missingRepo.status));
} finally {
  await rm(workspace, { recursive: true, force: true });
}

if (failures.length > 0) {
  process.stderr.write(`\n${failures.length} of ${checks} package checks failed:\n`);
  for (const failure of failures) process.stderr.write(`  - ${failure}\n`);
  process.exitCode = 1;
} else {
  process.stdout.write(`All ${checks} packed artifact checks passed for ${packageJson.name}@${packageJson.version}.\n`);
}
