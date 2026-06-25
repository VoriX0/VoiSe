$ErrorActionPreference = "Stop"

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw "dotnet SDK not found. Install .NET SDK 8 or newer."
}

if (-not (Test-Path "VoiSe.sln")) {
    dotnet new sln -n VoiSe
}

function Add-ProjectIfMissing($path) {
    $list = dotnet sln VoiSe.sln list
    if ($list -notcontains $path) {
        dotnet sln VoiSe.sln add $path
    }
}

Add-ProjectIfMissing "src/VoiSe.Audio/VoiSe.Audio.csproj"
Add-ProjectIfMissing "src/VoiSe.Gate0.Cli/VoiSe.Gate0.Cli.csproj"
Add-ProjectIfMissing "src/VoiSe.App/VoiSe.App.csproj"

Write-Host "Restoring packages..."
dotnet restore

Write-Host "Building audio library..."
dotnet build src/VoiSe.Audio/VoiSe.Audio.csproj -c Debug

Write-Host "Building CLI prototype..."
dotnet build src/VoiSe.Gate0.Cli/VoiSe.Gate0.Cli.csproj -c Debug

Write-Host "Building WinUI 3 app..."
dotnet build src/VoiSe.App/VoiSe.App.csproj -c Debug

Write-Host "Done. Try: dotnet run --project src/VoiSe.App"
