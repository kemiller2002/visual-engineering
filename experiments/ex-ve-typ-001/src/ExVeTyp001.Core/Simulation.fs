namespace ExVeTyp001.Core

open System

[<RequireQualifiedAccess>]
module Simulation =

    let private clampDecimal minimum maximum value =
        max minimum (min maximum value)

    let private clampInt minimum maximum value =
        max minimum (min maximum value)

    let private normalizedDistance low high value target =
        if high = low then
            0.0M
        else
            abs (value - target) / (high - low)

    let private normalizedDistanceInt low high value target =
        normalizedDistance (decimal low) (decimal high) (decimal value) (decimal target)

    let private participantIdeal participantIndex =
        let i = participantIndex + 1
        let bounds = DesignBounds.pilot

        let decimalAt bounds baseN =
            let low, high = bounds
            low + (high - low) * Design.halton i baseN

        let intAt bounds baseN =
            let low, high = bounds
            int (Decimal.Round(decimal low + decimal (high - low) * Design.halton i baseN, 0))

        decimalAt bounds.FontSizePx 29,
        intAt bounds.Weight 31,
        intAt bounds.WidthPercent 37,
        decimalAt bounds.OpticalSizePt 41,
        decimalAt bounds.LetterSpacingEm 43,
        decimalAt bounds.WordSpacingEm 47,
        decimalAt bounds.LineHeight 53,
        intAt bounds.LineLengthCh 59,
        decimalAt bounds.ContrastRatio 61

    let private taskAdjustedIdeal (task: TaskClass) participantIndex =
        let fontSize, weight, width, optical, letter, word, lineHeight, lineLength, contrast =
            participantIdeal participantIndex

        match task with
        | ContinuousProse ->
            fontSize,
            weight,
            width,
            optical,
            letter,
            word,
            min 1.75M (lineHeight + 0.08M),
            min 76 (lineLength + 5),
            contrast
        | InterfaceLabels ->
            max 16.0M (fontSize - 0.5M),
            weight,
            width,
            optical,
            letter,
            word,
            lineHeight,
            max 48 (lineLength - 8),
            contrast
        | IdentifierRecognition ->
            min 22.0M (fontSize + 0.75M),
            weight,
            width,
            optical,
            min 0.06M (letter + 0.018M),
            min 0.12M (word + 0.01M),
            lineHeight,
            max 48 (lineLength - 12),
            contrast
        | DenseComparison ->
            fontSize,
            weight,
            max 90 (width - 3),
            optical,
            letter,
            word,
            max 1.35M (lineHeight - 0.04M),
            max 48 (lineLength - 10),
            contrast

    let private distance (task: TaskClass) participantIndex (condition: TypographyCondition) =
        let bounds = DesignBounds.pilot

        let fontSize, weight, width, optical, letter, word, lineHeight, lineLength, contrast =
            taskAdjustedIdeal task participantIndex

        let distances =
            [ normalizedDistance (fst (bounds.FontSizePx) (snd (bounds.FontSizePx)) condition.FontSizePx fontSize
              normalizedDistanceInt (fst (bounds.Weight) (snd (bounds.Weight)) condition.Weight weight
              normalizedDistanceInt (fst (bounds.WidthPercent) (snd (bounds.WidthPercent)) condition.WidthPercent width
              normalizedDistance (fst (bounds.OpticalSizePt) (snd (bounds.OpticalSizePt)) condition.OpticalSizePt optical
              normalizedDistance (fst (bounds.LetterSpacingEm) (snd (bounds.LetterSpacingEm)) condition.LetterSpacingEm letter
              normalizedDistance (fst (bounds.WordSpacingEm) (snd (bounds.WordSpacingEm)) condition.WordSpacingEm word
              normalizedDistance (fst (bounds.LineHeight) (snd (bounds.LineHeight)) condition.LineHeight lineHeight
              normalizedDistanceInt (fst (bounds.LineLengthCh) (snd (bounds.LineLengthCh)) condition.LineLengthCh lineLength
              normalizedDistance (fst (bounds.ContrastRatio) (snd (bounds.ContrastRatio)) condition.ContrastRatio contrast ]

        distances |> List.average |> clampDecimal 0.0M 1.0M

    let private baseDuration (task: TaskClass) =
        match task with
        | ContinuousProse -> 15_000
        | InterfaceLabels -> 3_000
        | IdentifierRecognition -> 4_000
        | DenseComparison -> 8_000

    let private hasComprehension (task: TaskClass) =
        match task with
        | ContinuousProse
        | DenseComparison -> true
        | InterfaceLabels
        | IdentifierRecognition -> false

    let simulateAssignment participantIndex (rng: StableRandom) (assignment: TrialAssignment) : TrialObservation =
        let d = distance assignment.Task participantIndex assignment.Condition
        let noise = rng.NextSigned 0.035M

        let durationMultiplier =
            clampDecimal 0.80M 1.55M (1.0M + d * 0.42M + noise)

        let duration =
            int (decimal (baseDuration assignment.Task) * durationMultiplier)

        let accuracy =
            clampDecimal 0.70M 1.0M (1.0M - d * 0.10M + rng.NextSigned 0.015M)

        let comprehension =
            if hasComprehension assignment.Task then
                Some(clampDecimal 0.55M 1.0M (0.97M - d * 0.12M + rng.NextSigned 0.025M))
            else
                None

        let effort =
            2 + int (Decimal.Round(d * 4.0M + rng.NextSigned 0.6M, 0))
            |> clampInt 1 7

        let confidence =
            5 - int (Decimal.Round(d * 2.5M + rng.NextSigned 0.5M, 0))
            |> clampInt 1 5

        let preference =
            7 - int (Decimal.Round(d * 4.0M + rng.NextSigned 1.4M, 0))
            |> clampInt 1 7
            |> Some

        let abandoned = d > 0.82M && rng.NextUnit() < 0.06M

        { ParticipantId = $"SIM-{participantIndex + 1:D3}"
          ParticipantIndex = participantIndex
          SessionNumber = 1
          Task = assignment.Task
          ConditionId = assignment.Condition.Id
          StimulusId = assignment.StimulusId
          DurationMs = duration
          Accuracy = accuracy
          Comprehension = comprehension
          Effort = effort
          Confidence = confidence
          Preference = preference
          Corrections = int (Decimal.Round(d * 3.0M + rng.NextSigned 0.4M, 0)) |> max 0
          Regressions =
            match assignment.Task with
            | ContinuousProse
            | DenseComparison -> Some(max 0 (int (Decimal.Round(d * 4.0M + rng.NextSigned 0.6M, 0))))
            | _ -> None
          Abandoned = abandoned }

    let simulateCohort participantCount seed (conditions: TypographyCondition list) : TrialObservation list =
        if participantCount <= 0 then
            invalidArg (nameof participantCount) "participant count must be positive"

        [ for participantIndex in 0 .. participantCount - 1 do
              let schedule = Design.primarySchedule conditions participantIndex seed
              let rng = StableRandom(seed + participantIndex * 7919)

              for assignment in schedule.Assignments do
                  yield simulateAssignment participantIndex rng assignment ]

    let summarize participantCount (observations: TrialObservation list) : SimulationSummary =
        let classifications = Analysis.classify observations

        let participantsWithAllTasks =
            classifications
            |> List.filter _.Inside
            |> List.groupBy _.ParticipantId
            |> List.filter (fun (_, values) ->
                values
                |> List.map _.Task
                |> Set.ofList
                |> Set.count = TaskClass.all.Length)
            |> List.length

        let fastest = Analysis.fastestConditionByTask observations
        let preferred = Analysis.preferredConditionByTask observations

        let comparisons =
            fastest
            |> Map.toList
            |> List.choose (fun (key, fastestCondition) ->
                preferred
                |> Map.tryFind key
                |> Option.map (fun preferredCondition -> fastestCondition = preferredCondition))

        { Participants = participantCount
          Observations = observations.Length
          ClassifiedTrials = classifications.Length
          InEnvelopeTrials = classifications |> List.filter _.Inside |> List.length
          ParticipantsWithAtLeastOneEnvelopeConditionPerTask = participantsWithAllTasks
          PreferenceFastestMatches = comparisons |> List.filter id |> List.length
          PreferenceFastestComparisons = comparisons.Length }
