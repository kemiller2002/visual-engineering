// Motion evidence for the Application Polish gate
// (framework/standards/MOTION-AND-INTERACTION.md; VE-MOT-001).
//
// Framework-neutral: evidence names the motion model and the observable
// invariants that were exercised. It never requires a particular CSS
// implementation or design system.
//
// Every function here is pure.

export const MOTION_MODELS = Object.freeze(["inertial", "gravity", "cadence", "direct", "perceptual"]);

// Motion may communicate these things (continuity, confirmation, ...).
export const MOTION_PURPOSES = Object.freeze([
  "continuity",
  "confirmation",
  "spatial-relationship",
  "hierarchy",
  "attention",
  "transient-surface",
  "progress",
  "activity",
  "direct-manipulation"
]);

// Motion must never be the hidden carrier of domain meaning (core invariant 7).
export const FORBIDDEN_ENCODINGS = Object.freeze([
  "severity",
  "priority",
  "importance",
  "confidence",
  "permission",
  "risk",
  "legality",
  "correctness",
  "score",
  "quality",
  "preference-strength",
  "improvement",
  "decline",
  "estimated-completion"
]);

export const MOTION_ROLES = Object.freeze([
  "press",
  "selection-indicator",
  "transient-surface",
  "loading",
  "progress",
  "drag",
  "drop",
  "resize",
  "snap",
  "scroll-linked",
  "view-transition",
  "intrinsic-size",
  "value-change",
  "theme-change"
]);

export const MOTION_CHECKS = Object.freeze({
  semanticAuthority: "Semantic state changes immediately; animation represents state and never establishes or delays it.",
  interruption: "Interrupted motion converges on the current state.",
  rapidRepeat: "Rapid repeated input retargets instead of queueing stale motion.",
  reducedMotion: "The model-specific reduced-motion substitution is applied and the final state stays clear.",
  focus: "Focus is retained or restored correctly during and after the transition.",
  directManipulation: "The visual tracks the authoritative input without decorative lag or smoothing.",
  progressBounds: "Determinate progress never visually exceeds the authoritative value and does not overshoot.",
  cadenceStops: "Repeated activity motion stops when the represented activity stops.",
  boundaries: "Settling, snapping, drop and resize motion stay within legal bounds; rejected results return truthfully.",
  progressiveFallback: "Behavior and layout are correct when the progressive motion feature is unsupported or disabled.",
  concurrentComposition: "Concurrent effects compose without one clobbering another's property.",
  semanticIndependence: "Motion is not the sole or hidden carrier of consequential meaning."
});

const CHECK_STATUS = new Set(["passed", "failed", "not-applicable", "untested", "unknown"]);

const unique = (values) => [...new Set(values)];

// Checks an entry must report, derived from its model and declared roles.
export const requiredChecks = (entry) => {
  const roles = new Set(entry?.roles ?? []);
  const model = entry?.model;
  return unique([
    "semanticAuthority",
    "reducedMotion",
    "semanticIndependence",
    ...(model === "inertial" || model === "gravity" ? ["interruption"] : []),
    ...(model === "inertial" || roles.has("press") || roles.has("selection-indicator") ? ["rapidRepeat"] : []),
    ...(model === "cadence" || roles.has("loading") ? ["cadenceStops"] : []),
    ...(model === "direct" || entry?.phase === "direct" ? ["directManipulation"] : []),
    ...(roles.has("progress") ? ["progressBounds"] : []),
    ...(["drag", "drop", "resize", "snap"].some((role) => roles.has(role)) ? ["boundaries"] : []),
    ...(roles.has("transient-surface") || roles.has("view-transition") ? ["focus"] : []),
    ...(entry?.progressiveEnhancement ? ["progressiveFallback"] : []),
    ...((entry?.concurrentWith ?? []).length > 0 ? ["concurrentComposition"] : [])
  ]);
};

const coverageErrors = (path, check) => {
  if (!check || typeof check !== "object") return [`${path} must be an object`];
  if (!CHECK_STATUS.has(check.status)) return [`${path} status must be passed, failed, not-applicable, untested, or unknown`];
  return [
    ...(check.status === "passed" && (!Array.isArray(check.evidence) || check.evidence.length === 0) ? [`${path} passed without evidence`] : []),
    ...(check.status === "not-applicable" && !check.rationale ? [`${path} needs not-applicable rationale`] : [])
  ];
};

// Structural and invariant errors of one motion entry.
const entryErrors = (entry, index) => {
  const at = `motion/${entry?.id ?? index}`;
  if (!entry || typeof entry !== "object") return [`${at} must be an object`];
  const roles = entry.roles ?? [];
  const conveys = entry.conveys ?? [];
  const checks = entry.checks ?? {};
  return [
    ...(!entry.id ? [`${at} needs an id`] : []),
    ...(![...MOTION_MODELS, "unknown"].includes(entry.model) ? [`${at} model must be one of ${MOTION_MODELS.join(", ")} or unknown`] : []),
    ...(!entry.phenomenon ? [`${at} must state the phenomenon the motion represents`] : []),
    ...(!entry.authority?.source ? [`${at} must name the authoritative state or input`] : []),
    ...(!["immediate", "delayed", "unknown"].includes(entry.authority?.semanticUpdate) ? [`${at} authority.semanticUpdate must be immediate, delayed, or unknown`] : []),
    ...roles.filter((role) => !MOTION_ROLES.includes(role)).map((role) => `${at} unknown role ${role}`),
    ...conveys.filter((purpose) => !MOTION_PURPOSES.includes(purpose) && !FORBIDDEN_ENCODINGS.includes(purpose)).map((purpose) => `${at} unknown purpose ${purpose}`),
    ...(entry.phase !== undefined && !["direct", "settle", "none"].includes(entry.phase) ? [`${at} phase must be direct, settle, or none`] : []),
    ...(typeof entry.reducedMotion?.substitution !== "string" || !entry.reducedMotion.substitution ? [`${at} must describe its reduced-motion substitution`] : []),
    ...Object.keys(checks).filter((name) => !Object.hasOwn(MOTION_CHECKS, name)).map((name) => `${at} unknown check ${name}`),
    ...Object.entries(checks).filter(([name]) => Object.hasOwn(MOTION_CHECKS, name)).flatMap(([name, check]) => coverageErrors(`${at}/${name}`, check))
  ];
};

