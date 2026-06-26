# Gate 6.13 — Logs Button and Playing Title

Changes:

- Removed the small embedded log text box from Settings.
- Settings now shows only an `Open logs` button and a short hint.
- Full log view now uses an in-memory log buffer and a ScrollViewer/TextBlock dialog, so all log entries are shown instead of only the first line.
- SoundBoard transport status now shows the active sound name:
  - `playing: {sound name}`
  - `paused: {sound name}`
- SoundBoard scroll logic remains the known-good Gate 6.8 / Gate 6.5 version.
- Voice Changer scroll remains extended down to the bottom of the window.
