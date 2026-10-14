import { execFileSync } from "node:child_process";
import crypto from "node:crypto";
import { readFileSync, writeFileSync } from "node:fs";
import path from "node:path";
import { pathToFileURL } from "node:url";

// Assigns canonical identifiers to published documents that lack one and
// checks that none remain. The research publisher decides which documents are
// published; this script reads its validation diagnostics rather than
// re-deriving publication rules. Policy: DF-VE-ID-2026-5C1E.

export const DIAGNOSTICS_PATH = "build-reports/validation-diagnostics.json";

export const ID_PATTERN = /^[A-Z]{2}-[A-Z0-9-]+-\d{4}-(?:\d{4}|[0-9A-F]{4})$/;

const projectAreas = {
  "project-atlas": "VE-ATL",
  "composition-science": "VE-COMP",
  "product-genome": "VE-PG",
  "design-library": "VE-DL",
};

// Folder kinds under content/projects/<project>/ that hold evidence records.
const evidenceKinds = new Set(["evidence-registry"]);

const segmentsOf = (sourcePath) => sourcePath.split("/");

const projectRule = ([, , project, kind]) => ({
  prefix: evidenceKinds.has(kind) ? "EV" : kind === "template" ? "GV" : "RR",
  area: projectAreas[project] ?? `VE-${project.toUpperCase().replace(/[^A-Z0-9]+/g, "-")}`,
});

const rules = [
  [(s) => s[0] === "content" && s[1] === "concepts", () => ({ prefix: "CN", area: "VE" })],
  [(s) => s[0] === "content" && s[1] === "projects" && s.length > 4, projectRule],
  [(s) => s[0] === "content" && s[1] === "themes" && s[2] === "research", () => ({ prefix: "RR", area: "VE-THM" })],
  [(s) => s[0] === "research" && s.at(-1) !== "README.md", () => ({ prefix: "RR", area: "VE" })],
  [() => true, () => ({ prefix: "GV", area: "VE" })],
];

export const classify = (sourcePath) => {
  const segments = segmentsOf(sourcePath);
  const [, rule] = rules.find(([matches]) => matches(segments));
  return rule(segments);
};

export const formatId = ({ prefix, area }, year, token) => `${prefix}-${area}-${year}-${token}`;

export const randomToken = () => crypto.randomBytes(2).toString("hex").toUpperCase();

// Draws tokens until the identifier is unused; the caller's `taken` set is the
// uniqueness authority, so gaps and retries are expected and harmless.
export const allocate = (stem, taken, nextToken) => {
  const candidate = formatId(stem, stem.year, nextToken());
  return taken.has(candidate) ? allocate(stem, taken, nextToken) : candidate;
};

export const planAssignments = (documents, existingIds, nextToken = randomToken) =>
  documents.reduce(
    ({ taken, assignments }, { sourcePath, year }) => {
      const id = allocate({ ...classify(sourcePath), year }, taken, nextToken);
      return { taken: new Set([...taken, id]), assignments: [...assignments, { sourcePath, id }] };
    },
    { taken: new Set(existingIds), assignments: [] },
  ).assignments;

const FRONT_MATTER = /^---\r?\n([\s\S]*?)\r?\n---(?:\r?\n|$)/;

export const frontMatterId = (source) => {
  const block = source.match(FRONT_MATTER)?.[1] ?? "";
  return block.match(/^id:\s*["']?([^"'\s#]+)/m)?.[1] ?? null;
};

export const withId = (source, id) =>
  FRONT_MATTER.test(source)
    ? source.replace(/^---(\r?\n)/, (_, newline) => `---${newline}id: ${id}${newline}`)
    : `---\nid: ${id}\n---\n\n${source}`;

export const missingIdPaths = (diagnostics) =>
  [...new Set(diagnostics.filter(({ code }) => code === "missing-id").map(({ sourcePath }) => sourcePath))].sort();

const readJson = (file) => JSON.parse(readFileSync(file, "utf8"));

const diagnosticsFrom = (report) => (Array.isArray(report) ? report : report.diagnostics ?? []);

const git = (args) => execFileSync("git", args, { encoding: "utf8" }).trim();

const trackedMarkdown = () => git(["ls-files", "*.md"]).split("\n").filter(Boolean);

const firstCommitYear = (sourcePath) =>
  git(["log", "--follow", "--diff-filter=A", "--format=%ad", "--date=format:%Y", "--", sourcePath])
    .split("\n")
    .filter(Boolean)
    .at(-1) ?? String(new Date().getUTCFullYear());

const existingIds = (files) =>
  files.map((file) => frontMatterId(readFileSync(file, "utf8"))).filter(Boolean);

const check = (missing) =>
  missing.length === 0
    ? { code: 0, lines: ["Every published document has a canonical identifier."] }
    : {
        code: 1,
        lines: [
          `${missing.length} published document(s) lack a canonical identifier (DF-VE-ID-2026-5C1E):`,
          ...missing.map((sourcePath) => `  ${sourcePath}`),
          "Run `npm run research:ids -- --write` to assign them, then commit the result.",
        ],
      };

const assign = (missing, write) => {
  const assignments = planAssignments(
    missing.map((sourcePath) => ({ sourcePath, year: firstCommitYear(sourcePath) })),
    existingIds(trackedMarkdown()),
  );
  if (write) {
    assignments.forEach(({ sourcePath, id }) =>
      writeFileSync(sourcePath, withId(readFileSync(sourcePath, "utf8"), id)),
    );
  }
  return {
    code: 0,
    lines: [
      ...assignments.map(({ sourcePath, id }) => `${id}  ${sourcePath}`),
      `${assignments.length} identifier(s) ${write ? "written" : "planned (dry run; pass --write to apply)"}.`,
    ],
  };
};

const main = (args) => {
  const missing = missingIdPaths(diagnosticsFrom(readJson(DIAGNOSTICS_PATH)));
  const result = args.includes("--check") ? check(missing) : assign(missing, args.includes("--write"));
  result.lines.forEach((line) => console.log(line));
  process.exitCode = result.code;
};

if (import.meta.url === pathToFileURL(path.resolve(process.argv[1] ?? "")).href) {
  main(process.argv.slice(2));
}
