---
id: RR-VE-PG-2026-130B
authors:
- Kevin Miller
- OpenAI
confidence: Moderate
date: 2026-07-19
llm_ingest: true
machine_readable: true
project: product-genome
purpose: |
  Conduct the first autonomous evidence-driven investigation of the Product Genome branch of Project Atlas. The report tests whether recurring ideas associated with Dieter Rams, Richard Sapper, affordance theory, physical feedback, simplicity, repairability, and emotional durability can be converted into defensible and predictive product-design principles.
references:
- Product Genome: Project Atlas Research Framework v1.0
- Dieter Rams
- Richard Sapper
- James J. Gibson
- Donald Norman
- FDA human factors guidance
- NHTSA human factors guidance
- EN 45554:2020
status: active
summary: |
  This research run rejects minimalism as a universal product-design law and replaces it with a stronger model: products should minimize unnecessary interaction cost while preserving the complexity required by capability, safety, expertise, and context. The most defensible cross-domain principles concern compatibility between controls and user expectations, perceptible action possibilities, closed feedback loops, biomechanical fit, error-resistant constraints, and support for maintenance over time. Rams and Sapper are best understood not as opposing aesthetics but as different strategies for managing complexity. Rams tends to externalize order and suppress nonessential signals; Sapper often internalizes technical complexity while preserving direct manipulation and mechanical intelligibility. Repairability and emotional durability appear related to product lifetime, but current evidence does not justify treating either as independently sufficient. The report proposes a Product Legibility Model and a Complexity Allocation Model for future testing.
version: 1.0
purposes:
  - orient
  - integrate
  - verify
audiences:
  - practitioner
  - researcher
---

# Product Genome Autonomous Research Report: Run 01

## Purpose

This report moves the Product Genome from a conceptual framework into an evidence-driven research program.

The initial framework proposed that product design could be studied through recurring variables such as affordance, feedback, mapping, constraint, ergonomics, materials, manufacturing, durability, repairability, and emotional attachment. It also suggested beginning with Dieter Rams and Richard Sapper as contrasting designer genomes.

The largest uncertainty was whether the principles commonly associated with these designers represent universal design laws, useful heuristics, historically contingent values, or merely admired stylistic preferences.

This investigation therefore focused on five questions:

1. Is “less, but better” supported as a universal design principle?
2. What is the strongest scientifically defensible interpretation of affordance?
3. When and why do physical controls and tactile feedback improve performance?
4. Do repairability and emotional attachment actually extend product lifetime?
5. Can Rams and Sapper be described through a shared analytical model rather than aesthetic labels?

------------------------------------------------------------------------

# Executive Summary

## What Was Accomplished

Five research cycles were completed.

The investigation compared historical design documentation, museum records, empirical research, human-factors guidance, engineering-design literature, automotive safety research, medical-device guidance, sustainability research, and repairability standards.

The research did not validate the simple claim that minimal products are inherently better. Instead, it identified a more precise principle:

> Product quality improves when unnecessary interaction cost is reduced without removing the capability, feedback, redundancy, or deliberate friction required by the task and context.

This distinction matters. A product may be visually minimal while remaining cognitively opaque, physically awkward, difficult to repair, or unsafe. Conversely, a visibly complex product can perform well when its complexity is structured, stable, differentiated, and matched to the user's expertise.

## Major Discoveries

### 1. Minimalism is an expression, not a universal law

Evidence on feature fatigue supports the claim that additional features can reduce usability and post-use satisfaction. It does not support the claim that fewer features are always preferable. Users often value capability before use and usability during use, creating a predictable tension. Expert tools, safety systems, and high-capability products may require substantial complexity.

The better target is **legible complexity**.

### 2. Affordance has been weakened by casual design usage

The original ecological concept is relational. An affordance exists between an organism, an object, and an environment. It includes actual bodily capability, not merely what an object looks like it can do.

A handle that visually suggests grasping but cannot be comfortably reached by the intended population is not a successful affordance. It is a signifier attached to a failed action possibility.

### 3. Feedback is valuable because it closes the control loop

Physical and tactile feedback can reduce dependence on vision, improve movement correction, confirm activation, and support operation under divided attention. However, tactile feedback is not automatically superior. Its benefit depends on latency, discriminability, task demands, user capability, and whether the feedback conveys meaningful state rather than decorative sensation.

### 4. Compatibility is more defensible than intuitiveness

Regulatory human-factors guidance repeatedly emphasizes consistency with user expectations, abilities, likely behavior, and prior experience. “Intuitive” is too vague because expectations are partly learned and can differ among populations.

A stronger term is **population-compatible behavior**.

### 5. Repairability is a system property

Repairability cannot be inferred from visible screws or modular appearance alone. It depends on disassembly, access, tools, documentation, spare parts, software restrictions, component pricing, and reassembly. EN 45554 formalizes this broader view.

### 6. Emotional attachment may extend life, but it is not directly designable

Attachment research suggests that memories, pleasure, usability, appearance, reliability, and repair convenience can influence replacement decisions. However, many of the strongest attachment factors arise from personal history and circumstance. Designers can create conditions that make attachment more likely, but cannot reliably manufacture attachment itself.

### 7. Rams and Sapper are not simply “simple versus complex”

Rams and Sapper both reduce user-facing disorder. Their difference is where complexity is placed.

- Rams frequently reduces the number and prominence of visible signals, regularizes arrangement, and creates stable product families.
- Sapper frequently preserves complex technical capability while translating it into balanced motion, transformation, counterweights, compact mechanisms, or direct manipulation.

The useful comparison is therefore not minimalism versus complexity. It is **complexity allocation**.

## Overall Confidence

**Moderate.**

Confidence is high for the human-factors principles involving compatibility, feedback, biomechanical fit, control placement, task analysis, and error reduction. Confidence is moderate for claims about feature fatigue and product attachment because effects vary by product type, expertise, timing, and research method. Confidence is preliminary for designer-genome scoring because only a small set of products and historical records were examined.

## Remaining Uncertainty

The most important unresolved issue is quantification. We can identify dimensions of product legibility, but do not yet know how to combine them into a reliable predictive score.

The second major uncertainty is transferability. Evidence from medical devices, vehicle interfaces, laboratory haptics, and consumer electronics does not automatically generalize to furniture, kitchen tools, toys, or architectural hardware.

------------------------------------------------------------------------

# Key Findings

- “Less” should be treated as removal of unnecessary burden, not reduction of visible parts or features by default.
- Product complexity has at least four distinct forms: functional, interactional, representational, and structural.
- Complexity can be relocated rather than eliminated.
- The core unit of usability is the human-object-environment relationship, not the object alone.
- Perceived action possibility and actual action possibility must be evaluated separately.
- Compatibility with learned expectation reduces error, but may preserve poor conventions.
- Feedback is most valuable when it is timely, discriminable, proportional, and tied to meaningful state change.
- Physical controls are especially useful where eyes-free operation, continuous adjustment, immediate confirmation, or spatial memory matter.
- Constraints are valuable when they prevent harmful action without blocking legitimate variation.
- Repairability requires an ecosystem, not merely disassembly-friendly geometry.
- Product longevity has technical, functional, economic, software, emotional, and cultural components.
- Designer philosophies should be modeled as weightings and allocation strategies rather than universal truths.

------------------------------------------------------------------------

# Research Log

## Cycle 1: Test Minimalism as a Universal Principle

### Objective

