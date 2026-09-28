// Deterministic, pure pseudo-random numbers (mulberry32). A generator is a
// state value; every function returns new values and never mutates, so a
// seed fully determines schedules, datasets, and simulations.

const step = (state) => (state + 0x6d2b79f5) >>> 0;

const output = (state) => {
  const a = Math.imul(state ^ (state >>> 15), state | 1);
  const b = (a + Math.imul(a ^ (a >>> 7), a | 61)) ^ a;
  return ((b ^ (b >>> 14)) >>> 0) / 4294967296;
};

// [uniform in [0, 1), next state]
export const next = (state) => {
  const advanced = step(state);
  return [output(advanced), advanced];
};

// n uniforms from a seed, returned with the final state. The accumulation is
// local and linear (the simulations draw hundreds of thousands of values);
// the function itself is pure.
export const uniforms = (seed, n) => {
  const values = new Array(n);
  const final = values.fill(0).reduce((state, _, index) => {
    const [value, following] = next(state);
    values[index] = value;
    return following;
  }, seed >>> 0);
  return [values, final];
};

// A child seed per label, so independent streams never overlap by index.
export const derive = (seed, label) =>
  [...String(label)].reduce((hash, char) => Math.imul(hash ^ char.charCodeAt(0), 16777619) >>> 0, (seed ^ 2166136261) >>> 0);

export const integer = (u, min, max) => min + Math.floor(u * (max - min + 1));

// Fisher-Yates driven by precomputed uniforms; returns a new array.
export const shuffle = (seed, items) => {
  const [us] = uniforms(seed, items.length);
  return us.reduceRight(
    (acc, u, index) => {
      const swap = Math.floor(u * (index + 1));
      const copy = [...acc];
      [copy[index], copy[swap]] = [copy[swap], copy[index]];
      return copy;
    },
    [...items]
  );
};

// Standard normal via Box-Muller from two uniforms.
export const normal = (u1, u2) => Math.sqrt(-2 * Math.log(1 - u1)) * Math.cos(2 * Math.PI * u2);
