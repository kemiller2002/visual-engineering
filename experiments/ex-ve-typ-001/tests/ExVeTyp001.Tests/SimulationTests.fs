module ExVeTyp001.Tests.SimulationTests

open Xunit
open ExVeTyp001.Core

[<Fact>]
let synthetic_dry_run_produces_bounded_deterministic_observations () =
    let conditions = Design.defaultConditions ()
    let first = Simulation.simulateCohort 8 Design.DefaultSeed conditions
    let second = Simulation.simulateCohort 8 Design.DefaultSeed conditions

    Assert.Equal(first, second)
    Assert.Equal(8 * 24, first.Length)

    Assert.All(
        first,
        fun observation ->
            Assert.InRange(observation.Accuracy, 0.70M, 1.0M)
            Assert.InRange(observation.Effort, 1, 7)
            Assert.InRange(observation.Confidence, 1, 5)
            Assert.True(observation.DurationMs > 0)
    )

[<Fact>]
let simulation_summary_keeps_preference_separate_from_measured_speed () =
    let conditions = Design.defaultConditions ()
    let observations = Simulation.simulateCohort 16 Design.DefaultSeed conditions
    let summary = Simulation.summarize 16 observations

    Assert.Equal(16, summary.Participants)
    Assert.Equal(16 * 24, summary.Observations)
    Assert.Equal(summary.Observations, summary.ClassifiedTrials)
    Assert.True(summary.PreferenceFastestComparisons > 0)
