---
id: RR-VE-ATL-2026-2CCD
title: "Project Atlas Autonomous Research Report 001"
subtitle: "From Design-System Convergence to Predictive Mechanisms"
project: project-atlas
version: "0.1"
date: "2026-07-19"
status: "Research synthesis with falsification log"
scope:
  - perceptual grouping
  - spacing
  - visual crowding
  - progressive disclosure
  - command organization
  - spatial memory
  - safety-critical displays
  - accessibility
llm_ingest: true
machine_readable: true
purposes:
  - integrate
  - verify
audiences:
  - executive
  - practitioner
  - researcher
---

# Executive Summary

This research pass tested a central uncertainty in Project Atlas:

> When major design systems converge on the same recommendation, are they independently discovering universal perceptual laws, or merely repeating conventions that work within similar technological and cultural contexts?

Six repeated investigation cycles were completed. The strongest result is that the answer is neither extreme.

Design-system convergence is meaningful evidence, but it is not sufficient evidence of a universal law. Several recurring recommendations can be connected to established perceptual or cognitive mechanisms:

- proximity influences grouping;
- compatible grouping cues reinforce perceived structure;
- visual crowding limits identification in clutter;
- spatially stable interfaces support learned command location;
- visible structure can reduce recall demands;
- excessive information can impair search and monitoring;
- semantic and interaction structure must preserve visual meaning for accessibility.

However, these mechanisms do not yield simple universal design constants.

The most important falsification was the failure of the hypothesis that visual biology can directly produce a universal UI spacing scale or spacing ratio. Crowding thresholds vary with eccentricity, target and flanker similarity, orientation, context, task, timing, and observer. Meanwhile, proximity can improve grouping at distances that may impair individual identification. Therefore, “more spacing” is not a universal improvement and crowding research cannot be directly converted into an 8-pixel, 1.5-times, or similar universal interface rule.

A more defensible Atlas model emerged.

## The Relational Legibility Model

A composition must satisfy at least four different requirements:

1. **Grouping:** related items must be close or otherwise connected enough to form a coherent unit.
2. **Discrimination:** individual items must remain sufficiently separable to identify and operate.
3. **Hierarchy:** separation and salience must distinguish levels of organization.
4. **Stability:** important locations, identities, and relationships must persist across time and transformations.

These requirements can conflict. The design problem is not maximizing one. It is finding a feasible region where all remain adequate for the task, environment, user, and consequence level.

## Major discoveries

1. **Grouping and crowding are opposing distance pressures.** Proximity can strengthen organization while excessive proximity can impair identification.
2. **Hierarchical spacing is better modeled relationally than numerically.** Strong evidence supports greater separation between groups than within groups, but no universal ratio was established.
3. **Progressive disclosure conserves complexity rather than eliminating it.** It can reduce visible clutter, but costs move into discovery, interaction, navigation, memory, or loss of overview.
4. **Spatial stability is an independent resource.** Stable command locations and landmarks can support learning and expert performance.
5. **Salience is competitive and task-conditioned.** Multiple prominent signals compete, and the correct signal depends on current task and system state.
6. **Safety-critical guidance supports contextual evaluation, not universal component certification.** NASA and FAA materials frame display design as part of a larger human-system problem.
7. **Accessibility acts as a structural falsification test.** A hierarchy that works only visually is not robust.

## Confidence

- High confidence in the existence of the identified mechanisms.
- Moderate confidence in the proposed integrated model.
- Low confidence in any universal numerical spacing rule.
- Moderate-to-low confidence in broad claims that one command architecture is universally superior.
- High confidence that context and consequence must be explicit Atlas variables.

## Remaining uncertainty

The largest remaining uncertainty is quantitative:

> Can Atlas derive task-specific predictive ranges from combinations of visual angle, target size, similarity, eccentricity, semantic grouping, input method, and consequence without resorting to universal pixel rules?

---

# Research Log

# Cycle 1 — Can Biological Crowding Limits Produce a Universal Spacing Law?

## Objective

Test whether visual crowding research can provide a direct quantitative basis for interface spacing recommendations.

## Hypothesis

**H1:** There is a biologically grounded critical spacing relationship that can be translated into a broadly applicable UI spacing rule.

## Evidence Found

Research on visual crowding strongly supports critical spacing constraints. Objects in peripheral vision can become unidentifiable when surrounding objects are too close. Critical spacing changes with retinal eccentricity rather than being a fixed physical or pixel distance. Studies of realistic objects also find recognition declines as target and flanker spacing decreases.

