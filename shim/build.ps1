$outName = "shim.exe"
if ( (Test-Path ../bflat)) {
  echo "[bflat] using bflat build"
  ..\bflat\bflat.exe build .\Program.cs --os:windows -o ../$outName
  exit 0;
}
else {
  echo "[bflat] missing. see: https://github.com/bflattened/bflat/releases"
  echo "[bflat] skipping, using 'dotnet publish'"
}

dotnet publish -c Release --sc -r win-x64 -p:PublishTrimmed=true -p:PublishSingleFile=true -o ../

