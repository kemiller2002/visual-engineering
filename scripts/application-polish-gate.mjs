import { readFile } from "node:fs/promises";
import { pathToFileURL } from "node:url";
import { evaluateMotionEvidence } from "./application-polish-motion.mjs";

const GROUPS = ["states", "seams", "environments", "fixtures"];
const SEVERITIES = ["critical", "high", "medium", "low"];
const COVERAGE = new Set(["passed", "failed", "untested", "not-applicable"]);

export function evaluatePolishEvidence(doc, profile = null) {
  const errors = [];
  const missing = { dimensions: [], fixtures: [] };
  if (!doc || typeof doc !== "object" || Array.isArray(doc)) errors.push("evidence must be an object");
  if (doc?.schemaVersion !== 1) errors.push("schemaVersion must be 1");
  if (!doc?.surface || typeof doc.surface !== "string") errors.push("surface is required");
  if (!Array.isArray(doc?.dimensions) || doc.dimensions.length === 0) errors.push("at least one dimension is required");

  if (profile) {
    if (doc?.profile && doc.profile !== profile.id) errors.push("evidence profile " + doc.profile + " does not match loaded profile " + profile.id);
    const dimensions = new Set(doc?.dimensions || []);
    missing.dimensions = (profile.requiredDimensions || []).filter((id) => !dimensions.has(id));
  }

  const counts = { passed: 0, failed: 0, untested: 0, "not-applicable": 0 };
  const fixtureIds = new Set();
  for (const group of GROUPS) {
    if (!Array.isArray(doc?.[group])) { errors.push(group + " must be an array"); continue; }
    for (const item of doc[group]) {
      if (!item?.id || !COVERAGE.has(item?.status)) { errors.push(group + " contains invalid coverage"); continue; }
      if (group === "fixtures") fixtureIds.add(item.id);
      counts[item.status]++;
      if (item.status === "not-applicable" && !item.rationale) errors.push(group + "/" + item.id + " needs not-applicable rationale");
      if (item.status === "passed" && (!Array.isArray(item.evidence) || item.evidence.length === 0)) errors.push(group + "/" + item.id + " passed without evidence");
    }
  }
  if (profile) missing.fixtures = (profile.requiredFixtures || []).filter((id) => !fixtureIds.has(id));

  if (!Array.isArray(doc?.unknowns)) errors.push("unknowns must be an array");
  if (!Array.isArray(doc?.findings)) errors.push("findings must be an array");
  const open = { critical: 0, high: 0, medium: 0, low: 0 };
  for (const finding of doc?.findings || []) {
    if (!finding?.id || !SEVERITIES.includes(finding?.severity) || !["open","resolved","accepted"].includes(finding?.status) || !finding?.summary) { errors.push("invalid finding"); continue; }
    if (finding.status === "open") open[finding.severity]++;
  }

  // Motion evidence (VE-MOT-001). Required when the evidence or its profile
  // claims the motion dimension; absent motion evidence is unknown, not pass.
  const motionRequired = (doc?.dimensions || []).includes("motion") || (profile?.requiredDimensions || []).includes("motion");
  const motion = evaluateMotionEvidence(doc?.motion, { required: motionRequired });
  for (const error of motion.errors) errors.push(error);
  if (motion.missing.includes("motion evidence")) missing.motion = true;

  const tested = counts.passed + counts.failed;
  const applicable = tested + counts.untested;
  const coveragePercent = applicable === 0 ? null : Math.round((tested / applicable) * 10000) / 100;
  const profileIncomplete = missing.dimensions.length > 0 || missing.fixtures.length > 0;
  const hasUnknown = (doc?.unknowns?.length || 0) > 0 || counts.untested > 0 || profileIncomplete || motion.disposition === "incomplete";
  const hardFailure = counts.failed > 0 || open.critical > 0 || motion.disposition === "fail";
  const highUnresolved = open.high > 0;

  let computedDisposition = "pass";
  if (errors.length || hardFailure) computedDisposition = "fail";
  else if (hasUnknown || highUnresolved || applicable === 0) computedDisposition = "incomplete";

  if (doc?.disposition && doc.disposition !== computedDisposition) {
    errors.push("declared disposition " + doc.disposition + " does not match computed disposition " + computedDisposition);
    computedDisposition = "fail";
  }
  return { disposition: computedDisposition, profile: profile?.id || doc?.profile || null, coveragePercent, counts, missing, openFindings: open, motion, errors };
}

