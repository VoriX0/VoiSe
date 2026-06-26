# VoiSe Gate 5.21 — SoundBoard Wide Track List

Gate 5.21 keeps the corrected SoundBoard head layout, removes debug borders, and tries a different fix for the fullscreen scrolling problem.

## Changes

- Window/header version updated to Gate 5.21.
- Debug borders are removed.
- SoundBoard head blocks are raised again: `Previous / Next / Stop` and `Play / Timeline / Time` are aligned like in the last good head version.
- SoundBoard body spacing is tightened so the track list starts directly after `Add Track / Delete Track` again.
- Track list and log areas get explicit dynamic height from their visual top edge to the bottom of the app surface.
- Added a window-level `WM_MOUSEWHEEL` fallback: when the cursor is visually over the track-list/log area, the wheel is routed to the internal ScrollViewer even if WinUI hit-testing is shifted in fullscreen.
- Double-click playback for tracks is kept.
- Custom timeline is kept.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## What to check

1. Head layout: the transport block is closer to `No sound`, and the timeline block remains aligned with `Stop`.
2. Track list starts right after the track buttons again.
3. In fullscreen, mouse wheel scrolling works over the actual track list area, including below the 4th track.
4. Settings log scrolling works in the lower part of the log area.
5. Double-clicking a track starts playback.


## Gate 5.21 changes

- Redesigns SoundBoard body: left 25% for category controls and category selector, 5% gap, right 70% for the track list.
- Removes separate/internal track-list scrolling.
- Moves Add Track / Delete Track into the left control column below the category selector.
- Removes the previous window-level wheel routing hacks that caused shifted scroll zones in fullscreen.
- Keeps the successful Head alignment from the previous gate.
