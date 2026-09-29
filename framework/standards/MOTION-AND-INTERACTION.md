# Motion and Interaction Standard

Status: normative
Established: 2026-09-29

## Principle

Motion is part of interaction semantics and perceptual continuity. It SHALL be chosen from the phenomenon being represented, not added as decoration after layout and behavior are complete.

A polished interface SHALL NOT use one universal easing curve or apply spring behavior to every changing property. Different phenomena require different motion models.

Motion may reinforce state, continuity, spatial relationship, activity, direct manipulation, or attention. It SHALL NOT become authority for semantic state, legal action, severity, confidence, completion, or correctness.

## Canonical motion models

Every material UI animation SHALL be classified under one of the following models before implementation.

### 1. Inertial / spring response

Use when a perceived object with visual mass moves toward a stable resting state after activation or release.

Typical uses include:

- switch or selection indicators;
- dialog, drawer, or transient-surface entry;
- post-drop settling;
- post-snap pane settling;
- restrained press release.

The model SHOULD expose or inherit equivalent concepts to mass, restoring stiffness, and damping.

Requirements:

- heavier perceived mass MAY settle more slowly;
- lower damping MAY create limited overshoot;
- overshoot SHALL NOT imply a false semantic value or cross a legal boundary;
- semantic state SHALL update independently of the visual settling time;
- repeated input SHALL retarget the current response rather than queue long sequences.

### 2. Gravity-derived directional response

Use only when a small directional cue is intentionally modeled as vertical travel under uniform acceleration.

Requirements:

- travel distance and acceleration determine the response;
- mass SHALL NOT change gravity-derived timing;
- the model SHALL NOT be used merely because an element moves vertically;
- reduced motion SHALL remove the spatial travel.

### 3. Constant-velocity / cadence motion

Use for ongoing or repeated activity rather than an object settling at rest.

Typical uses include:

- spinner rotation;
- skeleton shimmer;
- periodic activity indicators.

Requirements:

- period, velocity, angular velocity, or cadence SHALL be explicit;
- each cycle SHALL NOT use spring overshoot or settling;
- cadence SHALL NOT falsely imply expected completion time;
- repeated motion SHALL stop when the represented activity stops;
- reduced motion SHALL replace repeated motion with a static or minimally changing equivalent while preserving activity semantics.

### 4. Direct-manipulation / authoritative-value tracking

Use when a user-controlled input or real application value is authoritative during the interaction.

Typical uses include:

- dragging;
- resizing;
- scrubbing;
- sliders;
- scroll-linked effects;
- determinate progress.

Requirements:

- presentation SHALL track the authoritative input without decorative lag;
- the mapped dimension SHALL NOT overshoot;
- the interface SHALL NOT smooth a value in a way that causes visible disagreement with the authoritative state;
- post-release snapping or settling MAY switch to an inertial model after direct manipulation ends;
- reduced motion SHALL preserve the direct mapping rather than add smoothing.

### 5. Perceptual interpolation

Use when a visual property changes but there is no meaningful physical body moving through space.

Typical uses include:

- opacity;
- color;
- backdrop tint;
- restrained blur;
- non-spatial crossfade;
- theme or surface-state interpolation.

Requirements:

- these effects SHALL use shared perceptual state timing rather than invented mechanical metaphors;
- fake mass, gravity, momentum, or bounce SHALL NOT be assigned only to make the effect feel physical;
- color SHALL NOT be the only semantic state cue;
- reduced motion MAY retain a brief non-spatial transition when it materially supports comprehension.

## Motion selection procedure

Before adding or changing animation, the designer or implementation agent SHALL answer:

1. What information does the motion communicate?
2. What is authoritative for the state being represented?
3. Which canonical model matches the phenomenon?
4. Which property or degree of freedom is being animated?
5. Can interruption occur, and what is the correct final state?
6. Can overshoot imply an invalid value, boundary, or conclusion?
7. What happens under rapid repeated input?
8. What is the reduced-motion substitution?
9. Can the interaction remain fully usable if the animation is unsupported or disabled?
10. What evidence will verify the intended behavior?

