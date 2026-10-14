---
id: RR-VE-DL-2026-9947
authors:
- OpenAI Research Agent
confidence: Moderate
date: 2026-07-19
llm_ingest: true
machine_readable: true
project: design-library
purpose: |
  Establish the evidence-backed architectural foundation upon which a long-lived,
  highly redesignable web component library should later be implemented.
references:
- https://html.spec.whatwg.org/multipage/custom-elements.html
- https://www.w3.org/WAI/WCAG22/quickref/
- https://open-ui.org/
- https://design-tokens.github.io/community-group/format/
status: research-baseline
summary: |
  The evidence supports a hybrid, native-first component library rather than a
  web-component-only architecture. Semantic HTML can remain relatively stable
  across substantial visual redesign, but only when markup represents durable
  information relationships rather than current visual groupings. Different
  categories require different ownership and encapsulation strategies. Native
  controls should remain native unless measurable requirements cannot be met;
  light DOM is favored for content-rich, composition-sensitive structures;
  Shadow DOM is favored for tightly bounded behavioral widgets whose internal
  invariants require protection. CSS should own presentation and contextual
  layout, while component logic owns only intrinsic behavior and state. Design
  tokens should encode decisions through a layered dependency model, but must
  not become a universal escape-hatch API. Implementation should begin only
  after a small set of targeted validation experiments confirms these rules in
  the project’s actual product contexts.
version: 0.1
purposes:
  - integrate
  - verify
audiences:
  - executive
  - practitioner
  - researcher
entryPoint: true
entryPointOrder: 10
entryPointLabel: Research foundation
---

# Component Library Foundations Research Report

## Purpose

This report determines what the future component library should be built upon. It does not define production APIs, names, markup, tokens, or components. Its purpose is to reduce the chance of creating architectural commitments that later prevent visual redesign, accessibility, replacement, or cross-product reuse.

The central question is not whether Web Components work. They do. The central question is where custom elements, native HTML, CSS, JavaScript behavior, design tokens, and composition each create the most durable boundary.

------------------------------------------------------------------------

# Executive Summary

## Final research position

The most defensible foundation is a **hybrid, native-first, category-specific architecture**.

A single universal component model is not supported by the evidence. The platform itself distinguishes semantic elements, form controls, custom elements, shadow trees, slots, accessibility APIs, and styling mechanisms because these solve different classes of problems. Mature design systems similarly separate foundations, tokens, components, patterns, and product-level compositions.

The future library should therefore be built on five architectural strata:

1. **Native semantic HTML** for document meaning and browser-provided interaction.
2. **Styling conventions and compositions** for visual presentation and page-level relationships.
3. **Behavioral enhancement** for reusable interaction that the platform does not provide adequately.
4. **Semantic or composite structures** only where a stable domain relationship has been demonstrated across multiple contexts.
5. **Custom elements with selective encapsulation** where lifecycle, state coordination, replacement boundaries, or implementation protection provide measurable value.

## Findings with the highest confidence

### F-001: Native controls must be the default

Native controls carry semantics, keyboard behavior, form participation, accessibility mappings, mobile behavior, and browser integration that custom replacements must reconstruct. Open UI’s research repeatedly identifies reduced accessibility, reliability, and performance in custom replacements for selects, comboboxes, and ranges. The correct burden of proof is therefore reversed: a team must justify replacing a native element, not justify keeping it.

### F-002: Semantic stability is conditional, not absolute

HTML can survive radical visual redesign when it expresses durable content relationships. It fails when it encodes a particular layout, prominence scheme, or product context. “Stable markup” should mean stable while the meaning remains stable, not immutable regardless of what the design reveals.

### F-003: Encapsulation must vary by component category

Shadow DOM provides genuine protection from accidental external CSS and DOM manipulation. That protection also creates boundaries around styling, ID references, querying, content relationships, debugging, testing, and global composition. Content-rich structures and components whose visual hierarchy must remain open to experimentation generally favor light DOM. Mechanically bounded widgets with protected interaction invariants more often justify Shadow DOM.

### F-004: Layout ownership is the major coupling boundary

Most reusable components should not own their placement, external spacing, width, or page-level arrangement. A component should own only layout intrinsic to its semantics or interaction. Parent compositions should own relationships among siblings. Confusing intrinsic and contextual layout is a primary source of modes such as `horizontal`, `dashboard`, `marketing`, and `compact`.

### F-005: Tokens are a dependency system, not a bag of variables

Material, Spectrum, Carbon, and FAST all treat tokens as named design decisions. The useful common pattern is indirection from reference values toward semantic and contextual decisions. The danger is excessive component tokens, which expose internals and turn theming into remote control of every declaration. Token layers must have explicit responsibilities and permitted dependencies.

### F-006: Accessibility cannot be “contained” entirely inside a component

A component can guarantee intrinsic keyboard interaction, roles, state exposure, focus behavior, and local labeling hooks. It cannot guarantee meaningful labels, correct placement in a page hierarchy, valid reading order, sufficient surrounding instructions, or appropriate use. Accessibility is therefore a shared contract between the library and the consumer.

### F-007: Cross-framework interoperability is achievable but incomplete as a definition of reuse

Custom elements provide a browser-level element interface and can cross framework boundaries. That does not automatically solve data-binding conventions, server rendering, event conventions, forms, styling, documentation, or author ergonomics. “Works in React, Vue, and plain HTML” is necessary evidence for interoperability, but not sufficient evidence of a good component.

### F-008: Visual redesignability depends more on semantic and CSS boundaries than on custom elements

Custom elements do not inherently make an interface redesignable. A poorly modeled custom element can be more rigid than ordinary HTML. Redesignability comes from durable content models, presentation-neutral DOM order, parent-owned composition, contextual tokens, low-specificity CSS, and controlled escape hatches.

## Assumptions weakened or rejected

- **Rejected:** Every reusable UI element should become a custom element.
- **Rejected:** Shadow DOM should be the default because encapsulation is always beneficial.
- **Rejected:** A sufficiently flexible component can serve every product context through variants.
- **Rejected:** Accessibility can be solved once inside the library.
- **Weakened:** Markup can remain unchanged through any redesign.
- **Weakened:** Themes can produce radically different systems through token substitution alone.
- **Weakened:** Design tokens necessarily reduce complexity.
- **Weakened:** Component reuse is always preferable to duplication.