Determine whether the widespread interpretation of Dieter Rams' “less, but better” can be defended as a universal product-design law.

### Hypothesis

**HYP-001:** Reducing the number of product features and visible controls generally improves product quality.

### Evidence That Would Support the Hypothesis

- Products with fewer features consistently produce fewer errors and higher satisfaction.
- Reduced visible complexity predicts faster learning and operation across novice and expert users.
- Additional capability provides little compensating value.
- The result generalizes across low-risk and high-risk contexts.

### Evidence Found

Thompson, Hamilton, and Rust's feature-fatigue research found that consumers weigh capability heavily before use, but usability becomes more important after use. Products with extensive features can attract purchasers while producing frustration during operation. This supports a real cost of feature accumulation.

Rams' principles emphasize usefulness, understandability, unobtrusiveness, longevity, thoroughness, environmental responsibility, and “as little design as possible.” Historical accounts show that his minimalism was embedded in a broader ethical and functional program rather than being merely visual reduction.

FDA human-factors guidance states that redesigning the interface is generally more effective than relying on labeling or training. It emphasizes logical operation, prevention of harmful actions, compatibility with user expectations, and task analysis. This supports removal of unnecessary interpretive burden.

### Evidence Against

The feature-fatigue research does not conclude that fewer features are always better. It identifies a tension between desired capability and actual usability.

Sarkar's critique of simplicity in user-interface design argues that rigid simplicity can limit power and flexibility, particularly in feature-rich expert systems. While focused on software, the critique generalizes to physical professional tools.

Safety-critical devices may require redundancy, confirmation, protective guards, alarms, differentiated states, and deliberate action sequences. These additions increase visible or procedural complexity while reducing risk.

Rams' own principles include innovation, usefulness, thoroughness, and environmental responsibility. These can conflict with literal reduction. A repairable product may need visible fasteners, replaceable modules, diagnostic modes, or service access that a visually seamless product omits.

### Sources

- Thompson, D. V., Hamilton, R. W., and Rust, R. T. “Feature Fatigue: When Product Capabilities Become Too Much of a Good Thing.” *Journal of Marketing Research*, 42(4), 2005. DOI: 10.1509/jmkr.2005.42.4.431.
- Rams, D. historical principles and 1976 design speech, as preserved by the Design Museum and later archival publications.
- U.S. Food and Drug Administration. *Applying Human Factors and Usability Engineering to Medical Devices*, 2016.
- Sarkar, A. “Should Computers Be Easy to Use? Questioning the Doctrine of Simplicity in User Interface Design,” 2023.

### Analysis

The hypothesis confuses several different variables.

A product can have:

- many capabilities but few interaction steps;
- few capabilities but obscure interaction;
- many visible controls with clear one-to-one mappings;
- one multifunction control with hidden modes;
- a minimal exterior and inaccessible internal construction;
- complex construction with effortless operation.

The number of features is therefore an inadequate proxy for complexity.

At least four types of complexity must be separated:

1. **Functional complexity:** How many distinct things the product can do.
2. **Interactional complexity:** How much users must perceive, remember, decide, and execute.
3. **Representational complexity:** How many signals, controls, labels, modes, and states are visible or must be interpreted.
4. **Structural complexity:** How many components, dependencies, materials, and mechanisms make up the product.

These dimensions can move independently. A Sapper mechanism may have substantial structural complexity but low interactional complexity. A touchscreen may have low representational complexity at rest but high interactional complexity because controls are hidden behind modes.

### Attempted Falsification

A strong form of the minimalism hypothesis predicts that a single multifunction control should outperform several dedicated controls. Evidence from divided-attention contexts contradicts that prediction. Dedicated controls can support tactile differentiation, spatial memory, and eyes-free use.

A second prediction is that hidden construction should improve product quality by reducing visual noise. Repairability research contradicts this when hidden construction creates inaccessible components, destructive disassembly, or unavailable service information.

### Conclusion

**HYP-001 is rejected in its original form.**

A revised principle is retained:

> Remove complexity that does not contribute enough capability, safety, comprehension, durability, repair, or meaning to justify its interaction and lifecycle cost.

### Confidence

**High** that literal minimalism is not a universal law.

**Moderate** in the revised principle because the phrase “contribute enough” still requires measurable thresholds.

### Next Step

Clarify the unit being designed. Investigate whether affordance theory can connect visible form, bodily capability, action, and environment.

------------------------------------------------------------------------

## Cycle 2: Reconstruct Affordance as a Relational Concept

### Objective

Determine whether affordance can serve as a scientifically useful Product Genome variable rather than a vague synonym for “looks clickable” or “looks usable.”

### Hypothesis

**HYP-002:** Product usability improves when form directly communicates available actions.

### Evidence That Would Support the Hypothesis

- Users reliably identify intended actions from product form.
- Correctly perceived actions correspond to actions users can physically perform.
- Reduced need for symbolic interpretation improves task performance.
- Effects hold across different bodies and environments.

### Evidence Found

Gibson's ecological account defines affordances as action possibilities arising through the relationship between an organism and its environment. Affordances are not merely visual properties and do not depend on conscious recognition.

Masoudi, Fadel, and colleagues' review of affordance-based design argues that later design usage often omitted direct perception and biomechanics. Their review restores ergonomics and bodily capability as necessary components.

Experimental work on visual signifiers shows that signifiers can increase users' perception of available inputs. This demonstrates that communication of action possibility can be manipulated through visible form.

FDA guidance supports designing interface behavior around user abilities, expectations, and likely behavior rather than relying primarily on instructions.

### Evidence Against

A visible signifier can indicate an action that the product does not actually afford. A decorative groove may resemble a grip but provide insufficient clearance or leverage.

An object may afford unintended actions. A stool affords sitting, standing, carrying, and tripping. Therefore, making an intended action visible does not remove competing or hazardous affordances.

Direct perception is not always enough. Novel products, abstract systems, mode-dependent devices, and culturally specific conventions may require labels, training, or learned symbolic systems.

Affordance can vary by user. A control reachable by a tall, strong adult may not be reachable by a child, older adult, wheelchair user, or person with limited grip strength.

### Sources

- Gibson, J. J. *The Ecological Approach to Visual Perception*. 1979.
- Masoudi, N., Fadel, G. M., Pagano, C. C., and Elena, M. V. “A Review of Affordances and Affordance-Based Design to Address Usability.” *Proceedings of the Design Society: International Conference on Engineering Design*, 2019. DOI: 10.1017/dsi.2019.141.
- Mackamul, E. and colleagues. “Exploring Visual Signifier Characteristics to Improve the Perception of Affordances,” 2023.
- U.S. Food and Drug Administration. Human factors guidance, 2016.

### Analysis

The hypothesis is directionally correct but incomplete because it treats form as the sole carrier of interaction meaning.

Product interaction depends on at least five linked layers:

1. **Actual action possibility:** The action is physically possible for a specific user in a specific context.
2. **Perceived action possibility:** The user notices that some action may be possible.
3. **Interpreted action:** The user predicts what the action will do.
4. **Executed action:** The user can apply the required reach, force, precision, posture, and sequence.
5. **Outcome confirmation:** The product communicates whether the intended state change occurred.

A product can succeed at one layer and fail at another.

Example:

- A flush capacitive surface may physically accept touch.
- The user may not perceive the touch region.
- A printed icon may make it noticeable.
- The icon may still fail to explain whether the control toggles, holds, cycles, or opens a menu.
- The action may register without tactile or audible confirmation.

