// EX-VE-TCG-2026-319C session harness. Test code outside Forma: it presents
// each planned trial, logs keydown, focus, input, scroll, submit, and answer
// events with timestamps, and hands the researcher one JSON file of raw trial
// records. Effects are confined to this module's boundary; everything it
// records is derived from the plan and the browser's events.

const plan = await (await fetch("./plan.json")).json();

const $ = (selector) => document.querySelector(selector);
const stage = $("#stage");
const card = $("#card");
const answers = $("#answers");
const begin = $("#begin");
const setup = $("#setup");
const done = $("#done");

const now = () => Math.round(performance.now() * 10) / 10;

const nameOf = (element) =>
  element === window || element === document ? "window"
    : element.classList?.contains("ef-character-grid__viewport") ? "viewport"
      : element.name || element.dataset?.efAction || element.id || element.tagName?.toLowerCase() || "unknown";

const environment = (extra) => ({
  userAgent: navigator.userAgent,
  viewport: { width: window.innerWidth, height: window.innerHeight },
  devicePixelRatio: window.devicePixelRatio,
  prefersReducedMotion: matchMedia("(prefers-reduced-motion: reduce)").matches,
  forcedColors: matchMedia("(forced-colors: active)").matches,
  ...extra
});

// ---- rendering (pure string builders) --------------------------------------

const escape = (text) => String(text).replaceAll("&", "&amp;").replaceAll("<", "&lt;").replaceAll(">", "&gt;").replaceAll('"', "&quot;");

const cardHtml = (trial, position, total) => {
  const header = `<p class="ex-progress">Trial ${position + 1} of ${total}</p><p>${escape(trial.instruction.text)}</p>`;
  return trial.instruction.kind === "record-card"
    ? `${header}<table class="ex-record"><caption>Record to enter</caption><tbody>${trial.instruction.rows
        .map((row) => `<tr>${row.map((cell) => `<th scope="row">${escape(cell.label)}</th><td>${escape(cell.value)}</td>`).join("")}</tr>`)
        .join("")}</tbody></table>`
    : header;
};

const answersHtml = (trial) =>
  trial.instruction.kind === "question"
    ? `${trial.instruction.answers
        .map((answer) => `<p><label for="answer-${answer.key}">${escape(answer.label)}</label> <input id="answer-${answer.key}" name="answer-${answer.key}" autocomplete="off"></p>`)
        .join("")}<p><button type="submit">Submit answer</button></p>`
    : "";

// ---- logging -------------------------------------------------------------------

const createLog = () => {
  const events = [];
  const record = (event) => events.push({ t: now(), ...event });
  const inScope = (element) => stage.contains(element) || answers.contains(element);
  const listeners = {
    focusin: (event) => record({ type: "focus", target: nameOf(event.target), inStimulus: stage.contains(event.target) }),
    keydown: (event) => inScope(event.target) && record({ type: "keydown", key: event.key, shift: event.shiftKey, target: nameOf(event.target) }),
    input: (event) => inScope(event.target) && record({ type: "input", target: nameOf(event.target), value: event.target.value, inStimulus: stage.contains(event.target) }),
    scroll: (event) => {
      const element = event.target === document ? document.scrollingElement : event.target;
      if (element === document.scrollingElement || stage.contains(element)) {
        record({ type: "scroll", target: element === document.scrollingElement ? "window" : nameOf(element), left: element.scrollLeft, top: element.scrollTop });
      }
    }
  };
  Object.entries(listeners).forEach(([type, listener]) => document.addEventListener(type, listener, true));
  return {
    record,
    stop: () => {
      Object.entries(listeners).forEach(([type, listener]) => document.removeEventListener(type, listener, true));
      return [...events];
    }
  };
};

// ---- one trial -----------------------------------------------------------------

