param(
    [string]$InstallDir = "$env:LOCALAPPDATA\Programs\VoiSe"
)

$Exe = Join-Path $InstallDir "VoiSe.App.exe"

Write-Host "VoiSe install smoke test"
Write-Host "InstallDir: $InstallDir"

if (-not (Test-Path $Exe)) {
    throw "VoiSe.App.exe was not found at $Exe"
}

Write-Host "Found executable." -ForegroundColor Green
Write-Host "Launching VoiSe..."
Start-Process -FilePath $Exe -WorkingDirectory $InstallDir
Write-Host "Check manually: app starts, Engine is Running, SoundBoard plays one sound."
