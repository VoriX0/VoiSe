# VoiSe Gate 3.5 — WinUI 3 Control Panel

Gate 3.5 keeps the proven Gate 2 unified mixer and moves testing from CLI to a WinUI 3 window.

## What's new in Gate 3.5

- WinUI 3 window opens with target navigation: SoundBoard, Voice Changer, Scenes, Settings.
- SoundBoard tab now contains only file selection and Play/Stop controls.
- Mixer-related controls moved to Settings:
  - Virtual Mic Master volume
  - Monitor Master volume
  - SoundBoard Virtual Mic Delay, default 85 ms
  - SoundBoard → Virtual Mic volume
  - SoundBoard → Monitor volume
- Voice Changer sliders are applied live to the running engine.
- Master output volume sliders are applied live to the running engine.
- SoundBoard route volume sliders update the currently playing sound.
- Changing audio devices while the engine is running schedules an automatic engine restart.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Test checklist

1. Open the app.
2. In Settings choose:
   - Input Microphone: Fifine Microphone
   - Virtual Output: CABLE Input
   - Monitor Output: Realtek headphones
3. Start Engine.
4. In SoundBoard choose a WAV/MP3/OGG file and press Play Sound.
5. Confirm headphones play immediately and CABLE Output gets the sound with the configured delay.
6. Change Voice Changer sliders while running — the sound should change without manual restart.
7. Change Virtual Mic Master and Monitor Master sliders — output volumes should update live.
8. Change device selection while running — engine should auto-restart.
