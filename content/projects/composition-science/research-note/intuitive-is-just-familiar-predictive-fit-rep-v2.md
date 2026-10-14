---
id: RR-VE-COMP-2026-1E8C
authors:
- OpenAI autonomous research agent
confidence: High for familiarity and feedback findings; Moderate for predictive-fit model; Low-Moderate for neural unification
date: 2026-07-21
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
status: research-extension-complete
summary: |
  Familiarity is a major source of what users call intuitive, but it is not the whole phenomenon. Repetition increases processing fluency, liking, confidence, speed, accuracy, and automaticity. It can therefore make an initially mediocre interface feel natural and, for practiced users, perform well. However, familiarity cannot reliably repair poor task structure, excessive physical effort, weak feedback, hidden state, preventable errors, or dangerous mappings. The report proposes that perceived intuitiveness is an inference generated from processing fluency and prediction success, while durable design quality depends on separate dimensions: immediate legibility, learnability, practiced efficiency, error resistance, transferability, and adaptability. Familiarity is best modeled as a performance multiplier and switching-cost generator, not as evidence that the underlying design is good.
version: 2.0
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

------------------------------------------------------------------------

# Predictive Processing Extension

## Research State Snapshot

- **Research Package:** RP-CS-INT-002
- **Theory version:** TH-INT-2.0 candidate
- **Knowledge base version:** 2026-07-21
- **Highest-confidence areas:** prior knowledge transfer, practice-dependent automaticity, expectation effects on perception and response, importance of reliable feedback
- **Lowest-confidence areas:** direct neural identity between perceived intuitiveness and prediction-error minimization; general quantitative coefficients across interface classes
- **Largest remaining unknown:** whether a compact, exposure-sensitive surprise model predicts interface performance better than established task-level models alone
- **Active research streams:** predictive processing, ecological affordances, intuitive physics, expert intuition, information-theoretic surprise, HCI measurement
- **Recently invalidated idea:** that minimizing prediction error alone is a sufficient definition of good or intuitive design
- **Priority change:** move from a single-cause account to a multi-mechanism, testable interaction model

------------------------------------------------------------------------

# Executive Summary of the Extension

This extension tested the proposal that intuitive interaction is best understood as prediction-error minimization. The proposal survives only in a qualified form.

Expectations clearly shape perception, attention, action selection, confidence, and response timing. Familiarity supplies priors that make outcomes easier to anticipate. Stable mappings allow users to predict both what action is available and what will happen after acting. Unexpected results attract attention, slow behavior, and force model revision. These findings make prediction and surprise valuable variables for interface science.

However, predictive processing is not yet a sufficiently discriminating or empirically settled theory to replace lower-level explanations. Broad predictive-processing accounts can often explain almost any result after the fact. Neural evidence is mixed, and some findings attributed to prediction error are also explained by adaptation, attention, representational sharpening, associative learning, or ordinary decision models. Ecological psychology further challenges the assumption that action must always be mediated by an internal predictive model.

The strongest revised conclusion is therefore:

> Perceived intuitiveness is the experienced confidence that an available action will produce an expected, controllable result without requiring costly conscious inference.

Familiarity is one major way to generate that confidence. It is not the only way. Visible constraints, body-scaled affordances, natural mappings, clear causal feedback, and simple task structure can support accurate prediction on first contact. Conversely, familiarity can generate high confidence in an incorrect model, making an interface feel intuitive while producing systematic errors.

The report proposes the **Predictive Fit Model of Intuitive Interaction**, which separates four questions:

1. Can the user identify a plausible action?
2. Can the user predict the action’s result?
3. Does the observed result match the prediction?
4. Can the user update or recover when it does not?

This model is deliberately weaker than the claim that the brain globally minimizes free energy. It is an operational HCI model that can be tested using first-action accuracy, choice entropy, response time, gaze dispersion, confidence calibration, prediction violation, recovery latency, and learning curves.

**Confidence:** High that expectation congruence is a major component of intuitive interaction. Moderate that Bayesian surprise provides a useful quantitative abstraction. Low-to-moderate that active inference is currently the best neural foundation for HCI.

------------------------------------------------------------------------

# Research Log: Extension Cycles

## Cycle 9: Can Predictive Processing Explain the Feeling of Intuition?

### Objective

Test whether low prediction error offers a better explanation of intuitive interaction than familiarity alone.

### Hypothesis

An interaction feels intuitive when the user can predict the next state of the system with high confidence and low conscious effort.

### Evidence Found

Predictive-processing theories model perception as an interaction between prior expectations and sensory evidence. Expected stimuli are often identified more quickly, and ambiguous perception can be biased toward prior expectations. Experimental work on face perception has shown faster identification of expected faces and perceptual shifts toward cued priors. Research on long-term priors in bistable perception likewise supports a role for prior experience in shaping what is perceived.

In HCI, Blackler and colleagues found that users operating unfamiliar products transferred knowledge from previously encountered features. Familiar features were used more quickly and with less overt reasoning. Hurtienne’s schema-based account similarly defines intuitive use through transfer of prior knowledge into a new task environment.

These traditions converge on a common structure: the user does not approach the interface without expectations. Existing knowledge generates candidate interpretations and actions. When the interface behaves as expected, little model revision is required.

### Evidence Against

The evidence does not show that users literally experience a scalar neural prediction-error quantity as “intuitiveness.” Expected stimuli can sometimes generate stronger rather than weaker neural representation, depending on task and measurement. Prediction, repetition suppression, attention, and adaptation are difficult to isolate experimentally.

Moreover, a user can correctly predict a cumbersome ten-step process. Predictability can make a process familiar without making it physically efficient, safe, or well structured.

### Sources

- Friston, K. (2010). “The Free-Energy Principle: A Unified Brain Theory?”
- Garlichs, A., et al. (2024). “Prediction Error Processing and Sharpening of Expected Representations.”
- Hardstone, R., et al. (2021). “Long-Term Priors Influence Visual Perception.”
- Blackler, A., Popovic, V., and Mahar, D. (2010). “Investigating Users’ Intuitive Interaction with Complex Artefacts.”
- Hurtienne, J., and Israel, J. H. (2007/2009). Cognitive-schema approaches to intuitive interaction.

### Analysis

Prediction provides a useful bridge between familiarity and first-use clarity. Familiarity improves prediction by supplying priors. Visible structure improves prediction by constraining possible actions. Feedback improves prediction by making causal relationships learnable. These are different mechanisms with a shared functional outcome.

But prediction fit is not equivalent to total design quality. It is one component of interaction quality and one plausible cause of the subjective label “intuitive.”

### Conclusion

Low-cost successful prediction is a strong candidate mechanism for perceived intuitiveness, but not a complete definition of good design.

### Confidence

Moderate-high.

