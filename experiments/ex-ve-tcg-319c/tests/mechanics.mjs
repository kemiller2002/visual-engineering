// Mechanics check for the EX-VE-TCG-2026-319C harness (not a study run).
//
// A deterministic scripted agent completes one full session in a real
// browser: in Study 1 it always types the record in row-major order with Tab
// between fields; in Study 2 it reads the answer from the DOM, scrolling the
// grid first. The harness's own event logs are then scored. The check proves
// that logging and scoring detect what they must (misentries appear only when
// focus order differs from the typing order; answers are scored; scrolling is
// measured). Agent behaviour is scripted, so the output is not evidence about
// people and must never be used for HY-VE-TCG-2026-9151 or -8750.
//
//   CHROMIUM_PATH=/path/to/chrome node experiments/ex-ve-tcg-319c/tests/mechanics.mjs
import assert from "node:assert/strict";
import fs from "node:fs";
import http from "node:http";
import path from "node:path";
import { fileURLToPath } from "node:url";
import { chromium } from "playwright-core";
import { sessionFiles } from "../src/build-harness.mjs";
import { scoreTrial, toCsv } from "../src/scoring.mjs";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const read = (relative) => fs.readFileSync(path.join(root, relative), "utf8");
const ROW_MAJOR = ["custno", "acct", "last", "first", "dob", "postal"];

const { files } = sessionFiles({
  config: JSON.parse(read("protocol/pilot-config.json")),
  snapshotHtml: read("protocol/forma-snapshot/character-grid-workflow.html"),
  css: read("protocol/forma-snapshot/all.css"),
  harness: { html: read("harness/index.html"), script: read("harness/harness.mjs") },
  stratum: "N",
  index: 0
});

const types = { ".html": "text/html", ".mjs": "text/javascript", ".css": "text/css", ".json": "application/json" };
const server = http.createServer((request, response) => {
  const name = decodeURIComponent(new URL(request.url, "http://x").pathname.slice(1)) || "index.html";
  const body = files[name];
  response.writeHead(body === undefined ? 404 : 200, { "content-type": types[path.extname(name)] ?? "text/plain" });
  response.end(body ?? "not found");
});
await new Promise((resolve) => server.listen(0, "127.0.0.1", resolve));
const url = `http://127.0.0.1:${server.address().port}/index.html`;

const executablePath = process.env.CHROMIUM_PATH || undefined;
const browser = await chromium.launch({ executablePath });
const page = await browser.newPage({ viewport: { width: 1280, height: 900 } });
await page.goto(url);
await page.fill("#at", "none (scripted mechanics agent)");
await page.click("#setup button[type=submit]");

const plan = await page.evaluate(() => window.__ex319c.plan());

const study1 = async (trial) => {
  await page.click("#begin");
  // Row-major typist: the record, left to right and top to bottom, Tab between fields.
  for (const [position, field] of ROW_MAJOR.entries()) {
    await page.keyboard.type(trial.expected[field]);
    if (position < ROW_MAJOR.length - 1) await page.keyboard.press("Tab");
  }
  await page.keyboard.press("Enter");
};

const study2 = async (trial) => {
  await page.click("#begin");
  // Scroll whatever holds the grid horizontally, as a sighted user must at 320px.
  await page.evaluate(() => {
    const scrollers = [...document.querySelectorAll("#stage .ef-character-grid__viewport, #stage .ef-character-grid__table")]
      .filter((element) => element.scrollWidth > element.clientWidth + 1);
    scrollers.forEach((element) => { element.scrollLeft = element.scrollWidth; });
  });
  await page.waitForTimeout(50);
  const answer = await page.evaluate(({ screen, expected }) => {
    if (screen === "ACCD") {
      const valueAfter = (label) => {
        const labels = [...document.querySelectorAll("#stage .ef-character-grid__text")];
        const at = labels.find((element) => element.textContent.startsWith(label));
        return at.nextElementSibling.textContent;
      };
      return { available: valueAfter("Available"), overdraft: valueAfter("Overdraft") };
    }
    const question = document.querySelector("#card").textContent;
    const row = [...document.querySelectorAll("#stage tbody tr")].find((tr) => {
      const [date, description] = [...tr.cells].map((cell) => cell.textContent);
      return question.includes(date) && question.includes(`"${description}"`);
    });
    return { amount: row.cells[2].textContent, status: row.cells[4].textContent };
  }, { screen: trial.screen, expected: trial.expected });
  for (const [key, value] of Object.entries(answer)) await page.fill(`[name="answer-${key}"]`, value);
  await page.click("#answers button[type=submit]");
};

for (const trial of plan.trials) {
  if (trial.study === 2 && plan.trials[trial.trialIndex - 1]?.study !== 2) {
    await page.setViewportSize({ width: 320, height: 640 });
    await page.click("#gate-continue");
  }
  await (trial.study === 1 ? study1(trial) : study2(trial));
}
await page.waitForSelector("#done:not([hidden])");
const records = await page.evaluate(() => window.__ex319c.records());
await browser.close();
server.close();

const scored = records.map(scoreTrial);
const out = path.join(root, "dist", "mechanics");
fs.mkdirSync(out, { recursive: true });
fs.writeFileSync(path.join(out, "N-000-synthetic-trials.json"), JSON.stringify(records, null, 2));
fs.writeFileSync(path.join(out, "N-000-synthetic-scored.csv"), toCsv(scored));

const where = (study, condition, screen) => scored.filter((row) => row.study === study && row.condition === condition && (!screen || row.screen === screen));

assert.equal(records.length, plan.trials.length, "one record per planned trial");
assert.ok(where(1, "R").every((row) => row.navigationErrors === 0 && row.finalCorrect && row.submitted), "row-major typing under R: no navigation errors");
assert.ok(where(1, "C").every((row) => row.misentries > 0 && !row.finalCorrect), "row-major typing under C: misentries detected");
assert.ok(where(1, "C").every((row) => row.backtracks === 0), "Tab-only navigation never backtracks");
assert.ok(scored.filter((row) => row.study === 2).every((row) => row.correct), "DOM-read answers score correct");
assert.ok(where(2, "contained", "TRNH").every((row) => row.horizontalScrollPx > 0), "contained TRNH at 320px requires horizontal scrolling");
assert.ok(records.filter((record) => record.study === 2).every((record) => record.environment.viewport.width === 320 && !record.environment.viewportMismatch), "Study 2 ran at 320 CSS px");

const mean = (rows, key) => (rows.reduce((sum, row) => sum + row[key], 0) / rows.length).toFixed(2);
console.log(`mechanics check passed: ${records.length} trials (scripted agent, not participant data)`);
console.log(`  Study 1 R: navigation errors/trial ${mean(where(1, "R"), "navigationErrors")}; C: ${mean(where(1, "C"), "navigationErrors")}`);
for (const screen of ["TRNH", "ACCD"]) {
  for (const strategy of ["contained", "reflow"]) {
    const rows = where(2, strategy, screen);
    console.log(`  Study 2 ${screen} ${strategy}: correct ${rows.filter((row) => row.correct).length}/${rows.length}, horizontal scroll px/trial ${mean(rows, "horizontalScrollPx")}, focus stops/trial ${mean(rows, "focusStops")}`);
  }
}
console.log(`  raw records and scored CSV: ${path.relative(process.cwd(), out)}`);