If no canonical model fits, the interaction SHALL be treated as a new Visual Engineering requirement rather than implemented with arbitrary timing.

## Core invariants

1. **Semantic authority is immediate.** Animation SHALL represent state, never establish it.
2. **Direct manipulation has no decorative lag.** Drag, resize, scrub, scroll, and other direct mappings SHALL remain responsive to the controlling input.
3. **Progress is truthful.** Determinate progress SHALL never visually exceed the authoritative value and SHALL NOT use overshooting spring motion.
4. **Interruption is safe.** Rapid or interrupted motion SHALL converge on the current state without queued stale sequences.
5. **Motion preserves control.** A user SHALL NOT have to wait for decorative animation before continuing when the product is already ready.
6. **Reduced motion is model-aware.** Spatial travel, bounce, repeated motion, and decorative compression SHALL be removed or substituted while final state and feedback remain clear.
7. **Motion does not encode hidden domain meaning.** Perceived mass, animation speed, bounce, travel distance, or cadence SHALL NOT silently encode severity, priority, confidence, permission, risk, or legal action.
8. **Equivalent phenomena behave coherently.** Physically or perceptually equivalent interactions SHOULD use the same model unless a documented context difference justifies otherwise.
9. **Unsupported motion falls back safely.** Progressive CSS features SHALL not be required for semantic operation or layout correctness.
10. **Timing is explainable.** A new duration or easing SHALL come from a named shared model, cadence, perceptual token, compatibility fallback, or documented exception.

## Press and selection behavior

Press feedback SHOULD be immediate and restrained.

- A press effect SHALL NOT move or shrink the hit target.
- Release MAY use damped restoration.
- Selection indicators MAY use a light inertial response after semantic selection has already changed.
- Repeated keyboard selection SHALL retarget rather than queue animation.
- Option text SHOULD remain stable unless movement communicates real spatial continuity.
- Reduced motion SHALL remove spatial compression and indicator travel.

## Loading and indeterminate activity

Indeterminate activity SHALL use cadence rather than spring physics.

- A spinner rotates at a defined angular cadence.
- A skeleton shimmer moves at a defined linear cadence.
- Re-rendering SHALL NOT restart an activity animation in a way that suggests a new operation.
- Animation SHALL NOT be the only indication of what is loading.
- Reduced motion SHALL stop repeated motion and retain explicit activity/status information.

## Determinate progress

Progress SHALL use authoritative-value tracking.

- visual progress SHALL not exceed known progress;
- completion treatment SHALL not appear before actual completion;
- reconciliation to a lower value SHALL be represented truthfully;
- interpolation MAY smooth between updates only while remaining bounded by authoritative values;
- reduced motion MAY place the new value immediately.

## Drag, reorder, and drop

During dragging, the manipulated representation SHALL track the pointer directly.

- candidate placement SHALL stay unambiguous;
- sibling preview motion SHALL not obscure the destination;
- auto-scroll SHALL not add a second inertial layer to pointer tracking;
- after a successful drop, displaced items MAY settle using an inertial model;
- a rejected drop SHALL return to authoritative position without implying success;
- keyboard reorder SHALL provide equivalent final state and feedback;
- reduced motion SHALL place final positions directly.

## Resize and split panes

Pointer resize SHALL track pointer position directly.

Snap, collapse, or release-time settling MAY use damped inertial response only after the manipulation ends.

No motion may cross minimum, maximum, or other legal bounds. Keyboard resizing SHALL remain prompt and shall not accumulate animation lag.

## Tooltip and contextual hint

Transient explanatory surfaces SHOULD be used only when visible help text would be inappropriate.

When animated:

- use light perceived mass;
- use opacity plus only a small origin-related displacement;
- entry SHOULD preserve the perceived relationship to the invoker;
- exit SHOULD be shorter and more damped;
- hover SHALL NOT be the only access path;
- reduced motion SHALL remove spatial travel.

Motion SHALL NOT determine accessibility semantics, focus behavior, timeout policy, or dismissal policy.

