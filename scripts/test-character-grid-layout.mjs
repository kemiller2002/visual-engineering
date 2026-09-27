import test from "node:test";
import assert from "node:assert/strict";
import { readFile } from "node:fs/promises";
import path from "node:path";
import {
  composeState,
  density,
  focusOrder,
  screenErrors,
  segmentsOf,
  validateWorkflow
} from "./character-grid-layout.mjs";

const root = path.resolve(path.dirname(new URL(import.meta.url).pathname), "..");
const workflowPath = path.join(root, "content/layouts/reference-workflows/customer-account-inquiry.json");
const loadWorkflow = async () => JSON.parse(await readFile(workflowPath, "utf8"));

const reserved = { messageRow: 21, actionRows: [22, 23], statusRow: 24 };
const base = (runs) => ({
  id: "T",
  rows: 24,
  columns: 80,
  runs: [
    { kind: "heading", id: "title", row: 1, column: 2, text: "TEST" },
    ...runs,
    { kind: "message", id: "msg", row: 21, column: 2, length: 78, severity: "information", text: "OK" },
    { kind: "action", id: "a-enter", row: 22, column: 2, key: "Enter", label: "Go" }
  ]
});
const errorsFor = (runs) => screenErrors(base(runs), reserved);

test("the reference workflow is valid in every screen state", async () => {
  const result = validateWorkflow(await loadWorkflow());
  assert.deepEqual(result.errors, []);
  assert.equal(result.screens.length, 15);
});

test("the reference workflow exercises every construct the issue requires", async () => {
  const workflow = await loadWorkflow();
  const [inquiry, detail, history] = workflow.screens;
  const kinds = (screen) => screen.runs.map((run) => run.kind);
  assert.ok(kinds(inquiry).filter((kind) => kind === "field").length >= 2, "inquiry: multiple editable fields");
  assert.ok(inquiry.states.some((state) => state.runs.some((run) => run.severity === "validation")), "inquiry: validation");
  assert.ok(!kinds(detail).includes("field"), "detail: protected only");
  assert.ok(kinds(detail).filter((kind) => kind === "heading").length >= 3, "detail: grouping");
  const table = history.runs.find((run) => run.kind === "table");
  assert.ok(table.records.length >= 10, "history: dense repeated rows");
  ["empty", "error"].forEach((id) => assert.ok(history.states.some((state) => state.id === id), `history: ${id} state`));
  ["a-pf7", "a-pf8"].forEach((id) => assert.ok(history.runs.some((run) => run.id === id), `history: ${id}`));
});

test("focus order is row-major and matches source order in the reference screens", async () => {
  const workflow = await loadWorkflow();
  const inquiry = { ...workflow.screens[0], ...workflow.geometry };
  assert.deepEqual(focusOrder(inquiry), [
    "f-custno", "f-last", "f-first", "f-dob", "f-postal", "f-acct",
    "a-enter", "a-clear", "a-reset", "a-pf1", "a-pf3"
  ]);
});

test("out-of-bounds rows and right-edge crossing are rejected", () => {
  assert.ok(errorsFor([{ kind: "text", id: "x", row: 30, column: 2, text: "A" }]).some((e) => e.includes("outside")));
  assert.ok(errorsFor([{ kind: "text", id: "x", row: 3, column: 78, text: "ABCDE" }]).some((e) => e.includes("no row wrap")));
});

test("collisions are rejected", () => {
  const errors = errorsFor([
    { kind: "text", id: "a", row: 3, column: 2, text: "ABCDEF" },
    { kind: "text", id: "b", row: 3, column: 5, text: "XY" }
  ]);
  assert.ok(errors.some((error) => error.includes("b overlaps a")));
});

test("source order that contradicts row-major order is rejected", () => {
  const errors = errorsFor([
    { kind: "text", id: "later", row: 5, column: 2, text: "B" },
    { kind: "text", id: "earlier", row: 4, column: 2, text: "A" }
  ]);
  assert.ok(errors.some((error) => error.includes("earlier precedes later")));
});

test("protected overflow and field overflow are rejected rather than clipped", () => {
  assert.ok(errorsFor([{ kind: "value", id: "v", row: 3, column: 2, length: 3, text: "ABCD" }]).some((e) => e.includes("exceeds")));
  assert.ok(
    errorsFor([
      { kind: "text", id: "l", row: 3, column: 2, text: "L" },
      { kind: "field", id: "f", row: 3, column: 4, length: 2, labelledBy: "l", value: "ABC" }
    ]).some((e) => e.includes("exceeds field length"))
  );
});

test("fields need a preceding label and invalid fields need a message", () => {
  assert.ok(errorsFor([{ kind: "field", id: "f", row: 3, column: 4, length: 2 }]).some((e) => e.includes("no label")));
  assert.ok(
    errorsFor([
      { kind: "field", id: "f", row: 3, column: 4, length: 2, labelledBy: "l" },
      { kind: "text", id: "l", row: 3, column: 8, text: "L" }
    ]).some((e) => e.includes("must exist and precede"))
  );
  assert.ok(
    errorsFor([
      { kind: "text", id: "l", row: 3, column: 2, text: "L" },
      { kind: "field", id: "f", row: 3, column: 4, length: 2, labelledBy: "l", invalid: true }
    ]).some((e) => e.includes("must reference the message"))
  );
});

test("reserved rows hold only their region", () => {
  assert.ok(errorsFor([{ kind: "text", id: "x", row: 22, column: 40, text: "A" }]).some((e) => e.includes("reserved row")));
  assert.ok(errorsFor([{ kind: "action", id: "a", row: 5, column: 2, key: "PF1", label: "Help" }]).some((e) => e.includes("reserved action row")));
});

test("messages carry a severity and tables need an empty state", () => {
  const screen = base([]);
  const unlabelled = {
    ...screen,
    runs: screen.runs.map((run) => (run.kind === "message" ? { ...run, severity: "red" } : run))
  };
  assert.ok(screenErrors(unlabelled, reserved).some((e) => e.includes("severity")));
  const table = {
    kind: "table", id: "t", row: 3, maxRows: 2,
    columns: [{ id: "c", header: "C", column: 2, length: 5 }], records: []
  };
  assert.ok(errorsFor([table]).some((e) => e.includes("emptyText")));
  assert.equal(segmentsOf(table).length, 3);
});

test("states compose by id without mutating the base screen", () => {
  const screen = base([{ kind: "text", id: "x", row: 3, column: 2, text: "A" }]);
  const state = composeState(screen, { id: "s", runs: [{ id: "x", text: "B" }] });
  assert.equal(state.runs.find((run) => run.id === "x").text, "B");
  assert.equal(screen.runs.find((run) => run.id === "x").text, "A");
  assert.equal(state.id, "T#s");
});

test("density is measured from occupied cells", () => {
  const measured = density(base([]));
  assert.equal(measured.totalCells, 1920);
  assert.equal(measured.usedCells, 4 + 78 + "Enter=Go".length);
});