Calling all of this “affordance” hides the failure location.

### Attempted Falsification

The strongest version of direct affordance predicts that instruction-free products should be superior. This fails for products where action consequences are hidden, delayed, dangerous, or dependent on internal state.

The opposite theory, that learned signifiers are sufficient, also fails. A perfectly labeled control remains poor if it is unreachable, requires excessive force, or produces ambiguous feedback.

### Conclusion

**HYP-002 is revised.**

> Product usability improves when intended action possibilities are physically available to the target population, perceptually discoverable in context, correctly interpreted, executable within biomechanical limits, and followed by clear outcome feedback.

This becomes the basis of the **Product Legibility Model** proposed later in the report.

### Confidence

**High** in the relational model.

**Moderate** in its predictive power until its components are operationalized with metrics.

### Next Step

Investigate the feedback stage, particularly whether physical and tactile feedback produce measurable advantages over visually mediated controls.

------------------------------------------------------------------------

## Cycle 3: Test Physical Feedback and Dedicated Controls

### Objective

Determine when physical controls, tactile differentiation, and haptic feedback improve interaction.

### Hypothesis

**HYP-003:** Physical controls are generally more usable than touchscreens because they provide tactile feedback.

### Evidence That Would Support the Hypothesis

- Physical controls consistently reduce error and task time.
- Users can operate them with less visual attention.
- Tactile confirmation improves state awareness.
- Advantages remain across all task types and populations.

### Evidence Found

NHTSA guidance emphasizes that vehicle controls should be easy to find and reach, should be visible or locatable without vision, and should not interfere with the primary driving task. This supports the importance of spatial persistence and eyes-free operation.

NHTSA distraction research identifies visual-manual interaction as a major source of driver distraction. Interfaces that require looking away from the primary environment impose measurable risk.

Experimental research on vibrotactile feedback shows that augmenting vision with tactile information can reduce movement error in motor-learning tasks.

Research on prosthetic manipulation found that tactile feedback and reflexive grasp control improved consistency during tasks completed without direct vision.

FDA guidance emphasizes visible system state, compatibility, and interface designs that prevent or discourage harmful action.

### Evidence Against

A 2024 comparative study of touchscreen and physical-button tactile feedback found that touchscreen tactile feedback could produce more positive emotional valence under some conditions. This does not prove superior task performance, but it undermines the claim that mechanical buttons are universally preferred.

Physical controls have costs:

- fixed space consumption;
- limited adaptability;
- mechanical wear;
- sealing difficulty;
- added parts;
- reduced contextual flexibility;
- possible confusion when many similar controls are crowded together.

Touchscreens can provide large reconfigurable surfaces, context-sensitive controls, language localization, enlarged targets, progressive disclosure, and software updates.

Poor physical controls can be worse than well-designed touch controls. A small, identical, weakly detented button array provides little tactile benefit.

### Sources

- National Highway Traffic Safety Administration. *Human Factors Design Guidance for Driver-Vehicle Interfaces*, 2016.
- National Highway Traffic Safety Administration. Visual-manual distraction research and guidelines.
- Bark, K. and colleagues. “Effects of Vibrotactile Feedback on Human Learning of Arm Motions.” 2014.
- Thomas, N., Fazlollahi, F., Brown, J. D., and Kuchenbecker, K. J. Sensorimotor-inspired tactile feedback research, 2021.
- Huo, F. and colleagues. Comparative study of touchscreens and physical buttons, 2024.

### Analysis

“Physical versus touchscreen” is the wrong primary comparison. The more useful variables are:

- tactile discriminability;
- spatial persistence;
- eyes-free findability;
- control-response latency;
- movement continuity;
- mode visibility;
- target size;
- required precision;
- consequence severity;
- frequency of use;
- environmental vibration, moisture, gloves, or motion;
- need for reconfiguration.

Physical controls tend to have an advantage when users must locate and manipulate controls without continuous visual attention. They also support continuous adjustment through knobs, sliders, and levers, where position can directly encode state.

Touch interfaces tend to have an advantage when the number, language, arrangement, or relevance of controls changes frequently.

The important design issue is not whether the control is mechanical. It is whether the control creates a low-latency, comprehensible, closed-loop relationship between action and state.

### Attempted Falsification

The original hypothesis predicts that adding vibration to a touchscreen should never match a physical control. Existing evidence contradicts this. Engineered haptics can improve touch interaction and may be emotionally preferred in some contexts.

The inverse hypothesis, that touchscreens can fully replace physical controls, also fails under divided attention, gloves, motion, poor visibility, and safety-critical continuous control.

### Conclusion

**HYP-003 is rejected as a universal claim.**

A stronger conditional law is retained:

> Persistent, differentiated controls are favored when operation must occur under divided attention, without direct vision, with continuous adjustment, or with high consequence for selection error. Reconfigurable surfaces are favored when contextual flexibility outweighs the cost of visual dependence and hidden state.

### Confidence

**High** for the conditional distinction.

**Moderate** for product-level prediction because implementation quality can overwhelm control category.

### Next Step

Investigate product lifetime. Determine whether repairability and attachment can be treated as measurable design outcomes rather than ethical aspirations.

------------------------------------------------------------------------

## Cycle 4: Test Repairability and Emotional Durability

### Objective

Determine whether repairability and emotional attachment reliably extend product lifetime and whether designers can intentionally create them.

### Hypotheses

**HYP-004A:** Products that are easy to disassemble are repairable.

**HYP-004B:** Products that create emotional attachment will remain in use longer.

### Evidence That Would Support the Hypotheses

For HYP-004A:

- Disassembly time strongly predicts successful repair.
- Visible fasteners and modular construction are sufficient.
- Repair outcomes remain independent of parts, documentation, software, and price.

For HYP-004B:

- Attachment consistently delays replacement.
- Designers can intentionally produce attachment through specified product attributes.
- Attachment remains effective after technical failure or functional obsolescence.

### Evidence Found

EN 45554:2020 establishes general methods for assessing ability to repair, reuse, and upgrade energy-related products. Its existence confirms that repairability is measurable, but its criteria extend beyond geometry.

Repairability frameworks identify documentation, disassembly, spare-parts availability, tools, pricing, software barriers, and product-specific constraints as relevant.

Page's attachment research found memories to be the strongest attachment theme, followed by pleasure and usability. Appearance and reliability influenced replacement attitudes. Ease and convenience of repair were associated with longer product use in participant accounts.

Emotional-durability research identifies narrative, attachment, identity, sensory experience, and graceful aging as possible contributors to longer relationships with objects.

### Evidence Against

Easy disassembly is not sufficient when:

- replacement parts are unavailable;
- parts cost nearly as much as replacement;
- diagnostic information is inaccessible;
- software rejects replacement components;
- calibration tools are proprietary;
- reassembly cannot restore seals or safety certification;
- repair labor exceeds economic value.

Attachment is not fully controlled by the designer. Page's research found that memories and personal circumstances play a major role. These emerge through ownership rather than being embedded directly in form.

Attachment can also preserve inefficient, unsafe, or environmentally costly products. Longer ownership is not automatically a net sustainability benefit if energy consumption, emissions, or safety performance are substantially worse than replacement alternatives.

A product may be emotionally valued but no longer functionally useful. It may be retained but not used, which is different from lifetime extension in service.

### Sources

