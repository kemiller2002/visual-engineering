namespace VisualEngineering.Core

open System
open System.Text.Json

/// Independent presentation channels that can carry visible meaning.
/// These are semantic channels, not assumptions about a particular CSS implementation.
type VisualChannel =
    | Hue
    | Luminance
    | TextLabel
    | IconShape
    | BorderShape
    | Pattern
    | Position
    | Motion

[<RequireQualifiedAccess>]
module VisualChannel =

    let all =
        [ Hue
          Luminance
          TextLabel
          IconShape
          BorderShape
          Pattern
          Position
          Motion ]

    let toString = function
        | Hue -> "hue"
        | Luminance -> "luminance"
        | TextLabel -> "text-label"
        | IconShape -> "icon-shape"
        | BorderShape -> "border-shape"
        | Pattern -> "pattern"
        | Position -> "position"
        | Motion -> "motion"

    let tryParse = function
        | "hue" -> Some Hue
        | "luminance" -> Some Luminance
        | "text-label" -> Some TextLabel
        | "icon-shape" -> Some IconShape
        | "border-shape" -> Some BorderShape
        | "pattern" -> Some Pattern
        | "position" -> Some Position
        | "motion" -> Some Motion
        | _ -> None

    let isExplicit = function
        | TextLabel
        | IconShape -> true
        | _ -> false

type SemanticCriticality =
    | Informational
    | Important
    | Critical

[<RequireQualifiedAccess>]
module SemanticCriticality =

    let toString = function
        | Informational -> "informational"
        | Important -> "important"
        | Critical -> "critical"

    let tryParse = function
        | "informational" -> Some Informational
        | "important" -> Some Important
        | "critical" -> Some Critical
        | _ -> None

    /// Provisional operational policy.
    /// Informational meaning may use one channel. Important and critical meaning
    /// must remain recoverable through at least two independent visible channels.
    let minimumVisualChannels = function
        | Informational -> 1
        | Important
        | Critical -> 2

    /// Critical state must retain at least one explicit cue rather than depending
    /// entirely on relational styling such as hue, position or luminance.
    let requiresExplicitChannel = function
        | Critical -> true
        | Informational
        | Important -> false

type SemanticStateEncoding =
    { Id: string
      Criticality: SemanticCriticality
      Channels: Set<VisualChannel>
      ProgrammaticSemantics: bool }

type DegradationScenario =
    { Id: string
      LostChannels: Set<VisualChannel> }

type ScenarioRobustness =
    { ScenarioId: string
      Survives: bool
      RemainingChannels: Set<VisualChannel>
      Reasons: string list }

type StateRobustness =
    { StateId: string
      Criticality: SemanticCriticality
      BaselineValid: bool
      SingleChannelSurvivable: bool
      ProgrammaticSemantics: bool
      FailureBoundary: int option
      MinimalFailureSets: VisualChannel list list
      ScenarioResults: ScenarioRobustness list
      Findings: string list }

type RobustnessManifest =
    { SchemaVersion: int
      States: SemanticStateEncoding list
      Scenarios: DegradationScenario list }

type RobustnessReport =
    { Passed: bool
      StateCount: int
      ScenarioCount: int
      States: StateRobustness list }