const runTrial = (trial, position, session) =>
  new Promise((resolve) => {
    card.innerHTML = cardHtml(trial, position, plan.trials.length);
    answers.innerHTML = "";
    stage.innerHTML = "";
    begin.hidden = false;
    begin.focus();
    begin.onclick = () => {
      begin.hidden = true;
      const log = createLog();
      const startedAt = new Date().toISOString();
      stage.innerHTML = trial.stimulus;
      answers.innerHTML = answersHtml(trial);
      log.record({ type: "shown", target: trial.screen });
      const finish = (event, detail) => {
        event.preventDefault();
        log.record(detail);
        const events = log.stop();
        resolve({
          experiment: plan.experiment,
          phase: plan.phase,
          participant: plan.participant,
          stratum: plan.stratum,
          study: trial.study,
          condition: trial.condition,
          screen: trial.screen,
          item: trial.item,
          trialIndex: trial.trialIndex,
          expected: trial.expected,
          focusOrder: trial.focusOrder,
          startedAt,
          environment: environment({
            ...session,
            viewportMismatch: trial.study === 2 && Math.abs(window.innerWidth - plan.viewport.study2.width) > 2
          }),
          events
        });
      };
      if (trial.study === 1) {
        stage.querySelector("form").addEventListener("submit", (event) =>
          finish(event, { type: "submit", target: event.submitter ? nameOf(event.submitter) : "implicit" }), { once: true });
        // As on a terminal, the cursor starts in the first unprotected field.
        stage.querySelector(`[name="${trial.focusOrder[0]}"]`).focus();
      } else {
        stage.querySelector("form").addEventListener("submit", (event) => event.preventDefault());
        answers.addEventListener("submit", (event) => {
          const value = Object.fromEntries(trial.instruction.answers.map((answer) => [answer.key, answers.querySelector(`[name="answer-${answer.key}"]`).value]));
          finish(event, { type: "answer", target: "answers", value });
        }, { once: true });
        stage.querySelector(".ef-character-grid__viewport").focus();
      }
    };
  });

// ---- viewport gate ---------------------------------------------------------------

// Study 2 is specified at a 320 CSS px viewport. Before its first trial the
// researcher resizes (device emulation or window size); the gate opens only
// when the measured width is within 2px.
const viewportGate = (width) =>
  new Promise((resolve) => {
    const gate = $("#gate");
    const measured = $("#gate-width");
    const proceed = $("#gate-continue");
    const update = () => {
      measured.textContent = String(window.innerWidth);
      proceed.disabled = Math.abs(window.innerWidth - width) > 2;
    };
    $("#gate-target").textContent = String(width);
    gate.hidden = false;
    update();
    window.addEventListener("resize", update);
    proceed.onclick = () => {
      window.removeEventListener("resize", update);
      gate.hidden = true;
      resolve();
    };
  });

// ---- session ---------------------------------------------------------------------

const download = (records) => {
  const blob = new Blob([JSON.stringify({ plan: { experiment: plan.experiment, participant: plan.participant, stratum: plan.stratum }, records }, null, 2)], { type: "application/json" });
  const link = Object.assign(document.createElement("a"), { href: URL.createObjectURL(blob), download: `${plan.participant}-trials.json` });
  link.click();
};

const records = [];
// Read-only hook for the automated mechanics check.
window.__ex319c = { plan: () => plan, records: () => [...records] };

$("#participant").textContent = plan.participant;

setup.addEventListener("submit", async (event) => {
  event.preventDefault();
  const session = { assistiveTechnology: $("#at").value, researcherNotes: $("#notes").value };
  setup.hidden = true;
  $("#session").hidden = false;
  const results = await plan.trials.reduce(
    (chain, trial, position) => chain.then(async (acc) => {
      if (trial.study === 2 && plan.trials[position - 1]?.study !== 2) await viewportGate(plan.viewport.study2.width);
      return [...acc, await runTrial(trial, position, session)];
    }).then((acc) => {
      records.splice(0, records.length, ...acc);
      return acc;
    }),
    Promise.resolve([])
  );
  $("#session").hidden = true;
  done.hidden = false;
  $("#download").onclick = () => download(results);
});
