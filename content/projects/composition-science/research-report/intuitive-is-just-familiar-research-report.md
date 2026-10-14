---
id: RR-VE-COMP-2026-9755
slug: intuitive-is-just-familiar-research-report
authors:
- OpenAI autonomous research agent
confidence: High for the central model; Moderate for proposed quantitative laws
date: 2026-07-19
llm_ingest: true
machine_readable: true
project: composition-science
purpose: |
  Investigate the claim "intuitive is just familiar," determine how repeated exposure changes perceived and actual design quality, identify the limits of familiarity, and develop testable models for predicting when convention, repetition, and intrinsic design structure will dominate user performance.
references:
- EVD-001
- EVD-002
- EVD-003
- EVD-004
- EVD-005
- EVD-006
- EVD-007
- EVD-008
- EVD-009
- EVD-010
- EVD-011
- EVD-012
status: research-complete
summary: |
  Familiarity is a major source of what users call intuitive, but it is not the whole phenomenon. Repetition increases processing fluency, liking, confidence, speed, accuracy, and automaticity. It can therefore make an initially mediocre interface feel natural and, for practiced users, perform well. However, familiarity cannot reliably repair poor task structure, excessive physical effort, weak feedback, hidden state, preventable errors, or dangerous mappings. The report proposes that perceived intuitiveness is an inference generated from processing fluency and prediction success, while durable design quality depends on separate dimensions: immediate legibility, learnability, practiced efficiency, error resistance, transferability, and adaptability. Familiarity is best modeled as a performance multiplier and switching-cost generator, not as evidence that the underlying design is good.
version: 1.0
purposes:
  - integrate
  - verify
audiences:
  - executive
  - practitioner
  - researcher
---

# Intuitive Is Just Familiar

## Purpose

This report investigates a deceptively simple claim:

> Intuitive is just familiar.

The useful core of the claim is that users often call an interaction intuitive when it matches something they have already learned. Repetition may make an acceptable or even awkward design feel natural, fast, and preferable. That possibility matters because design evaluation often confuses three different things:

1. A design that is easy on first contact.
2. A design that becomes easy after learning.
3. A design that remains effective, efficient, and safe after extensive use.

The research goal was not to defend the phrase. It was to identify what it explains, what it fails to explain, and how to turn it into predictive design principles.

------------------------------------------------------------------------

# Executive Summary

## What Was Accomplished

The investigation connected evidence from cognitive psychology, human factors, human-computer interaction, motor control, aesthetics, learning theory, safety engineering, and standards.

The central proposition was broken into five testable claims:

1. Repetition makes stimuli and interactions feel easier.
2. Felt ease is often interpreted as goodness, truth, confidence, or intuitiveness.
3. Practice can make initially awkward procedures fast and automatic.
4. Familiarity creates switching costs that can preserve inferior designs.
5. Some aspects of good design remain independent of familiarity.

All five claims received support, but the strongest version of the slogan was rejected.

## Major Discoveries

### 1. “Intuitive” is usually an attribution, not a directly observed property

Users do not inspect the causes of their own fluency. They experience low effort, successful prediction, and rapid action, then attribute that experience to the object: “This is intuitive.” The same subjective ease can arise from prior exposure, clear mapping, perceptual salience, reduced choice uncertainty, or motor compatibility.

### 2. Familiarity changes both feeling and performance

Repeated exposure can increase liking even without explicit recognition. Practice can convert controlled operations into automatic responses, reducing attention demand and improving speed and accuracy. Interface habit studies show measurable gains that disappear when stable cues are disrupted.

### 3. Familiarity can make an average design locally excellent for an experienced population

A design does not need to be globally optimal to become highly effective within a stable user population. If users repeatedly perform the same tasks, memorize locations, chunk action sequences, and develop motor routines, an initially unremarkable interface may become extremely fast.

This is real quality in one sense: experienced users can achieve goals efficiently. It is not imaginary. But it is contingent quality, purchased through user learning.

### 4. Familiarity also conceals defects

Practice can suppress evidence of poor discoverability, weak labels, excessive memorization, and arbitrary mappings. Experienced users route around defects so effectively that observation of experts alone can make a bad design look good.

### 5. Habit creates design debt

Once a pattern becomes automatic, changing it carries a retraining cost and can temporarily increase errors. A familiar but mediocre design can therefore survive because the transition cost exceeds the expected benefit of a better replacement.

### 6. Familiarity is not sufficient for goodness

Repeated use cannot fully repair:

- excessive action count
- long physical travel distance
- slow system response
- hidden or ambiguous state
- poor error recovery
- dangerous mode confusion
- inaccessible controls
- mismatch between task structure and interface structure
- high consequences for slips

Practice can reduce thinking time, but it cannot make a distant target physically closer, remove a required step, or make absent feedback informative.

### 7. First-use intuition has at least three sources

What appears intuitive on first use can come from:

- **Biological or physical compatibility:** larger targets are easier to acquire; spatially compatible controls are easier to operate.
- **Cultural convention:** magnifying glass means search; underlined text suggests a link.
- **Structural inference:** visible constraints, signifiers, mappings, and feedback expose how the system works.

Only the second is purely familiarity. The first and third can support successful novel interaction.

## Central Conclusion

The phrase is directionally right but literally wrong.

A better formulation is:

> Much of what people call intuitive is familiar, and repeated successful use can convert a merely adequate design into a highly effective practiced system. But familiarity is only one source of fluency, and fluency is only one dimension of design quality.

## Confidence

- **High:** repetition increases fluency, liking, automaticity, and habit strength.
- **High:** familiarity can improve performance while increasing switching costs and habit-related errors.
- **High:** usability cannot be reduced to subjective ease; effectiveness, efficiency, satisfaction, context, and error consequences remain distinct.
- **Moderate:** the proposed decomposition of intuitiveness into prediction, search, control, and consequence costs.
- **Moderate-low:** the proposed equations are conceptual models, not validated measurement laws.

## Remaining Uncertainty

The largest unresolved question is quantitative: how much exposure is required for familiarity to overcome specific kinds of design friction, and where does the performance curve plateau? Existing studies establish the mechanisms but do not yet yield a general conversion function from repetitions to perceived intuitiveness across interface classes.

------------------------------------------------------------------------

# Key Findings

