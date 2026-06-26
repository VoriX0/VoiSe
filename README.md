# VoiSe Gate 6.4 — Voice Timbre and Fun Effects

Gate 6.4 adds the required **Timbre** slider for moving the voice color toward a darker/bassier or brighter/squeakier tone, plus a couple of extra fun DSP effects.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Active voice sliders

- Voice Gain
- Gate
- Compressor
- Timbre
- Bass
- Treble
- Distortion
- Robot
- Tremolo
- Echo
- Reverb
- Radio
- Bit Crusher
- Chorus
- Alien

## Timbre behavior

- Negative values: darker, bassier voice color.
- Positive values: brighter, thinner, squeakier voice color.

This is still a real-time DSP approximation, not a full formant/pitch shifter. Proper pitch/formant shifting remains a separate Gate because it needs a dedicated library.

## Presets

New and recreated presets save all active Gate 6.4 sliders as separate JSON files in:

```powershell
%LOCALAPPDATA%\VoiSe\presets\
```

Existing older presets still load; missing new slider keys simply keep the current/default slider values.
