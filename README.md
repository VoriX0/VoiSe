# VoiSe Gate 5.28 — SoundBoard Global Wheel Fix

Gate 5.28 keeps the current visual design, fixes double-click playback for custom sound rows, and bypasses shifted WinUI hit-test zones by routing WM_MOUSEWHEEL at the window level while the SoundBoard tab is selected.

## Changes

- Window/header version updated to Gate 5.28.
- Visual layout from Gate 5.26/5.27 is preserved.
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

## Gate 5.28 notes

- The whole app window routes mouse wheel to the Sounds scroller when the SoundBoard tab is active.
- Scroll step is reduced for smoother movement.
- Double-click playback is handled manually on pointer press, so it does not depend only on WinUI DoubleTapped routing.
