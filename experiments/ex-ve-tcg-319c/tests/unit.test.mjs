import assert from "node:assert/strict";
import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import test from "node:test";
import { fileURLToPath } from "node:url";
import { checkHtml } from "../protocol/forma-snapshot/character-grid-conformance.mjs";
import { sessionFiles, snapshotErrors } from "../src/build-harness.mjs";
import { accdItems, assignItems, distinctPrefixes, study1Items, STUDY1_FIELDS, trnhItems } from "../src/datasets.mjs";
import { balance, participantSchedule, stratumSchedules, study2Sequences, williams } from "../src/design.mjs";
import { sessionPlan } from "../src/plan.mjs";
import { pairedDifferences, pairedPower, sampleSize, summarizeDifferences, tCdf, tQuantile } from "../src/power.mjs";
import { shuffle, uniforms } from "../src/random.mjs";
import { backtracks, misentries, scoreStudy1, scoreStudy2, scrollDistance, toCsv } from "../src/scoring.mjs";
import { accountDetail, STUDY1_ORDERS, study1Screen, transactionHistory } from "../src/stimuli.mjs";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const read = (relative) => fs.readFileSync(path.join(root, relative), "utf8");
const config = JSON.parse(read("protocol/pilot-config.json"));
const snapshot = read("protocol/forma-snapshot/character-grid-workflow.html");

// ---- random and design ---------------------------------------------------------

test("the generator is deterministic and pure", () => {
  assert.deepEqual(uniforms(42, 5), uniforms(42, 5));
  assert.notDeepEqual(uniforms(42, 5)[0], uniforms(43, 5)[0]);
  const items = [1, 2, 3, 4, 5];
  assert.deepEqual([...shuffle(7, items)].sort(), items);
  assert.deepEqual(items, [1, 2, 3, 4, 5], "shuffle does not mutate");
});

test("the Williams square balances position and first-order carryover", () => {
  const square = williams(4);
  square.forEach((row) => assert.equal(new Set(row).size, 4));
  [0, 1, 2, 3].forEach((column) => assert.equal(new Set(square.map((row) => row[column])).size, 4));
  const pairs = square.flatMap((row) => row.slice(1).map((value, i) => `${row[i]}>${value}`));
  assert.equal(new Set(pairs).size, 12, "every ordered pair of neighbours exactly once");
});

test("schedules counterbalance, respect strata, and report imbalance honestly", () => {
  const n = stratumSchedules(config, "N", 8);
  assert.deepEqual(Object.values(balance(n, "study1")), [4, 4]);
  assert.deepEqual(Object.values(balance(n, "study2")), [2, 2, 2, 2]);
  const pilot = stratumSchedules(config, "T", config.pilotParticipantsPerStratum);
  assert.deepEqual(Object.values(balance(pilot, "study2")).sort(), [1, 1, 2, 2], "six is not a multiple of four");
  assert.equal(participantSchedule(config, "S", 0).study1, null, "stratum S runs Study 2 only");
  assert.equal(study2Sequences(config).length, 4);
  assert.throws(() => participantSchedule(config, "X", 0));
});

// ---- datasets -----------------------------------------------------------------

test("Study 1 records are fictitious, fit their fields, and have distinct prefixes", () => {
  const items = study1Items(config.seed, 24);
  assert.equal(new Set(items.map((item) => JSON.stringify(item.record))).size, 24);
  items.forEach(({ record }) => {
    assert.ok(distinctPrefixes(record));
    assert.match(record.dob, /^\d{4}-\d{2}-\d{2}$/);
    assert.ok(record.acct.length <= 14 && record.custno.length <= 10 && record.last.length <= 20 && record.first.length <= 15);
    assert.deepEqual(Object.keys(record), [...STUDY1_FIELDS]);
  });
});

