rm *.exe
rm *.pdb
pushd

if (!(Test-Path ./bflat/)) {
    echo "bflat missing, downloading..."
    $json = curl -s -k  https://api.github.com/repos/bflattened/bflat/releases/latest | ConvertFrom-Json
    $bflatVer = $json.name
    echo "  found: $bflatVer"
    $url = "https://github.com/bflattened/bflat/releases/download/$bflatVer/bflat-$($bflatVer.Trim('v'))-windows-x64.zip"
    wget -q --show-progress $url -O bflat.zip
    if ($LASTEXITCODE -ne 0) {
        throw "download failed"
    }
    mkdir bflat
    cd bflat
    7z x ../bflat.zip
    cd ..
}

ls -Depth 1 build.ps1 | foreach { echo "[BUILD] $_";  cd $_.Directory && ./build.ps1 } 
popd
rm *.pdb
