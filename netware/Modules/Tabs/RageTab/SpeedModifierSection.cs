using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.RageTab;

public sealed class SpeedModifierSection
{
    public static void Draw()
    {
        Menu.NewSection("Speed Modifier");
        DrawToggle();
        DrawSettings();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle("Enabled", Config.Active.SpeedModifier.Enabled);
        Config.Active.SpeedModifier.Enabled = enabled;
    }

    private static void DrawSettings()
    {
        Menu.NewTitle("Settings");

        // multiplier
        var multiplier = Menu.NewSlider("Multiplier", Config.Active.SpeedModifier.Multiplier, 1, 30);
        Config.Active.SpeedModifier.Multiplier = multiplier;
    }
}
