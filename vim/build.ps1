if ( (Test-Path ../bflat)) {
  echo "[bflat] using bflat build"
  ..\bflat\bflat.exe build .\vi.cs --os:windows -o ../vi.exe
  ..\bflat\bflat.exe build .\vim.cs --os:windows -o ../vim.exe
  exit 0;
}
else {
  throw "not supported"
#   echo "[bflat] missing. see: https://github.com/bflattened/bflat/releases"
#   echo "[bflat] skipping, using 'dotnet publish'"
# dotnet publish -c Release --sc -r win-x64 -p:PublishTrimmed=true -p:PublishSingleFile=true -o ../
}


