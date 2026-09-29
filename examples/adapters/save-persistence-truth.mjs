export async function run({page,assert}) {
  const button=page.locator("[data-polish-action=save]");
  const status=page.locator("[data-polish-state]");
  if(await button.count()===0) return {status:"unsupported",notes:"surface does not expose data-polish-action=save"};
  await button.click();
  await status.waitFor({state:"visible"});
  const observed=await status.getAttribute("data-polish-state");
  assert(["saving","saved"].includes(observed),"save action did not expose saving or saved state");
  return {status:"passed",evidence:[`observed persistence presentation state=${observed}`]};
}
