# VoiSe Gate 6.8 — Voice Changer Scroll Fix

Gate 6.8 keeps the working Gate 6.8 SoundBoard wheel behavior and fixes Voice Changer scrolling by routing mouse wheel events for the Voice Changer tab directly to its own ScrollViewer.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

## Active voice sliders

- Voice Gain
- Gate
- Compressor
- Pitch
- Bass
- Treble
- Distortion
- Robot
- Tremolo
- Echo
- Reverb
- Radio
- Bit Crusher
- Alien

## Pitch behavior

- Negative values lower the voice toward a deeper/bassier sound.
- Positive values raise the voice toward a thinner/squeakier sound.
- Slider range remains `-100..+100` and maps roughly to `-12..+12` semitones.
- Numeric fields may exceed the slider range, but the DSP path clamps pitch to a safe `-24..+24` semitones.

## Removed from Gate 6.4

- Timbre was removed because it sounded too similar to Bass.
- Chorus was removed because the effect was not noticeable enough.
- Alien was kept.

## SoundBoard wheel zone

The SoundBoard wheel catch-zone was expanded again:

- +60% to the right;
- +50% downward.

## Presets

New and recreated presets save all active Gate 6.8 sliders as separate JSON files in:

```powershell
%LOCALAPPDATA%\VoiSe\presets\
```

Existing older presets still load; removed keys are ignored.


## Gate 6.8 change

After comparing Gate 6.5 and Gate 6.6, the centered SoundBoard wheel-zone change was reverted: it replaced the working Gate 6.5 DIP-based SoundBoard wheel catch-zone with a client-pixel centered zone and broke the SoundBoard scroll area. Gate 6.8 restores the Gate 6.5 SoundBoard logic and adds a separate global wheel route for the Voice Changer tab.
