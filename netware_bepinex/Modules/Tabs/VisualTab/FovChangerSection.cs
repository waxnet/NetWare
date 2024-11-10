using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.VisualTab;

public static class FovChangerSection
{
    public static void Draw()
    {
        Menu.NewSection("FOV Changer");
        DrawToggle();
        DrawFovValues();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle("Enabled", Config.Active.FovChanger.Enabled);
        Config.Active.FovChanger.Enabled = enabled;
    }
    private static void DrawFovValues()
    {
        // amount
        var amount = Menu.NewSlider("Amount", Config.Active.FovChanger.Amount, 20, 180);
        Config.Active.FovChanger.Amount = amount;
    }
}
