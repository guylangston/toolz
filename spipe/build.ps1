function build-Single
{
  param([string]$source, [string]$outp)

  if ( (Test-Path ../bflat) -ne $true) {
    return $false
  }
  Write-Output "[bflat] compile $source -> $outp"
  ..\bflat\bflat.exe build $source -Os --no-reflection --no-globalization --no-debug-info `
  --no-stacktrace-data --os:windows -o $outp
  return $true
}

build-Single ./Program.cs ../spipe.exe
# dotnet publish -c Release --sc -r win-x64 -p:PublishTrimmed=true -p:PublishSingleFile=true -o ../

