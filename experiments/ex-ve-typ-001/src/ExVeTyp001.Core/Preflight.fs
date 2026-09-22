namespace ExVeTyp001.Core

[<RequireQualifiedAccess>]
module Preflight =

    let private crossoverBalanced conditions =
        conditions
        |> List.mapi (fun conditionIndex _ ->
            [ 0 .. 3 ]
            |> List.map (fun participantIndex -> Design.taskForCondition participantIndex conditionIndex)
            |> Set.ofList
            |> Set.count = TaskClass.all.Length)
        |> List.forall id

    let private taskOrderBalanced () =
        let orders = [ 0 .. 3 ] |> List.map Design.taskOrder

        [ 0 .. 3 ]
        |> List.forall (fun position ->
            orders
            |> List.map (fun order -> order[position])
            |> Set.ofList
            |> Set.count = TaskClass.all.Length)

    let run conditions =
        let findings = Design.validateConditions DesignBounds.pilot conditions
        let primary0 = Design.primarySchedule conditions 0 Design.DefaultSeed
        let primary0Again = Design.primarySchedule conditions 0 Design.DefaultSeed
        let repeat0 = Design.repeatabilitySchedule conditions 0 Design.DefaultSeed

        let perTask =
            primary0.Assignments
            |> List.countBy _.Task
            |> List.map snd
            |> function
                | [] -> 0
                | counts -> counts |> List.min

        { ConditionCount = conditions.Length
          ErrorCount = findings |> List.filter (fun finding -> finding.Severity = Error) |> List.length
          WarningCount = findings |> List.filter (fun finding -> finding.Severity = Warning) |> List.length
          TrialsPerPrimarySession = primary0.Assignments.Length
          TrialsPerRepeatabilitySession = repeat0.Assignments.Length
          ConditionsPerTaskPerParticipant = perTask
          FourCohortTaskCrossOverBalanced = crossoverBalanced conditions
          FourCohortTaskOrderBalanced = taskOrderBalanced ()
          DeterministicSchedule = primary0 = primary0Again
          DesignFingerprint = Design.fingerprint conditions
          Findings = findings }
