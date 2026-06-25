# Gate 3.6 — WinUI Control Panel

Gate 3.6 keeps the working Gate 2 unified mixer and moves testing from CLI into a WinUI 3 window.

## Main UX decisions

- `Settings` contains device selection, Start/Stop Engine, and one global `Virtual Mic Master` slider.
- `Voice Changer` contains live voice processing sliders and a `Voice Monitor` toggle.
- `Voice Monitor: On` sends the processed user voice to headphones at 100%.
- `Voice Monitor: Off` mutes only the user's own voice in headphones.
- Voice monitoring does not affect SoundBoard monitoring.
- `SoundBoard` contains file selection, Play/Stop, SoundBoard Virtual Mic Delay, and separate SoundBoard route volumes:
  - `SoundBoard → Virtual Mic`
  - `SoundBoard → Headphones`

## Expected behavior

- Changing voice sliders applies live without manual engine restart.
- Changing `Virtual Mic Master` applies live to everything sent to VB-CABLE.
- Changing SoundBoard route volumes applies live to the current sound.
- SoundBoard delay is applied on the next Play Sound.
- Changing audio devices while the engine is running schedules an automatic engine restart.

## Test command

```powershell
dotnet run --project src/VoiSe.App
```
