using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.RageTab;

public static class RecoilModifierSection
{
    public static void Draw()
    {
        Menu.NewSection("Recoil Modifier");
        DrawToggle();
        DrawSettings();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle("Enabled", Config.Active.RecoilModifier.Enabled);
        Config.Active.RecoilModifier.Enabled = enabled;
    }

    private static void DrawSettings()
    {
        // multiplier
        var multiplier = Menu.NewSlider("Multiplier", Config.Active.RecoilModifier.Multiplier, 0, 1);
        Config.Active.RecoilModifier.Multiplier = multiplier;
    }
}
