module ExVeTyp001.Runner.Output

open System.Text.Json
open ExVeTyp001.Core

let private options =
    JsonSerializerOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    )

let private serialize value =
    JsonSerializer.Serialize(value, options) + "
"

let private conditionDto (condition: TypographyCondition) =
    {| id = condition.Id
       fontSizePx = condition.FontSizePx
       weight = condition.Weight
       widthPercent = condition.WidthPercent
       opticalSizePt = condition.OpticalSizePt
       letterSpacingEm = condition.LetterSpacingEm
       wordSpacingEm = condition.WordSpacingEm
       lineHeight = condition.LineHeight
       lineLengthCh = condition.LineLengthCh
       polarity = Polarity.code condition.Polarity
       contrastRatio = condition.ContrastRatio |}

let preflight (report: PreflightReport) =
    serialize
        {| experimentId = Design.ExperimentId
           protocolVersion = Design.ProtocolVersion
           conditionCount = report.ConditionCount
           errorCount = report.ErrorCount
           warningCount = report.WarningCount
           trialsPerPrimarySession = report.TrialsPerPrimarySession
           trialsPerRepeatabilitySession = report.TrialsPerRepeatabilitySession
           conditionsPerTaskPerParticipant = report.ConditionsPerTaskPerParticipant
           fourCohortTaskCrossOverBalanced = report.FourCohortTaskCrossOverBalanced
           fourCohortTaskOrderBalanced = report.FourCohortTaskOrderBalanced
           deterministicSchedule = report.DeterministicSchedule
           designFingerprint = report.DesignFingerprint
           findings =
               report.Findings
               |> List.map (fun finding ->
                   {| severity =
                          match finding.Severity with
                          | Error -> "error"
                          | Warning -> "warning"
                      code = finding.Code
                      message = finding.Message |}) |}

let schedule (schedule: ParticipantSchedule) =
    serialize
        {| experimentId = schedule.ExperimentId
           protocolVersion = schedule.ProtocolVersion
           participantIndex = schedule.ParticipantIndex
           sessionNumber = schedule.SessionNumber
           seed = schedule.Seed
           taskOrder = schedule.TaskOrder |> List.map TaskClass.code
           assignments =
               schedule.Assignments
               |> List.map (fun assignment ->
                   {| ordinal = assignment.Ordinal
                      block = assignment.Block
                      task = TaskClass.code assignment.Task
                      stimulusId = assignment.StimulusId
                      isRepeatabilityProbe = assignment.IsRepeatabilityProbe
                      condition = conditionDto assignment.Condition |}) |}

let simulation (summary: SimulationSummary) =
    serialize
        {| experimentId = Design.ExperimentId
           protocolVersion = Design.ProtocolVersion
           syntheticOnly = true
           scientificEvidence = false
           participants = summary.Participants
           observations = summary.Observations
           classifiedTrials = summary.ClassifiedTrials
           inEnvelopeTrials = summary.InEnvelopeTrials
           participantsWithAtLeastOneEnvelopeConditionPerTask =
               summary.ParticipantsWithAtLeastOneEnvelopeConditionPerTask
           preferenceFastestMatches = summary.PreferenceFastestMatches
           preferenceFastestComparisons = summary.PreferenceFastestComparisons |}
