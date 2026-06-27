# VoiSe Gate 6.20 — Preset Import Tools

Gate 6.20 is the final Gate 6 polish pass before Scenes.

Added to Voice Changer presets:

- a combined tools tile after New preset;
- top half: Import preset JSON;
- bottom half: open presets folder;
- imported presets are copied into `%LOCALAPPDATA%\VoiSe\presets\`;
- existing Gate 6.19 hotkey capture UX is preserved.

Run:

```powershell
dotnet run --project src/VoiSe.App
```
