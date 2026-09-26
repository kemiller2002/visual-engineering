---
project: visual-engineering
purposes:
  - apply
  - reference
audiences:
  - practitioner
  - contributor
---

# Visual Engineering UI Decision Checklist

## Before implementation

- What is the primary user task?
- What must be recognized immediately?
- What requires deliberate verification?
- What is the intended reading and action order?
- Which relationships must remain visible?
- What are the consequences of misunderstanding or error?
- Which existing product and design-system constraints apply?
- What is authoritative for the state, uncertainty, completion, unresolved work, and legal actions this view presents?
- Which state distinctions change what the user may safely conclude or do?
- Which semantic scopes must remain distinct rather than being combined into one status?
- Which visual channels carry consequential meaning, and what happens if hue, iconography, border, or another channel is unavailable?
- Is a proposed accessibility preset based on measured need, explicit user choice, or an unsupported assumption about a diagnosis?
- Does this task contain strong linguistic context, or is it low-context content such as an identifier, code, unfamiliar name, or critical value?

## During implementation

- Does visual order agree with semantic, DOM, reading, and focus order?
- Is emphasis proportional to importance?
- Are related elements closer or more strongly grouped than unrelated elements?
- Can users distinguish status without color?
- Are labels meaningful without implementation knowledge?
- Does density support the actual scanning or comparison task?
- Are components based on durable semantics or bounded behavior?
- Is native HTML being replaced without a demonstrated benefit?
- Does responsive behavior preserve meaning and task priority?
- Are loading, empty, error, success, and recovery states designed?
- Has an unknown, partial, stale, or unverifiable state been strengthened into a definite success, failure, empty, or complete state?
- Where authoritative capabilities or legal actions exist, do controls derive from them rather than recreate legality locally?
- Do summaries preserve the scope and uncertainty of the facts they aggregate?
- Is decision-relevant unresolved work visible enough that users do not have to infer it from missing controls or generic errors?
- Does consequential state remain identifiable without hue alone?
- If CVD simulation is used, is it treated as screening rather than proof?
- If presentation adapts to a user profile, are semantic meaning, legal actions, reading order, and programmatic state unchanged?
- Are user preference and measured performance being kept distinct when either is used to justify a design decision?

## Required verification

- Keyboard-only navigation
- Visible and unobscured focus
- 200% text scaling and browser zoom
- Narrow viewport and content reflow
- Reduced motion
- Forced colors or high contrast
- Color-independent state recognition
- Grayscale plus protan, deutan, and tritan simulation as screening conditions where color carries categorization
- Text-spacing overrides where reading content is material
- Semantic channel dropout for consequential states: remove at least one nonessential visual cue and verify meaning survives
- Long, missing, and extreme content
- Screen-reader semantics for critical workflows
- First-glance hierarchy inspection
- Deliberate verification of consequential information
- Exercise at least one consequential non-happy semantic state and compare the rendered actions/status with the authoritative product state
- Where stale state can change meaning or legal action, verify that old presentation evidence is not treated as proof of the current state

## Application polish gate

- Have applicable polish dimensions been identified?
- Have reachable non-happy states been exercised rather than inferred?
- Have high-risk state and environment seams been tested?
- Have pathological content and boundary fixtures been used?
- Does persistence presentation tell the truth about local, pending, persisted, failed and conflicting state?
- Are untested environments and states recorded as unknown rather than passed?
- Is there evidence behind a zero-finding review?
- Have recurring defect classes been pushed upstream into shared components, rules or fixtures where appropriate?

## Application polish gate

- Have applicable polish dimensions been identified?
- Have reachable non-happy states been exercised rather than inferred?
- Have high-risk state and environment seams been tested?
- Have pathological content and boundary fixtures been used?
- Does persistence presentation tell the truth about local, pending, persisted, failed and conflicting state?
- Are untested environments and states recorded as unknown rather than passed?
- Is there evidence behind a zero-finding review?
- Have recurring defect classes been pushed upstream into shared components, rules or fixtures where appropriate?

## Agent handoff

Report:

- Visual Engineering context version and source commit
- Principles applied
- Verification performed
- Material deviations and rationale
- Unresolved evidence or product questions
- Material semantic-projection assumptions, including the authoritative state source when it is not obvious