- CEN-CENELEC. EN 45554:2020, *General Methods for the Assessment of the Ability to Repair, Reuse and Upgrade Energy-Related Products*.
- Page, T. “Product Attachment and Replacement: Implications for Sustainable Design.” *International Journal of Sustainable Design*, 2(3), 2014.
- Haines-Gadd, M. and colleagues. “Emotional Durability Design Nine.” *Sustainability*, 10(6), 2018.
- Repairability assessment literature comparing standards and scoring methods.

### Analysis

Repairability is a **network property** spanning product architecture and the surrounding service system.

A useful repairability model requires at least:

1. Fault detection.
2. Fault localization.
3. Access.
4. Non-destructive disassembly.
5. Tool availability.
6. Part availability.
7. Part affordability.
8. Documentation.
9. Software and authentication permission.
10. Reassembly and restoration of performance.
11. Economic justification.
12. Safety and legal authorization.

Emotional durability is similarly systemic. A product may become meaningful through:

- reliable service;
- visible traces of use;
- repair history;
- sensory pleasure;
- personal customization;
- gifts and social association;
- rarity;
- identity expression;
- mastery and ritual.

Only some of these can be directly influenced by design.

### Attempted Falsification

HYP-004A predicts that a product held together with standard screws is repairable. This fails if paired components, unavailable parts, or adhesive-sealed subassemblies block the actual repair path.

HYP-004B predicts that emotionally evocative styling should reliably extend use. Existing evidence does not support this. Novel styling can age poorly, while quiet products can accumulate attachment through reliability and memory.

### Conclusion

**HYP-004A is rejected.** Easy disassembly is one necessary contributor, not a sufficient condition.

**HYP-004B is retained only in a weak conditional form.** Attachment may delay replacement, but it is partly emergent and cannot be reliably designed as a standalone feature.

Revised principles:

> Repairability is the probability that a fault can be diagnosed and corrected to an acceptable standard within available technical, legal, informational, and economic constraints.

> Designers can increase the probability of attachment by supporting reliability, sensory quality, personalization, repair, graceful aging, and meaningful continuity, but cannot guarantee attachment.

### Confidence

**High** for the repairability-system conclusion.

**Moderate-low** for emotional durability as a predictive design strategy.

### Next Step

Return to Rams and Sapper. Test whether their work can be modeled through complexity allocation, product legibility, and lifecycle support.

------------------------------------------------------------------------

## Cycle 5: Reframe the Rams-Sapper Comparison

### Objective

Replace superficial aesthetic comparison with a shared analytical model.

### Hypothesis

**HYP-005:** Rams represents simplicity and Sapper represents complexity.

### Evidence That Would Support the Hypothesis

- Rams products consistently contain fewer functions, mechanisms, and interactions.
- Sapper products consistently expose or require greater user complexity.
- Their products occupy opposite ends of a single complexity scale.

### Evidence Found

Rams' work at Braun and Vitsœ is consistently associated with ordered layouts, restrained signaling, product-family coherence, long service life, modularity in furniture systems, and reduction of decorative noise.

Sapper's Tizio lamp uses counterweights and conducts low-voltage current through the armature, eliminating visible wiring while allowing broad movement. The product contains sophisticated engineering but presents a direct manipulation model: move the lamp head and arms to the desired position.

Sapper's broader work includes compact, transformable, and mechanically expressive products. Museum documentation emphasizes engineering innovations such as reorganized internal components, strengthened bent sheet metal, plastic manufacturing, and transformable arrangements.

Both designers therefore worked to reduce user-facing disorder.

### Evidence Against

Rams products are not technically simple. Radios, audio systems, shavers, appliances, and modular furniture require substantial engineering and manufacturing coordination.

Sapper's products are not necessarily interactionally complex. Tizio translates an elaborate counterbalanced electrical structure into an easily understood physical adjustment.

Rams sometimes exposes controls clearly rather than hiding them. Sapper sometimes conceals mechanisms and wires. Neither designer can be placed consistently on a single “visible simplicity” axis.

### Sources

- Design Museum historical material on Dieter Rams and Braun.
- Museum of Modern Art collection documentation for Richard Sapper's Tizio lamp and other works.
- Museum of Modern Art exhibition documentation on Marco Zanuso and Richard Sapper.
- Historical interviews and designer statements.

### Analysis

The original contrast commits a category error. Complexity is not a single quantity.

The better comparison asks where each designer allows complexity to remain and where each suppresses it.

#### Rams tendency

- High regularity of visible composition.
- Stable visual hierarchy.
- Explicit but restrained controls.
- Product-family consistency.
- Low decorative entropy.
- Emphasis on longevity and compatibility with domestic environments.
- In furniture, modular extensibility rather than product replacement.

#### Sapper tendency

- Mechanical transformation.
- Hidden technical infrastructure.
- Direct manipulation of balanced systems.
- Compactness through structural ingenuity.
- Distinctive moments of motion, sound, or reveal.
- Use of mechanism as both function and character.

The distinction may be summarized as:

- Rams often creates calm through **external order**.
- Sapper often creates clarity through **mechanical translation**.

This is not absolute. It is a hypothesis about relative weighting.

### Attempted Falsification

If Rams were simply a reducer, his modular furniture systems would avoid expansion and reconfiguration. They do not. Their value depends on structured extensibility.

If Sapper favored complexity for its own sake, Tizio would expose its electrical and counterweight system as something the user must manage. Instead, the mechanism removes the need for clamping and visible wires while supporting direct movement.

### Conclusion

**HYP-005 is rejected.**

A revised designer-genome model is adopted:

> Designers differ partly in how they allocate necessary complexity among visible form, user action, internal mechanism, manufacturing, maintenance, and time.

Rams and Sapper should be compared through a multidimensional matrix, not a single simplicity score.

### Confidence

**Moderate.** The model explains the initial cases well, but requires a larger coded product sample.

### Next Step

Develop a repeatable case-study protocol and score at least 20 products per designer across multiple decades and categories.

------------------------------------------------------------------------

# Confirmed Findings

Only findings with strong support are included here.

## CF-001: Interface redesign generally has greater error-reduction potential than instructions alone

Human-factors guidance for medical devices explicitly prioritizes changes to device design over labeling or training when reducing use-related hazards. Training depends on memory and may be unavailable at the moment of use.

**Confidence:** High.

## CF-002: Control behavior that violates established user expectations increases use-error risk

FDA guidance uses directional control behavior as a concrete example. Compatibility is therefore a safety variable, not merely an aesthetic preference.

**Confidence:** High.

## CF-003: Affordance is relational

Action possibility depends on the user, object, and environment. A product feature cannot be classified as usable solely from its geometry.

**Confidence:** High.

## CF-004: Visual signifiers and actual action possibilities are distinct

A feature can communicate an action without supporting it, and it can support an action without communicating it. Product analysis must score both.

**Confidence:** High.

## CF-005: Divided-attention contexts change the value of physical controls

Controls that can be found and differentiated without vision are particularly valuable when visual attention belongs elsewhere, as in driving.

**Confidence:** High.

## CF-006: Additional product capability can decrease usability

Feature-fatigue research demonstrates that capability and usability can pull evaluations in opposite directions before and after use.

**Confidence:** High for the existence of the tension; moderate for generalization to all product classes.

## CF-007: Repairability is broader than disassembly

Standards and repairability frameworks include parts, tools, information, access, and other systemic factors.

**Confidence:** High.

## CF-008: Product attachment has multiple causes

