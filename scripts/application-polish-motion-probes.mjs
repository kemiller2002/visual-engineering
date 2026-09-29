// Reusable adversarial motion probes (VE-MOT-002;
// framework/standards/MOTION-AND-INTERACTION.md, "Verification requirements").
//
// Probes are framework-neutral: a JSON configuration names selectors,
// actions and semantic readers for any web surface. Each probe returns one of
// pass | fail | not-applicable | unknown with evidence, the exercised
// environment and the state/seam it covers. Results fold into the motion
// evidence model of the Application Polish gate (VE-MOT-001) through
// applyMotionProbeResults.
//
// Probes are engineering screens. They do not prove universal human comfort,
// perceived smoothness or performance; they detect specific, observable
// violations of the motion invariants.
import { chromium } from "playwright-core";
import { readFile, writeFile } from "node:fs/promises";
import { dirname, resolve } from "node:path";
import { pathToFileURL } from "node:url";

export const PROBE_CHECKS = Object.freeze({
  "motion-semantic-authority": "semanticAuthority",
  "motion-rapid-repeat": "rapidRepeat",
  "motion-interruption": "interruption",
  "motion-reduced-substitution": "reducedMotion",
  "motion-focus": "focus",
  "motion-direct-lag": "directManipulation",
  "motion-progress-bounds": "progressBounds",
  "motion-cadence-stops": "cadenceStops",
  "motion-rejected-drop": "boundaries",
  "motion-boundary": "boundaries",
  "motion-progressive-fallback": "progressiveFallback",
  "motion-concurrent-composition": "concurrentComposition",
  "motion-semantic-cue": "semanticIndependence"
});

export const RESULT_STATUS = Object.freeze(["pass", "fail", "not-applicable", "unknown"]);

const SPATIAL = [
  "transform", "translate", "scale", "rotate", "top", "right", "bottom", "left", "inset",
  "width", "height", "inline-size", "block-size", "margin", "offset", "offset-distance", "background-position", "clip-path"
];

// ---------------------------------------------------------------------------
// Result helpers (pure)

const result = (status, evidence = [], notes = null, observations = {}) => ({ status, evidence, notes, observations });
const pass = (evidence, observations) => result("pass", evidence, null, observations);
const fail = (evidence, notes, observations) => result("fail", evidence, notes, observations);
const unknown = (notes, observations) => result("unknown", [], notes, observations);
const notApplicable = (notes) => result("not-applicable", [], notes);

const isSpatial = (property) => SPATIAL.some((name) => property === name || property.startsWith(`${name}-`));

// ---------------------------------------------------------------------------
// Page primitives

const frame = (page) => page.evaluate(() => new Promise((done) => requestAnimationFrame(() => requestAnimationFrame(() => done()))));

const frames = (page, count) => Array.from({ length: count }).reduce((chain) => chain.then(() => frame(page)), Promise.resolve());

const sequence = (items, run) => items.reduce((chain, item, index) => chain.then(async (acc) => [...acc, await run(item, index)]), Promise.resolve([]));

// Run one declarative action. `times` repeats it back to back without waiting.
export const runAction = async (page, action) => {
  const times = action.times ?? 1;
  const once = async () => {
    const target = action.target ? page.locator(action.target).first() : null;
    switch (action.type) {
      case "click": return target.click({ force: action.force ?? false, noWaitAfter: true });
      case "check": return target.check({ force: true });
      case "focus": return target.focus();
      case "hover": return target.hover();
      case "press": return target ? target.press(action.key) : page.keyboard.press(action.key);
      case "wait": return page.waitForTimeout(action.ms ?? 0);
      case "evaluate": return page.evaluate(action.script);
      case "reducedMotion": return page.emulateMedia({ reducedMotion: action.value ?? "reduce" });
      default: throw new Error(`unknown action type ${action.type}`);
    }
  };
  return sequence(Array.from({ length: times }), once);
};

const runActions = (page, actions = []) => sequence(actions, (action) => runAction(page, action));

// Read a semantic value from the DOM: attribute, property, text or a script.
export const readValue = (page, reader) =>
  page.evaluate((spec) => {
    if (spec.script) return (0, eval)(spec.script);
    const element = document.querySelector(spec.selector);
    if (!element) return { missing: spec.selector };
    if (spec.attribute) return element.getAttribute(spec.attribute);
    if (spec.property) return element[spec.property];
    if (spec.style) return getComputedStyle(element).getPropertyValue(spec.style);
    if (spec.rect) return element.getBoundingClientRect()[spec.rect];
    return element.textContent.trim();
  }, reader);

