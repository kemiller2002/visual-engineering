---
id: RR-VE-ATL-2026-501A
title: "Project Atlas Rosetta Stone: Cross-System Design Mechanism Map"
project: project-atlas
version: "0.1"
status: "Initial Verified Crosswalk"
date: "2026-07-18"
authors:
  - Kevin Miller
  - ChatGPT
systems:
  - Microsoft Ribbon
  - Microsoft Fluent 2
  - Apple Human Interface Guidelines
  - Material Design 3
  - GOV.UK Design System
  - IBM Carbon Design System
purpose: |
  Map differently named design guidance across major systems to common Atlas
  mechanisms, while distinguishing direct equivalence, partial overlap, and
  superficial similarity.
llm_ingest: true
machine_readable: true
purposes:
  - apply
  - integrate
  - verify
audiences:
  - practitioner
  - researcher
---

# Project Atlas Rosetta Stone

## Cross-System Design Mechanism Map

# Purpose

Major design systems often describe similar compositional problems using
different terminology.

One system may discuss:

- hierarchy;
- another, prominence;
- another, emphasis;
- another, grouping;
- another, progressive disclosure.

The Atlas Rosetta Stone maps these recommendations to shared candidate
mechanisms without assuming that similarly named components are functionally or
perceptually identical.

The goal is not to merge the systems into one universal style guide.

The goal is to determine:

1. where independent systems converge;
2. where they solve the same problem differently;
3. which recommendations reflect platform convention;
4. which recommendations may derive from common perceptual or cognitive
   constraints;
5. where scientific evidence is needed to distinguish principle from practice.

---

# Source Boundary

This first version uses current first-party documentation from:

- Microsoft Fluent 2
- Microsoft Windows and Office Ribbon documentation
- Apple Human Interface Guidelines
- Material Design 3
- GOV.UK Design System
- IBM Carbon Design System

A design-system recommendation establishes that the organization recommends a
practice. It does not establish that the practice is universally optimal.

---

# Equivalence Scale

Every mapping receives an equivalence grade.

| Grade | Meaning |
|---|---|
| **A — Direct mechanism match** | The systems explicitly describe substantially the same design mechanism or goal. |
| **B — Strong functional overlap** | The recommendations solve the same user problem but differ in implementation or scope. |
| **C — Partial analogy** | The concepts share some properties but should not be treated as interchangeable. |
| **D — Surface resemblance only** | The visual pattern looks similar, but the documented purpose or behavior differs. |
| **U — Unresolved** | More documentation or empirical evidence is required. |

---

# Rosetta Stone Summary

| Atlas mechanism | Microsoft | Apple | Material | GOV.UK | Carbon | Equivalence |
|---|---|---|---|---|---|---|
| Relative separation and grouping | Spacing ramp, dividers, Ribbon command groups | Layout spacing, grouped list/table styles | Padding, containment, layout spacing | Spacing scales, fieldsets, task groups | Form groups, section spacing, grid | A |
| Hierarchical spacing differentiation | Spacing and hierarchy; Ribbon tab → group → command | Layout and type hierarchy | Size, spacing, placement, containment | Heading and spacing conventions | Explicitly larger spacing between groups than items | A |
| Semantic label reinforcement | Ribbon group labels, tab labels | Section titles, navigation labels | Labels, headings, supporting text | Legends, labels, headings | Group headings and concise labels | A |
| Salience and action prominence | Control size, Fluent button prominence, elevation | Button roles, placement, platform hierarchy | Emphasis through size, color, placement, containment | Primary button conventions and restrained styling | Primary, secondary, tertiary, ghost hierarchy | A |
| Progressive or contextual disclosure | Contextual Ribbon tabs, overflow, collapsible structures | Disclosure controls and progressive disclosure | Adaptive and conditional surfaces | One-question pages, conditional reveal patterns | Accordions and progressive-disclosure forms | B |
| Recognition versus recall | Ribbon exposes commands | Visible toolbars, familiar platform controls | Visible navigation and actions | Plain, explicit service flows | Visible labels, progressive disclosure with warnings | B |
| Task-oriented organization | Ribbon tabs and groups organized around tasks | Content and controls organized around user activity | Layout directs attention and action | Services broken into focused questions and tasks | Logical form groups and workflow sections | B |
| Common region and containment | Ribbon groups, panels, dividers | Grouped lists, tables, sheets | Containers and layout regions | Fieldsets, component wrappers | Containers, grid, sections, accordions | A |
| Adaptive hierarchy | Ribbon scaling policies, Fluent responsive spacing | Adaptive layouts across Apple platforms | Adaptive design and window-size classes | Responsive spacing scale | Responsive grid and component behavior | B |
| Consistent rhythm and tokenization | Fluent spacing and typography tokens | Platform metrics and standard components | 4dp spacing basis and tokens | 5px-related spacing and type rhythm | 2x grid and spacing tokens | B |
| Structural accessibility | Fluent hierarchy, focus order, accessibility guidance | Semantic and platform accessibility guidance | Information architecture and heading structure | Native HTML, fieldset, legend, label | Form structure, labels, component accessibility | A |
| Controlled visual density | Simplified Ribbon and overflow | Progressive disclosure and platform-specific toolbars | Adaptive layouts, containment, component hierarchy | One question per page and limited options | Accordion and progressive disclosure guidance | B |

