// Participant schedules for EX-VE-TCG-2026-319C. Pure: config in, schedule out.

// Williams design for an even number of conditions: every condition appears
// once per position and every ordered pair of neighbours once, which
// balances first-order carryover. Rows are sequences of condition indices.
export const williams = (n) => {
  const first = Array.from({ length: n }, (_, j) => (j % 2 === 0 ? j / 2 : n - (j + 1) / 2));
  return Array.from({ length: n }, (_, row) => first.map((value) => (value + row) % n));
};

export const study2Cells = (config) =>
  config.study2.screens.flatMap((screen) => config.study2.strategies.map((strategy) => ({ screen, strategy })));

export const study2Sequences = (config) => {
  const cells = study2Cells(config);
  return williams(cells.length).map((row) => row.map((index) => cells[index]));
};

// Participants are numbered within a stratum from 0; the index carries no
// identifying information. Sequence assignment cycles so that any multiple of
// the sequence count is exactly balanced.
export const participantSchedule = (config, stratum, index) => {
  const studies = config.strata[stratum]?.studies;
  if (!studies) throw new Error(`unknown stratum ${stratum}`);
  const s1 = config.study1.sequences;
  const s2 = study2Sequences(config);
  return {
    experiment: config.experiment,
    participant: `${stratum}-${String(index).padStart(3, "0")}`,
    stratum,
    study1: studies.includes(1) ? { sequence: s1[index % s1.length], trialsPerCondition: config.study1.trialsPerCondition } : null,
    study2: studies.includes(2) ? { sequence: s2[index % s2.length], trialsPerCell: config.study2.trialsPerCell } : null
  };
};

export const stratumSchedules = (config, stratum, count) =>
  Array.from({ length: count }, (_, index) => participantSchedule(config, stratum, index));

// How often each sequence is used; a pilot size that is not a multiple of the
// sequence count is reported, not hidden.
export const balance = (schedules, study) =>
  schedules
    .map((schedule) => schedule[study] && JSON.stringify(schedule[study].sequence))
    .filter(Boolean)
    .reduce((counts, key) => ({ ...counts, [key]: (counts[key] ?? 0) + 1 }), {});

export const smallestBalancedSize = (config, study) =>
  study === "study1" ? config.study1.sequences.length : study2Sequences(config).length;
