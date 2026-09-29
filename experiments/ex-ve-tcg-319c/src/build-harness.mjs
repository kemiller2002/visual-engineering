// Builds one self-contained session folder per participant:
//   node experiments/ex-ve-tcg-319c/src/build-harness.mjs --stratum N --index 0 [--out DIR]
// Serve the folder over http (for example `npx http-server DIR`) and open
// index.html. The frozen Forma snapshot is verified against its manifest
// first, so a session can never run on stimuli other than the recorded ones.
import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import { fileURLToPath, pathToFileURL } from "node:url";
import { sessionPlan } from "./plan.mjs";

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), "..");
const read = (relative) => fs.readFileSync(path.join(root, relative), "utf8");
const sha256 = (text) => crypto.createHash("sha256").update(text).digest("hex");

export const snapshotErrors = (manifest, readFile) =>
  Object.entries(manifest.files)
    .filter(([file, hash]) => sha256(readFile(file)) !== hash)
    .map(([file]) => `forma-snapshot/${file} does not match its manifest hash`);

// Pure: inputs to the files of one session folder.
export const sessionFiles = ({ config, snapshotHtml, css, harness, stratum, index }) => {
  const plan = sessionPlan(config, snapshotHtml, stratum, index);
  return {
    folder: plan.participant,
    files: {
      "index.html": harness.html,
      "harness.mjs": harness.script,
      "forma.css": css,
      "plan.json": JSON.stringify(plan, null, 2) + "\n"
    }
  };
};

const argument = (name, fallback) => {
  const at = process.argv.indexOf(`--${name}`);
  return at === -1 ? fallback : process.argv[at + 1];
};

const main = () => {
  const manifest = JSON.parse(read("protocol/forma-snapshot/manifest.json"));
  const errors = snapshotErrors(manifest, (file) => read(`protocol/forma-snapshot/${file}`));
  if (errors.length > 0) {
    errors.forEach((error) => console.error(error));
    process.exitCode = 1;
    return;
  }
  const { folder, files } = sessionFiles({
    config: JSON.parse(read("protocol/pilot-config.json")),
    snapshotHtml: read("protocol/forma-snapshot/character-grid-workflow.html"),
    css: read("protocol/forma-snapshot/all.css"),
    harness: { html: read("harness/index.html"), script: read("harness/harness.mjs") },
    stratum: argument("stratum", "N"),
    index: Number(argument("index", "0"))
  });
  const out = path.resolve(argument("out", path.join(root, "dist")), folder);
  fs.mkdirSync(out, { recursive: true });
  Object.entries(files).forEach(([name, content]) => fs.writeFileSync(path.join(out, name), content));
  console.log(`session ${folder} written to ${out} (Forma ${manifest.commit.slice(0, 7)})`);
};

if (import.meta.url === pathToFileURL(process.argv[1]).href) {
  main();
}
