# VoiSe Gate 5.29 — SoundBoard Input Overlay Fix

Gate 5.29 keeps the current SoundBoard design, but changes the way input is handled for the right-side Sounds list.

## Why this gate exists

In Gate 5.28 the same problem affected three actions at once:

- mouse wheel did not work in the real lower/right part of the Sounds list;
- double-click did not start a sound;
- right-click context menu did not open.

That points to a shifted WinUI hit-test/input area rather than to separate bugs in scroll, double-click, or context menu code.

## Changes

- Visual design is preserved.
- The right-side Sounds list still uses the custom visual scroller.
- Added an invisible input overlay placed exactly over the visual Sounds list.
- The overlay handles:
  - wheel scrolling;
  - single-click selection;
  - manual double-click playback;
  - right-click context menu.
- Removed the previous window-level `WM_MOUSEWHEEL` routing hack.
- Reduced wheel step for smoother scrolling.
- Window/header version updated to Gate 5.29.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Check

1. Open SoundBoard.
2. Scroll while the cursor is over the lower/right part of the Sounds list.
3. Double-click a sound row; it should start playback.
4. Right-click a sound row; the context menu should open.
5. Visual layout should remain the same as Gate 5.28.