// Invariant violations: declared facts that the standard forbids outright.
const entryViolations = (entry) => {
  const at = `motion/${entry.id}`;
  const roles = new Set(entry.roles ?? []);
  return [
    ...(entry.authority?.semanticUpdate === "delayed" ? [`${at}: semantic state waits for animation (core invariant 1)`] : []),
    ...(entry.phase === "direct" && entry.model !== "direct" ? [`${at}: the direct-manipulation phase must use the direct model; ${entry.model} adds lag (core invariant 2)`] : []),
    ...(roles.has("progress") && entry.model !== "direct" ? [`${at}: determinate progress must be a direct projection of the authoritative value, not ${entry.model} (core invariant 3)`] : []),
    ...(roles.has("loading") && ["inertial", "gravity"].includes(entry.model) ? [`${at}: repeated activity must use cadence, not ${entry.model}`] : []),
    ...(entry.model === "perceptual" && entry.reducedMotion?.spatial === true ? [`${at}: perceptual interpolation cannot keep spatial motion under reduced motion`] : []),
    ...(entry.model !== "direct" && entry.reducedMotion?.spatial === true ? [`${at}: spatial motion survives reduced motion (core invariant 6)`] : []),
    ...(entry.conveys ?? []).filter((purpose) => FORBIDDEN_ENCODINGS.includes(purpose)).map((purpose) => `${at}: motion must not encode ${purpose} (core invariant 7)`)
  ];
};

const checkStatus = (entry, name) => entry.checks?.[name]?.status ?? "missing";

// Evaluate the motion section. Returns counts, missing checks, errors and a
// motion-only disposition that the polish gate folds into its own.
export const evaluateMotionEvidence = (motion, { required = false } = {}) => {
  if (motion === undefined || motion === null) {
    return {
      present: false,
      disposition: required ? "incomplete" : "not-required",
      entries: 0,
      counts: { passed: 0, failed: 0, "not-applicable": 0, unknown: 0 },
      missing: required ? ["motion evidence"] : [],
      unknownChecks: [],
      unknownModels: [],
      violations: [],
      errors: []
    };
  }
  const entries = Array.isArray(motion.entries) ? motion.entries : [];
  const structural = [
    ...(typeof motion !== "object" || Array.isArray(motion) ? ["motion must be an object"] : []),
    ...(motion.entries !== undefined && !Array.isArray(motion.entries) ? ["motion.entries must be an array"] : []),
    ...(entries.length === 0 && !motion.notApplicable?.rationale ? ["motion needs entries or a notApplicable rationale"] : []),
    ...(entries.length > 0 && motion.notApplicable ? ["motion cannot be both not-applicable and have entries"] : []),
    ...entries.flatMap(entryErrors),
    ...unique(entries.map((entry) => entry?.id)).length !== entries.length ? ["motion entry ids must be unique"] : []
  ];
  const valid = entries.filter((entry) => entry && typeof entry === "object" && entry.id);
  const statuses = valid.flatMap((entry) => requiredChecks(entry).map((name) => ({ entry: entry.id, name, status: checkStatus(entry, name) })));
  const optional = valid.flatMap((entry) =>
    Object.entries(entry.checks ?? {})
      .filter(([name]) => !requiredChecks(entry).includes(name) && Object.hasOwn(MOTION_CHECKS, name))
      .map(([name, check]) => ({ entry: entry.id, name, status: check?.status }))
  );
  const all = [...statuses, ...optional];
  const counts = {
    passed: all.filter((item) => item.status === "passed").length,
    failed: all.filter((item) => item.status === "failed").length,
    "not-applicable": all.filter((item) => item.status === "not-applicable").length,
    unknown: all.filter((item) => ["untested", "unknown", "missing"].includes(item.status)).length
  };
  const missing = statuses.filter((item) => item.status === "missing").map((item) => `${item.entry}/${item.name}`);
  const unknownChecks = all.filter((item) => ["untested", "unknown"].includes(item.status)).map((item) => `${item.entry}/${item.name}`);
  const unknownModels = valid.filter((entry) => entry.model === "unknown" || entry.authority?.semanticUpdate === "unknown").map((entry) => entry.id);
  const violations = valid.flatMap(entryViolations);
  const disposition = structural.length || violations.length || counts.failed
    ? "fail"
    : counts.unknown || unknownModels.length
      ? "incomplete"
      : "pass";
  return { present: true, disposition, entries: valid.length, counts, missing, unknownChecks, unknownModels, violations, errors: structural };
};
