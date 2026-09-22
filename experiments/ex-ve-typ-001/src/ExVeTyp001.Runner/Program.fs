module ExVeTyp001.Runner.Program

open System
open ExVeTyp001.Core

let private usage =
    """EX-VE-TYP-001 computational preflight

Usage:
  ex-ve-typ-001 preflight
  ex-ve-typ-001 schedule PARTICIPANT_INDEX [SESSION]
  ex-ve-typ-001 simulate [PARTICIPANTS]

Commands:
  preflight   Validate the 24-condition pilot design and crossover invariants.
  schedule    Emit a deterministic JSON schedule. SESSION is 1 or 2; default 1.
  simulate    Run a synthetic mechanics-only cohort dry-run; default 64 participants.

Synthetic output is never scientific evidence and must not be used to resolve a hypothesis.
"""

let private parseNonNegative name raw =
    match Int32.TryParse raw with
    | true, value when value >= 0 -> Ok value
    | _ -> Error $"{name} must be a non-negative integer"

let private parsePositive name raw =
    match Int32.TryParse raw with
    | true, value when value > 0 -> Ok value
    | _ -> Error $"{name} must be a positive integer"

[<EntryPoint>]
let main argv =
    let conditions = Design.defaultConditions ()

    match List.ofArray argv with
    | []
    | [ "preflight" ] ->
        let report = Preflight.run conditions
        Console.Out.Write(Output.preflight report)
        if report.ErrorCount = 0 then 0 else 3

    | [ "schedule"; participantRaw ]
    | [ "schedule"; participantRaw; "1" ] ->
        match parseNonNegative "participant index" participantRaw with
        | Error message ->
            Console.Error.WriteLine message
            2
        | Ok participantIndex ->
            let schedule = Design.primarySchedule conditions participantIndex Design.DefaultSeed
            Console.Out.Write(Output.schedule schedule)
            0

    | [ "schedule"; participantRaw; "2" ] ->
        match parseNonNegative "participant index" participantRaw with
        | Error message ->
            Console.Error.WriteLine message
            2
        | Ok participantIndex ->
            let schedule = Design.repeatabilitySchedule conditions participantIndex Design.DefaultSeed
            Console.Out.Write(Output.schedule schedule)
            0

    | [ "simulate" ] ->
        let observations = Simulation.simulateCohort 64 Design.DefaultSeed conditions
        Console.Out.Write(Output.simulation (Simulation.summarize 64 observations))
        0

    | [ "simulate"; participantRaw ] ->
        match parsePositive "participant count" participantRaw with
        | Error message ->
            Console.Error.WriteLine message
            2
        | Ok participants ->
            let observations = Simulation.simulateCohort participants Design.DefaultSeed conditions
            Console.Out.Write(Output.simulation (Simulation.summarize participants observations))
            0

    | [ "help" ]
    | [ "--help" ]
    | [ "-h" ] ->
        Console.Out.Write usage
        0

    | _ ->
        Console.Error.Write usage
        2
