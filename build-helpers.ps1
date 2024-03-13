function build-Single
{
  param([string]$source, [string]$outp, [string]$bflatbin)

  if ( (Test-Path $bflatbin) -ne $true) {
    Write-Host "[bflat] not found"
    return $false
  }
  Write-Host "[bflat] compile $source -> $outp"
  & $bflatbin build $source -Os --no-reflection --no-globalization --no-debug-info `
    --no-stacktrace-data --os:windows -o $outp
  return $true
}
