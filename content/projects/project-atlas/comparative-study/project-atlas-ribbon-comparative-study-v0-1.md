---
id: RR-VE-ATL-2026-942D
project: project-atlas
purposes:
  - apply
  - integrate
  - verify
audiences:
  - practitioner
  - researcher
---

# Project Atlas – Ribbon Comparative Study v0.1

## Objective

Compare multiple Ribbon implementations using the same Atlas framework instead of judging them by preference.

## Comparison Set

1. Microsoft Word – Standard Ribbon
2. Microsoft Word – Simplified Ribbon
3. Microsoft Word – Contextual Picture Format Ribbon
4. Representative Third-Party Ribbon

## Standard Ribbon

Strengths:
- Rich task-oriented grouping
- Persistent tab structure
- Labeled command groups
- Large controls communicate priority
- Quick Access Toolbar separates cross-context commands

Atlas mechanisms:
- Relative Separation
- Hierarchical Spacing Differentiation
- Semantic Label Reinforcement
- Recognition over Recall

Weaknesses:
- Higher visual density
- Consumes vertical space
- Large command inventories can overwhelm novices

## Simplified Ribbon

Advantages:
- Lower visual competition
- Cleaner scanning
- Reduced cognitive entry cost

Tradeoffs:
- Less recognition
- More hidden hierarchy
- More interaction steps

Candidate principle: Recognition Exposure Tradeoff.

## Contextual Ribbon

Advantages:
- Removes irrelevant commands
- Strong task context

Risks:
- Poor discoverability
- Users may never know commands exist

Candidate principle: Contextual Disclosure.

## Third-Party Ribbon Evaluation

Checklist:
- Tabs organized by user task
- Meaningful groups
- Spacing and labels reinforce each other
- Large controls reflect importance
- Predictable responsive collapse
- Justified persistent commands
- Minimal duplication

## Comparative Summary

| Feature | Standard | Simplified | Contextual |
|---|---|---|---|
| Recognition | High | Medium | High |
| Visual Density | High | Low | Medium |
| Discoverability | High | Medium | Low |
| Recall Required | Low | Medium | Medium |
| Task Context | Medium | Medium | High |

## Research Backlog

Collect literature on recognition vs recall, menu breadth vs depth, spatial memory, adaptive interfaces, progressive disclosure, command frequency, eye tracking, and expert vs novice behavior.

## Image Collection Queue

- Word Home
- Word Simplified Ribbon
- Picture Format Ribbon
- Excel Home
- Office 2003
- Weak third-party Ribbon

Annotate each with intended groups, competing groups, spacing hierarchy, salience hierarchy, Atlas principles, and improvement opportunities.

## Next Milestone

Repeat the same process for Apple HIG, Material Design, IBM Carbon, GOV.UK, and NASA standards.