const semanticHolds = async (page, semantic) => {
  if (!semantic) return { ok: true, actual: null };
  const actual = await readValue(page, semantic);
  const expected = semantic.equals;
  return { ok: actual !== null && typeof actual === "object" ? false : String(actual) === String(expected), actual, expected };
};

// Running animations/transitions within a watched subtree.
const runningAnimations = (page, watch) =>
  page.evaluate((selector) => {
    const roots = selector ? [...document.querySelectorAll(selector)] : [document.documentElement];
    const nodes = [...new Set(roots.flatMap((root) => [root, ...root.querySelectorAll("*")]))];
    const key = (node) => node.id || node.getAttribute("data-probe-id") || `${node.tagName.toLowerCase()}#${[...document.querySelectorAll(node.tagName)].indexOf(node)}`;
    return document.getAnimations()
      .filter((animation) => animation.playState === "running" && nodes.includes(animation.effect?.target))
      .map((animation) => {
        const timing = animation.effect.getComputedTiming();
        const keyframeProperties = [...new Set((animation.effect.getKeyframes?.() ?? []).flatMap((frame) => Object.keys(frame).filter((name) => !["offset", "easing", "composite", "computedOffset"].includes(name))))];
        return {
          target: key(animation.effect.target),
          pseudo: animation.effect.pseudoElement ?? null,
          kind: animation.constructor.name,
          property: animation.transitionProperty ?? null,
          name: animation.animationName ?? null,
          properties: animation.transitionProperty ? [animation.transitionProperty] : keyframeProperties.map((name) => name.replace(/[A-Z]/g, (letter) => `-${letter.toLowerCase()}`)),
          duration: Number(timing.duration) || 0,
          iterations: timing.iterations
        };
      });
  }, watch ?? null);

const settle = (page, watch, timeoutMs = 3000) =>
  page.evaluate(({ selector, timeout }) => {
    const roots = selector ? [...document.querySelectorAll(selector)] : [document.documentElement];
    const nodes = new Set(roots.flatMap((root) => [root, ...root.querySelectorAll("*")]));
    const finite = document.getAnimations().filter((animation) => nodes.has(animation.effect?.target) && animation.effect.getComputedTiming().iterations !== Infinity);
    const started = performance.now();
    return Promise.race([
      Promise.all(finite.map((animation) => animation.finished.catch(() => null))).then(() => performance.now() - started),
      new Promise((done) => setTimeout(() => done(-1), timeout))
    ]);
  }, { selector: watch ?? null, timeout: timeoutMs });

// ---------------------------------------------------------------------------
// Probes. Each receives { page, config } and returns a result.

