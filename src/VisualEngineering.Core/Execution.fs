namespace VisualEngineering.Core

open System
open System.IO
open System.Text

/// Outcome of applying a plan. Partial application is reported explicitly: it is never
/// presented as success and never silently swallowed.
type ExecutionResult =
    | Executed of applied: PlannedChange list
    | Aborted of blockers: PlanBlocker list
    | PartiallyApplied of applied: PlannedChange list * failed: PlannedChange * reason: string

/// Applies a validated plan. Every file is written to a staging area inside `.echelon`
/// first, the staged bytes are read back and checked, and only then are the targets replaced.
[<RequireQualifiedAccess>]
module Execution =

    let private stagingDirectory (root: string) =
        Path.Combine(root, Tool.EchelonDirectory, ".staging-" + Guid.NewGuid().ToString("N").Substring(0, 12))

    let private fileContent change =
        match change with
        | CreateFile(path, _, content) -> Some(path, content)
        | UpdateManagedFile(path, _, content) -> Some(path, content)
        | UpdateConfiguration(path, content) -> Some(path, content)
        | RegisterIntegration(_, path, content) -> Some(path, content)
        | CreateDirectory _
        | RunMigration _ -> None

    /// Applies the plan, or refuses when it carries blockers.
    let apply (root: string) (plan: Plan) : ExecutionResult =
        if not (Plan.isExecutable plan) then
            Aborted plan.Blockers
        else
            let staging = stagingDirectory root
            Directory.CreateDirectory staging |> ignore

            try
                // Stage every file write and verify the staged bytes before touching a target.
                let staged =
                    plan.Changes
                    |> List.indexed
                    |> List.choose (fun (index, change) ->
                        fileContent change
                        |> Option.map (fun (path, content) ->
                            let stagedFile = Path.Combine(staging, string index)
                            File.WriteAllText(stagedFile, content, UTF8Encoding false)
                            change, path, stagedFile, Hash.ofText content))

                let corrupt =
                    staged
                    |> List.tryFind (fun (_, _, stagedFile, expected) ->
                        Hash.ofText (File.ReadAllText stagedFile) <> expected)

                match corrupt with
                | Some(change, _, _, _) ->
                    PartiallyApplied([], change, "staged content did not read back identically")
                | None ->

                let stagedByChange =
                    staged |> List.map (fun (change, path, stagedFile, _) -> change, (path, stagedFile))

                let rec commit remaining applied =
                    match remaining with
                    | [] -> Executed(List.rev applied)
                    | change :: rest ->
                        let outcome =
                            try
                                match change with
                                | CreateDirectory path ->
                                    Files.createDirectory root path
                                    Ok()
                                | RunMigration _ -> Ok()
                                | _ ->
                                    match stagedByChange |> List.tryFind (fun (candidate, _) -> candidate = change) with
                                    | None -> Ok()
                                    | Some(_, (path, stagedFile)) ->
                                        let target = RepoPath.toAbsolute root path
                                        Paths.ensureParent target
                                        File.Move(stagedFile, target, true)
                                        Ok()
                            with error ->
                                Error error.Message

                        match outcome with
                        | Ok() -> commit rest (change :: applied)
                        | Error reason -> PartiallyApplied(List.rev applied, change, reason)

                commit plan.Changes []
            finally
                try
                    if Directory.Exists staging then
                        Directory.Delete(staging, true)
                with _ ->
                    ()
