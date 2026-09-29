// Trial scoring for EX-VE-TCG-2026-319C. Pure functions from a raw trial
// record (protocol/trial-record.schema.json) to the preregistered measures.
// The operational definitions below are fixed before any data exist; see the
// "Operational definitions" section of the experiment specification.

export const PREFIX = 2;

const normalize = (value) => String(value ?? "").replace(/[\s,]/g, "").toUpperCase();

const sum = (values) => values.reduce((total, value) => total + value, 0);

// A visit is one uninterrupted stay of focus on one element, with the inputs
// made during it. Events before the first focus belong to no visit.
export const visits = (events) =>
  events.reduce((acc, event) => {
    if (event.type === "focus") return [...acc, { target: event.target, t: event.t, inputs: [] }];
    if (event.type === "input" && acc.length > 0 && acc.at(-1).target === event.target) {
      const last = acc.at(-1);
      return [...acc.slice(0, -1), { ...last, inputs: [...last.inputs, event] }];
    }
    return acc;
  }, []);

// Misentry: during a visit to field F the value changed and ended as text of
// at least PREFIX characters that is not a prefix of F's target but begins
// with the first PREFIX characters of another field's target. A typo inside
// the right field is not a navigation error.
export const misentries = (events, targets) =>
  visits(events)
    .filter((visit) => visit.target in targets && visit.inputs.length > 0)
    .map((visit) => ({ field: visit.target, value: String(visit.inputs.at(-1).value ?? "") }))
    .filter(({ field, value }) =>
      value.length >= PREFIX &&
      !targets[field].startsWith(value) &&
      Object.entries(targets).some(([other, target]) => other !== field && value.startsWith(target.slice(0, PREFIX)))
    );

export const shiftTabs = (events) => events.filter((event) => event.type === "keydown" && event.key === "Tab" && event.shift === true).length;

// A backtrack is any focus move to a field earlier in the condition's focus
// order than the field focused before it, whatever the means.
export const backtracks = (events, order) =>
  events
    .filter((event) => event.type === "focus" && order.includes(event.target))
    .map((event) => order.indexOf(event.target))
    .reduce(([count, previous], position) => [count + (previous !== null && position < previous ? 1 : 0), position], [0, null])[0];

const firstOf = (events, type) => events.find((event) => event.type === type);

export const finalValues = (events) =>
  events.filter((event) => event.type === "input").reduce((values, event) => ({ ...values, [event.target]: String(event.value ?? "") }), {});

export const scoreStudy1 = (trial) => {
  const targets = trial.expected;
  const order = trial.focusOrder;
  const wrong = misentries(trial.events, targets);
  const presses = shiftTabs(trial.events);
  const start = firstOf(trial.events, "keydown")?.t ?? trial.events[0]?.t ?? 0;
  const submit = firstOf(trial.events, "submit");
  const values = finalValues(trial.events);
  return {
    navigationErrors: wrong.length + presses,
    misentries: wrong.length,
    correctiveShiftTabs: presses,
    backtracks: backtracks(trial.events, order),
    completionMs: submit ? submit.t - start : null,
    submitted: Boolean(submit),
    finalCorrect: Object.entries(targets).every(([field, target]) => values[field] === target)
  };
};

// ---- Study 2 -----------------------------------------------------------------

export const scrollDistance = (events, axis) =>
  Object.values(
    events
      .filter((event) => event.type === "scroll")
      .reduce((byTarget, event) => ({ ...byTarget, [event.target]: [...(byTarget[event.target] ?? []), event[axis] ?? 0] }), {})
  ).reduce((total, positions) => total + sum(positions.map((position, index) => (index === 0 ? Math.abs(position) : Math.abs(position - positions[index - 1])))), 0);

export const scoreStudy2 = (trial) => {
  const answer = firstOf(trial.events, "answer");
  const given = answer?.value ?? {};
  const parts = Object.entries(trial.expected).map(([key, expected]) => normalize(given[key]) === normalize(expected));
  const start = trial.events[0]?.t ?? 0;
  return {
    correct: answer !== undefined && parts.every(Boolean),
    partsCorrect: parts.filter(Boolean).length,
    answerMs: answer ? answer.t - start : null,
    horizontalScrollPx: scrollDistance(trial.events, "left"),
    verticalScrollPx: scrollDistance(trial.events, "top"),
    focusStops: trial.events.filter((event) => event.type === "focus" && event.inStimulus === true).length
  };
};

export const scoreTrial = (trial) => ({
  participant: trial.participant,
  stratum: trial.stratum,
  study: trial.study,
  condition: trial.condition,
  screen: trial.screen ?? null,
  item: trial.item,
  trialIndex: trial.trialIndex,
  ...(trial.study === 1 ? scoreStudy1(trial) : scoreStudy2(trial))
});

// Tidy rows (one per trial) for the preregistered mixed-effects models.
export const toCsv = (rows) => {
  const columns = [...new Set(rows.flatMap((row) => Object.keys(row)))];
  const cell = (value) => (value === null || value === undefined ? "" : /[",\n]/.test(String(value)) ? `"${String(value).replaceAll('"', '""')}"` : String(value));
  return [columns.join(","), ...rows.map((row) => columns.map((column) => cell(row[column])).join(","))].join("\n") + "\n";
};