### Next Step

Determine whether ecological affordances can explain novel intuitive action without internal prediction.

------------------------------------------------------------------------

## Cycle 10: Does Affordance Perception Require Familiarity or Prediction?

### Objective

Test the strongest counterexample to the familiarity and predictive-model accounts: direct perception of action opportunities.

### Hypothesis

Some novel interactions are immediately usable because the environment exposes action-relevant structure relative to the user’s body, not because the user recognizes a learned convention.

### Evidence Found

Ecological psychology defines affordances relationally: an environment offers actions relative to an organism’s capabilities. Research on aperture passage, reachability, stair climbing, grasping, and tool-extended action shows that people calibrate judgments to body dimensions and action capacity. Systematic review evidence indicates that affordance perception adapts through attunement and recalibration when the person-plus-object system changes.

This matters for interface design because some mappings inherit regularities from action in the physical world. A slider moving in the same direction as its controlled quantity, a handle shaped and positioned for grasping, or an object that visibly follows the finger can reduce the need for symbolic interpretation.

### Evidence Against

Affordances are frequently overclaimed in digital design. A flat visual mark does not physically afford clicking in Gibson’s original sense. Its action meaning often depends on cultural learning, platform convention, and prior experience with screens. Even physical affordance perception requires developmental calibration and ongoing learning; “direct” does not mean innate, infallible, or independent of history.

Action-specific and ecological accounts also disagree about the role of internal mediation. Evidence that perceived properties vary with action capability does not by itself decide whether internal predictive representation is necessary.

### Sources

- Gibson, J. J. (1979). *The Ecological Approach to Visual Perception*.
- Vauclin, P., et al. (2023). Systematic review of attunement and recalibration in affordance perception.
- Witt, J. K. (2014). Reconciliation and conflict between action-specific and ecological approaches.
- Chong, I., and Proctor, R. W. (2020). Historical analysis of affordance theory.

### Analysis

The ecological account prevents a critical mistake: treating every easy interaction as retrieval of a symbolic rule. Some structures reduce uncertainty because the space of physically plausible actions is constrained before conscious interpretation.

For design science, the metaphysical dispute between direct perception and internal inference is less important than the empirical distinction between:

- action possibilities made legible by geometry and dynamics,
- action meanings supplied by convention,
- and action sequences learned through product-specific repetition.

### Conclusion

First-use success can arise from action-compatible structure without product-specific familiarity. The phrase “intuitive is just familiar” is therefore too strong.

### Confidence

High for the design-level conclusion; low on resolving the theoretical dispute between ecological and predictive accounts.

### Next Step

Test whether apparently innate physical expectations are actually learned statistical priors.

------------------------------------------------------------------------

## Cycle 11: Are There Pre-Existing Physical Expectations?

### Objective

Determine whether users possess expectations about objects, motion, containment, support, and causality that can support novel interaction.

### Hypothesis

Humans enter interface encounters with broad physical expectations learned early enough and generally enough that they function as near-universal priors.

### Evidence Found

Developmental research using violation-of-expectation paradigms reports that infants respond differently when objects appear to pass through solid barriers, disappear without occlusion, remain unsupported, or violate continuity. Adult electrophysiological evidence also indicates that intuitive physical expectations influence online object tracking.

Computational work shows that systems trained only on visual sequences can learn useful object-centric expectations concerning continuity, solidity, and persistence. This supports the possibility that “intuitive physics” can emerge from exposure to environmental regularities rather than requiring fully specified innate rules.

### Evidence Against

Infant violation-of-expectation findings are contested. Longer looking may reflect novelty, perceptual preference, or task-specific processing rather than adult-like physical concepts. The action demands placed on passive infants differ from those required for interactive tool use. Reviews increasingly caution against treating infant expectations as proof of explicit physical theories.

The distinction between innate and rapidly learned is also unnecessary for most design decisions. Both produce broad priors by the time an adult uses an interface.

### Sources

- Lin, Y., et al. (2021). Object-file and physical-reasoning systems.
- Vicovaro, M., and colleagues (2023). “Grounding Intuitive Physics in Perceptual Experience.”
- Balaban, H., et al. (2024). Electrophysiological evidence that intuitive physics guides dynamic tracking.
- Piloto, L. S., et al. (2022). “Intuitive Physics Learning in a Deep-Learning Model Inspired by Developmental Psychology.”
- Liu, S., et al. (2024). Review of violations of physical and psychological expectations.

### Analysis

The useful design insight is not that a particular interface gesture is biologically innate. It is that interfaces can borrow highly overlearned world regularities with enormous transfer breadth. Object permanence, continuity, spatial correspondence, and immediate causal response are familiar at a far deeper level than platform conventions.

This suggests a hierarchy of prior breadth. A drag interaction that preserves object continuity recruits broader experience than a platform-specific three-finger gesture. Both are familiar, but not equivalently transferable.

### Conclusion

Novel interfaces can feel immediately understandable by aligning with broad physical priors. These priors may themselves be learned, but they are not product-specific familiarity.

### Confidence

Moderate-high.

### Next Step

Examine when experience produces reliable intuition versus confident error.

------------------------------------------------------------------------

## Cycle 12: When Does Familiarity Produce Valid Intuition?

### Objective

Distinguish expertise from mere repetition and identify the conditions under which familiar action deserves trust.

### Hypothesis

Repeated interaction creates reliable intuition only when the environment contains stable regularities and supplies timely, diagnostic feedback.

### Evidence Found

Kahneman and Klein’s joint analysis concluded that skilled intuition can develop when an environment has sufficiently valid cues and when the learner has adequate opportunity to learn those cues through feedback. Recognition-primed decision research describes experts rapidly matching situations to patterns developed through experience.

This explains why repeated use of a stable interface can produce accurate, rapid action. It also explains why users can become skilled at detecting subtle system states that novices do not notice.

### Evidence Against

Experience alone is not a guarantee of expertise. In low-validity or delayed-feedback environments, confidence can increase without accuracy. Interfaces can teach the wrong model when feedback is ambiguous, intermittent, or masked. Workarounds may become habitual even after the original constraint disappears.

A design can also create local expertise that transfers poorly. Product-specific skill may look like general competence until the context changes.

### Sources

- Kahneman, D., and Klein, G. (2009). “Conditions for Intuitive Expertise: A Failure to Disagree.”
- Klein, G. (1998). *Sources of Power*.
- Shanteau, J. (1992). Competence in experts.
- Ericsson, K. A., et al. Research on deliberate practice and expert performance.

### Analysis

The decisive variable is not exposure count by itself. It is the quality of the learning loop:

\[
\text{Intuition Reliability} \approx \text{Environmental Regularity} \times \text{Feedback Diagnosticity} \times \text{Relevant Practice}
\]

