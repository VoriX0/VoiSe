# Gate 6.16 — Global Hotkeys Build Fix

## Goal

Make hotkeys usable in SoundBoard, Voice Changer presets, and common SoundBoard transport actions.

## Implemented

- Low-level keyboard hook while VoiSe is running.
- SoundBoard track hotkeys:
  - `Assign Hotkey` from track context menu;
  - hotkey selects and plays that track.
- Voice preset hotkeys:
  - `Preset select` applies the preset once;
  - `Push to talk` applies a preset while held and restores previous sliders on release.
- Settings transport hotkeys:
  - Play;
  - Pause;
  - Stop;
  - Next;
  - Previous.
- Hotkey parsing and normalization for `Ctrl`, `Alt`, `Shift`, letters, numbers, function keys, arrows, Space, Enter, Tab, Esc, etc.

## Storage

- Per-track hotkeys: `%LOCALAPPDATA%\\VoiSe\\soundboard.json`.
- Voice preset hotkeys: `%LOCALAPPDATA%\\VoiSe\\presets\\*.json`.
- Transport hotkeys: `%LOCALAPPDATA%\\VoiSe\\settings.json`.

## Known limitations

- Conflict detection is not yet implemented. If two actions share one hotkey, the first matching group wins.
- Hotkey capture UI is still text-based; a future Gate can replace this with a real "press key combination" capture control.