---

# Mechanism Crosswalks

# RS-001 — Relative Separation and Grouping

## Atlas definition

Elements are more likely to be perceived as belonging together when their
within-group separation is smaller than their separation from neighboring
groups, particularly when other cues reinforce that organization.

## Microsoft

Fluent states that layout uses spacing and hierarchy to create relationships
between components. Dividers, headers, and spacing are used together to create
groupings and hierarchy.

The Ribbon implements grouping through:

- proximity;
- labeled command groups;
- separators;
- common alignment;
- tab containment.

Primary sources:

- https://fluent2.microsoft.design/layout
- https://fluent2.microsoft.design/components/web/react/core/divider/usage
- https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons

## Apple

Apple's layout guidance addresses spacing, organization, and platform-aware
layout. Its list and table guidance notes that visual details can communicate
grouping and hierarchy.

Primary sources:

- https://developer.apple.com/design/human-interface-guidelines/layout
- https://developer.apple.com/design/human-interface-guidelines/lists-and-tables

## Material

Material defines layout as the visual arrangement that directs attention and
supports action. Padding is measured in 4dp increments, and containment,
placement, size, and spacing are used to establish hierarchy.

Primary sources:

- https://m3.material.io/foundations/layout/understanding-layout
- https://m3.material.io/foundations/layout/understanding-layout/spacing
- https://m3.material.io/foundations/usability

## GOV.UK

GOV.UK provides responsive and static spacing scales and uses semantic grouping
through fieldsets, legends, button groups, and clearly headed task groups.

Primary sources:

- https://design-system.service.gov.uk/styles/spacing/
- https://design-system.service.gov.uk/components/checkboxes/
- https://design-system.service.gov.uk/components/button/
- https://design-system.service.gov.uk/components/task-list/

## Carbon

Carbon's form guidance explicitly states that spacing between groups should be
adjusted relative to spacing between individual items. It recommends larger
separation between sections than between fields.

Primary source:

- https://carbondesignsystem.com/patterns/forms-pattern/

## Equivalence grade

**A — Direct mechanism match**

## Atlas interpretation

This is one of the strongest areas of convergence. The systems independently
use relative spatial separation, often reinforced by labels and containment, to
communicate organization.

## Research question

Do the systems' different numeric spacing scales produce comparable perceived
group strength when normalized by typography, element size, density, and
viewing distance?

---

# RS-002 — Hierarchical Spacing Differentiation

## Atlas definition

Nested semantic levels require distinguishable visual evidence. Spacing between
sections should generally exceed spacing between items within a section.

## Strongest explicit source

Carbon states this relationship directly. It recommends adjusting spacing
between groups in relation to spacing between individual items and gives
examples in which section separation is larger than field separation.

Source:

- https://carbondesignsystem.com/patterns/forms-pattern/

## Microsoft

Fluent describes spacing and hierarchy as relationship-building tools. The
Ribbon visibly encodes several grouping levels:

