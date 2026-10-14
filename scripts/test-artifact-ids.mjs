import assert from "node:assert/strict";
import test from "node:test";
import {
  ID_PATTERN,
  classify,
  frontMatterId,
  missingIdPaths,
  planAssignments,
  withId,
} from "./artifact-ids.mjs";

const tokens = (...values) => {
  const queue = [...values];
  return () => queue.shift();
};

test("classifies documents by location", () => {
  assert.deepEqual(classify("content/concepts/color/index.md"), { prefix: "CN", area: "VE" });
  assert.deepEqual(
    classify("content/projects/project-atlas/research-note/atlas-0001.md"),
    { prefix: "RR", area: "VE-ATL" },
  );
  assert.deepEqual(
    classify("content/projects/project-atlas/evidence-registry/color.md"),
    { prefix: "EV", area: "VE-ATL" },
  );
  assert.deepEqual(
    classify("content/projects/composition-science/template/markdown-template.md"),
    { prefix: "GV", area: "VE-COMP" },
  );
  assert.deepEqual(classify("content/projects/new-thing/report/x.md"), { prefix: "RR", area: "VE-NEW-THING" });
  assert.deepEqual(classify("content/themes/research/matrix.md"), { prefix: "RR", area: "VE-THM" });
  assert.deepEqual(classify("framework/standards/UI-LAYOUT-INTERACTION.md"), { prefix: "GV", area: "VE" });
  assert.deepEqual(classify("research/frontier/polish.md"), { prefix: "RR", area: "VE" });
  assert.deepEqual(classify("research/frontier/README.md"), { prefix: "GV", area: "VE" });
  assert.deepEqual(classify("CLAUDE.md"), { prefix: "GV", area: "VE" });
});

test("allocates well-formed identifiers and retries on collision", () => {
  const assignments = planAssignments(
    [
      { sourcePath: "content/concepts/color/index.md", year: "2026" },
      { sourcePath: "content/concepts/spacing/index.md", year: "2026" },
    ],
    ["CN-VE-2026-AAAA"],
    tokens("AAAA", "BBBB", "BBBB", "CCCC"),
  );
  assert.deepEqual(assignments, [
    { sourcePath: "content/concepts/color/index.md", id: "CN-VE-2026-BBBB" },
    { sourcePath: "content/concepts/spacing/index.md", id: "CN-VE-2026-CCCC" },
  ]);
  assignments.forEach(({ id }) => assert.match(id, ID_PATTERN));
});

test("inserts the identifier into existing front matter", () => {
  const source = "---\ntitle: Color\n---\n\n# Color\n";
  const updated = withId(source, "CN-VE-2026-ABCD");
  assert.equal(updated, "---\nid: CN-VE-2026-ABCD\ntitle: Color\n---\n\n# Color\n");
  assert.equal(frontMatterId(updated), "CN-VE-2026-ABCD");
});

test("adds front matter when a document has none", () => {
  const updated = withId("# Handoff\n\nBody\n", "GV-VE-2026-ABCD");
  assert.equal(updated, "---\nid: GV-VE-2026-ABCD\n---\n\n# Handoff\n\nBody\n");
  assert.equal(frontMatterId(updated), "GV-VE-2026-ABCD");
});

test("ignores id-like text outside front matter", () => {
  assert.equal(frontMatterId("# Title\n\nid: NOT-AN-ID\n"), null);
  assert.equal(frontMatterId("---\ntitle: x\n---\nid: NOT-AN-ID\n"), null);
});

test("reads only missing-id diagnostics, once per document", () => {
  assert.deepEqual(
    missingIdPaths([
      { code: "missing-id", sourcePath: "b.md" },
      { code: "unknown-purpose", sourcePath: "c.md" },
      { code: "missing-id", sourcePath: "a.md" },
      { code: "missing-id", sourcePath: "b.md" },
    ]),
    ["a.md", "b.md"],
  );
});
