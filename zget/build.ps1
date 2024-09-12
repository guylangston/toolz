param([switch]$ignoreBflat)

if ( !$ignoreBflat && (Test-Path ../bflat)) {
  echo "[bflat] using bflat build"
  ..\bflat\bflat.exe build .\Program.cs --os:windows -r ./System.Web.HttpUtility.dll  -o ../zget.exe
  if (!$?) {
    echo "FAILED. Trying dotnet"
    dotnet build --tl --sc
  }
  else {
    exit 0;
  }
}
dotnet publish -c Release --sc -r win-x64 -p:PublishTrimmed=true -p:PublishSingleFile=true -o ..