```text
tab set
    → selected tab
        → command group
            → command family
                → control
```

Sources:

- https://fluent2.microsoft.design/layout
- https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons

## Apple

Apple uses layout, type, list styles, and platform conventions to distinguish
hierarchical levels. The exact spacing-ratio principle is less explicit in the
retrieved public guidance than in Carbon.

## Material

Material recommends building strong hierarchy through size, spacing, placement,
color, and containment.

Source:

- https://m3.material.io/foundations/usability

## GOV.UK

GOV.UK's spacing and typography scales create consistent vertical rhythm.
Question pages concentrate the page around one primary question, reducing
competition among hierarchy levels.

Sources:

- https://design-system.service.gov.uk/styles/spacing/
- https://design-system.service.gov.uk/styles/type-scale/
- https://design-system.service.gov.uk/patterns/question-pages/

## Equivalence grade

**A for mechanism, B for explicitness**

## Atlas result

The candidate principle **Hierarchical Spacing Differentiation** now has direct
normative support from Carbon and strong structural support across the other
systems.

This is not yet scientific validation, but it is meaningful cross-system
convergence.

---

# RS-003 — Semantic Label Reinforcement

## Atlas definition

Geometric grouping becomes more predictable when a concise label names the
shared function or meaning of the group.

## Microsoft

Ribbon groups and tabs are named. Geometry identifies the group boundary while
the label explains the group's intended meaning.

## Apple

Section titles, navigation labels, and standard control labels reinforce
content structure.

## Material

Headings, labels, and supporting text make structure and component purpose
explicit.

## GOV.UK

GOV.UK strongly couples visible and semantic grouping. Checkbox groups use a
`fieldset` and `legend`, typically phrased as the question the group answers.

Source:

- https://design-system.service.gov.uk/components/checkboxes/

## Carbon

Carbon recommends short, precise group headings and states that inputs should be
grouped logically so users understand what is required.

Source:

- https://carbondesignsystem.com/patterns/forms-pattern/

## Equivalence grade

**A — Direct mechanism match**

## Atlas interpretation

Labels do more than improve accessibility or documentation. They constrain the
set of plausible interpretations for a spatial group.

## Candidate principle

**Semantic Constraint Reinforcement**

> A concise, accurate group label reduces ambiguity when it agrees with the
> group's spatial and behavioral organization.

## Failure condition

A vague label such as “More,” “Other,” or “Tools” may create a visible group
without meaningfully constraining expectations.

---

# RS-004 — Salience and Action Prominence

## Atlas definition

Visual weight allocates attention and implies relative importance, urgency, or
task priority.

## Microsoft

Fluent defines button prominence levels and uses size, color, elevation, and
placement to communicate hierarchy. Ribbon controls also vary in size.

Sources:

- https://fluent2.microsoft.design/components/android/core/button/usage
- https://fluent2.microsoft.design/elevation
- https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons

## Apple

Apple uses platform-standard button roles, placement, sizing, and control
treatment. Its guidance also establishes minimum interaction regions.

Sources:

- https://developer.apple.com/design/human-interface-guidelines/buttons
- https://developer.apple.com/design/human-interface-guidelines/toolbars

## Material

Material explicitly recommends strong visual hierarchy using color, size,
spacing, placement, and containment.

Source:

- https://m3.material.io/foundations/usability

## GOV.UK

GOV.UK uses a restrained button vocabulary and advises designers not to assign
new meanings to established styles.

Sources:

- https://design-system.service.gov.uk/components/button/
- https://design-system.service.gov.uk/styles/

## Carbon

Carbon has an explicit action hierarchy:

- primary;
- secondary;
- tertiary;
- ghost;
- danger variants.

It warns against multiple high-emphasis actions and notes that placement itself
can create prominence.

Primary source:

- https://carbondesignsystem.com/components/button/usage/

## Equivalence grade

**A — Direct mechanism match**

## Candidate principle

**Salience Budget**

> A composition has a limited capacity for high-prominence elements. Increasing
> the number of elements claiming primary attention reduces the distinctiveness
> of each claim.

## Evidence opportunity