This is not a validated universal equation, but it states a falsifiable relationship. Familiarity in an unstable or deceptive system may increase speed and confidence faster than correctness.

### Conclusion

Familiarity becomes valid intuition only under learnable regularities and useful feedback. Repetition can otherwise automate error.

### Confidence

High.

### Next Step

Evaluate whether information-theoretic surprise can quantify expectation violation in interfaces.

------------------------------------------------------------------------

## Cycle 13: Can Surprise Be Quantified for Interface Design?

### Objective

Translate the predictive account into measurable variables without depending on unobservable neural claims.

### Hypothesis

The behavioral cost of an interaction can be partly predicted by the information conveyed when the system violates the user’s expected state transition.

### Evidence Found

Information theory defines surprisal of an event as:

\[
S(o) = -\log_2 P(o)
\]

An event assigned low prior probability has high surprisal. Bayesian surprise instead measures the divergence between prior and posterior beliefs, often represented by Kullback-Leibler divergence:

\[
B = D_{KL}(P(\theta\mid o) \parallel P(\theta))
\]

Itti and Baldi found that Bayesian surprise predicted human visual attention in natural scenes better than several alternatives. Work on unexpected events shows behavioral interruption and response-time costs. Surprise-based traffic models have also treated response timing as belief updating following violations of prior expectation.

A recent active-inference-inspired HCI proposal attempts to connect surprise, task signal-to-noise ratio, mental capacity, Fitts’ law, Hick’s law, and the power law of practice. It is promising but remains new and requires independent replication.

### Evidence Against

The probability distribution in a real interface is rarely known. Different users possess different priors. An event can be statistically rare but semantically unsurprising, or common but damaging because it violates a strong causal expectation. Surprise can also improve attention and learning rather than merely harm usability.

A single surprisal measure cannot represent target acquisition, physical effort, moral consequence, accessibility, or emotional meaning.

### Sources

- Shannon, C. E. (1948). “A Mathematical Theory of Communication.”
- Itti, L., and Baldi, P. (2009). “Bayesian Surprise Attracts Human Attention.”
- Guan, Y., et al. (2021). Unexpected events and response timing.
- Engström, J., et al. (2022). Surprise-based framework for road-user response timing.
- Vertegaal, R., et al. (2025). “Interactive Inference: A Neuromorphic Theory of HCI.” Preprint.

### Analysis

Surprise becomes useful when treated as a user-model variable rather than an intrinsic property of the screen. The experimenter must estimate what the user expected, not merely what the designer intended.

The most practical measures are therefore paired:

- explicit pre-action prediction,
- confidence in that prediction,
- observed outcome,
- behavioral interruption,
- recovery time,
- and subsequent model update.

### Conclusion

Information-theoretic surprise is a viable abstraction for expectation violation, but only as one layer in a multi-cost model.

### Confidence

Moderate.

### Next Step

Attempt to falsify predictive processing as the unifying foundation.

------------------------------------------------------------------------

## Cycle 14: Falsification of the Unified Predictive Account

### Objective

Determine whether predictive processing should become the primary theory of intuitive interaction.

### Hypothesis

If predictive processing is the correct unifying foundation, it should generate distinctive predictions that competing accounts cannot explain equally well.

### Evidence Found

Predictive accounts unify many observations elegantly: contextual facilitation, expectation-biased perception, repetition effects, action selection, surprise, and learning. Meta-analytic work identifies a broad network involved in predictive tasks. Cellular and computational studies show that neural systems can implement forms of prediction-error minimization.

### Evidence Against

Critical reviews identify major limitations:

1. Many experiments test expectation effects, not the specific neural architecture required by predictive coding.
2. Reduced responses to expected stimuli can result from adaptation or attention.
3. Some studies find sharpening or enhanced representation of expected stimuli rather than simple suppression.
4. Broad formulations risk accommodating contradictory outcomes by changing assumptions about precision weighting.
5. Active inference and the free-energy principle often require auxiliary commitments before yielding falsifiable behavioral predictions.
6. Ecological approaches explain action through organism-environment coupling without requiring an internal generative model.
7. Existing HCI models already predict important components such as choice time, pointing time, practice, and error without invoking a unified brain theory.

Walsh and colleagues concluded that neurophysiological support for predictive processing is less decisive than is sometimes implied. Hodson and colleagues similarly found that direct empirical evaluation of predictive coding and active inference remains comparatively recent and incomplete.

### Sources

- Walsh, K. S., et al. (2020). “Evaluating the Neurophysiological Evidence for Predictive Processing as a Model of Perception.”
- Hodson, R., et al. (2024). “The Empirical Status of Predictive Coding and Active Inference.”
- Ficco, L., et al. (2021). Meta-analysis of predictive-processing studies.
- Yon, D., et al. (2020). “Beliefs and Desires in the Predictive Brain.”
- Araújo, D., et al. Ecological critiques of inferential action accounts.

### Analysis

Predictive processing is most useful here as a **research grammar**: priors, uncertainty, expected transitions, surprise, precision, and model updating. It is not yet justified as the sole biological truth beneath intuitive design.

The project should therefore avoid translating a useful metaphor into an unsupported law. The operational model should stand or fall on behavioral prediction even if predictive-coding neuroscience is later revised.

### Conclusion

The unified predictive-processing hypothesis is rejected in its strongest form. A narrower predictive-fit model is retained.

### Confidence

High that the strong claim is unjustified; moderate that the narrower model will add predictive value beyond established HCI measures.

### Next Step

Construct and test a behavioral model that competes directly with familiarity-only and conventional usability models.

------------------------------------------------------------------------

# Confirmed Findings Added by This Pass

## CF-009: Expectation Congruence Reduces Interaction Cost

When interface states and outcomes match a user’s expectations, recognition and action are generally faster and require less corrective processing.

**Evidence:** EV-013, EV-014, EV-015, EV-019  
**Confidence:** High

## CF-010: Prior Knowledge Is Hierarchical

Prior knowledge ranges from broad physical and embodied regularities to cultural conventions, domain schemas, product conventions, and task-specific habits. Broader priors usually transfer to more contexts.

**Evidence:** EV-016, EV-017, EV-018  
**Confidence:** Moderate-high

## CF-011: Feedback Quality Determines Whether Familiarity Becomes Expertise

Stable repetition produces trustworthy intuition only when the environment is sufficiently regular and the feedback identifies whether the action was correct.

**Evidence:** EV-020  
**Confidence:** High

## CF-012: Surprise Is Observer-Relative

An interface outcome is surprising only relative to a user’s prior model and confidence. Designer intent is not a substitute for measuring that model.

**Evidence:** EV-021, EV-022  
**Confidence:** High

## CF-013: Prediction Fit Is Not Equivalent to Design Quality

