# VoiSe Gate 6.13 — Logs Button and Playing Title

Gate 6.13 keeps the working Gate 6.8 / 6.5 SoundBoard wheel behavior and the Gate 6.10/6.11 Voice Changer scroll behavior.

## What changed

- SoundBoard scroll logic is unchanged from the working Gate 6.8/6.5 calibration.
- Voice Changer scroll remains extended down to the bottom fullscreen area.
- Voice Changer sliders are now grouped 4 per row instead of 3 per row.
- Settings log window height is reduced again.
- Settings log wheel zone is compact and starts from the actual log textbox, so it should not steal wheel events from Settings controls above.
- Added **Open fullscreen** button for the Settings log.
- Preset context menu still includes **Copy JSON file** for quick sharing.

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
