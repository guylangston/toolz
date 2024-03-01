$outFile =  Split-Path -Path (Get-Location) -Leaf
if ( (Test-Path ../bflat)) {
  echo "[bflat] using bflat build"
  ..\bflat\bflat.exe build .\Program.cs --os:windows -o "../$outFile.exe"
  exit 0;
}
dotnet publish -c Release --sc -r win-x64 -p:PublishTrimmed=true -p:PublishSingleFile=true -o ../

