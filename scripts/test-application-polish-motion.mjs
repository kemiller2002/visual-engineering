import test from "node:test";
import assert from "node:assert/strict";
import { readFile } from "node:fs/promises";
import { evaluatePolishEvidence, resolveProfile } from "./application-polish-gate.mjs";
import { evaluateMotionEvidence, FORBIDDEN_ENCODINGS, MOTION_CHECKS, requiredChecks } from "./application-polish-motion.mjs";

const passed = (id = "test:x") => ({ status: "passed", evidence: [id] });

const entry = (overrides = {}) => ({
  id: "indicator",
  model: "inertial",
  phenomenon: "Selection indicator settles on the selected option.",
  conveys: ["continuity"],
  roles: ["selection-indicator"],
  authority: { source: "native radio group", semanticUpdate: "immediate" },
  reducedMotion: { substitution: "Indicator placed immediately." },
  checks: {
    semanticAuthority: passed(),
    interruption: passed(),
    rapidRepeat: passed(),
    reducedMotion: passed(),
    semanticIndependence: passed()
  },
  ...overrides
});

const evidence = (motion, overrides = {}) => ({
  schemaVersion: 1,
  surface: "example",
  dimensions: ["interaction", "motion"],
  states: [{ id: "loaded", status: "passed", evidence: ["test:loaded"] }],
  seams: [],
  environments: [],
  fixtures: [],
  unknowns: [],
  findings: [],
  ...(motion === undefined ? {} : { motion }),
  ...overrides
});

const gate = (doc) => evaluatePolishEvidence(doc);

test("complete motion evidence passes", () => {
  const result = gate(evidence({ entries: [entry()] }, { disposition: "pass" }));
  assert.equal(result.disposition, "pass");
  assert.equal(result.motion.disposition, "pass");
  assert.deepEqual(result.errors, []);
});

test("claiming the motion dimension without motion evidence is incomplete, not pass", () => {
  const result = gate(evidence(undefined, { disposition: "incomplete" }));
  assert.equal(result.disposition, "incomplete");
  assert.equal(result.missing.motion, true);
});

test("profiles that require motion require motion evidence", async () => {
  const registry = JSON.parse(await readFile(new URL("../registries/application-polish-profiles.json", import.meta.url), "utf8"));
  const profile = resolveProfile(registry, "informational-site");
  assert.ok(profile.requiredDimensions.includes("motion"));
  const doc = evidence(undefined, { dimensions: ["interaction"] });
  const result = evaluatePolishEvidence(doc, profile);
  assert.equal(result.motion.disposition, "incomplete");
  assert.notEqual(result.disposition, "pass");
});

test("evidence without the motion dimension stays backward compatible", () => {
  const result = gate(evidence(undefined, { dimensions: ["interaction"], disposition: "pass" }));
  assert.equal(result.disposition, "pass");
  assert.equal(result.motion.disposition, "not-required");
});

test("an explicit not-applicable rationale satisfies the motion dimension", () => {
  const ok = gate(evidence({ notApplicable: { rationale: "The surface has no animation; every state change is instant." } }, { disposition: "pass" }));
  assert.equal(ok.disposition, "pass");
  const bare = gate(evidence({ entries: [] }, { disposition: "fail" }));
  assert.ok(bare.errors.some((error) => /notApplicable rationale/.test(error)));
});

test("unknown or untested required checks are incomplete; missing required checks are named", () => {
  const unknown = evaluateMotionEvidence({ entries: [entry({ checks: { ...entry().checks, rapidRepeat: { status: "unknown" } } })] });
  assert.equal(unknown.disposition, "incomplete");
  assert.deepEqual(unknown.unknownChecks, ["indicator/rapidRepeat"]);
  const { rapidRepeat, ...rest } = entry().checks;
  const missing = evaluateMotionEvidence({ entries: [entry({ checks: rest })] });
  assert.equal(missing.disposition, "incomplete");
  assert.deepEqual(missing.missing, ["indicator/rapidRepeat"]);
});

test("a smooth-looking animation is not evidence: passed checks need evidence", () => {
  const result = evaluateMotionEvidence({ entries: [entry({ checks: { ...entry().checks, interruption: { status: "passed" } } })] });
  assert.equal(result.disposition, "fail");
  assert.ok(result.errors.some((error) => /interruption passed without evidence/.test(error)));
});

test("an unknown model is incomplete and an unlisted model is invalid", () => {
  assert.equal(evaluateMotionEvidence({ entries: [entry({ model: "unknown" })] }).disposition, "incomplete");
  assert.equal(evaluateMotionEvidence({ entries: [entry({ model: "bouncy" })] }).disposition, "fail");
});

