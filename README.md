# VoiSe Gate 7.0 — Scenes Foundation

Gate 7 starts the Scenes feature.

A scene now captures and restores:

- current Voice Changer slider values;
- last applied voice preset name when available;
- Voice Monitor On/Off;
- selected SoundBoard category;
- selected SoundBoard sound;
- Virtual Mic Master;
- SoundBoard → Virtual Mic volume;
- SoundBoard → Headphones volume;
- SoundBoard virtual mic delay.

Scenes are stored as separate JSON files in:

```text
%LOCALAPPDATA%\VoiSe\scenes\
```

Run:

```powershell
dotnet run --project src/VoiSe.App
```
