# Prompts

This directory holds autonomous-agent prompts. They are **inputs and historical records**, not
documentation and not instructions to follow when working in this repository. Nothing here is
executed by a build, a workflow, or a test.

## Implementation policy

Repository lifecycle tooling in this repository is implemented in **F#**.

`src/VisualEngineering.Core` owns every lifecycle decision — detection, installation,
configuration, validation, diagnosis and upgrade — and `src/VisualEngineering.Cli` is a thin
adapter over it. Node exists only as the minimal npm/npx bootstrap that launches the F#
executable. See [docs/development.md](../docs/development.md).

A prompt that directs an agent to implement repository lifecycle tooling in JavaScript,
TypeScript, Python or shell is therefore obsolete, regardless of how well it worked at the time.
Those prompts are marked **Deprecated** below and carry a banner at the top of the file. They are
kept because they record how the current design was reached; they must not be re-run.

New tooling work starts from the F# core, not from a prompt in this directory.

## Status

### Deprecated

Superseded by the F# lifecycle tool, or already executed once and not repeatable.

| Prompt | Why |
| --- | --- |
| [implement-project-context-contract.md](implement-project-context-contract.md) | Designed the Node `ve-context` context contract. Superseded by `@echelon-foundry/visual-engineering`, which installs, verifies, diagnoses and upgrades the same context from F#. |
| [install-research-publisher-on-existing-repo.md](install-research-publisher-on-existing-repo.md) | Installs a capability into a repository by prompting an agent. That is now a lifecycle command backed by F#, not a prompt. |
| [upgrade-existing-research-publisher-installation.md](upgrade-existing-research-publisher-installation.md) | Upgrades an installation by prompting an agent. Superseded by the typed, sequential migration model in `src/VisualEngineering.Core/Migrations.fs`. |
| [visual-engineering-repository-cleanup-codex-prompt.md](visual-engineering-repository-cleanup-codex-prompt.md) | A one-off restructuring that has already run. Its output is `content/`, including `content/archive/`. Re-running it would re-litigate a completed migration. |

### Active

Research prompts. These produce research artifacts under `content/`; they do not implement
tooling, so the F# policy above does not apply to them.

| Prompt | Purpose |
| --- | --- |
| [Clinical-Communication-Engineering-REP-Research-Agent-v2.md](Clinical-Communication-Engineering-REP-Research-Agent-v2.md) | Clinical communication engineering research execution package |
| [REP_Visual_Engineering_Johannes_Itten_Modern_Color_Theory.md](REP_Visual_Engineering_Johannes_Itten_Modern_Color_Theory.md) | Itten colour theory research execution package |
| [RP-COMP-005-Visual-Scene-Construction-Predictive-Processing-and-Active-Perception.md](RP-COMP-005-Visual-Scene-Construction-Predictive-Processing-and-Active-Perception.md) | Composition theory research plan |
| [visual-engineering-section-research.md](visual-engineering-section-research.md) | Generates the section research roadmap |
| [research-publisher-mark-documents.md](research-publisher-mark-documents.md) | Classifies an existing Markdown corpus by reader purpose. Organizes research content rather than installing tooling. |
| [visual-engineering/](visual-engineering/) | Per-section research roadmaps and prompts |

### Out of scope

| Prompt | Note |
| --- | --- |
| [Visual-Engineering-Web-Component-Framework-Research-and-Handoff-Prompt.md](Visual-Engineering-Web-Component-Framework-Research-and-Handoff-Prompt.md) | Targets a separate Web Components repository, not this repository's tooling. |
| [web-components/implement-cross-project-web-component-framework.md](web-components/implement-cross-project-web-component-framework.md) | Same. |
