using MelonLoader;

namespace SNBypass;

public class SNBypassMod : MelonMod
{
    public override void OnInitializeMelon()
    {
        ModuleCheckBypass.Initialize();
    }
}