test("Study 2 items have one identifiable target row, coherent balances, and spread targets", () => {
  const items = trnhItems(config.seed, 16);
  items.forEach((item) => {
    const matches = item.rows.filter((row) => row.date === item.question.date && row.description === item.question.description);
    assert.equal(matches.length, 1);
    assert.deepEqual({ amount: matches[0].amount, status: matches[0].status }, item.answer);
    const cents = (text) => Math.round(Number(text.replaceAll(",", "")) * 100);
    item.rows.slice(0, -1).forEach((row, i) => assert.equal(cents(row.balance), cents(item.rows[i + 1].balance) + cents(row.amount)));
  });
  assert.ok(new Set(items.map((item) => item.targetRow)).size >= 8, "targets fall in many row positions");
  accdItems(config.seed, 4).forEach((item) => assert.equal(item.answer.available, item.values.available));
});

test("item sets rotate across conditions between sequence cycles", () => {
  const items = Array.from({ length: 4 }, (_, i) => i);
  assert.deepEqual(assignItems(items, ["R", "C"], 0, 2).map((set) => set.items), [[0, 1], [2, 3]]);
  assert.deepEqual(assignItems(items, ["R", "C"], 2, 2).map((set) => set.items), [[2, 3], [0, 1]]);
});

// ---- stimuli ------------------------------------------------------------------

