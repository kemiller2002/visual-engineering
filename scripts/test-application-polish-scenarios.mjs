import test from "node:test";
import assert from "node:assert/strict";
import { validateScenarioResult } from "./application-polish-scenarios.mjs";

const s={id:"save",probe:"persistence-truth"};
test("passed scenario requires retained evidence",()=>{
 const r=validateScenarioResult({status:"passed",evidence:["saved=true"]},s);
 assert.equal(r.status,"passed"); assert.deepEqual(r.errors,[]);
});
test("unsupported scenario requires explanation",()=>{
 const r=validateScenarioResult({status:"unsupported"},s);
 assert.equal(r.status,"failed"); assert.match(r.errors.join("\n"),/notes/);
});
test("pass without evidence cannot pass",()=>{
 const r=validateScenarioResult({status:"passed",evidence:[]},s);
 assert.equal(r.status,"failed"); assert.match(r.errors.join("\n"),/requires evidence/);
});
test("not-run remains explicit incomplete evidence",()=>{
 const r=validateScenarioResult({status:"not-run",notes:"fixture unavailable"},s);
 assert.equal(r.status,"not-run");
});
