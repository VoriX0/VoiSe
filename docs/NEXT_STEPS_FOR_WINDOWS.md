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

6. Start prototype:

```powershell
dotnet run --project src/VoiSe.Gate0.Cli -- --input "Microphone" --virtual-output "CABLE Input" --monitor "Headphones"
```

7. In Discord/Telegram select `CABLE Output` as microphone.

8. Record test results:

- microphone device name:
- VB-CABLE render endpoint name:
- VB-CABLE capture endpoint name in Discord/Telegram:
- monitor device name:
- audible latency subjective score:
- crackling/dropouts:
- CPU usage:
- notes:


## Gate 1.1 / 1.2: SoundBoard monitor delay

Use `--sound-monitor-delay-ms <ms>` to delay soundboard playback in headphones only. The sound still goes to the virtual microphone immediately. This is intended for cases where the user sings or speaks along with the sound and needs the monitoring to line up with what listeners hear through the virtual microphone. Suggested values: 40, 80, 120, 160 ms.
