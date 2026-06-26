# VoiSe Gate 6.11 — Settings Log and Preset Export

Gate 6.11 keeps the working Gate 6.8 / Gate 6.5 SoundBoard wheel behavior and the extended Voice Changer scroll fix from Gate 6.10.

## What changed

- SoundBoard scroll logic is unchanged from the working Gate 6.8/6.5 calibration.
- Voice Changer scroll remains extended down to the bottom fullscreen area.
- Settings log area is reduced to half height.
- Settings log wheel zone now starts at the actual log textbox instead of the whole log panel, so it should not steal wheel events from the Settings controls above it.
- Voice preset context menu now has **Copy JSON file**.
- **Copy JSON file** copies the preset `.json` file to the Windows clipboard as a file, so it can be pasted/sent quickly in apps that accept files from clipboard.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Active voice sliders

- Voice Gain
- Gate
- Compressor
- Pitch
- Bass
- Treble
- Distortion
- Robot
- Tremolo
- Echo
- Reverb
- Radio
- Bit Crusher
- Alien

## Presets

Presets are stored as separate JSON files in:

```powershell
%LOCALAPPDATA%\VoiSe\presets\
```

Right-click a preset button to select, rename, recreate, choose hotkeys, copy its JSON file, or delete it.
