using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.VisualTab;

public static class FovChangerSection
{
    public static void Draw()
    {
        Menu.NewSection("FOV Changer");

        DrawToggle();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.FovChanger.Enabled,
            Config.Active.FovChanger.KeyBind
            );

        Config.Active.FovChanger.Enabled = enabled.Value;
        Config.Active.FovChanger.KeyBind = enabled.KeyBind;
    }

    private static void DrawFovValues()
    {
        // amount
        var amount = Menu.NewSlider("Amount", Config.Active.FovChanger.Amount, 20, 180);
        Config.Active.FovChanger.Amount = amount;

        // reset
        Menu.NewButton("Reset", () => Data.ResetFov = true);
    }
}
