# VoiSe 8.2.0 installer-ready buildfix 2

This buildfix fixes the XAML compiler error introduced in buildfix 1.

## Included changes

- Removed `RequestedTheme="Dark"` from the `Window` declaration because `Window` is not a `FrameworkElement` and WinUI XAML compiler rejects this property there.
- Kept dark theme enforcement on the root `Grid` and `TabView`, where `RequestedTheme` is valid.
- VB-CABLE protection from buildfix 1 remains unchanged:
  - the engine does not auto-start until `CABLE Input` / VB-CABLE is detected;
  - ordinary speakers are not used as virtual output fallback;
  - Settings shows the VB-CABLE required notice and download link.
