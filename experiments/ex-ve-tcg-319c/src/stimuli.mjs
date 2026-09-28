// Stimulus markup for EX-VE-TCG-2026-319C, built only from Forma's public
// CharacterGrid contracts (protocol/forma-snapshot). Pure: data in, HTML out.
import { STUDY1_FIELDS } from "./datasets.mjs";

const escape = (text) =>
  String(text).replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll('"', "&quot;");

const attributes = (map) =>
  Object.entries(map)
    .filter(([, value]) => value !== undefined && value !== false)
    .map(([name, value]) => (value === true ? name : `${name}="${escape(value)}"`))
    .join(" ");

const run = (tag, cls, [row, col, len], extra, content) =>
  `<${tag} ${attributes({ class: cls, ...extra, "data-ef-row": row, "data-ef-col": col, "data-ef-len": len })}>${content}</${tag}>`;

// ---- Study 1: two-column, six-field entry screen ---------------------------

// Two columns of three; labels left of each field. Positions are identical
// in both conditions; only source (and therefore focus) order differs.
export const STUDY1_LAYOUT = Object.freeze({
  custno: { label: "Customer number", row: 6, labelCol: 2, fieldCol: 20, len: 10, inputmode: "numeric" },
  acct: { label: "Account number", row: 6, labelCol: 42, fieldCol: 60, len: 14 },
  last: { label: "Last name", row: 7, labelCol: 2, fieldCol: 20, len: 20 },
  first: { label: "First name", row: 7, labelCol: 42, fieldCol: 60, len: 15 },
  dob: { label: "Date of birth", row: 8, labelCol: 2, fieldCol: 20, len: 10 },
  postal: { label: "Postal code", row: 8, labelCol: 42, fieldCol: 60, len: 10, inputmode: "numeric" }
});

const LABEL_LEN = 17;

// Row-major is the family rule (CG-6); column-major exists only as the
// experimental contrast and deliberately violates CG-6.
export const STUDY1_ORDERS = Object.freeze({
  R: [...STUDY1_FIELDS].sort((a, b) => STUDY1_LAYOUT[a].row - STUDY1_LAYOUT[b].row || STUDY1_LAYOUT[a].labelCol - STUDY1_LAYOUT[b].labelCol),
  C: [...STUDY1_FIELDS].sort((a, b) => STUDY1_LAYOUT[a].labelCol - STUDY1_LAYOUT[b].labelCol || STUDY1_LAYOUT[a].row - STUDY1_LAYOUT[b].row)
});

const leader = (text) => {
  const room = LABEL_LEN - text.length;
  return room >= 2 ? ` ${". ".repeat(LABEL_LEN).slice(0, room - 2).trimEnd().padEnd(room - 2, " ")}:` : "";
};

const study1Field = (id, name) => {
  const spec = STUDY1_LAYOUT[name];
  return [
    run("label", "ef-character-grid__label", [spec.row, spec.labelCol, LABEL_LEN], { for: `${id}-${name}` },
      `${escape(spec.label)}<span class="ef-character-grid__leader" aria-hidden="true">${leader(spec.label)}</span>`),
    run("input", "ef-character-grid__field", [spec.row, spec.fieldCol, spec.len],
      { id: `${id}-${name}`, name, type: "text", maxlength: spec.len, autocomplete: "off", inputmode: spec.inputmode }, "")
      .replace("></input>", ">")
  ];
};

