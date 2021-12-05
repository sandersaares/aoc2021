$ErrorActionPreference = "Stop"

if (!$IsLinux) {
    Write-Error "You must run this script on Linux. WSL works fine."
}

# Prerequisite: execute https://raw.githubusercontent.com/Metalnem/sharpfuzz/master/build/Install.sh

# TODO: Is it valid to instrument multiple assemblies? It seems to work but did not go too deep in checking.
$libraries = @(
    "Aoc2021.dll",
    "Koek.dll",
    "Koek.NetStandard.dll"
)

$libraryPaths = $libraries | % { Join-Path $PSScriptRoot $_ }
$entrypointPath = Join-Path $PSScriptRoot "Aoc2021.Fuzz.dll"
$inputPath = Join-Path $PSScriptRoot "D2P2"
$outputPath = Join-Path $PSScriptRoot "out"

# Instrument the library.
foreach ($libraryPath in $libraryPaths) {
    Write-Host "Instrumenting $libraryPath"
    & sharpfuzz $libraryPath
}

# Start!
# We need -m to increase the default (tiny) memory limit.
& afl-fuzz -i $inputPath -o $outputPath -t 10000 -m 10000 dotnet $entrypointPath