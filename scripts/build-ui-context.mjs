import { createHash } from "node:crypto";
import { execFileSync } from "node:child_process";
import { cp, mkdir, readFile, rm, writeFile } from "node:fs/promises";
import path from "node:path";
import { fileURLToPath } from "node:url";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const catalogPath = path.join(root, "dist/data/research-catalog.json");
const packageRoot = path.join(root, "packages/visual-engineering-context");
const outputDir = path.join(packageRoot, "context");
const sourceDir = path.join(root, "agent-context");
const packageJson = JSON.parse(await readFile(path.join(packageRoot, "package.json"), "utf8"));
const catalog = JSON.parse(await readFile(catalogPath, "utf8"));

const contextVersion = process.env.VE_CONTEXT_VERSION || packageJson.version;
const sourceCommit = process.env.GITHUB_SHA || process.env.VE_SOURCE_COMMIT || currentCommit();
const generatedAt = process.env.VE_GENERATED_AT || new Date().toISOString();
const repositoryUrl = "https://github.com/kemiller2002/Visual-Engineering";
const publishedResearchUrl = "https://kemiller2002.github.io/Visual-Engineering";

function currentCommit() {
  try {
    return execFileSync("git", ["rev-parse", "HEAD"], { cwd: root, encoding: "utf8" }).trim();
  } catch {
    return "unknown";
  }
}

function sha256(value) {
  return createHash("sha256").update(value).digest("hex");
}

function normalize(value) {
  return String(value || "").toLowerCase();
}

const uiTerms = [
  "accessibility", "attention", "cognition", "color", "component", "composition",
  "density", "design", "ergonomic", "form", "hierarchy", "human-factors",
  "information-architecture", "interaction", "layout", "legibility", "perception",
  "product", "proximity", "reading", "semantic", "spacing", "typography",
  "usability", "visual", "wayfinding", "working-memory",
];

function isRelevant(record) {
  if (record.sourcePath?.startsWith("content/concepts/")) return true;
  if (!record.sourcePath?.startsWith("content/projects/")) return false;
  const haystack = normalize([
    record.title,
    record.summary,
    record.researchArea,
    ...(record.tags || []),
    ...(record.keywords || []),
    ...(record.discipline || []),
    ...(record.headings || []).map((heading) => heading.text),
  ].join(" "));
  return uiTerms.some((term) => haystack.includes(term));
}

function markdownSummary(record) {
  const summary = String(record.summary || "").replace(/\s+/g, " ").trim();
  if (summary) return summary;
  const headings = (record.headings || [])
    .filter((heading) => heading.depth === 2)
    .slice(0, 4)
    .map((heading) => heading.text);
  return headings.length
    ? `Covers ${headings.join("; ")}.`
    : "Consult the canonical source for its current findings and limitations.";
}

const records = catalog.records
  .filter(isRelevant)
  .sort((a, b) =>
    String(a.title || a.sourcePath).localeCompare(String(b.title || b.sourcePath))
    || String(a.id || a.sourcePath).localeCompare(String(b.id || b.sourcePath)))
  .map((record) => ({
    id: record.id || `PATH-${sha256(record.sourcePath).slice(0, 12).toUpperCase()}`,
    title: record.title || record.sourcePath,
    status: record.status,
    updated: record.updated,
    summary: markdownSummary(record),
    tags: record.tags || [],
    sourcePath: record.sourcePath,
    sourceUrl: `${repositoryUrl}/blob/${sourceCommit}/${record.sourcePath}`,
    publishedUrl: `${publishedResearchUrl}${record.url}`,
    contentHash: record.contentHash,
  }));

const researchIndex = `# Current Visual Engineering UI Research Index

Generated from Visual Engineering research catalog ${catalog.schemaVersion}. This index updates whenever the context package is published.

- Context version: \`${contextVersion}\`
- Source commit: \`${sourceCommit}\`
- Generated: \`${generatedAt}\`
- Relevant documents: ${records.length}

The operational briefing is intentionally concise. Use this index to inspect provenance, uncertainty, and deeper evidence before consequential or disputed UI decisions.

${records.map((record) => `## ${record.id}: ${record.title}

- Status: ${record.status || "unspecified"}
- Updated: ${record.updated || "unspecified"}
- Tags: ${record.tags.length ? record.tags.join(", ") : "none"}
- Canonical source: [${record.sourcePath}](${record.sourceUrl})
- Published research: [Open document](${record.publishedUrl})

${record.summary}
`).join("\n")}
`;

await rm(outputDir, { recursive: true, force: true });
await mkdir(outputDir, { recursive: true });

const sourceFiles = [
  "AGENT-INSTRUCTIONS.md",
  "UI-FOUNDATIONS.md",
  "UI-DECISION-CHECKLIST.md",
  "UI-ANTI-PATTERNS.md",
  "APPLICATION-POLISH.md",
];

const artifacts = [];
for (const file of sourceFiles) {
  const content = await readFile(path.join(sourceDir, file), "utf8");
  await writeFile(path.join(outputDir, file), content);
  artifacts.push({ file, sha256: sha256(content) });
}

await writeFile(path.join(outputDir, "RESEARCH-INDEX.md"), researchIndex);
artifacts.push({ file: "RESEARCH-INDEX.md", sha256: sha256(researchIndex) });

const sources = {
  schemaVersion: "1.0",
  contextVersion,
  sourceRepository: repositoryUrl,
  sourceCommit,
  generatedAt,
  records,
};
const sourcesJson = `${JSON.stringify(sources, null, 2)}\n`;
await writeFile(path.join(outputDir, "sources.json"), sourcesJson);
artifacts.push({ file: "sources.json", sha256: sha256(sourcesJson) });

const context = {
  schemaVersion: "1.0",
  contextVersion,
  sourceRepository: repositoryUrl,
  sourceCommit,
  generatedAt,
  researchCatalogVersion: catalog.schemaVersion,
  researchDocuments: records.length,
  profile: "ui-foundations",
  classification: "public",
  artifacts,
};
await writeFile(path.join(outputDir, "context.json"), `${JSON.stringify(context, null, 2)}\n`);

// The package has always declared MIT; stage the repository's licence file so the published
// package carries the text too, from a single authoritative source.
await cp(path.join(root, "LICENSE"), path.join(packageRoot, "LICENSE"));

process.stdout.write(`Built UI context ${contextVersion} from ${records.length} research documents.\n`);
