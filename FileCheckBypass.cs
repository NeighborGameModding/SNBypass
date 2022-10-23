using HarmonyLib;
using MelonLoader;
using System;
using System.IO;
using UnhollowerBaseLib;

namespace SNBypass;

[HarmonyPatch(typeof(Il2CppSystem.IO.Directory), nameof(Il2CppSystem.IO.Directory.GetFiles), new Type[] { typeof(string), typeof(string) })]
public static class FileCheckBypass
{
    private static bool Prefix([HarmonyArgument(0)] string path, [HarmonyArgument(1)] string filter, ref Il2CppStringArray __result)
    {
        if (filter != "*.dll")
            return true;

        __result = new Il2CppStringArray(new string[]
        {
                    Path.Combine(path, "GameAssembly.dll"),
                    Path.Combine(path, "UnityPlayer.dll")
        });

        Melon<SNBypassMod>.Logger.Msg("File Check Bypassed");
        return false;
    }
}
