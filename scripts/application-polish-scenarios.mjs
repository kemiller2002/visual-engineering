import { chromium } from "playwright-core";
import { readFile, writeFile } from "node:fs/promises";
import { pathToFileURL, fileURLToPath } from "node:url";
import { resolve, dirname } from "node:path";

const ROOT=dirname(fileURLToPath(import.meta.url));
const validStatus=new Set(["passed","failed","unsupported","not-run"]);

export function validateScenarioResult(result, scenario) {
  const errors=[];
  if(!result || typeof result!=="object") errors.push("scenario returned no result");
  if(!validStatus.has(result?.status)) errors.push("scenario status must be passed, failed, unsupported, or not-run");
  if(result?.status==="passed" && (!Array.isArray(result.evidence)||result.evidence.length===0)) errors.push("passed scenario requires evidence");
  if(result?.status==="unsupported" && !result?.notes) errors.push("unsupported scenario requires notes");
  return {id:scenario.id,probe:scenario.probe,status:errors.length?"failed":result.status,evidence:result.evidence??[],notes:result.notes??null,errors};
}

export async function runScenarioAdapter(page, scenario, baseDir=process.cwd()) {
  const modulePath=resolve(baseDir,scenario.adapter);
  const adapter=await import(pathToFileURL(modulePath));
  if(typeof adapter.run!=="function") return {id:scenario.id,probe:scenario.probe,status:"failed",evidence:[],notes:null,errors:["adapter must export async run(context)"]};
  try {
    const result=await adapter.run({page,scenario,assert(condition,detail){if(!condition) throw new Error(detail);}});
    return validateScenarioResult(result,scenario);
  } catch(error) {
    return {id:scenario.id,probe:scenario.probe,status:"failed",evidence:[],notes:error.message,errors:[error.message]};
  }
}

export async function runScenarios(config, launch={}) {
  const browser=await chromium.launch({headless:true,...launch});
  try {
    const context=await browser.newContext({viewport:config.viewport??{width:1280,height:800}});
    const page=await context.newPage();
    if(config.url) await page.goto(config.url,{waitUntil:"networkidle"});
    const results=[];
    const baseDir=config.baseDir??process.cwd();
    for(const scenario of config.scenarios??[]) results.push(await runScenarioAdapter(page,scenario,baseDir));
    await context.close();
    return {schemaVersion:1,surface:config.surface,url:config.url??null,generatedAt:new Date().toISOString(),scenarios:results};
  } finally {await browser.close();}
}

async function main(){
  const [configPath,outPath="application-polish-scenarios.json"]=process.argv.slice(2);
  if(!configPath){process.stderr.write("usage: node scripts/application-polish-scenarios.mjs <config.json> [output.json]\n");process.exitCode=2;return;}
  const config=JSON.parse(await readFile(configPath,"utf8"));
  config.baseDir=resolve(dirname(resolve(configPath)),config.baseDir??".");
  const report=await runScenarios(config);
  await writeFile(outPath,JSON.stringify(report,null,2)+"\n");
  const failed=report.scenarios.filter(x=>x.status==="failed");
  const incomplete=report.scenarios.filter(x=>x.status==="unsupported"||x.status==="not-run");
  process.stdout.write(`Polish scenarios: ${failed.length?"FAIL":incomplete.length?"INCOMPLETE":"PASS"} (${failed.length} failed, ${incomplete.length} incomplete) -> ${outPath}\n`);
  process.exitCode=failed.length?4:incomplete.length?3:0;
}
if(process.argv[1]&&import.meta.url===pathToFileURL(process.argv[1]).href) await main();
