namespace VisualEngineering.Cli

open VisualEngineering.Core

/// CLI help is public documentation. It must agree with README.md and docs/cli.md.
[<RequireQualifiedAccess>]
module Help =

    let private exe = Tool.ExecutableName
    let private pkg = Tool.PackageName

    let private globalOptions =
        [ "  -h, --help            Show help for the program or a command."
          "  -V, --version         Print the CLI version and exit."
          "      --repo <path>     Repository to operate on. Defaults to the working directory."
          "      --json            Emit machine readable JSON on stdout and nothing else."
          "      --verbose         Include per file detail in human readable output." ]

    let general =
        Text.lines (
            [ $"{Tool.DisplayName} repository lifecycle tool ({pkg})"
              ""
              "Installs, verifies, diagnoses and upgrades the Visual Engineering UI research"
              "context in a repository so implementation agents work from current evidence."
              ""
              "USAGE"
              $"  npx {pkg} <command> [options]"
              $"  {exe} <command> [options]"
              ""
              "COMMANDS"
              "  init                 Bring the repository into a valid installed state."
              "  status               Report installation state. Read only."
              "  verify               Validate the installation. Read only."
              "  upgrade              Move an existing installation to this release."
              "  doctor               Explain what is wrong and how to fix it. Read only."
              ""
              "GLOBAL OPTIONS" ]
            @ globalOptions
            @ [ ""
                "EXIT CODES"
                "  0  success                    4  changes required (--check)"
                "  1  internal failure           5  installation blocked"
                "  2  invalid arguments          6  environment or packaging failure"
                "  3  verification failed        7  unsupported platform"
                ""
                "EXAMPLES"
                $"  npx {pkg} init"
                $"  npx {pkg} status --json"
                $"  npx {pkg} verify --strict"
                ""
                $"Run `{exe} <command> --help` for command specific help." ]
        )

    let private commandHelp name summary sideEffects options examples =
        Text.lines (
            [ $"{exe} {name}"
              ""
              summary
              ""
              "USAGE"
              $"  npx {pkg} {name} [options]"
              ""
              "SIDE EFFECTS"
              $"  {sideEffects}"
              ""
              "OPTIONS" ]
            @ options
            @ globalOptions
            @ [ ""; "EXAMPLES" ]
            @ examples
        )

    let init =
        commandHelp
            "init"
            (Text.lines
                [ "Brings the repository into a valid installed state for this capability."
                  ""
                  "init is idempotent: running it again when nothing needs to change reports no"
                  "changes and rewrites nothing. It installs the packaged context, writes the"
                  "installation manifest and repository configuration, and registers the managed"
                  "regions in .gitignore and the agent instructions file. Files you own are never"
                  "overwritten, and content outside a managed region is preserved."
                 ]
             |> fun text -> text.TrimEnd '\n')
            "Creates and updates tool owned files. Refuses to replace locally modified content unless --force is given."
            [ "      --dry-run         Calculate and report the plan without changing anything."
              "      --check           Make no changes and exit 4 when changes are required."
              "      --force           Replace locally modified tool maintained content." ]
            [ $"  npx {pkg} init"
              $"  npx {pkg} init --dry-run --json"
              $"  npx {pkg} init --check" ]

    let status =
        commandHelp
            "status"
            (Text.lines
                [ "Reports the tool name, CLI version, installed version, configuration version,"
                  "artifact status, integration status, verification status and any available"
                  "upgrade." ]
             |> fun text -> text.TrimEnd '\n')
            "None. status never modifies the repository."
            []
            [ $"  npx {pkg} status"; $"  npx {pkg} status --json" ]

    let verify =
        commandHelp
            "verify"
            (Text.lines
                [ "Validates that the capability is correctly installed."
                  ""
                  "Default mode asks whether the installation is internally consistent: every"
                  "required file present, nothing modified locally, the packaged context intact."
                  "An installation that is consistent but older than this release still passes."
                  ""
                  "--strict additionally requires the installation to be exactly what this release"
                  "would produce: no stale files and no pending upgrade. Use it in CI." ]
             |> fun text -> text.TrimEnd '\n')
            "None. verify never modifies the repository."
            [ "      --strict          Also fail when the installation is behind this release." ]
            [ $"  npx {pkg} verify"
              $"  npx {pkg} verify --strict"
              $"  npx {pkg} verify --json" ]

    let upgrade =
        commandHelp
            "upgrade"
            (Text.lines
                [ "Moves an existing installation to the version this release provides."
                  ""
                  "Upgrades run one configuration version at a time. Every transition checks its"
                  "preconditions before anything is written, and the upgrade stops at the first"
                  "failure rather than leaving the repository half migrated. User owned files are"
                  "preserved, and locally modified tool maintained content blocks the upgrade"
                  "until it is resolved or --force is given." ]
             |> fun text -> text.TrimEnd '\n')
            "Creates and updates tool owned files and managed regions. Never deletes user content."
            [ "      --dry-run         Calculate and report the plan without changing anything."
              "      --check           Make no changes and exit 4 when an upgrade is required."
              "      --force           Replace locally modified tool maintained content." ]
            [ $"  npx {pkg} upgrade"
              $"  npx {pkg} upgrade --dry-run --json"
              $"  npx {pkg} upgrade --check" ]

    let doctor =
        commandHelp
            "doctor"
            (Text.lines
                [ "Diagnoses problems and explains them."
                  ""
                  "Findings are classified as error, warning or information, and carry a remedy"
                  "where one exists. Not every deviation is an error: an available upgrade is a"
                  "warning, and a healthy installation still reports information." ]
             |> fun text -> text.TrimEnd '\n')
            "None. doctor never modifies the repository."
            [ "      --strict          Treat warnings as failures." ]
            [ $"  npx {pkg} doctor"; $"  npx {pkg} doctor --json" ]

    let forCommand (topic: string option) =
        match topic with
        | None -> general
        | Some "init" -> init
        | Some "status" -> status
        | Some "verify" -> verify
        | Some "upgrade" -> upgrade
        | Some "doctor" -> doctor
        | Some _ -> general
