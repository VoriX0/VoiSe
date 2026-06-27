# VoiSe Gate 6.16 — Global Hotkeys Build Fix

Gate 6.16 adds global hotkeys on top of the working Gate 6.14 Settings layout, SoundBoard scroll behavior, Voice Changer Pitch/Formant controls, preset JSON export, and SoundBoard playing-title UI.

## What is included

- Window title/version updated to **VoiSe Gate 6.16**.
- Global keyboard hook for hotkeys while VoiSe is running.
- SoundBoard per-sound hotkeys now execute:
  - right click a sound → **Assign Hotkey**;
  - example format: `Ctrl+Alt+1`, `Ctrl+Shift+F1`, `Alt+Space`;
  - pressing the hotkey selects and plays that sound.
- Voice preset hotkeys now execute:
  - right click preset → **Choose hotkey**;
  - **Preset select**: press once to apply the voice preset;
  - **Push to talk**: hold to temporarily apply the voice preset, release to restore previous slider state.
- Settings now has **Hotkeys → Configure transport hotkeys** for:
  - Play;
  - Pause;
  - Stop;
  - Next;
  - Previous.
- Transport hotkeys control the SoundBoard transport only.
- Hotkey strings are normalized before saving.
- Existing SoundBoard, Settings and Voice Changer layout is preserved.

## Notes

- Prefer modifier combinations like `Ctrl+Alt+1` or `Ctrl+Shift+F1` to avoid stealing normal typing keys.
- If the same hotkey is assigned to multiple actions, the first matched action wins in this order:
  1. SoundBoard sound hotkey;
  2. Voice preset hotkey;
  3. Transport hotkey.
- SoundBoard hotkeys are saved in `soundboard.json`.
- Voice preset hotkeys are saved inside the individual preset JSON files.
- Transport hotkeys are saved in `settings.json`.

## Run

```powershell
dotnet run --project src/VoiSe.App
```