const probes = {
  async "motion-semantic-authority"({ page, config }) {
    await runActions(page, config.actions);
    const running = await runningAnimations(page, config.watch);
    const semantic = await semanticHolds(page, config.semantic);
    const evidence = [`semantic=${JSON.stringify(semantic.actual)} expected=${JSON.stringify(semantic.expected)}`, `animations-running-at-read=${running.length}`];
    if (!semantic.ok) return fail(evidence, "semantic state did not update immediately after the action");
    return pass(evidence, { runningAtRead: running.length });
  },

  async "motion-rapid-repeat"({ page, config }) {
    const bursts = await sequence(config.actions ?? [], async (action) =>
      sequence(Array.from({ length: action.times ?? 1 }), async () => {
        await runAction(page, { ...action, times: 1 });
        return runningAnimations(page, config.watch);
      })
    );
    const samples = bursts.flat();
    const duplicates = Math.max(0, ...samples.map((running) => {
      const keys = running.flatMap((item) => item.properties.map((property) => `${item.target}|${item.pseudo}|${property}`));
      return keys.length - new Set(keys).size;
    }));
    const settleMs = await settle(page, config.watch, config.maxSettleMs ?? 3000);
    const semantic = await semanticHolds(page, config.semantic);
    const evidence = [`actions=${samples.length}`, `max-duplicate-property-animations=${duplicates}`, `settle-ms=${Math.round(settleMs)}`, `semantic=${JSON.stringify(semantic.actual)}`];
    if (!semantic.ok) return fail(evidence, `final semantic state ${JSON.stringify(semantic.actual)} is not ${JSON.stringify(semantic.expected)}`);
    if (duplicates > 0) return fail(evidence, "rapid input queued concurrent animations on the same property instead of retargeting");
    if (settleMs < 0) return fail(evidence, "motion did not settle within maxSettleMs after rapid input");
    return pass(evidence, { duplicates, settleMs });
  },

  async "motion-interruption"({ page, config }) {
    await runActions(page, config.first);
    await page.waitForTimeout(config.interruptAfterMs ?? 30);
    await runActions(page, config.then);
    const settleMs = await settle(page, config.watch, config.maxSettleMs ?? 3000);
    const remaining = await runningAnimations(page, config.watch);
    const semantic = await semanticHolds(page, config.semantic);
    const styles = await sequence(config.expectStyles ?? [], async (spec) => ({ ...spec, actual: String(await readValue(page, spec)).trim() }));
    const mismatched = styles.filter((spec) => spec.actual !== String(spec.equals));
    const evidence = [`settle-ms=${Math.round(settleMs)}`, `remaining-finite=${remaining.filter((item) => item.iterations !== Infinity).length}`, `semantic=${JSON.stringify(semantic.actual)}`, ...styles.map((spec) => `${spec.selector} ${spec.style ?? spec.rect}=${spec.actual}`)];
    if (!semantic.ok) return fail(evidence, "interrupted motion did not converge on the latest semantic state");
    if (settleMs < 0 || mismatched.length) return fail(evidence, "interrupted motion did not converge on the latest visual state");
    return pass(evidence);
  },

  async "motion-reduced-substitution"({ page, config }) {
    await page.emulateMedia({ reducedMotion: "reduce" });
    await page.reload();
    await runActions(page, config.actions);
    const sampled = (await sequence(Array.from({ length: config.frames ?? 3 }), async () => {
      const running = await runningAnimations(page, config.watch);
      await frame(page);
      return running;
    })).flat();
    const threshold = config.maxSpatialMs ?? 50;
    const spatial = sampled.filter((item) => item.properties.some(isSpatial) && item.duration > threshold);
    const repeating = sampled.filter((item) => item.iterations === Infinity);
    const semantic = await semanticHolds(page, config.semantic);
    const evidence = [
      `reduced-motion=reduce`,
      `spatial-over-${threshold}ms=${[...new Set(spatial.map((item) => `${item.target}${item.pseudo ?? ""}:${item.properties.join("+")}`))].join(",") || "none"}`,
      `repeating=${repeating.length}`,
      `semantic=${JSON.stringify(semantic.actual)}`
    ];
    if (!semantic.ok) return fail(evidence, "reduced motion lost the final semantic state");
    if (spatial.length && !config.allowSpatial) return fail(evidence, "spatial motion survives reduced motion");
    if (repeating.length && !config.allowRepeating) return fail(evidence, "repeated motion keeps running under reduced motion");
    return pass(evidence);
  },

  async "motion-focus"({ page, config }) {
    await runActions(page, config.actions);
    const immediate = await page.evaluate((selector) => document.activeElement?.matches(selector) ?? false, config.expectFocus);
    await settle(page, config.watch, config.maxSettleMs ?? 3000);
    const settled = await page.evaluate((selector) => {
      const element = document.activeElement;
      if (!element || !element.matches(selector)) return { matches: false };
      const style = getComputedStyle(element);
      const hidden = style.visibility === "hidden" || Number(style.opacity) === 0 || element.closest("[inert]") !== null || element.getClientRects().length === 0;
      return { matches: true, hidden };
    }, config.expectFocus);
    const evidence = [`focus-immediate=${immediate}`, `focus-after-settle=${settled.matches}`, `focused-hidden=${settled.hidden ?? "n/a"}`];
    if (!immediate) return fail(evidence, "focus waited on animation or moved to the wrong element");
    if (!settled.matches || settled.hidden) return fail(evidence, "focus was lost or left on an invisible element after the transition");
    return pass(evidence);
  },

  async "motion-direct-lag"({ page, config }) {
    const handle = page.locator(config.handle).first();
    const box = await handle.boundingBox();
    if (!box) return unknown(`handle ${config.handle} is not rendered`);
    const axis = config.axis ?? "x";
    const ratio = config.ratio ?? 1;
    const tolerance = config.tolerancePx ?? 1;
    const readTracker = () => page.locator(config.tracker).first().boundingBox();
    const start = await readTracker();
    const origin = { x: box.x + box.width / 2, y: box.y + box.height / 2 };
    await page.mouse.move(origin.x, origin.y);
    await page.mouse.down();
    const steps = await sequence(config.steps ?? [20, 60, 120], async (delta) => {
      await page.mouse.move(origin.x + (axis === "x" ? delta : 0), origin.y + (axis === "y" ? delta : 0));
      await frame(page);
      const now = await readTracker();
      const moved = axis === "x" ? now.x - start.x : now.y - start.y;
      const smoothing = (await runningAnimations(page, config.tracker)).filter((item) => item.properties.some(isSpatial));
      return { delta, expected: delta * ratio, moved, smoothing: smoothing.length };
    });
    await page.mouse.up();
    const lagging = steps.filter((step) => Math.abs(step.moved - step.expected) > tolerance);
    const smoothed = steps.filter((step) => step.smoothing > 0);
    const evidence = steps.map((step) => `pointer=${step.delta} expected=${step.expected} visual=${Math.round(step.moved * 100) / 100} smoothing-animations=${step.smoothing}`);
    if (smoothed.length) return fail(evidence, "a transition or animation smooths the directly manipulated visual");
    if (lagging.length) return fail(evidence, `visual lagged the pointer by more than ${tolerance}px`);
    return pass(evidence);
  },

  async "motion-progress-bounds"({ page, config }) {
    const tolerance = config.tolerance ?? 0.005;
    const samples = (await sequence(config.steps ?? [], async (step) => {
      await runActions(page, step.actions ?? [step]);
      return sequence(Array.from({ length: config.framesPerStep ?? 12 }), async () => {
        const [authoritative, visual] = [Number(await readValue(page, config.authoritative)), Number(await readValue(page, config.visual))];
        await frame(page);
        return { authoritative, visual };
      });
    })).flat();
    if (samples.length === 0 || samples.some((sample) => !Number.isFinite(sample.authoritative) || !Number.isFinite(sample.visual))) {
      return unknown("progress readers did not return numbers", { samples: samples.length });
    }
    const exceeded = samples.filter((sample) => sample.visual > sample.authoritative + tolerance);
    const evidence = [`samples=${samples.length}`, `max-excess=${Math.max(0, ...samples.map((sample) => sample.visual - sample.authoritative)).toFixed(4)}`, `tolerance=${tolerance}`];
    if (exceeded.length) return fail(evidence, "visual progress exceeded the authoritative value");
    return pass(evidence);
  },

  async "motion-cadence-stops"({ page, config }) {
    await runActions(page, config.start);
    const before = (await runningAnimations(page, config.watch)).filter((item) => item.iterations === Infinity || item.iterations > 1);
    if (before.length === 0 && !config.allowNoActivity) return notApplicable("no repeated activity motion was running before the stop action");
    await runActions(page, config.stop);
    await page.waitForTimeout(config.graceMs ?? 100);
    const after = (await runningAnimations(page, config.watch)).filter((item) => item.iterations === Infinity || item.iterations > 1);
    const semantic = await semanticHolds(page, config.semantic);
    const evidence = [`repeating-before=${before.length}`, `repeating-after=${after.length}`, `status=${JSON.stringify(semantic.actual)}`];
    if (after.length) return fail(evidence, "repeated activity motion continued after the activity stopped");
    if (!semantic.ok) return fail(evidence, "activity status did not report the stopped state");
    return pass(evidence);
  },

  async "motion-rejected-drop"({ page, config }) {
    const item = page.locator(config.item).first();
    const before = await item.boundingBox();
    const handle = await page.locator(config.handle ?? config.item).first().boundingBox();
    if (!before || !handle) return unknown("draggable item is not rendered");
    const origin = { x: handle.x + handle.width / 2, y: handle.y + handle.height / 2 };
    await page.mouse.move(origin.x, origin.y);
    await page.mouse.down();
    await page.mouse.move(origin.x + (config.to?.dx ?? 0), origin.y + (config.to?.dy ?? 0), { steps: 4 });
    await page.mouse.up();
    await settle(page, config.watch ?? config.item, config.maxSettleMs ?? 3000);
    const after = await item.boundingBox();
    const accepted = config.acceptedMarker ? await page.locator(config.acceptedMarker).count() : 0;
    const semantic = await semanticHolds(page, config.semantic);
    const drift = Math.hypot(after.x - before.x, after.y - before.y);
    const evidence = [`return-drift-px=${drift.toFixed(2)}`, `accepted-markers=${accepted}`, `semantic=${JSON.stringify(semantic.actual)}`];
    if (!semantic.ok) return fail(evidence, "rejected drop changed the authoritative state");
    if (accepted > 0) return fail(evidence, "rejected drop presented success");
    if (drift > (config.tolerancePx ?? 1)) return fail(evidence, "rejected item did not return to its authoritative position");
    return pass(evidence);
  },

  async "motion-boundary"({ page, config }) {
    await runActions(page, config.actions);
    const readings = await sequence(Array.from({ length: config.frames ?? 30 }), async () => {
      const value = Number(await readValue(page, config.measure));
      await frame(page);
      return value;
    });
    const tolerance = config.tolerance ?? 0.5;
    const outside = readings.filter((value) => (config.min !== undefined && value < config.min - tolerance) || (config.max !== undefined && value > config.max + tolerance));
    const evidence = [`frames=${readings.length}`, `min-observed=${Math.min(...readings)}`, `max-observed=${Math.max(...readings)}`, `bounds=[${config.min ?? "-∞"}, ${config.max ?? "∞"}]`];
    if (readings.some((value) => !Number.isFinite(value))) return unknown("boundary reader did not return numbers");
    if (outside.length) return fail(evidence, "settling overshot a legal boundary");
    return pass(evidence);
  },

  async "motion-progressive-fallback"({ page, config }) {
    const disabled = config.disable ?? {};
    await page.route("**/*", async (route) => {
      const response = await route.fetch();
      const type = response.headers()["content-type"] ?? "";
      if (!type.includes("css") && !type.includes("html")) return route.fulfill({ response });
      const body = (disabled.supports ?? []).reduce(
        (text, feature) => text.replace(new RegExp(`@supports([^{]*?)${feature.replace(/[.*+?^${}()|[\]\\]/g, "\\$&")}`, "g"), "@supports (not-a-supported-feature: 1)"),
        await response.text()
      );
      return route.fulfill({ response, body });
    });
    await page.addInitScript((apis) => apis.forEach((path) => {
      const [owner, name] = path.split(".");
      const target = owner === "document" ? Document.prototype : owner === "CSS" ? globalThis.CSS : globalThis[owner];
      try { Object.defineProperty(target, name, { value: undefined, configurable: true }); } catch { /* not removable */ }
    }), disabled.apis ?? []);
    await page.reload();
    await runActions(page, config.actions);
    const checks = await sequence(config.semantics ?? (config.semantic ? [config.semantic] : []), (semantic) => semanticHolds(page, semantic));
    const overflow = await page.evaluate(() => document.documentElement.scrollWidth > document.documentElement.clientWidth);
    const evidence = [`disabled=${JSON.stringify(disabled)}`, ...checks.map((check) => `semantic=${JSON.stringify(check.actual)} expected=${JSON.stringify(check.expected)}`), `horizontal-overflow=${overflow}`];
    await page.unroute("**/*");
    if (checks.length === 0) return unknown("no semantic assertion configured for the fallback");
    if (checks.some((check) => !check.ok)) return fail(evidence, "behavior is not correct without the progressive feature");
    if (overflow) return fail(evidence, "layout overflows without the progressive feature");
    return pass(evidence);
  },

  async "motion-concurrent-composition"({ page, config }) {
    await page.evaluate((selector) => {
      window.__polishCancels = [];
      const root = document.querySelector(selector) ?? document.documentElement;
      ["transitioncancel", "animationcancel"].forEach((type) =>
        root.addEventListener(type, (event) => window.__polishCancels.push(`${type}:${event.propertyName ?? event.animationName}`), true));
    }, config.watch ?? null);
    await Promise.all((config.actions ?? []).map((action) => runAction(page, action)));
    await settle(page, config.watch, config.maxSettleMs ?? 3000);
    const cancels = await page.evaluate(() => window.__polishCancels);
    const styles = await sequence(config.expectStyles ?? [], async (spec) => ({ ...spec, actual: String(await readValue(page, spec)).trim() }));
    const mismatched = styles.filter((spec) => spec.actual !== String(spec.equals));
    const evidence = [`cancelled=${cancels.join(",") || "none"}`, ...styles.map((spec) => `${spec.selector} ${spec.style}=${spec.actual} expected=${spec.equals}`)];
    if (mismatched.length) return fail(evidence, "a concurrent effect clobbered another effect's final state");
    if (config.forbidCancel && cancels.length) return fail(evidence, "a concurrent effect cancelled another running effect");
    return pass(evidence);
  },

  // The consequential change must stay perceivable with all motion removed:
  // compare the target's non-motion presentation before and after the change.
  async "motion-semantic-cue"({ page, config }) {
    const describe = () =>
      page.evaluate((selector) => {
        const element = document.querySelector(selector);
        if (!element) return null;
        const states = ["aria-selected", "aria-checked", "aria-pressed", "aria-current", "aria-expanded", "aria-invalid", "aria-busy", "data-state"];
        return {
          state: states.map((name) => element.getAttribute(name)).concat([element.matches(":checked") || element.querySelector(":checked") !== null]),
          text: element.textContent.replace(/\s+/g, " ").trim(),
          style: ["color", "background-color", "border-top-color", "border-top-width", "outline-style", "font-weight", "text-decoration-line"].map((name) => getComputedStyle(element).getPropertyValue(name))
        };
      }, config.target);
    await page.emulateMedia({ reducedMotion: "reduce" });
    await page.addStyleTag({ content: "*, *::before, *::after { animation: none !important; transition: none !important; }" });
    const before = await describe();
    if (!before) return unknown(`semantic-cue target ${config.target} is not rendered`);
    await runActions(page, config.actions);
    const after = await describe();
    const changed = {
      state: JSON.stringify(before.state) !== JSON.stringify(after.state),
      text: before.text !== after.text,
      style: JSON.stringify(before.style) !== JSON.stringify(after.style)
    };
    const evidence = [`state-changed=${changed.state}`, `text-changed=${changed.text}`, `static-style-changed=${changed.style}`];
    if (!changed.state && !changed.text && !changed.style) return fail(evidence, "the change is perceivable only through motion");
    return pass(evidence);
  }
};

