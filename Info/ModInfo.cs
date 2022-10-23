using MelonLoader;
using SNBypass.Info;
using System;

[assembly: MelonInfo(typeof(SNBypass.SNBypassMod), ModInfo.Name, ModInfo.Version, ModInfo.Author, ModInfo.GitLink)]
[assembly: MelonGame("Hologryph")]
[assembly: MelonColor(ConsoleColor.Red)]
[assembly: MelonAuthorColor(ConsoleColor.DarkRed)]

namespace SNBypass.Info;

public static class ModInfo
{
    public const string Version = "1.0.0";
    public const string Name = "SNBypass";
    public const string Author = "SN Modding";
    public const string GitLink = "https://github.com/NeighborGameModding/SNBypass";
}
