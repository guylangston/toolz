# git diff summary

> Process a git diff to produce a vim quickfix list

```pwsh
❯ git diff main -p | diff-summary.exe
diff-summary/Program.cs|9| +9,6 =>  using System.Collections.Generic;
watch/README.md|1| +1,16 =>
```
## Arguments

```
diff-summary.exe
```
- stdin - some git diff/patch stream
- `-1` show one-line per file (largest hunk)

