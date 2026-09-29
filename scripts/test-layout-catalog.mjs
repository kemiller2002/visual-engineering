import test from "node:test";
import assert from "node:assert/strict";
import path from "node:path";
import {
  collectRegistryIds,
  crossReferenceErrors,
  indexErrors,
  loadCatalog,
  validateCatalog,
  validateSchema
} from "./layout-catalog.mjs";

const root = path.resolve(path.dirname(new URL(import.meta.url).pathname), "..");

test("the committed layout catalog is valid", async () => {
  const result = validateCatalog(await loadCatalog(root));
  assert.deepEqual(result.errors, []);
  assert.ok(result.familyCount >= 1);
});

test("the terminal family records every contract section required by GH-18", async () => {
  const { families } = await loadCatalog(root);
  const family = families.find((entry) => entry.id === "LAY-TERMINAL-CHARACTER-GRID");
  assert.ok(family, "terminal family is present");
  const regionIds = family.semanticRegions.map((region) => region.id);
  ["screen-identity", "body", "message", "actions"].forEach((id) =>
    assert.ok(regionIds.includes(id), `semantic region ${id}`)
  );
  assert.equal(family.responsive.narrow.default, "contained");
  assert.equal(family.responsive.noPageOverflow, true);
  ["long", "missing", "extreme", "localization"].forEach((key) =>
    assert.ok(family.content[key].length > 0, `content.${key}`)
  );
  assert.ok(family.formaPrimitives.knownGaps.length > 0);
  assert.ok(family.assumptions.length > 0);
  assert.ok(family.verification.some((check) => check.kind === "browser"));
});

test("schema subset reports missing, mistyped, enum, and pattern errors", () => {
  const schema = {
    type: "object",
    required: ["id", "status", "tasks"],
    properties: {
      id: { type: "string", pattern: "^LAY-" },
      status: { enum: ["observed", "verified"] },
      tasks: { type: "array", minItems: 1, items: { type: "string" } }
    }
  };
  assert.deepEqual(validateSchema(schema, { id: "LAY-X", status: "observed", tasks: ["a"] }), []);
  const errors = validateSchema(schema, { id: "X", status: "done", tasks: [1] });
  assert.ok(errors.some((error) => error.includes("/id must match")));
  assert.ok(errors.some((error) => error.includes("/status must be one of")));
  assert.ok(errors.some((error) => error.includes("/tasks/0 must be string")));
  assert.ok(validateSchema(schema, {}).some((error) => error.includes("/tasks is required")));
});

test("registry ids are collected only from declarations", () => {
  const ids = collectRegistryIds([
    "### EV-VE-TCG-2026-AAAA — heading\nmentions EV-VE-TCG-2026-BBBB inline",
    "| HY-VE-TCG-2026-CCCC | hypothesis |\n| other | HY-VE-TCG-2026-DDDD |"
  ]);
  assert.deepEqual([...ids.evidence], ["EV-VE-TCG-2026-AAAA"]);
  assert.deepEqual([...ids.hypotheses], ["HY-VE-TCG-2026-CCCC"]);
});

test("cross references fail closed", () => {
  const family = {
    id: "LAY-T",
    evidence: ["EV-MISSING-2026-0000"],
    hypotheses: ["HY-MISSING-2026-0000"],
    gaps: ["GAP-X"],
    formaPrimitives: { knownGaps: [] },
    verification: [{ id: "V-1" }, { id: "V-1" }],
    assumptions: [],
    responsive: { noPageOverflow: false }
  };
  const errors = crossReferenceErrors(family, { evidence: new Set(), hypotheses: new Set() });
  assert.equal(errors.length, 5);
});

test("index must agree with records", () => {
  const index = {
    supportStates: ["observed"],
    families: [
      { id: "LAY-A", name: "A", status: "observed" },
      { id: "LAY-B", name: "B", status: "unknown" }
    ]
  };
  const errors = indexErrors(index, [{ id: "LAY-A", name: "Renamed", status: "observed" }]);
  assert.ok(errors.some((error) => error.includes("LAY-B is not a support state") || error.includes("status for LAY-B")));
  assert.ok(errors.some((error) => error.includes("disagrees")));
  assert.ok(errors.some((error) => error.includes("no record was loaded")));
});
