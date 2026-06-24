# Gate 2 Unified Mixer Prototype

## Goal

Gate 2 moves the working Gate 0/Gate 1 audio route closer to the MVP architecture.
Instead of playing the microphone and soundboard as separate WASAPI output streams, Gate 2 uses a single internal route mixer per destination:

```text
microphone capture
-> voice processing
-> mic queue for virtual route
-> mic queue for monitor route

soundboard transport
-> immediate monitor route
-> delayed virtual microphone route

route mixer
-> mic + soundboard
-> limiter
-> VB-CABLE Input / headphones
```

## Why this matters

The final WinUI 3 app needs one controllable audio engine, not many independent players.
Gate 2 is the first step toward:

- one limiter after mixing;
- separate monitor and virtual microphone volumes;
- soundboard virtual microphone delay slider;
- future foreground/background sound slots;
- future Scenes integration.

## Run

```powershell
dotnet run --project src/VoiSe.Gate0.Cli -- `
  --input "Микрофон (Fifine Microphone)" `
  --virtual-output "CABLE Input" `
  --monitor "Наушники (Realtek(R) Audio)" `
  --sound-file "C:\Path\To\song.wav" `
  --sound-virtual-delay-ms 85
```

Runtime keys:

- `S` — play the selected sound through the unified mixer
- `X` — stop the selected sound
- `Ctrl+C` — stop VoiSe

## Expected result

- Live voice reaches `CABLE Input` and headphones.
- SoundBoard reaches headphones immediately.
- SoundBoard reaches `CABLE Input` after the configured delay.
- Voice and SoundBoard are limited after mixing.
- There is only one VoiSe output stream per output route.

## Known limitations

- Gate 2 still uses a simple prototype limiter.
- Gate 2 supports one foreground sound at a time.
- Background loop sounds are not implemented yet.
- Pitch/formant effects are not implemented yet.
- Noise suppression is intentionally post-MVP.
