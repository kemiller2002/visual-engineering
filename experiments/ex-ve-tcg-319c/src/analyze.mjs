// Pilot analysis: session files in, tidy trial CSV and a planning report out.
//   node experiments/ex-ve-tcg-319c/src/analyze.mjs --out DIR session1.json [session2.json ...]
// Each session file is what the harness downloads ({ plan, records }).
import fs from "node:fs";
import path from "node:path";
import { fileURLToPath, pathToFileURL } from "node:url";
import { smallestBalancedSize } from "./design.mjs";
import { pairedDifferences, sampleSizeTable, summarizeDifferences } from "./power.mjs";
import { scoreTrial, toCsv } from "./scoring.mjs";

// The preregistered contrasts, each oriented so that the hypothesis predicts
// a positive difference.
export const CONTRASTS = Object.freeze([
  { key: "study1.navigationErrors.C-R", study: 1, measure: "navigationErrors", a: "C", b: "R", cycle: "study1" },
  { key: "study2.correct.contained-reflow.TRNH", study: 2, measure: "correct", a: "contained", b: "reflow", screen: "TRNH", cycle: "study2" },
  { key: "study2.correct.reflow-contained.ACCD", study: 2, measure: "correct", a: "reflow", b: "contained", screen: "ACCD", cycle: "study2" }
]);

export const analyze = (config, records) => {
  const scored = records.map(scoreTrial).map((row) => ({ ...row, correct: row.correct === undefined ? undefined : Number(row.correct) }));
  const contrasts = CONTRASTS.map((contrast) => {
    const summary = summarizeDifferences(pairedDifferences(scored, contrast));
    return {
      ...contrast,
      summary,
      sampleSizes: sampleSizeTable({
        summary,
        deltas: config.power.candidateEffects[contrast.key],
        power: config.power,
        seed: config.seed,
        multiple: smallestBalancedSize(config, contrast.cycle)
      })
    };
  });
  return { scored, report: { experiment: config.experiment, phase: config.phase, trials: scored.length, participants: new Set(scored.map((row) => row.participant)).size, power: config.power, contrasts } };
};

const main = () => {
  const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
  const at = process.argv.indexOf("--out");
  const out = at === -1 ? path.join(root, "dist", "analysis") : process.argv[at + 1];
  const inputs = process.argv.slice(2).filter((arg, i, all) => arg !== "--out" && all[i - 1] !== "--out");
  const config = JSON.parse(fs.readFileSync(path.join(root, "protocol/pilot-config.json"), "utf8"));
  const records = inputs.flatMap((file) => {
    const parsed = JSON.parse(fs.readFileSync(file, "utf8"));
    return Array.isArray(parsed) ? parsed : parsed.records;
  });
  const { scored, report } = analyze(config, records);
  fs.mkdirSync(out, { recursive: true });
  fs.writeFileSync(path.join(out, "trials.csv"), toCsv(scored));
  fs.writeFileSync(path.join(out, "pilot-report.json"), JSON.stringify(report, null, 2) + "\n");
  report.contrasts.forEach((contrast) =>
    contrast.sampleSizes.forEach((row) =>
      console.log(`${contrast.key}\t${row.stratum}\tpilot n=${row.pilotN}\tsd=${Number.isFinite(row.pilotSd) ? row.pilotSd.toFixed(3) : "n/a"}\t${row.sizes.map((s) => `delta ${s.delta}: n=${s.n ?? "n/a"}`).join("\t")}`)
    )
  );
  console.log(`${report.trials} trials from ${report.participants} participant(s) -> ${out}`);
};

if (import.meta.url === pathToFileURL(process.argv[1]).href) {
  main();
}
