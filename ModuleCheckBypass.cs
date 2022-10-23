using HarmonyLib;
using MelonLoader;
using System.Reflection;

namespace SNBypass;

internal static class ModuleCheckBypass
{
    private static bool _initialized;

    internal static void Initialize()
    {
        if (_initialized)
            return;

        var moduleCheckerType = Assembly.Load("Assembly-CSharp").GetType(ModuleCheckerTypeName);
        if (moduleCheckerType == null)
        {
            Melon<SNBypassMod>.Logger.Warning("Module Checker Not Found");
            return;
        }

        var checkMethod = moduleCheckerType.GetMethod(nameof(ModuleChecker.Method_Public_Boolean_0));
        if (checkMethod == null)
        {
            Melon<SNBypassMod>.Logger.Warning("Module Checker Version Unsupported");
            return;
        }

        Melon<SNBypassMod>.Instance.HarmonyInstance.Patch(checkMethod, new HarmonyMethod(new PatchDel(Patch).Method));
        Melon<SNBypassMod>.Logger.Msg("Module Checker Patched");

        _initialized = true;
    }

    private static bool Patch(ref bool __result)
    {
        __result = false;

        Melon<SNBypassMod>.Logger.Msg("Module Checker Bypassed");

        return false;
    }

    delegate bool PatchDel(ref bool __result);
}
