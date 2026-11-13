# Com3d2DoorstopFix

A tiny BepInEx 5 plugin for **COM3D2.5** that makes the **KCES** (Kiss Character Edit System / CR EditSystem) editor launched from the game load BepInEx and its plugins.

## The problem

When you open the character editor from COM3D2.5 with a BepInEx/Doorstop install (`winhttp.dll`), the game starts `CR EditSystem.exe` as a child process.
The child inherits the Doorstop environment markers (`DOORSTOP_INITIALIZED` and friends) from the game, so KCES's own Doorstop proxy thinks it was already injected and **never loads BepInEx or any plugin** in KCES.
Starting KCES by hand works, starting it from the game does not.

## The fix

At game start the plugin clears these variables from the game process environment, so processes started later (KCES) do not inherit them:

- `DOORSTOP_INITIALIZED`
- `DOORSTOP_INVOKE_DLL_PATH`
- `DOORSTOP_PROCESS_PATH`
- `DOORSTOP_DISABLE`

The game itself is already injected at that point, so nothing changes for it.

## Install

1. Build (below) or take the DLL from a release.
2. Copy `Com3d2DoorstopFix.dll` to `<COM3D2.5>\BepInEx\plugins`.
3. KCES needs its own BepInEx 5 install (Doorstop `winhttp.dll`, `doorstop_config.ini`, `BepInEx\core`); this plugin does not install it.

## Build

```
dotnet build -c Release -o out -p:GameDir="C:\path\to\COM3D2_5"
```

References `BepInEx.dll` and the game's `UnityEngine.dll` / `UnityEngine.CoreModule.dll` from `GameDir` (not included).

## Notes

- Tested on COM3D2.5 with BepInEx 5.4.x only. Old COM3D2 has no KCES bridge and no CRC support, so there is nothing to fix there.
- No license has been chosen yet.