Research identifies memories, pleasure, usability, appearance, reliability, and repair as relevant factors. No single formal property explains attachment.

**Confidence:** Moderate.

## CF-009: Tactile feedback can improve performance in tasks where vision is limited or augmented

Motor-learning and prosthetic-control studies support measurable benefits under certain conditions.

**Confidence:** Moderate-high.

## CF-010: Complexity can be relocated rather than eliminated

Historical analysis of Sapper's Tizio demonstrates high structural complexity paired with low interactional complexity. This distinction is observable and analytically useful.

**Confidence:** Moderate-high.

------------------------------------------------------------------------

# Rejected Hypotheses

## RH-001: Fewer features are inherently better

Rejected because feature value, user expertise, task requirements, safety, and flexibility can justify additional capability. The measurable problem is not feature count alone but interaction burden relative to value.

## RH-002: Visual minimalism predicts usability

Rejected because hidden modes, multifunction controls, poor feedback, and inaccessible construction can exist behind a visually simple exterior.

## RH-003: Affordance means that an object looks usable

Rejected because affordance includes actual action possibility relative to a user and environment. Appearance is one source of information, not the entire construct.

## RH-004: Physical buttons are always superior to touchscreens

Rejected because control quality depends on task, context, implementation, and the need for reconfiguration. Engineered haptics and large adaptive targets can outperform poor physical controls in some contexts.

## RH-005: Easy disassembly equals repairability

Rejected because diagnosis, documentation, parts, software, tools, economics, and reassembly determine whether repair actually occurs.

## RH-006: Emotional attachment can be directly designed

Rejected in strong form. Designers can support antecedents of attachment, but memories and personal circumstances are often decisive and cannot be specified into the object.

## RH-007: Rams and Sapper occupy opposite ends of one simplicity scale

Rejected because both manage substantial technical complexity and often reduce interactional complexity. Their differences concern allocation and expression, not simple quantity.

------------------------------------------------------------------------

# Emerging Patterns

## EP-001: The Visibility Paradox

Making a product visually quieter can reduce search noise, but hiding controls or state can increase cognitive burden.

This suggests a non-monotonic relationship. Too much visible information overwhelms. Too little conceals structure. The likely optimum is not maximum reduction but enough persistent information to support prediction and control.

### Why It Matters

This pattern connects product design, interface design, typography, and architecture. In every medium, visibility has both informational benefit and attentional cost.

## EP-002: Complexity Conservation

Complexity is rarely destroyed. It is transferred among:

- the user;
- the physical mechanism;
- software;
- manufacturing;
- maintenance;
- supply chains;
- service organizations;
- disposal and recycling.

A seamless product may be easy for the first user because complexity has been moved into manufacturing, proprietary tools, cloud services, or future repair difficulty.

### Why It Matters

This prevents Project Atlas from praising surface simplicity while ignoring displaced costs.

## EP-003: Compatibility Is Historically Layered

Users' expectations emerge from biology, repeated convention, local culture, profession, and prior products.

Turning a knob clockwise to increase a value is not purely biological. It is a learned compatibility reinforced by repeated exposure. Violating it may increase error even if another mapping is logically defensible.

### Why It Matters

Atlas should distinguish:

- biological compatibility;
- biomechanical compatibility;
- perceptual compatibility;
- cultural compatibility;
- product-family compatibility;
- task compatibility.

## EP-004: Good Feedback Reduces Epistemic Uncertainty

Feedback does more than create pleasure. It tells the user:

- whether the action registered;
- whether the state changed;
- how much it changed;
- whether the action is complete;
- whether correction is needed.

### Why It Matters

This connects physical controls to control theory. The product-user pair is a feedback system. Delay, noise, weak signal, and hidden state destabilize it.

## EP-005: Longevity Requires Continuing Fit

A durable object is not necessarily long-lived in use. Continued use requires ongoing fit across:

- technical function;
- user needs;
- available infrastructure;
- software compatibility;
- aesthetics;
- repair economics;
- regulation;
- emotional value.

### Why It Matters

This reframes longevity from a material property into a longitudinal relationship.

## EP-006: Calm and Delight Are Different Complexity Strategies

Rams and Sapper suggest two routes to product coherence:

- suppress competing signals until the product becomes calm;
- orchestrate mechanism and transformation until complexity becomes understandable and pleasurable.

### Why It Matters

Atlas should not equate quality with a single emotional tone. Calm, precision, delight, ceremony, ruggedness, and play may all be valid outcomes when matched to purpose.

------------------------------------------------------------------------

# Proposed Models

## MODEL-001: Product Legibility Model

### Purpose

Evaluate whether a user can discover, understand, execute, and verify an interaction.

### Components

Let product-interaction legibility be represented conceptually as:

```text
L = f(A, D, I, E, F, C)
```

Where:

- **A: Actual action possibility**  
  Can the target user physically perform the action in the target environment?

- **D: Discoverability**  
  Can the user locate the relevant control, surface, or manipulation opportunity?

- **I: Interpretability**  
  Can the user predict the action's effect?

- **E: Executability**  
  Are reach, force, precision, posture, timing, and sequence within acceptable limits?

- **F: Feedback quality**  
  Is response timely, perceptible, discriminable, and proportional to state change?

- **C: Consequence comprehension**  
  Does the user understand the resulting state and any risk or reversibility?

### Assumptions

- The components interact and are not purely additive.
- A severe failure in one critical component may dominate the total result.
- Scores must be population- and context-specific.
- Safety-critical actions require weighting consequence comprehension and error resistance more heavily.

### Prediction

Products with high visual discoverability but low actual action possibility will produce approach attempts followed by physical failure or abandonment.

Products with good action possibility and weak feedback will produce repeated actions, overcorrection, and uncertainty.

Products with strong feedback but poor interpretability will confirm actions without preventing the wrong action.

### Status

Preliminary model. Ready for operationalization.

------------------------------------------------------------------------

## MODEL-002: Complexity Allocation Model

### Purpose

Track where product complexity resides rather than treating complexity as a single visible quantity.

### Dimensions

```text
Total product complexity is distributed across:

1. Capability complexity
2. Interaction complexity
3. Representational complexity
4. Structural complexity
5. Manufacturing complexity
6. Maintenance complexity
7. Infrastructure complexity
8. End-of-life complexity
```

### Core Rule

> A design intervention that reduces complexity in one layer should be evaluated for complexity added to other layers and across time.

### Example: Tizio Lamp

- Structural complexity: relatively high.
- Manufacturing complexity: moderate to high.
- Interaction complexity: low.
- Representational complexity: low.
- Maintenance complexity: not yet fully assessed.

### Example: Seamless Sealed Device

- Representational complexity: low.
- Interaction complexity: potentially low at first use.
- Structural visibility: low.
- Maintenance complexity: potentially high.
- End-of-life complexity: potentially high.

### Assumptions

- Complexity cannot always be reduced to a common unit.
- The model is initially comparative, not absolute.
- Some complexity is beneficial because it creates capability, safety, or adaptability.

### Status

Strong conceptual model; quantitative method not yet defined.

------------------------------------------------------------------------

## MODEL-003: Feedback Closure Index

### Purpose

Assess whether an action-response loop gives users sufficient information to remain in control.

### Candidate Variables

- Response latency.
- Signal detectability.
- Signal discriminability.
- Directional compatibility.
- Magnitude proportionality.
- Persistence of state indication.
- Multimodal redundancy.
- Error signaling.
- Reversibility indication.

