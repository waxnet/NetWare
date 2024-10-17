using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.RageTab;

public static class ReloadSpeedModifierSection
{
    public static void Draw()
    {
        Menu.NewSection("Reload Speed Modifier");
        DrawToggle();
        DrawSettings();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle("Enabled", Config.Active.ReloadSpeedModifier.Enabled);
        Config.Active.ReloadSpeedModifier.Enabled = enabled;
    }

    private static void DrawSettings()
    {
        // multiplier
        var multiplier = Menu.NewSlider("Multiplier", Config.Active.ReloadSpeedModifier.Multiplier, 1, 50);
        Config.Active.ReloadSpeedModifier.Multiplier = multiplier;
    }
}
