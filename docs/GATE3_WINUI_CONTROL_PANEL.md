# Gate 3 — WinUI 3 Control Panel

Gate 3 moves VoiSe from CLI testing to a minimal visual control panel while keeping the already verified Gate 2 unified audio engine.

## Goal

Verify that the WinUI 3 application can control the same audio path:

```text
Microphone + SoundBoard
-> unified VoiSe mixer
-> limiter
-> VB-CABLE Input
-> CABLE Output as microphone in Discord/Telegram

SoundBoard
-> monitor immediately
-> virtual microphone with configurable delay, default 85 ms
```

## What is functional in Gate 3

- Device refresh.
- Input microphone selection.
- Virtual output selection, usually `CABLE Input`.
- Monitor output selection.
- Start / Stop engine.
- Sound file picker for WAV / MP3 / OGG.
- Play / Stop sound.
- SoundBoard virtual mic delay slider, default 85 ms.
- Separate sound volume sliders for virtual microphone and monitor.
- Basic Voice Changer sliders applied on engine start.
- Basic log panel.

## What is intentionally not implemented yet

- Scenes data model and persistence.
- Real soundboard categories.
- Hotkeys.
- Tray mode.
- Live update of voice settings while the engine is running.
- Full preset system.

## How to run

From the repository root:

```powershell
./scripts/bootstrap.ps1
```

Then run the app from Visual Studio or from CLI:

```powershell
dotnet run --project src/VoiSe.App
```

If WinUI 3 runtime/tooling is missing, install Visual Studio 2022 with Windows App SDK support and .NET desktop development workload.

## Manual test

1. Open VoiSe Gate 3.
2. Go to Settings.
3. Select:
   - Input Microphone: `Микрофон (Fifine Microphone)`
   - Virtual Output: `CABLE Input`
   - Monitor Output: `Наушники (Realtek(R) Audio)`
4. Click `Start Engine`.
5. Go to SoundBoard.
6. Select a sound file.
7. Keep `Virtual Mic Delay` at 85 ms.
8. Click `Play Sound`.
9. Confirm:
   - voice reaches CABLE Output;
   - sound reaches headphones immediately;
   - sound reaches virtual microphone after 85 ms;
   - `Stop Sound` works;
   - `Stop Engine` releases devices.


## Gate 3.4 startup fix

Gate 3.4 fixes two startup problems: the custom WinUI entry point no longer assigns to the Application.Start callback parameter, and App.xaml now merges WinUI XamlControlsResources required by controls such as TabView.