### Candidate Formula

```text
FCI = (Detectability × Discriminability × Compatibility × State Persistence)
      / (Latency × Ambiguity × Visual Dependence)
```

This is not yet a validated mathematical law. It is a structured hypothesis for comparing controls.

### Prediction

Low-latency feedback that is detectable but not discriminable will produce confidence without accurate state understanding.

High-quality tactile feedback will be most valuable when visual dependence is costly.

### Status

Preliminary.

------------------------------------------------------------------------

## MODEL-004: Product Lifetime Fit Model

### Purpose

Separate physical durability from continued useful life.

### Dimensions

```text
Useful Lifetime = min(
  Technical Life,
  Functional Relevance,
  Infrastructure Support,
  Repair Viability,
  Safety Acceptability,
  Economic Viability,
  User Relationship
)
```

The use of `min` is intentional. A single limiting factor can end active service even when all other factors remain strong.

### Assumptions

- A product no longer actively used has reached the end of useful life even if retained.
- Replacement may be rational when newer products substantially improve safety or resource use.
- Attachment can extend user relationship but cannot compensate indefinitely for every failure mode.

### Status

Promising. Requires longitudinal case data.

------------------------------------------------------------------------

## MODEL-005: Designer Genome as Weighted Strategy

### Purpose

Describe designers without reducing them to style labels.

### Candidate Dimensions

- Visible-order regularity.
- Control explicitness.
- Mechanical expression.
- Complexity concealment.
- Direct manipulation.
- Product-family coherence.
- Modularity.
- Repair access.
- Sensory drama.
- Material expression.
- Transformability.
- Lifecycle orientation.

### Initial Rams Hypothesis

High weights on visible order, product-family coherence, restraint, modularity, and lifecycle orientation.

### Initial Sapper Hypothesis

High weights on direct manipulation, mechanical translation, transformability, compactness, and sensory/mechanical character.

### Warning

These are hypotheses, not scores. Coding must be based on documented product properties and use behavior, not reputation.

------------------------------------------------------------------------

# Candidate Laws

## LAW-PROD-001: Necessary Complexity Law

### Hypothesis

Product quality declines when complexity imposes interaction or lifecycle cost without producing proportional capability, safety, adaptability, durability, or meaning.

### Prediction

Removing low-value features will improve post-use satisfaction more than removing frequently used or safety-critical features.

### Supporting Evidence

Feature-fatigue research; human-factors guidance; repairability analysis.

### Counter Evidence

Users may value optionality, perceived capability, and future flexibility even when features are rarely used.

### Confidence

Moderate.

------------------------------------------------------------------------

## LAW-PROD-002: Relational Affordance Law

### Hypothesis

An intended action is successfully afforded only when it is physically possible for the target user, perceptible in context, correctly interpreted, and executable under actual environmental constraints.

### Prediction

Products tested only by able-bodied designers in ideal environments will show higher failure rates in broader populations and real contexts.

### Supporting Evidence

Ecological affordance theory; engineering-design review; ergonomics guidance.

### Counter Evidence

Learned symbolic systems can sometimes compensate for weak direct perception.

### Confidence

High as a conceptual law; moderate as an operational law.

------------------------------------------------------------------------

## LAW-PROD-003: Feedback Closure Law

### Hypothesis

Interaction error and repeated action increase as feedback becomes delayed, ambiguous, weakly discriminable, or dependent on attention unavailable to the task.

### Prediction

Adding rapid, state-specific tactile or auditory feedback to visually demanding controls will reduce repeated activation and visual confirmation checks.

### Supporting Evidence

Control-loop reasoning; vibrotactile studies; NHTSA and FDA guidance.

### Counter Evidence

Additional feedback can distract, annoy, or overload users when redundant or poorly encoded.

### Confidence

Moderate-high.

------------------------------------------------------------------------

## LAW-PROD-004: Complexity Displacement Law

### Hypothesis

Reductions in user-visible complexity frequently transfer complexity into mechanisms, software, manufacturing, maintenance, infrastructure, or end-of-life processes.

### Prediction

Products marketed as seamless will not consistently show lower total lifecycle complexity than visibly modular products.

### Supporting Evidence

Tizio case; repairability standards; sealed-device analysis.

### Counter Evidence

Integrated design can sometimes genuinely remove components and failure points rather than merely hiding them.

### Confidence

Moderate.

------------------------------------------------------------------------

## LAW-PROD-005: Persistent Control Advantage

### Hypothesis

Persistent, spatially stable, tactilely differentiated controls outperform transient controls when tasks require eyes-free location, continuous adjustment, rapid repeated use, or low selection error.

### Prediction

Performance differences will increase under vibration, divided attention, gloves, or movement.

### Supporting Evidence

NHTSA control-placement and distraction guidance; haptic and motor-control research.

### Counter Evidence

Adaptive controls can outperform persistent controls when task context changes substantially and visual attention is available.

### Confidence

High within stated conditions.

------------------------------------------------------------------------

## LAW-PROD-006: Repair Ecosystem Law

### Hypothesis

Repairability is limited by the weakest necessary element in the repair chain, not by disassembly alone.

### Prediction

Improving fastener access without improving parts, diagnostics, documentation, or software permission will produce limited improvement in actual repair rates.

### Supporting Evidence

EN 45554; repairability frameworks.

### Counter Evidence

For simple mechanical products, access and standard parts may dominate nearly all other factors.

### Confidence

High.

------------------------------------------------------------------------

## LAW-PROD-007: Longitudinal Fit Law

### Hypothesis

A product remains in active use only while it maintains sufficient technical, functional, economic, infrastructural, safety, and relational fit.

### Prediction

Improving physical durability alone will have weak effects for products with rapid software or infrastructure obsolescence.

### Supporting Evidence

Product-attachment research; repairability analysis; lifecycle reasoning.

### Counter Evidence

Some products remain valuable as artifacts, collections, or backups after active use ends.

### Confidence

Moderate.

------------------------------------------------------------------------

# Open Questions

Ranked by importance.

## 1. How should Product Legibility be measured?

**Importance:** Critical  
**Reason:** Without operational metrics, the model remains descriptive.  
**Needed evidence:** Task-time studies, error counts, discoverability tests, biomechanical measures, confidence ratings, and feedback-latency experiments.

## 2. Can complexity be quantified across layers?

**Importance:** Critical  
**Reason:** Complexity Allocation is the most promising unifying model, but different forms of complexity use different units.  
**Needed evidence:** Information-theoretic measures, component-network metrics, interaction-step counts, dependency graphs, repair procedures, and cognitive workload measures.

## 3. Which Rams principles are empirically supported and which are ethical judgments?

**Importance:** High  
**Reason:** “Useful” and “understandable” can be tested differently from “honest,” “unobtrusive,” or “aesthetic.”  
**Needed evidence:** Principle-by-principle decomposition and product coding.

## 4. Does Sapper's mechanical delight improve long-term use or only first impressions?

**Importance:** High  
**Reason:** Delight may increase attachment, but novelty can decay.  
**Needed evidence:** Longitudinal owner interviews, maintenance records, repeated-use observations, and comparison with functionally equivalent products.

## 5. When does compatibility preserve a bad convention?

**Importance:** High  
**Reason:** Following expectation reduces immediate error but may inhibit better systems.  
**Needed evidence:** Transition studies comparing convention-compatible and redesigned mappings over learning periods.

## 6. What is the threshold where visible controls become clutter rather than helpful persistence?