A process can be predictable yet slow, inaccessible, unsafe, or unnecessarily complex. Prediction fit is one dimension rather than a total quality score.

**Evidence:** synthesis of EV-013 through EV-023 and prior evidence  
**Confidence:** High

## CF-014: Predictive Processing Is Not Yet a Sufficient Neural Foundation for HCI

The framework has explanatory reach, but key neural claims remain difficult to distinguish from adaptation, attention, associative learning, and alternative theories.

**Evidence:** EV-023, EV-024, EV-025  
**Confidence:** Moderate-high

------------------------------------------------------------------------

# Rejected and Revised Hypotheses

## RH-006: Intuition Equals Prediction-Error Minimization

**Status:** Rejected as an identity claim.

Prediction success contributes to intuitive experience, but the construct also involves action visibility, effort, control, consequence, feedback, and learned confidence. A theory broad enough to call all of these prediction error loses discriminating power.

## RH-007: Completely Novel Interfaces Can Be Intuitive Without Prior Knowledge

**Status:** Rejected as usually formulated.

Users always bring prior structure, including bodily capabilities, physical regularities, linguistic categories, cultural conventions, and learned causal expectations. “Novel product” does not imply “absence of relevant prior knowledge.”

## RH-008: Affordance Is the Opposite of Familiarity

**Status:** Rejected.

Affordance perception and familiarity interact. People calibrate to action possibilities through experience, while physical structure can support transfer beyond product conventions.

## RH-009: Minimizing Surprise Is Always Desirable

**Status:** Rejected.

Some surprise attracts attention, promotes learning, communicates state change, or creates aesthetic interest. The design objective is not zero surprise. It is **appropriate, interpretable surprise at the correct semantic level**.

------------------------------------------------------------------------

# Emerging Patterns

## Pattern 1: Intuition Is Confidence Before It Is Correctness

Users can feel certain before acting because the interface activates a strong prior. That confidence may be accurate or misplaced. Therefore subjective intuitiveness must be calibrated against outcome accuracy.

## Pattern 2: Familiarity Is Nested, Not Binary

A user may be unfamiliar with a product but familiar with its physical analogy, task domain, icon vocabulary, interaction grammar, or causal structure. Research that labels participants merely “novice” or “experienced” loses this structure.

## Pattern 3: The Best Design Often Preserves Prediction While Improving Mechanics

High-value redesigns frequently keep the user’s causal model stable while reducing action count, latency, or error exposure. This preserves behavioral capital without preserving every visual detail.

## Pattern 4: Surprise Has a Budget

Users tolerate and sometimes enjoy local novelty when the larger task model remains stable. When novelty appears simultaneously in navigation, terminology, state behavior, and visual hierarchy, prediction violations compound.

## Pattern 5: Feedback Converts Action Into Learning

Without clear feedback, repetition creates familiarity with sequences but not necessarily understanding. With diagnostic feedback, users can refine a causal model and transfer it.

## Pattern 6: Intuitive Design Is a Coordination Problem

The designer and user must share enough of a model for signs, actions, and outcomes to coordinate. Intuitiveness is therefore relational, population-specific, and historical rather than an intrinsic surface property.

------------------------------------------------------------------------

# Proposed Models

## MODEL-IF-005: Predictive Fit Model of Intuitive Interaction

### Definition

For an action candidate \(a\) in state \(s\), define perceived intuitive fit as:

\[
I(a,s,u) = w_1 A_v + w_2 O_p + w_3 C_f + w_4 R_e - w_5 D_c - w_6 U_c
\]

Where:

- \(A_v\): action visibility or availability
- \(O_p\): predicted-outcome confidence
- \(C_f\): causal feedback fit after action
- \(R_e\): recovery expectancy
- \(D_c\): deliberation cost
- \(U_c\): consequence-weighted uncertainty
- \(u\): user history and capabilities

The coefficients are population- and task-dependent. The equation is a measurement scaffold, not a validated psychophysical law.

### Predictions

1. High predicted-outcome confidence with poor feedback will produce fast first actions but weak learning and poor recovery.
2. High action visibility with low outcome predictability will produce exploration rather than intuition.
3. Familiarity will primarily increase \(O_p\), lower \(D_c\), and sometimes inflate confidence beyond accuracy.
4. Clear feedback will increase subsequent \(C_f\) and improve transfer.
5. Consequence severity will increase the effective cost of uncertainty even when action time is unchanged.

### Assumptions

- The user maintains at least an implicit expectation distribution over actions and outcomes.
- Confidence and uncertainty can be approximated behaviorally or through self-report.
- Intuitive fit is task- and user-relative.

### Confidence

Moderate.

------------------------------------------------------------------------

## MODEL-IF-006: Prior-Breadth Hierarchy

### Levels

- **P0: Organism constraints** — reach, grip, visual field, motor compatibility
- **P1: Environmental regularities** — continuity, support, containment, spatial correspondence
- **P2: Cultural-linguistic conventions** — reading direction, symbols, metaphors
- **P3: Domain schemas** — editing, accounting, cooking, navigation, music
- **P4: Platform conventions** — browser, mobile, desktop, game controller
- **P5: Product conventions** — local navigation and command structure
- **P6: Task-specific habits** — memorized paths and motor chunks

### Prediction

Transfer breadth generally decreases from P0 toward P6, while routine execution speed may increase toward P6.

### Design Implication

Prefer the broadest prior that accurately communicates the intended action. Use narrow product-specific priors when they create substantial recurring advantage.

### Confidence

Moderate-high.

------------------------------------------------------------------------

## MODEL-IF-007: Expectation Violation Cost

For an observed system outcome \(o\):

\[
EVC = S_u(o) \times K_t \times C_e \times (1 - R_q)
\]

Where:

- \(S_u(o)\): surprise relative to the user’s expectation
- \(K_t\): task interruption sensitivity
- \(C_e\): consequence or error severity
- \(R_q\): quality of recovery support, scaled 0 to 1

### Interpretation

A surprising animation may have low cost in exploration but high cost during a rapid safety-critical task. Recovery support can reduce the cost of the same violation.

### Confidence

Moderate-low until experimentally calibrated.

------------------------------------------------------------------------

## MODEL-IF-008: Intuition Reliability Matrix

| Environment | Feedback | Expected result |
|---|---|---|
| Stable | Immediate and diagnostic | Fast, increasingly accurate intuition |
| Stable | Delayed or ambiguous | Habit with uncertain understanding |
| Variable | Immediate and diagnostic | Adaptable but effortful expertise |
| Variable | Delayed or misleading | Confident error and superstition |

### Confidence

High as a qualitative model.

------------------------------------------------------------------------

# Candidate Laws and Principle Updates

## LAW-IF-011: Predictive Fit Principle

