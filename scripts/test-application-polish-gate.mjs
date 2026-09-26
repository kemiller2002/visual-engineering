import test from "node:test";
import assert from "node:assert/strict";
import { evaluatePolishEvidence } from "./application-polish-gate.mjs";

function evidence(overrides = {}) {
  return {
    schemaVersion: 1,
    surface: "example",
    dimensions: ["interaction"],
    states: [{ id: "loaded", status: "passed", evidence: ["test:loaded"] }],
    seams: [{ id: "loading-loaded", status: "passed", evidence: ["test:seam"] }],
    environments: [{ id: "narrow", status: "passed", evidence: ["test:narrow"] }],
    fixtures: [{ id: "rapid-double-action", status: "passed", evidence: ["test:double"] }],
    unknowns: [],
    findings: [],
    disposition: "pass",
    ...overrides
  };
}

test("complete evidenced coverage passes", () => {
  const result = evaluatePolishEvidence(evidence());
  assert.equal(result.disposition, "pass");
  assert.equal(result.coveragePercent, 100);
  assert.deepEqual(result.errors, []);
});

test("untested coverage is incomplete, never pass", () => {
  const doc = evidence({ states: [{ id: "offline", status: "untested" }], disposition: "incomplete" });
  const result = evaluatePolishEvidence(doc);
  assert.equal(result.disposition, "incomplete");
});

test("unknown knowledge is incomplete", () => {
  const result = evaluatePolishEvidence(evidence({ unknowns: ["Safari not exercised"], disposition: "incomplete" }));
  assert.equal(result.disposition, "incomplete");
});

test("failed coverage fails the gate", () => {
  const result = evaluatePolishEvidence(evidence({ seams: [{ id: "save-failure", status: "failed" }], disposition: "fail" }));
  assert.equal(result.disposition, "fail");
});

test("open critical finding fails the gate", () => {
  const findings = [{ id: "P-1", severity: "critical", status: "open", summary: "user work lost" }];
  assert.equal(evaluatePolishEvidence(evidence({ findings, disposition: "fail" })).disposition, "fail");
});

test("open high finding makes release incomplete", () => {
  const findings = [{ id: "P-2", severity: "high", status: "open", summary: "keyboard path blocked" }];
  assert.equal(evaluatePolishEvidence(evidence({ findings, disposition: "incomplete" })).disposition, "incomplete");
});

test("passed coverage requires evidence", () => {
  const result = evaluatePolishEvidence(evidence({ fixtures: [{ id: "long-text", status: "passed" }], disposition: "fail" }));
  assert.equal(result.disposition, "fail");
  assert.match(result.errors.join("\n"), /passed without evidence/);
});

test("not applicable coverage requires rationale", () => {
  const result = evaluatePolishEvidence(evidence({ environments: [{ id: "print", status: "not-applicable" }], disposition: "fail" }));
  assert.equal(result.disposition, "fail");
  assert.match(result.errors.join("\n"), /rationale/);
});

test("a false declared pass is rejected", () => {
  const result = evaluatePolishEvidence(evidence({ unknowns: ["unverified browser"] }));
  assert.equal(result.disposition, "fail");
  assert.match(result.errors.join("\n"), /does not match/);
});