**Importance:** High  
**Reason:** This is central to physical versus digital interface design.  
**Needed evidence:** Controlled variation in control count, differentiation, grouping, task frequency, and expertise.

## 7. Which product categories benefit most from emotional durability strategies?

**Importance:** Medium-high  
**Reason:** Attachment may matter more for furniture, tools, watches, and instruments than rapidly evolving electronics.  
**Needed evidence:** Cross-category replacement and retention data.

## 8. Can repairability scores predict actual repair behavior?

**Importance:** Medium-high  
**Reason:** A technically repairable product may still be discarded.  
**Needed evidence:** Warranty, service, spare-part, and consumer behavior data.

## 9. How should disabled and aging users be represented in genome scoring?

**Importance:** High  
**Reason:** Averaging across users can conceal exclusion.  
**Needed evidence:** Population-specific distributions and worst-case critical-task analysis.

## 10. How much product value comes from stable product-family grammar?

**Importance:** Medium  
**Reason:** Rams' work suggests that consistency across a family may reduce learning and increase trust.  
**Needed evidence:** Cross-product transfer studies.

------------------------------------------------------------------------

# Observations

## OBS-PROD-001

### Observation

The term “simple” is used in product-design commentary to describe visual appearance, feature count, interaction steps, internal construction, and emotional tone.

### Interpretation

Many disagreements about simplicity are actually disagreements about which dimension is being discussed.

### Confidence

High.

------------------------------------------------------------------------

## OBS-PROD-002

### Observation

Regulatory human-factors documents use task-specific language such as reach, findability, expectation, error, feedback, and critical tasks more often than aesthetic concepts.

### Interpretation

Product Genome laws will become stronger when translated from design adjectives into task and failure variables.

### Confidence

High.

------------------------------------------------------------------------

## OBS-PROD-003

### Observation

The Tizio lamp's visible calm depends on nontrivial counterweight and electrical engineering.

### Interpretation

Minimal appearance can be the output of high structural complexity rather than absence of complexity.

### Confidence

High.

------------------------------------------------------------------------

## OBS-PROD-004

### Observation

Attachment studies identify reliability and usability alongside memory and pleasure.

### Interpretation

Emotional durability may emerge partly from competent long-term service rather than emotional styling.

### Confidence

Moderate.

------------------------------------------------------------------------

## OBS-PROD-005

### Observation

Repairability standards emerged because intuitive judgments of repairability are incomplete and inconsistent.

### Interpretation

Atlas should avoid assigning repairability from photographs or teardown impressions without system-level evidence.

### Confidence

High.

------------------------------------------------------------------------

# Evidence

## EVD-PROD-001

### Citation

U.S. Food and Drug Administration. *Applying Human Factors and Usability Engineering to Medical Devices*. 2016.

### Summary

Design changes are generally more effective than labeling or training for reducing use-related hazards. Interface logic should match user expectations, abilities, and likely behavior.

### Supports

- LAW-PROD-002
- LAW-PROD-003

### Challenges

- The belief that instructions can compensate for poor interaction design.

------------------------------------------------------------------------

## EVD-PROD-002

### Citation

Masoudi, N. and colleagues. “A Review of Affordances and Affordance-Based Design to Address Usability.” 2019.

### Summary

Later design interpretations of affordance often omitted direct perception and biomechanics from the ecological concept.

### Supports

- LAW-PROD-002
- MODEL-001

### Challenges

- Affordance as visual suggestion alone.

------------------------------------------------------------------------

## EVD-PROD-003

### Citation

Thompson, D. V., Hamilton, R. W., and Rust, R. T. “Feature Fatigue.” 2005.

### Summary

Consumers value capability before use but place greater value on usability after experience, causing high-feature products to become less satisfying.

### Supports

- LAW-PROD-001
- MODEL-002

### Challenges

- Feature count as a straightforward predictor of purchase and satisfaction.

------------------------------------------------------------------------

## EVD-PROD-004

### Citation

National Highway Traffic Safety Administration. *Human Factors Design Guidance for Driver-Vehicle Interfaces*. 2016.

### Summary

Controls should be easy to reach and find, including without vision where appropriate, and should not interfere with the primary driving task.

### Supports

- LAW-PROD-005
- MODEL-003

### Challenges

- Replacing persistent controls without accounting for visual-attention cost.

------------------------------------------------------------------------

## EVD-PROD-005

### Citation

Bark, K. and colleagues. Vibrotactile feedback and arm-motion learning study. 2014.

### Summary

Adding vibrotactile feedback to visual feedback reduced movement error in the tested task.

### Supports

- LAW-PROD-003

### Challenges

- Vision as sufficient feedback for all motor tasks.

------------------------------------------------------------------------

## EVD-PROD-006

### Citation

CEN-CENELEC. EN 45554:2020.

### Summary

Repairability, reuse, and upgradeability require systematic assessment rather than a single construction feature.

### Supports

- LAW-PROD-006
- MODEL-004

### Challenges

- Visible screws as a sufficient repairability indicator.

------------------------------------------------------------------------

## EVD-PROD-007

### Citation

Page, T. “Product Attachment and Replacement.” 2014.

### Summary

Memories, pleasure, usability, appearance, reliability, and repair convenience influenced attachment and replacement attitudes in the study.

### Supports

- MODEL-004
- LAW-PROD-007

### Challenges

- Emotional attachment as a single directly controllable design feature.

------------------------------------------------------------------------

## EVD-PROD-008

### Citation

Museum of Modern Art. Collection and exhibition documentation for Richard Sapper's Tizio lamp and related work.

### Summary

Tizio uses counterweights and conducts current through its armature, creating broad adjustability without conventional visible wiring.

### Supports

- MODEL-002
- MODEL-005
- LAW-PROD-004

### Challenges

- Sapper as merely decorative or complication-seeking.

------------------------------------------------------------------------

# Recommendations

## Priority 1: Build the Product Case-Study Dataset

### Action

Code 20 Rams products and 20 Sapper products using the same schema.

### Required Fields

- Product and year.
- Intended user and task.
- Context of use.
- Control types.
- Action sequence.
- Feedback channels.
- Actual and perceived affordances.
- Reach, force, precision, and posture demands.
- Failure modes.
- Materials and manufacturing processes.
- Structural, interactional, representational, and maintenance complexity.
- Repair pathway.
- Evidence of continued use or replacement.
- Documentary source quality.

### Expected Value

Very high. This tests the designer-genome model and prevents reputation-driven scoring.

### Effort

High.

------------------------------------------------------------------------

## Priority 2: Operationalize Product Legibility

### Action

Define measurable scales for each MODEL-001 component.

### Candidate Measures

- Discovery time.
- Correct-action rate.
- Prediction accuracy.
- Required reach percentile.
- Force relative to user capability.
- Task completion time.
- Correction count.
- Repeated-action count.
- State-identification accuracy.
- Confidence calibration.

### Expected Value

Very high. This is the shortest path from taxonomy to predictive science.

### Effort

Moderate-high.

------------------------------------------------------------------------

## Priority 3: Create a Complexity Transfer Ledger

### Action

For each case study, record every claimed simplification and identify where complexity moved.

### Example Questions

- Did fewer controls create more modes?
- Did seamless construction create harder repair?
- Did automation create infrastructure dependence?
- Did modularity create more seams or parts?
- Did user simplicity require expensive manufacturing tolerances?

### Expected Value

