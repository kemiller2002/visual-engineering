namespace VisualEngineering.Cli

open System

/// A small typed argument parser. The CLI surface is deliberately narrow, so a hand written
/// parser is smaller and clearer than a framework, and keeps the published contract visible.
[<RequireQualifiedAccess>]
module Parser =

    type private Flags =
        { Repository: string option
          Json: bool
          Verbose: bool
          DryRun: bool
          Check: bool
          Force: bool
          Strict: bool
          HelpRequested: bool
          VersionRequested: bool }

    let private empty =
        { Repository = None
          Json = false
          Verbose = false
          DryRun = false
          Check = false
          Force = false
          Strict = false
          HelpRequested = false
          VersionRequested = false }

    let private parseFlags (args: string list) =
        let rec loop remaining flags =
            match remaining with
            | [] -> Ok flags
            | "--help" :: rest
            | "-h" :: rest -> loop rest { flags with HelpRequested = true }
            | "--version" :: rest
            | "-V" :: rest -> loop rest { flags with VersionRequested = true }
            | "--json" :: rest -> loop rest { flags with Json = true }
            | "--verbose" :: rest -> loop rest { flags with Verbose = true }
            | "--dry-run" :: rest -> loop rest { flags with DryRun = true }
            | "--check" :: rest -> loop rest { flags with Check = true }
            | "--force" :: rest -> loop rest { flags with Force = true }
            | "--strict" :: rest -> loop rest { flags with Strict = true }
            | "--repo" :: value :: rest -> loop rest { flags with Repository = Some value }
            | "--repo" :: [] -> Error "--repo requires a path"
            | argument :: _ when argument.StartsWith "-" -> Error $"unknown option: {argument}"
            | argument :: _ -> Error $"unexpected argument: {argument}"

        loop args empty

    let private common (flags: Flags) =
        { Repository =
            flags.Repository
            |> Option.defaultWith (fun () -> Environment.CurrentDirectory)
          Json = flags.Json
          Verbose = flags.Verbose }

    /// Rejects flags a command does not accept instead of ignoring them silently.
    let private reject (command: string) (unsupported: (bool * string) list) =
        match unsupported |> List.tryFind fst with
        | Some(_, name) -> Error $"{command} does not support {name}"
        | None -> Ok()

    let parse (argv: string list) : Result<Command, string> =
        match argv with
        | [] -> Ok(Help None)
        | head :: tail ->
            let known = [ "init"; "status"; "verify"; "upgrade"; "doctor"; "help" ]

            if head = "help" then
                match tail with
                | [] -> Ok(Help None)
                | topic :: _ when List.contains topic known -> Ok(Help(Some topic))
                | topic :: _ -> Error $"unknown command: {topic}"
            elif head.StartsWith "-" then
                match parseFlags argv with
                | Error message -> Error message
                | Ok flags when flags.VersionRequested -> Ok Command.Version
                | Ok flags when flags.HelpRequested -> Ok(Help None)
                | Ok _ -> Error $"unknown option: {head}"
            elif not (List.contains head known) then
                Error $"unknown command: {head}"
            else
                match parseFlags tail with
                | Error message -> Error message
                | Ok flags when flags.HelpRequested -> Ok(Help(Some head))
                | Ok flags when flags.VersionRequested -> Ok Command.Version
                | Ok flags ->
                    let result =
                        match head with
                        | "init" ->
                            reject "init" [ flags.Strict, "--strict" ]
                            |> Result.map (fun () ->
                                Init
                                    { Common = common flags
                                      DryRun = flags.DryRun
                                      Check = flags.Check
                                      Force = flags.Force })
                        | "upgrade" ->
                            reject "upgrade" [ flags.Strict, "--strict" ]
                            |> Result.map (fun () ->
                                Upgrade
                                    { Common = common flags
                                      DryRun = flags.DryRun
                                      Check = flags.Check
                                      Force = flags.Force })
                        | "status" ->
                            reject
                                "status"
                                [ flags.DryRun, "--dry-run"
                                  flags.Check, "--check"
                                  flags.Force, "--force"
                                  flags.Strict, "--strict" ]
                            |> Result.map (fun () -> Status { Common = common flags })
                        | "verify" ->
                            reject
                                "verify"
                                [ flags.DryRun, "--dry-run"
                                  flags.Check, "--check"
                                  flags.Force, "--force" ]
                            |> Result.map (fun () ->
                                Verify
                                    { Common = common flags
                                      Strict = flags.Strict })
                        | _ ->
                            reject
                                "doctor"
                                [ flags.DryRun, "--dry-run"
                                  flags.Check, "--check"
                                  flags.Force, "--force" ]
                            |> Result.map (fun () ->
                                Doctor
                                    { Common = common flags
                                      Strict = flags.Strict })

                    result