- Familiarity is a powerful cause of perceived intuitiveness, not a synonym for it.
- Repetition changes affect, judgment, attention, memory retrieval, action selection, and motor execution.
- A mediocre design can become excellent for experts while remaining poor for novices, infrequent users, and users transferring from another convention.
- Familiarity primarily reduces cognitive and search costs. It does not eliminate structural, physical, temporal, or safety costs.
- Stability has value because it allows habit formation, but stability also accumulates switching costs.
- Convention is shared familiarity. Internal consistency is locally manufactured familiarity.
- “Intuitive” should be replaced in serious evaluation with measurable components: first-use success, learning rate, retained performance, practiced efficiency, error rate, recovery cost, transfer, and subjective fluency.
- Expert performance is not sufficient evidence of good design because experts may have absorbed the cost into training.
- Novel designs should outperform familiar ones by enough to repay migration, retraining, and error costs.

------------------------------------------------------------------------

# Research Log

## Cycle 1: Does Repetition Change Evaluation Without Improving the Object?

### Objective

Determine whether familiarity alone changes preference and perceived quality.

### Hypothesis

Repeated exposure increases liking and subjective ease even when the stimulus itself is unchanged.

### Evidence Found

The mere exposure literature consistently reports that prior exposure can increase preference. Bornstein’s work and later reviews indicate that the effect can occur even when recognition is weak or absent. Processing-fluency research offers a mechanism: repeated stimuli are processed more easily, and that ease is misattributed to positive qualities of the stimulus.

The same family of effects appears beyond liking. Repetition can increase perceived truth, confidence, familiarity, and certainty. This establishes that subjective ease is not a transparent measure of objective quality.

### Evidence Against

The effect is not universal or unlimited. Repetition can create boredom or reactance, and preference depends on stimulus complexity, exposure duration, context, task, and awareness. Some evidence suggests exposure affects salience and evaluative extremity, not only fluency. Familiarity and novelty can both be preferred under different processing conditions.

### Sources

- Bornstein, R. F. “Stimulus Recognition and the Mere Exposure Effect.”
- Zajonc, R. B. “Attitudinal Effects of Mere Exposure.”
- Reber, Schwarz, and Winkielman. Processing fluency theory.
- Mrkva and Van Boven. Salience theory of mere exposure.
- Song et al. Familiarity and novelty in aesthetic preference.

### Analysis

The phrase “repeated use can make an okay design great” has at least a perceptual truth. Repetition can improve evaluation without changing the design. But “great” in this case may mean preferred, comfortable, trusted, or easy-feeling rather than more effective.

This is a critical distinction. Subjective quality is partly a relationship between a design and a trained nervous system. It is not located entirely inside the artifact.

### Conclusion

Familiarity can increase perceived design quality independently of objective design change.

### Confidence

High.

### Next Step

Test whether familiarity also produces actual performance gains rather than only favorable judgments.

------------------------------------------------------------------------

## Cycle 2: Can Practice Make Arbitrary Interaction Efficient?

### Objective

Determine whether repeated use can transform an awkward or arbitrary interaction into fast, low-effort performance.

### Hypothesis

Stable repeated mappings become automatic, reducing attention demand, decision time, and error rate.

### Evidence Found

Schneider and Shiffrin’s controlled-versus-automatic processing research demonstrated that consistent practice can produce automatic attending and response patterns. Automatic processes require less active control and can operate with reduced attentional demand.

Human-computer interaction research on interface habits found that users became faster and more accurate as stable cue-response relationships were repeated. When the familiar mapping was disrupted, the gain disappeared.

The Keystroke-Level Model also distinguishes expert execution from learning and problem solving. Once a method is known, performance can be modeled as a sequence of mental, pointing, homing, and keystroke operators. Practice reduces or reorganizes mental preparation but cannot remove the physical operators still required.

### Evidence Against

Automaticity is strongly dependent on consistency. Variable mappings do not automatize in the same way. Automatic responses can also persist when they are no longer appropriate, producing capture errors. A procedure may become fast but brittle.

### Sources

- Schneider, W., and Shiffrin, R. M. “Controlled and Automatic Human Information Processing.”
- Garaialde et al. “Quantifying the Impact of Making and Breaking Interface Habits.”
- Card, Moran, and Newell. “The Keystroke-Level Model for User Performance Time with Interactive Systems.”
- Freed and Shafto. Habit-capture modeling in air traffic control.

### Analysis

Practice does not merely change opinion. It changes the operating system of behavior. Users stop solving the interface as a fresh problem and begin executing compiled routines.

This means an arbitrary design can become genuinely fast. QWERTY-like persistence does not require the design to be best in the abstract. It only requires sufficiently stable practice, adequate performance, and high switching costs.

However, the gain belongs to the combined system of user plus interface. Calling the interface itself intuitive erases the training contribution.

### Conclusion

Repeated use can turn arbitrary but stable mappings into efficient expert behavior.

### Confidence

High.

### Next Step

Determine what practice cannot repair.

------------------------------------------------------------------------

## Cycle 3: What Costs Remain After Familiarity?

### Objective

Identify design defects that persist even after users become highly practiced.

### Hypothesis

Familiarity primarily reduces interpretation, search, and decision costs, but cannot eliminate physical, temporal, structural, or consequence costs.

### Evidence Found

Fitts’s law relates movement time to target distance and size. Practice can improve the constants and movement strategy, but a small distant target remains more demanding than a large nearby target under otherwise comparable conditions.

Hick’s work relates choice reaction time to uncertainty among response alternatives. Familiarity and unequal probabilities can reduce effective uncertainty, but unnecessary alternatives still impose costs, particularly in uncommon or changing tasks.

The Keystroke-Level Model shows that expert performance remains constrained by the count and duration of physical and system operators. A six-step process does not become a two-step process merely because the six steps are automatic.

ISO 9241-11 defines usability through effectiveness, efficiency, satisfaction, and context of use. Subjective comfort is therefore insufficient. Safety-oriented human-factors guidance similarly requires testing for use errors and real-world conditions rather than assuming training solves interface risk.

### Evidence Against

Experts can chunk multiple actions into a single perceived unit, use shortcuts, anticipate response delays, and exploit stable spatial memory. Consequently, the felt cost of a long sequence can become much lower than its literal step count suggests.

### Sources

