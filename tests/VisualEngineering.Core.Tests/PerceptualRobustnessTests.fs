module VisualEngineering.Core.Tests.PerceptualRobustnessTests

open Xunit
open VisualEngineering.Core

let private state id criticality channels programmatic =
    { Id = id
      Criticality = criticality
      Channels = Set.ofList channels
      ProgrammaticSemantics = programmatic }

[<Fact>]
let important_state_requires_enough_redundancy_to_survive_one_channel_loss () =
    let manifest =
        { SchemaVersion = 1
          States =
            [ state
                  "warning"
                  Important
                  [ Hue; TextLabel; IconShape ]
                  true ]
          Scenarios = [] }

    let report = PerceptualRobustness.evaluate manifest
    let warning = Assert.Single report.States

    Assert.True report.Passed
    Assert.True warning.BaselineValid
    Assert.True warning.SingleChannelSurvivable
    Assert.Equal(Some 2, warning.FailureBoundary)

[<Fact>]
let two_channel_important_state_fails_operational_single_channel_rule () =
    let manifest =
        { SchemaVersion = 1
          States = [ state "warning" Important [ Hue; TextLabel ] true ]
          Scenarios = [] }

    let report = PerceptualRobustness.evaluate manifest
    let warning = Assert.Single report.States

    Assert.False report.Passed
    Assert.False warning.SingleChannelSurvivable
    Assert.Equal(Some 1, warning.FailureBoundary)

[<Fact>]
let critical_state_requires_an_explicit_surviving_cue () =
    let manifest =
        { SchemaVersion = 1
          States =
            [ state
                  "error"
                  Critical
                  [ TextLabel; Hue; Luminance; BorderShape ]
                  true ]
          Scenarios = [] }

    let report = PerceptualRobustness.evaluate manifest
    let error = Assert.Single report.States

    Assert.False report.Passed
    Assert.False error.SingleChannelSurvivable
    Assert.Contains(
        error.MinimalFailureSets,
        fun channels -> channels = [ TextLabel ]
    )

[<Fact>]
let critical_state_with_text_and_icon_survives_either_explicit_cue_loss () =
    let manifest =
        { SchemaVersion = 1
          States =
            [ state
                  "error"
                  Critical
                  [ TextLabel; IconShape; Hue; BorderShape ]
                  true ]
          Scenarios = [] }

    let report = PerceptualRobustness.evaluate manifest
    let error = Assert.Single report.States

    Assert.True report.Passed
    Assert.True error.SingleChannelSurvivable
    Assert.Equal(Some 2, error.FailureBoundary)

[<Fact>]
let missing_programmatic_semantics_invalidates_state () =
    let manifest =
        { SchemaVersion = 1
          States =
            [ state
                  "selected"
                  Important
                  [ TextLabel; IconShape; BorderShape ]
                  false ]
          Scenarios = [] }

    let report = PerceptualRobustness.evaluate manifest
    let selected = Assert.Single report.States

    Assert.False report.Passed
    Assert.False selected.BaselineValid
    Assert.Equal(Some 0, selected.FailureBoundary)
    Assert.Contains("programmatic semantics are missing", selected.Findings)

[<Fact>]
let declared_degradation_scenarios_participate_in_pass_fail () =
    let manifest =
        { SchemaVersion = 1
          States =
            [ state
                  "warning"
                  Important
                  [ TextLabel; IconShape; Hue; BorderShape ]
                  true ]
          Scenarios =
            [ { Id = "no-explicit-cues"
                LostChannels = Set.ofList [ TextLabel; IconShape ] } ] }

    let report = PerceptualRobustness.evaluate manifest
    let warning = Assert.Single report.States

    Assert.False report.Passed
    Assert.False(Assert.Single warning.ScenarioResults).Survives

[<Fact>]
let manifest_parser_accepts_a_valid_operational_manifest () =
    let json =
        """
        {
          "schemaVersion": 1,
          "states": [
            {
              "id": "warning",
              "criticality": "important",
              "channels": ["hue", "text-label", "icon-shape"],
              "programmaticSemantics": true
            }
          ],
          "scenarios": [
            {
              "id": "no-color",
              "lostChannels": ["hue"]
            }
          ]
        }
        """

    match PerceptualRobustness.parseAndEvaluate json with
    | Error errors -> failwithf "expected valid manifest, got %A" errors
    | Ok report ->
        Assert.True report.Passed
        Assert.Equal(1, report.StateCount)
        Assert.Equal(1, report.ScenarioCount)

[<Fact>]
let manifest_parser_rejects_unknown_channel_names () =
    let json =
        """
        {
          "schemaVersion": 1,
          "states": [
            {
              "id": "warning",
              "criticality": "important",
              "channels": ["hue", "magic"],
              "programmaticSemantics": true
            }
          ]
        }
        """

    match PerceptualRobustness.parseManifest json with
    | Ok _ -> failwith "expected invalid manifest"
    | Error errors ->
        Assert.Contains(errors, fun error -> error.Contains "unknown visual channel")