## Evidence Against

The threshold varies with retinal eccentricity, radial versus tangential arrangement, target and flanker similarity, spatial frequency, contrast, object category, context, depth, observer, presentation duration, and task. Real-world context can sometimes mitigate crowding, while large depth differences can amplify it.

## Sources

- Pelli and Tillman, crowding as a cortical constraint: https://pubmed.ncbi.nlm.nih.gov/18835355/
- Strasburger, Rentschler, and Jüttner review: https://pubmed.ncbi.nlm.nih.gov/22207654/
- Real-world object crowding: https://pubmed.ncbi.nlm.nih.gov/35145614/
- Spatial frequency and contrast: https://pubmed.ncbi.nlm.nih.gov/11369047/
- Context mitigation: https://pubmed.ncbi.nlm.nih.gov/30055337/
- Semantic and spatial proximity: https://pubmed.ncbi.nlm.nih.gov/30906517/
- Depth differences: https://pubmed.ncbi.nlm.nih.gov/37665324/

## Analysis

Crowding is better represented as a conditional function:

```text
crowding risk =
f(
  retinal eccentricity,
  angular target size,
  target–flanker angular separation,
  similarity,
  orientation,
  contrast,
  context,
  exposure time,
  observer,
  task
)
```

Pixel spacing contributes to angular separation only under a specific device and viewing condition.

## Conclusion

**H1 rejected in its universal form.**

A narrower hypothesis survives: crowding research can constrain task-specific layouts when viewing geometry, target properties, and task conditions are known.

## Confidence

High.

## Next Step

Investigate whether perceptual grouping supplies the missing relational component.

---

# Cycle 2 — Does Relative Separation Produce Hierarchical Grouping?

## Objective

Test Hierarchical Spacing Differentiation.

## Hypothesis

**H2:** Nested groups are communicated more reliably when between-group separation exceeds within-group separation.

## Evidence Found

Ben-Av and Sagi placed proximity and similarity cues in conflict. Proximity influenced grouping rapidly, while similarity became more dominant with additional processing time. Wagemans and colleagues confirm proximity, similarity, common region, connectedness, continuity, and other cues as central to perceptual organization. Bae and colleagues provide evidence that proximity, color similarity, common region, connectivity, and alignment can reinforce structure in realistic content.

Applied systems also converge. Carbon explicitly recommends greater spacing between form groups than between items, while Microsoft, Material, GOV.UK, Apple, Spectrum, and USWDS implement similar nested differentiation.

## Evidence Against

No source established one universal between-group/within-group ratio. Proximity can lose to similarity or semantic cues. Connectedness and common region may dominate spacing. Grouping also does not guarantee identification.

## Sources

- Ben-Av and Sagi: https://pubmed.ncbi.nlm.nih.gov/7740775/
- Wagemans et al.: https://pubmed.ncbi.nlm.nih.gov/22845751/
- Bae et al.: https://pubmed.ncbi.nlm.nih.gov/26356911/
- Pomerantz and Portillo: https://pubmed.ncbi.nlm.nih.gov/21728463/

## Analysis

The evidence supports an ordinal relation rather than a universal metric:

```text
for intended nested groups:
evidence(group boundary at level n+1)
>
evidence(item separation at level n)
```

Spacing is one source of evidence. Labels, borders, alignment, color, connectivity, and behavior contribute to the total.

## Conclusion

**H2 confirmed in relational form, rejected in universal-ratio form.**

## Confidence

High for relative grouping; low for a universal ratio.

## Next Step

Resolve the contradiction between proximity helping grouping and harming identification.

---

# Cycle 3 — Is More Spacing Generally Better?

## Objective

Test whether increasing spacing monotonically improves clarity.

## Hypothesis

**H3:** Increasing spacing improves clarity until physical space runs out.

## Evidence Found

Crowding evidence supports increased separation when users must identify individual targets among similar neighbors. FAA guidance recommends displaying only required information to avoid overload and clutter.

## Evidence Against

Grouping research shows increasing separation can weaken perceived relationship. Excessive spacing can increase eye movement, pointer travel, scrolling, loss of overview, and distance between labels and controls.

## Sources

- FAA multifunction-display guidelines: https://www.faa.gov/sites/faa.gov/files/data_research/research/med_humanfacs/oamtechreports/0117.pdf
- Grouping and crowding sources from Cycles 1 and 2.

