# VoiSe Gate 5.30 — SoundBoard Local Input Overlay

This Gate keeps the Gate 5.25/5.29 visual design, but moves the invisible Sounds input overlay directly into the same layout cell as the visible Sounds list.

## Changes

- Window title/header updated to Gate 5.30.
- The Sounds input overlay is no longer positioned by TransformToVisual/Margin.
- The overlay now stretches locally over the Sounds list, so its hit-test/wheel zone should not drift left/up in fullscreen.
- Wheel handling remains smooth and manual.
- Double-click Play and right-click context menu from Gate 5.29 are preserved.

## Run

```powershell
dotnet run --project src/VoiSe.App
```
