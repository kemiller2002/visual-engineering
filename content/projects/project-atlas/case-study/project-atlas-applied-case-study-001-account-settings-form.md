---
id: RR-VE-ATL-2026-943E
title: "Project Atlas Applied Case Study 001: Account Settings Form"
project: project-atlas
version: "0.1"
status: "Applied Analysis — Working Draft"
date: "2026-07-18"
principles:
  - "ATLAS-0001: Proximity and Relative Separation"
purpose: |
  Test whether the Atlas proximity model produces useful, falsifiable design
  guidance when applied to an ordinary interface rather than a laboratory
  stimulus.
llm_ingest: true
machine_readable: true
purposes:
  - apply
  - verify
audiences:
  - practitioner
  - researcher
---

# Project Atlas Applied Case Study 001

## Account Settings Form

## Objective

Apply **ATLAS-0001: Proximity and Relative Separation** to a common account
settings form.

The goal is not to prove that the design is good or bad. The goal is to test
whether Atlas can:

1. identify intended groups;
2. identify competing groupings;
3. explain why misgrouping may occur;
4. generate design changes from the mechanism;
5. state what evidence would confirm or reject the diagnosis.

# Baseline Layout

Assume a settings page contains:

```text
Account Settings

First name
[ Kevin                    ]

Last name
[ Miller                   ]

Email address
[ kevin@example.com        ]

Change password

Current password
[                          ]

New password
[                          ]

Confirm new password
[                          ]

[ Cancel ]        [ Save changes ]
```

Assume the current spacing is:

| Relationship | Gap |
|---|---:|
| Label to its input | 8 px |
| Input to next label | 16 px |
| Field to field inside a section | 16 px |
| Last profile field to "Change password" heading | 20 px |
| Password field to password field | 16 px |
| Last password field to action buttons | 20 px |
| Cancel to Save changes | 12 px |

At first glance, the page looks neat and consistent.

That consistency is also the problem.

# Intended Groups

## Group G1 — Profile information

- First name label and input
- Last name label and input
- Email address label and input

## Group G2 — Password information

- Change password heading
- Current password label and input
- New password label and input
- Confirm new password label and input

## Group G3 — Page actions

- Cancel
- Save changes

## Nested groups

Each label and input form a local pair.

Each set of fields forms a larger semantic section.

The page therefore contains at least three levels of grouping:

```text
label + input
    ↓
field collection
    ↓
page section
```

# Competing Groupings

## CG1 — Email grouped with "Change password"

The 20 px gap below the email field is only slightly larger than the 16 px
field-to-field rhythm.

The page may therefore be read as one continuous column rather than two
sections.

## CG2 — Confirm password grouped with buttons

The 20 px gap before the buttons is the same as the gap used to introduce the
password section.

That implies two different relationships have been assigned the same spatial
strength:

- section transition;
- transition from content to action.

## CG3 — Cancel and Save treated as equivalent actions

A 12 px gap and similar visual treatment may produce a single neutral action
group.

Semantically, however, the actions are not equivalent:

- Save changes advances the task.
- Cancel abandons or reverses it.

## CG4 — Labels visually attached to preceding inputs

Because the next label is 16 px below the preceding input while its own input
is only 8 px below, the intended local pair is probably preserved.

However, if typography, alignment, or field borders are weak, the preceding
field can still compete for ownership of the next label.

# Relative Separation Analysis

## Field-level grouping

```text
label-to-input gap: 8 px
input-to-next-label gap: 16 px
relative separation ratio: 16 / 8 = 2.0
```

This creates evidence for the intended label-input pair.

Atlas does **not** claim that `2.0` is universally sufficient. It only records
that the competing gap is twice the within-pair gap in this composition.

## Section-level grouping

```text
field-to-field gap: 16 px
profile-to-password gap: 20 px
relative separation ratio: 20 / 16 = 1.25
```

The section boundary is only 25% stronger than the internal field rhythm.

That is weak evidence for a new group, especially because both sections use the
same alignment, controls, typography, and background.

## Action separation

```text
password-to-actions gap: 20 px
field-to-field gap: 16 px
relative separation ratio: 20 / 16 = 1.25
```

The transition from content to action is also only weakly differentiated.

# Atlas Diagnosis

## Finding 1 — Uniform rhythm is flattening hierarchy

The layout uses similar gaps for relationships inside a field group, transitions
between major sections, and transitions from content to action.

The visual system is being asked to infer different semantic levels from nearly
the same spatial evidence.

**Confidence: High**

## Finding 2 — The design relies on headings to repair weak geometry

"Change password" must carry most of the burden of creating the second section.

If the heading is missed during scanning, the geometry alone does not strongly
separate the groups.