## Analysis

Atlas should use a **Separation Window**:

```text
too little separation
    → crowding, accidental grouping, weak discrimination

useful separation range
    → items remain distinguishable and related

too much separation
    → weakened continuity, extra search/travel, false segmentation
```

## Conclusion

**H3 rejected.** More spacing is not inherently better.

## Confidence

High.

## Next Step

Model grouping and discrimination as separate functions.

---

# Cycle 4 — Does Progressive Disclosure Reduce Complexity?

## Objective

Test Disclosure Conservation.

## Hypothesis

**H4:** Progressive disclosure reduces interface complexity.

## Evidence Found

FAA guidance recommends displaying required information and making additional information available on request. Ribbons, disclosure controls, accordions, question-page flows, and adaptive panes can reduce simultaneous visual competition.

## Evidence Against

Hidden content may not be noticed. A 2026 study reported that progressive disclosure improved perceived learning but did not effectively mitigate cognitive load. Progressive disclosure can add reveal decisions, interaction steps, hidden-state memory, navigation depth, mode dependence, and loss of comparison.

## Sources

- FAA multifunction-display guidelines: https://www.faa.gov/sites/faa.gov/files/data_research/research/med_humanfacs/oamtechreports/0117.pdf
- Anik et al. 2026: https://dl.acm.org/doi/10.1145/3742413.3789087
- Springer and Whittaker: https://dl.acm.org/doi/10.1145/3374218
- Earlier empirical approach: https://dl.acm.org/doi/10.1145/3301275.3302322

## Analysis

```text
total interaction burden =
visible search burden
+ reveal burden
+ navigation burden
+ memory burden
+ state-tracking burden
+ overview loss
```

Progressive disclosure can lower one term while increasing others.

## Conclusion

**H4 rejected in its simple form.**

Revised principle: progressive disclosure redistributes complexity across space, time, interaction, and memory.

## Confidence

High in the revised principle.

## Next Step

Determine when stability outweighs hiding or adapting commands.

---

# Cycle 5 — Is Adaptive Rearrangement Worth the Cost to Spatial Memory?

## Objective

Test whether adaptive command layouts generally improve performance.

## Hypothesis

**H5:** Moving, hiding, or resizing commands according to relevance generally improves performance.

## Evidence Found

Adaptive systems can reduce irrelevant options and prioritize likely commands.

## Evidence Against

Spatially stable command layouts support learning. CommandMaps research found performance benefits from stable layouts. Recent studies show users develop spatial memory for command sets, command-set size can impair learning, and landmarks or visual differentiation can support location learning.

## Sources

- CommandMaps: https://dl.acm.org/doi/10.1145/2468356.2468711
- Spatial-memory review: https://dl.acm.org/doi/10.1561/1100000046
- Landmarks: https://dl.acm.org/doi/10.1145/3411764.3445050
- Increasing command capacity: https://dl.acm.org/doi/10.1145/3696762.3698055
- Large command sets: https://dl.acm.org/doi/10.1145/3769872.3769882
- Artificial landmarks: https://dl.acm.org/doi/10.1145/3706599.3720030

## Analysis

```text
benefit of adaptation =
reduced irrelevant competition
+ increased immediate relevance

cost of adaptation =
location instability
+ hidden-state uncertainty
```

A defensible adaptation order is:

1. preserve location and change emphasis;
2. preserve group location and change internal prominence;
3. collapse secondary detail predictably;
4. move commands only when context change is explicit and persistent;
5. avoid opaque frequency-driven relocation.

## Conclusion

**H5 rejected as a general rule.**

Revised principle: adapt relevance before location whenever possible.

## Confidence

Moderate-high.

## Next Step

Investigate salience as a competitive resource.

---

# Cycle 6 — Is Salience a Fixed Visual Property?

## Objective

Test the Salience Budget principle.

## Hypothesis

**H6:** Increasing visual contrast reliably increases the chance that the correct element is noticed and acted upon.

## Evidence Found

Contrast, size, color, motion, orientation, and isolation can make elements more detectable. Design systems use them to create hierarchy. WCAG requires visible focus and non-text contrast. FAA and NASA treat warnings, critical states, and clutter as human-factors concerns.

## Evidence Against

Bottom-up saliency models do not fully predict task-directed attention and often omit crowding. Multiple high-contrast signals compete. A signal can be detected but misinterpreted. NASA research found conditions where obstacles were detected but some pilots did not recognize threat severity.

