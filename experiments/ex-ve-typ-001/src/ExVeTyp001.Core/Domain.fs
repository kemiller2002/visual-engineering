namespace ExVeTyp001.Core

/// The four task classes in EX-VE-TYP-001. They deliberately differ in linguistic
/// redundancy and scanning behavior.
type TaskClass =
    | ContinuousProse
    | InterfaceLabels
    | IdentifierRecognition
    | DenseComparison

[<RequireQualifiedAccess>]
module TaskClass =
    let all =
        [ ContinuousProse
          InterfaceLabels
          IdentifierRecognition
          DenseComparison ]

    let code = function
        | ContinuousProse -> "PROSE"
        | InterfaceLabels -> "LABEL"
        | IdentifierRecognition -> "IDENT"
        | DenseComparison -> "DENSE"

    let display = function
        | ContinuousProse -> "Continuous prose"
        | InterfaceLabels -> "Interface labels"
        | IdentifierRecognition -> "Identifier recognition"
        | DenseComparison -> "Dense comparison"

type Polarity =
    | DarkOnLight
    | LightOnDark

[<RequireQualifiedAccess>]
module Polarity =
    let code = function
        | DarkOnLight -> "dark-on-light"
        | LightOnDark -> "light-on-dark"

/// A single rendered typography condition. The experiment uses CSS-compatible
/// units so a browser runner can apply this record without reinterpretation.
type TypographyCondition =
    { Id: string
      FontSizePx: decimal
      Weight: int
      WidthPercent: int
      OpticalSizePt: decimal
      LetterSpacingEm: decimal
      WordSpacingEm: decimal
      LineHeight: decimal
      LineLengthCh: int
      Polarity: Polarity
      ContrastRatio: decimal }

/// Safe pilot bounds. The values are intentionally conservative: Phase 0 should
/// test the experiment machinery without exposing participants to extreme text.
type DesignBounds =
    { FontSizePx: decimal * decimal
      Weight: int * int
      WidthPercent: int * int
      OpticalSizePt: decimal * decimal
      LetterSpacingEm: decimal * decimal
      WordSpacingEm: decimal * decimal
      LineHeight: decimal * decimal
      LineLengthCh: int * int
      ContrastRatio: decimal * decimal }

[<RequireQualifiedAccess>]
module DesignBounds =
    let pilot =
        { FontSizePx = 16.0M, 22.0M
          Weight = 350, 650
          WidthPercent = 90, 110
          OpticalSizePt = 14.0M, 22.0M
          LetterSpacingEm = -0.01M, 0.06M
          WordSpacingEm = 0.0M, 0.12M
          LineHeight = 1.35M, 1.75M
          LineLengthCh = 48, 76
          ContrastRatio = 7.0M, 12.0M }

type FindingSeverity =
    | Error
    | Warning

type DesignFinding =
    { Severity: FindingSeverity
      Code: string
      Message: string }

type TrialAssignment =
    { Ordinal: int
      Block: int
      Task: TaskClass
      Condition: TypographyCondition
      StimulusId: string
      IsRepeatabilityProbe: bool }

type ParticipantSchedule =
    { ExperimentId: string
      ProtocolVersion: string
      ParticipantIndex: int
      SessionNumber: int
      Seed: int
      TaskOrder: TaskClass list
      Assignments: TrialAssignment list }

type TrialObservation =
    { ParticipantId: string
      ParticipantIndex: int
      SessionNumber: int
      Task: TaskClass
      ConditionId: string
      StimulusId: string
      DurationMs: int
      Accuracy: decimal
      Comprehension: decimal option
      Effort: int
      Confidence: int
      Preference: int option
      Corrections: int
      Regressions: int option
      Abandoned: bool }

type EnvelopeThresholds =
    { MinAccuracy: decimal
      MinComprehension: decimal option
      MaxDurationRatioToTaskMedian: decimal
      MaxEffort: int }

type EnvelopeClassification =
    { ParticipantId: string
      Task: TaskClass
      ConditionId: string
      Inside: bool
      DurationRatioToTaskMedian: decimal
      FailedCriteria: string list }

type PreflightReport =
    { ConditionCount: int
      ErrorCount: int
      WarningCount: int
      TrialsPerPrimarySession: int
      TrialsPerRepeatabilitySession: int
      ConditionsPerTaskPerParticipant: int
      FourCohortTaskCrossOverBalanced: bool
      FourCohortTaskOrderBalanced: bool
      DeterministicSchedule: bool
      DesignFingerprint: string
      Findings: DesignFinding list }

type SimulationSummary =
    { Participants: int
      Observations: int
      ClassifiedTrials: int
      InEnvelopeTrials: int
      ParticipantsWithAtLeastOneEnvelopeConditionPerTask: int
      PreferenceFastestMatches: int
      PreferenceFastestComparisons: int }
