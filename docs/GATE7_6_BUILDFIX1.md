# Gate 7.6 buildfix 1

## Fixed

- Restored `NormalizeSceneSoundVolumes` in `SceneStore.cs`.
- Scene button volumes and looped sound volumes are now normalized to the supported `0.0 .. 1.5` range when scenes are loaded or saved.
- Invalid persisted values such as `NaN` or `Infinity` are reset to `1.0`.

## Notes

This buildfix targets the reported compile error:

```text
SceneStore.cs(...): error CS0103: The name "NormalizeSceneSoundVolumes" does not exist in the current context.
```