## Recommended implementation posture

Implementation should proceed after a short validation gate, not after indefinite research. The gate should test the highest-risk conclusions in the project’s real environment:

- stable semantic content across contrasting layouts,
- native versus custom control tradeoffs,
- light versus Shadow DOM by category,
- nested theme isolation,
- no-JavaScript and server-rendered behavior,
- and redesign blast radius.

The goal of the first implementation phase should not be a broad catalog. It should be to establish reliable foundations and prove that one small vertical slice can survive genuine redesign.

------------------------------------------------------------------------

# Research Method

This report used repeated comparison across four evidence classes:

1. **Platform standards:** WHATWG HTML, W3C CSS, WCAG, ARIA-related specifications and guidance.
2. **Platform evolution research:** Open UI investigations into controls and common interaction patterns.
3. **Mature production systems:** Material, Carbon, Spectrum, FAST, and related Web Component libraries.
4. **Failure-oriented analysis:** identifying where apparently reusable abstractions transfer complexity into variants, tokens, slots, consumer knowledge, or accessibility obligations.

The research deliberately separates observations from interpretations. Some conclusions are strong platform constraints; others remain architectural recommendations requiring validation in the future library’s actual contexts.

------------------------------------------------------------------------

# Foundational Model

## The library should not be one thing

The phrase “component library” hides several distinct artifacts:

- semantic HTML conventions,
- CSS foundations,
- design decisions,
- behavior modules,
- custom elements,
- composite domain structures,
- accessibility contracts,
- layout compositions,
- documentation,
- tests,
- and governance.

Treating all of these as equivalent “components” creates false uniformity. A button and a report section may both appear as entries in documentation, but their engineering constraints are fundamentally different.

## Proposed research classification

This is a classification model, not a production naming or API decision.

### C1 — Native semantic element

Use browser HTML directly. The library may document it but does not wrap it.

Appropriate when HTML already expresses the meaning and behavior accurately.

Examples of likely candidates include headings, paragraphs, lists, links, buttons, many inputs, tables, `details`, and `dialog` where platform support meets requirements.

### C2 — Styled native convention

Native HTML remains the author-facing and accessibility-facing object. Shared CSS and usage guidance provide a consistent visual and behavioral baseline.

This is likely the default category for ordinary controls.

### C3 — Progressive behavioral enhancement

A script or custom element enhances valid, understandable native markup. The underlying content remains usable before upgrade or after JavaScript failure.

This is appropriate for disclosure orchestration, certain tab patterns, validation coordination, or data-table behavior.

### C4 — Bounded behavioral widget

A reusable widget owns a nontrivial state machine and keyboard model. It may justify a custom element and Shadow DOM when protecting internal invariants has more value than open composition.

Examples could include a date picker, complex combobox, or specialized data visualization, but only when native capabilities are insufficient.

### C5 — Semantic composite

A stable domain concept consists of several meaningful subregions. It may use light DOM, structured child elements, or constrained slots. Its validity depends on repeated evidence that the domain relationship survives across contexts.

Examples might include an evidence record, experiment result, financial metric, or message thread—not a generic “card.”

### C6 — Composition pattern

A CSS-managed relationship among independent children. It should normally not become a custom element unless it adds meaningful semantics, lifecycle, constraints, or measurement.

Examples include stack, cluster, sidebar, grid, frame, and content measure.

### C7 — Product composition

A page- or workflow-specific assembly. Reuse may happen, but it should not be promoted into the shared library until repeated independent use reveals a stable abstraction.

### C8 — Experimental or rejected abstraction

An explicit category is needed for ideas that are being tested or have failed. Without it, experiments tend to become permanent through inertia.

------------------------------------------------------------------------

# Assumption Registry

## A-001: Semantic HTML can remain stable through substantial redesign

**Why it exists:** The project wants to improve visual design rapidly through CSS without continuously rebuilding markup and behavior.

**Supporting evidence:** HTML and CSS are explicitly separate languages for structure/semantics and presentation. CSS can alter typography, spacing, layout, grouping, color, media-specific rendering, and responsive behavior. Modern container queries and cascade controls extend contextual adaptation.

**Contradicting evidence:** Visual redesign can expose a different information hierarchy or interaction model. CSS reordering can produce conflict between visual order, reading order, focus order, and source order. A structure designed as a dashboard summary may not contain the semantic relationships required by an editorial narrative.

**Confidence:** Moderate-high.

**Current recommendation:** Seek semantic stability, not DOM immutability. Markup changes are acceptable when the meaning, interaction, or required reading order changes. They are suspect when driven only by surface styling.

**Open questions:** How often do the project’s real designs require different content order rather than different presentation? Which domain structures survive editorial, dashboard, mobile, and report contexts?

## A-002: Most visual redesign should be possible in CSS

**Supporting evidence:** CSS owns rendering across screen, print, and other media; custom properties, container queries, cascade layers, logical properties, media queries, and scoped selectors provide substantial adaptation.

**Contradicting evidence:** CSS cannot invent missing semantics, change accessible relationships safely, or replace a fundamentally different interaction model. Excessive CSS flexibility can also produce hidden coupling, selector complexity, and theme-specific exceptions.

**Confidence:** High within presentation; low outside presentation.

**Recommendation:** CSS owns appearance and contextual layout. Do not use CSS to conceal semantic mismatch. Measure redesign success by unchanged meaning and APIs, not by zero markup changes at any cost.

## A-003: Web Components should form the center of the library

**Supporting evidence:** Custom elements provide browser-native lifecycle and a reusable element interface. Shadow DOM and slots can support encapsulation and composition. They are framework-independent at the platform level.

**Contradicting evidence:** Native HTML already solves many controls better. Custom elements add upgrade timing, authoring rules, event/API conventions, SSR considerations, form participation decisions, testing complexity, and accessibility obligations. Cross-framework use still requires integration discipline.

**Confidence:** Low as a universal strategy; moderate for selected categories.

**Recommendation:** Use custom elements selectively. The library may expose a coherent catalog without implementing every catalog entry as a custom element.

## A-004: Shadow DOM should be the default encapsulation strategy

**Supporting evidence:** Shadow DOM prevents accidental external CSS and DOM mutation, protects implementation details, scopes selectors, and supports parts and slots.

