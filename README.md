# Simple terminal tools in the spirit of unix

- using stdin, stdout, error
- composable with pipes/shell/pwsh
- cross-platform
- typically a small, single source file app

## Get Started

Run `build.ps1` for each sub-project

Or, use [bflat](https://github.com/bflattened/bflat/releases) by unziping into this directory as `./bflat`
```
mkdir ./bflat
cd ./bflat
7z x ../bflat.xxxxx.zip
```
## Implemented
- [diff-summary](./diff-summary/README.md)
- [watch](./watch/README.md)
- spipe "string pipe" -- a bunch of little string parsing/formatting functions

## Ideas
- Launcher: Run will additional flags (ProcessAffinity, ProcessPriority, Environment flags)
