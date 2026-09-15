// Verifies what can be verified before any of the release's packages is published.
//
// The tool ships as seven packages -- one root package and six platform packages, all at the
// same version. `npm publish` is not transactional across them, so a problem discovered on
// the seventh package leaves six versions permanently taken on the registry and
// unpublishable under the same number. This runs first and refuses the whole release when it
// can see that the release would fail.
//
// What it cannot do is prove that the token may publish. npm exposes no such endpoint:
// `npm access list packages <scope>` returns each package's own public access setting rather
// than the caller's permission, `npm org ls` exits 0 with empty output when unauthenticated,
// and a publish authorization failure comes back as 404 rather than 403. The only true test
// of publish permission is a publish. So the preflight rules out the failures it can see,
// and the publish step reports precisely which packages went out if the rest still fails.
//
// Every check runs even after an earlier one fails, so a single run reports everything that
// is wrong rather than one problem per attempt.

import { execFileSync } from "node:child_process";
import { existsSync, readFileSync } from "node:fs";
import path from "node:path";
import { fileURLToPath } from "node:url";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const registry = process.env.VE_PREFLIGHT_REGISTRY ?? "https://registry.npmjs.org";

// npm is a .cmd shim on Windows, which execFileSync cannot launch and which recent Node
// refuses to spawn without a shell. Running npm's own JS entry point with the current Node
// works identically on every platform and keeps shell quoting out of the picture.
const resolveNpm = () => {
  const fromEnvironment = process.env.npm_execpath;
  if (fromEnvironment && fromEnvironment.endsWith(".js") && existsSync(fromEnvironment)) {
    return [process.execPath, fromEnvironment];
  }

  const nodeDirectory = path.dirname(process.execPath);
  const candidates = [
    path.join(nodeDirectory, "node_modules", "npm", "bin", "npm-cli.js"),
    path.join(nodeDirectory, "..", "lib", "node_modules", "npm", "bin", "npm-cli.js"),
  ];

  return (
    candidates.filter(existsSync).map((candidate) => [process.execPath, candidate])[0] ?? [
      process.platform === "win32" ? "npm.cmd" : "npm",
    ]
  );
};

const [npmCommand, ...npmPrefixArgs] = resolveNpm();

// A failed npm invocation is data here, not a crash: its exit code and output are what the
// checks reason about.
const npm = (args) => {
  try {
    const stdout = execFileSync(npmCommand, [...npmPrefixArgs, ...args, "--registry", registry], {
      cwd: root,
      encoding: "utf8",
      stdio: ["ignore", "pipe", "pipe"],
    });
    return { ok: true, stdout, stderr: "" };
  } catch (error) {
    return {
      ok: false,
      stdout: error.stdout ?? "",
      stderr: error.stderr ?? String(error.message ?? error),
    };
  }
};

const indent = (text) =>
  String(text ?? "")
    .trim()
    .split("\n")
    .filter((line) => line.trim() !== "")
    .map((line) => `    ${line}`)
    .join("\n");

const failure = (title, ...detail) => ({ level: "error", title, detail });
const warning = (title, ...detail) => ({ level: "warning", title, detail });

// The staged package is the source of truth for what is about to be published: the publish
// job publishes exactly npm/platforms/*/ and npm/, and the root package pins each platform
// package to its own version. Reading it here keeps the preflight and the publish in step
// without repeating the package list in the workflow.
const readPlan = () => {
  const manifestPath = path.join(root, "npm", "package.json");
  if (!existsSync(manifestPath)) {
    throw new Error(
      `No staged package at ${manifestPath}. Run "npm run tool:build -- --version <version>" first.`,
    );
  }

  const { name, version, optionalDependencies = {} } = JSON.parse(
    readFileSync(manifestPath, "utf8"),
  );

  const platforms = Object.entries(optionalDependencies).map(([dependency, pinned]) => ({
    name: dependency,
    version: pinned,
  }));

  return {
    scope: name.startsWith("@") ? name.slice(0, name.indexOf("/")) : null,
    version,
    packages: [{ name, version }, ...platforms],
    mismatched: platforms.filter((platform) => platform.version !== version),
  };
};

// The one check that genuinely exercises the token. It is the only npm command that reports
// the caller's own identity and that a granular token can still answer.
const checkAuthentication = () => {
  const result = npm(["whoami"]);
  if (result.ok) {
    console.log(`  token authenticates as ${result.stdout.trim()}`);
    return [];
  }

  return [
    failure(
      "The npm token does not authenticate.",
      "npm whoami failed, so the NPM_TOKEN secret is missing, expired, or revoked. Every publish in this release would fail.",
      indent(result.stderr),
    ),
  ];
};

