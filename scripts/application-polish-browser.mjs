import { chromium } from "playwright-core";
import { readFile, writeFile } from "node:fs/promises";
import { pathToFileURL } from "node:url";

const evidence = (id, ok, detail) => ({ id, status: ok ? "passed" : "failed", evidence: [detail] });

export async function probePage(page, config) {
  const results = [];
  await page.goto(config.url, { waitUntil: "networkidle" });

  const overflow = await page.evaluate(() => ({
    scrollWidth: document.documentElement.scrollWidth,
    clientWidth: document.documentElement.clientWidth
  }));
  results.push(evidence("layout-horizontal-overflow", overflow.scrollWidth <= overflow.clientWidth,
    `scrollWidth=${overflow.scrollWidth};clientWidth=${overflow.clientWidth}`));

  const focusables = page.locator('a[href],button,input,select,textarea,[tabindex]:not([tabindex="-1"])');
  const count = await focusables.count();
  let focusOk = true;
  let focused = 0;
  for (let i=0; i<Math.min(count, config.maxFocusTargets ?? 50); i++) {
    await focusables.nth(i).focus();
    const state = await focusables.nth(i).evaluate((el) => {
      const s=getComputedStyle(el);
      return {active:document.activeElement===el, outline:s.outlineStyle!=="none" && s.outlineWidth!=="0px", shadow:s.boxShadow!=="none"};
    });
    focused++;
    if (!state.active || (!state.outline && !state.shadow)) focusOk=false;
  }
  results.push(evidence("focus-visible", focusOk, `exercised=${focused};available=${count}`));

  await page.emulateMedia({ reducedMotion:"reduce" });
  const animated = await page.locator("*").evaluateAll((els) => els.filter((el) => {
    const s=getComputedStyle(el);
    const durations=(s.animationDuration+","+s.transitionDuration).split(",").map(x=>parseFloat(x)||0);
    return Math.max(...durations)>0.5;
  }).length);
  results.push(evidence("reduced-motion", animated===0, `elements-over-500ms=${animated}`));

  return results;
}

export async function runBrowserProbes(config, launch = {}) {
  const browser = await chromium.launch({ headless:true, ...launch });
  try {
    const all=[];
    for (const environment of config.environments ?? [{id:"desktop",width:1280,height:800}]) {
      const context=await browser.newContext({viewport:{width:environment.width,height:environment.height}});
      const page=await context.newPage();
      const probes=await probePage(page,config);
      all.push({environment:environment.id,viewport:{width:environment.width,height:environment.height},probes});
      await context.close();
    }
    return {schemaVersion:1,surface:config.surface,url:config.url,generatedAt:new Date().toISOString(),runs:all};
  } finally { await browser.close(); }
}

async function main(){
  const [configPath,outPath="application-polish-probes.json"]=process.argv.slice(2);
  if(!configPath){process.stderr.write("usage: node scripts/application-polish-browser.mjs <config.json> [output.json]\n");process.exitCode=2;return;}
  const config=JSON.parse(await readFile(configPath,"utf8"));
  const report=await runBrowserProbes(config);
  await writeFile(outPath,JSON.stringify(report,null,2)+"\n");
  const failed=report.runs.flatMap(r=>r.probes).filter(p=>p.status==="failed");
  process.stdout.write(`Polish browser probes: ${failed.length?"FAIL":"PASS"} (${failed.length} failed) -> ${outPath}\n`);
  process.exitCode=failed.length?4:0;
}
if(process.argv[1]&&import.meta.url===pathToFileURL(process.argv[1]).href) await main();