export function resolveProfile(registry, id) {
  const profiles = new Map((registry?.profiles || []).map((p) => [p.id, p]));
  const seen = new Set();
  function visit(name) {
    if (seen.has(name)) throw new Error("profile inheritance cycle at " + name);
    const p = profiles.get(name);
    if (!p) throw new Error("unknown polish profile " + name);
    seen.add(name);
    const parent = p.extends ? visit(p.extends) : { requiredDimensions: [], requiredFixtures: [] };
    seen.delete(name);
    return {...p, requiredDimensions:[...new Set([...(parent.requiredDimensions||[]),...(p.requiredDimensions||[])])], requiredFixtures:[...new Set([...(parent.requiredFixtures||[]),...(p.requiredFixtures||[])])]};
  }
  return visit(id);
}

async function main() {
  const args = process.argv.slice(2);
  const json = args.includes("--json");
  const positionals = args.filter((arg) => !arg.startsWith("--") && !args[args.indexOf(arg)-1]?.startsWith("--profile"));
  const path = args[0]?.startsWith("--") ? null : args[0];
  const profileIndex = args.indexOf("--profile");
  const profileId = profileIndex >= 0 ? args[profileIndex + 1] : null;
  if (!path) { process.stderr.write("usage: node scripts/application-polish-gate.mjs <evidence.json> [--profile ID] [--json]\n"); process.exitCode=2; return; }
  try {
    const doc = JSON.parse(await readFile(path, "utf8"));
    let profile = null;
    if (profileId) {
      const registry = JSON.parse(await readFile(new URL("../registries/application-polish-profiles.json", import.meta.url), "utf8"));
      profile = resolveProfile(registry, profileId);
    }
    const result = evaluatePolishEvidence(doc, profile);
    if (json) process.stdout.write(JSON.stringify(result,null,2)+"\n");
    else {
      process.stdout.write("Application polish: "+result.disposition.toUpperCase()+"\n");
      if (result.profile) process.stdout.write("Profile: "+result.profile+"\n");
      process.stdout.write("Coverage: "+(result.coveragePercent===null?"n/a":result.coveragePercent+"%")+"\n");
      if (result.missing.dimensions.length) process.stdout.write("Missing dimensions: "+result.missing.dimensions.join(", ")+"\n");
      if (result.missing.fixtures.length) process.stdout.write("Missing fixtures: "+result.missing.fixtures.join(", ")+"\n");
      if (result.motion.present || result.motion.disposition !== "not-required") process.stdout.write("Motion: "+result.motion.disposition+" ("+result.motion.entries+" entries, "+result.motion.counts.unknown+" unknown checks)"+"\n");
      if (result.missing.motion) process.stdout.write("Missing motion evidence: the motion dimension requires motion.entries or motion.notApplicable\n");
      for (const item of result.motion.missing) if (item !== "motion evidence") process.stdout.write("Missing motion check: "+item+"\n");
      for (const item of result.motion.unknownChecks) process.stdout.write("Unknown motion check: "+item+"\n");
      for (const item of result.motion.unknownModels) process.stdout.write("Unknown motion model or authority: "+item+"\n");
      for (const violation of result.motion.violations) process.stdout.write("MOTION VIOLATION: "+violation+"\n");
      for (const error of result.errors) process.stdout.write("ERROR: "+error+"\n");
    }
    process.exitCode = result.disposition==="pass"?0:result.disposition==="incomplete"?3:4;
  } catch(error) { process.stderr.write("unable to evaluate polish evidence: "+error.message+"\n"); process.exitCode=2; }
}
if (process.argv[1] && import.meta.url === pathToFileURL(process.argv[1]).href) await main();
