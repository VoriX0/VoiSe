# VoiSe Gate 5.31 — SoundBoard Full Tab Wheel Zone

Gate 5.31 keeps the Gate 5.30 visual design and changes only the wheel-input strategy for the Sounds list.

## Changes

- Window/header version updated to Gate 5.31.
- The current SoundBoard design is preserved.
- Double-click playback and right-click context menu remain handled by the Sounds input overlay.
- Added a low-level mouse wheel hook for the active VoiSe window.
- When the SoundBoard tab is active, the wheel zone is now the whole SoundBoard tab below the tab headers.
- Wheel input from that zone scrolls only the Sounds list.
- Scroll step is reduced to make scrolling smoother.
- The old local overlay wheel handler remains as a fallback.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Check

1. Open the SoundBoard tab.
2. Use the mouse wheel anywhere below the tab headers.
3. Only the Sounds list should scroll.
4. Check the formerly dead areas: below the 4th sound and to the right side of the Sounds list.
5. Double-click a sound row to start playback.
6. Right-click a sound row to open the context menu.