### Hypothesis

An interaction is more likely to be experienced as intuitive when the user can select an action and accurately anticipate its outcome with low deliberation.

### Prediction

Controlling for motor effort, higher pre-action prediction accuracy and confidence calibration will correlate with lower response time, lower gaze dispersion, and fewer reversals.

### Counter Evidence

Highly practiced but incorrect expectations can produce fast errors. Therefore confidence must be evaluated against correctness.

### Confidence

Moderate-high.

## LAW-IF-012: Prior Breadth Principle

### Hypothesis

Designs aligned with broader priors transfer across more populations and contexts than designs dependent on product-specific habits.

### Prediction

Users unfamiliar with a product but familiar with the broader physical or domain schema will outperform users who lack that schema.

### Confidence

Moderate-high.

## LAW-IF-013: Diagnostic Feedback Principle

### Hypothesis

Repeated use becomes reliable intuition in proportion to the speed and diagnosticity of feedback.

### Prediction

When two designs have equal repetition, the design with clearer causal feedback will produce better confidence calibration, retention, and transfer.

### Confidence

High.

## LAW-IF-014: Surprise Concentration Principle

### Hypothesis

Users tolerate more novelty when prediction violations are localized and the higher-level task model remains stable.

### Prediction

A redesign changing one interaction layer at a time will produce fewer errors than a redesign with equal total change distributed simultaneously across terminology, layout, navigation, and system behavior.

### Confidence

Moderate.

## Deprecated Principle

**“Intuitive is just familiar.”** Deprecated as a literal law.

## Replacement Principle

> Intuitive interaction is usually successful transfer: the interface allows users to apply prior knowledge, embodied expectations, and visible causal structure to predict and control outcomes with little conscious inference.

------------------------------------------------------------------------

# Proposed Experimental Program

## EX-IF-001: Familiarity Versus Structural Legibility

### Question

Can visible structure outperform familiar convention on first use, and does familiarity overtake it after practice?

### Design

Create four versions of the same task:

1. familiar convention with clear mapping,
2. familiar convention with poor mapping,
3. novel pattern with clear physical/causal mapping,
4. novel pattern with arbitrary mapping.

Measure first-action accuracy, completion time, errors, prediction confidence, and performance over 20 repetitions.

### Critical Result

If the novel-clear condition beats familiar-poor on first use, familiarity is not sufficient. If familiar-poor later overtakes novel-clear, practice can compensate strongly for structure. If it never overtakes, some defects resist familiarity.

### Expected Value / Effort

Very high / Moderate.

## EX-IF-002: Explicit State Prediction Protocol

### Question

Does prediction accuracy explain intuitive-use performance beyond prior-experience self-report?

### Design

Before each action, ask a subset of participants to select the state they expect next and rate confidence. Compare prediction accuracy with response time, errors, gaze, and subjective intuitiveness.

### Expected Value / Effort

Very high / Moderate.

## EX-IF-003: Surprise and Recovery

### Question

What makes an expectation violation recoverable rather than disorienting?

### Design

Introduce controlled violations differing in semantic severity, visual salience, explanation, undo availability, and persistence. Measure interruption, error propagation, recovery latency, confidence loss, and subsequent learning.

### Expected Value / Effort

High / Moderate-high.

## EX-IF-004: Prior-Breadth Transfer Test

### Question

Do broad physical and domain priors transfer better than platform and product conventions?

### Design

Recruit groups with different domain and platform histories. Use tasks whose mappings separately align with P1 through P5 priors. Model transfer using multilevel regression rather than novice/expert labels.

### Expected Value / Effort

Very high / High.

## EX-IF-005: Redesign Surprise Budget

### Question

Do simultaneous expectation violations interact superadditively?

### Design

Manipulate changes to terminology, location, visual form, interaction, and system response in isolation and combination. Compare observed error cost with the sum of individual costs.

### Expected Value / Effort

High / High.

------------------------------------------------------------------------

# Evidence Registry Additions

## EV-013

**Citation:** Friston, K. (2010). “The Free-Energy Principle: A Unified Brain Theory?” *Nature Reviews Neuroscience*, 11, 127–138.  
**Type:** Theoretical review  
**Contribution:** Formalizes perception and action as inference under generative models and surprise minimization.  
**Limits:** Very broad; does not directly validate interface-level intuitiveness.  
**Supports:** HY-009, LAW-IF-011

## EV-014

**Citation:** Garlichs, A., et al. (2024). “Prediction Error Processing and Sharpening of Expected Representations.” *Nature Communications*.  
**Type:** Experimental neuroimaging and computational modeling  
**Contribution:** Expected faces were identified faster; ambiguous perception shifted toward priors.  
**Limits:** Face perception does not directly establish action-interface behavior.  
**Supports:** HY-009

## EV-015

**Citation:** Hardstone, R., et al. (2021). “Long-Term Priors Influence Visual Perception Through Recruitment of Long-Range Feedback.” *Nature Communications*.  
**Type:** Experimental and computational  
**Contribution:** Supports durable prior effects on ambiguous perception.  
**Supports:** HY-009

## EV-016

**Citation:** Blackler, A., Popovic, V., and Mahar, D. (2010). “Investigating Users’ Intuitive Interaction with Complex Artefacts.” *Applied Ergonomics*, 41(1), 72–92.  
**Type:** Empirical HCI/human factors  
**Contribution:** Links intuitive use of unfamiliar products to transferred prior experience and familiar features.  
**Supports:** HY-001, HY-009, LAW-IF-012

## EV-017

**Citation:** Vauclin, P., et al. (2023). “Perception of Affordances for the Person-Plus-Object System: A Systematic Review.”  
**Type:** Systematic review  
**Contribution:** Shows attunement and recalibration of action possibilities relative to body-plus-object capabilities.  
**Supports:** HY-010, MODEL-IF-006

## EV-018

**Citation:** Piloto, L. S., et al. (2022). “Intuitive Physics Learning in a Deep-Learning Model Inspired by Developmental Psychology.” *Nature Human Behaviour*.  
**Type:** Computational modeling with developmental comparison  
**Contribution:** Demonstrates that broad object expectations can be learned from visual experience.  
**Limits:** Model competence does not resolve infant cognition or interface transfer.  
**Supports:** HY-011

## EV-019

**Citation:** Balaban, H., et al. (2024). “Electrophysiology Reveals That Intuitive Physics Guides Visual Tracking and Working Memory.”  
**Type:** Human electrophysiology  
**Contribution:** Physical expectations affect online representation of moving objects.  
**Supports:** HY-011

## EV-020

**Citation:** Kahneman, D., and Klein, G. (2009). “Conditions for Intuitive Expertise: A Failure to Disagree.” *American Psychologist*, 64(6), 515–526.  
**Type:** Integrative theoretical review  
**Contribution:** Identifies environmental validity and learning opportunity as conditions for reliable intuitive expertise.  
**Supports:** HY-012, LAW-IF-013

