import { readFile } from "node:fs/promises";
import path from "node:path";
import { pathToFileURL } from "node:url";

// Mechanical checks for screens expressed in the LAY-TERMINAL-CHARACTER-GRID
// vocabulary. Every rule here is a relational rule from the catalog entry or
// CN-VE-TCG-2026-6CA0; nothing is specific to one product or profile.

export const RUN_KINDS = Object.freeze([
  "heading", "text", "value", "field", "table", "message", "status", "action"
]);
export const SEVERITIES = Object.freeze(["information", "success", "warning", "validation", "error"]);
export const PROTECTED_KINDS = Object.freeze(["heading", "text", "value", "message", "status"]);

const textLength = (text = "") => [...text].length;

export const actionText = (run) => `${run.key}=${run.label}`;

export const runLength = (run) =>
  run.kind === "action"
    ? textLength(actionText(run))
    : run.kind === "table"
      ? Math.max(...run.columns.map((column) => column.column + column.length)) - run.columns[0].column
      : run.length ?? textLength(run.text);

export const runHeight = (run) => (run.kind === "table" ? 1 + run.maxRows : 1);

// Segments are the cell runs a run occupies: one per row for simple runs;
// the header row plus one row per visible record (or the empty-state line)
// for tables.
export const segmentsOf = (run) =>
  run.kind === "table"
    ? Array.from({ length: runHeight(run) }, (_, offset) => ({
        id: run.id,
        row: run.row + offset,
        column: run.columns[0].column,
        length: runLength(run)
      }))
    : [{ id: run.id, row: run.row, column: run.column, length: runLength(run) }];

const cellsOf = (segment) =>
  Array.from({ length: segment.length }, (_, offset) => `${segment.row}:${segment.column + offset}`);

const address = ({ row, column }, columns) => (row - 1) * columns + (column - 1);

const startOf = (run) =>
  run.kind === "table" ? { row: run.row, column: run.columns[0].column } : { row: run.row, column: run.column };

// Device status rows (DF-VE-TCG-2026-1320) follow the application rows,
// outside the presentation space, and hold status runs only.
export const statusRowsOf = (screen) => screen.statusRows ?? 0;

const lastRowFor = (screen, run) =>
  run.kind === "status" ? screen.rows + statusRowsOf(screen) : screen.rows;

export const boundsErrors = (screen) =>
  screen.runs
    .flatMap((run) => segmentsOf(run).map((segment) => ({ segment, lastRow: lastRowFor(screen, run) })))
    .flatMap(({ segment, lastRow }) => [
      ...(segment.row < 1 || segment.row > lastRow
        ? [`${segment.id}: row ${segment.row} is outside 1..${lastRow}`]
        : []),
      ...(segment.column < 1 ? [`${segment.id}: column ${segment.column} is before column 1`] : []),
      ...(segment.column + segment.length - 1 > screen.columns
        ? [`${segment.id}: row ${segment.row} ends at column ${segment.column + segment.length - 1}, beyond ${screen.columns} (no row wrap)`]
        : []),
      ...(segment.length < 1 ? [`${segment.id}: length must be at least 1`] : [])
    ]);

export const collisionErrors = (screen) => {
  const owners = screen.runs
    .flatMap(segmentsOf)
    .flatMap((segment) => cellsOf(segment).map((cell) => [cell, segment.id]));
  const firstOwner = owners.reduce(
    (map, [cell, id]) => (map.has(cell) ? map : new Map([...map, [cell, id]])),
    new Map()
  );
  const clashes = owners
    .filter(([cell, id]) => firstOwner.get(cell) !== id)
    .map(([cell, id]) => `${id} overlaps ${firstOwner.get(cell)} at ${cell}`);
  return [...new Set(clashes)];
};

