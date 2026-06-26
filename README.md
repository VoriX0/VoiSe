# VoiSe Gate 5.14 - SoundBoard Overlay Scroll Fix

Gate 5.14 continues polishing the SoundBoard UI.

## Changes

- Window title and visible app header now show **VoiSe Gate 5.14**.
- Track list area is recreated as an explicit top overlay-style area with its own native `ListView` scrolling.
- The track list height is recalculated from the real cursor/list position to the bottom of the window, which should help in maximized/fullscreen mode.
- Settings log area gets an explicit height recalculation as well.
- Current track time and total duration are bottom-aligned with the Stop button while the timeline remains one line above them.
- Double-click on a track still starts playback.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Check

1. Open the app maximized/fullscreen.
2. Add enough tracks to require scrolling.
3. Try mouse-wheel scrolling below the fourth track.
4. Check Settings log scrolling.
5. Check that current time and total duration align with the bottom of the Stop button.

If scrolling still fails, the next diagnostic gate should enable visible debug borders around all major layout zones to find the overlay that steals wheel hit-testing.