## EV-021

**Citation:** Itti, L., and Baldi, P. (2009). “Bayesian Surprise Attracts Human Attention.” *Vision Research*, 49(10), 1295–1306.  
**Type:** Computational and behavioral eye-movement study  
**Contribution:** Formal Bayesian surprise predicted attention during natural viewing.  
**Supports:** HY-013, MODEL-IF-007

## EV-022

**Citation:** Engström, J., et al. (2022). “Modeling Road User Response Timing in Naturalistic Settings: A Surprise-Based Framework.”  
**Type:** Behavioral modeling / naturalistic traffic data  
**Contribution:** Models response timing as belief updating after expectation violations.  
**Limits:** Domain-specific and initially published as a preprint.  
**Supports:** HY-013

## EV-023

**Citation:** Walsh, K. S., et al. (2020). “Evaluating the Neurophysiological Evidence for Predictive Processing as a Model of Perception.”  
**Type:** Critical review  
**Contribution:** Finds that evidence often underdetermines predictive-processing mechanisms.  
**Challenges:** Strong form of HY-009

## EV-024

**Citation:** Hodson, R., et al. (2024). “The Empirical Status of Predictive Coding and Active Inference.” *Neuroscience & Biobehavioral Reviews*.  
**Type:** Critical empirical review  
**Contribution:** Direct empirical support is growing but remains incomplete and uneven.  
**Challenges:** Strong form of HY-009

## EV-025

**Citation:** Ficco, L., et al. (2021). “Disentangling Predictive Processing in the Brain: A Meta-Analytic Study.” *Scientific Reports*.  
**Type:** Meta-analysis  
**Contribution:** Supports a broad predictive network but does not establish a clean network-level separation between predictions and errors.  
**Supports/Challenges:** Narrow predictive framework; challenges overly specific neural claims

------------------------------------------------------------------------

# Hypothesis Registry Additions

## HY-009: Predictive Fit Hypothesis

**Statement:** Perceived intuitiveness rises when users can predict available actions and resulting states with low deliberation.  
**Status:** Provisionally supported in narrowed form  
**Confidence:** Moderate-high  
**Evidence:** EV-013 through EV-016, EV-023 through EV-025

## HY-010: Action-Structure Hypothesis

**Statement:** Body-compatible geometry and dynamics can support successful novel action without product-specific familiarity.  
**Status:** Supported  
**Confidence:** High  
**Evidence:** EV-017

## HY-011: Broad-Prior Hypothesis

**Statement:** Physical regularities learned very early and across many environments function as broad priors for novel interfaces.  
**Status:** Supported with qualification  
**Confidence:** Moderate-high  
**Evidence:** EV-018, EV-019

## HY-012: Valid-Feedback Hypothesis

**Statement:** Familiarity produces reliable intuitive expertise only where patterns are learnable and feedback is timely and diagnostic.  
**Status:** Supported  
**Confidence:** High  
**Evidence:** EV-020

## HY-013: Surprise-Cost Hypothesis

**Statement:** Behavioral disruption increases with observer-relative expectation violation, task sensitivity, and consequence severity, and decreases with recovery support.  
**Status:** Plausible, unvalidated as a general model  
**Confidence:** Moderate  
**Evidence:** EV-021, EV-022

------------------------------------------------------------------------

# Open Questions Ranked by Importance

1. **Incremental validity:** Does the Predictive Fit Model explain performance beyond familiarity, Fitts’ law, Hick-Hyman law, and standard usability measures?
2. **Prior measurement:** How can user priors be estimated without interrupting natural interaction or teaching the expected answer?
3. **Surprise composition:** Do multiple small expectation violations add linearly, multiplicatively, or according to a threshold?
4. **Confidence calibration:** Which interfaces create fast but unjustified confidence?
5. **Prior breadth:** Can the P0–P6 hierarchy be empirically ordered by transfer across populations?
6. **Affective moderation:** When does surprise create curiosity and pleasure instead of friction?
7. **Accessibility:** How do sensory, motor, cognitive, and neurodivergent differences alter prior weighting and action visibility?
8. **Collective familiarity:** How do organizational training, documentation, peer assistance, and social norms create shared intuitiveness?
9. **Longitudinal decay:** How quickly do product-specific habits decay, and how does interference from competing systems change retention?
10. **Ethics:** When do interfaces exploit familiar cues to induce actions whose consequences users do not accurately predict?

------------------------------------------------------------------------

# Recommendations and Research Backlog

| Priority | Research action | Expected value | Effort | Reason |
|---|---|---:|---:|---|
| 1 | Run EX-IF-001 and EX-IF-002 together | Very high | Moderate | Directly separates familiarity, structural legibility, prediction, and confidence |
| 2 | Build a Prior Inventory instrument based on P0–P6 | Very high | Moderate | Replaces crude novice/expert classification |
| 3 | Create an expectation-violation annotation format for usability studies | High | Low-moderate | Makes surprise evidence reusable across projects |
| 4 | Replicate the Interactive Inference model against standard HCI baselines | High | High | Tests whether active-inference language adds real predictive power |
| 5 | Review safety-critical mode errors through MODEL-IF-007 | High | Moderate | Connects surprise, habit capture, and consequence |
| 6 | Study positive surprise and aesthetic novelty | Medium-high | Moderate | Prevents the theory from collapsing into “make everything predictable” |
| 7 | Add accessibility and neurodiversity as a dedicated research stream | Very high | High | Priors and precision weighting are unlikely to be population-invariant |

------------------------------------------------------------------------

# Suggested Specialized Research Agents

## Agent A: Predictive HCI Experimentalist

Designs and analyzes EX-IF-001 through EX-IF-003. Must compare proposed models against Fitts, Hick-Hyman, KLM/GOMS, and mixed-effects learning curves.

## Agent B: Ecological Interaction Researcher

Maps Gibsonian affordance research to digital, tangible, spatial, and embodied interfaces while preventing misuse of the term “affordance.”

## Agent C: Developmental and Cross-Cultural Prior Researcher

Investigates which action expectations are broad, culturally variable, developmentally early, and population-specific.

## Agent D: Safety and Habit-Capture Researcher

Studies aviation, medicine, transportation, and industrial controls where strong priors produce rare but costly errors.

## Agent E: Information-Theoretic Modeler

Operationalizes user-relative surprise, compares surprisal and Bayesian surprise, and determines whether either predicts response interruption and recovery.

------------------------------------------------------------------------

# Parallel Research Opportunities

