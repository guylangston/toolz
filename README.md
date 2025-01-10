# Simple terminal tools in the spirit of unix

- using stdin, stdout, error
- composable with pipes/shell/pwsh
- cross-platform
- typically a small, single source file app

## TLDR; quick start

- Each sub-folder is a self-contained project.
- Each project builds itself and places the results in `./bin`

With dotnet 8.0 installed, run:\
```
./build-all.ps1
```

## Implemented
- [diff-summary](./diff-summary/README.md) -- `git diff master | <convert-to-vimgrep-quickfix-format>` 
- [watch](./watch/README.md) -- watch a path, the spawn a command for each file change event
- `spipe` aka "string pipe" -- a bunch of little string parsing/formatting functions `stdin -> FUNC -> stdout`
- `shim` -- use environment variables to add hidden args `xargs for env`
- `vim` -- shim `vim <args>` => `nvim --clean <args>`
- `quickfix-dotnet` -- dotnet build -> find and convert errors into nvim quickfix format. `dotnet build | quickfix-dotnet | nvim --`

## Extras: other tools (not build from this repo)
 - `strip-ansi`  https://github.com/KSXGitHub/strip-ansi-cli

## Ideas / TODO / Future work
- Launcher: Run will additional flags (ProcessAffinity, ProcessPriority, Environment flags)
    - `turtle` runs and sets priority to lowest, single core. Although very easily done with a pwsh script. TODO: Add example script
