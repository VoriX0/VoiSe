# VoiSe Version 8.1.6 Installer RC1

This is the first installer-ready package for the VoiSe first release.

Main app state is based on Gate 8.1 buildfix 6, plus installer packaging files.

## Build installer

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\scripts\build-installer.ps1
```

Outputs are created in:

```text
artifacts\installer\
```

See `docs/VoiSe_INSTALLER_RC1.md` for details.
