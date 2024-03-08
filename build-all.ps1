rm *.exe
rm *.pdb
pushd
ls -Depth 1 build.ps1 | foreach { echo "[BUILD] $_";  cd $_.Directory && ./build.ps1 } 
popd
rm *.pdb
