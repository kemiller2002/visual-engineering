import test from "node:test";
import assert from "node:assert/strict";
import { createServer } from "node:http";
import { readFile } from "node:fs/promises";
import { chromium } from "playwright-core";
import { applyMotionProbeResults, PROBE_CHECKS, RESULT_STATUS, runProbe } from "./application-polish-motion-probes.mjs";
import { evaluatePolishEvidence } from "./application-polish-gate.mjs";
import { MOTION_CHECKS } from "./application-polish-motion.mjs";

// Real-browser tests of the probe machinery: every probe must pass against
// the good fixture variant and fail against the bad one.
const FIXTURES = new URL("../examples/motion-probes/", import.meta.url);
const config = JSON.parse(await readFile(new URL("motion-probes.config.json", FIXTURES), "utf8"));
const launch = process.env.POLISH_CHROMIUM_EXECUTABLE ? { executablePath: process.env.POLISH_CHROMIUM_EXECUTABLE } : {};

const server = createServer(async (request, response) => {
  const path = new URL(request.url, "http://localhost").pathname.replace(/^\/+/, "") || "fixture.html";
  try {
    const body = await readFile(new URL(path, FIXTURES));
    response.writeHead(200, { "content-type": path.endsWith(".html") ? "text/html" : "application/octet-stream" });
    response.end(body);
  } catch {
    response.writeHead(404).end();
  }
});
await new Promise((done) => server.listen(0, "127.0.0.1", done));
const base = `http://127.0.0.1:${server.address().port}/fixture.html`;
const browser = await chromium.launch({ headless: true, ...launch });

test.after(async () => {
  await browser.close();
  server.close();
});

const run = async (probe, variant) => {
  const context = await browser.newContext({ viewport: { width: 1280, height: 900 } });
  const page = await context.newPage();
  const outcome = await runProbe(page, probe, `${base}?variant=${variant}`);
  await context.close();
  return outcome;
};

test("every configured probe maps to a motion evidence check", () => {
  Object.values(PROBE_CHECKS).forEach((check) => assert.ok(Object.hasOwn(MOTION_CHECKS, check), check));
  const probes = new Set(config.probes.map((probe) => probe.probe));
  Object.keys(PROBE_CHECKS).forEach((probe) => assert.ok(probes.has(probe), `example config exercises ${probe}`));
});

config.probes.forEach((probe) => {
  test(`${probe.probe} (${probe.id}) passes when the invariant holds and fails when it is violated`, async () => {
    const good = await run(probe, "good");
    assert.equal(good.status, "pass", `good: ${good.notes} ${good.evidence.join("; ")}`);
    assert.ok(good.evidence.length > 0, "a pass carries evidence");
    const bad = await run(probe, "bad");
    assert.equal(bad.status, "fail", `bad: ${bad.notes} ${bad.evidence.join("; ")}`);
    assert.ok(bad.notes, "a failure explains the violated invariant");
    [good, bad].forEach((outcome) => {
      assert.ok(RESULT_STATUS.includes(outcome.status));
      assert.equal(outcome.check, PROBE_CHECKS[probe.probe]);
      assert.equal(outcome.state, probe.state, "results identify the exercised state/seam");
    });
  });
});

test("a probe that cannot observe its target reports unknown, never pass", async () => {
  const outcome = await run({ id: "missing", probe: "motion-focus", requires: ["#does-not-exist"], actions: [], expectFocus: "#x" }, "good");
  assert.equal(outcome.status, "unknown");
  const unreadable = await run({ id: "nan", probe: "motion-progress-bounds", steps: [{ type: "wait", ms: 0 }], authoritative: { selector: "#nope" }, visual: { selector: "#nope" } }, "good");
  assert.equal(unreadable.status, "unknown");
  const unknownProbe = await run({ id: "x", probe: "motion-telepathy" }, "good");
  assert.equal(unknownProbe.status, "unknown");
});

