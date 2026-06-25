# VoiSe Gate 3.6

WinUI 3 control panel for the validated VoiSe audio engine.

## What works

- Device selection in Settings.
- Start/Stop unified audio engine.
- SoundBoard playback to VB-CABLE and headphones.
- SoundBoard virtual mic delay, default 85 ms.
- Separate SoundBoard volume to virtual mic and headphones.
- Voice Changer sliders applied live.
- Voice Monitor toggle for hearing only the processed voice in headphones.
- Global Virtual Mic Master volume in Settings.

## Run

```powershell
dotnet run --project src/VoiSe.App
```

If WinUI build fails, ensure `global.json` uses .NET SDK 9.0.315 and Windows App SDK tooling is installed.
