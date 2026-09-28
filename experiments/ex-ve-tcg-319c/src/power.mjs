// Pilot variance and sample-size planning for EX-VE-TCG-2026-319C.
//
// The design is within-subject, so each participant contributes one paired
// difference per contrast (mean primary measure under condition A minus
// under B). Planning uses a two-sided paired t test on those differences, by
// deterministic simulation. This is a planning approximation; the
// confirmatory analysis remains the preregistered mixed-effects models.
import { derive, normal, uniforms } from "./random.mjs";

const mean = (values) => values.reduce((a, b) => a + b, 0) / values.length;
const sd = (values) => (values.length < 2 ? NaN : Math.sqrt(values.reduce((a, v) => a + (v - mean(values)) ** 2, 0) / (values.length - 1)));

// ---- Student t distribution ----------------------------------------------------

const logGamma = (x) => {
  const c = [76.18009172947146, -86.50532032941677, 24.01409824083091, -1.231739572450155, 0.1208650973866179e-2, -0.5395239384953e-5];
  const tmp = x + 5.5 - (x + 0.5) * Math.log(x + 5.5);
  const series = c.reduce((acc, coefficient, j) => acc + coefficient / (x + 1 + j), 1.000000000190015);
  return -tmp + Math.log((2.5066282746310005 * series) / x);
};

// Continued fraction for the regularized incomplete beta (Lentz's method).
const betaFraction = (a, b, x) => {
  const tiny = 1e-30;
  const clamp = (v) => (Math.abs(v) < tiny ? tiny : v);
  const iterate = (m, c, d, h) => {
    if (m > 300) return h;
    const m2 = 2 * m;
    const aa1 = (m * (b - m) * x) / ((a + m2 - 1) * (a + m2));
    const d1 = 1 / clamp(1 + aa1 * d);
    const c1 = clamp(1 + aa1 / c);
    const h1 = h * d1 * c1;
    const aa2 = (-(a + m) * (a + b + m) * x) / ((a + m2) * (a + m2 + 1));
    const d2 = 1 / clamp(1 + aa2 * d1);
    const c2 = clamp(1 + aa2 / c1);
    const delta = d2 * c2;
    return Math.abs(delta - 1) < 1e-12 ? h1 * delta : iterate(m + 1, c2, d2, h1 * delta);
  };
  const d0 = 1 / clamp(1 - ((a + b) * x) / (a + 1));
  return iterate(1, 1, d0, d0);
};

export const incompleteBeta = (a, b, x) => {
  if (x <= 0) return 0;
  if (x >= 1) return 1;
  const front = Math.exp(logGamma(a + b) - logGamma(a) - logGamma(b) + a * Math.log(x) + b * Math.log(1 - x));
  return x < (a + 1) / (a + b + 2) ? (front * betaFraction(a, b, x)) / a : 1 - (front * betaFraction(b, a, 1 - x)) / b;
};

export const tCdf = (t, df) => {
  const tail = 0.5 * incompleteBeta(df / 2, 0.5, df / (df + t * t));
  return t >= 0 ? 1 - tail : tail;
};

export const tQuantile = (p, df) => {
  const search = (lo, hi, steps) => {
    const mid = (lo + hi) / 2;
    return steps === 0 ? mid : tCdf(mid, df) < p ? search(mid, hi, steps - 1) : search(lo, mid, steps - 1);
  };
  return search(-1000, 1000, 200);
};

// ---- Pilot summaries -------------------------------------------------------------

const byKey = (rows, key) => rows.reduce((groups, row) => ({ ...groups, [key(row)]: [...(groups[key(row)] ?? []), row] }), {});

// One paired difference per participant: mean(measure | a) - mean(measure | b).
export const pairedDifferences = (scored, { study, measure, a, b, screen }) =>
  Object.values(byKey(scored.filter((row) => row.study === study && (screen === undefined || row.screen === screen)), (row) => row.participant))
    .map((rows) => {
      const under = (condition) => rows.filter((row) => row.condition === condition).map((row) => Number(row[measure]));
      const [ua, ub] = [under(a), under(b)];
      return ua.length && ub.length ? { participant: rows[0].participant, stratum: rows[0].stratum, difference: mean(ua) - mean(ub) } : null;
    })
    .filter(Boolean);

export const summarizeDifferences = (differences) =>
  Object.fromEntries(
    Object.entries(byKey(differences, (row) => row.stratum)).map(([stratum, rows]) => {
      const values = rows.map((row) => row.difference);
      return [stratum, { n: values.length, mean: mean(values), sd: sd(values) }];
    })
  );

// ---- Power and sample size ------------------------------------------------------

export const pairedPower = ({ n, delta, sd: sigma, alpha, simulations, seed }) => {
  const critical = tQuantile(1 - alpha / 2, n - 1);
  const [us] = uniforms(derive(seed, `power:${n}:${delta}:${sigma}`), simulations * n * 2);
  const rejections = Array.from({ length: simulations }, (_, s) => {
    const draws = Array.from({ length: n }, (_, i) => delta + sigma * normal(us[(s * n + i) * 2], us[(s * n + i) * 2 + 1]));
    const t = mean(draws) / (sd(draws) / Math.sqrt(n));
    return Math.abs(t) > critical ? 1 : 0;
  });
  return rejections.reduce((a, b) => a + b, 0) / simulations;
};

// Smallest n (a multiple of the counterbalancing cycle) reaching the target
// power, or null when even `max` does not.
export const sampleSize = ({ delta, sd: sigma, alpha, targetPower, simulations, seed, multiple = 1, max = 400 }) => {
  if (!(sigma > 0) || !(Math.abs(delta) > 0)) return null;
  const candidates = Array.from({ length: Math.floor(max / multiple) }, (_, k) => (k + 1) * multiple).filter((n) => n >= 3);
  const reaches = (n) => pairedPower({ n, delta, sd: sigma, alpha, simulations, seed }) >= targetPower;
  const search = (lo, hi) => {
    if (lo > hi) return null;
    const mid = Math.floor((lo + hi) / 2);
    return reaches(candidates[mid]) ? (mid === lo || !reaches(candidates[mid - 1]) ? candidates[mid] : search(lo, mid - 1)) : search(mid + 1, hi);
  };
  return reaches(candidates.at(-1)) ? search(0, candidates.length - 1) : null;
};

export const sampleSizeTable = ({ summary, deltas, power, seed, multiple }) =>
  Object.entries(summary).map(([stratum, { n, sd: sigma }]) => ({
    stratum,
    pilotN: n,
    pilotSd: sigma,
    sizes: deltas.map((delta) => ({ delta, n: sampleSize({ delta, sd: sigma, alpha: power.alpha, targetPower: power.targetPower, simulations: power.simulations, seed, multiple }) }))
  }));
