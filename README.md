# VoiSe Gate 5.27 — SoundBoard Tab Wheel Sound Scroll

Gate 5.27 keeps the Gate 5.26 visual design, restores double-click playback for custom sound rows, and changes mouse-wheel handling so the whole SoundBoard tab scrolls only the sound list.

## Changes

- Window/header version updated to Gate 5.27.
- Visual layout from Gate 5.26 is preserved.
- Double-click on a sound row starts playback again.
- The whole SoundBoard tab now acts as the mouse-wheel zone.
- Wheel input scrolls only the right-side Sounds list.
- The custom overlay sound scroller remains; the old ListView internal scrolling remains removed.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Check

1. Open SoundBoard.
2. Put the cursor anywhere inside the SoundBoard tab and use the mouse wheel.
3. Only the Sounds list should scroll.
4. Double-click a sound row; it should start playback.
