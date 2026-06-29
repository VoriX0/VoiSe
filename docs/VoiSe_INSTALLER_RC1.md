# VoiSe installer RC1

This package adds a Windows installer build pipeline for the first VoiSe release.

## What is included

- `installer/VoiSe.iss` — Inno Setup 6 installer script.
- `scripts/build-installer.ps1` — publishes the app and builds installer artifacts.
- `scripts/smoke-installed.ps1` — tiny post-install launch smoke helper.
- Portable ZIP output support.

## Build requirements on Windows

- .NET 8 SDK.
- Inno Setup 6 if you want `.exe` installer output.
- Windows 10/11 x64.

## Build command

From the project root:

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\scripts\build-installer.ps1
```

Outputs:

```text
artifacts\publish\VoiSe\
artifacts\installer\VoiSe-Portable-8.1.6-x64.zip
artifacts\installer\VoiSe-Setup-8.1.6-x64.exe
```

If Inno Setup is not installed, the script still creates the portable ZIP and explains what is missing.

## Installer behavior

- Per-user install.
- Default install path: `%LOCALAPPDATA%\Programs\VoiSe`.
- Start Menu shortcut.
- Optional desktop shortcut.
- Launch VoiSe after installation.
- User settings/data are kept in AppData during uninstall by default.

## Release smoke test after installing

```powershell
.\scripts\smoke-installed.ps1
```

Then manually check:

1. App starts.
2. App icon is displayed.
3. Engine switches to Running automatically.
4. SoundBoard can play one sound.
5. Settings links open correctly.
