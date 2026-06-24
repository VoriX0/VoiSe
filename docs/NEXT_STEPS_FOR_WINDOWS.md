# Next steps on Windows machine

1. Install .NET SDK 8+.
2. Install VB-CABLE.
3. Open PowerShell in this repository.
4. Run:

```powershell
./scripts/bootstrap.ps1
```

5. List devices:

```powershell
dotnet run --project src/VoiSe.Gate0.Cli -- --list-devices
```

6. Start Gate 2 unified mixer:

```powershell
dotnet run --project src/VoiSe.Gate0.Cli -- --input "Микрофон (Fifine Microphone)" --virtual-output "CABLE Input" --monitor "Наушники (Realtek(R) Audio)" --sound-file "C:\Path\To\song.wav" --sound-virtual-delay-ms 85
```

7. In Discord/Telegram select `CABLE Output` as microphone.

8. Check:

- microphone reaches Discord/Telegram;
- soundboard reaches headphones immediately;
- soundboard reaches Discord/Telegram with configured delay;
- voice and soundboard are not clipping badly;
- `X` stops the sound;
- `Ctrl+C` stops the prototype.