**Contradicting evidence:** It restricts ordinary styling and querying; cross-root accessible relationships and ID references require care; global typography and composition do not pass through in the same way; slotted content introduces a two-tree mental model; theme APIs must be deliberately exposed; debugging and testing become more complex.

**Confidence:** Low as a default; high as a targeted tool.

**Recommendation:** Choose encapsulation by category. Favor light DOM for content-rich semantic composites and highly redesignable structures. Favor Shadow DOM for bounded widgets with protected internal invariants. Avoid closed roots for the general library.

## A-005: Design tokens make radical redesign inexpensive

**Supporting evidence:** Material, Spectrum, Carbon, and FAST use tokens to translate reusable design decisions into data or custom properties. Tokens enable consistent changes and scoped themes.

**Contradicting evidence:** Tokens cannot change DOM relationships, interaction models, or composition by themselves. Component-level token proliferation can expose implementation details and create a second, poorly typed API. Token renaming can itself create broad migrations, as documented by mature systems.

**Confidence:** High for controlled design decisions; moderate for radical redesign.

**Recommendation:** Treat tokens as a constrained dependency graph. Use them for repeated decisions with semantic meaning. Do not create a token merely to make every declaration externally configurable.

## A-006: Component-owned layout improves reuse

**Supporting evidence:** Intrinsic layout can protect a widget’s functional structure and reduce repeated implementation.

**Contradicting evidence:** Owning external spacing, width, orientation, or sibling relationships makes components context-aware. This pressure produces presentation modes and page-specific overrides.

**Confidence:** Low for contextual layout; high for intrinsic layout.

**Recommendation:** Components own internal relationships required for meaning or behavior. Parents own placement and relationships among components. External margins should normally be absent.

## A-007: Small components maximize composability

**Supporting evidence:** Small units can be independently reused and replaced.

**Contradicting evidence:** Tiny wrappers can obscure native HTML, increase nesting, distribute one interaction contract across many files, and force authors to understand hidden assembly rules. Slot-heavy systems can trade variant complexity for composition complexity.

**Confidence:** Moderate-low.

**Recommendation:** Boundaries should follow coherent semantics, state, accessibility, or change cadence—not line count or visual fragments.

## A-008: One highly flexible component is better than several specialized components

**Supporting evidence:** Shared implementation reduces duplication and may make behavior consistent.

**Contradicting evidence:** Legitimately different semantics become flags and modes. Attribute combinations become invalid or inaccessible. Consumers must learn an expanding configuration language. A generic “card” often conflates resource, action, metric, notification, and article concepts.

**Confidence:** Low.

**Recommendation:** Prefer semantic specialization when differences affect content model, interaction, or accessibility. Share lower-level behavior or styling mechanisms rather than forcing one public abstraction.

## A-009: Accessibility can be guaranteed by the library

**Supporting evidence:** Libraries can centralize tested keyboard behavior, roles, states, focus handling, forced-colors support, target sizes, and error-announcement mechanisms.

**Contradicting evidence:** Consumers supply labels, hierarchy, instructions, ordering, context, and content. They can misuse valid components or create inaccessible composition. Visual reordering and theme choices can break accessibility outside the component boundary.

**Confidence:** Falsified as a complete guarantee.

**Recommendation:** Define a shared accessibility contract. The library guarantees intrinsic behavior and prevents invalid states where possible. Consumers retain explicit obligations, enforced through documentation, tests, and diagnostics.

## A-010: Themes should be able to change every visual property

**Supporting evidence:** Broad theming supports brand and experimentation.

**Contradicting evidence:** Unlimited theme control destroys component invariants, creates unstable APIs, and can undermine accessibility. Some geometry, hit areas, focus treatments, and state distinctions are functional constraints rather than branding choices.

**Confidence:** Low.

**Recommendation:** Define protected invariants, themeable decisions, and composition-level decisions separately. A theme is not permission to violate usability requirements.

## A-011: Cross-context reuse should include marketing, applications, dashboards, and reports

**Supporting evidence:** Shared primitives and behaviors can reduce duplication across these contexts.

**Contradicting evidence:** These contexts often have different content models, density, narrative order, interaction expectations, and performance constraints. Universal reuse can turn into mode accumulation.

**Confidence:** Moderate for foundations and behaviors; low for large composites.

**Recommendation:** Expect reuse breadth to decrease as semantic and behavioral specificity increases. Foundations may be universal; complex composites should have declared context boundaries.

## A-012: Server rendering and no-JavaScript operation can be deferred

**Supporting evidence:** Some applications require JavaScript by definition.

**Contradicting evidence:** Upgrade delay, slow loading, failure, indexing, print, and content extraction expose architecture choices early. Declarative Shadow DOM improves SSR but does not erase the need to define pre-upgrade behavior.

**Confidence:** Low as a general assumption.

**Recommendation:** Define pre-upgrade, server-rendered, and failed-upgrade behavior before stable component implementation.

------------------------------------------------------------------------

# Architectural Comparisons

## Native HTML versus custom elements

### Native HTML advantages

- built-in semantics and accessibility mappings,
- keyboard and pointer behavior,
- form submission and validation,
- mobile and assistive-technology integration,
- browser optimization,
- graceful operation without JavaScript,
- lower authoring and testing burden.

### Native HTML limits

- inconsistent or constrained styling,
- missing complex patterns,
- limited component lifecycle,
- historical browser inconsistencies,
- no domain-specific semantic vocabulary.

### Custom element advantages

- explicit lifecycle and upgrade behavior,
- reusable browser-level API,
- state and behavior ownership,
- optional encapsulation,
- cross-framework distribution potential,
- domain-specific element vocabulary.

### Custom element costs

- naming and registration constraints,
- asynchronous upgrade,
- form participation and accessible-state work,
- event/property/attribute design burden,
- SSR and hydration decisions,
- wrapper temptation,
- testing across browser and framework boundaries.

### Conclusion

Native HTML is the foundation. Custom elements are an escalation used when native semantics plus behavior modules cannot provide the required durable boundary.

## Light DOM versus Shadow DOM

### Light DOM is favored when

- content is meaningful outside the component,
- authors need normal document semantics,
- visual hierarchy will undergo substantial experimentation,
- global typography and content styling should apply,
- page CSS must compose or query meaningful descendants,
- server output should remain directly understandable,
- consumer-provided content relationships are central.

