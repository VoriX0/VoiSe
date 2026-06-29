# VoiSe 8.2.0 installer-ready buildfix 1

This buildfix fixes first-run behavior on another computer.

## Included changes

- Forced the application UI theme to `Dark` at the window/root level so controls no longer inherit a light-system look.
- Added a `VB-CABLE required` notice panel in Settings with a direct link to the VB-Audio download page.
- Prevented the audio engine from auto-starting until a VB-CABLE output device (`CABLE Input`) is detected.
- Prevented the `Virtual Output` combo box from silently falling back to ordinary speakers when VB-CABLE is not installed.
- Disabled the manual `Start Engine` button until VB-CABLE is detected.
