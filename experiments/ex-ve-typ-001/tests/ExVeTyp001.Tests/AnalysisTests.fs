module ExVeTyp001.Tests.AnalysisTests

open Xunit
open ExVeTyp001.Core

let private observation accuracy comprehension duration effort =
    { ParticipantId = "P-001"
      ParticipantIndex = 0
      SessionNumber = 1
      Task = ContinuousProse
      ConditionId = "TYP-C001"
      StimulusId = "PROSE-S01"
      DurationMs = duration
      Accuracy = accuracy
      Comprehension = comprehension
      Effort = effort
      Confidence = 4
      Preference = Some 5
      Corrections = 0
      Regressions = Some 0
      Abandoned = false }

[<Fact>]
let envelope_classification_requires_every_preregistered_criterion () =
    let observations =
        [ observation 0.99M (Some 0.90M) 1000 2
          { observation 0.90M (Some 0.90M) 1000 2 with ConditionId = "TYP-C002" } ]

    let result = Analysis.classify observations
    let good = result |> List.find (fun item -> item.ConditionId = "TYP-C001")
    let bad = result |> List.find (fun item -> item.ConditionId = "TYP-C002")

    Assert.True good.Inside
    Assert.False bad.Inside
    Assert.Contains("accuracy", bad.FailedCriteria)

[<Fact>]
let missing_required_comprehension_cannot_enter_envelope () =
    let result =
        [ observation 1.0M None 1000 1 ]
        |> Analysis.classify
        |> List.exactlyOne

    Assert.False result.Inside
    Assert.Contains("comprehension-missing", result.FailedCriteria)