export const overflowErrors = (screen) =>
  screen.runs.flatMap((run) =>
    run.kind === "table"
      ? [
          ...run.columns
            .filter((column) => textLength(column.header) > column.length)
            .map((column) => `${run.id}.${column.id}: header exceeds ${column.length} cells`),
          ...(run.records.length > run.maxRows
            ? [`${run.id}: ${run.records.length} records exceed ${run.maxRows} visible rows (page instead)`]
            : []),
          ...run.records.flatMap((record, index) =>
            run.columns
              .filter((column) => textLength(record[column.id] ?? "") > column.length)
              .map((column) => `${run.id} record ${index + 1}.${column.id}: value exceeds ${column.length} cells`)
          ),
          ...(run.records.length === 0 && !run.emptyText
            ? [`${run.id}: an empty table must declare emptyText`]
            : []),
          ...(run.records.length === 0 && textLength(run.emptyText) > runLength(run)
            ? [`${run.id}: emptyText exceeds table width`]
            : [])
        ]
      : ["value", "message", "status"].includes(run.kind) && textLength(run.text) > run.length
        ? [`${run.id}: text of ${textLength(run.text)} cells exceeds declared length ${run.length}`]
        : run.kind === "field" && textLength(run.value ?? "") > run.length
          ? [`${run.id}: value exceeds field length ${run.length}`]
          : []
  );

export const sourceOrderErrors = (screen) =>
  screen.runs.slice(1).flatMap((run, index) => {
    const previous = screen.runs[index];
    return address(startOf(run), screen.columns) < address(startOf(previous), screen.columns)
      ? [`${run.id} precedes ${previous.id} in row-major order but follows it in source order`]
      : [];
  });

export const kindErrors = (screen) =>
  screen.runs.flatMap((run) => [
    ...(RUN_KINDS.includes(run.kind) ? [] : [`${run.id}: unknown run kind ${run.kind}`]),
    ...(run.kind === "message" && !SEVERITIES.includes(run.severity)
      ? [`${run.id}: message severity must be one of ${SEVERITIES.join(", ")}`]
      : []),
    ...(run.kind === "field" && !(Number.isInteger(run.length) && run.length > 0)
      ? [`${run.id}: field length must be a positive integer`]
      : []),
    ...(run.kind === "action" && !(run.key && run.label) ? [`${run.id}: action needs key and label`] : [])
  ]);

// describedBy may name one element or several (hint plus error), matching
// native aria-describedby.
const describedByIds = (run) =>
  run.describedBy === undefined ? [] : [run.describedBy].flat();

const idsBefore = (screen, index) => new Set(screen.runs.slice(0, index).map((run) => run.id));
const allIds = (screen) => new Set(screen.runs.map((run) => run.id));

export const relationshipErrors = (screen) =>
  screen.runs.flatMap((run, index) => [
    ...(run.kind === "field" && !run.labelledBy ? [`${run.id}: editable field has no label`] : []),
    ...(run.kind === "field" && run.labelledBy && !idsBefore(screen, index).has(run.labelledBy)
      ? [`${run.id}: label ${run.labelledBy} must exist and precede the field`]
      : []),
    ...(run.kind === "value" && run.labelledBy && !allIds(screen).has(run.labelledBy)
      ? [`${run.id}: label ${run.labelledBy} does not exist`]
      : []),
    ...(run.kind === "field" && run.invalid === true && !run.describedBy
      ? [`${run.id}: invalid field must reference the message that explains it`]
      : []),
    ...describedByIds(run)
      .filter((id) => !allIds(screen).has(id))
      .map((id) => `${run.id}: describedBy ${id} does not exist`)
  ]);

export const reservedRegionErrors = (screen, reserved) =>
  screen.runs.flatMap((run) => [
    ...(run.kind === "message" && run.row !== reserved.messageRow
      ? [`${run.id}: message must be on reserved row ${reserved.messageRow}`]
      : []),
    ...(run.kind === "status" && run.row !== reserved.statusRow
      ? [`${run.id}: status must be on reserved row ${reserved.statusRow}`]
      : []),
    ...(run.kind === "action" && !reserved.actionRows.includes(run.row)
      ? [`${run.id}: action must be on a reserved action row`]
      : []),
    ...(!["message", "status", "action"].includes(run.kind) &&
    segmentsOf(run).some((segment) =>
      [reserved.messageRow, reserved.statusRow, ...reserved.actionRows].includes(segment.row)
    )
      ? [`${run.id}: content may not occupy a reserved row`]
      : [])
  ]);

export const requiredRegionErrors = (screen) =>
  [
    ["heading", "screen identity heading"],
    ["message", "message region"],
    ["action", "action affordance"]
  ]
    .filter(([kind]) => !screen.runs.some((run) => run.kind === kind))
    .map(([, name]) => `${screen.id}: missing ${name}`);

