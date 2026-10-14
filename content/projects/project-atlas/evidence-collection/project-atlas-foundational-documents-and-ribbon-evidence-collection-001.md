---
id: RR-VE-ATL-2026-9D11
title: "Project Atlas Foundational Documents and Evidence Collection 001: Microsoft Ribbon"
project: project-atlas
version: "0.1"
status: "Verified Source Register and Applied Evidence Review"
date: "2026-07-18"
authors:
  - Kevin Miller
  - ChatGPT
principles:
  - "ATLAS-0001: Proximity and Relative Separation"
  - "Candidate: Hierarchical Spacing Differentiation"
  - "Candidate: Command Salience"
  - "Candidate: Contextual Disclosure"
purpose: |
  Establish the Foundational Documents collection, register Microsoft's primary
  Ribbon documentation, and begin collecting positive and negative examples
  through an Atlas evidence framework.
llm_ingest: true
machine_readable: true
purposes:
  - verify
  - reference
audiences:
  - practitioner
  - researcher
---

# Project Atlas Foundational Documents and Evidence Collection 001

## Microsoft Ribbon

# Purpose

This document begins two linked Atlas collections:

1. **Foundational Documents**, containing authoritative design guidance,
   standards, and first-party explanations.
2. **Applied Evidence Gallery**, containing real-world compositions analyzed
   through Atlas principles.

The Microsoft Ribbon is the first case because it is a mature command interface
with explicit structural guidance covering:

- tabs;
- labeled groups;
- command sizing;
- contextual commands;
- the Quick Access Toolbar;
- adaptive scaling;
- discoverability;
- keyboard access;
- consistency.

The Ribbon is not accepted as correct merely because Microsoft created it.
Atlas treats the documentation as evidence of design intent and the interface as
an applied composition that can be analyzed, supported, challenged, and
compared.

---

# Collection Architecture

## Collection A — Foundational Documents

Authoritative or historically important materials that state design rules,
goals, mechanisms, or constraints.

Each document record should contain:

- Document ID
- Organization
- Title
- Source type
- Publication or update date
- Authority level
- Scope
- Key claims
- Relevant Atlas principles
- Evidence limitations
- Stable source
- Archive status

## Collection B — Applied Evidence Gallery

Real-world examples evaluated using the current Atlas model.

Each entry should contain:

- Gallery ID
- Product or composition
- Date or version
- Source image
- Intended task
- Intended groups
- Competing organizations
- Positive mechanisms
- Negative mechanisms
- Atlas principles
- Predictions
- Confidence
- Research links

## Collection C — Scientific Literature

Published research that supports, qualifies, contradicts, or explains the
design guidance and observed examples.

The three collections should remain linked but distinct:

```text
Foundational recommendation
        ↓
Applied implementation
        ↓
Scientific mechanism
        ↓
Observed outcome or contradiction
```

A design document is evidence that an organization recommends a practice. It is
not, by itself, proof that the practice improves human performance.

---

# Foundational Document Register

## FND-MS-001 — Windows 7 Ribbons UX Guide

### Organization

Microsoft

### Title

**Windows 7 Ribbons**

### Source type

First-party user-experience guidance

### Authority level

**Primary design guidance**

### Scope

Guidance for deciding when to use a Ribbon and how to organize tabs, groups,
commands, labels, scaling, contextual tabs, and the Quick Access Toolbar.

### Verified source

https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons

### Core documented claims

Microsoft defines a Ribbon as a command bar that organizes features into tabs
at the top of a window.

The guide treats a Ribbon as appropriate when:

- commands benefit from direct exposure;
- users otherwise have difficulty finding commands;
- commands can be organized into meaningful groups;
- the interface can justify the required vertical space.

The guide states that good Ribbon design is not achieved by moving existing
menu commands into tabs. Command organization should be task-oriented rather
than inherited from implementation or historical menu structure.

### Atlas relevance

- Proximity and common region create command groups.
- Group labels reinforce organization.
- Tabs expose one command context while suppressing others.
- Large and small control sizes create salience differences.
- Contextual tabs condition available commands on object or task state.
- The Quick Access Toolbar creates a persistent cross-context command group.
- Scaling policies preserve relative organization as space changes.

### Evidence limitations

This is normative guidance. It does not publish the full experimental record or
effect sizes behind each recommendation.

---

## FND-MS-002 — Overview of the Office Fluent Ribbon

### Organization

Microsoft

### Title

**Overview of the Office Fluent Ribbon**

### Source type

First-party Office conceptual documentation

### Authority level

**Primary product documentation**

### Verified source

https://learn.microsoft.com/en-us/office/vba/library-reference/concepts/overview-of-the-office-fluent-ribbon

### Core documented claim

