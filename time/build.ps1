. ../build-helpers.ps1
$res = build-Single ./Program.cs ../time.exe ../bflat/bflat.exe
if (!$res) {
  dotnet publish -c Release --sc -r win-x64 -p:PublishTrimmed=true -p:PublishSingleFile=true -o ../
}