High. This could become one of Project Atlas' most distinctive contributions.

### Effort

Moderate.

------------------------------------------------------------------------

## Priority 4: Test Physical Controls by Task Class

### Action

Organize existing research by task rather than by control technology.

### Task Classes

- Binary activation.
- Continuous adjustment.
- Repeated selection.
- Emergency action.
- Mode switching.
- Text entry.
- Parameter exploration.
- Eyes-free operation.
- Gloved or wet operation.
- Accessibility use cases.

### Expected Value

High.

### Effort

Moderate.

------------------------------------------------------------------------

## Priority 5: Separate Rams' Ethical Claims from Performance Claims

### Action

Classify each of the ten principles as:

- empirically testable performance claim;
- lifecycle claim;
- ethical claim;
- aesthetic judgment;
- contextual heuristic.

### Expected Value

High. This prevents the project from treating moral positions as physical laws while preserving their importance.

### Effort

Moderate.

------------------------------------------------------------------------

## Priority 6: Investigate Repairability Versus Actual Repair

### Action

Compare published repairability scores with repair rates, spare-part sales, warranty outcomes, independent repair activity, and product retention.

### Expected Value

Medium-high.

### Effort

High because behavioral data may be proprietary.

------------------------------------------------------------------------

## Priority 7: Delay Emotional-Durability Scoring

### Action

Do not yet assign products an “attachment score.” First establish longitudinal evidence and distinguish active use from sentimental retention.

### Expected Value

Medium. This avoids premature quantification.

### Effort

Low.

------------------------------------------------------------------------

# Highest-Value Next Research Cycle

The next autonomous run should focus on **the Product Legibility Model and Complexity Allocation Model through concrete products**.

Recommended sample:

### Dieter Rams

- Braun SK 4 record player/radio.
- Braun T 3 radio.
- Braun TP 1 portable radio/record player.
- Braun ET 66 calculator.
- Braun audio systems.
- Vitsœ 606 Universal Shelving System.
- Vitsœ 620 Chair Programme.

### Richard Sapper

- Tizio lamp.
- Grillo telephone.
- Doney television.
- 9090 espresso maker.
- Bollitore kettle.
- IBM ThinkPad 700C.
- ThinkPad 701C.

The run should avoid assigning aesthetic scores first. It should reconstruct task loops, mechanisms, product states, and maintenance paths, then infer designer tendencies from the coded evidence.

------------------------------------------------------------------------

# Bibliography

## Academic

Bark, K., Hyman, E., Tan, F., Cha, E., Jax, S. A., Buxbaum, L. J., and Kuchenbecker, K. J. “Effects of Vibrotactile Feedback on Human Learning of Arm Motions.” *IEEE Transactions on Neural Systems and Rehabilitation Engineering*, 2014.

Haines-Gadd, M., Chapman, J., Lloyd, P., Mason, J., and Aliakseyeu, D. “Emotional Durability Design Nine: A Tool for Product Longevity.” *Sustainability*, 10(6), 2018.

Mackamul, E. and colleagues. “Exploring Visual Signifier Characteristics to Improve the Perception of Affordances.” 2023.

Masoudi, N., Fadel, G. M., Pagano, C. C., and Elena, M. V. “A Review of Affordances and Affordance-Based Design to Address Usability.” *Proceedings of the Design Society: International Conference on Engineering Design*, 2019. DOI: 10.1017/dsi.2019.141.

Page, T. “Product Attachment and Replacement: Implications for Sustainable Design.” *International Journal of Sustainable Design*, 2(3), 2014, pp. 265–282.

Thompson, D. V., Hamilton, R. W., and Rust, R. T. “Feature Fatigue: When Product Capabilities Become Too Much of a Good Thing.” *Journal of Marketing Research*, 42(4), 2005, pp. 431–442. DOI: 10.1509/jmkr.2005.42.4.431.

Thomas, N., Fazlollahi, F., Brown, J. D., and Kuchenbecker, K. J. “Sensorimotor-Inspired Tactile Feedback and Control Improve Consistency of Prosthesis Manipulation in the Absence of Direct Vision.” 2021.

Tiab, J. and Hornbæk, K. “Understanding Affordance, System State, and Feedback in Shape-Changing Buttons.” 2016.

## Books

Gibson, J. J. *The Ecological Approach to Visual Perception*. Houghton Mifflin, 1979.

Norman, D. A. *The Design of Everyday Things*. Revised and expanded edition. Basic Books, 2013.

Rams, D. *Less but Better*. Multiple editions.

## Industry and Government

National Highway Traffic Safety Administration. *Human Factors Design Guidance for Driver-Vehicle Interfaces*. Report DOT HS 812 360, 2016.

National Highway Traffic Safety Administration. *Visual-Manual NHTSA Driver Distraction Guidelines for In-Vehicle Electronic Devices*. 2013.

U.S. Food and Drug Administration. *Applying Human Factors and Usability Engineering to Medical Devices*. 2016.

## Standards

CEN-CENELEC. EN 45554:2020. *General Methods for the Assessment of the Ability to Repair, Reuse and Upgrade Energy-Related Products*.

ISO 2575. *Road Vehicles: Symbols for Controls, Indicators and Tell-Tales*.

SAE J1138. *Design Criteria: Driver Hand Controls Location for Passenger Cars, Multipurpose Passenger Vehicles, and Trucks*.

## Historical and Museum Documentation

Design Museum. Historical documentation on Dieter Rams, Braun, and the ten principles of good design.

Museum of Modern Art. Collection record for Richard Sapper's *Tizio Table Lamp*, 1971.

Museum of Modern Art. *Marco Zanuso and Richard Sapper: Selections from the Permanent Collection*.

Museum of Modern Art. Collection records for Sapper and IBM portable computers and related product designs.

## Other

Sarkar, A. “Should Computers Be Easy to Use? Questioning the Doctrine of Simplicity in User Interface Design.” 2023.

Repairability assessment literature comparing EN 45554 with product-specific scoring methods.

------------------------------------------------------------------------

# Next Actions

- Create a structured case-study schema for individual products.
- Code the first seven Rams and seven Sapper products.
- Build a Complexity Transfer Ledger for each product.
- Define operational measures for all six Product Legibility components.
- Add disability and aging-population constraints before general scoring.
- Separate ethical, aesthetic, and empirical claims in Rams' ten principles.
- Search patent records for the Tizio mechanism, Braun control systems, ThinkPad mechanisms, and repair-relevant construction details.
- Compare historical product manuals with current museum descriptions to reconstruct actual interaction rather than relying on photographs.

------------------------------------------------------------------------

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 1.0 | 2026-07-19 | Kevin Miller and OpenAI | First autonomous Product Genome research run. Tested minimalism, affordance, tactile feedback, repairability, emotional durability, and the Rams-Sapper comparison. Proposed Product Legibility and Complexity Allocation models. |

------------------------------------------------------------------------

# Agent Instructions

When creating or modifying this document:

1. Separate observation from interpretation.
2. Never strengthen a conclusion beyond the available evidence.
3. Preserve contradictory findings.
4. Prefer measurable variables over subjective descriptions.
5. Reference candidate laws and genome nodes whenever possible.
6. Use stable IDs for observations, evidence, laws, experiments, metrics, and case studies.
7. Record assumptions explicitly.
8. Record confidence explicitly.
9. Keep the YAML header valid.
10. Do not delete revision history; append to it.

This template is the standard for all Composition Science project documents.