## Sources

- Computational versus psychophysical saliency: https://pubmed.ncbi.nlm.nih.gov/21422490/
- NASA synthetic/enhanced vision evaluation: https://ntrs.nasa.gov/api/citations/20090019723/downloads/20090019723.pdf
- WCAG 2.2: https://www.w3.org/TR/WCAG22/

## Analysis

Salience needs three layers:

1. **Physical salience:** sensory contrast.
2. **Task salience:** relevance to the current goal.
3. **Operational salience:** importance to safe or successful system state.

```text
effective priority signal
≈ physical detectability
× semantic interpretability
× task relevance
× response availability
```

## Conclusion

**H6 rejected.** Salience is relational, competitive, and task-conditioned.

## Confidence

High conceptually; quantitative form remains uncertain.

## Next Step

Integrate the cycles into a predictive Atlas model.

---

# Confirmed Findings

1. Proximity affects visual grouping. **Confidence: High.**
2. Grouping is multi-cue and competitive. **Confidence: High.**
3. Visual crowding limits identification in clutter. **Confidence: High.**
4. Crowding thresholds are conditional, not fixed pixel distances. **Confidence: High.**
5. Grouping and identification impose opposing spacing pressures. **Confidence: High.**
6. Spatially stable command layouts support learning. **Confidence: Moderate-high.**
7. Progressive disclosure redistributes rather than eliminates complexity. **Confidence: High.**
8. Display clutter is a recognized human-factors risk. **Confidence: High as normative guidance.**
9. Detection and interpretation are distinct. **Confidence: High.**
10. Visual hierarchy alone is structurally insufficient for accessibility. **Confidence: High.**

---

# Rejected Hypotheses

## A universal spacing token can be derived from visual biology

Rejected because crowding depends on angular geometry, eccentricity, similarity, orientation, context, and task.

## More whitespace always increases clarity

Rejected because excessive separation can weaken grouping, continuity, overview, and label-control association.

## A universal between-group/within-group ratio exists

Rejected because the ordinal relationship is supported, but no context-independent ratio was found.

## Progressive disclosure reduces total complexity

Rejected because burden moves into hidden state, interaction, memory, navigation, and overview loss.

## Adaptive command relocation generally improves performance

Rejected because relocation can destroy spatial learning.

## Visual contrast alone determines useful salience

Rejected because attention and response depend on relevance, interpretation, competition, and system state.

## Conformance to a design system demonstrates usability

Rejected because high-quality standards still require contextual evaluation.

---

# Open Questions

## 1. Can Atlas quantify a task-specific Relational Legibility Envelope?

Importance: Very high  
Expected value: Very high  
Difficulty: High

## 2. How should Atlas combine grouping probability and crowding risk?

Importance: Very high  
Expected value: Very high  
Difficulty: High

## 3. What is the best measurable definition of hierarchy strength?

Importance: High  
Expected value: High  
Difficulty: Medium-high

Candidates include classification accuracy, response time, observer agreement, eye-movement transitions, information-theoretic separability, and robustness under degraded presentation.

## 4. When should adaptive interfaces preserve location versus hide content?

Importance: High  
Expected value: High  
Difficulty: Medium

## 5. Can salience competition be modeled as a limited budget?

Importance: High  
Expected value: High  
Difficulty: High

## 6. What transformations preserve structural identity?

Importance: High  
Expected value: High  
Difficulty: Medium

## 7. How do failure consequences alter acceptable design margins?

Importance: High  
Expected value: High in safety-critical domains  
Difficulty: High

## 8. How well do design-system recommendations predict measured performance?

Importance: Medium-high  
Expected value: High  
Difficulty: Medium-high

---

# Emerging Patterns

## Competing objectives

Design guidance often appears one-directional, but each recommendation has an opposing cost. Atlas should model feasible tradeoff regions rather than isolated best practices.

## Structure as probabilistic evidence

Spacing, labels, borders, alignment, color, and behavior each provide evidence for an organization. No cue guarantees interpretation.

## Complexity conservation

Attempts to simplify one dimension move burden elsewhere:

- space to interaction;
- visibility to memory;
- density to navigation;
- flexibility to predictability;
- adaptation to instability.

## Stable landmarks support expertise

Distinct regions, landmarks, size variation, and stable structure provide anchors. This connects interfaces to architecture, wayfinding, maps, memory palaces, and cockpit layouts.