test("Study 1 conditions differ only in source order, and only C breaks CG-6", () => {
  assert.deepEqual(STUDY1_ORDERS.R, ["custno", "acct", "last", "first", "dob", "postal"]);
  assert.deepEqual(STUDY1_ORDERS.C, ["custno", "last", "dob", "acct", "first", "postal"]);
  assert.deepEqual(checkHtml(study1Screen({ order: "R" })), []);
  const errors = checkHtml(study1Screen({ order: "C" }));
  assert.ok(errors.length > 0 && errors.every((error) => error.includes("CG-6")), errors.join("\n"));
  const positions = (html) => [...html.matchAll(/data-ef-row="(\d+)" data-ef-col="(\d+)"/g)].map((m) => m.slice(1).join(":")).sort();
  assert.deepEqual(positions(study1Screen({ order: "R" })), positions(study1Screen({ order: "C" })), "visual placement is identical");
  assert.doesNotMatch(study1Screen({ order: "C" }), /tabindex="[1-9]/, "order comes from source, never positive tabindex");
});

test("Study 2 stimuli are Forma's screens with data and strategy substituted, and conform", () => {
  const [trnh] = trnhItems(config.seed, 1);
  const [accd] = accdItems(config.seed, 1);
  for (const strategy of ["contained", "reflow"]) {
    const history = transactionHistory(snapshot, { rows: trnh.rows, strategy });
    const detail = accountDetail(snapshot, { values: accd.values, strategy });
    assert.deepEqual(checkHtml(history), []);
    assert.deepEqual(checkHtml(detail), []);
    assert.match(history, new RegExp(`data-ef-narrow="${strategy}"`));
    assert.ok(history.includes(trnh.answer.amount) && detail.includes(accd.values.available));
    assert.equal((history.match(/<div/g) ?? []).length, (history.match(/<\/div>/g) ?? []).length, "balanced markup");
  }
  assert.throws(() => accountDetail(snapshot.replace("12,480.55", "0.00"), { values: accd.values, strategy: "reflow" }), /exactly one/);
  assert.throws(() => transactionHistory(snapshot, { rows: trnh.rows, strategy: "shrink" }));
});

test("the Forma snapshot matches its manifest and a changed file is refused", () => {
  const manifest = JSON.parse(read("protocol/forma-snapshot/manifest.json"));
  assert.deepEqual(snapshotErrors(manifest, (file) => read(`protocol/forma-snapshot/${file}`)), []);
  assert.equal(snapshotErrors(manifest, (file) => read(`protocol/forma-snapshot/${file}`) + " ").length, Object.keys(manifest.files).length);
  assert.equal(crypto.createHash("sha256").update(snapshot).digest("hex"), manifest.files["character-grid-workflow.html"]);
});

// ---- plans --------------------------------------------------------------------

test("a session plan is deterministic and complete", () => {
  const plan = sessionPlan(config, snapshot, "N", 3);
  assert.deepEqual(plan, sessionPlan(config, snapshot, "N", 3));
  assert.equal(plan.trials.length, 2 * 12 + 4 * 8);
  assert.deepEqual(plan.trials.map((trial) => trial.trialIndex), plan.trials.map((_, i) => i));
  assert.ok(plan.trials.slice(0, 24).every((trial) => trial.study === 1) && plan.trials.slice(24).every((trial) => trial.study === 2));
  assert.equal(new Set(plan.trials.map((trial) => trial.item)).size, plan.trials.length, "no item repeats within a session");
  assert.equal(sessionPlan(config, snapshot, "S", 0).trials.length, 32);
  const { files } = sessionFiles({ config, snapshotHtml: snapshot, css: "", harness: { html: "", script: "" }, stratum: "T", index: 1 });
  assert.deepEqual(Object.keys(files).sort(), ["forma.css", "harness.mjs", "index.html", "plan.json"]);
});

// ---- scoring ------------------------------------------------------------------

const targets = { custno: "0622088681", acct: "72570-4055-91", last: "YILMAZ", first: "ESME", dob: "1956-11-20", postal: "99908" };
const typed = (t, target, value) => ({ t, type: "input", target, value });

test("a misentry is another field's value typed into a field; a typo is not", () => {
  const events = [
    { t: 0, type: "focus", target: "custno" }, typed(1, "custno", "0622088681"),
    { t: 2, type: "focus", target: "last" }, typed(3, "last", "72570"),
    { t: 4, type: "focus", target: "dob" }, typed(5, "dob", "1956-11-21")
  ];
  assert.deepEqual(misentries(events, targets).map((m) => m.field), ["last"]);
  assert.deepEqual(misentries([{ t: 0, type: "focus", target: "last" }, typed(1, "last", "7")], targets), [], "one character is not attributable");
});

test("navigation errors add corrective Shift+Tab presses; backtracks follow focus order", () => {
  const events = [
    { t: 0, type: "focus", target: "custno" }, { t: 1, type: "keydown", key: "0", shift: false }, typed(2, "custno", "0622088681"),
    { t: 3, type: "keydown", key: "Tab", shift: false }, { t: 4, type: "focus", target: "last" }, typed(5, "last", "72570-4055-91"),
    { t: 6, type: "keydown", key: "Tab", shift: true }, { t: 7, type: "focus", target: "custno" },
    { t: 8, type: "submit", target: "enter" }
  ];
  const score = scoreStudy1({ expected: targets, focusOrder: STUDY1_ORDERS.C, events });
  assert.equal(score.misentries, 1);
  assert.equal(score.correctiveShiftTabs, 1);
  assert.equal(score.navigationErrors, 2);
  assert.equal(score.backtracks, 1);
  assert.equal(score.completionMs, 7);
  assert.equal(score.finalCorrect, false);
  assert.equal(backtracks(events, STUDY1_ORDERS.R), 1);
});

test("Study 2 accuracy ignores spacing and thousands separators but not values", () => {
  const trial = (value) => ({ expected: { amount: "-1,200.00", status: "POSTED" }, events: [{ t: 10, type: "shown" }, { t: 40, type: "answer", value }] });
  assert.equal(scoreStudy2(trial({ amount: " -1200.00", status: "posted" })).correct, true);
  assert.equal(scoreStudy2(trial({ amount: "-1,200.00", status: "PENDING" })).correct, false);
  assert.equal(scoreStudy2(trial({ amount: "-1,200.00", status: "POSTED" })).answerMs, 30);
  assert.equal(scoreStudy2({ expected: { a: "1" }, events: [{ t: 0, type: "shown" }] }).correct, false, "no answer is incorrect");
  const scrolls = [{ type: "scroll", target: "viewport", left: 100 }, { type: "scroll", target: "viewport", left: 40 }, { type: "scroll", target: "window", left: 10 }];
  assert.equal(scrollDistance(scrolls, "left"), 100 + 60 + 10);
});

test("the CSV export quotes values that need it", () => {
  assert.equal(toCsv([{ a: 1, b: 'x,"y"' }, { a: null, c: true }]), 'a,b,c\n1,"x,""y""",\n,,true\n');
});

// ---- power --------------------------------------------------------------------

test("t distribution matches tabulated values", () => {
  assert.equal(tQuantile(0.975, 10).toFixed(3), "2.228");
  assert.equal(tQuantile(0.975, 5).toFixed(3), "2.571");
  assert.equal(tQuantile(0.95, 20).toFixed(3), "1.725");
  assert.equal(tCdf(0, 9), 0.5);
});

test("simulated paired power reproduces standard sample sizes", () => {
  const plan = { alpha: 0.05, targetPower: 0.8, simulations: 2000, seed: 5 };
  assert.ok(Math.abs(sampleSize({ ...plan, delta: 0.8, sd: 1 }) - 15) <= 1);
  assert.equal(sampleSize({ ...plan, delta: 0.8, sd: 1, multiple: 4 }) % 4, 0, "rounded to the counterbalancing cycle");
  assert.equal(sampleSize({ ...plan, delta: 0, sd: 1 }), null);
  assert.ok(pairedPower({ n: 10, delta: 0, sd: 1, alpha: 0.05, simulations: 2000, seed: 1 }) < 0.08, "type I error near alpha");
});

test("pilot differences are per participant and per stratum", () => {
  const rows = [
    ["N-000", "N", "C", 3], ["N-000", "N", "C", 1], ["N-000", "N", "R", 0],
    ["N-001", "N", "C", 1], ["N-001", "N", "R", 1], ["T-000", "T", "C", 2], ["T-000", "T", "R", 0]
  ].map(([participant, stratum, condition, navigationErrors]) => ({ participant, stratum, condition, navigationErrors, study: 1 }));
  const differences = pairedDifferences(rows, { study: 1, measure: "navigationErrors", a: "C", b: "R" });
  assert.deepEqual(differences.map((d) => d.difference), [2, 0, 2]);
  const summary = summarizeDifferences(differences);
  assert.deepEqual([summary.N.n, summary.N.mean, summary.T.n], [2, 1, 1]);
  assert.ok(Number.isNaN(summary.T.sd), "one participant gives no variance estimate");
});

test("the pilot analysis turns trial records into differences and a sample-size table", async () => {
  const { analyze, CONTRASTS } = await import("../src/analyze.mjs");
  const fast = { ...config, power: { ...config.power, simulations: 300 } };
  // Four synthetic participants whose C-condition entry differs by 1, 2, 1, 3
  // misentries: enough to exercise the arithmetic, never evidence.
  const entry = (participant, condition, wrong) => ({
    participant, stratum: "N", study: 1, condition, screen: "CENT", item: `${participant}-${condition}`, trialIndex: 0,
    expected: targets, focusOrder: STUDY1_ORDERS[condition],
    events: [
      { t: 0, type: "focus", target: "custno" }, typed(1, "custno", targets.custno),
      ...Array.from({ length: wrong }, (_, k) => [{ t: 2 + k * 2, type: "focus", target: "last" }, typed(3 + k * 2, "last", targets.acct)]).flat(),
      { t: 50, type: "submit", target: "enter" }
    ]
  });
  const records = [["N-000", 1], ["N-001", 2], ["N-002", 1], ["N-003", 3]].flatMap(([p, wrong]) => [entry(p, "R", 0), entry(p, "C", wrong)]);
  const { scored, report } = analyze(fast, records);
  assert.equal(scored.length, 8);
  const study1 = report.contrasts.find((contrast) => contrast.key === CONTRASTS[0].key);
  assert.deepEqual([study1.summary.N.n, study1.summary.N.mean], [4, 1.75]);
  assert.ok(study1.sampleSizes[0].sizes.every((size) => size.n === null || size.n % 2 === 0), "sizes respect the AB/BA cycle");
  assert.ok(study1.sampleSizes[0].sizes.some((size) => Number.isInteger(size.n)));
});