The Office Fluent Ribbon replaced the earlier layered combination of menus,
toolbars, and task panes with a tabbed command surface.

### Atlas relevance

The replacement changes the command-discovery model:

```text
recall menu path
    → recognition of exposed commands
```

It also changes the spatial model:

```text
separate menus + toolbars + panes
    → one coordinated command region
```

### Evidence limitations

The page explains architecture and customization more than the original design
research.

---

## FND-MS-003 — Introducing the Windows Ribbon Framework

### Organization

Microsoft

### Title

**Introducing the Windows Ribbon Framework**

### Source type

First-party framework overview

### Authority level

**Primary technical and conceptual documentation**

### Verified source

https://learn.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-introduction

### Core documented claims

The Windows Ribbon Framework provides a standardized command framework with
consistent appearance and behavior, accessibility support, high-contrast
adaptation, and high-DPI awareness.

### Atlas relevance

This source connects visual organization with platform consistency,
accessibility, and adaptive presentation.

### Evidence limitations

Framework capability does not guarantee good information architecture.

---

## FND-MS-004 — Ribbon Size Definitions and Scaling Policies

### Organization

Microsoft

### Title

**Customizing a Ribbon Through Size Definitions and Scaling Policies**

### Source type

First-party implementation guidance

### Authority level

**Primary technical guidance**

### Verified source

https://learn.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-templates

### Core documented claims

Microsoft provides structured templates controlling group layout, supported
control families, size behavior, and adaptation as available width changes.

### Atlas relevance

The Ribbon treats responsive reduction as a controlled hierarchy rather than
unstructured shrinking.

This raises an important Atlas question:

> Which commands retain visual salience as space collapses, and what model
> determines that priority?

### Evidence limitations

Technical templates encode assumptions about importance and grouping but do not
fully explain their perceptual basis.

---

## FND-MS-005 — Fluent 2 Layout

### Organization

Microsoft

### Title

**Layout — Fluent 2 Design System**

### Source type

Current first-party design-system guidance

### Authority level

**Primary current design guidance**

### Verified source

https://fluent2.microsoft.design/layout

### Core documented claim

Fluent describes layout as using spacing and hierarchy to create relationships,
highlight importance, and support decisions.

### Atlas relevance

This is directly aligned with:

- Relative Separation
- Hierarchical Spacing Differentiation
- Nested grouping
- Salience allocation

### Evidence limitations

The guidance offers a design framework, not a scientific derivation of every
spacing value.

---

## FND-MS-006 — Fluent 2 Design Principles

### Organization

Microsoft

### Title

**Design Principles — Fluent 2 Design System**

### Source type

Current first-party design philosophy

### Authority level

**Primary current design guidance**

### Verified source

https://fluent2.microsoft.design/design-principles

### Atlas relevance

The principles provide current organizational intent that can be compared with
the Ribbon's earlier task- and command-oriented architecture.

### Evidence limitations

High-level principles are not operational laws and may permit several
contradictory implementations.

---

## FND-MS-007 — Current Windows App Design Guidance

### Organization

Microsoft

### Title

**Design Windows Apps Overview**

### Source type

Current platform design guidance

### Authority level

**Primary current design guidance**

### Verified source

https://learn.microsoft.com/en-us/windows/apps/design/

### Atlas relevance

Provides the present context for layout, navigation, input, typography, motion,
and platform behavior. It allows Atlas to distinguish historical Ribbon
guidance from Microsoft's current application-design direction.

---

# Applied Evidence Gallery

# GAL-MS-001 — Modern Word Ribbon: Positive Structural Example

## Domain

Desktop productivity software

## Composition

Microsoft Word Ribbon with:

- top-level tabs;
- visible selected tab;
- labeled command groups;
- mixed control sizes;
- local separators;
- icons paired with text where needed;
- persistent Quick Access commands.

## Intended task

Allow users to locate and invoke a large command set without navigating deeply
nested menu structures.

## Intended organization

```text
application
    ↓
task context represented by tab
    ↓
functional command group
    ↓
individual command or command family
```

## Positive Atlas observations

### 1. Multiple grouping levels are explicitly represented

Tabs, group boundaries, labels, spacing, and control size create a nested
organization.

The composition does not ask one spacing rhythm to communicate every level.

### 2. Compatible cues reinforce grouping

Commands are grouped through several simultaneous cues:

- proximity;
- alignment;
- common vertical region;
- group labels;
- separators;
- icon and label treatment.

This should reduce reliance on any one cue.

### 3. Group labels provide semantic repair

Pure geometry might distinguish groups without explaining them. Labels convert
the geometric partition into a named command model.

### 4. Control size expresses salience

Large controls are more visually prominent than compact controls.