test("required checks follow the model and roles", () => {
  assert.ok(requiredChecks({ model: "cadence", roles: ["loading"] }).includes("cadenceStops"));
  assert.ok(requiredChecks({ model: "direct", roles: ["progress"] }).includes("progressBounds"));
  assert.ok(requiredChecks({ model: "direct", roles: ["drag"] }).includes("directManipulation"));
  assert.ok(requiredChecks({ model: "direct", roles: ["drag"] }).includes("boundaries"));
  assert.ok(requiredChecks({ model: "perceptual", progressiveEnhancement: true }).includes("progressiveFallback"));
  assert.ok(requiredChecks({ model: "inertial", concurrentWith: ["press"] }).includes("concurrentComposition"));
  assert.ok(!requiredChecks({ model: "perceptual" }).includes("rapidRepeat"));
  requiredChecks({ model: "inertial", roles: ["transient-surface"] }).forEach((name) => assert.ok(Object.hasOwn(MOTION_CHECKS, name)));
});

test("direct manipulation is distinguishable from post-release settling", () => {
  const drag = { ...entry(), id: "drag", roles: ["drag"], phase: "direct", model: "inertial", checks: {} };
  const result = evaluateMotionEvidence({ entries: [drag] });
  assert.equal(result.disposition, "fail");
  assert.ok(result.violations.some((violation) => /direct-manipulation phase must use the direct model/.test(violation)));
  const settle = evaluateMotionEvidence({ entries: [entry({ id: "settle", roles: ["drop"], phase: "settle", checks: { ...entry().checks, boundaries: passed() } })] });
  assert.equal(settle.violations.length, 0);
});

test("determinate progress must be a direct projection and prove its bounds", () => {
  const spring = evaluateMotionEvidence({ entries: [entry({ id: "progress", roles: ["progress"], model: "inertial" })] });
  assert.ok(spring.violations.some((violation) => /determinate progress/.test(violation)));
  const overshoot = evaluateMotionEvidence({
    entries: [entry({
      id: "progress",
      model: "direct",
      phase: "direct",
      roles: ["progress"],
      reducedMotion: { substitution: "Value placed immediately." },
      checks: { semanticAuthority: passed(), reducedMotion: passed(), semanticIndependence: passed(), directManipulation: passed(), progressBounds: { status: "failed", evidence: ["probe:visual 0.62 > value 0.6"] } }
    })]
  });
  assert.equal(overshoot.disposition, "fail");
});

test("semantic state that waits for animation fails", () => {
  const result = evaluateMotionEvidence({ entries: [entry({ authority: { source: "application", semanticUpdate: "delayed" } })] });
  assert.equal(result.disposition, "fail");
  assert.ok(result.violations.some((violation) => /waits for animation/.test(violation)));
  assert.equal(evaluateMotionEvidence({ entries: [entry({ authority: { source: "application", semanticUpdate: "unknown" } })] }).disposition, "incomplete");
});

test("motion weight or speed cannot be accepted as a hidden domain-semantic channel", () => {
  FORBIDDEN_ENCODINGS.forEach((meaning) => {
    const result = evaluateMotionEvidence({ entries: [entry({ conveys: ["attention", meaning] })] });
    assert.equal(result.disposition, "fail", meaning);
  });
});

test("spatial motion may survive reduced motion only for direct manipulation", () => {
  const inertial = evaluateMotionEvidence({ entries: [entry({ reducedMotion: { substitution: "none", spatial: true } })] });
  assert.ok(inertial.violations.some((violation) => /survives reduced motion/.test(violation)));
  const loading = evaluateMotionEvidence({ entries: [entry({ id: "spinner", model: "inertial", roles: ["loading"] })] });
  assert.ok(loading.violations.some((violation) => /must use cadence/.test(violation)));
});

test("not-applicable checks need a rationale", () => {
  const result = evaluateMotionEvidence({ entries: [entry({ checks: { ...entry().checks, rapidRepeat: { status: "not-applicable" } } })] });
  assert.ok(result.errors.some((error) => /rapidRepeat needs not-applicable rationale/.test(error)));
});

test("the shipped motion example is honest: incomplete while boundary coverage is unknown", async () => {
  const doc = JSON.parse(await readFile(new URL("../examples/application-polish-motion-evidence.json", import.meta.url), "utf8"));
  const models = new Set(doc.motion.entries.map((item) => item.model));
  ["inertial", "cadence", "direct", "perceptual"].forEach((model) => assert.ok(models.has(model), model));
  const result = evaluatePolishEvidence(doc);
  assert.equal(result.disposition, "incomplete");
  assert.deepEqual(result.errors, []);
  assert.deepEqual(result.motion.violations, []);
  assert.deepEqual(result.motion.unknownChecks, ["item-reorder-drag/boundaries", "item-reorder-settle/boundaries"]);
});
