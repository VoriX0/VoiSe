# Gate 7.5 — Scene volume controls

## Changes

- Added per-scene volume controls for the single looped sound:
  - headphones / monitor route volume;
  - virtual microphone route volume.
- Added per-scene volume controls for regular scene sound buttons:
  - headphones / monitor route volume;
  - virtual microphone route volume.
- The looped sound volume sliders are shown directly after the `Looped Sound` heading.
- The scene button volume sliders are shown directly after the `Scene sound buttons` heading.
- Scene sound buttons now use the scene button volume pair instead of the global SoundBoard volume pair.
- Scene looped sound playback and autostart now use the looped sound volume pair instead of the global SoundBoard volume pair.
- Global SoundBoard sliders remain available for normal SoundBoard playback outside scene-specific playback.

## Data model

`VoiSeScene` schema version is now 4 and includes:

- `LoopedSoundVirtualMicVolume`
- `LoopedSoundHeadphonesVolume`
- `SceneButtonsVirtualMicVolume`
- `SceneButtonsHeadphonesVolume`

All values default to `1.0` for older scene files.