### Shadow DOM is favored when

- internal DOM is implementation detail,
- accidental host CSS is a serious operational risk,
- a widget has strict interaction and layout invariants,
- internal replacement should not affect consumers,
- a small and intentional styling API can be defined,
- slot composition remains limited and understandable.

### Hybrid rule

Encapsulation should follow the source of volatility. Hide implementation detail that should change independently; expose semantic content that should participate in the surrounding document.

## Slots versus structured children

### Slots

Slots are useful when consumers provide content but should not know internal placement mechanics. They become costly when there are many named slots, strict ordering rules, conditional combinations, or semantics spanning shadow boundaries.

### Structured children

Explicit semantic children keep content relationships visible and styleable. They create tighter knowledge of the public content model and may require validation.

### Configuration

Properties and JSON-like configuration can simplify data-driven widgets but hide semantics from server HTML and can reduce progressive enhancement. Configuration is most defensible for data, not document content.

### Conclusion

Use ordinary children for document-like semantic content, limited slots for bounded extension points, and configuration for non-document widget data. Avoid turning slots into a templating language.

## Variants versus composition

A variant is justified when the same semantic object and interaction has a small number of legitimate, mutually understood states. Composition is preferred when differences arise from surrounding context or arrangement. Separate components are preferred when the content model or interaction meaning differs.

A practical test:

- If changing the option changes what the thing **means**, split it.
- If it changes how siblings are **arranged**, compose it.
- If it changes a bounded presentation while meaning and behavior remain stable, a variant or CSS state may be justified.
- If it exists only for one page, keep it local.

## Utility CSS versus semantic CSS

Utility CSS provides explicit local control, low selector specificity, and predictable composition. It can also scatter design decisions through markup and make broad redesign dependent on stable utility semantics.

Semantic CSS centralizes intent and allows theme-level reinterpretation, but can create large selectors and hidden behavior when class names become vague.

The defensible foundation is mixed:

- low-level utilities for exceptional composition and explicit constraints,
- semantic tokens for repeated design meaning,
- component-local selectors for intrinsic structure,
- page compositions for contextual layout,
- cascade layers to make ownership and override order explicit.

## Global CSS versus component CSS

### Global CSS should own

- reset and normalization,
- typography inheritance and document defaults,
- user-preference media queries,
- broadly shared semantic tokens,
- composition primitives,
- utilities,
- print and forced-colors foundations.

### Component CSS should own

- intrinsic state visualization,
- local structure required by behavior,
- protected minimum interaction geometry,
- styling of implementation details,
- documented parts or contextual variables.

### Page CSS should own

- placement,
- sibling relationships,
- page rhythm,
- region-specific hierarchy,
- exceptional product compositions.

------------------------------------------------------------------------

# Design-System Evidence

## Material

Material’s current token model maps component tokens to system tokens and system tokens to reference values. This supports structured indirection and scoped CSS custom properties. Its strength is consistency and broad tooling. Its limitation for this project is that Material is itself a coherent visual language; adopting its component assumptions can constrain radically different composition unless the underlying architecture is separated from Material-specific decisions.

## Carbon

Carbon explicitly separates foundations, guidelines, components, and implementation. Its contribution checklist requires token use, globalization support, and avoidance of un-tokenized magic values. Carbon’s migrations also show that token systems are not free of breaking change: renaming or restructuring tokens can require broad asset and code changes even when the visual design remains similar.

## Spectrum

Spectrum describes tokens as design decisions translated into data and uses them to maintain cohesion. Spectrum’s value is its explicit treatment of design decisions as shared artifacts across tools and platforms. Its risk is similar to other mature systems: cohesive product identity and universal redesignability are not identical goals.

## FAST

FAST demonstrates a token-driven, Web Component-oriented model in which tokens can cascade through the DOM. This strongly supports contextual design decisions and local themes. It also illustrates how a token system can become deeply coupled to component implementation if boundaries are not controlled.

## Shoelace and similar Web Component libraries

Libraries using Shadow DOM, CSS custom properties, slots, and parts demonstrate that highly distributable components are practical. They also reveal the central trade: strong encapsulation requires an explicit styling surface, and consumers inevitably request more access when their design needs exceed that surface.

## Open Props and utility-oriented systems

Open Props and utility philosophies show the value of reusable low-level decisions without requiring a full component catalog. They are strong foundations for experimentation but do not independently define semantics, interaction contracts, or component boundaries.

## Cross-system pattern

The recurring durable pattern is not a particular library technology. It is layered responsibility:

- foundations,
- named design decisions,
- accessible primitives,
- reusable behavior,
- compositional patterns,
- product-specific assemblies,
- documentation and governance.

Systems become rigid when these layers collapse into component variants or when one visual language is mistaken for universal semantics.

------------------------------------------------------------------------

# Accessibility Foundation

## Native-first rule

The HTML standard defines native form-associated elements and also provides form-associated custom elements. The latter capability does not make custom controls equivalent in cost. Native controls begin with browser semantics and behavior; custom controls begin with obligations.

## Component obligations

A stable behavioral component must define:

- semantic role or native element basis,
- accessible name mechanism,
- accessible description mechanism,
- keyboard interaction model,
- focus entry and exit behavior,
- state exposure,
- disabled and read-only behavior,
- error and status announcement behavior,
- high-contrast and forced-colors rendering,
- zoom and text-resize behavior,
- reduced-motion behavior,
- no-JavaScript behavior,
- localization and bidirectionality behavior.

## Consumer obligations

Consumers must provide:

- meaningful labels and instructions,
- correct heading and landmark context,
- logical DOM order,
- sufficient surrounding contrast and spacing,
- valid task flow,
- non-duplicative announcements,
- appropriate component selection,
- complete localized content.

## Architectural implication

Accessibility requirements must be represented as contracts and tests, not only documentation prose. However, automated tests cannot prove task comprehension, meaningful labels, sensible reading order, or usability. Manual and assistive-technology testing remains necessary.

## Visual reordering rule

Do not use CSS order as a routine mechanism to create a visual sequence that disagrees with reading or focus order. When visual hierarchy requires a different semantic sequence, reconsider the DOM or the component boundary. Stable markup is subordinate to coherent interaction.