- Fitts, P. M. “The Information Capacity of the Human Motor System in Controlling the Amplitude of Movement.”
- Hick, W. E. “On the Rate of Gain of Information.”
- Card, Moran, and Newell. Keystroke-Level Model.
- ISO 9241-11:2018.
- U.S. Consumer Product Safety Commission Human Factors Standard Practice.

### Analysis

Familiarity is strongest against cognitive overhead. It reduces questions such as:

- What does this mean?
- Where is the control?
- What happens next?
- Which sequence should I use?

It is weaker or powerless against:

- How far must I move?
- How many mandatory actions exist?
- How long must the system respond?
- Can I perceive the current state?
- What happens if I slip?

This yields a useful boundary: familiarity can optimize the execution of a method, but it cannot necessarily improve the method’s structural efficiency or safety.

### Conclusion

Practice can hide but not erase many forms of design friction.

### Confidence

High.

### Next Step

Investigate whether any interaction can be intuitive without prior cultural familiarity.

------------------------------------------------------------------------

## Cycle 4: Is First-Use Intuition Possible?

### Objective

Test the strongest version of the slogan: that all intuitive behavior is learned familiarity.

### Hypothesis

Some successful first-use interaction can be explained by physical compatibility, affordances, constraints, and structural mappings rather than prior familiarity with a specific convention.

### Evidence Found

Human-factors research on display-control compatibility shows that spatial and directional relationships affect response time and accuracy. Controls arranged to correspond with controlled objects reduce the need for arbitrary memory.

Norman’s account of signifiers, mapping, constraints, and feedback describes how knowledge can be placed in the world rather than in the user’s memory. A control can expose the action it supports and the relation between action and effect.

Fitts’s law and basic perceptual-motor constraints apply without requiring users to learn a cultural symbol. Large, close, visually distinct targets are easier to acquire because of human motor and perceptual characteristics.

Scene-perception research also suggests that actionable possibilities, or affordances, provide strong structure for categorization.

### Evidence Against

Many apparently natural mappings are partly cultural. Direction-of-turn stereotypes vary with display arrangement, control plane, population, and learned standards. Even doors and controls rely on prior experience with handles, plates, hinges, and mechanical causality.

The clean separation between biology and culture is therefore difficult. Humans encounter physical regularities repeatedly from infancy, so some “natural” mappings may be extremely early familiarity.

### Sources

- Tsang. “Interface Design and Display-Control Compatibility.”
- Vu et al. “Population Stereotypes for Objects and Representations.”
- Norman. *The Design of Everyday Things*.
- Greene et al. “Affordances Provide a Fundamental Categorization Principle for Visual Scenes.”
- Shinar and Acton. Control-display linkage stereotypes.

### Analysis

The evidence does not support a pure innate-versus-learned binary. First-use success can arise from three layers:

1. **Species constraints:** perception and movement properties shared broadly by humans.
2. **World regularities:** learned through lifelong interaction with physical environments.
3. **Cultural conventions:** learned through exposure to a design ecosystem.

All three can feel immediate because none requires conscious reasoning at the moment of use.

The slogan fails if “familiar” means prior exposure to the exact interface. It becomes more defensible if familiarity is expanded so broadly that it includes embodied learning of gravity, causality, containment, and spatial correspondence. But at that point it explains almost everything and loses practical precision.

### Conclusion

Some first-use fluency is not specific-interface familiarity, though it may still depend on deep embodied and cultural learning.

### Confidence

Moderate-high.

### Next Step

Examine the costs and risks created by familiarity itself.

------------------------------------------------------------------------

## Cycle 5: When Does Familiarity Become a Liability?

### Objective

Determine whether familiar interaction patterns can reduce performance or safety.

### Hypothesis

Strong habits create brittle automatic responses, negative transfer, capture errors, and resistance to superior alternatives.

### Evidence Found

Habit-capture research describes cases where a frequent action overrides an intended but less common action, especially when contexts share cues. Human-error research distinguishes slips from mistakes and emphasizes that system design must anticipate predictable human behavior.

Interface habit experiments show that disrupting a learned mapping eliminates prior speed and accuracy gains. Nielsen Norman Group reports similar usability findings: variations in practiced patterns cause errors because users act from expectation rather than re-reading every interface.

Population stereotypes can conflict with local standards. A user trained on one mapping may perform worse than a novice when moved to a superficially similar but differently mapped system.

### Evidence Against

Habits are not inherently harmful. They are the mechanism that makes frequent work efficient. The problem is not habit but unstable or safety-critical contexts in which automatic responses can be triggered incorrectly.

### Sources

- Reason, J. “Human Error: Models and Management.”
- Freed and Shafto. Predicting habit capture.
- Garaialde et al. Interface habits.
- Nielsen Norman Group. “Variations on Practiced Patterns Cause Mistakes.”
- Human-factors literature on population stereotypes.

### Analysis

Familiarity produces a trade:

- It lowers the recurring cost of expected actions.
- It raises the disruption cost of unexpected changes.

This means familiarity should be treated as stored behavioral capital. Stable conventions accumulate capital. Redesign can destroy it.

But the same capital can become a liability when the environment changes. The more automatic the behavior, the less likely the user is to inspect the interface before acting.

### Conclusion

Familiarity improves routine performance while increasing change sensitivity and certain classes of error.

### Confidence

High.

### Next Step

Investigate whether familiarity can preserve objectively inferior designs through economic and network mechanisms.

------------------------------------------------------------------------

## Cycle 6: Why Do Mediocre Familiar Designs Persist?

### Objective

Explain why a design that is not structurally optimal may dominate for long periods.

### Hypothesis

Installed user knowledge, social coordination, training materials, interoperability, and migration risk create increasing returns to familiarity.

### Evidence Found

The cognitive evidence establishes individual switching costs: practiced mappings are faster, and disruption removes the gain. Standards and conventions add collective value because users can transfer knowledge across products. The more products share a pattern, the more valuable that pattern becomes.

This resembles path dependence in economics and network effects in technology markets. The value of a convention is partly endogenous to adoption. A technically superior alternative must overcome not only feature comparison but retraining, documentation, support, error, and coordination costs.

### Evidence Against

Familiar conventions do get replaced when the benefit is large enough, migration is gradual, compatibility layers are provided, or the old system becomes unsustainable. Familiarity creates inertia, not permanence.

### Sources