// ---------------------------------------------------------------------------
// Runner

const environmentOf = async (browser, page, environment) => ({
  id: environment.id,
  browser: browser.browserType().name(),
  version: browser.version(),
  viewport: page.viewportSize(),
  reducedMotion: await page.evaluate(() => matchMedia("(prefers-reduced-motion: reduce)").matches),
  forcedColors: await page.evaluate(() => matchMedia("(forced-colors: active)").matches)
});

export const runProbe = async (page, probe, url) => {
  const implementation = probes[probe.probe];
  const base = { id: probe.id, probe: probe.probe, check: PROBE_CHECKS[probe.probe] ?? null, entry: probe.entry ?? null, state: probe.state ?? null };
  if (!implementation) return { ...base, ...unknown(`unknown probe ${probe.probe}`) };
  if (probe.notApplicable) return { ...base, ...notApplicable(probe.notApplicable) };
  try {
    await page.emulateMedia({ reducedMotion: "no-preference" });
    await page.goto(probe.url ?? url, { waitUntil: "load" });
    const missing = await page.evaluate((selectors) => selectors.filter((selector) => !document.querySelector(selector)), probe.requires ?? []);
    if (missing.length) return { ...base, ...unknown(`required elements not rendered: ${missing.join(", ")}`) };
    return { ...base, ...(await implementation({ page, config: probe })) };
  } catch (error) {
    return { ...base, ...unknown(`probe could not observe the surface: ${error.message}`) };
  }
};