- **Language:** linguistic surprisal, garden-path sentences, and how readers recover from violated syntactic expectations
- **Music:** expectation, tension, repetition, and optimal violation
- **Architecture:** wayfinding, spatial legibility, and learned building typologies
- **Economics:** switching costs, standards, network effects, and path dependence
- **Manufacturing:** poka-yoke, physical constraints, and error-proofing
- **AI interaction:** model unpredictability, confidence calibration, and recoverable uncertainty
- **Education:** desirable difficulty versus fluent but shallow learning
- **Security:** familiar cues used for phishing, consent manipulation, and deceptive design

------------------------------------------------------------------------

# Risks

1. **Neuroscience laundering:** using predictive-processing vocabulary to make ordinary usability claims appear more scientific.
2. **Circularity:** defining intuitive as predictable and then inferring predictability from reports of intuitiveness.
3. **Population erasure:** assuming that one group’s priors represent universal human expectation.
4. **Optimization error:** minimizing surprise so aggressively that the system becomes stagnant, undiscoverable, or unable to signal important change.
5. **Confidence inflation:** producing familiar-looking interactions whose actual consequences differ from the convention they invoke.
6. **Measurement contamination:** asking users for predictions can alter attention and learning.
7. **Overfitting:** creating a model with enough hidden weights to explain every result without predicting new ones.

------------------------------------------------------------------------

# Theory Impact Assessment

## Affected Theory Records

- TH-INT-001: Familiarity Theory of Intuitive Interaction
- TH-COMP-PRED-001: Prediction and Surprise in Composition
- TH-LEARN-001: Repetition, Fluency, and Automaticity

## New Principle Candidates

- LAW-IF-011 Predictive Fit Principle
- LAW-IF-012 Prior Breadth Principle
- LAW-IF-013 Diagnostic Feedback Principle
- LAW-IF-014 Surprise Concentration Principle

## Deprecated Principles

- “Intuitive is just familiar” as a literal equivalence
- “Least surprise” interpreted as zero novelty

## Confidence Changes

- Familiarity as a major source of intuitiveness: remains High
- Familiarity as a sufficient explanation: reduced from Moderate to Low
- Prediction congruence as a mechanism: raised to Moderate-high
- Predictive processing as unified neural foundation: held at Low-moderate
- Feedback as a boundary condition for expertise: raised to High

## Predictions Created

- Novel-clear mappings can outperform familiar-poor mappings on first use.
- Familiarity will reduce deliberation before it reduces physical operator cost.
- Confidence will mediate subjective intuitiveness but can diverge from accuracy.
- Combined violations across multiple interface layers may create superadditive disruption.
- Diagnostic feedback will improve transfer more than repetition alone.

## Predictions Invalidated

- Any interface with low prediction error will necessarily be good.
- Completely novel interaction can be evaluated independently of prior knowledge.
- Neural response suppression is a unique signature of fulfilled prediction.

## Required Registry Updates

Add HY-009 through HY-013, EV-013 through EV-025, MODEL-IF-005 through MODEL-IF-008, and LAW-IF-011 through LAW-IF-014.

------------------------------------------------------------------------

# Knowledge Relationships

```text
Familiarity
  -> stronger priors
  -> faster action selection
  -> lower deliberation cost
  -> higher subjective fluency
  -> greater habit strength

Visible structure
  -> constrained action set
  -> lower choice entropy
  -> stronger outcome prediction

Diagnostic feedback
  -> causal model update
  -> confidence calibration
  -> retention and transfer

Expectation violation
  -> attention capture
  -> interruption and model revision
  -> learning OR error propagation
    depending on explanation, consequence, and recovery

Broad priors
  -> greater transfer
  -> lower population dependence

Narrow habits
  -> high local speed
  -> high switching cost
  -> low transfer
```

------------------------------------------------------------------------

# Repository and Website Updates

## Repository Updates

1. Replace the original report with this version or store it as `RP-CS-INT-002-intuitive-familiar-predictive-fit.md`.
2. Add structured registry entries for all new EV, HY, LAW, MODEL, and EX identifiers.
3. Create a machine-readable crosswalk between the former F0–F5 familiarity taxonomy and the new P0–P6 Prior-Breadth Hierarchy.
4. Add a `/experiments/intuitive-interaction/` directory containing protocols and data dictionaries for EX-IF-001 through EX-IF-005.
5. Preserve version 1.0 as superseded evidence history rather than overwriting it without trace.

## Website Updates

Create an explorable page with:

- the original claim,
- the revised principle,
- a familiarity-versus-predictive-fit comparison,
- the P0–P6 hierarchy,
- the Intuition Reliability Matrix,
- and interactive learning curves showing first use through expert use.

Do not present predictive processing as settled neuroscience. Mark theoretical status and confidence visibly.

------------------------------------------------------------------------

# AI Consumption Notes

- Treat “intuitive” as a latent, relational construct, not an artifact attribute.
- Never infer universality from a single population’s familiarity.
- Preserve the distinction between prediction as an operational behavioral construct and predictive coding as a neural theory.
- When recommending convention-breaking, calculate learning and migration cost.
- When evaluating expert performance, request exposure history and feedback conditions.
- Prefer explicit measurements: first-action accuracy, prediction accuracy, confidence calibration, response time, error count, recovery latency, transfer, and retention.
- Do not merge surprise, novelty, salience, and prediction error without definition.

------------------------------------------------------------------------

# Handoff Instructions

The next agent should begin with EX-IF-001 and EX-IF-002. It should not conduct another broad literature review until it has specified:

1. exact independent variables,
2. operational definitions of familiarity and structural legibility,
3. a method for measuring expected outcome without excessive task interference,
4. baseline models for comparison,
5. exclusion criteria,
6. preregistered primary outcomes,
7. and a plan for mixed-effects analysis across repeated trials.

The goal of the next REP is to determine whether predictive fit has incremental predictive validity. Failure to outperform simpler models should be recorded as a valuable negative result.

------------------------------------------------------------------------

# Research Quality Metrics

- **Primary or original empirical sources added:** 9
- **Systematic/meta-analytic or critical reviews added:** 4
- **Independent disciplinary traditions compared:** 6
- **Counterexamples reviewed:** 8
- **Competing viewpoints reviewed:** predictive coding, ecological psychology, schema transfer, expertise research, intuitive physics, information theory
- **Hypotheses tested:** 5
- **Hypotheses rejected or narrowed:** 4
- **Research completeness:** 82% for conceptual foundation; 35% for quantitative validation
- **Confidence gain:** High on boundary conditions; moderate on operational model
- **Open questions reduced:** 1 major binary question replaced by 10 more precise empirical questions

------------------------------------------------------------------------

# Research Debt