------------------------------------------------------------------------

# Styling and Token Foundation

## Token hierarchy

The research supports a layered model, but implementation names remain intentionally undecided.

### Reference values

Raw palettes, dimensions, type scales, motion values, and other source material. These are implementation inputs, not recommended application APIs.

### System or semantic decisions

Values named by purpose: text, surface, border, focus, spacing relationship, content measure, interaction state, and similar concepts.

### Context decisions

Values that change according to environment: density, brand, color scheme, accessibility mode, product region, content type, or input modality.

### Component decisions

Only values representing legitimate public variation of a stable component. These should generally map to semantic/context values rather than raw values.

## Dependency rules

- Components should prefer semantic or contextual decisions over reference values.
- Themes may change reference and semantic mappings.
- Component decisions should not become a mirror of every CSS declaration.
- Product-specific tokens should not enter the global system until repeated cross-product use is demonstrated.
- Accessibility invariants may constrain or override theme decisions.
- Circular token references and context leakage must be detected automatically.

## Token admission test

A new token should require evidence that:

1. the decision repeats,
2. the decision has stable meaning,
3. consumers legitimately need to vary it,
4. exposing it will not bind them to internal structure,
5. a broader semantic token cannot serve the need,
6. its valid range and accessibility constraints are understood.

## Themes

Themes should be treated as coherent mappings of design decisions, not selector files that patch individual components. A theme that repeatedly targets component internals indicates one of four failures:

- missing semantic/context tokens,
- an overly rigid component,
- an invalid theme requirement,
- or a component that belongs to a narrower visual system.

Nested themes must be considered from the beginning because CSS custom properties naturally inherit across boundaries. This is powerful but can produce accidental leakage when values are incomplete or names are too generic.

------------------------------------------------------------------------

# Composition Foundation

## Intrinsic versus contextual layout

This distinction should become a governing architectural rule.

### Intrinsic layout

The spatial relationship is required for the component to preserve meaning or interaction. The component may own it.

Examples:

- relationship between a checkbox and its visible label,
- internal alignment of an icon and control text,
- placement of a dialog’s focusable region relative to its modal surface,
- association of a table header with its data structure.

### Contextual layout

The relationship depends on where the component is used. The parent should own it.

Examples:

- whether a card is in a grid or list,
- spacing between sections,
- sidebar width,
- whether a navigation region is horizontal or vertical because of page layout,
- prominence relative to neighboring content.

## External margin rule

Shared components should normally avoid owning outer margins. Margin is a relationship between objects and therefore belongs to composition. Internal padding may be component-owned when it is part of the component’s bounded surface.

## Container response

Components should respond primarily to content pressure and container context rather than only viewport width. Container queries are appropriate for component-internal adaptation, but explicit content constraints and logical properties remain necessary. Viewport queries still belong at page and application shell levels.

## Fragmentation warning

Composition has gone too far when:

- authors must assemble many wrappers in a strict undocumented order,
- one accessible pattern is distributed across several independently optional pieces,
- components exist only to attach one class,
- normal HTML becomes difficult to recognize,
- replacement requires understanding hidden parent-child protocols.

------------------------------------------------------------------------

# Maintainability Model

## Flexibility is not free

Every variation mechanism creates an API, even when it is expressed as CSS. Attributes, slots, parts, custom properties, utility classes, child selectors, inherited tokens, and context selectors are all coupling surfaces.

A component is not more flexible simply because it exposes more of them. It may merely transfer responsibility to consumers.

## Complexity indicators

Before implementation, the library should commit to measuring:

- public attributes, properties, events, methods, slots, and parts,
- exposed custom properties,
- theme-specific component overrides,
- parent or ancestor selectors required by a component,
- invalid public-state combinations,
- component-specific responsive breakpoints,
- consumer markup changes during redesign,
- consumer changes during component replacement,
- accessibility exceptions,
- CSS specificity distribution,
- `!important` usage,
- duplicate patterns across components,
- JavaScript required before useful rendering,
- and documentation steps required for correct use.

## Split signals

A component should be considered for splitting when:

- modes change its content model,
- keyboard behavior differs by mode,
- required slots vary substantially,
- many attribute combinations are invalid,
- themes repeatedly target different internal structures,
- consumers use only disjoint subsets of its API,
- its name describes appearance rather than purpose.

## Merge signals

Separate components should be considered for unification when:

- semantics and keyboard behavior are identical,
- differences are purely visual and bounded,
- duplicated implementations repeatedly receive the same fixes,
- replacement and authoring are simpler through one abstraction,
- configuration does not create contradictory states.

## Replacement test

A library should optimize not only reuse but removal. Consumers should depend on documented semantics and public behavior, not undocumented descendants, timing, or theme internals. Replacement cost is a better measure of loose coupling than the number of contexts in which a component appears.

------------------------------------------------------------------------

# Failed Hypotheses and Dangerous Directions

## FH-001: Generic card as a foundational component

**Why it seems reasonable:** Cards are visually common and appear across product types.

**Why it fails:** “Card” describes a surface treatment, not a single semantic object. Article previews, metrics, actions, alerts, products, and evidence records have different content and interaction models. A generic card tends to collect slots and visual modes.

**Learning:** Keep card-like surface treatment as a composition or styling primitive until a stable semantic object is identified.

## FH-002: Encapsulate all internals to guarantee safety

**Why it seems reasonable:** External CSS and DOM mutation can break components.

**Why it fails:** The system’s primary objective includes radical visual experimentation. Encapsulation can force every future styling need through parts, properties, or tokens and can hide meaningful document structure.

**Learning:** Protect implementation details selectively; do not hide semantic content merely for uniformity.

## FH-003: Expose a custom property for every style decision

**Why it seems reasonable:** Consumers can redesign without forking.

**Why it fails:** The variable set becomes an undocumented parallel CSS API tied to internal structure. Invalid combinations and accessibility regressions become consumer responsibility.

**Learning:** Expose decisions, not declarations.

## FH-004: One component plus enough variants avoids duplication

**Why it seems reasonable:** Shared code appears easier to maintain.

**Why it fails:** Variants interact combinatorially and conceal semantic differences. Internal code becomes a conditional renderer; documentation becomes a configuration matrix.

**Learning:** Duplication can be cheaper than false unification. Share behavior beneath separate semantic components when necessary.