export const runMotionProbes = async (config, launch = {}) => {
  const browser = await chromium.launch({ headless: true, ...launch });
  try {
    const runs = await sequence(config.environments ?? [{ id: "desktop", width: 1280, height: 800 }], async (environment) => {
      const context = await browser.newContext({ viewport: { width: environment.width, height: environment.height } });
      const results = await sequence(config.probes ?? [], async (probe) => {
        const page = await context.newPage();
        const outcome = await runProbe(page, probe, config.url);
        const exercised = await environmentOf(browser, page, environment).catch(() => ({ id: environment.id }));
        await page.close();
        return { ...outcome, environment: exercised };
      });
      await context.close();
      return results;
    });
    return {
      schemaVersion: 1,
      surface: config.surface,
      url: config.url ?? null,
      generatedAt: new Date().toISOString(),
      limitations: "Automated motion probes are engineering evidence for specific observable invariants. They do not prove universal human comfort, perceived smoothness or performance.",
      results: runs.flat()
    };
  } finally {
    await browser.close();
  }
};

// ---------------------------------------------------------------------------
// Integration with the Application Polish motion evidence (VE-MOT-001)

const CHECK_STATUS = { pass: "passed", fail: "failed", "not-applicable": "not-applicable", unknown: "unknown" };
const RANK = { failed: 3, unknown: 2, passed: 1, "not-applicable": 0 };

