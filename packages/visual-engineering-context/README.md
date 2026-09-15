# Visual Engineering Context

Generated, versioned UI research context for implementation agents.

> **Legacy compatibility.** This package remains supported and is not changing. New projects
> should use the canonical Echelon Foundry lifecycle interface instead:
>
> ```bash
> npx @echelon-foundry/visual-engineering init
> ```
>
> It installs the same context, adds an installation manifest, verification, diagnostics and
> in-place upgrades, and adopts an existing `ve-context` installation without deleting anything.
> See the [repository README](../../README.md) and [docs/installation.md](../../docs/installation.md).

## Always retrieve the latest published context

```bash
npm exec --yes --package=@kemiller2002/visual-engineering-context@latest -- ve-context sync
```

This writes the verified briefing to `.visual-engineering/` in the current project.

Then instruct the agent to read:

1. `.visual-engineering/AGENT-INSTRUCTIONS.md`
2. `.visual-engineering/UI-FOUNDATIONS.md`
3. `.visual-engineering/UI-DECISION-CHECKLIST.md`
4. `.visual-engineering/UI-ANTI-PATTERNS.md`
5. `.visual-engineering/RESEARCH-INDEX.md`

For reproducible production work, install and pin an exact version instead:

```bash
npm install --save-dev --save-exact @kemiller2002/visual-engineering-context
npm exec ve-context sync
```

Available commands:

```bash
ve-context sync
ve-context verify
ve-context status
ve-context show foundations
ve-context show checklist
ve-context show anti-patterns
ve-context show research
```
