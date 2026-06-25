# ADR-001: Стек MVP

## Статус

Принято для MVP.

## Решение

- Язык: C#.
- Платформа: .NET 8 или новее.
- UI: WinUI 3 / Windows App SDK.
- Audio: NAudio / WASAPI.
- Виртуальный аудиокабель: VB-CABLE.
- Декодирование OGG/Vorbis: NVorbis или NAudio.Vorbis после проверки лицензии.
- Собственный виртуальный драйвер: не входит в MVP.
- Шумоподавление: после MVP.

## Причины

WinUI 3 выбран сразу, чтобы не закладывать будущую миграцию с WinUI 3. VB-CABLE выбран вместо VAC из-за платной зависимости VAC. Собственный драйвер остаётся желательным направлением после MVP, но требует отдельного исследования трудоёмкости, установки и подписи.


## Gate 3 update

The MVP UI framework is WinUI 3 / Windows App SDK. Gate 3 introduces the first visual control panel over the verified unified audio mixer.