Carbon's warning against two high-emphasis buttons and Fluent's warning not to
overload layouts with buttons create a testable cross-system prediction.

---

# RS-005 — Progressive and Contextual Disclosure

## Atlas definition

Information or controls are withheld until they become relevant, reducing
simultaneous competition at the cost of discoverability and additional action.

## Microsoft

The Ribbon uses:

- contextual tabs;
- overflow;
- collapsed group states;
- the Simplified Ribbon.

These methods reduce the visible command set based on context or available
space.

## Apple

Apple explicitly recommends disclosure controls to hide details until relevant
and describes progressive disclosure in layout guidance.

Sources:

- https://developer.apple.com/design/human-interface-guidelines/disclosure-controls
- https://developer.apple.com/design/human-interface-guidelines/layout
- https://developer.apple.com/design/human-interface-guidelines/sheets

## Material

Material's adaptive-design guidance addresses interfaces that change in
response to context, user preferences, devices, and available space.

Source:

- https://m3.material.io/foundations/adaptive-design

## GOV.UK

GOV.UK often controls complexity through service structure rather than dense
component-level disclosure. Its one-question-per-page pattern limits the
information and decision set visible at one time.

Source:

- https://design-system.service.gov.uk/patterns/question-pages/

GOV.UK also advises reducing a long list of choices by asking prior questions
where practical.

Source:

- https://design-system.service.gov.uk/components/select/

## Carbon

Carbon explicitly recommends progressive disclosure in forms. Its accordion
guidance also identifies the tradeoff: accordions save space and provide
overview, but hidden content may not be noticed.

Sources:

- https://carbondesignsystem.com/patterns/forms-pattern/
- https://carbondesignsystem.com/components/accordion/usage/

## Equivalence grade

**B — Strong functional overlap**

## Important distinction

These are not identical patterns.

- A Ribbon contextual tab conditionally reveals commands.
- An Apple disclosure control expands local hierarchy.
- A Carbon accordion hides content sections.
- A GOV.UK question flow distributes disclosure across pages.

They share a complexity-management mechanism but differ substantially in
interaction cost, persistence, visibility, and user expectation.

## Candidate law

**Disclosure Conservation**

> Reducing simultaneous visual complexity does not remove complexity. It moves
> some of the cost into discovery, memory, navigation, or interaction.

---

# RS-006 — Recognition Versus Recall

## Atlas definition

Visible, labeled choices support recognition. Hidden paths, remembered command
locations, and concealed options increase recall or search demands.

## Microsoft

The Ribbon was explicitly designed as a more directly exposed command surface
than the prior combination of menus and toolbars.

Sources:

- https://learn.microsoft.com/en-us/office/vba/library-reference/concepts/overview-of-the-office-fluent-ribbon
- https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons

## Apple

Apple relies heavily on familiar platform controls and stable conventions.
Toolbars and visible controls support recognition, while disclosure and menus
limit density.

## Material

Material uses recognizable components and persistent or contextual navigation
patterns, but the balance varies by screen size and platform.

## GOV.UK

GOV.UK favors plain language, explicit questions, and visible choices. Its
select guidance recommends presenting fewer visible options when possible
rather than hiding a poorly structured choice set inside a long selector.

## Carbon

Carbon's accordion guidance explicitly acknowledges that hidden content may not
be noticed. This is a direct recognition-versus-density tradeoff.

## Equivalence grade

**B — Strong functional overlap**

## Candidate principle

**Recognition Exposure Tradeoff**

> Increasing visible choices can improve discoverability until visual
> competition and search cost exceed the recognition benefit.

---

# RS-007 — Task-Oriented Organization

## Atlas definition

Information and controls are organized according to user goals and expected
activities rather than implementation ownership, historical accumulation, or
internal system boundaries.

## Microsoft

Ribbon guidance advises against merely translating existing menus into tabs and
instead emphasizes meaningful organization.

Source:

- https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons

## Apple

Apple's platform guidance generally organizes controls around the content and
activity occurring in the current view.

## Material

Material describes layout as directing attention toward important information
and making action easier.

Source:

- https://m3.material.io/foundations/layout/understanding-layout

