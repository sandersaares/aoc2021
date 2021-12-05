$ErrorActionPreference = "Stop"

if (!$IsLinux) {
    Write-Error "You must run this script on Linux. WSL works fine."
}

# Prerequisite: execute https://raw.githubusercontent.com/Metalnem/sharpfuzz/master/build/Install.sh

$libraryPath = Join-Path $PSScriptRoot "Aoc2021.dll"
$entrypointPath = Join-Path $PSScriptRoot "Aoc2021.Fuzz.dll"
$inputPath = Join-Path $PSScriptRoot "D2P2"
$outputPath = Join-Path $PSScriptRoot "out"

function VerifySuccess() {
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Command failed with exit code $LASTEXITCODE"
    }
}

# Instrument the library.
& sharpfuzz $libraryPath
VerifySuccess

# Start!
# We need -m to increase the default (tiny) memory limit.
& afl-fuzz -i $inputPath -o $outputPath -t 10000 -m 10000 dotnet $entrypointPath