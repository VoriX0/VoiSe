# VoiSe Gate 6.3 — Voice Connected Effects

Gate 6.3 expands the Voice Changer tab with connected real-time DSP sliders.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## New active voice sliders

- Voice Gain
- Gate
- Compressor
- Bass
- Treble
- Distortion
- Robot
- Tremolo
- Echo
- Reverb
- Radio
- Bit Crusher

Sliders remain -100..+100. Numeric boxes can store -9999..+9999; the real-time DSP clamps extreme values internally to keep the audio path stable.

## Presets

New and recreated presets now save all active Gate 6.3 sliders as separate JSON files in:

```powershell
%LOCALAPPDATA%\VoiSe\presets\
```

Existing older presets still load; missing new slider keys simply keep the current/default slider values.