## GOV.UK

GOV.UK service patterns are strongly task-oriented. The question-page pattern
focuses each page on a specific question and answer.

## Carbon

Carbon recommends logical form grouping so users understand what is required,
and its progressive form patterns reveal dependent information based on prior
selections.

## Equivalence grade

**B — Strong functional overlap**

## Research problem

“Task-oriented” is often used without a shared unit of analysis.

A task may mean:

- a broad user goal;
- a workflow stage;
- an object operation;
- a command family;
- a page-level decision.

Atlas should record the task granularity used by each system.

---

# RS-008 — Common Region and Containment

## Atlas definition

A visible boundary or shared surface increases the likelihood that contained
elements are perceived as a group.

## Microsoft

Ribbon command groups, panels, dividers, and elevation create regions.

## Apple

Grouped lists, tables, sheets, and platform surfaces use containment to express
structure.

## Material

Containment is explicitly listed as a hierarchy-building tactic.

## GOV.UK

Fieldsets semantically and visually group form controls. Layout wrappers and
component structures create stable regions.

## Carbon

Form sections, accordions, panels, and grid regions provide containment.

## Equivalence grade

**A — Direct mechanism match**

## Atlas warning

Containment and proximity may conflict.

A border can claim that distant elements belong together, while internal
spacing may imply several subgroups. Atlas should analyze both levels rather
than treating the container as decisive.

---

# RS-009 — Adaptive Hierarchy

## Atlas definition

As available space, platform, or user context changes, a composition modifies
presentation while attempting to preserve relationships, priority, and task
continuity.

## Microsoft

Ribbon size definitions and scaling policies control how groups and commands
adapt as width changes. Fluent uses a multi-platform spacing ramp.

Sources:

- https://learn.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-templates
- https://fluent2.microsoft.design/layout

## Apple

Apple's layout guidance is platform-specific and adapts across device classes,
window sizes, and interaction modes.

## Material

Material explicitly defines adaptive design as responding to context such as
the user, settings, device, and available space.

Source:

- https://m3.material.io/foundations/adaptive-design

## GOV.UK

GOV.UK uses responsive and static spacing scales. Responsive spacing changes at
the tablet breakpoint while smaller spacing units remain stable.

Source:

- https://design-system.service.gov.uk/styles/spacing/

## Carbon

Carbon uses responsive grids and breakpoint-aware component layouts.

## Equivalence grade

**B — Strong functional overlap**

## Candidate principle

**Relational Preservation Under Transformation**

> A successful adaptive layout changes geometry while preserving the
> composition's important grouping, hierarchy, and task relationships.

## Failure pattern

Uniformly shrinking every element preserves proportions but may destroy:

- legibility;
- target size;
- group differentiation;
- salience;
- useful visible content.

---

# RS-010 — Consistent Rhythm and Tokenization

## Atlas definition

A restricted set of spacing, type, size, and elevation values creates recurring
relationships that can improve consistency and reduce arbitrary variation.

## Microsoft

Fluent uses global spacing ramps, typography systems, and design tokens.

Sources:

- https://fluent2.microsoft.design/layout
- https://fluent2.microsoft.design/typography
- https://fluent2.microsoft.design/design-tokens

## Apple

Apple provides platform metrics, standard controls, layout conventions, and
typographic systems.

## Material

Material measures padding in 4dp increments and provides a system of layout and
component tokens.

Source:

- https://m3.material.io/foundations/layout/understanding-layout/spacing

## GOV.UK

GOV.UK uses spacing and type scales based around recurring increments. Its type
scale uses line heights in multiples of 5px to create consistent vertical
rhythm.

Sources:

- https://design-system.service.gov.uk/styles/spacing/
- https://design-system.service.gov.uk/styles/type-scale/

## Carbon

Carbon uses a 2x grid and spacing tokens to drive visual rhythm and alignment.

Source:

- https://carbondesignsystem.com/components/tabs/usage/

## Equivalence grade

**B — Strong functional overlap**

## Important distinction

Token convergence is not proof of biological optimality.

The common use of 2-, 4-, 5-, or 8-based scales may reflect:

- display technology;
- implementation convenience;
- divisibility;
- platform inheritance;
- aesthetic convention;
- perceptual benefits;
- combinations of these factors.

Atlas should distinguish **system consistency benefits** from claims that any
specific numeric scale is universally perceptual.

---

# RS-011 — Structural Accessibility

## Atlas definition

Visual structure is reinforced by semantic, navigational, and interaction
structure so that organization survives changes in modality.

## Microsoft

Fluent accessibility guidance recommends logical, predictable organization and
uses typography, color, dividers, and spacing to create groupings and importance
levels.

Source:

- https://fluent2.microsoft.design/accessibility

## Apple

Apple HIG integrates accessibility throughout platform design guidance and
standard controls.

## Material

Material connects heading levels to the layout's information architecture.

Source:

- https://m3.material.io/foundations/designing/structure

## GOV.UK

GOV.UK strongly couples visual grouping with native semantic structure,
including `fieldset`, `legend`, headings, and labels.

Source:

- https://design-system.service.gov.uk/components/checkboxes/

## Carbon

Carbon components and form guidance combine visible labels, headings, sections,
and accessibility behavior.

## Equivalence grade

**A — Direct mechanism match**

## Atlas interpretation

A visual group that is not represented semantically is fragile. It may disappear
for:

- screen-reader users;
- keyboard navigation;
- magnification;
- reflow;
- text-only extraction;
- machine interpretation.

## Candidate principle

**Cross-Modal Structural Redundancy**

> Important relationships should be represented through more than one channel
> so that organization survives perceptual, device, and interaction changes.

---

# RS-012 — Controlled Visual Density

## Atlas definition

The amount of simultaneously visible information must balance scan efficiency,
discoverability, target differentiation, and available space.

## Microsoft

The standard and Simplified Ribbons represent different density allocations.
Overflow and responsive collapse reduce visible controls but increase hidden
structure.

## Apple

Platform toolbars and disclosure patterns balance direct access against spatial
constraints.

## Material

Adaptive layouts, containment, navigation changes, and component hierarchy
control information density across windows and devices.

## GOV.UK

One-question-per-page patterns reduce simultaneous decision density at the cost
of additional pages.

## Carbon

Accordions and progressive disclosure reduce page length and visible content,
while Carbon explicitly warns that users may overlook hidden material.

## Equivalence grade

**B — Strong functional overlap**

## Candidate principle

**Density Redistribution**

> Interfaces do not eliminate information density. They redistribute it across
> space, time, interaction steps, and memory.

---

# Strongest Initial Convergences

The first crosswalk reveals six especially strong areas.

## 1. Grouping is multi-cue

No mature system relies on spacing alone.

Common reinforcement includes:

- headings;
- labels;
- containers;
- dividers;
- alignment;
- typography;
- color;
- interaction behavior.

## 2. Between-group separation exceeds within-group separation

Carbon states this explicitly. The other systems implement it structurally.

This materially strengthens the Atlas candidate principle of Hierarchical
Spacing Differentiation.

## 3. Prominence is treated as scarce

Microsoft, Material, GOV.UK, Apple, and Carbon all restrict or structure
high-emphasis actions.

This supports development of the **Salience Budget** hypothesis.

## 4. Disclosure always introduces a tradeoff

Every system that hides content or controls also creates a discoverability,
navigation, or interaction cost.

This supports **Disclosure Conservation**.

## 5. Adaptation should preserve relationships, not pixels

The systems change layout by breakpoint, platform, state, or context rather than
merely scaling the entire composition uniformly.

This supports **Relational Preservation Under Transformation**.

## 6. Visual hierarchy and semantic hierarchy are linked

The strongest systems encode grouping through visible and machine-readable
structure.

This supports **Cross-Modal Structural Redundancy**.

---

# Important Non-Equivalences

A useful Rosetta Stone must record differences as carefully as similarities.

## Ribbon tabs are not equivalent to all tabs

Ribbon tabs select broad command contexts. Content tabs often switch among
peer content views. Their visual form may resemble each other, but task,
persistence, and information architecture differ.

**Grade: C**

## A GOV.UK question page is not an accordion