- Interface habit and automaticity research.
- ISO and usability standards emphasizing context.
- Nielsen Norman Group guidance on consistency and standards.
- Historical examples from keyboards, command systems, operating systems, and industrial controls, used here as interpretive rather than decisive evidence.

### Analysis

A mediocre standard can become locally optimal because the environment has adapted around it. Training, tools, mental models, and complementary products all encode the convention.

This explains why redesign arguments based solely on reduced click count often fail. The redesign must repay a migration balance sheet:

- learning cost
- temporary productivity loss
- error spike
- support cost
- documentation change
- cross-system inconsistency
- loss of expert shortcuts
- emotional distrust caused by reduced fluency

### Conclusion

Familiarity is not only psychological. At scale it becomes infrastructure.

### Confidence

Moderate-high.

### Next Step

Develop a model separating intrinsic design quality from familiarity-dependent performance.

------------------------------------------------------------------------

## Cycle 7: Can “Intuitiveness” Be Decomposed Into Measurable Parts?

### Objective

Replace the vague adjective with a testable model.

### Hypothesis

Perceived intuitiveness can be approximated by the inverse of four experienced costs: prediction, search, control, and consequence uncertainty.

### Evidence Found

The reviewed literature repeatedly converges on these mechanisms:

- Processing fluency affects evaluation.
- Consistent practice reduces controlled processing.
- Hick-type uncertainty affects selection time.
- Fitts-type constraints affect movement time.
- Mapping and signifiers reduce inference and search.
- Feedback reduces state uncertainty.
- Error-prevention research separates successful completion from comfortable feeling.

### Evidence Against

The variables are not independent. Familiarity changes search strategy, prediction, motor preparation, and confidence simultaneously. Emotional state, motivation, stakes, accessibility, and context also alter the experience.

### Analysis

A practical decomposition is:

**Perceived Intuitiveness ≈ Prediction Success + Processing Fluency + Action Confidence − Conscious Effort**

A more diagnostic cost model is:

**Interaction Cost = Search Cost + Interpretation Cost + Decision Cost + Motor Cost + System Delay + Error Risk + Recovery Cost**

Familiarity has large effects on the first three, moderate effects on motor preparation and error likelihood in stable contexts, and limited direct effects on system delay or inherent recovery structure.

### Conclusion

“Intuitive” should be treated as a compressed user judgment generated by multiple underlying variables.

### Confidence

Moderate-high.

### Next Step

Attempt to falsify the emerging model with counterexamples.

------------------------------------------------------------------------

## Cycle 8: Falsification Through Counterexamples

### Objective

Search for cases that break the proposed relationship between familiarity, fluency, and quality.

### Hypothesis

If familiarity is only a multiplier, then some familiar designs should remain poor and some unfamiliar designs should succeed immediately.

### Evidence Found

Familiar but poor:

- Repeated false statements become more believable without becoming true.
- Habitual actions can generate capture errors.
- Familiar interfaces can preserve hidden modes or dangerous mappings.
- Expert users can be fast on systems that remain inaccessible to novices.

Unfamiliar but successful:

- Spatially compatible controls can be understood through visible mapping.
- Large, close targets are easier without special training.
- Constraints can make incorrect actions impossible.
- Immediate feedback can support exploration and correction.

### Evidence Against

Even the unfamiliar-success cases rely on prior knowledge of physical causality, objects, reading direction, or interaction vocabulary. Total novelty is nearly impossible to construct for an adult participant.

### Analysis

The model survives if it avoids claiming that familiarity and quality are independent. They interact. Good structure accelerates learning; familiarity amplifies stable structure; poor structure increases the training required; habit can compensate until the context changes.

### Conclusion

The strongest slogan is falsified. The weaker interaction model remains supported.

### Confidence

High for rejection of the literal slogan; Moderate-high for the interaction model.

### Next Step

Translate findings into candidate laws and practical evaluation methods.

------------------------------------------------------------------------

# Confirmed Findings

## CF-001: Repetition Can Increase Preference Without Changing Objective Properties

Repeated exposure can alter liking and evaluative judgment. Therefore, user preference after prolonged exposure is not clean evidence that the design’s structural properties are superior.

**Confidence:** High.

## CF-002: Practice Can Produce Automatic Interaction

Consistent cue-response mappings can become faster, more accurate, and less attention-demanding with practice.

**Confidence:** High.

## CF-003: Familiarity Is Context- and Population-Dependent

A pattern can be familiar to one group and opaque to another. “Intuitive” without a specified user population is therefore incomplete.

**Confidence:** High.

## CF-004: Familiarity Can Create Errors When Context Changes

Automatic responses can be misapplied after redesign, transfer, mode changes, or rare exceptions.

**Confidence:** High.

## CF-005: Subjective Fluency Is Not Equivalent to Usability

A system can feel easy while performing poorly on effectiveness, safety, recovery, or accessibility. Standards explicitly treat usability as contextual and multidimensional.

**Confidence:** High.

## CF-006: Some Design Costs Persist Under Expertise

Physical target acquisition, required action count, system delay, and absent feedback cannot be fully removed by learning.

**Confidence:** High.

## CF-007: Consistency Has Compounding Value

Stable patterns allow users to amortize learning across repetitions and products. Consistency is therefore not merely aesthetic uniformity. It is a mechanism for accumulating behavioral efficiency.

**Confidence:** High.

## CF-008: Expert Observation Can Underestimate Design Friction

Experts may have memorized locations, commands, exceptions, and workarounds. Their performance does not reveal the original learning cost or transfer difficulty.

**Confidence:** Moderate-high.

------------------------------------------------------------------------

# Rejected Hypotheses

## RH-001: All Intuitive Design Is Merely Familiar Design

### Why Rejected

First-use success can be supported by visible mapping, constraints, feedback, perceptual salience, and physical compatibility. These do not require familiarity with the exact artifact or convention.

### Residual Truth

Most adult first-use behavior still draws on prior embodied and cultural learning. “Novel” interfaces are rarely cognitively blank.

## RH-002: Repetition Makes Any Design Good

### Why Rejected

Practice can improve execution of a method but cannot remove unnecessary actions, system latency, inaccessible targets, missing feedback, or catastrophic consequences of slips.

### Residual Truth

Repeated use can make many poor features less noticeable and can make arbitrary mappings operationally efficient.

## RH-003: Familiarity Only Changes Preference, Not Performance

### Why Rejected

