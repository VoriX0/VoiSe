# VoiSe Gate 4.3

Gate 4.3 fixes settings restore timing.

Changes:
- settings restore runs after first window activation, not from constructor DispatcherQueue async flow;
- settings are applied with `_loadingSettings=true`, so UI events do not overwrite restored values;
- devices are restored by ID, exact name, partial name, then fallback;
- scalar settings and last sound file are restored before device refresh;
- logs show: `Saved scalar settings applied`, `Devices refreshed`, `Settings restored`.

Run:

```powershell
dotnet run --project src/VoiSe.App
```

Settings file:

```powershell
Get-Content "$env:LOCALAPPDATA\VoiSe\settings.json"
```
