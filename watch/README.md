# Watch

> Watch a directory/file; emmit a file path for each change detected

```pwsh
.\watch.exe ./ | foreach { echo "[$(Get-Date)] Detected: $_" }
[08/21/2023 21:35:02] Detected: C:\git\guy\toolz\watch\README.md
```
## Arguments
```
watch.exe <path> <regex-match>
```
- `-r` Emmit relative paths
- `-d` Debug mode
- Exit with <Ctrl-C>

