# VoiSe Gate 3.4 — WinUI startup diagnostics

This build uses a custom Program.Main and console output to diagnose WinUI startup.

Run:

```powershell
dotnet run --project src/VoiSe.App
```

If no window appears, check:

```powershell
Get-Content "$env:LOCALAPPDATA\VoiSe\gate3-startup.log" -Tail 120
```

# VoiSe Development Starter — Gate 3

Gate 3 adds the first WinUI 3 visual control panel on top of the verified Gate 2 unified mixer.

## Current status

Confirmed on Windows before Gate 3:

- Gate 0: physical mic -> VoiSe -> VB-CABLE -> monitoring works.
- Gate 1: SoundBoard playback works.
- Gate 1.3: SoundBoard monitoring starts immediately; virtual mic receives sound after delay.
- Gate 2: unified internal mixer works.

Gate 3 goal:

```text
WinUI 3 app
-> select devices
-> start unified audio engine
-> play SoundBoard file
-> adjust SoundBoard Virtual Mic Delay, default 85 ms
```

## Requirements

- Windows 10 x64 22H2 or Windows 11 x64
- .NET SDK 8 or newer
- VB-CABLE installed
- Visual Studio 2022 or Build Tools with Windows App SDK support

## Quick start

```powershell
./scripts/bootstrap.ps1
```

Run the visual app:

```powershell
dotnet run --project src/VoiSe.App
```

CLI prototype is still available:

```powershell
dotnet run --project src/VoiSe.Gate0.Cli -- --list-devices
```

## Gate 3 UI

Tabs:

- `SoundBoard` — choose and play one sound file, set virtual mic delay.
- `Voice Changer` — basic voice sliders applied when the engine starts.
- `Scenes` — placeholder for the final navigation structure.
- `Settings` — audio device selection and engine start/stop.

## Recommended tested values

- Input: `Микрофон (Fifine Microphone)`
- Virtual Output: `CABLE Input`
- Monitor: `Наушники (Realtek(R) Audio)`
- SoundBoard Virtual Mic Delay: `85 ms`

## Documentation

- `docs/GATE3_WINUI_CONTROL_PANEL.md` — Gate 3 test plan
- `docs/GATE2_UNIFIED_MIXER.md` — Gate 2 test plan
- `docs/ADR-001-tech-stack.md` — MVP technology stack
- `docs/ADR-002-audio-contract.md` — audio contract
- `docs/ROADMAP.md` — next steps


## Gate 3.1 build note

This package includes `<EnableMsixTooling>true</EnableMsixTooling>` in `src/VoiSe.App/VoiSe.App.csproj` to allow Windows App SDK PRI/MSIX build tasks to resolve during `dotnet build`/`dotnet run` on machines where the task is not found under the .NET SDK path. It also includes `global.json` pinned to .NET SDK 9.0.315 with latestFeature roll-forward.


## Gate 3.2 startup diagnostics

If `dotnet run --project src/VoiSe.App` builds successfully but no window appears, check the startup log:

```powershell
Get-Content "$env:LOCALAPPDATA\VoiSe\gate3-startup.log" -Tail 80
```

Gate 3.2 delays audio device enumeration until after the window is created and logs startup exceptions to `%LOCALAPPDATA%\VoiSe\gate3-startup.log`.


## Gate 3.4 fix

Gate 3.4 fixes WinUI startup issues found in Gate 3.3:

- fixes custom `Program.Main` for WinUI `Application.Start`;
- adds `XamlControlsResources` to `App.xaml` so `TabView` resources are available;
- keeps startup logging at `%LOCALAPPDATA%\VoiSe\gate3-startup.log`.

Run:

```powershell
dotnet run --project src/VoiSe.App
```

If the window does not appear, inspect:

```powershell
Get-Content "$env:LOCALAPPDATA\VoiSe\gate3-startup.log" -Tail 120
```
