// Synthetic, fictitious item pools for EX-VE-TCG-2026-319C. Items are fixed
// by seed and index (item is a random effect in the analysis); which item set
// a participant meets under which condition is crossed by participant index.
import { derive, integer, uniforms } from "./random.mjs";

const pick = (u, list) => list[Math.floor(u * list.length)];
const digits = (us) => us.map((u) => String(integer(u, 0, 9))).join("");

const LAST = ["ABERNATHY", "BOLAND", "CASTRO", "DUNMORE", "ELLISON", "FARRAH", "GUTIERREZ", "HALVORSEN", "IKEDA", "JANSSEN", "KOWALSKI", "LINDQVIST", "MOREAU", "NAKAMURA", "OKAFOR", "PETROVA", "QUINLAN", "ROSSI", "SANTOS", "TREMBLAY", "UDOKA", "VARGAS", "WEBER", "YILMAZ"];
const FIRST = ["ADA", "BRUNO", "CLARA", "DMITRI", "ESME", "FELIX", "GRETA", "HUGO", "INES", "JONAH", "KIRA", "LEON", "MIRA", "NILS", "OLGA", "PAVEL", "ROSA", "SAMIR", "TESSA", "VIKTOR", "WREN", "XENIA", "YUSUF", "ZOE"];

// ---- Study 1: six-field entry records ------------------------------------

export const STUDY1_FIELDS = Object.freeze(["custno", "acct", "last", "first", "dob", "postal"]);

const pad2 = (n) => String(n).padStart(2, "0");

const study1Candidate = (seed) => {
  const [us] = uniforms(seed, 40);
  return {
    custno: "0" + digits(us.slice(0, 9)),
    acct: `${digits(us.slice(9, 14))}-${digits(us.slice(14, 18))}-${digits(us.slice(18, 20))}`,
    last: pick(us[20], LAST),
    first: pick(us[21], FIRST),
    dob: `${integer(us[22], 1941, 2004)}-${pad2(integer(us[23], 1, 12))}-${pad2(integer(us[24], 1, 28))}`,
    postal: digits(us.slice(25, 30))
  };
};

// Scoring attributes a keystroke run to the field whose target it begins,
// so no two targets in a record may share their first two characters.
export const distinctPrefixes = (record, width = 2) => {
  const prefixes = STUDY1_FIELDS.map((field) => record[field].slice(0, width));
  return new Set(prefixes).size === prefixes.length;
};

const firstDistinct = (seed, attempt = 0) => {
  const candidate = study1Candidate(derive(seed, attempt));
  return distinctPrefixes(candidate) ? candidate : firstDistinct(seed, attempt + 1);
};

export const study1Items = (seed, count) =>
  Array.from({ length: count }, (_, index) => ({ item: `S1-${String(index).padStart(2, "0")}`, record: firstDistinct(derive(seed, `s1:${index}`)) }));

// ---- Study 2: account detail and transaction history ------------------------

const money = (cents) => {
  const sign = cents < 0 ? "-" : "";
  const [whole, fraction] = (Math.abs(cents) / 100).toFixed(2).split(".");
  return `${sign}${whole.replace(/\B(?=(\d{3})+(?!\d))/g, ",")}.${fraction}`;
};

const MERCHANTS = ["CARD 4411 CITY MARKET", "CARD 4411 FUEL STOP 118", "TRANSFER TO SAVINGS 7710", "ACH ELECTRIC UTILITY", "CARD 4411 BOOKSHOP", "ATM WITHDRAWAL 0931", "ONLINE PAYMENT CREDIT CARD", "CARD 4411 PHARMACY", "ACH WATER DISTRICT", "CARD 4411 HARDWARE 22", "CARD 4411 CAFE NORTE", "ACH INSURANCE PREMIUM", "CARD 4411 TRANSIT PASS"];
const CREDITS = ["PAYROLL DEPOSIT ACME CORP", "INTEREST CREDIT", "TRANSFER FROM SAVINGS 7710"];
const STATUSES = ["POSTED", "POSTED", "POSTED", "POSTED", "PENDING", "RETURNED", "REVERSED"];

export const TRNH_ROWS = 12;

const transactionRows = (seed) => {
  const [us] = uniforms(seed, TRNH_ROWS * 4 + 1);
  const opening = integer(us[TRNH_ROWS * 4], 500000, 1500000);
  const drafts = Array.from({ length: TRNH_ROWS }, (_, row) => {
    const [a, b, c, d] = us.slice(row * 4, row * 4 + 4);
    const credit = a < 0.12;
    return {
      day: 27 - row * 2 - integer(b, 0, 1),
      description: credit ? pick(c, CREDITS) : pick(c, MERCHANTS),
      cents: credit ? integer(d, 100, 250000) : -integer(d, 1000, 150000),
      status: pick(b, STATUSES)
    };
  });
  // Newest first; each balance is the running balance after that row.
  const balances = drafts
    .slice()
    .reverse()
    .reduce((acc, row) => [...acc, (acc.at(-1) ?? opening) + row.cents], [])
    .reverse();
  return drafts.map((row, index) => ({
    date: `2026-09-${pad2(Math.max(row.day, 1))}`,
    description: `${row.description}`.slice(0, 27),
    amount: money(row.cents),
    balance: money(balances[index]),
    status: row.status
  }));
};

// A target row must be identifiable by date and description alone.
const uniqueKey = (rows, index) =>
  rows.filter((row) => row.date === rows[index].date && row.description === rows[index].description).length === 1;

export const trnhItems = (seed, count) =>
  Array.from({ length: count }, (_, index) => {
    const rows = transactionRows(derive(seed, `trnh:${index}`));
    // Targets cycle through every row position so the answer is sometimes in
    // the first rows and sometimes near the bottom of the table.
    const preferred = (index * 5) % TRNH_ROWS;
    const target = [...Array(TRNH_ROWS).keys()].map((k) => (preferred + k) % TRNH_ROWS).find((row) => uniqueKey(rows, row));
    return {
      item: `TRNH-${String(index).padStart(2, "0")}`,
      rows,
      question: { date: rows[target].date, description: rows[target].description },
      answer: { amount: rows[target].amount, status: rows[target].status },
      targetRow: target
    };
  });

export const accdItems = (seed, count) =>
  Array.from({ length: count }, (_, index) => {
    const [us] = uniforms(derive(seed, `accd:${index}`), 6);
    const ledger = integer(us[0], 10000, 3000000);
    const holds = integer(us[1], 0, 50000);
    const overdraft = integer(us[2], 0, 5) * 50000;
    return {
      item: `ACCD-${String(index).padStart(2, "0")}`,
      values: {
        ledger: money(ledger),
        available: money(ledger - holds),
        holds: money(holds),
        overdraft: money(overdraft)
      },
      answer: { available: money(ledger - holds), overdraft: money(overdraft) }
    };
  });

// ---- Crossing items with conditions -----------------------------------------

// Items split into as many sets as a participant has conditions; the set
// used with each condition rotates every sequence cycle, so across
// participants each set meets each condition.
export const assignItems = (items, conditions, participantIndex, cycle) => {
  const perSet = items.length / conditions.length;
  const rotation = Math.floor(participantIndex / cycle) % conditions.length;
  return conditions.map((condition, position) => ({
    condition,
    items: items.slice(((position + rotation) % conditions.length) * perSet, ((position + rotation) % conditions.length + 1) * perSet)
  }));
};