## FH-005: Stable DOM is an absolute success criterion

**Why it seems reasonable:** Zero markup changes imply maximum redesignability.

**Why it fails:** It can incentivize inaccessible CSS reordering or overly generic markup. A new design may reveal that the original information architecture was wrong.

**Learning:** Preserve meaning and contracts, not accidental structure.

## FH-006: Design-system consistency is always beneficial

**Why it seems reasonable:** Consistency reduces cognitive and maintenance costs.

**Why it fails:** Over-standardization can prevent product-appropriate hierarchy, experimentation, and domain expression. Familiarity and consistency have value, but they are not substitutes for task fit.

**Learning:** The library should provide coherent defaults and constraints while allowing controlled divergence at composition and theme layers.

------------------------------------------------------------------------

# Evidence Registry

## EVD-001 — Custom elements are a platform-defined extension mechanism

**Source:** WHATWG HTML Standard, Custom Elements.

**Observation:** Custom elements allow authors to define fully featured DOM elements with construction and lifecycle behavior.

**Supports:** Selective use of custom elements for durable behavioral boundaries.

**Does not support:** Making every reusable pattern a custom element.

**Confidence:** High.

**Limitations:** A specification defines capability, not architectural suitability.

## EVD-002 — Form-associated custom elements exist but require deliberate implementation

**Source:** WHATWG HTML Standard, form-associated custom elements and `ElementInternals`.

**Observation:** The platform provides mechanisms for custom elements to participate in forms and expose default accessible roles and states.

**Supports:** Complex custom controls are technically possible.

**Challenges:** Claims that custom controls are cheap substitutes for native controls.

**Confidence:** High.

## EVD-003 — Native-control replacements commonly lose reliability and accessibility

**Source:** Open UI customizable select, combobox, and enhanced range research.

**Observation:** Open UI identifies reduced performance, reliability, and accessibility as common consequences of custom replacements motivated by styling limitations.

**Supports:** Native-first escalation rule.

**Confidence:** High.

**Limitations:** Some specialized requirements still justify custom controls.

## EVD-004 — Declarative Shadow DOM improves server rendering

**Source:** HTML Standard and web.dev documentation.

**Observation:** Shadow roots can be represented declaratively in HTML and are interoperable across current major browsers.

**Supports:** SSR is no longer a categorical reason to reject Shadow DOM.

**Challenges:** Claims that Declarative Shadow DOM removes all hydration, accessibility, or authoring concerns.

**Confidence:** High.

## EVD-005 — Shadow DOM intentionally creates a style and DOM boundary

**Source:** DOM/HTML standards and MDN platform documentation.

**Observation:** Shadow DOM shields internal markup and styling from ordinary page CSS and JavaScript access.

**Supports:** Implementation protection for bounded widgets.

**Challenges:** Universal global styling and open composition.

**Confidence:** High.

## EVD-006 — Cross-root ID relationships have explicit constraints

**Source:** WAI ACT rule material and HTML reflection mechanisms.

**Observation:** ID-reference resolution and accessible relationships interact with tree boundaries and require deliberate APIs.

**Supports:** Caution around content-rich Shadow DOM components and labeling across roots.

**Confidence:** Moderate-high.

## EVD-007 — WCAG requirements include behavior beyond component appearance

**Source:** WCAG 2.2 quick reference.

**Observation:** Accessibility covers keyboard access, focus, contrast, reflow, text resizing, motion, names, roles, status messages, and more.

**Supports:** Shared library-consumer accessibility contract and multi-condition testing.

**Confidence:** High.

## EVD-008 — Tokens are treated as reusable design decisions across mature systems

**Source:** Material, Spectrum, Carbon, FAST documentation.

**Observation:** Independent mature systems use token indirection for color, typography, spacing, motion, and component decisions.

**Supports:** A layered token foundation.

**Confidence:** High.

## EVD-009 — Token migrations can create broad blast radius

**Source:** Carbon migration documentation.

**Observation:** Token naming and architecture changes can require updates across design assets and code even without a visual redesign.

**Supports:** Treating token names and dependency layers as public architecture.

**Confidence:** High.

## EVD-010 — Themes can be locally scoped

**Source:** Material Web theming and Carbon theme documentation.

**Observation:** CSS custom-property and provider approaches can theme page regions rather than only entire applications.

**Supports:** Contextual and nested themes.

**Challenges:** Assumption that inheritance automatically prevents leakage or incomplete theme state.

**Confidence:** High.

## EVD-011 — Open UI decomposes common controls into conceptual parts before standardization

**Source:** Open UI tabs and other control research.

**Observation:** Reusable control work begins by identifying anatomy, semantics, states, and interaction across existing systems rather than by selecting a component API immediately.

**Supports:** Researching component boundaries and content models before naming APIs.

**Confidence:** High.

## EVD-012 — Production systems distinguish foundations from components

**Source:** Carbon, Spectrum, Material, and FAST documentation.

**Observation:** Mature systems contain tokens, guidelines, patterns, foundations, and tools in addition to coded components.

**Supports:** Treating the future library as a layered system rather than a catalog of custom elements.

**Confidence:** High.

------------------------------------------------------------------------

# Candidate Architectural Laws

These are provisional laws to be tested during pre-implementation validation and early construction.

## LAW-001 — Native Capability Conservation

### Hypothesis

A library should preserve native semantics and behavior unless replacing them delivers measurable value that cannot be achieved through styling or progressive enhancement.

### Prediction

Native-first implementations will require fewer keyboard, accessibility, form, mobile, and failure-mode exceptions than custom replacements.

### Counterevidence that would weaken it

A native element consistently cannot meet core product requirements, while a custom implementation demonstrates equivalent accessibility and lower total complexity across supported contexts.

### Confidence

High.

## LAW-002 — Semantic Stability Boundary

### Hypothesis

Markup remains durable only to the extent that it encodes stable information relationships rather than current visual hierarchy.

### Prediction

Domain-named structures will survive redesign better than appearance-named structures, but will still change when reading order or interaction meaning changes.

### Confidence

Moderate-high.

## LAW-003 — Intrinsic Layout Ownership

### Hypothesis

A reusable component should own only layout intrinsic to its semantics or behavior; contextual arrangement should belong to parent compositions.

### Prediction

