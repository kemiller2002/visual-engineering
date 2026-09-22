module ExVeTyp001.Tests.DesignTests

open Xunit
open ExVeTyp001.Core

[<Fact>]
let default_pilot_design_contains_24_unique_valid_conditions () =
    let conditions = Design.defaultConditions ()
    let findings = Design.validateConditions DesignBounds.pilot conditions

    Assert.Equal(24, conditions.Length)
    Assert.Equal(24, conditions |> List.map _.Id |> Set.ofList |> Set.count)
    Assert.DoesNotContain(findings, fun finding -> finding.Severity = Error)

[<Fact>]
let primary_schedule_gives_every_participant_six_conditions_per_task () =
    let conditions = Design.defaultConditions ()

    for participantIndex in 0 .. 7 do
        let schedule = Design.primarySchedule conditions participantIndex Design.DefaultSeed
        Assert.Equal(24, schedule.Assignments.Length)

        for task in TaskClass.all do
            let count = schedule.Assignments |> List.filter (fun trial -> trial.Task = task) |> List.length
            Assert.Equal(6, count)

[<Fact>]
let four_cohorts_expose_every_condition_once_to_every_task () =
    let conditions = Design.defaultConditions ()

    conditions
    |> List.iteri (fun conditionIndex _ ->
        let tasks =
            [ 0 .. 3 ]
            |> List.map (fun participantIndex -> Design.taskForCondition participantIndex conditionIndex)
            |> Set.ofList

        Assert.Equal(4, tasks.Count))

[<Fact>]
let four_task_orders_balance_every_serial_position () =
    let orders = [ 0 .. 3 ] |> List.map Design.taskOrder

    for position in 0 .. 3 do
        let tasks = orders |> List.map (fun order -> order[position]) |> Set.ofList
        Assert.Equal(4, tasks.Count)

[<Fact>]
let schedule_generation_is_deterministic_for_participant_and_seed () =
    let conditions = Design.defaultConditions ()
    let first = Design.primarySchedule conditions 3 98765
    let second = Design.primarySchedule conditions 3 98765
    Assert.Equal(first, second)

[<Fact>]
let repeatability_session_repeats_eight_condition_task_pairs_with_new_stimuli () =
    let conditions = Design.defaultConditions ()
    let primary = Design.primarySchedule conditions 1 Design.DefaultSeed
    let repeatability = Design.repeatabilitySchedule conditions 1 Design.DefaultSeed

    Assert.Equal(8, repeatability.Assignments.Length)
    Assert.All(repeatability.Assignments, fun trial -> Assert.True trial.IsRepeatabilityProbe)

    for repeated in repeatability.Assignments do
        let original =
            primary.Assignments
            |> List.find (fun trial -> trial.Condition.Id = repeated.Condition.Id)

        Assert.Equal(original.Task, repeated.Task)
        Assert.NotEqual<string>(original.StimulusId, repeated.StimulusId)

[<Fact>]
let preflight_invariants_are_green_for_default_design () =
    let report = Design.defaultConditions () |> Preflight.run
    Assert.Equal(0, report.ErrorCount)
    Assert.True report.FourCohortTaskCrossOverBalanced
    Assert.True report.FourCohortTaskOrderBalanced
    Assert.True report.DeterministicSchedule
    Assert.Equal(24, report.TrialsPerPrimarySession)
    Assert.Equal(8, report.TrialsPerRepeatabilitySession)