// Not an authorization check -- the registry answers this for anyone. It is here because it
// tells us whether this is a first publish into the scope, which is the case that needs
// scope-level write access and the case where a granular token most often turns out to be
// configured wrongly.
const inspectScope = (scope, packages) => {
  if (scope === null) return [];

  const result = npm(["access", "list", "packages", scope, "--json"]);
  if (!result.ok) {
    return [
      warning(
        `Could not list the packages in ${scope}.`,
        "This is not an authorization check, so the release continues. It only means the scope listing was unavailable.",
        indent(result.stderr),
      ),
    ];
  }

  const existing = new Set(Object.keys(JSON.parse(result.stdout)));
  const fresh = packages.filter((entry) => !existing.has(entry.name));

  console.log(`  ${scope} currently holds ${existing.size} packages`);
  console.log(`  ${fresh.length} of this release's ${packages.length} packages are new to it`);

  if (fresh.length === 0) return [];

  return [
    {
      level: "notice",
      title: `${fresh.length} of the release's packages do not exist yet, so this publish needs scope-level write access to ${scope}.`,
      detail: [
        "A granular npm token cannot be scoped to packages that do not exist yet. If this token lists individual packages rather than the whole scope, the publish will fail, and it will fail as a 404 rather than a 403.",
        "npm exposes no way to check publish permission without publishing, so this is a caution, not a verdict.",
      ],
    },
  ];
};

// A version that already exists cannot be republished. Finding one now is what stops a
// re-run of a partially published release from failing halfway through a second time.
const checkVersionAvailable = async ({ name, version }) => {
  const url = `${registry}/${name.replace("/", "%2f")}/${encodeURIComponent(version)}`;

  const response = await fetch(url, { headers: { accept: "application/json" } }).catch(
    (error) => error,
  );

  if (response instanceof Error) {
    return {
      line: `  ${name}@${version}: registry unreachable`,
      problems: [
        warning(
          `Could not reach the registry to check ${name}@${version}.`,
          indent(response.message),
        ),
      ],
    };
  }

  if (response.status === 404) {
    return { line: `  ${name}@${version} is free`, problems: [] };
  }

  if (response.status === 200) {
    return {
      line: `  ${name}@${version} is ALREADY PUBLISHED`,
      problems: [
        failure(
          `${name}@${version} is already published.`,
          "A published version cannot be replaced. If an earlier run published some of these packages and then failed, those versions are permanently taken: release under a new version rather than retrying this one.",
        ),
      ],
    };
  }

  return {
    line: `  ${name}@${version}: registry answered ${response.status}`,
    problems: [
      warning(
        `The registry answered ${response.status} for ${name}@${version}.`,
        "Availability could not be determined, so the release continues.",
      ),
    ],
  };
};

const report = (problems) => {
  for (const problem of problems) {
    const detail = problem.detail.filter(Boolean).join("\n");
    console.error(`::${problem.level}::${problem.title}`);
    if (detail !== "") console.error(detail);
  }
};

const main = async () => {
  const plan = readPlan();

  console.log(
    `Preflight for ${plan.packages.length} packages at ${plan.version} against ${registry}\n`,
  );

  const mismatches = plan.mismatched.map((platform) =>
    failure(
      `${platform.name} is pinned to ${platform.version}, not ${plan.version}.`,
      "The staged package is internally inconsistent. Re-stage it with npm run tool:build.",
    ),
  );

  const authentication = checkAuthentication();
  const scope = inspectScope(plan.scope, plan.packages);

  // Checked together for speed, but reported in the order the packages are declared so the
  // log reads the same way on every run.
  const availability = await Promise.all(plan.packages.map(checkVersionAvailable));
  for (const result of availability) console.log(result.line);

  const problems = [
    ...mismatches,
    ...authentication,
    ...scope,
    ...availability.flatMap((result) => result.problems),
  ];

  console.log("");
  report(problems);

  const fatal = problems.filter((problem) => problem.level === "error");
  if (fatal.length > 0) {
    console.error(
      `\nPreflight failed with ${fatal.length} blocking problem${fatal.length === 1 ? "" : "s"}. Nothing was published.`,
    );
    process.exit(1);
  }

  console.log(
    `\nPreflight passed. Publishing ${plan.packages.length} packages at ${plan.version}.`,
  );
};

main().catch((error) => {
  console.error(`::error::${error.message}`);
  process.exit(1);
});
