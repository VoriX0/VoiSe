# VoiSe Gate 5.36 — SoundBoard Timeline 10 Percent Polish

Gate 5.36 starts from Gate 5.34 and adjusts only the SoundBoard timeline vertical position.

## Changes

- Window/header version updated to Gate 5.36.
- Sounds wheel catch-zone is expanded 10% further downward compared with Gate 5.33.
- Top expansion is reduced from 30% to 25% and still clamped so it does not go above the tab selection area.
- Right expansion remains 60%.
- Visual SoundBoard design is unchanged.
- Sounds list is still the only thing that scrolls.
- Double click and context menu handling are kept from the working overlay version.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Gate 5.36

Check the former dead zones in the lower and right parts of the Sounds list.


## Gate 5.36

- Based on Gate 5.34.
- Moved the Play/Pause button and timeline line down by 10 px, roughly 10% of the 104 px timeline area.
- Kept the Gate 5.34 wheel-zone tuning unchanged.