// Fold probe results into evidence. Several results for the same entry/check
// (for example across environments) combine to the most severe status:
// failed > unknown > passed > not-applicable. Returns new evidence.
export const applyMotionProbeResults = (evidence, report) => {
  const byCheck = (report.results ?? [])
    .filter((item) => item.entry && item.check)
    .reduce((map, item) => map.set(`${item.entry}|${item.check}`, [...(map.get(`${item.entry}|${item.check}`) ?? []), item]), new Map());
  const combine = (items) => {
    const statuses = items.map((item) => CHECK_STATUS[item.status] ?? "unknown");
    const status = statuses.reduce((worst, current) => (RANK[current] > RANK[worst] ? current : worst), statuses[0]);
    const cite = (item) => `probe:${item.probe}#${item.id}@${item.environment?.id ?? "?"}: ${item.status}${item.notes ? ` (${item.notes})` : ""}; ${item.evidence.join("; ")}`;
    return {
      status,
      probe: items[0].probe,
      evidence: items.filter((item) => item.status !== "not-applicable").map(cite),
      ...(status === "not-applicable" ? { rationale: items.map((item) => item.notes).filter(Boolean).join("; ") || "probe reported not applicable" } : {})
    };
  };
  const entries = (evidence.motion?.entries ?? []).map((entry) => ({
    ...entry,
    checks: {
      ...(entry.checks ?? {}),
      ...Object.fromEntries(
        [...byCheck.entries()]
          .filter(([key]) => key.startsWith(`${entry.id}|`))
          .map(([key, items]) => [key.split("|")[1], combine(items)])
      )
    }
  }));
  const unmatched = [...byCheck.keys()].filter((key) => !entries.some((entry) => key.startsWith(`${entry.id}|`)));
  return {
    evidence: { ...evidence, motion: { ...(evidence.motion ?? {}), entries } },
    unmatched
  };
};

