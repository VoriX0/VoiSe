# Gate 3.5 — WinUI 3 Control Panel

Gate 3.5 is the first usable visual shell over the unified audio engine.

## Confirmed before Gate 3.5

- Gate 0: microphone → VB-CABLE → monitor works.
- Gate 1: soundboard file playback works.
- Gate 1.3: SoundBoard Virtual Mic Delay works correctly with 85 ms on the user's Windows machine.
- Gate 2: mic and soundboard are mixed inside VoiSe and routed to VB-CABLE and monitoring.
- Gate 3.4: WinUI 3 window opens and audio engine can be controlled from UI.

## Gate 3.5 changes

- SoundBoard tab is no longer overloaded with mixer settings.
- Output and soundboard routing settings moved to Settings.
- Live-updatable settings now apply to the running engine where possible.
- Device changes trigger automatic engine restart instead of requiring manual Stop/Start.

## Current limitations

- No persistent config yet.
- No scenes implementation yet.
- No real preset storage yet.
- Pitch/formants are still post-Gate work.
- SoundBoard Virtual Mic Delay applies to the next Play Sound call; current playback delay is not retimed mid-playback.