[<RequireQualifiedAccess>]
module PerceptualRobustness =

    let private requirements criticality remaining programmatic =
        let reasons = ResizeArray<string>()
        let minimum = SemanticCriticality.minimumVisualChannels criticality

        if not programmatic then
            reasons.Add "programmatic semantics are missing"

        if Set.count remaining < minimum then
            reasons.Add $"fewer than {minimum} independent visible channels remain"

        if SemanticCriticality.requiresExplicitChannel criticality
           && not (remaining |> Seq.exists VisualChannel.isExplicit) then
            reasons.Add "critical state has no surviving explicit text or icon cue"

        List.ofSeq reasons

    let private combinations size (items: 'a list) =
        let rec choose count remaining =
            match count, remaining with
            | 0, _ -> [ [] ]
            | _, [] -> []
            | count, head :: tail ->
                [ for rest in choose (count - 1) tail -> head :: rest
                  yield! choose count tail ]

        choose size items

    let private minimalFailureSets (state: SemanticStateEncoding) =
        let channels = state.Channels |> Set.toList

        if not (requirements state.Criticality state.Channels state.ProgrammaticSemantics |> List.isEmpty) then
            Some 0, [ [] ]
        else
            match
                [ 1 .. channels.Length ]
                |> List.tryPick (fun size ->
                    let failures =
                        combinations size channels
                        |> List.filter (fun lost ->
                            let remaining = Set.difference state.Channels (Set.ofList lost)

                            requirements state.Criticality remaining state.ProgrammaticSemantics
                            |> List.isEmpty
                            |> not)

                    if List.isEmpty failures then None else Some(size, failures))
            with
            | Some(size, failures) -> Some size, failures
            | None -> None, []

    let private scenario (state: SemanticStateEncoding) (scenario: DegradationScenario) =
        let remaining = Set.difference state.Channels scenario.LostChannels
        let reasons = requirements state.Criticality remaining state.ProgrammaticSemantics

        { ScenarioId = scenario.Id
          Survives = List.isEmpty reasons
          RemainingChannels = remaining
          Reasons = reasons }

    let evaluateState (scenarios: DegradationScenario list) (state: SemanticStateEncoding) : StateRobustness =
        let baselineReasons =
            requirements state.Criticality state.Channels state.ProgrammaticSemantics

        let boundary, minimalSets = minimalFailureSets state

        let singleChannelSurvivable =
            if state.Criticality = Informational then
                true
            else
                state.Channels
                |> Seq.forall (fun channel ->
                    let remaining = Set.remove channel state.Channels

                    requirements state.Criticality remaining state.ProgrammaticSemantics
                    |> List.isEmpty)

        let scenarioResults = scenarios |> List.map (scenario state)

        let findings =
            [ yield! baselineReasons

              if state.Criticality <> Informational && not singleChannelSurvivable then
                  yield "meaning does not survive every single-channel dropout"

              for result in scenarioResults do
                  if not result.Survives then
                      let joinedReasons = String.Join("; ", result.Reasons)
                      yield $"scenario '{result.ScenarioId}' breaks meaning: {joinedReasons}" ]

        { StateId = state.Id
          Criticality = state.Criticality
          BaselineValid = List.isEmpty baselineReasons
          SingleChannelSurvivable = singleChannelSurvivable
          ProgrammaticSemantics = state.ProgrammaticSemantics
          FailureBoundary = boundary
          MinimalFailureSets = minimalSets
          ScenarioResults = scenarioResults
          Findings = findings }

    let evaluate (manifest: RobustnessManifest) : RobustnessReport =
        let states = manifest.States |> List.map (evaluateState manifest.Scenarios)

        let passed =
            states
            |> List.forall (fun state ->
                state.BaselineValid
                && (state.Criticality = Informational || state.SingleChannelSurvivable)
                && (state.ScenarioResults |> List.forall _.Survives))

        { Passed = passed
          StateCount = states.Length
          ScenarioCount = manifest.Scenarios.Length
          States = states }

    let private requireObject label (element: JsonElement) =
        if element.ValueKind = JsonValueKind.Object then
            Ok element
        else
            Error $"{label} must be an object"

    let private requireString name (element: JsonElement) =
        match Json.tryString name element with
        | Some value when not (String.IsNullOrWhiteSpace value) -> Ok value
        | _ -> Error $"{name} must be a non-empty string"

    let private parseChannelArray name (element: JsonElement) =
        match Json.tryProperty name element with
        | None -> Error $"{name} is required"
        | Some value when value.ValueKind <> JsonValueKind.Array -> Error $"{name} must be an array"
        | Some value ->
            let mutable errors = []
            let mutable channels = Set.empty

            for item in value.EnumerateArray() do
                if item.ValueKind <> JsonValueKind.String then
                    errors <- $"{name} entries must be strings" :: errors
                else
                    let raw = item.GetString() |> Option.ofObj |> Option.defaultValue ""

                    match VisualChannel.tryParse raw with
                    | Some channel -> channels <- Set.add channel channels
                    | None -> errors <- $"unknown visual channel '{raw}'" :: errors

            if List.isEmpty errors then Ok channels else Error(String.Join("; ", List.rev errors))

    let private parseState index (element: JsonElement) =
        match requireObject $"states[{index}]" element with
        | Error error -> Error error
        | Ok element ->
            match requireString "id" element with
            | Error error -> Error error
            | Ok id ->
                match requireString "criticality" element with
                | Error error -> Error error
                | Ok criticalityRaw ->
                    match SemanticCriticality.tryParse criticalityRaw with
                    | None -> Error $"unknown criticality '{criticalityRaw}'"
                    | Some criticality ->
                        match parseChannelArray "channels" element with
                        | Error error -> Error error
                        | Ok channels ->
                            match Json.tryBool "programmaticSemantics" element with
                            | None -> Error "programmaticSemantics must be true or false"
                            | Some programmatic ->
                                Ok
                                    { Id = id
                                      Criticality = criticality
                                      Channels = channels
                                      ProgrammaticSemantics = programmatic }

    let private parseScenario index (element: JsonElement) =
        match requireObject $"scenarios[{index}]" element with
        | Error error -> Error error
        | Ok element ->
            match requireString "id" element with
            | Error error -> Error error
            | Ok id ->
                match parseChannelArray "lostChannels" element with
                | Error error -> Error error
                | Ok channels ->
                    Ok
                        { Id = id
                          LostChannels = channels }

    let private collect parser (elements: JsonElement list) =
        elements
        |> List.mapi parser
        |> List.fold
            (fun state next ->
                match state, next with
                | Ok values, Ok value -> Ok(values @ [ value ])
                | Error errors, Ok _ -> Error errors
                | Ok _, Error error -> Error [ error ]
                | Error errors, Error error -> Error(errors @ [ error ]))
            (Ok [])

    let parseManifest (text: string) : Result<RobustnessManifest, string list> =
        match Json.tryParse text with
        | Error error -> Error [ error ]
        | Ok document ->
            use document = document
            let root = document.RootElement

            if root.ValueKind <> JsonValueKind.Object then
                Error [ "manifest root must be an object" ]
            else
                let schemaVersion = Json.tryInt "schemaVersion" root

                let states =
                    Json.arrayItems "states" root
                    |> collect parseState

                let scenarios =
                    match Json.tryProperty "scenarios" root with
                    | None -> Ok []
                    | Some value when value.ValueKind = JsonValueKind.Array ->
                        value.EnumerateArray() |> List.ofSeq |> collect parseScenario
                    | Some _ -> Error [ "scenarios must be an array" ]

                match schemaVersion, states, scenarios with
                | Some 1, Ok states, Ok scenarios when not (List.isEmpty states) ->
                    let duplicateStates =
                        states
                        |> List.countBy _.Id
                        |> List.filter (fun (_, count) -> count > 1)
                        |> List.map fst

                    let duplicateScenarios =
                        scenarios
                        |> List.countBy _.Id
                        |> List.filter (fun (_, count) -> count > 1)
                        |> List.map fst

                    let duplicateStateText = String.Join(", ", duplicateStates)
                    let duplicateScenarioText = String.Join(", ", duplicateScenarios)

                    let errors =
                        [ if not (List.isEmpty duplicateStates) then
                              $"duplicate state ids: {duplicateStateText}"

                          if not (List.isEmpty duplicateScenarios) then
                              $"duplicate scenario ids: {duplicateScenarioText}" ]

                    if List.isEmpty errors then
                        Ok
                            { SchemaVersion = 1
                              States = states
                              Scenarios = scenarios }
                    else
                        Error errors
                | None, _, _ -> Error [ "schemaVersion is required" ]
                | Some version, _, _ when version <> 1 -> Error [ $"unsupported schemaVersion {version}" ]
                | _, Error errors, Ok _ -> Error errors
                | _, Ok _, Error errors -> Error errors
                | _, Error left, Error right -> Error(left @ right)
                | _, Ok [], Ok _ -> Error [ "states must contain at least one semantic state" ]
                | _ -> Error [ "manifest is invalid" ]

    let parseAndEvaluate text =
        parseManifest text |> Result.map evaluate