Both reduce simultaneous complexity, but one distributes a workflow across
pages while the other expands content locally.

**Grade: C**

## Spacing tokens are not perceptual laws

A 4dp Material increment, a GOV.UK 5px-related rhythm, Fluent tokens, and
Carbon's 2x grid all produce consistency. Their numeric difference means they
cannot be treated as one universal spacing law.

**Grade: B for system function, U for biological basis**

## Large Ribbon icons are not automatically primary actions

Size creates salience, but Ribbon size may encode frequency, discoverability,
available layout, historical importance, or product-specific priority.

**Grade: C until command-priority evidence is collected**

## Contextual display is not always progressive disclosure

A contextual tab is triggered by selection state. An accordion is explicitly
opened. A responsive overflow is triggered by available width. These share
concealment but differ in agency and predictability.

**Grade: C**

---

# Candidate Atlas Principles Produced by the Crosswalk

## ATLAS-CAND-001 — Hierarchical Spacing Differentiation

Nested semantic groups require distinguishable separation at each level.

Current support:

- Direct normative support: Carbon
- Strong structural convergence: Microsoft, Apple, Material, GOV.UK
- Scientific validation: pending

## ATLAS-CAND-002 — Semantic Constraint Reinforcement

A meaningful label reduces ambiguity when it agrees with spatial and behavioral
grouping.

## ATLAS-CAND-003 — Salience Budget

The number of elements that can remain distinctly high priority is limited.

## ATLAS-CAND-004 — Disclosure Conservation

Hiding information reduces visible complexity by transferring cost into
discovery, interaction, memory, or navigation.

## ATLAS-CAND-005 — Relational Preservation Under Transformation

Adaptive layouts should preserve important relationships and priority rather
than literal geometry.

## ATLAS-CAND-006 — Cross-Modal Structural Redundancy

Important structure should be represented through multiple perceptual and
semantic channels.

## ATLAS-CAND-007 — Density Redistribution

Information complexity is distributed across space, time, and interaction
rather than eliminated.

---

# Evidence Confidence

| Finding | Confidence | Reason |
|---|---|---|
| Major systems use spacing to communicate relationships | High | Explicit first-party guidance across systems |
| Larger between-group spacing is recommended | Moderate–High | Direct in Carbon, structurally present elsewhere |
| Multiple cues reinforce hierarchy | High | Explicit and repeated across sources |
| High-prominence actions should be limited | High | Direct guidance from several systems |
| Progressive disclosure trades density for discovery | High | Directly acknowledged by Apple and Carbon; structurally present elsewhere |
| Similar token scales reflect a universal perceptual ratio | Low | No evidence established |
| Task-oriented grouping is always superior | Moderate | Strong normative convergence; task definition and boundary conditions unresolved |
| Adaptive layouts should preserve relational structure | Moderate–High | Strong cross-system convergence; empirical thresholds unresolved |

---

# Research Backlog

## Scientific literature

Collect research on:

1. Gestalt proximity and common region
2. Hierarchical grouping
3. Visual search and salience competition
4. Recognition versus recall
5. Menu breadth versus depth
6. Progressive disclosure
7. Information scent
8. Spatial memory under adaptive relocation
9. Semantic congruence between labels and groups
10. Cross-modal and redundant coding
11. Visual density and crowding
12. Target acquisition and motor control
13. Responsive reflow and comprehension
14. Expert versus novice command search
15. Working memory during multi-step workflows

## Applied examples

Collect matched positive and negative examples for:

- forms;
- command surfaces;
- settings screens;
- dashboards;
- navigation;
- accordions;
- tables;
- mobile adaptive layouts;
- responsive desktop layouts;
- accessibility and reflow.

## Measurements

For each example, record:

- within-group spacing;
- between-group spacing;
- ratio between spacing levels;
- number of hierarchy levels;
- number of cues supporting each group;
- number of high-salience elements;
- visible versus hidden option count;
- interaction steps required to reveal hidden content;
- layout changes across breakpoints;
- semantic structure corresponding to visual structure.

---

# Machine-Readable Crosswalk Schema

