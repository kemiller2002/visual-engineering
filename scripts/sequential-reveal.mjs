// Reference model of the SequentialReveal behavior (CN-VE-TCG-2026-F9F1).
//
// This is a specification artifact, not a runtime. It computes, as pure data,
// when each cell of a character grid becomes *visually* revealed, so the
// behavior's rules can be tested. Implementations (Forma presentation hooks,
// Limen/application orchestration) must be observably equivalent to it.

export const DEFAULTS = Object.freeze({
  charMs: 12,
  lineMs: 40,
  maxDurationMs: 1500,
  skipBlank: true,
  mode: "animated"
});

const clampNonNegative = (value) => (Number.isFinite(value) && value > 0 ? value : 0);

// Reduced motion always wins; an explicit static mode or zero timing is static.
export const resolveMode = ({ mode, reducedMotion, charMs, lineMs, maxDurationMs }) =>
  reducedMotion === true ||
  mode === "immediate" ||
  clampNonNegative(maxDurationMs) === 0 ||
  (clampNonNegative(charMs) === 0 && clampNonNegative(lineMs) === 0)
    ? "immediate"
    : "animated";

const cellKey = (row, column) => `${row}:${column}`;

// Exclusions are runs ({ row, column, length }, 1-based) that are never
// staged: fields, actions, status and message regions, consequential values.
const excludedCells = (exclusions) =>
  new Set(
    exclusions.flatMap(({ row, column, length }) =>
      Array.from({ length }, (_, offset) => cellKey(row, column + offset))
    )
  );

// Row-major list of cells with their raw (uncompressed) cost in ms.
const costedCells = (rows, { charMs, lineMs, skipBlank }, excluded) =>
  rows.flatMap((text, rowIndex) => {
    const row = rowIndex + 1;
    const cells = [...text].map((character, columnIndex) => {
      const column = columnIndex + 1;
      const isExcluded = excluded.has(cellKey(row, column));
      const isBlank = character.trim() === "";
      return {
        row,
        column,
        character,
        staged: !isExcluded,
        cost: isExcluded || (skipBlank && isBlank) ? 0 : charMs
      };
    });
    const lineCost = rowIndex < rows.length - 1 ? lineMs : 0;
    return cells.map((cell, index) =>
      index === cells.length - 1 ? { ...cell, trailing: lineCost } : { ...cell, trailing: 0 }
    );
  });

const withTimes = (cells, scale) =>
  cells.reduce(
    ({ elapsed, timed }, cell) => {
      const visibleAt = cell.staged ? elapsed + cell.cost * scale : 0;
      const next = cell.staged ? visibleAt + cell.trailing * scale : elapsed + cell.trailing * scale;
      return {
        elapsed: next,
        timed: [...timed, { row: cell.row, column: cell.column, character: cell.character, visibleAt }]
      };
    },
    { elapsed: 0, timed: [] }
  ).timed;

export const planReveal = (rows, options = {}, exclusions = []) => {
  const config = { ...DEFAULTS, ...options };
  const charMs = clampNonNegative(config.charMs);
  const lineMs = clampNonNegative(config.lineMs);
  const maxDurationMs = clampNonNegative(config.maxDurationMs);
  const mode = resolveMode({ ...config, charMs, lineMs, maxDurationMs });
  const excluded = excludedCells(exclusions);

  if (mode === "immediate") {
    const cells = rows.flatMap((text, rowIndex) =>
      [...text].map((character, columnIndex) => ({
        row: rowIndex + 1,
        column: columnIndex + 1,
        character,
        visibleAt: 0
      }))
    );
    return { mode, scale: 0, durationMs: 0, cells };
  }

  const costed = costedCells(rows, { charMs, lineMs, skipBlank: config.skipBlank }, excluded);
  const rawDuration = costed.reduce((sum, cell) => sum + cell.cost + cell.trailing, 0);
  // Compress proportionally rather than truncate, so order is preserved and
  // the whole reveal always completes within the cap.
  const scale = rawDuration > maxDurationMs ? maxDurationMs / rawDuration : 1;
  const cells = withTimes(costed, scale);
  const durationMs = cells.reduce((latest, cell) => Math.max(latest, cell.visibleAt), 0);
  return { mode, scale, durationMs, cells };
};

// Visual state at time t. Interruption at time i completes the reveal: every
// cell is visible from i onward. Interruption never hides anything.
export const visibleAt = (plan, t, interruptedAt = Infinity) =>
  plan.cells.filter((cell) => t >= interruptedAt || cell.visibleAt <= t);

export const isComplete = (plan, t, interruptedAt = Infinity) =>
  visibleAt(plan, t, interruptedAt).length === plan.cells.length;

// The semantic layer is independent of time: the full text is available at
// every instant. This function exists so tests can assert it explicitly.
export const semanticTextAt = (rows) => rows.join("\n");
