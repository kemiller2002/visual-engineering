import { readFile } from "node:fs/promises";
import { pathToFileURL } from "node:url";

const GROUPS = ["states", "seams", "environments", "fixtures"];
const SEVERITIES = ["critical", "high", "medium", "low"];
const COVERAGE = new Set(["passed", "failed", "untested", "not-applicable"]);

export function evaluatePolishEvidence(doc) {
  const errors = [];
  if (!doc || typeof doc !== "object" || Array.isArray(doc)) errors.push("evidence must be an object");
  if (doc?.schemaVersion !== 1) errors.push("schemaVersion must be 1");
  if (!doc?.surface || typeof doc.surface !== "string") errors.push("surface is required");
  if (!Array.isArray(doc?.dimensions) || doc.dimensions.length === 0) errors.push("at least one dimension is required");

  const counts = { passed: 0, failed: 0, untested: 0, "not-applicable": 0 };
  for (const group of GROUPS) {
    if (!Array.isArray(doc?.[group])) {
      errors.push(group + " must be an array");
      continue;
    }
    for (const item of doc[group]) {
      if (!item?.id || !COVERAGE.has(item?.status)) {
        errors.push(group + " contains invalid coverage");
        continue;
      }
      counts[item.status]++;
      if (item.status === "not-applicable" && !item.rationale) errors.push(group + "/" + item.id + " needs not-applicable rationale");
      if (item.status === "passed" && (!Array.isArray(item.evidence) || item.evidence.length === 0)) errors.push(group + "/" + item.id + " passed without evidence");
    }
  }

  if (!Array.isArray(doc?.unknowns)) errors.push("unknowns must be an array");
  if (!Array.isArray(doc?.findings)) errors.push("findings must be an array");

  const open = { critical: 0, high: 0, medium: 0, low: 0 };
  for (const finding of doc?.findings || []) {
    if (!finding?.id || !SEVERITIES.includes(finding?.severity) || !["open","resolved","accepted"].includes(finding?.status) || !finding?.summary) {
      errors.push("invalid finding");
      continue;
    }
    if (finding.status === "open") open[finding.severity]++;
  }

  const tested = counts.passed + counts.failed;
  const applicable = tested + counts.untested;
  const coveragePercent = applicable === 0 ? null : Math.round((tested / applicable) * 10000) / 100;
  const hasUnknown = (doc?.unknowns?.length || 0) > 0 || counts.untested > 0;
  const hardFailure = counts.failed > 0 || open.critical > 0;
  const highUnresolved = open.high > 0;

  let computedDisposition = "pass";
  if (errors.length || hardFailure) computedDisposition = "fail";
  else if (hasUnknown || highUnresolved || applicable === 0) computedDisposition = "incomplete";

  if (doc?.disposition && doc.disposition !== computedDisposition) {
    errors.push("declared disposition " + doc.disposition + " does not match computed disposition " + computedDisposition);
    computedDisposition = "fail";
  }

  return { disposition: computedDisposition, coveragePercent, counts, openFindings: open, errors };
}

async function main() {
  const args = process.argv.slice(2);
  const json = args.includes("--json");
  const path = args.find((arg) => !arg.startsWith("--"));
  if (!path) {
    process.stderr.write("usage: node scripts/application-polish-gate.mjs <evidence.json> [--json]\n");
    process.exitCode = 2;
    return;
  }
  let doc;
  try { doc = JSON.parse(await readFile(path, "utf8")); }
  catch (error) {
    process.stderr.write("unable to read polish evidence: " + error.message + "\n");
    process.exitCode = 2;
    return;
  }
  const result = evaluatePolishEvidence(doc);
  if (json) process.stdout.write(JSON.stringify(result, null, 2) + "\n");
  else {
    process.stdout.write("Application polish: " + result.disposition.toUpperCase() + "\n");
    process.stdout.write("Coverage: " + (result.coveragePercent === null ? "n/a" : result.coveragePercent + "%") + "\n");
    process.stdout.write("Passed " + result.counts.passed + ", failed " + result.counts.failed + ", untested " + result.counts.untested + ", N/A " + result.counts["not-applicable"] + "\n");
    for (const error of result.errors) process.stdout.write("ERROR: " + error + "\n");
  }
  process.exitCode = result.disposition === "pass" ? 0 : result.disposition === "incomplete" ? 3 : 4;
}

if (process.argv[1] && import.meta.url === pathToFileURL(process.argv[1]).href) await main();