```yaml
crosswalk_id:
atlas_mechanism:
atlas_definition:

systems:
  microsoft:
    terminology:
    documented_goal:
    implementation:
    source_urls:
  apple:
    terminology:
    documented_goal:
    implementation:
    source_urls:
  material:
    terminology:
    documented_goal:
    implementation:
    source_urls:
  govuk:
    terminology:
    documented_goal:
    implementation:
    source_urls:
  carbon:
    terminology:
    documented_goal:
    implementation:
    source_urls:

equivalence_grade:
shared_mechanism:
important_differences:
scientific_support:
confidence:
candidate_principles:
open_questions:
applied_examples:
```

---

# Next Collection Pass

The next Rosetta Stone version should add:

1. NASA human-systems integration guidance
2. FAA human-factors guidance
3. W3C Web Content Accessibility Guidelines
4. Nielsen Norman research as a secondary professional source
5. ISO ergonomics standards where accessible
6. Adobe Spectrum
7. Atlassian Design System
8. Salesforce Lightning
9. U.S. Web Design System
10. Shopify Polaris

The next applied gallery pass should then select one example for every
crosswalk row and pair it with a contrasting weak example.

---

# Primary Source Register

## Microsoft

- Fluent Layout  
  https://fluent2.microsoft.design/layout

- Fluent Accessibility  
  https://fluent2.microsoft.design/accessibility

- Fluent Typography  
  https://fluent2.microsoft.design/typography

- Fluent Design Tokens  
  https://fluent2.microsoft.design/design-tokens

- Fluent Elevation  
  https://fluent2.microsoft.design/elevation

- Windows Ribbon UX Guide  
  https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons

- Office Fluent Ribbon Overview  
  https://learn.microsoft.com/en-us/office/vba/library-reference/concepts/overview-of-the-office-fluent-ribbon

- Ribbon Size Definitions and Scaling Policies  
  https://learn.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-templates

## Apple

- Human Interface Guidelines  
  https://developer.apple.com/design/human-interface-guidelines

- Layout  
  https://developer.apple.com/design/human-interface-guidelines/layout

- Lists and Tables  
  https://developer.apple.com/design/human-interface-guidelines/lists-and-tables

- Buttons  
  https://developer.apple.com/design/human-interface-guidelines/buttons

- Toolbars  
  https://developer.apple.com/design/human-interface-guidelines/toolbars

- Disclosure Controls  
  https://developer.apple.com/design/human-interface-guidelines/disclosure-controls

- Sheets  
  https://developer.apple.com/design/human-interface-guidelines/sheets

## Material Design 3

- Understanding Layout  
  https://m3.material.io/foundations/layout/understanding-layout

- Layout Spacing  
  https://m3.material.io/foundations/layout/understanding-layout/spacing

- Usability  
  https://m3.material.io/foundations/usability

- Adaptive Design  
  https://m3.material.io/foundations/adaptive-design

- Accessibility Structure  
  https://m3.material.io/foundations/designing/structure

## GOV.UK

- Spacing  
  https://design-system.service.gov.uk/styles/spacing/

- Type Scale  
  https://design-system.service.gov.uk/styles/type-scale/

- Question Pages  
  https://design-system.service.gov.uk/patterns/question-pages/

- Checkboxes  
  https://design-system.service.gov.uk/components/checkboxes/

- Buttons  
  https://design-system.service.gov.uk/components/button/

- Select  
  https://design-system.service.gov.uk/components/select/

- Task List  
  https://design-system.service.gov.uk/components/task-list/

## IBM Carbon

- Forms Pattern  
  https://carbondesignsystem.com/patterns/forms-pattern/

- Accordion  
  https://carbondesignsystem.com/components/accordion/usage/

- Buttons  
  https://carbondesignsystem.com/components/button/usage/

- Tabs  
  https://carbondesignsystem.com/components/tabs/usage/

---

# Revision History

| Version | Date | Summary |
|---|---|---|
| 0.1 | 2026-07-18 | Created the first verified cross-system mechanism map across Microsoft, Apple, Material, GOV.UK, and Carbon; defined equivalence grades; mapped twelve mechanisms; identified non-equivalences; and produced seven candidate Atlas principles. |