## Intrinsic-size change

Animating between intrinsic content sizes MAY be used as progressive enhancement.

- correct layout SHALL not depend on support for intrinsic-size interpolation;
- no runtime measurement loop SHALL be introduced solely to animate auto dimensions when a static layout is sufficient;
- focused or required content SHALL not become inaccessible during clipping;
- reduced motion SHALL use immediate size change or a non-spatial substitute.

## View transitions

View transitions MAY reinforce continuity across navigation or major state changes.

They SHALL remain optional enhancement.

- navigation, history, deep links, focus, and announcements SHALL work without them;
- stale snapshots SHALL not be treated as current state;
- shared-element transitions SHALL be used only when identity is genuinely continuous;
- unrelated objects SHALL not morph into one another merely because geometry permits it;
- reduced motion SHALL skip or simplify spatial transitions.

## Scroll-linked motion

Scroll position is authoritative.

- animation progress SHALL be a deterministic function of scroll progress or an explicit scroll timeline;
- no secondary spring lag SHALL be layered over direct scroll response;
- a consequential action SHALL NOT be triggered solely because a visual scroll-linked animation crossed a threshold;
- reduced motion SHALL use a static or non-spatial equivalent;
- scroll-driven animation support SHALL remain progressive enhancement.

## Concurrent motion

Simultaneous effects SHALL compose deliberately.

Prefer independent transform properties, additive composition where appropriate, or separate nested presentation layers.

Two effects SHALL NOT silently overwrite the same property and cause press, state, or entry motion to disappear or reset unexpectedly.

## Changing metrics and values

Metrics, counters, totals, dates, status values, and dashboard numbers MAY use a restrained change cue.

- semantic value SHALL update immediately;
- short crossfade or emphasis is preferred over rolling through invented intermediate values;
- odometer or count-through motion requires a demonstrated task benefit;
- animation SHALL NOT imply positive or negative meaning unsupported by the data;
- reduced motion SHALL show the new value immediately.

## Reduced-motion substitutions

The reduced-motion response depends on the model:

| Motion model | Reduced-motion response |
| --- | --- |
| inertial / spring | remove travel, overshoot, bounce, and spatial compression |
| gravity-derived | remove travel |
| constant-velocity / cadence | stop repeated motion; retain static status |
| direct manipulation / value tracking | preserve direct mapping |
| perceptual interpolation | retain only brief non-spatial change when useful |

The final state, focus indication, progress value, loading semantics, and recovery path SHALL remain available.

## Verification requirements

Motion review SHALL exercise, where applicable:

- final state correctness;
- interruption;
- rapid repeated activation;
- keyboard and pointer parity;
- direct-manipulation latency;
- reduced motion;
- disabled state;
- focus retention or restoration;
- no semantic delay;
- no invisible interactive state;
- progress bounds;
- rejected drag/drop or snap behavior;
- unsupported progressive-enhancement fallback;
- concurrent animation composition;
- cadence stopping when activity ends.

Unknown coverage SHALL be recorded as unknown, not passed.

## Anti-arbitrariness rule

The following require explicit justification:

- component-local hardcoded durations;
- component-local cubic Bézier curves;
- spring motion on non-physical state changes;
- bounce or overshoot on determinate progress;
- smoothing direct manipulation;
- infinite decorative motion;
- spatial motion that survives reduced-motion mode;
- animation used to communicate a state that lacks a textual, structural, programmatic, or otherwise durable cue.

## Relationship to Forma

Visual Engineering defines the motion decision discipline and observable quality requirements.

Forma may implement a canonical CSS motion vocabulary, physics-derived parameters, presentation presets, and component-level visual contracts. Forma SHALL NOT turn presentation motion into application or domain authority.

Consuming applications and Limen own behavior that requires runtime orchestration such as drag tracking, resize tracking, view-transition coordination, complex reorder behavior, or scroll-state integration.

Ordo/application state remains authoritative for legal transitions, capabilities, obligations, completion, and domain meaning.