test("explicitly not-applicable probes are reported as not applicable with the reason", async () => {
  const outcome = await run({ id: "na", probe: "motion-direct-lag", notApplicable: "No direct manipulation on this surface." }, "good");
  assert.equal(outcome.status, "not-applicable");
  assert.equal(outcome.notes, "No direct manipulation on this surface.");
});

test("probe results fold into motion evidence and drive the polish gate", () => {
  const evidence = {
    schemaVersion: 1,
    surface: "example",
    dimensions: ["motion"],
    states: [{ id: "loaded", status: "passed", evidence: ["test:loaded"] }], seams: [], environments: [], fixtures: [], unknowns: [], findings: [],
    motion: {
      entries: [{
        id: "tab-indicator",
        model: "inertial",
        phenomenon: "Tab indicator settles on the selected tab.",
        roles: ["selection-indicator"],
        authority: { source: "application tab state", semanticUpdate: "immediate" },
        reducedMotion: { substitution: "Indicator placed immediately." },
        checks: { reducedMotion: { status: "passed", evidence: ["test:reduced"] }, semanticIndependence: { status: "passed", evidence: ["review:aria-selected"] } }
      }]
    }
  };
  const report = (statuses) => ({
    results: [
      { id: "a", probe: "motion-semantic-authority", check: "semanticAuthority", entry: "tab-indicator", status: statuses[0], evidence: ["x"], notes: null, environment: { id: "desktop" } },
      { id: "b", probe: "motion-rapid-repeat", check: "rapidRepeat", entry: "tab-indicator", status: statuses[1], evidence: ["y"], notes: null, environment: { id: "desktop" } },
      { id: "c", probe: "motion-rapid-repeat", check: "rapidRepeat", entry: "tab-indicator", status: statuses[2], evidence: ["z"], notes: "narrow", environment: { id: "narrow" } },
      { id: "d", probe: "motion-interruption", check: "interruption", entry: "tab-indicator", status: statuses[3], evidence: ["w"], notes: null, environment: { id: "desktop" } },
      { id: "e", probe: "motion-focus", check: "focus", entry: "no-such-entry", status: "pass", evidence: ["v"], notes: null, environment: { id: "desktop" } }
    ]
  });

  const passing = applyMotionProbeResults(evidence, report(["pass", "pass", "pass", "pass"]));
  assert.deepEqual(passing.unmatched, ["no-such-entry|focus"]);
  assert.equal(passing.evidence.motion.entries[0].checks.rapidRepeat.status, "passed");
  assert.ok(passing.evidence.motion.entries[0].checks.rapidRepeat.evidence.some((item) => item.includes("@narrow")));
  assert.equal(evaluatePolishEvidence(passing.evidence).disposition, "pass");

  const unknownInOneEnvironment = applyMotionProbeResults(evidence, report(["pass", "pass", "unknown", "pass"]));
  assert.equal(unknownInOneEnvironment.evidence.motion.entries[0].checks.rapidRepeat.status, "unknown");
  assert.equal(evaluatePolishEvidence(unknownInOneEnvironment.evidence).disposition, "incomplete");

  const failing = applyMotionProbeResults(evidence, report(["pass", "unknown", "fail", "pass"]));
  assert.equal(failing.evidence.motion.entries[0].checks.rapidRepeat.status, "failed");
  assert.equal(evaluatePolishEvidence(failing.evidence).disposition, "fail");
  assert.equal(evidence.motion.entries[0].checks.rapidRepeat, undefined, "input evidence is not mutated");
});

test("every motion probe is registered with its check, claim, application guidance and limitations", async () => {
  const registry = JSON.parse(await readFile(new URL("../registries/application-polish-probes.json", import.meta.url), "utf8"));
  Object.entries(PROBE_CHECKS).forEach(([id, check]) => {
    const entry = registry.probes.find((probe) => probe.id === id);
    assert.ok(entry, `${id} is registered`);
    assert.equal(entry.motionCheck, check);
    assert.ok(entry.claim && entry.applyWhen && entry.limitations, `${id} documents claim, applyWhen and limitations`);
    assert.match(entry.limitations, /does not prove universal human comfort/);
  });
});