export const study1Screen = ({ order, id = "s1", profile = "ibm-3270" }) => {
  const fields = STUDY1_ORDERS[order];
  if (!fields) throw new Error(`unknown Study 1 condition ${order}`);
  return [
    `<div ${attributes({ class: "ef-character-grid", "data-ef-profile": profile, "data-ef-rows": 24, "data-ef-status-rows": 1, "data-ef-columns": 80, "data-ef-narrow": "contained", "data-condition": order })}>`,
    `  <div class="ef-character-grid__viewport" role="region" aria-labelledby="${id}-title" tabindex="0">`,
    `    <form class="ef-character-grid__surface" id="${id}-form">`,
    `      ${run("span", "ef-character-grid__text", [1, 2, 4], {}, "CENT")}`,
    `      ${run("h2", "ef-character-grid__text", [1, 33, 14], { id: `${id}-title`, "data-ef-emphasis": "intensified" }, "CUSTOMER ENTRY")}`,
    `      ${run("p", "ef-character-grid__text", [4, 2, 34], {}, "Type the record, then press Enter.")}`,
    ...fields.flatMap((name) => study1Field(id, name)).map((line) => `      ${line}`),
    `      ${run("p", "ef-character-grid__message", [21, 2, 78], { id: `${id}-message`, role: "status", "data-ef-severity": "information" }, '<span class="ef-character-grid__severity"><span>INFO</span></span> CENT000I Type the record and press Enter.')}`,
    `      <div class="ef-character-grid__group" role="group" aria-label="Function keys">`,
    `        ${run("button", "ef-character-grid__key", [22, 2, 10], { type: "submit", name: "action", value: "enter", "data-ef-action": "enter" }, "<kbd>Enter</kbd>=Save")}`,
    `      </div>`,
    `      ${run("p", "ef-character-grid__status", [25, 2, 78], { role: "status" }, '<span class="ef-character-grid__indicator"><span>READY</span></span>')}`,
    `    </form>`,
    `  </div>`,
    `</div>`
  ].join("\n");
};

// ---- Study 2: Forma's Account Detail and Transaction History ----------------

const blockFor = (html, screen) => {
  const marker = `aria-labelledby="character-grid-workflow-${screen.toLowerCase()}-title"`;
  const block = html.split(/(?=<div class="ef-character-grid" )/).find((part) => part.includes(marker));
  if (!block) throw new Error(`screen ${screen} not found in the Forma workflow snapshot`);
  // The last grid also carries the closing tag of the workflow's wrapper.
  const trimmed = block.trimEnd();
  return trimmed.endsWith("</div>\n</div>\n</div>") ? trimmed.replace(/\n<\/div>$/, "") : trimmed;
};

// Replace exactly one occurrence; anything else means the snapshot changed
// under the stimulus and must fail loudly rather than produce a wrong screen.
const replaceOnce = (text, from, to) => {
  const count = text.split(from).length - 1;
  if (count !== 1) throw new Error(`expected exactly one ${JSON.stringify(from)} in the snapshot, found ${count}`);
  return text.replace(from, to);
};

const withNarrow = (block, strategy) => {
  if (!["contained", "reflow"].includes(strategy)) throw new Error(`unknown strategy ${strategy}`);
  return replaceOnce(block, 'data-ef-narrow="contained"', `data-ef-narrow="${strategy}" data-condition="${strategy}"`);
};

const ACCD_SNAPSHOT_VALUES = Object.freeze({ ledger: "12,480.55", available: "12,230.55", holds: "250.00", overdraft: "1,000.00" });

export const accountDetail = (snapshotHtml, { values, strategy }) =>
  Object.entries(ACCD_SNAPSHOT_VALUES).reduce(
    (block, [key, original]) => replaceOnce(block, `>${original}</span>`, `>${escape(values[key])}</span>`),
    withNarrow(blockFor(snapshotHtml, "ACCD"), strategy)
  );

const transactionRow = (row) =>
  `          <tr><td>${escape(row.date)}</td><td>${escape(row.description)}</td><td data-ef-align="end">${escape(row.amount)}</td><td data-ef-align="end">${escape(row.balance)}</td><td>${escape(row.status)}</td></tr>`;

export const transactionHistory = (snapshotHtml, { rows, strategy }) => {
  const block = withNarrow(blockFor(snapshotHtml, "TRNH"), strategy);
  const body = block.match(/<tbody>[\s\S]*?<\/tbody>/g);
  if (!body || body.length !== 1) throw new Error("expected exactly one <tbody> in the TRNH snapshot");
  return block.replace(body[0], `<tbody>\n${rows.map(transactionRow).join("\n")}\n          </tbody>`);
};
