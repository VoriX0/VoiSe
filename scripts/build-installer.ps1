param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64",
    [switch]$SkipInstaller
)

$ErrorActionPreference = "Stop"

$Root = Resolve-Path (Join-Path $PSScriptRoot "..")
$PublishDir = Join-Path $Root "artifacts\publish\VoiSe"
$InstallerDir = Join-Path $Root "artifacts\installer"
$Project = Join-Path $Root "src\VoiSe.App\VoiSe.App.csproj"
$Iss = Join-Path $Root "installer\VoiSe.iss"

Write-Host "== VoiSe release build ==" -ForegroundColor Cyan
Write-Host "Root: $Root"
Write-Host "Configuration: $Configuration"
Write-Host "Runtime: $Runtime"

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw "dotnet SDK was not found. Install .NET 8 SDK first."
}

if (Test-Path $PublishDir) {
    Remove-Item $PublishDir -Recurse -Force
}
if (Test-Path $InstallerDir) {
    Remove-Item $InstallerDir -Recurse -Force
}

New-Item -ItemType Directory -Force -Path $PublishDir | Out-Null
New-Item -ItemType Directory -Force -Path $InstallerDir | Out-Null

Write-Host "Publishing unpackaged self-contained WinUI app..." -ForegroundColor Cyan
dotnet publish $Project `
    -c $Configuration `
    -r $Runtime `
    --self-contained true `
    -p:WindowsPackageType=None `
    -p:PublishSingleFile=false `
    -p:EnableCompressionInSingleFile=false `
    -o $PublishDir

$Exe = Join-Path $PublishDir "VoiSe.App.exe"
if (-not (Test-Path $Exe)) {
    throw "Publish did not produce VoiSe.App.exe at $Exe"
}

Write-Host "Published to: $PublishDir" -ForegroundColor Green

# Portable zip is useful even if Inno Setup is not installed.
$PortableZip = Join-Path $InstallerDir "VoiSe-Portable-8.1.6-x64.zip"
if (Test-Path $PortableZip) {
    Remove-Item $PortableZip -Force
}

Write-Host "Creating portable ZIP..." -ForegroundColor Cyan
Compress-Archive -Path (Join-Path $PublishDir "*") -DestinationPath $PortableZip -Force
Write-Host "Portable ZIP: $PortableZip" -ForegroundColor Green

if ($SkipInstaller) {
    Write-Host "Skipping installer build because -SkipInstaller was specified." -ForegroundColor Yellow
    exit 0
}

$CandidateISCC = @(
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles}\Inno Setup 6\ISCC.exe"
) | Where-Object { $_ -and (Test-Path $_) } | Select-Object -First 1

if (-not $CandidateISCC) {
    Write-Host "Inno Setup 6 was not found. Install it, then rerun this script." -ForegroundColor Yellow
    Write-Host "Portable ZIP was still created successfully." -ForegroundColor Yellow
    exit 0
}

Write-Host "Building installer with Inno Setup..." -ForegroundColor Cyan
& $CandidateISCC $Iss

$SetupExe = Join-Path $InstallerDir "VoiSe-Setup-8.1.6-x64.exe"
if (Test-Path $SetupExe) {
    Write-Host "Installer: $SetupExe" -ForegroundColor Green
} else {
    throw "Installer build finished, but expected setup file was not found: $SetupExe"
}
