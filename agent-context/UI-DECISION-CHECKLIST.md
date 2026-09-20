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

## Required verification

- Keyboard-only navigation
- Visible and unobscured focus
- 200% text scaling and browser zoom
- Narrow viewport and content reflow
- Reduced motion
- Forced colors or high contrast
- Color-independent state recognition
- Long, missing, and extreme content
- Screen-reader semantics for critical workflows
- First-glance hierarchy inspection
- Deliberate verification of consequential information
- Exercise at least one consequential non-happy semantic state and compare the rendered actions/status with the authoritative product state
- Where stale state can change meaning or legal action, verify that old presentation evidence is not treated as proof of the current state

## Agent handoff

Report:

- Visual Engineering context version and source commit
- Principles applied
- Verification performed
- Material deviations and rationale
- Unresolved evidence or product questions
- Material semantic-projection assumptions, including the authoritative state source when it is not obvious