Following this rule will reduce orientation variants, external margins, ancestor knowledge, and page-specific overrides.

### Confidence

High.

## LAW-004 — Selective Encapsulation

### Hypothesis

Encapsulation produces net value when the protected implementation is more volatile than the public styling and composition requirements.

### Prediction

Bounded behavioral widgets will benefit from Shadow DOM more often than content-rich semantic composites.

### Confidence

Moderate.

## LAW-005 — Variation Surface Conservation

### Hypothesis

Every variation mechanism is a public coupling surface and should be introduced only after repeated concrete pressure.

### Prediction

Components governed by an admission rule for attributes, slots, parts, and custom properties will show lower redesign and replacement blast radius.

### Confidence

Moderate-high.

## LAW-006 — Accessibility Contract Distribution

### Hypothesis

Accessibility is most durable when intrinsic obligations are owned by the library and contextual obligations are explicit, testable consumer responsibilities.

### Prediction

Attempting to hide all accessibility inside components will either fail or create rigid APIs that cannot fit real contexts.

### Confidence

High.

## LAW-007 — Reuse Specificity Gradient

### Hypothesis

Potential reuse breadth decreases as semantic, behavioral, and contextual specificity increases.

### Prediction

Foundations and basic behaviors will transfer broadly; large domain composites will require narrower declared scope.

### Confidence

Moderate-high.

------------------------------------------------------------------------

# Required Pre-Implementation Research Experiments

No production components should be written during these experiments. Throwaway prototypes may be used only as measuring instruments after the research-only phase is formally closed.

## EXP-001 — Semantic durability mapping

**Question:** Which content relationships remain stable across the project’s actual visual systems?

**Method:** Select three real domain objects, such as evidence, recommendation, and metric. Model their information without styling. Compare the required reading order and optional regions across editorial, dashboard, mobile, report, and low-vision contexts.

**Measure:** Stable fields, conflicting order, context-specific fields, interaction differences, and any visual-only groupings.

**Failure threshold:** A proposed shared model requires visual reordering that conflicts with reading/focus order or contains many context-only fields.

**Expected information gain:** Very high.

## EXP-002 — Native capability inventory

**Question:** Which anticipated library items are already adequately supported by native HTML and current platform features?

**Method:** For every proposed catalog item, document native semantics, current browser support, accessibility behavior, styling limits, and missing product requirements.

**Measure:** Native fit, enhancement needed, custom behavior needed, and replacement cost.

**Expected information gain:** Very high.

## EXP-003 — Encapsulation decision matrix

**Question:** Which categories justify light DOM, Shadow DOM, or behavior-only enhancement?

**Method:** Evaluate representative categories against styling openness, semantic visibility, internal invariants, SSR, accessible relationships, testing, and replacement.

**Failure threshold:** Any universal rule that produces serious costs in more than one category.

**Expected information gain:** High.

## EXP-004 — Token dependency simulation

**Question:** Can proposed token layers support multiple visual systems without component-level exception growth?

**Method:** Model design decisions abstractly across four contrasting systems without writing production CSS. Track which decisions are global, semantic, contextual, component-specific, or composition-specific.

**Measure:** Number of component exceptions, ambiguous token meanings, dependency cycles, and values that violate accessibility constraints.

**Expected information gain:** High.

## EXP-005 — Accessibility responsibility matrix

**Question:** Which accessibility obligations can the library guarantee, and which necessarily remain contextual?

**Method:** Map WCAG and APG obligations across representative controls and composites. Identify required author inputs and invalid uses.

**Measure:** Intrinsic guarantees, required consumer responsibilities, enforceable constraints, and manual-test obligations.

**Expected information gain:** High.

## EXP-006 — Cross-framework authoring study

**Question:** Does the same component contract remain understandable and reliable in plain HTML, server templates, React, Vue, and another likely consumer?

**Method:** Analyze event, property, boolean attribute, children, form, SSR, and styling conventions. No production implementation required.

**Measure:** Adapter requirements, semantic differences, typing gaps, hydration concerns, and documentation burden.

**Expected information gain:** Moderate-high.

## EXP-007 — Governance and evolution study

**Question:** What process prevents experiments and accidental APIs from becoming permanent?

**Method:** Compare maturity stages, deprecation processes, decision logs, and migration practices in long-lived systems.

**Measure:** Required evidence for promotion, reversal conditions, migration obligations, and ownership model.

**Expected information gain:** Moderate-high.

------------------------------------------------------------------------

# Research-to-Implementation Gate

Implementation may begin when all of the following are true:

1. Every anticipated first-release item has a provisional classification.
2. Native capabilities and gaps have been documented.
3. The DOM/encapsulation strategy is category-specific rather than universal.
4. Accessibility responsibilities are divided between library and consumers.
5. The token dependency model and invariants are documented.
6. Layout ownership rules are explicit.
7. Server-rendered, pre-upgrade, failed-upgrade, and no-JavaScript expectations are defined.
8. At least three real domain structures have undergone semantic durability analysis.
9. Theme goals distinguish token substitution from composition changes.
10. Component admission, maturity, deprecation, and reversal processes are defined.
11. Remaining uncertainties are known and accepted rather than hidden.

Passing the gate does not mean the architecture is proven. It means the largest avoidable foundational mistakes have been addressed well enough for incremental implementation and empirical testing.

------------------------------------------------------------------------

# Recommended Foundation

## Architectural stance

Adopt a **hybrid architecture with native HTML at the base and selective Web Components above it**.

## Component boundaries

Create a shared abstraction only when it owns at least one durable unit of:

- semantics,
- behavior or state,
- accessibility interaction,
- validation,
- lifecycle,
- or repeated composition with demonstrated cross-context stability.

Appearance alone is insufficient.

## DOM strategy

- Native and styled native elements remain ordinary DOM.
- Content-rich semantic composites default to light DOM.
- Bounded behavioral widgets may use open Shadow DOM.
- Closed Shadow DOM is rejected for the general library.
- Declarative Shadow DOM should be evaluated where server rendering of shadow components is required.
- Slots remain limited, documented extension points rather than a general template language.

## Styling strategy