Automaticity and interface-habit research show real gains in speed and accuracy.

## RH-004: A Familiar Design Is Always Safer

### Why Rejected

Familiarity can reduce routine errors but increase capture errors, complacency, mode mistakes, and negative transfer when familiar cues trigger the wrong response.

## RH-005: Convention Is Evidence of Optimality

### Why Rejected

Conventions may persist because of installed knowledge, coordination benefits, path dependence, and switching costs. Survival demonstrates fitness within a historical ecosystem, not global optimality.

## RH-006: Novelty and Familiarity Are Simple Opposites

### Why Rejected

Preference can favor familiarity under low-effort automatic processing and novelty under active exploration. Complex designs may require a balance: enough familiarity for orientation, enough novelty for information or interest.

------------------------------------------------------------------------

# Emerging Patterns

## EP-001: Intuitiveness Is a Prediction Experience

An interaction feels intuitive when the user’s predictions are repeatedly confirmed with little conscious effort. Familiarity improves prediction because the user already possesses a model. Good mapping improves prediction because the interface exposes the model.

**Why it matters:** This unifies convention-based and structure-based intuition without treating them as identical.

## EP-002: Familiarity Transfers Cost From Runtime to Training

A poor design can appear efficient at runtime because its cost was paid earlier through learning and repetition.

**Why it matters:** Design comparisons must include acquisition cost, not only expert task time.

## EP-003: Stability Compounds, Change Liquidates

Stable layouts and mappings accumulate procedural memory. Redesign partially liquidates that stored value.

**Why it matters:** Redesign should be evaluated as an investment decision, not a visual refresh.

## EP-004: The Better the Habit, the More Dangerous the Exception

Strong automaticity improves frequent cases while increasing the chance that rare deviations will be missed.

**Why it matters:** Safety-critical systems should make exceptional states perceptually distinct and may need deliberate friction.

## EP-005: Familiarity Can Mask Structural Inefficiency

Users stop noticing arbitrary sequences once they become chunks. This produces a ceiling on introspective feedback: experts may no longer be able to explain what was hard to learn.

**Why it matters:** Retrospective interviews with experts are insufficient for novice usability assessment.

## EP-006: Convention Is Distributed Memory

A shared icon, layout, or gesture stores knowledge outside any single product. Designers who follow it borrow training from the surrounding ecosystem.

**Why it matters:** Breaking convention spends collective learning capital and should require measurable benefit.

## EP-007: “Natural” Often Means Deeply Learned

Physical-world regularities are learned so early and broadly that they feel innate. The practical distinction is not innate versus learned, but how widely and robustly the knowledge transfers.

**Why it matters:** Designers should prefer mappings grounded in broad, durable experience over narrow product-specific conventions.

------------------------------------------------------------------------

# Proposed Models

## LAW-IF-001: Familiarity Attribution Law

### Hypothesis

When an interaction is processed fluently and produces expected outcomes, users tend to attribute the ease to the design rather than to their own prior learning.

### Prediction

Two users with different exposure histories will rate the same interface differently on “intuitiveness,” even when objective task structure is identical.

### Supporting Evidence

Processing fluency, mere exposure, illusory truth, and interface habit research.

### Counter Evidence

Users can sometimes correctly identify that familiarity is the cause, particularly after explicit comparison or training disruption.

### Confidence

High as a qualitative law; unvalidated quantitatively.

------------------------------------------------------------------------

## LAW-IF-002: Familiarity Compensation Law

### Hypothesis

Repeated stable use can compensate for arbitrary mapping and poor discoverability, but compensation declines as structural, physical, temporal, or consequence costs increase.

### Prediction

Practice will produce large gains for location memory and command selection, smaller gains for small distant targets, and minimal gains for system latency or missing feedback.

### Supporting Evidence

Automaticity, KLM, Fitts, and human-factors evidence.

### Counter Evidence

Experts can sometimes restructure tasks with shortcuts or tools, indirectly changing structural costs.

### Confidence

Moderate-high.

------------------------------------------------------------------------

## LAW-IF-003: Habit Fragility Law

### Hypothesis

The performance advantage created by a stable interface habit is proportional to the disruption cost when the cue-response mapping changes.

### Prediction

The most frequently practiced controls will show the largest temporary error and latency increase after relocation or remapping.

### Supporting Evidence

Interface habit disruption and capture-error research.

### Counter Evidence

Highly explicit change signaling, retraining, dual support, and reversible customization may reduce disruption.

### Confidence

High qualitatively.

------------------------------------------------------------------------

## LAW-IF-004: Convention Investment Law

### Hypothesis

The burden of proof for breaking a convention should rise with the size, frequency, and cross-context portability of the installed user habit.

### Prediction

Replacing a high-frequency cross-application pattern requires a larger performance benefit than replacing a rare product-specific pattern.

### Supporting Evidence

Automaticity, consistency, transfer, and switching-cost evidence.

### Counter Evidence

A convention with severe safety or accessibility defects may warrant replacement despite high installed familiarity.

### Confidence

Moderate-high.

------------------------------------------------------------------------

## LAW-IF-005: Exceptional-State Distinction Law

### Hypothesis

When a familiar action becomes inappropriate in a rare state, the interface must make that state more perceptually distinct as habit strength and consequence severity increase.

### Prediction

Error rates will decline when exceptional modes alter multiple salient cues rather than relying on a small label or memory-based warning.

### Supporting Evidence

Habit capture, mode errors, human-error systems theory, and safety guidance.

### Counter Evidence

Excessive visual change may itself disrupt orientation or create alarm fatigue.

### Confidence

Moderate-high.

------------------------------------------------------------------------

## MODEL-IF-001: The Familiarity-Adjusted Design Quality Model

A design should be evaluated at three exposure states:

### State A: Encounter Quality

Performance with little or no product-specific learning.

Measures:

- first-attempt completion
- time to discover action
- interpretation accuracy
- reliance on help
- initial error severity

### State B: Acquisition Quality

How rapidly stable competence develops.

Measures:

- learning-curve slope
- repetitions to criterion
- retention after delay
- transfer from related products
- number of concepts or exceptions to memorize

### State C: Practiced Quality

Performance after habit formation.

Measures:

- expert task time
- throughput
- attention demand
- error rate
- recovery time
- shortcut availability

A design can score differently at each state. This creates a more useful taxonomy than “intuitive” versus “not intuitive.”

