# Gate 8.0 buildfix 3 — Icon picker wheel zones

Fixes the remaining dead wheel zones inside the Voice Changer preset icon picker.

Changes:
- The icon picker now routes mouse-wheel events from the whole icon host to the picker ScrollViewer.
- Toggle buttons fill their 52x52 cells, reducing gaps between icons.
- Wheel events over icon buttons are explicitly forwarded to the picker ScrollViewer.
- The main Voice Changer wheel hook is still suppressed while the dialog is open, so the Voice Changer page no longer steals wheel input from the picker.

No changes to the icon set itself in this buildfix.
