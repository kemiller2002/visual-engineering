namespace ExVeTyp001.Core

open System

[<RequireQualifiedAccess>]
module Analysis =

    let pilotThresholds task =
        match task with
        | ContinuousProse ->
            { MinAccuracy = 0.95M
              MinComprehension = Some 0.80M
              MaxDurationRatioToTaskMedian = 1.10M
              MaxEffort = 4 }
        | DenseComparison ->
            { MinAccuracy = 0.95M
              MinComprehension = Some 0.80M
              MaxDurationRatioToTaskMedian = 1.10M
              MaxEffort = 4 }
        | InterfaceLabels
        | IdentifierRecognition ->
            { MinAccuracy = 0.98M
              MinComprehension = None
              MaxDurationRatioToTaskMedian = 1.10M
              MaxEffort = 4 }

    let private median (values: int list) =
        match values |> List.sort with
        | [] -> invalidArg (nameof values) "median requires at least one value"
        | sorted ->
            let midpoint = sorted.Length / 2

            if sorted.Length % 2 = 1 then
                decimal sorted[midpoint]
            else
                (decimal sorted[midpoint - 1] + decimal sorted[midpoint]) / 2.0M

    let classify (observations: TrialObservation list) =
        observations
        |> List.groupBy (fun observation -> observation.ParticipantId, observation.Task)
        |> List.collect (fun ((participantId, task), taskObservations) ->
            let baseline =
                taskObservations
                |> List.filter (fun observation -> not observation.Abandoned)
                |> List.map _.DurationMs
                |> function
                    | [] -> 1.0M
                    | values -> median values

            let threshold = pilotThresholds task

            taskObservations
            |> List.map (fun observation ->
                let durationRatio =
                    if baseline <= 0.0M then
                        Decimal.MaxValue
                    else
                        decimal observation.DurationMs / baseline

                let failures = ResizeArray<string>()

                if observation.Abandoned then
                    failures.Add "abandoned"

                if observation.Accuracy < threshold.MinAccuracy then
                    failures.Add "accuracy"

                match threshold.MinComprehension, observation.Comprehension with
                | Some minimum, Some score when score < minimum -> failures.Add "comprehension"
                | Some _, None -> failures.Add "comprehension-missing"
                | _ -> ()

                if durationRatio > threshold.MaxDurationRatioToTaskMedian then
                    failures.Add "duration"

                if observation.Effort > threshold.MaxEffort then
                    failures.Add "effort"

                { ParticipantId = participantId
                  Task = task
                  ConditionId = observation.ConditionId
                  Inside = failures.Count = 0
                  DurationRatioToTaskMedian = durationRatio
                  FailedCriteria = failures |> List.ofSeq }))

    let fastestConditionByTask observations =
        observations
        |> List.filter (fun observation -> not observation.Abandoned)
        |> List.groupBy (fun observation -> observation.ParticipantId, observation.Task)
        |> List.choose (fun (key, values) ->
            values
            |> List.sortBy _.DurationMs
            |> List.tryHead
            |> Option.map (fun observation -> key, observation.ConditionId))
        |> Map.ofList

    let preferredConditionByTask observations =
        observations
        |> List.choose (fun observation ->
            observation.Preference
            |> Option.map (fun preference -> observation, preference))
        |> List.groupBy (fun (observation, _) -> observation.ParticipantId, observation.Task)
        |> List.choose (fun (key, values) ->
            values
            |> List.sortByDescending (fun (observation, preference) -> preference, -observation.DurationMs)
            |> List.tryHead
            |> Option.map (fun (observation, _) -> key, observation.ConditionId))
        |> Map.ofList