- CSS owns presentation.
- Parent compositions own contextual layout.
- Components own intrinsic layout and state visualization.
- Low-specificity selectors and cascade layers should make ownership explicit.
- Logical properties, user preferences, forced colors, print, and content resizing are foundational requirements.
- Themes alter coherent semantic/context mappings; they should not patch internal selectors broadly.

## Token strategy

Use a layered dependency model with explicit admission rules. Preserve accessibility and interaction invariants outside ordinary theme discretion. Treat token names and layer boundaries as versioned public architecture.

## Accessibility strategy

Native-first, progressive enhancement, explicit keyboard models, tested pre-upgrade behavior, shared responsibility contracts, and mandatory manual validation for complex interactions.

## Composition strategy

Distinguish intrinsic from contextual layout. Favor ordinary CSS and recognizable HTML over wrapper proliferation. Promote repeated compositions only after evidence of stable reuse.

## Governance strategy

Every abstraction should begin experimental. Promotion requires evidence from multiple contexts, hostile content, accessibility review, replacement analysis, and documented reversal conditions. Failed abstractions remain recorded.

------------------------------------------------------------------------

# Remaining Unknowns

The following cannot be resolved confidently without project-specific evidence:

1. Which exact domain composites are stable enough to enter the shared library.
2. How radical the desired visual systems will be in structural terms.
3. Which frameworks, rendering environments, and browser support ranges are mandatory.
4. Whether form-associated custom elements are needed in the first releases.
5. How much SSR and no-JavaScript functionality each product requires.
6. Whether nested themes are a common production need or primarily an experiment need.
7. How authoring complexity should be measured with the actual team.
8. Which accessibility testing environments and assistive technologies are operationally supportable.
9. How iconography, data visualization, and rich text should fit the encapsulation model.
10. What publication and versioning model will be used across packages and products.

------------------------------------------------------------------------

# Recommended Next Research Areas

Ranked by expected information gain and architectural risk:

| Rank | Research area | Information gain | Risk addressed | Effort | Urgency |
|---:|---|---|---|---|---|
| 1 | Semantic durability of real domain objects | Very high | Wrong content model and rigid markup | Medium | Immediate |
| 2 | Native capability and control inventory | Very high | Unnecessary custom controls and accessibility debt | Medium | Immediate |
| 3 | Encapsulation matrix by category | High | Styling lock-in and hidden coupling | Medium | Immediate |
| 4 | Accessibility responsibility matrix | High | Incomplete contracts and audit failures | Medium | Immediate |
| 5 | Token dependency and theme-boundary study | High | Token explosion and theme leakage | Medium | Immediate |
| 6 | Framework/SSR interoperability constraints | Moderate-high | Consumer adapters and hydration failure | Medium | Before API work |
| 7 | Governance and maturity model | Moderate-high | Premature standardization | Low-medium | Before repository creation |
| 8 | Performance budget and measurement strategy | Moderate | Large-list and upgrade cost | Medium | Before implementation benchmarks |
| 9 | Design-theory encoding rules | Moderate | Embedding temporary visual theories in APIs | High | Parallel research |
| 10 | Long-term migration case studies | Moderate | Versioning and replacement debt | High | Before stable release |

------------------------------------------------------------------------

# Final Recommendation

## Adopt with constraints: hybrid native-first foundation

The evidence is sufficient to reject a universal Web Component architecture and to support a hybrid model as the starting foundation.

The library should be built, but not as a catalog in which every documented object is a custom element. It should be a layered interface system in which native HTML remains visible, CSS owns presentation and contextual composition, behaviors are progressively enhanced where possible, semantic composites are admitted only after durability evidence, and Shadow DOM is selected deliberately for bounded implementation protection.

The largest remaining risk is not choosing the wrong JavaScript base class. It is defining the wrong semantic and ownership boundaries. Those boundaries determine whether later design learning can flow through CSS and composition or whether every insight becomes a new variant, slot, custom property, and exception.

Implementation should begin after the focused research gate above is completed. Once it begins, architecture research should continue alongside construction, with each early component treated as a falsifiable hypothesis rather than a permanent standard.

------------------------------------------------------------------------

# References

1. WHATWG HTML Standard — Custom Elements: https://html.spec.whatwg.org/multipage/custom-elements.html
2. WHATWG HTML Standard — Creating a form-associated custom element: https://html.spec.whatwg.org/dev/custom-elements.html
3. WHATWG HTML Standard — Scripting, templates, and slots: https://html.spec.whatwg.org/dev/scripting.html
4. W3C WCAG 2.2 Quick Reference: https://www.w3.org/WAI/WCAG22/quickref/
5. W3C TAG — Guidelines for creating web-platform-compatible components: https://www.w3.org/2001/tag/doc/webcomponents-design-guidelines/
6. Open UI — Customizable Select: https://open-ui.org/components/customizable-select.explainer/
7. Open UI — Combobox: https://open-ui.org/components/combobox.explainer/
8. Open UI — Enhanced Range Input: https://open-ui.org/components/enhanced-range-input.explainer/
9. Open UI — Tabs anatomy research: https://open-ui.org/components/tabs.research.parts/
10. Open UI — Popover research: https://open-ui.org/components/popover.research.explainer/
11. web.dev — Declarative Shadow DOM: https://web.dev/articles/declarative-shadow-dom
12. Material Web — Theming: https://material-web.dev/theming/material-theming/
13. Material Design 3 — Design Tokens: https://m3.material.io/foundations/design-tokens
14. IBM Carbon Design System: https://carbondesignsystem.com/
15. Carbon Component Checklist: https://carbondesignsystem.com/contributing/component-checklist/
16. Carbon Migration FAQ: https://carbondesignsystem.com/migrating/faq/
17. Adobe Spectrum — Design Tokens: https://spectrum.adobe.com/page/design-tokens/
18. FAST — Design Systems Overview: https://fast.design/docs/1.x/design-systems/overview/
19. Design Tokens Community Group: https://design-tokens.github.io/community-group/format/
20. MDN — Web Components: https://developer.mozilla.org/en-US/docs/Web/API/Web_components
21. MDN — Using Shadow DOM: https://developer.mozilla.org/en-US/docs/Web/API/Web_components/Using_shadow_DOM

------------------------------------------------------------------------

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 0.1 | 2026-07-19 | OpenAI Research Agent | Initial research baseline, assumption registry, candidate laws, evidence registry, architectural recommendation, and pre-implementation research gate. |