// A state is the base screen with runs replaced or added by id.
export const composeState = (screen, state) => {
  const overrides = new Map((state.runs ?? []).map((run) => [run.id, run]));
  const merged = screen.runs.map((run) => (overrides.has(run.id) ? { ...run, ...overrides.get(run.id) } : run));
  const added = (state.runs ?? []).filter((run) => !screen.runs.some((base) => base.id === run.id));
  const runs = [...merged, ...added].sort(
    (a, b) => address(startOf(a), screen.columns) - address(startOf(b), screen.columns)
  );
  return { ...screen, id: `${screen.id}#${state.id}`, runs: added.length ? runs : merged };
};

export const screenErrors = (screen, reserved) => [
  ...kindErrors(screen),
  ...requiredRegionErrors(screen),
  ...boundsErrors(screen),
  ...collisionErrors(screen),
  ...overflowErrors(screen),
  ...sourceOrderErrors(screen),
  ...relationshipErrors(screen),
  ...reservedRegionErrors(screen, reserved)
].map((error) => (error.startsWith(screen.id) ? error : `${screen.id}: ${error}`));

export const focusOrder = (screen) =>
  screen.runs
    .filter((run) => (run.kind === "field" && run.disabled !== true) || run.kind === "action")
    .map((run) => run.id);

// Density is measured over the application's rows only (DF-VE-TCG-2026-1320).
export const density = (screen) => {
  const used = new Set(
    screen.runs.flatMap(segmentsOf).filter((segment) => segment.row <= screen.rows).flatMap(cellsOf)
  ).size;
  return { usedCells: used, totalCells: screen.rows * screen.columns, ratio: used / (screen.rows * screen.columns) };
};

export const transitionErrors = (workflow) => {
  const screens = new Map(workflow.screens.map((screen) => [screen.id, screen]));
  return (workflow.applicationTransitions?.transitions ?? []).flatMap((transition) => [
    ...(screens.has(transition.from) ? [] : [`transition from unknown screen ${transition.from}`]),
    ...(transition.to && !screens.has(transition.to) ? [`transition to unknown screen ${transition.to}`] : []),
    ...(screens.has(transition.from) &&
    !screens.get(transition.from).runs.some((run) => run.kind === "action" && run.id === transition.action)
      ? [`${transition.from}: transition uses action ${transition.action}, which the screen does not present`]
      : [])
  ]);
};

export const validateWorkflow = (workflow) => {
  const reserved = workflow.reserved;
  const geometry = {
    rows: workflow.geometry.rows,
    columns: workflow.geometry.columns,
    statusRows: workflow.geometry.statusRows ?? 0
  };
  const expanded = workflow.screens.flatMap((screen) => [
    { ...screen, ...geometry },
    ...(screen.states ?? []).map((state) => composeState({ ...screen, ...geometry }, state))
  ]);
  const errors = [
    ...expanded.flatMap((screen) => screenErrors(screen, reserved)),
    ...transitionErrors(workflow)
  ];
  return {
    valid: errors.length === 0,
    errors,
    screens: expanded.map((screen) => ({
      id: screen.id,
      focusOrder: focusOrder(screen),
      density: density(screen)
    }))
  };
};

const main = async () => {
  const root = path.resolve(path.dirname(new URL(import.meta.url).pathname), "..");
  const file =
    process.argv.slice(2).find((argument) => !argument.startsWith("--")) ??
    "content/layouts/reference-workflows/customer-account-inquiry.json";
  const workflow = JSON.parse(await readFile(path.resolve(root, file), "utf8"));
  const result = validateWorkflow(workflow);
  result.errors.forEach((error) => console.error(`ERROR ${error}`));
  if (process.argv.includes("--report")) {
    result.screens.forEach((screen) =>
      console.log(
        `${screen.id}\tcells ${screen.density.usedCells}/${screen.density.totalCells} (${(screen.density.ratio * 100).toFixed(1)}%)\tfocus ${screen.focusOrder.join(" > ")}`
      )
    );
  }
  console.log(result.valid ? `workflow valid (${result.screens.length} screen states)` : `workflow invalid: ${result.errors.length} error(s)`);
  process.exitCode = result.valid ? 0 : 1;
};

if (import.meta.url === pathToFileURL(process.argv[1]).href) {
  await main();
}
