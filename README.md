# VoiSe Gate 6.14 — Settings Layout and Formant

Gate 6.14 keeps the working Gate 6.8 / 6.5 SoundBoard wheel behavior and the Gate 6.10/6.11 Voice Changer scroll behavior.

Changes:

- Settings: `Virtual Mic Master` moved into `Audio devices`, to the right of `Refresh Devices`.
- Settings: bottom log block removed; `Open logs` is now under the `Output` heading.
- Voice Changer: added a separate `Formant` slider with number input.
- DSP: `Pitch` and `Formant` are now separate controls. Pitch changes note height; Formant shifts a compact vocal-resonance model for a bigger/smaller vocal tract effect.

Run:

```powershell
dotnet run --project src/VoiSe.App
```