## Consequence changes acceptable margins

A weak cue tolerable in entertainment software may be unacceptable in aviation, medical, industrial, or emergency systems.

## Accessibility reveals structural robustness

If hierarchy disappears during reflow, screen-reader navigation, keyboard use, or localization, it was partly a styling effect rather than a robust system relationship.

## Interfaces as communication channels

The design encodes intended structure using noisy signals. The user decodes it under perceptual, cognitive, environmental, and task constraints.

---

# Proposed Models

# Model 1 — Relational Legibility Envelope

A composition is acceptable when grouping, discrimination, task cost, and consequence-adjusted error risk are simultaneously within limits.

```yaml
geometry:
  target_visual_angle:
  separation_visual_angle:
  eccentricity:
  alignment:
  orientation:
  containment:

content:
  target_similarity:
  semantic_relationship:
  label_quality:
  visual_complexity:

task:
  identify:
  compare:
  group:
  scan:
  monitor:
  operate:
  read:

interaction:
  input_method:
  target_size:
  movement_distance:
  reveal_steps:

context:
  viewing_distance:
  time_pressure:
  workload:
  consequence:
  visual_ability:
```

```text
grouping confidence >= minimum threshold
and
identification accuracy >= minimum threshold
and
task completion cost <= acceptable limit
and
error risk <= consequence-adjusted limit
```

# Model 2 — Cue Evidence Accumulation

```text
evidence for grouping G =
w_p * proximity
+ w_r * common_region
+ w_c * connectivity
+ w_a * alignment
+ w_s * similarity
+ w_l * label semantics
+ w_b * behavioral coupling
+ interactions
```

Weights depend on processing time, task, experience, stimulus type, and viewing conditions.

# Model 3 — Complexity Redistribution Ledger

```yaml
visible_search_cost:
interaction_cost:
navigation_depth:
memory_cost:
state_tracking_cost:
overview_loss:
spatial_instability:
error_recovery_cost:
```

# Model 4 — Spatial Stability Hierarchy

1. Preserve identity.
2. Preserve group.
3. Preserve approximate location.
4. Preserve landmarks.
5. Change emphasis before position.
6. Hide predictably before relocating unpredictably.
7. Expose a stable recovery path.

# Model 5 — Three-Layer Salience

- Physical salience
- Task salience
- Operational salience

An effective priority signal aligns all three.

# Model 6 — Consequence-Adjusted Design Margin

```text
required design margin increases with:
failure severity
× irreversibility
× time pressure
× workload
× uncertainty
```

---

# Recommendations

| Priority | Research direction | Expected value | Effort | Rationale |
|---|---|---:|---:|---|
| 1 | Build the Relational Legibility Envelope from psychophysics and HCI data | Very high | High | Converts principles into conditional prediction |
| 2 | Extract quantitative rules from FAA HF-STD-001B and NASA HIDH | Very high | Medium-high | Adds high-consequence constraints |
| 3 | Create paired measured examples for grouping versus crowding | Very high | Medium | Tests the central spacing tradeoff |
| 4 | Build a visual-angle normalization layer | High | Medium | Prevents misleading pixel claims |
| 5 | Synthesize spatial-memory command research | High | Medium | Critical for Ribbon and adaptive interfaces |
| 6 | Create a Complexity Redistribution scorecard | High | Low-medium | Immediately useful for disclosure analysis |
| 7 | Map WCAG structure to Atlas examples | High | Medium | Tests robustness across modalities |
| 8 | Investigate salience competition and alarm prioritization | High | High | Important for safety and dashboards |
| 9 | Compare novice and expert command organization | Medium-high | Medium-high | Determines when visibility or stability dominates |
| 10 | Delay universal token recommendations | High | Low | Evidence does not support them |

## Highest-value next artifact

**ATLAS-0002: The Relational Legibility Envelope**

It should integrate proximity grouping, crowding, target identification, hierarchical separation, semantic cue agreement, visual angle, task, and consequence. It should explicitly avoid claiming a universal spacing ratio.

---

# Bibliography

# Academic

