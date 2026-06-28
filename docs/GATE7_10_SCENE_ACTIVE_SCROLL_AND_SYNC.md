# Gate 7.10 — Scene Active Playback, Scroll Routing, and SoundBoard Independence

Changes:

- Restored the Gate 6.8 / 6.5 wheel-routing approach for tabs that need manual scrolling.
  - SoundBoard keeps the known-good wheel zone.
  - Voice Changer keeps extended vertical routing.
  - Scenes now routes wheel input to the scene sound button list.
  - Settings now routes wheel input to the settings scroll viewer instead of returning `false`.
- Scene playback controls are active-only:
  - scene sound button clicks do not play sounds unless that scene is active;
  - looped sound play/pause and loop-start controls require the selected scene to be active;
  - scene timelines are disabled unless the selected scene is active.
- Adding a scene sound button now initializes that button to 100% headphones and 100% virtual mic volume.
- Creating a new scene now starts with no selected voice preset and 100% scene/SoundBoard volumes.
- Applying a scene no longer changes the current SoundBoard category or selected SoundBoard sound.
- Scene sound hotkeys restart their assigned one-shot from the beginning on repeated presses.
  Mouse clicks keep the existing play/pause/resume behavior.
- Looped sound names are displayed from the SoundBoard source sound, so renaming the source sound updates the looped sound label in scenes.
