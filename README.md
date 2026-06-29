# VoiSe Version 8.2.0 Installer RC2

This archive is ready for building the VoiSe installer.

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\scripts\build-installer.ps1
```

Expected installer output:

```text
artifacts\installer\VoiSe-Setup-8.2.0-x64.exe
```

The installer package excludes user-created runtime data: categories, sound library JSON, copied sound files, voice presets, scenes, and settings. Those files live under `%LOCALAPPDATA%\VoiSe` on each user's PC and are not installed for other users.
