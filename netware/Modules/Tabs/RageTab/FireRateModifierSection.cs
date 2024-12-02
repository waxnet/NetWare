using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.RageTab;

public static class FireRateModifierSection
{
    public static void Draw()
    {
        Menu.NewSection("Fire Rate Modifier");
        DrawToggle();
        DrawSettings();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle("Enabled", Config.Active.FireRateModifier.Enabled);
        Config.Active.FireRateModifier.Enabled = enabled;
    }

    private static void DrawSettings()
    {
        Menu.NewTitle("Settings");

        // multiplier
        var multiplier = Menu.NewSlider("Multiplier", Config.Active.FireRateModifier.Multiplier, 1, 50);
        Config.Active.FireRateModifier.Multiplier = multiplier;
    }
}
