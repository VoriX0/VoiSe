# Gate 7.9 — Scene button layout and hotkey behavior

## Scene sound buttons

- Scene sound buttons are slightly wider and slightly shorter.
- The scene/SoundBoard hotkey text is displayed on the same line as the sound name, aligned to the right.
- The button timeline remains under the title row.
- The add sound button keeps the same button shell and the plus sign is centered directly in the button.

## Looped sound control

- The looped sound `Play once` action was removed.
- The second looped sound action is now `Play / Pause` for the current looped/background sound.
- The pause glyph uses the same Segoe MDL2 Assets pause symbol as the SoundBoard timeline.
- The looped sound timeline no longer has its own play/pause button.

## Hotkeys

- The Settings button was renamed from `Configure transport hotkeys` to `Hotkeys`.
- The scene action hotkey buttons were removed from the scene editor.
- `Disable scene` is configured in the shared Hotkeys dialog.
- `Stop one-shots` and `Pause one-shots` no longer have separate hotkey fields.
- When no scene is active, the transport hotkeys keep their SoundBoard behavior.
- When a scene is active:
  - `Play / Pause` toggles pause/resume for active scene one-shot sounds only.
  - `Stop` stops active scene one-shot sounds only.
  - the looped/background sound is not stopped or paused by these transport hotkeys.
  - `Next` and `Previous` are blocked because SoundBoard transport is locked while a scene is active.
