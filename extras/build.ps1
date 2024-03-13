if ((Test-Path ./strip-ansi-x86_64-pc-windows-gnu.exe) -eq $false) {
    echo "[download] strip-ansi-cli"
    wget -q https://github.com/KSXGitHub/strip-ansi-cli/releases/download/0.1.0/strip-ansi-x86_64-pc-windows-gnu.exe
}
cp ./strip-ansi-x86_64-pc-windows-gnu.exe ../strip-ansi.exe
