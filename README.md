# VoiSe Development Starter — Gate 2

Стартовый пакет разработки VoiSe после успешных Gate 0 и Gate 1.

Gate 2 проверяет более правильную архитектуру аудиодвижка:

```text
Physical microphone
-> voice processing
-> unified route mixer
-> VB-CABLE Input
-> CABLE Output as microphone in Discord/Telegram

SoundBoard
-> headphones immediately
-> virtual microphone with configurable delay
```

## Требования

- Windows 10 x64 22H2 или Windows 11 x64
- .NET SDK 8 или новее
- Установленный VB-CABLE
- Visual Studio 2022 с Windows App SDK workload — для будущего WinUI 3 UI

## Быстрый старт

```powershell
./scripts/bootstrap.ps1
```

Посмотреть аудиоустройства:

```powershell
dotnet run --project src/VoiSe.Gate0.Cli -- --list-devices
```

Запустить Gate 2:

```powershell
dotnet run --project src/VoiSe.Gate0.Cli -- --input "Микрофон (Fifine Microphone)" --virtual-output "CABLE Input" --monitor "Наушники (Realtek(R) Audio)" --sound-file "C:\Path\To\song.wav" --sound-virtual-delay-ms 85
```

В Discord/Telegram нужно выбрать входной микрофон **CABLE Output**.

## Runtime keys

- `S` — проиграть soundboard-файл через единый микшер
- `X` — остановить soundboard-файл
- `Ctrl+C` — выйти

## Главное изменение Gate 2

В Gate 1 звук soundboard и голос шли отдельными WASAPI-потоками.
В Gate 2 они смешиваются внутри VoiSe route mixer, и limiter применяется после суммы голоса и звука.

Это ближе к будущему MVP с WinUI 3, сценами, громкостями и настройками.

## Документация

- `docs/GATE2_UNIFIED_MIXER.md` — тест-план Gate 2
- `docs/ADR-001-tech-stack.md` — стек MVP
- `docs/ADR-002-audio-contract.md` — аудиоконтракт
- `docs/ROADMAP.md` — следующие этапы