- Abdullah, S. M., et al. *Effects of Increasing Command Capacity of Spatial Memory-Based Interfaces*. ACM. https://dl.acm.org/doi/10.1145/3696762.3698055
- Abdullah, S. M., et al. *Enhancing Spatial Learning of Large Command Sets*. ACM. https://dl.acm.org/doi/10.1145/3769872.3769882
- Anik, A. I., et al. *The Impact of Information Depth and Progressive Disclosure*. ACM, 2026. https://dl.acm.org/doi/10.1145/3742413.3789087
- Bae, J., et al. *Reinforcing Visual Grouping Cues to Communicate Complex Informational Structure*. https://pubmed.ncbi.nlm.nih.gov/26356911/
- Ben-Av, M. B., and Sagi, D. *Perceptual Grouping by Similarity and Proximity*. https://pubmed.ncbi.nlm.nih.gov/7740775/
- Chung, S. T. L., Levi, D. M., and Legge, G. E. *Spatial-frequency and Contrast Properties of Crowding*. https://pubmed.ncbi.nlm.nih.gov/11369047/
- Pelli, D. G., and Tillman, K. A. *Crowding: A Cortical Constraint on Object Recognition*. https://pubmed.ncbi.nlm.nih.gov/18835355/
- Pomerantz, J. R., and Portillo, M. C. *Grouping and Emergent Features in Vision*. https://pubmed.ncbi.nlm.nih.gov/21728463/
- Scarr, J., et al. *Exploiting Spatial Memory to Design Efficient Command Interfaces*. https://dl.acm.org/doi/10.1145/2468356.2468711
- Scarr, J., et al. *Supporting and Exploiting Spatial Memory in User Interfaces*. https://dl.acm.org/doi/10.1561/1100000046
- Springer, A., and Whittaker, S. *Progressive Disclosure: When, Why, and How Do Users Want Algorithmic Transparency Information?* https://dl.acm.org/doi/10.1145/3374218
- Strasburger, H., Rentschler, I., and Jüttner, M. *Peripheral Vision and Pattern Recognition: A Review*. https://pubmed.ncbi.nlm.nih.gov/22207654/
- Uddin, M. S., et al. *How People Use Landmarks to Develop Spatial Memory in Large Command Spaces*. https://dl.acm.org/doi/10.1145/3411764.3445050
- Wagemans, J., et al. *A Century of Gestalt Psychology in Visual Perception*. https://pubmed.ncbi.nlm.nih.gov/22845751/

# Standards

- Federal Aviation Administration. *Human Factors Design Standard, HF-STD-001B*. https://hf.tc.faa.gov/publications/2016-12-human-factors-design-standard/full_text.pdf
- Federal Aviation Administration. *Human Factors Design Guidelines for Multifunction Displays*. https://www.faa.gov/sites/faa.gov/files/data_research/research/med_humanfacs/oamtechreports/0117.pdf
- W3C. *Web Content Accessibility Guidelines 2.2*. https://www.w3.org/TR/WCAG22/
- W3C. *Understanding WCAG 2.2*. https://www.w3.org/WAI/WCAG22/Understanding/

# Industry

- Microsoft. *Windows Ribbon UX Guide*. https://learn.microsoft.com/en-us/windows/win32/uxguide/cmd-ribbons
- Microsoft. *Ribbon Size Definitions and Scaling Policies*. https://learn.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-templates
- IBM Carbon. *Forms Pattern*. https://carbondesignsystem.com/patterns/forms-pattern/
- Adobe Spectrum. *Spacing*. https://spectrum.adobe.com/page/spacing/
- Adobe Spectrum. *Platform Scale*. https://spectrum.adobe.com/page/platform-scale/
- U.S. Web Design System. *Form*. https://designsystem.digital.gov/components/form/

# Historical and Engineering

- Bailey, R. E., et al. *Evaluation of Fused Synthetic and Enhanced Vision Display Concepts*. https://ntrs.nasa.gov/api/citations/20090019723/downloads/20090019723.pdf
- Baty, D. L. *Advanced Flight Deck Display System Concepts*. NASA Technical Memorandum, 1976. https://ntrs.nasa.gov/api/citations/19770004101/downloads/19770004101.pdf
- Wiener, E. L. *Human Factors of Advanced Technology “Glass Cockpit” Transport Aircraft*. NASA Contractor Report 177528. https://ntrs.nasa.gov/api/citations/19890016609/downloads/19890016609.pdf

# Books

No technical book was necessary to resolve the highest-priority hypotheses in this pass.

# Patents

No patent was necessary to resolve the highest-priority hypotheses in this pass.

# Other

Secondary commentary was excluded where primary research, standards, or engineering documents were available.
