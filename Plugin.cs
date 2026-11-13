using BepInEx;
using System;

namespace Com3d2DoorstopFix
{
    // Doorstop marks the process environment (DOORSTOP_INITIALIZED...) so a child process that loads the same winhttp.dll proxy
    // does not inject twice. COM3D2 starts CR EditSystem (KCES) as a child process, which inherits the marker, so BepInEx
    // (and every plugin) never loads in KCES. Clear the markers once the game is up: they are only meant for this process.
    [BepInPlugin("local.com3d2.doorstopfix", "Doorstop env fix for KCES", "0.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            foreach (var n in new[] { "DOORSTOP_INITIALIZED", "DOORSTOP_INVOKE_DLL_PATH", "DOORSTOP_PROCESS_PATH", "DOORSTOP_DISABLE" })
            {
                if (Environment.GetEnvironmentVariable(n) != null)
                {
                    Logger.LogInfo("clearing " + n + "=" + Environment.GetEnvironmentVariable(n));
                    Environment.SetEnvironmentVariable(n, null);
                }
            }
        }
    }
}