- No direct dataset yet compares familiarity-only, structural-legibility, and predictive-fit models.
- No validated instrument exists in this project for estimating prior breadth.
- Accessibility and neurodiversity evidence remain underrepresented.
- Cultural variation in symbol and gesture priors needs systematic review.
- The proposed equations have not been calibrated.
- The active-inference HCI proposal requires peer-reviewed replication.
- Positive surprise and aesthetic value remain weakly integrated.
- Explicit prediction probes may alter the phenomenon being measured.

------------------------------------------------------------------------

# Completion Checklist

- [x] Original objective reconstructed
- [x] Existing findings reviewed
- [x] Largest uncertainty identified
- [x] Competing hypotheses generated
- [x] Supporting and contradictory evidence reviewed
- [x] Strong predictive-processing claim falsified
- [x] Narrow operational theory proposed
- [x] Evidence registry updated
- [x] Hypothesis registry updated
- [x] Failed assumptions recorded
- [x] Open questions ranked
- [x] Experiments proposed
- [x] Specialized agents specified
- [x] Theory impacts recorded
- [x] Research debt recorded
- [x] Handoff instructions supplied
- [ ] Quantitative experiments executed
- [ ] Models calibrated or externally validated

------------------------------------------------------------------------

# Bibliography Additions

## Academic and Neuroscience

- Balaban, H., et al. (2024). “Electrophysiology Reveals That Intuitive Physics Guides Online Object Representation.” Open-access article: https://pmc.ncbi.nlm.nih.gov/articles/PMC11634321/
- Ficco, L., et al. (2021). “Disentangling Predictive Processing in the Brain: A Meta-Analytic Study in Favour of a Predictive Network.” *Scientific Reports*, 11. https://www.nature.com/articles/s41598-021-95603-5
- Friston, K. (2010). “The Free-Energy Principle: A Unified Brain Theory?” *Nature Reviews Neuroscience*, 11, 127–138. https://www.nature.com/articles/nrn2787
- Garlichs, A., et al. (2024). “Prediction Error Processing and Sharpening of Expected Representations.” *Nature Communications*. https://www.nature.com/articles/s41467-024-47749-9
- Hardstone, R., et al. (2021). “Long-Term Priors Influence Visual Perception Through Recruitment of Long-Range Feedback.” *Nature Communications*. https://www.nature.com/articles/s41467-021-26544-w
- Hodson, R., et al. (2024). “The Empirical Status of Predictive Coding and Active Inference.” *Neuroscience & Biobehavioral Reviews*. https://www.sciencedirect.com/science/article/abs/pii/S0149763423004426
- Walsh, K. S., et al. (2020). “Evaluating the Neurophysiological Evidence for Predictive Processing as a Model of Perception.” https://pmc.ncbi.nlm.nih.gov/articles/PMC7187369/
- Yon, D., et al. (2020). “Beliefs and Desires in the Predictive Brain.” *Nature Communications*, 11. https://www.nature.com/articles/s41467-020-18332-9

## Human-Computer Interaction and Human Factors

- Blackler, A., Popovic, V., and Mahar, D. (2010). “Investigating Users’ Intuitive Interaction with Complex Artefacts.” *Applied Ergonomics*, 41(1), 72–92. https://pubmed.ncbi.nlm.nih.gov/19586618/
- Fischer, S., Itoh, M., and Inagaki, T. (2015). Schema-based approaches to diagnosing intuitive use.
- Hurtienne, J., and Israel, J. H. “A Cognitive Schema Approach to Diagnose Intuitiveness.” ACM Digital Library. https://dl.acm.org/doi/10.1145/1620509.1620516
- Lawry, S., Popovic, V., and Blackler, A. (2019). “Age, Familiarity, and Intuitive Use: An Empirical Investigation.” *Applied Ergonomics*. https://www.sciencedirect.com/science/article/abs/pii/S0003687018302898
- Vertegaal, R., Merritt, T., Greenberg, S., Tarun, A. P., Li, Z., and Fountas, Z. (2025). “Interactive Inference: A Neuromorphic Theory of Human-Computer Interaction.” Preprint. https://arxiv.org/abs/2502.05935
- Wennberg, A., and colleagues (2018). “The Intuitive in HCI: A Critical Discourse Analysis.” ACM. https://dl.acm.org/doi/pdf/10.1145/3240167.3240202

## Ecological and Developmental Psychology

- Chong, I., and Proctor, R. W. (2020). “On the Evolution of a Radical Concept: Affordances According to Gibson and Their Subsequent Use and Development.” https://pubmed.ncbi.nlm.nih.gov/31711365/
- Gibson, J. J. (1979). *The Ecological Approach to Visual Perception*. Houghton Mifflin.
- Piloto, L. S., et al. (2022). “Intuitive Physics Learning in a Deep-Learning Model Inspired by Developmental Psychology.” *Nature Human Behaviour*. https://pmc.ncbi.nlm.nih.gov/articles/PMC9489531/
- Vauclin, P., et al. (2023). “A Systematic Review of Perception of Affordances for the Person-Plus-Object System.” https://pubmed.ncbi.nlm.nih.gov/37407795/
- Vicovaro, M., et al. (2023). “Grounding Intuitive Physics in Perceptual Experience.” https://pmc.ncbi.nlm.nih.gov/articles/PMC10607174/
- Witt, J. K. (2014). “Reconceptualizing the Relations Between Action-Specific and Ecological Approaches to Perception.” https://pubmed.ncbi.nlm.nih.gov/24683098/

## Expertise and Decision Science

- Kahneman, D., and Klein, G. (2009). “Conditions for Intuitive Expertise: A Failure to Disagree.” *American Psychologist*, 64(6), 515–526. https://pubmed.ncbi.nlm.nih.gov/19739881/
- Klein, G. (1998). *Sources of Power: How People Make Decisions*. MIT Press.

## Information Theory and Surprise

- Engström, J., Liu, S.-Y., Dinparastdjadid, A., and Simoiu, C. (2022). “Modeling Road User Response Timing in Naturalistic Settings: A Surprise-Based Framework.” https://arxiv.org/abs/2208.08651
- Itti, L., and Baldi, P. (2009). “Bayesian Surprise Attracts Human Attention.” *Vision Research*, 49(10), 1295–1306. https://cseweb.ucsd.edu/classes/fa09/cse258a/papers/itti-baldi-2009.pdf
- Shannon, C. E. (1948). “A Mathematical Theory of Communication.” *Bell System Technical Journal*, 27, 379–423 and 623–656.

------------------------------------------------------------------------

# Revision History Addition

| Version | Date | Author | Summary |
|---|---|---|---|
| 2.0 | 2026-07-21 | OpenAI autonomous research agent | Added predictive-processing falsification pass, ecological and developmental alternatives, expert-intuition boundary conditions, surprise models, five experiments, REP v2 handoff sections, and evidence/hypothesis registry additions. |
