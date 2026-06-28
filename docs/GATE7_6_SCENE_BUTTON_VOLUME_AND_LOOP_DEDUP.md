# Gate 7.6 — Scene button volume and loop dedup

## Changes

- Removed scene-level one-shot volume sliders from the `Scene sound buttons` header.
- Added per-button scene volumes to `SceneSoundButton`:
  - `HeadphonesVolume`
  - `VirtualMicVolume`
- Each scene sound button context flyout now contains two sliders:
  - Headphones
  - Virtual Mic
- Scene sound button playback and scene hotkey playback use the button-specific volume values.
- The loop start button icon was changed from `↻` to `∞`.
- Removed the visible border around the scene editor panel to keep the layout scalable.
- Fixed likely duplicated playback on scene apply:
  - looped sounds are no longer used as the regular SoundBoard selection candidate when a scene is applied;
  - scene loop autostart stops the previous SoundBoard transport before starting the new loop;
  - starting a loop in `SoundboardTransport` now clears stale overlays.

## Notes

- Legacy scene-level `SceneButtonsVirtualMicVolume` and `SceneButtonsHeadphonesVolume` are kept as compatibility/default values for old scenes and newly added buttons.
- Looped sound still uses the two scene-level loop volume sliders after `Looped Sound`.
- One-shot scene sounds can still play as overlays on top of the active looped sound.