------------------------------------------------------------------------

## MODEL-IF-002: Interaction Cost Decomposition

For a task *t*, user *u*, design *d*, and exposure level *e*:

**Total Interaction Cost**

`C(t,u,d,e) = Cs + Ci + Cd + Cm + Cl + Cr`

Where:

- `Cs` = search cost
- `Ci` = interpretation and prediction cost
- `Cd` = decision cost
- `Cm` = motor and physical cost
- `Cl` = latency and waiting cost
- `Cr` = expected error and recovery cost

Familiarity primarily reduces `Cs`, `Ci`, and `Cd`. It can reduce portions of `Cm` through motor learning and portions of `Cr` in stable routine conditions. It has little direct effect on `Cl` and may increase `Cr` after unexpected change.

### Assumptions

- Costs can be estimated separately enough to guide diagnosis.
- Exposure affects more than one term.
- Context and consequence severity alter the weighting.

### Status

Conceptual model requiring validation.

------------------------------------------------------------------------

## MODEL-IF-003: Redesign Break-Even Model

A redesign is justified when:

`Long-term Benefit > Migration Cost + Habit Loss + Transition Error Cost + Ecosystem Inconsistency`

More explicitly:

`N × F × ΔC × H > Ct + Ce + Cs + Cd`

Where:

- `N` = number of affected users
- `F` = future task frequency per user
- `ΔC` = expected recurring cost reduction per task
- `H` = expected remaining useful life
- `Ct` = training and communication cost
- `Ce` = transition error cost
- `Cs` = support and documentation cost
- `Cd` = disruption of cross-product consistency

### Implication

Small per-task improvements can justify migration for extremely frequent tasks. Large visual novelty with no measurable recurring gain usually cannot.

### Status

Economic decision model, not yet empirically calibrated.

------------------------------------------------------------------------

## MODEL-IF-004: Familiarity Classes

### F0: Unknown

No recognizable model, convention, or physical analogy.

### F1: Structurally Inferable

No direct prior use, but visible mapping, constraints, or affordances support prediction.

### F2: Analogically Familiar

Resembles another known system, though details differ.

### F3: Conventionally Familiar

Uses a widely shared cultural or platform pattern.

### F4: Product Familiar

User knows the specific product and layout.

### F5: Procedurally Automatic

Cue-response sequence is habitual and requires little conscious control.

### Why This Matters

“Familiar” is not binary. A design may be new at the product level while familiar at the structural or conventional level. This taxonomy supports more precise research and testing.

------------------------------------------------------------------------

# Observations

## OBS-001

### Observation

Users commonly use “intuitive” as a global adjective even though measured performance can differ across first use, learning, and expert use.

### Interpretation

The word compresses multiple dimensions and obscures tradeoffs.

### Confidence

High.

## OBS-002

### Observation

Repetition can increase liking and truth judgments without changing stimulus quality or factual accuracy.

### Interpretation

Fluency is used as a heuristic signal and can be misattributed.

### Confidence

High.

## OBS-003

### Observation

Stable mappings produce automaticity; changed mappings eliminate part of the gain.

### Interpretation

Consistency accumulates procedural value and redesign destroys some of it.

### Confidence

High.

## OBS-004

### Observation

Experts can execute arbitrary sequences rapidly.

### Interpretation

Expert speed alone cannot distinguish good structure from absorbed training cost.

### Confidence

High.

## OBS-005

### Observation

Physical compatibility and visible structure can improve first-use performance.

### Interpretation

Prior exposure to the exact design is not the only path to low-effort interaction.

### Confidence

Moderate-high.

## OBS-006

### Observation

Strong habits can generate errors when a rare state or changed interface requires a different action.

### Interpretation

The optimization for common cases can create vulnerability in exceptional cases.

### Confidence

High.

------------------------------------------------------------------------

# Evidence

## EVD-001

### Citation

Bornstein, R. F. (1992). “Stimulus Recognition and the Mere Exposure Effect.” *Journal of Personality and Social Psychology*.

### Summary

Exposure effects can occur even when explicit recognition is weak, supporting a distinction between familiarity-driven evaluation and conscious memory.

### Supports

- LAW-IF-001
- CF-001

### Challenges

- RH-003

## EVD-002

### Citation

Hassan, A., and Barber, S. J. (2021). “The Effects of Repetition Frequency on the Illusory Truth Effect.”

### Summary

Repeated information becomes easier to process and is more likely to be judged true, illustrating that fluency can be mistaken for an objective property.

### Supports

- LAW-IF-001
- EP-001

### Challenges

- Any inference that comfortable feeling reliably indicates quality

## EVD-003

### Citation

Schneider, W., and Shiffrin, R. M. (1977). “Controlled and Automatic Human Information Processing.” *Psychological Review*.

### Summary

Consistent practice can convert controlled attention-demanding processing into automatic processing.

### Supports

- LAW-IF-002
- CF-002
- EP-002

### Challenges

- RH-003

## EVD-004

### Citation

Garaialde, D., et al. (2020). “Quantifying the Impact of Making and Breaking Interface Habits.”

### Summary

Users became faster and more accurate as interface habits formed, and performance gains disappeared when mappings were disrupted.

### Supports

- LAW-IF-003
- CF-004
- EP-003

### Challenges

- RH-004

## EVD-005

### Citation

Card, S. K., Moran, T. P., and Newell, A. (1980). “The Keystroke-Level Model for User Performance Time with Interactive Systems.” *Communications of the ACM*.

### Summary

Expert execution time can be decomposed into mental and physical operators, showing that familiarity can reduce mental preparation but does not erase every action cost.

### Supports

- LAW-IF-002
- MODEL-IF-002

### Challenges

- RH-002

## EVD-006

### Citation

Hick, W. E. (1952). “On the Rate of Gain of Information.” *Quarterly Journal of Experimental Psychology*.

### Summary

Choice reaction time varies with response uncertainty, providing a measurable basis for one component of decision friction.

### Supports

- MODEL-IF-002
- EP-001

### Challenges

- A unitary model of intuitiveness

## EVD-007

### Citation

Fitts, P. M. (1954). “The Information Capacity of the Human Motor System in Controlling the Amplitude of Movement.” *Journal of Experimental Psychology*.

### Summary

Movement time depends on target distance and size, demonstrating an interaction cost not reducible to semantic familiarity.

