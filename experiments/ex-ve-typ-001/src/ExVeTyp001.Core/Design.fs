namespace ExVeTyp001.Core

open System
open System.Globalization
open System.Security.Cryptography
open System.Text

/// Stable pseudo-random generator used only for reproducible experimental
/// scheduling and synthetic dry-runs. It is not a security primitive.
type StableRandom(seed: int) =
    let mutable state =
        let value = uint32 seed
        if value = 0u then 0x6D2B79F5u else value

    member _.NextUInt32() =
        state <- state ^^^ (state <<< 13)
        state <- state ^^^ (state >>> 17)
        state <- state ^^^ (state <<< 5)
        state

    member this.Next(maxExclusive: int) =
        if maxExclusive <= 0 then
            invalidArg (nameof maxExclusive) "maxExclusive must be positive"

        int (this.NextUInt32() % uint32 maxExclusive)

    member this.NextUnit() =
        decimal (this.NextUInt32()) / decimal UInt32.MaxValue

    member this.NextSigned(magnitude: decimal) =
        ((this.NextUnit() * 2.0M) - 1.0M) * magnitude

[<RequireQualifiedAccess>]
module Design =

    [<Literal>]
    let ExperimentId = "EX-VE-TYP-001"

    [<Literal>]
    let ProtocolVersion = "1.0-pilot"

    [<Literal>]
    let DefaultConditionCount = 24

    [<Literal>]
    let DefaultSeed = 20260922

    let halton (index: int) (baseN: int) =
        if index <= 0 then
            invalidArg (nameof index) "Halton indices start at one"

        if baseN < 2 then
            invalidArg (nameof baseN) "Halton base must be at least two"

        let mutable i = index
        let mutable factor = 1.0M
        let mutable value = 0.0M

        while i > 0 do
            factor <- factor / decimal baseN
            value <- value + factor * decimal (i % baseN)
            i <- i / baseN

        value

    let private round places value =
        Decimal.Round(value, places, MidpointRounding.AwayFromZero)

    let private scaleDecimal (minimum: decimal, maximum: decimal) fraction places =
        minimum + ((maximum - minimum) * fraction) |> round places

    let private scaleInt (minimum: int, maximum: int) fraction =
        let value = decimal minimum + decimal (maximum - minimum) * fraction
        int (Decimal.Round(value, 0, MidpointRounding.AwayFromZero))

    /// Creates a deterministic, space-filling coarse map instead of an exhaustive
    /// factorial. Each axis receives a different prime-base Halton sequence.
    let generateConditions count bounds =
        if count <= 0 then
            invalidArg (nameof count) "condition count must be positive"

        [ for zeroBased in 0 .. count - 1 do
              let i = zeroBased + 1

              yield
                  { Id = $"TYP-C{i:D3}"
                    FontSizePx = scaleDecimal bounds.FontSizePx (halton i 2) 2
                    Weight = scaleInt bounds.Weight (halton i 3)
                    WidthPercent = scaleInt bounds.WidthPercent (halton i 5)
                    OpticalSizePt = scaleDecimal bounds.OpticalSizePt (halton i 7) 2
                    LetterSpacingEm = scaleDecimal bounds.LetterSpacingEm (halton i 11) 3
                    WordSpacingEm = scaleDecimal bounds.WordSpacingEm (halton i 13) 3
                    LineHeight = scaleDecimal bounds.LineHeight (halton i 17) 3
                    LineLengthCh = scaleInt bounds.LineLengthCh (halton i 19)
                    Polarity = if i % 2 = 0 then DarkOnLight else LightOnDark
                    ContrastRatio = scaleDecimal bounds.ContrastRatio (halton i 23) 2 } ]

    let defaultConditions () =
        generateConditions DefaultConditionCount DesignBounds.pilot

    let private inDecimalRange (minimum, maximum) value =
        value >= minimum && value <= maximum

    let private inIntRange (minimum, maximum) value =
        value >= minimum && value <= maximum

    let validateConditions bounds (conditions: TypographyCondition list) =
        let findings = ResizeArray<DesignFinding>()

        if List.isEmpty conditions then
            findings.Add
                { Severity = Error
                  Code = "DESIGN001"
                  Message = "the condition set is empty" }

        let duplicateIds =
            conditions
            |> List.countBy _.Id
            |> List.filter (fun (_, count) -> count > 1)
            |> List.map fst

        if not duplicateIds.IsEmpty then
            let joinedIds = String.Join(", ", duplicateIds)

            findings.Add
                { Severity = Error
                  Code = "DESIGN002"
                  Message = $"duplicate condition IDs: {joinedIds}" }

        for condition in conditions do
            let checks =
                [ "font size", inDecimalRange bounds.FontSizePx condition.FontSizePx
                  "weight", inIntRange bounds.Weight condition.Weight
                  "width", inIntRange bounds.WidthPercent condition.WidthPercent
                  "optical size", inDecimalRange bounds.OpticalSizePt condition.OpticalSizePt
                  "letter spacing", inDecimalRange bounds.LetterSpacingEm condition.LetterSpacingEm
                  "word spacing", inDecimalRange bounds.WordSpacingEm condition.WordSpacingEm
                  "line height", inDecimalRange bounds.LineHeight condition.LineHeight
                  "line length", inIntRange bounds.LineLengthCh condition.LineLengthCh
                  "contrast ratio", inDecimalRange bounds.ContrastRatio condition.ContrastRatio ]

            for label, valid in checks do
                if not valid then
                    findings.Add
                        { Severity = Error
                          Code = "DESIGN003"
                          Message = $"{condition.Id} has {label} outside the preregistered pilot bounds" }

            if condition.ContrastRatio < 4.5M then
                findings.Add
                    { Severity = Error
                      Code = "DESIGN004"
                      Message = $"{condition.Id} falls below the WCAG 2.2 4.5:1 normal-text contrast floor" }
            elif condition.ContrastRatio < 7.0M then
                findings.Add
                    { Severity = Warning
                      Code = "DESIGN005"
                      Message = $"{condition.Id} has less than the pilot's intended 7:1 contrast reserve" }

        if conditions.Length % TaskClass.all.Length <> 0 then
            findings.Add
                { Severity = Error
                  Code = "DESIGN006"
                  Message = "condition count must be divisible by four for the balanced task crossover" }

        findings |> List.ofSeq

    let private shuffle seed values =
        let rng = StableRandom seed
        let items = values |> List.toArray

        for i = items.Length - 1 downto 1 do
            let j = rng.Next(i + 1)
            let temporary = items[i]
            items[i] <- items[j]
            items[j] <- temporary

        items |> List.ofArray

    /// Four-row balanced order used for the task blocks. Each task appears once
    /// in each serial position across the four cohort rotations.
    let taskOrder participantIndex =
        let rows =
            [ [ ContinuousProse; InterfaceLabels; DenseComparison; IdentifierRecognition ]
              [ InterfaceLabels; IdentifierRecognition; ContinuousProse; DenseComparison ]
              [ IdentifierRecognition; DenseComparison; InterfaceLabels; ContinuousProse ]
              [ DenseComparison; ContinuousProse; IdentifierRecognition; InterfaceLabels ] ]

        rows[participantIndex % rows.Length]

    /// Assigns a condition to one task for this participant. Across four adjacent
    /// participant cohorts every condition appears exactly once in every task.
    let taskForCondition participantIndex conditionIndex =
        let taskIndex = (conditionIndex + participantIndex) % TaskClass.all.Length
        TaskClass.all[taskIndex]

    let private stimulusId task participantIndex sessionNumber localIndex =
        let offset = participantIndex * 3 + sessionNumber * 5
        let stimulusNumber = ((localIndex + offset) % 8) + 1
        $"{TaskClass.code task}-S{stimulusNumber:D2}"

    let primarySchedule (conditions: TypographyCondition list) participantIndex seed =
        let order = taskOrder participantIndex

        let assignments =
            order
            |> List.mapi (fun blockIndex task ->
                conditions
                |> List.mapi (fun index condition -> index, condition)
                |> List.filter (fun (index, _) -> taskForCondition participantIndex index = task)
                |> shuffle (seed + participantIndex * 1009 + (blockIndex + 1) * 9176)
                |> List.mapi (fun localIndex (_, condition) ->
                    blockIndex,
                    task,
                    condition,
                    stimulusId task participantIndex 1 localIndex))
            |> List.concat
            |> List.mapi (fun ordinal (blockIndex, task, condition, stimulus) ->
                { Ordinal = ordinal + 1
                  Block = blockIndex + 1
                  Task = task
                  Condition = condition
                  StimulusId = stimulus
                  IsRepeatabilityProbe = false })

        { ExperimentId = ExperimentId
          ProtocolVersion = ProtocolVersion
          ParticipantIndex = participantIndex
          SessionNumber = 1
          Seed = seed
          TaskOrder = order
          Assignments = assignments }

    /// Session two deliberately repeats eight condition/task pairs from session
    /// one, with different stimuli, to measure stability without repeating the
    /// entire primary session.
    let repeatabilitySchedule (conditions: TypographyCondition list) participantIndex seed =
        let selectedIndices =
            [ 0; 3; 6; 9; 12; 15; 18; 21 ]
            |> List.filter (fun index -> index < conditions.Length)

        let order = taskOrder participantIndex
        let primary = primarySchedule conditions participantIndex seed

        let selected: (int * TypographyCondition * TaskClass) list =
            selectedIndices
            |> List.map (fun index -> index, conditions[index], taskForCondition participantIndex index)

        let differentStimulus task condition localIndex =
            let original =
                primary.Assignments
                |> List.find (fun trial -> trial.Condition.Id = condition.Id)

            let candidate = stimulusId task participantIndex 2 localIndex

            if candidate <> original.StimulusId then
                candidate
            else
                stimulusId task participantIndex 2 (localIndex + 1)

        let assignments =
            order
            |> List.mapi (fun blockIndex task ->
                selected
                |> List.filter (fun (_, _, assignedTask) -> assignedTask = task)
                |> shuffle (seed + 500_000 + participantIndex * 1009 + (blockIndex + 1) * 9176)
                |> List.mapi (fun localIndex (_, condition, _) ->
                    blockIndex,
                    task,
                    condition,
                    differentStimulus task condition localIndex))
            |> List.concat
            |> List.mapi (fun ordinal (blockIndex, task, condition, stimulus) ->
                { Ordinal = ordinal + 1
                  Block = blockIndex + 1
                  Task = task
                  Condition = condition
                  StimulusId = stimulus
                  IsRepeatabilityProbe = true })

        { ExperimentId = ExperimentId
          ProtocolVersion = ProtocolVersion
          ParticipantIndex = participantIndex
          SessionNumber = 2
          Seed = seed
          TaskOrder = order
          Assignments = assignments }

    let canonicalConditionText (condition: TypographyCondition) =
        let invariant (value: decimal) = value.ToString(CultureInfo.InvariantCulture)

        String.Join(
            "|",
            [ condition.Id
              invariant condition.FontSizePx
              string condition.Weight
              string condition.WidthPercent
              invariant condition.OpticalSizePt
              invariant condition.LetterSpacingEm
              invariant condition.WordSpacingEm
              invariant condition.LineHeight
              string condition.LineLengthCh
              Polarity.code condition.Polarity
              invariant condition.ContrastRatio ]
        )

    let fingerprint (conditions: TypographyCondition list) =
        let text =
            conditions
            |> List.map canonicalConditionText
            |> String.concat "
"
            |> Encoding.UTF8.GetBytes

        SHA256.HashData text
        |> Convert.ToHexString
        |> _.ToLowerInvariant()
