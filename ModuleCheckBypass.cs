using HarmonyLib;
using MelonLoader;
using System.Linq;
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

        var nameStart = ModuleCheckerCheckMethod.Remove(ModuleCheckerCheckMethod.Length - 1);
        var checkMethods = moduleCheckerType.GetMethods().Where(x => x.Name.StartsWith(nameStart)).ToArray();
        if (checkMethods.Length == 0)
        {
            Melon<SNBypassMod>.Logger.Warning("Module Checker Version Unsupported");
            return;
        }

        var patch = new HarmonyMethod(new PatchDel(Patch).Method);
        foreach (var checkMethod in checkMethods)
        {
            Melon<SNBypassMod>.Instance.HarmonyInstance.Patch(checkMethod, patch);
        }

        Melon<SNBypassMod>.Logger.Msg($"Module Checker Patched ({checkMethods.Length} {(checkMethods.Length == 1 ? "method" : "methods")})");

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
