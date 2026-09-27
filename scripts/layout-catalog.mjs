import { readFile, readdir } from "node:fs/promises";
import path from "node:path";
import { pathToFileURL } from "node:url";

// Minimal JSON Schema subset used by content/layouts/layout-family.schema.json:
// type, required, enum, pattern, minLength, minItems, items, properties.
const typeOf = (value) =>
  Array.isArray(value) ? "array" : value === null ? "null" : typeof value;

const at = (pointer, key) => `${pointer}/${key}`;

export const validateSchema = (schema, value, pointer = "") => {
  const typeErrors =
    schema.type && typeOf(value) !== schema.type
      ? [`${pointer || "/"} must be ${schema.type}`]
      : [];
  if (typeErrors.length > 0) return typeErrors;

  const enumErrors =
    schema.enum && !schema.enum.includes(value)
      ? [`${pointer || "/"} must be one of ${schema.enum.join(", ")}`]
      : [];
  const patternErrors =
    schema.pattern && typeof value === "string" && !new RegExp(schema.pattern).test(value)
      ? [`${pointer || "/"} must match ${schema.pattern}`]
      : [];
  const lengthErrors =
    schema.minLength !== undefined && typeof value === "string" && value.length < schema.minLength
      ? [`${pointer || "/"} must not be empty`]
      : [];
  const itemCountErrors =
    schema.minItems !== undefined && Array.isArray(value) && value.length < schema.minItems
      ? [`${pointer || "/"} must have at least ${schema.minItems} item(s)`]
      : [];
  const requiredErrors =
    schema.required && typeOf(value) === "object"
      ? schema.required
          .filter((key) => !(key in value))
          .map((key) => `${at(pointer, key)} is required`)
      : [];
  const propertyErrors =
    schema.properties && typeOf(value) === "object"
      ? Object.entries(schema.properties)
          .filter(([key]) => key in value)
          .flatMap(([key, child]) => validateSchema(child, value[key], at(pointer, key)))
      : [];
  const itemErrors =
    schema.items && Array.isArray(value)
      ? value.flatMap((item, index) => validateSchema(schema.items, item, at(pointer, index)))
      : [];

  return [
    ...enumErrors,
    ...patternErrors,
    ...lengthErrors,
    ...itemCountErrors,
    ...requiredErrors,
    ...propertyErrors,
    ...itemErrors
  ];
};

// Registry IDs are declared as headings (### EV-...) or as the first cell of a
// table row (| HY-... |). Mentions elsewhere do not declare an ID.
const EVIDENCE_HEADING = /^#{2,4}\s+(EV-[A-Z0-9]+(?:-[A-Z0-9]+)+)\b/gm;
const HYPOTHESIS_ROW = /^\|\s*(HY-[A-Z0-9]+(?:-[A-Z0-9]+)+)\s*\|/gm;

const matchesOf = (pattern, text) => [...text.matchAll(pattern)].map((match) => match[1]);

export const collectRegistryIds = (texts) => ({
  evidence: new Set(texts.flatMap((text) => matchesOf(EVIDENCE_HEADING, text))),
  hypotheses: new Set(texts.flatMap((text) => matchesOf(HYPOTHESIS_ROW, text)))
});

const duplicates = (values) =>
  values.filter((value, index) => values.indexOf(value) !== index);

export const crossReferenceErrors = (family, ids) => {
  const gapIds = (family.formaPrimitives?.knownGaps ?? []).map((gap) => gap.id);
  const verificationIds = (family.verification ?? []).map((check) => check.id);
  const assumptionIds = (family.assumptions ?? []).map((assumption) => assumption.id);
  return [
    ...(family.evidence ?? [])
      .filter((id) => !ids.evidence.has(id))
      .map((id) => `${family.id}: evidence ${id} is not declared in any evidence registry`),
    ...(family.hypotheses ?? [])
      .filter((id) => !ids.hypotheses.has(id))
      .map((id) => `${family.id}: hypothesis ${id} is not declared in any hypothesis registry`),
    ...(family.gaps ?? [])
      .filter((id) => !gapIds.includes(id))
      .map((id) => `${family.id}: gap ${id} is not described in formaPrimitives.knownGaps`),
    ...duplicates(verificationIds).map((id) => `${family.id}: duplicate verification id ${id}`),
    ...duplicates(assumptionIds).map((id) => `${family.id}: duplicate assumption id ${id}`),
    ...(family.responsive?.noPageOverflow === true
      ? []
      : [`${family.id}: responsive.noPageOverflow must be true`])
  ];
};

export const indexErrors = (index, families) => {
  const listed = index.families.map((entry) => entry.id);
  const loaded = families.map((family) => family.id);
  return [
    ...duplicates(listed).map((id) => `index lists ${id} more than once`),
    ...index.families
      .filter((entry) => !index.supportStates.includes(entry.status))
      .map((entry) => `index status for ${entry.id} is not a support state`),
    ...index.families
      .map((entry) => [entry, families.find((family) => family.id === entry.id)])
      .filter(([entry, family]) => family && (family.status !== entry.status || family.name !== entry.name))
      .map(([entry]) => `index entry for ${entry.id} disagrees with its record`),
    ...listed
      .filter((id) => !loaded.includes(id))
      .map((id) => `index lists ${id} but no record was loaded`)
  ];
};

export const validateCatalog = ({ index, schema, families, ids }) => {
  const errors = [
    ...indexErrors(index, families),
    ...families.flatMap((family) =>
      validateSchema(schema, family).map((error) => `${family.id ?? "unknown"}: ${error}`)
    ),
    ...families.flatMap((family) => crossReferenceErrors(family, ids))
  ];
  return { valid: errors.length === 0, errors, familyCount: families.length };
};

const readJson = async (file) => JSON.parse(await readFile(file, "utf8"));

const markdownFilesUnder = async (directory) => {
  const entries = await readdir(directory, { withFileTypes: true });
  const nested = await Promise.all(
    entries.map((entry) => {
      const full = path.join(directory, entry.name);
      return entry.isDirectory()
        ? markdownFilesUnder(full)
        : Promise.resolve(entry.name.endsWith(".md") ? [full] : []);
    })
  );
  return nested.flat();
};

export const loadCatalog = async (root) => {
  const index = await readJson(path.join(root, "content/layouts/index.json"));
  const schema = await readJson(path.join(root, "content/layouts/layout-family.schema.json"));
  const families = await Promise.all(
    index.families.map((entry) => readJson(path.join(root, entry.path)))
  );
  const registryFiles = await markdownFilesUnder(path.join(root, "content/projects"));
  const texts = await Promise.all(registryFiles.map((file) => readFile(file, "utf8")));
  return { index, schema, families, ids: collectRegistryIds(texts) };
};

const main = async () => {
  const root = path.resolve(path.dirname(new URL(import.meta.url).pathname), "..");
  const result = validateCatalog(await loadCatalog(root));
  result.errors.forEach((error) => console.error(`ERROR ${error}`));
  console.log(
    result.valid
      ? `layout catalog valid (${result.familyCount} famil${result.familyCount === 1 ? "y" : "ies"})`
      : `layout catalog invalid: ${result.errors.length} error(s)`
  );
  process.exitCode = result.valid ? 0 : 1;
};

if (import.meta.url === pathToFileURL(process.argv[1]).href) {
  await main();
}
