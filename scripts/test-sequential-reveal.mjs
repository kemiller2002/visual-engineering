import test from "node:test";
import assert from "node:assert/strict";
import {
  DEFAULTS,
  isComplete,
  planReveal,
  resolveMode,
  semanticTextAt,
  visibleAt
} from "./sequential-reveal.mjs";

const rows = ["ACCT INQUIRY", "", "CUSTOMER  1042"];

const rowMajor = (a, b) => a.row - b.row || a.column - b.column;

test("reveal order is row-major: left to right, then top to bottom", () => {
  const plan = planReveal(rows, { charMs: 10, lineMs: 50, maxDurationMs: 10_000, skipBlank: false });
  const staged = [...plan.cells].sort(rowMajor);
  staged.slice(1).forEach((cell, index) => {
    assert.ok(cell.visibleAt >= staged[index].visibleAt, `${cell.row}:${cell.column} after predecessor`);
  });
  assert.equal(plan.mode, "animated");
});

test("character and line timing are both honored", () => {
  const plan = planReveal(["AB", "C"], { charMs: 10, lineMs: 100, maxDurationMs: 10_000 });
  const at = (row, column) => plan.cells.find((cell) => cell.row === row && cell.column === column).visibleAt;
  assert.equal(at(1, 1), 10);
  assert.equal(at(1, 2), 20);
  assert.equal(at(2, 1), 20 + 100 + 10);
});

test("blank cells cost nothing when skipBlank is set", () => {
  const skipping = planReveal(["A    B"], { charMs: 10, lineMs: 0, maxDurationMs: 10_000, skipBlank: true });
  const typing = planReveal(["A    B"], { charMs: 10, lineMs: 0, maxDurationMs: 10_000, skipBlank: false });
  assert.equal(skipping.durationMs, 20);
  assert.equal(typing.durationMs, 60);
});

test("reduced motion always yields an immediate, static presentation", () => {
  const plan = planReveal(rows, { ...DEFAULTS, reducedMotion: true });
  assert.equal(plan.mode, "immediate");
  assert.equal(plan.durationMs, 0);
  assert.ok(plan.cells.every((cell) => cell.visibleAt === 0));
  assert.ok(isComplete(plan, 0));
});

test("static mode and zero timing are immediate", () => {
  assert.equal(resolveMode({ mode: "immediate", charMs: 10, lineMs: 10, maxDurationMs: 100 }), "immediate");
  assert.equal(resolveMode({ mode: "animated", charMs: 0, lineMs: 0, maxDurationMs: 100 }), "immediate");
  assert.equal(resolveMode({ mode: "animated", charMs: 10, lineMs: 0, maxDurationMs: 0 }), "immediate");
});

test("the whole reveal completes within the duration cap by proportional compression", () => {
  const long = Array.from({ length: 24 }, () => "X".repeat(80));
  const plan = planReveal(long, { charMs: 20, lineMs: 60, maxDurationMs: 1500 });
  assert.ok(plan.durationMs <= 1500 + 1e-9);
  assert.ok(plan.scale < 1);
  assert.ok(isComplete(plan, 1500));
});

test("the default cap stays well below the WCAG 2.2.2 five-second threshold", () => {
  assert.ok(DEFAULTS.maxDurationMs < 5000);
});

test("interruption completes the reveal immediately and never hides content", () => {
  const plan = planReveal(rows, { charMs: 50, lineMs: 50, maxDurationMs: 10_000 });
  const before = visibleAt(plan, 60);
  assert.ok(before.length < plan.cells.length);
  const after = visibleAt(plan, 60, 60);
  assert.equal(after.length, plan.cells.length);
  assert.ok(isComplete(plan, 61, 60));
});

test("excluded runs (fields, messages, consequential values) are visible from time zero", () => {
  const plan = planReveal(["NAME ____", "ERROR X"], { charMs: 10, lineMs: 10, maxDurationMs: 10_000 }, [
    { row: 1, column: 6, length: 4 },
    { row: 2, column: 1, length: 7 }
  ]);
  const excluded = plan.cells.filter(
    (cell) => (cell.row === 1 && cell.column >= 6) || cell.row === 2
  );
  assert.ok(excluded.every((cell) => cell.visibleAt === 0));
  assert.ok(visibleAt(plan, 0).length >= excluded.length);
});

test("replay is deterministic: the same content and options produce the same plan", () => {
  assert.deepEqual(planReveal(rows), planReveal(rows));
});

test("semantic text is complete at every instant, independent of reveal state", () => {
  assert.equal(semanticTextAt(rows), "ACCT INQUIRY\n\nCUSTOMER  1042");
});