**Confidence: Moderate to high**

## Finding 3 — The action row is spatially grouped but semantically unresolved

Cancel and Save are near one another, so they form a group.

That grouping is useful because both are page actions. However, the action group
still needs internal hierarchy so users can distinguish the preferred action
from the abandoning action.

**Confidence: High**

## Finding 4 — The page has nested grouping but only one visible spacing scale

The semantic structure has three levels:

1. label-input pair;
2. section;
3. page action boundary.

The spacing system expresses roughly one-and-a-half levels.

**Confidence: High**

# Design Revision A — Stronger Section Separation

Revise spacing to:

| Relationship | Gap |
|---|---:|
| Label to input | 8 px |
| Input to next label | 16 px |
| Field to field | 16 px |
| Profile section to password heading | 32 px |
| Last password field to actions | 32 px |
| Cancel to Save | 12 px |

Resulting ratios:

```text
section gap / field gap = 32 / 16 = 2.0
action boundary / field gap = 32 / 16 = 2.0
```

## Atlas prediction

The password section and action row should be perceived as distinct groups more
reliably than in the baseline.

## Risk

The page may become longer without improving understanding if headings,
typography, and control structure already make the sections obvious.

# Design Revision B — Add Compatible Cues

Keep more moderate spacing but reinforce the intended organization with:

- an explicit heading for profile information;
- a divider or common region;
- stronger heading hierarchy;
- distinct action-area alignment;
- primary treatment for Save changes;
- secondary or text treatment for Cancel.

## Atlas prediction

Compatible cues should increase grouping confidence without requiring spacing
alone to carry the structure.

## Risk

Too many cues can over-segment the page and make a simple form feel heavy.

# Design Revision C — Preserve Density Through Common Region

Instead of adding large vertical gaps, place each section in a subtle region.

## Atlas prediction

Common region can supply section organization while allowing tighter internal
spacing.

## Risk

Cards or panels may add visual noise, unnecessary borders, and false
independence between information that still belongs to one account task.

# Preferred Revision

The strongest initial revision is a restrained combination:

1. Keep label-input spacing at 8 px.
2. Keep field rhythm at 16 px.
3. Increase section transitions to approximately 28–32 px.
4. Add a clear heading for each major section.
5. Separate the action area with approximately 28–32 px.
6. Keep actions grouped, but give Save changes stronger visual hierarchy.
7. Avoid cards unless the page needs stronger separation for additional
   reasons.

This is not a universal spacing prescription.

It is a composition-specific response to weak relative separation.

# Falsifiable Predictions

## PRED-APP-001

Users should identify the two form sections more consistently in Revision A or
B than in the baseline.

## PRED-APP-002

The improvement should be larger during brief scanning than during unlimited
inspection.

## PRED-APP-003

If heading typography is already extremely strong, increasing spacing may
produce little additional benefit.

## PRED-APP-004

Revision C may improve section recognition while reducing perceived continuity
of the overall account-settings task.

## PRED-APP-005

Changing button hierarchy should affect action selection more than increasing
the gap between the buttons.

# What This Application Revealed About Atlas

## 1. Candidate organizations are practical

Listing competing groupings immediately exposed a weakness that a generic
"add more whitespace" review would miss.

## 2. Ratios are descriptive before they are prescriptive

The ratio `1.25` helped explain why the section transition was weak.

It did not prove that every section requires `2.0`.

## 3. Proximity is insufficient by itself

The best revision used spacing with headings and action hierarchy rather than
treating distance as the only control.

## 4. Nested grouping requires nested visual evidence

A composition with three semantic levels cannot be expected to work reliably
when every relationship uses nearly the same spacing strength.

## 5. Atlas can make predictions without pretending they are proven

The model produced clear expectations that can later be checked against
existing studies, usability evidence, or targeted testing.

# New Candidate Principle

## Hierarchical Spacing Differentiation

### Hypothesis

When a composition contains nested semantic groups, the visual system must
receive distinguishable evidence for each grouping level. Reusing nearly equal
spacing across multiple semantic levels weakens hierarchy and increases
competition among organizations.

### Status

**Derived hypothesis from application, not yet an established law**

### Next research need

Search existing research on:

- nested grouping;
- hierarchical clustering in visual perception;
- spacing scales in forms and documents;
- interaction between heading strength and section spacing;
- common region versus proximity in interface-like layouts.

# Revision History

| Version | Date | Summary |
|---|---|---|
| 0.1 | 2026-07-18 | First applied Atlas case study. Used ATLAS-0001 to diagnose a settings form, compare three revisions, and derive a new candidate principle concerning hierarchical spacing differentiation. |