// ---------------------------------------------------------------------------
// CLI

async function main() {
  const args = process.argv.slice(2);
  const [configPath, outPath = "application-polish-motion-probes.json"] = args.filter((arg, index) => !arg.startsWith("--") && !args[index - 1]?.startsWith("--"));
  const option = (name) => (args.includes(name) ? args[args.indexOf(name) + 1] : null);
  if (!configPath) {
    process.stderr.write("usage: node scripts/application-polish-motion-probes.mjs <config.json> [report.json] [--evidence evidence.json --write merged.json]\n");
    process.exitCode = 2;
    return;
  }
  const config = JSON.parse(await readFile(configPath, "utf8"));
  if (config.url && !/^[a-z]+:/.test(config.url)) config.url = pathToFileURL(resolve(dirname(resolve(configPath)), config.url)).href;
  const executablePath = process.env.POLISH_CHROMIUM_EXECUTABLE;
  const report = await runMotionProbes(config, executablePath ? { executablePath } : {});
  await writeFile(outPath, `${JSON.stringify(report, null, 2)}\n`);
  if (option("--evidence")) {
    const evidence = JSON.parse(await readFile(option("--evidence"), "utf8"));
    const merged = applyMotionProbeResults(evidence, report);
    await writeFile(option("--write") ?? option("--evidence"), `${JSON.stringify(merged.evidence, null, 2)}\n`);
    merged.unmatched.forEach((key) => process.stderr.write(`probe result has no matching motion entry: ${key}\n`));
  }
  const count = (status) => report.results.filter((item) => item.status === status).length;
  process.stdout.write(`Motion probes: ${count("fail") ? "FAIL" : count("unknown") ? "INCOMPLETE" : "PASS"} (${count("pass")} pass, ${count("fail")} fail, ${count("not-applicable")} not applicable, ${count("unknown")} unknown) -> ${outPath}\n`);
  process.exitCode = count("fail") ? 4 : count("unknown") ? 3 : 0;
}

if (process.argv[1] && import.meta.url === pathToFileURL(process.argv[1]).href) await main();
