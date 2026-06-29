# Gate 8.0 - UI polish and SoundBoard safety

Gate 8.0 is built on top of Gate 7.10 buildfix 4.

Included changes:

- Native title bar colors are forced to black with white caption buttons, matching the dark VoiSe UI.
- The audio engine now starts automatically after saved settings, devices, SoundBoard, Voice Changer presets, and scenes are restored.
- Manual `Start Engine` / `Stop Engine` controls were moved to the bottom of Settings for troubleshooting.
- SoundBoard transport now has a loop toggle button in the left transport block, keeping the controls in a rectangular 2x2 layout.
- When SoundBoard loop is enabled, the current track loops; when disabled, the current track returns to one-shot completion behavior.
- SoundBoard supports drag-and-drop of one or multiple `.wav`, `.mp3`, or `.ogg` files anywhere on the SoundBoard tab.
- A centered drop hint appears while supported files are dragged over the SoundBoard.
- SoundBoard wheel scrolling speed was increased significantly.
- Category deletion now asks for confirmation and shows how many tracks will be deleted.
- Voice Changer preset creation and rename now include icon selection.
- Voice preset tiles use a built-in white icon set by default; emoji fallbacks are also available.
- The default preset icon is now a microphone-style MDL2 glyph.

Notes:

- Drag-and-drop adds files to the currently selected category. If no category is selected, the first category is used.
- External icon packages are not required; icons come from Windows built-in Segoe MDL2 Assets plus emoji fallbacks.
