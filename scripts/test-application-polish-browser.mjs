import test from "node:test";
import assert from "node:assert/strict";
import { probePage } from "./application-polish-browser.mjs";

function fakePage({overflow=false,focusVisible=true,animated=0}={}){
  const focusable={
    count:async()=>2,
    nth:()=>({focus:async()=>{},evaluate:async()=>({active:true,outline:focusVisible,shadow:false})})
  };
  return {
    goto:async()=>{},
    evaluate:async()=>({scrollWidth:overflow?401:375,clientWidth:375}),
    locator:(selector)=>selector==="*"?{evaluateAll:async()=>animated}:focusable,
    emulateMedia:async()=>{}
  };
}
test("objective browser probes pass when invariants hold",async()=>{
 const r=await probePage(fakePage(),{url:"http://example.test"});
 assert.deepEqual(r.map(x=>x.status),["passed","passed","passed"]);
});
test("overflow is reported as failed evidence",async()=>{
 const r=await probePage(fakePage({overflow:true}),{url:"http://example.test"});
 assert.equal(r[0].status,"failed");
});
test("missing visible focus is reported",async()=>{
 const r=await probePage(fakePage({focusVisible:false}),{url:"http://example.test"});
 assert.equal(r[1].status,"failed");
});
test("long motion under reduced motion is reported",async()=>{
 const r=await probePage(fakePage({animated:2}),{url:"http://example.test"});
 assert.equal(r[2].status,"failed");
});