### Supports

- LAW-IF-002
- MODEL-IF-002

### Challenges

- RH-002

## EVD-008

### Citation

ISO 9241-11:2018. *Ergonomics of Human-System Interaction — Usability: Definitions and Concepts*.

### Summary

Usability is contextual and includes effectiveness, efficiency, and satisfaction. Felt intuitiveness is therefore only one possible indicator.

### Supports

- CF-005
- MODEL-IF-001

### Challenges

- Treating subjective preference as total design quality

## EVD-009

### Citation

Norman, D. A. (2013). *The Design of Everyday Things*, revised edition.

### Summary

Signifiers, mappings, constraints, conceptual models, and feedback can place knowledge in the world and improve discoverability.

### Supports

- F1 familiarity class
- EP-001

### Challenges

- RH-001

## EVD-010

### Citation

Reason, J. (2000). “Human Error: Models and Management.” *BMJ*.

### Summary

Errors must be understood through system conditions as well as individual actions. Familiar expertise does not eliminate predictable slips and latent design hazards.

### Supports

- LAW-IF-005
- CF-004

### Challenges

- RH-004

## EVD-011

### Citation

Tsang, S. N. H. (2015). “Interface Design and Display-Control Compatibility.”

### Summary

Compatibility between displays and controls improves operational effectiveness and safety, especially under emergency conditions.

### Supports

- RH-001 rejection
- F1 familiarity class

### Challenges

- Pure convention explanation

## EVD-012

### Citation

Nielsen Norman Group. “Maintain Consistency and Adhere to Standards” and “Variations on Practiced Patterns Cause Mistakes.”

### Summary

External and internal consistency allow users to apply existing knowledge; deviations from practiced patterns induce errors.

### Supports

- LAW-IF-003
- LAW-IF-004
- EP-006

### Challenges

- Novelty without migration justification

------------------------------------------------------------------------

# Open Questions

## OQ-001: What Is the Exposure-to-Fluency Function?

**Importance:** Very high.

How many repetitions are required for different interaction classes to reach 50%, 80%, and 95% of expert performance? The answer likely depends on mapping consistency, task complexity, spacing of practice, feedback quality, and prior analogies.

## OQ-002: How Much Structural Badness Can Familiarity Compensate For?

**Importance:** Very high.

A controlled program could vary action count, target difficulty, mapping arbitrariness, and feedback while tracking learning curves. This would reveal where practice plateaus below acceptable performance.

## OQ-003: Can We Separate Familiarity From Good Mapping Experimentally?

**Importance:** High.

Compare four conditions:

1. familiar and well mapped
2. familiar and poorly mapped
3. unfamiliar and well mapped
4. unfamiliar and poorly mapped

Measure first-use success, learning slope, retention, transfer, and preference.

## OQ-004: How Long Does Habit Debt Persist After Redesign?

**Importance:** High.

Measure the duration and severity of post-change performance loss, including delayed capture errors after apparent retraining.

## OQ-005: Does Familiarity Affect Accessibility Groups Differently?

**Importance:** High.

A pattern familiar to sighted mouse users may not transfer to screen-reader, switch-control, motor-impaired, or cognitively impaired users. The “installed habit” may be population-specific.

## OQ-006: When Is Deliberate Friction Better Than Intuitive Flow?

**Importance:** Medium-high.

High-consequence actions may benefit from interruption, confirmation, or mode distinction. The optimal friction likely scales with consequence severity and reversibility.

## OQ-007: Can Subjective Intuitiveness Be Debiased?

**Importance:** Medium.

Would asking users to report prior exposure, or comparing after a delay, reduce familiarity attribution errors?

## OQ-008: What Is the Optimal Familiarity-Novelty Ratio in Composition?

**Importance:** Medium.

In visual and auditory aesthetics, complete predictability can become boring while complete novelty becomes difficult. This may connect interface intuition to broader composition laws involving prediction, compression, and surprise.

------------------------------------------------------------------------

# Recommendations

## Priority 1: Replace “Intuitive” With an Exposure-Indexed Scorecard

### Action

For every design evaluation, report separate measures for:

- first encounter
- early learning
- delayed retention
- expert routine use
- transfer after change

### Expected Value

Very high.

### Effort

Low to moderate.

### Rationale

This prevents expert fluency from being mistaken for first-use clarity and exposes where the design’s cost is being paid.

## Priority 2: Build a Familiarity-Controlled Experiment Library

### Action

Use existing published datasets where possible and add lightweight remote experiments only where evidence is missing. Manipulate convention match, mapping quality, repetition count, and change disruption.

### Expected Value

Very high.

### Effort

Moderate to high.

### Rationale

The project needs quantitative learning curves, not more slogans.

## Priority 3: Add Familiarity as a Node in the Composition Genome

### Action

Represent familiarity as a relational variable connected to:

- processing fluency
- predictability
- repetition
- convention
- consistency
- automaticity
- preference
- trust
- error risk
- novelty
- switching cost

### Expected Value

High.

### Effort

Moderate.

### Rationale

Familiarity is not an isolated principle. It modifies perception, action, evaluation, and system economics.

## Priority 4: Create a Redesign Migration Calculator

### Action

Operationalize MODEL-IF-003 using task frequency, affected population, measured time savings, training costs, support costs, and error consequences.

### Expected Value

High for real product decisions.

### Effort

Moderate.

### Rationale

This turns debates about “modernizing” familiar systems into explicit break-even analysis.

## Priority 5: Study Exceptional-State Design

### Action

Review aviation, medical device, nuclear control, automotive, and industrial safety research for ways to interrupt habit safely when routine actions become inappropriate.

### Expected Value

High.

### Effort

Moderate.

### Rationale

The highest-value contradiction in the current theory is that the same automaticity that makes a design feel intuitive can create catastrophic rare-state errors.

## Priority 6: Develop a “Training Cost Hidden in the User” Audit

### Action

During design review, list every fact, location, sequence, exception, and workaround users must memorize. Treat these as part of the design’s total cost.

### Expected Value

High.

### Effort

Low.

### Rationale

It prevents organizations from crediting the product for competence that users laboriously supplied.

------------------------------------------------------------------------

# Candidate Practical Rules

## PR-001

Do not call a design intuitive unless the exposure history of the evaluated users is known.

## PR-002

