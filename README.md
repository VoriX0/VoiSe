# VoiSe Gate 5.1 — SoundBoard Layout

Gate 5.1 доводит вкладку SoundBoard до продуктовой компоновки:

- заголовок окна: `VoiSe Gate 5.1 - SoundBoard Layout`;
- SoundBoard разделён на `head / body / bottom`;
- задержка `SoundBoard Virtual Mic Delay` перенесена в `Settings`;
- в head добавлены transport-кнопки: `Prev`, `Play`, `Pause/Resume`, `Stop`, `Next`;
- в head добавлен timeline текущего звука с текущим временем и общей длительностью;
- в head добавлены отдельные слайдеры `SoundBoard → Virtual Mic` и `SoundBoard → Headphones`;
- в body добавлены кнопки категорий: создать, переименовать, удалить;
- в body добавлены кнопки треков: добавить, удалить;
- в body добавлен выпадающий список категорий и список треков выбранной категории;
- по правому клику на треке доступно меню: Play, Assign Hotkey, Rename, Choose Another File, Delete From Category;
- в bottom выводятся: количество категорий, количество звуков в выбранной категории, счётчик использования выбранной категории;
- библиотека продолжает храниться в `%LOCALAPPDATA%\VoiSe\soundboard.json`;
- файлы треков копируются в `%LOCALAPPDATA%\VoiSe\sounds`.

## Запуск

```powershell
dotnet run --project src/VoiSe.App
```

## Проверка

1. В заголовке окна должно быть `VoiSe Gate 5.1`.
2. Перейди в `Settings` и проверь, что задержка SoundBoard находится там.
3. В `SoundBoard` создай категорию.
4. Добавь WAV/MP3/OGG трек.
5. Проверь Play / Pause / Resume / Stop.
6. Проверь Prev / Next.
7. Проверь, что timeline движется во время воспроизведения.
8. Проверь правый клик по треку.
9. Проверь переименование категории и трека.
10. Перезапусти приложение и проверь восстановление библиотеки.
