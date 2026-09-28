// A participant's complete session plan: every trial with its stimulus,
// instruction, expected answer, and focus order. Pure and deterministic, so
// the plan a participant saw can be regenerated from seed, stratum, and index.
import { accdItems, assignItems, study1Items, trnhItems } from "./datasets.mjs";
import { participantSchedule } from "./design.mjs";
import { derive } from "./random.mjs";
import { accountDetail, STUDY1_LAYOUT, STUDY1_ORDERS, study1Screen, transactionHistory } from "./stimuli.mjs";

const study1Instruction = (record) => ({
  kind: "record-card",
  text: "Enter this record into the screen, then press Enter.",
  // The card mirrors the screen's two-column layout so it favors neither order.
  rows: [["custno", "acct"], ["last", "first"], ["dob", "postal"]].map((pair) =>
    pair.map((field) => ({ label: STUDY1_LAYOUT[field].label, value: record[field] }))
  )
});

const trnhInstruction = (item) => ({
  kind: "question",
  text: `Find the transaction dated ${item.question.date} with the description "${item.question.description}". Report its amount and its status.`,
  answers: [{ key: "amount", label: "Amount" }, { key: "status", label: "Status" }]
});

const accdInstruction = () => ({
  kind: "question",
  text: "Report the account's available balance and its overdraft limit.",
  answers: [{ key: "available", label: "Available balance" }, { key: "overdraft", label: "Overdraft limit" }]
});

const study1Trials = (config, schedule, index) => {
  const items = study1Items(derive(config.seed, "study1"), config.study1.trialsPerCondition * config.study1.conditions.length);
  return assignItems(items, schedule.study1.sequence, index, config.study1.sequences.length).flatMap(({ condition, items: assigned }) =>
    assigned.map((item) => ({
      study: 1,
      condition,
      screen: "CENT",
      item: item.item,
      stimulus: study1Screen({ order: condition }),
      instruction: study1Instruction(item.record),
      expected: item.record,
      focusOrder: STUDY1_ORDERS[condition]
    }))
  );
};

const study2Trials = (config, schedule, index, snapshotHtml) => {
  const perScreen = config.study2.trialsPerCell * config.study2.strategies.length;
  const pools = {
    TRNH: trnhItems(derive(config.seed, "trnh"), perScreen),
    ACCD: accdItems(derive(config.seed, "accd"), perScreen)
  };
  const cycle = schedule.study2.sequence.length;
  // Each screen's items are split across its two strategies, rotating with
  // the participant index so item sets are crossed with strategies.
  const assigned = Object.fromEntries(
    config.study2.screens.map((screen) => [
      screen,
      Object.fromEntries(assignItems(pools[screen], config.study2.strategies, index, cycle).map(({ condition, items }) => [condition, items]))
    ])
  );
  return schedule.study2.sequence.flatMap(({ screen, strategy }) =>
    assigned[screen][strategy].map((item) => ({
      study: 2,
      condition: strategy,
      screen,
      item: item.item,
      stimulus: screen === "TRNH"
        ? transactionHistory(snapshotHtml, { rows: item.rows, strategy })
        : accountDetail(snapshotHtml, { values: item.values, strategy }),
      instruction: screen === "TRNH" ? trnhInstruction(item) : accdInstruction(),
      expected: item.answer,
      focusOrder: null
    }))
  );
};

// Study 1 precedes Study 2 for every participant (entry before search).
export const sessionPlan = (config, snapshotHtml, stratum, index) => {
  const schedule = participantSchedule(config, stratum, index);
  const trials = [
    ...(schedule.study1 ? study1Trials(config, schedule, index) : []),
    ...(schedule.study2 ? study2Trials(config, schedule, index, snapshotHtml) : [])
  ];
  return {
    experiment: config.experiment,
    phase: config.phase,
    participant: schedule.participant,
    stratum,
    viewport: { study2: config.study2.viewport },
    trials: trials.map((trial, trialIndex) => ({ ...trial, trialIndex }))
  };
};