Do not use expert speed as evidence of discoverability.

## PR-003

Preserve high-frequency conventions unless a measurable recurring benefit repays migration cost.

## PR-004

Break convention more readily for rare, low-transfer, low-consequence patterns than for frequent ecosystem-wide patterns.

## PR-005

Make exceptional states visually and behaviorally distinct enough to overcome habitual action.

## PR-006

When a design feels good after repetition, separately measure whether it is effective, efficient, safe, accessible, and recoverable.

## PR-007

Prefer broadly transferable mappings over product-specific memorization.

## PR-008

Treat internal consistency as a way of manufacturing future familiarity.

## PR-009

Treat every redesign as both a design intervention and a behavioral retraining event.

## PR-010

Measure the area under the learning curve, not only the final plateau.

------------------------------------------------------------------------

# Bibliography

## Academic

- Bornstein, R. F. (1992). “Stimulus Recognition and the Mere Exposure Effect.” *Journal of Personality and Social Psychology*. https://pubmed.ncbi.nlm.nih.gov/1447685/
- Fitts, P. M. (1954). “The Information Capacity of the Human Motor System in Controlling the Amplitude of Movement.” *Journal of Experimental Psychology*, 47(6), 381–391.
- Garaialde, D., Bowers, C. P., Pinder, C., Shah, P., Parashar, S., Clark, L., and Cowan, B. R. (2020). “Quantifying the Impact of Making and Breaking Interface Habits.” https://arxiv.org/abs/2005.06842
- Greene, M. R., Baldassano, C., Esteva, A., Beck, D. M., and Fei-Fei, L. (2014). “Affordances Provide a Fundamental Categorization Principle for Visual Scenes.” https://arxiv.org/abs/1411.5340
- Hassan, A., and Barber, S. J. (2021). “The Effects of Repetition Frequency on the Illusory Truth Effect.” https://pmc.ncbi.nlm.nih.gov/articles/PMC8116821/
- Hick, W. E. (1952). “On the Rate of Gain of Information.” *Quarterly Journal of Experimental Psychology*, 4, 11–26. https://doi.org/10.1080/17470215208416600
- Mrkva, K., and Van Boven, L. (2020). “Salience Theory of Mere Exposure.” https://pubmed.ncbi.nlm.nih.gov/31971441/
- Proctor, R. W., and Schneider, D. W. (2018). “Hick’s Law for Choice Reaction Time: A Review.” *Quarterly Journal of Experimental Psychology*. https://pubmed.ncbi.nlm.nih.gov/28434379/
- Reason, J. (2000). “Human Error: Models and Management.” *BMJ*. https://pmc.ncbi.nlm.nih.gov/articles/PMC1117770/
- Reber, R., and Unkelbach, C. (2010). “The Epistemic Status of Processing Fluency as Source for Judgments of Truth.” https://pmc.ncbi.nlm.nih.gov/articles/PMC3339024/
- Schneider, W., and Shiffrin, R. M. (1977). “Controlled and Automatic Human Information Processing: II. Perceptual Learning, Automatic Attending, and a General Theory.” *Psychological Review*, 84(2), 127–190. https://psych.indiana.edu/documents/shiffrin-and-schneider-1977.pdf
- Song, J., et al. (2021). “Familiarity and Novelty in Aesthetic Preference.” https://pmc.ncbi.nlm.nih.gov/articles/PMC8345014/
- Vu, K. P. L., et al. (2019). “Population Stereotypes for Objects and Representations.” https://pubmed.ncbi.nlm.nih.gov/30689448/

## Books

- Card, S. K., Moran, T. P., and Newell, A. (1983). *The Psychology of Human-Computer Interaction*. Lawrence Erlbaum Associates.
- Norman, D. A. (2013). *The Design of Everyday Things*, revised and expanded edition. Basic Books.
- Reason, J. (1990). *Human Error*. Cambridge University Press.

## Industry

- Nielsen Norman Group. “Maintain Consistency and Adhere to Standards.” https://www.nngroup.com/articles/consistency-and-standards/
- Nielsen Norman Group. “Variations on Practiced Patterns Cause Mistakes.” https://www.nngroup.com/articles/practiced-patterns-mistakes/
- Nielsen Norman Group. “Match Between the System and the Real World.” https://www.nngroup.com/articles/match-system-real-world/

## Standards

- ISO 9241-11:2018. *Ergonomics of Human-System Interaction — Part 11: Usability: Definitions and Concepts*. https://www.iso.org/obp/ui/
- U.S. Consumer Product Safety Commission. *Human Factors Standard Practice*. https://www.cpsc.gov/s3fs-public/Human-Factors-Standard-Practice-Document-Final-ENGLISH-Feb03-2020_0.pdf

## Historical

- Card, S. K., Moran, T. P., and Newell, A. (1980). “The Keystroke-Level Model for User Performance Time with Interactive Systems.” *Communications of the ACM*, 23(7), 396–410. https://iiif.library.cmu.edu/file/Newell_box00072_fld05090_doc0005/Newell_box00072_fld05090_doc0005.pdf
- Zajonc, R. B. (1968). “Attitudinal Effects of Mere Exposure.” *Journal of Personality and Social Psychology Monograph Supplement*, 9(2), 1–27.

## Other

- Freed, M., and Shafto, M. G. “A Conceptual Framework for Predicting Error in Complex Human-Machine Environments.” NASA. https://ntrs.nasa.gov/api/citations/20020064619/downloads/20020064619.pdf
- Tsang, S. N. H. (2015). “Interface Design and Display-Control Compatibility.” https://journals.sagepub.com/doi/10.1177/0020294015569264

------------------------------------------------------------------------

# Next Actions

1. Add Familiarity, Automaticity, Convention, Switching Cost, and Habit Capture nodes to the Composition Genome.
2. Build a source extraction table containing sample size, task type, repetitions, effect direction, and measured outcome for the strongest studies.
3. Run a second research pass focused on quantitative learning curves and power-law-of-practice evidence.
4. Investigate safety-critical exception design in aviation, medicine, automotive controls, and industrial systems.
5. Develop an exposure-indexed evaluation worksheet for use in future design audits.

------------------------------------------------------------------------

# Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 1.0 | 2026-07-19 | OpenAI autonomous research agent | Initial autonomous research report; eight investigation cycles, falsification, models, laws, recommendations, and bibliography. |

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