Atlas interpretation:

> Size is being used as a prior over likely importance, frequency, or
> discoverability.

That interpretation must be checked against Microsoft's explicit command
priority rules and actual usage data.

### 5. The selected tab creates controlled disclosure

Only one broad command context is fully exposed at a time.

This reduces simultaneous command count while preserving visible top-level
choices.

### 6. Persistent commands are separated from contextual commands

The Quick Access Toolbar provides a small command group that remains available
across tab changes.

This is a deliberate exception to the tab hierarchy.

## Risks and limitations

- A command placed under an unexpected tab may remain difficult to find.
- Multiple small icons can become visually dense and semantically weak.
- Group names may be too general to predict included commands.
- Large controls can imply importance that does not match a particular user's
  task.
- Frequent tab switching creates interaction cost.
- Responsive collapse can hide commands or weaken learned spatial memory.
- The Ribbon occupies substantial vertical space.

## Atlas confidence

**Moderate to high for the structural diagnosis**

The interface clearly implements nested grouping and compatible cues. Claims
about improved task performance require separate evidence.

---

# GAL-MS-002 — Office 2003 Menus and Toolbars: Useful Negative Comparison

## Classification note

This is not classified as universally bad.

It is a comparative example that reveals problems the Ribbon was designed to
address and advantages the Ribbon may have sacrificed.

## Composition

Classic Office interface containing:

- textual menu bar;
- one or more icon toolbars;
- formatting controls;
- commands hidden inside menus;
- commands split among several surfaces.

## Negative Atlas observations

### 1. Related commands can be distributed across different surfaces

A task may require searching menus, toolbars, dialogs, and contextual panes.

This weakens spatial unity for the task even when each individual surface is
internally organized.

### 2. Icon rows can flatten hierarchy

When many commands receive similar icon size, spacing, and chrome, visual
salience is weakly differentiated.

### 3. Hidden menu depth increases recall and path cost

Users may need to remember:

```text
menu
    → submenu
    → dialog
    → tab
    → control
```

The interface can remain visually quiet while imposing navigational memory.

### 4. Toolbars can accumulate historically

Additional commands may be appended according to implementation history rather
than user task structure.

## Positive properties preserved by the older model

### 1. Lower persistent vertical footprint

Menus occupy little space when closed.

### 2. Stable textual categories

Menu names provide a familiar high-level map.

### 3. Efficient expert paths

Keyboard shortcuts and memorized menu paths can be fast for experienced users.

### 4. Less simultaneous visual density

Most commands remain hidden until requested.

## Atlas interpretation

The old interface and Ribbon optimize different constraints:

| Classic menus and toolbars | Ribbon |
|---|---|
| Lower persistent visual footprint | Greater direct command exposure |
| More hidden hierarchy | More visible hierarchy |
| Greater recall of paths | Greater recognition of commands |
| Stable textual menus | Richer spatial command organization |
| Potentially efficient for experts | Potentially easier discovery for broader users |

The Ribbon is therefore not simply "better." It reallocates costs.

---

# GAL-MS-003 — Poor Ribbon Implementation: Negative Pattern

## Pattern

A Ribbon-like interface can fail when developers reproduce the visual shell
without the underlying task architecture.

Common failures include:

- tabs copied from former menu categories;
- too many tabs;
- vague group names;
- every command given equal size;
- unrelated commands placed together to fill space;
- duplicate commands without a clear persistence rationale;
- excessive contextual colors;
- custom controls inconsistent with platform behavior;
- groups that collapse unpredictably;
- commands organized by internal subsystem instead of user task.

## Atlas diagnosis

### Surface imitation without structural modeling

The interface has the appearance of hierarchy but weak semantic organization.

### Competing groupings

Proximity and group borders claim that commands belong together while user
tasks or semantics claim otherwise.

### False salience

Large icons may be assigned for layout convenience rather than task relevance.

### Unstable spatial learning

Poor scaling can move or hide commands inconsistently as the window changes.

## Confidence

**High as a failure pattern; individual examples require case-specific review**

---

# Ribbon Mechanism Map

```text
large command inventory
    ↓
task-based tab partition
    ↓
functional groups within active context
    ↓
salience through control size and labels
    ↓
recognition-based command search
    ↓
command selection
```

Moderators include:

- correctness of information architecture;
- user's current task;
- command frequency;
- experience;
- window width;
- accessibility settings;
- semantic quality of labels and icons;
- persistence of command location;
- contextual tab visibility.

---

# Ribbon Principles Added to the Atlas Backlog

## CAND-MS-001 — Recognition Exposure Tradeoff

### Hypothesis

Exposing commands improves recognition and discovery but increases visual
density and competition for attention.

### Boundary

