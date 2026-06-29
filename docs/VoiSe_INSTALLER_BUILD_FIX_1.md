# VoiSe 8.1.6 installer buildfix 1

This patch fixes the installer build failure:

```text
Системе не удается найти указанный путь.
```

## Changes

- `installer/VoiSe.iss`
  - Added `SourceDir=..`, so Inno Setup resolves relative paths from the repository root instead of `installer/`.
  - Keeps installer output in `artifacts/installer`.
  - Includes all published application files from `artifacts/publish/VoiSe`.

- `scripts/build-installer.ps1`
  - Recreates `artifacts/publish/VoiSe` and `artifacts/installer`.
  - Publishes the app as `win-x64`, self-contained.
  - Adds `WindowsAppSDKSelfContained=true` for unpackaged WinUI 3 runtime deployment.
  - Checks that `VoiSe.App.exe` exists before invoking Inno Setup.

## Third-party runtime/services policy

The installer installs the VoiSe application files only.

It does not install third-party virtual audio drivers or services such as VB-CABLE / Virtual Audio Cable. Those remain external prerequisites.

The .NET runtime is included in the publish output because the app is built with `--self-contained true`.

The Windows App SDK runtime files are intended to be included in the publish output by `-p:WindowsAppSDKSelfContained=true`.

## Build

Run from the repository root:

```powershell
.\scripts\build-installer.ps1
```

Expected output:

```text
artifacts\installer\VoiSe-Setup-8.1.6-x64.exe
```
