# VoiSe Version 8.2.0 — installer package

This package is prepared for building a public installer with:

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\scripts\build-installer.ps1
```

## Release changes

- Display version updated to `VoiSe Version 8.2.0`.
- Application project output type changed from `Exe` to `WinExe` so the app starts without a console window when launched normally.
- Installer version updated to `8.2.0`.
- Portable ZIP and installer names now use `8.2.0`.

## User data policy

The installer must not contain developer/user-created runtime data:

- SoundBoard categories and library data: `%LOCALAPPDATA%\VoiSe\soundboard.json`
- User sound files: `%LOCALAPPDATA%\VoiSe\sounds\`
- Voice presets: `%LOCALAPPDATA%\VoiSe\presets\`
- Scenes: `%LOCALAPPDATA%\VoiSe\scenes\`
- Settings: `%LOCALAPPDATA%\VoiSe\settings.json`

The build script publishes only the application and then sanitizes the publish output before packing.
If any user-data folders or JSON files accidentally appear in the publish output, they are removed; if anything remains, the build fails instead of producing an installer.

## Expected outputs

```text
artifacts\installer\VoiSe-Portable-8.2.0-x64.zip
artifacts\installer\VoiSe-Setup-8.2.0-x64.exe
```