The benefit should reverse when the number or similarity of visible controls
overwhelms differentiation.

---

## CAND-MS-002 — Task-Oriented Command Grouping

### Hypothesis

Commands grouped according to user goals should be easier to predict and locate
than commands grouped according to implementation ownership or historical
menu placement.

### Evidence needed

- command-location studies;
- menu-information architecture studies;
- card-sorting or expectation studies;
- task-completion comparisons.

---

## CAND-MS-003 — Persistent Command Exception

### Hypothesis

A small persistent command set can reduce context-switching cost when those
commands are frequently used across several task contexts.

### Risk

An expanding persistent area recreates the undifferentiated toolbar problem.

---

## CAND-MS-004 — Adaptive Salience Preservation

### Hypothesis

When space contracts, reducing low-priority commands before high-priority
commands preserves task performance better than uniform scaling.

### Evidence needed

- responsive Ribbon studies;
- command frequency data;
- spatial-memory studies;
- effects of command relocation.

---

## CAND-MS-005 — Semantic Label Reinforcement

### Hypothesis

Group labels improve predictability when their names accurately describe the
commands they contain.

### Boundary

Generic labels such as "Tools," "More," or "Other" provide weak semantic
constraint.

---

## CAND-MS-006 — Contextual Disclosure

### Hypothesis

Showing commands only when the relevant object or task state exists reduces
irrelevant competition.

### Risk

Users may not discover that a contextual command exists or may not understand
how to activate the required context.

---

# Initial Example Collection Queue

The next examples should deliberately include both strong and weak
implementations.

## Microsoft examples

1. Word Home tab
2. Word Insert tab
3. Picture Format contextual tab
4. Excel Home tab
5. Excel Data tab
6. PowerPoint Design and Transitions tabs
7. Outlook classic Ribbon
8. Simplified Ribbon
9. File Explorer historical Ribbon
10. Third-party Ribbon implementation

## Cross-system comparisons

1. Adobe Creative Cloud toolbars and panels
2. Apple productivity application toolbars
3. Google Docs command surface
4. AutoCAD Ribbon
5. JetBrains IDE tool windows and menus
6. Visual Studio command system
7. Figma toolbar and contextual property panel
8. Blender workspaces and properties
9. ERP command ribbons
10. Medical or engineering desktop software

## Negative-pattern targets

1. Overloaded Ribbon tab
2. Ambiguous group labels
3. Too many equal-sized icons
4. Commands duplicated across multiple groups
5. Contextual tab that users fail to notice
6. Ribbon with poor responsive collapse
7. Ribbon organized by system module rather than task
8. Custom Ribbon with inconsistent controls
9. Excessive use of separators
10. Simplified command bar that hides essential commands

---

# Evidence Capture Template

```yaml
gallery_id:
product:
version_or_date:
source_url:
image_rights:
domain:
task:
user_population:

intended_groups:
competing_groups:

geometry:
  tab_count:
  visible_command_count:
  group_count:
  group_widths:
  separator_count:
  large_control_count:
  small_control_count:

cues:
  proximity:
  common_region:
  separators:
  labels:
  iconography:
  size:
  color:
  context:

positive_observations:
negative_observations:
predictions:
supporting_documents:
supporting_science:
confidence:
open_questions:
```

---

# Important Research Boundary

Microsoft's documentation supports claims about:

- what a Ribbon is;
- how Microsoft recommends it be organized;
- which components and behaviors the framework provides;
- which design concerns Microsoft considers important.

It does **not**, by itself, establish:

- universal superiority over menus;
- a quantified reduction in task time;
- a universal discoverability benefit;
- optimal tab, group, or command counts;
- the correct large-versus-small icon ratio;
- applicability to every software domain.

Those claims require scientific studies, product telemetry, or direct
comparative evidence.

---

# Next Actions

1. Capture representative images of:
   - modern Word Ribbon;
   - Office 2003 menu and toolbar interface;
   - simplified Ribbon;
   - contextual Ribbon tab.

2. Apply the evidence-capture template to each.

3. Pull scientific literature on:
   - menu breadth versus depth;
   - recognition versus recall;
   - command search;
   - visual search among icons and labels;
   - semantic grouping;
   - adaptive menus;
   - spatial memory and command relocation.

4. Compare Microsoft's normative recommendations with the scientific evidence.

5. Add GOV.UK, Apple, Material, IBM Carbon, and NASA or FAA guidance to the
   Foundational Documents register.

---

# Revision History

| Version | Date | Summary |
|---|---|---|
| 0.1 | 2026-07-18 | Created the Foundational Documents collection, verified seven Microsoft primary sources, added three Ribbon gallery entries, defined six candidate principles, and created the collection queue and evidence-capture schema. |
