using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.RageTab;

public static class FlySection
{
    public static void Draw()
    {
        Menu.NewSection("Fly");
        DrawToggle();
        DrawSettings();
        DrawOther();
    }

    private static void DrawToggle()
    {
        // enabled
        var (Value, KeyBind) = Menu.NewToggle(
                "Enabled",
                Config.Active.Fly.Enabled,
                Config.Active.Fly.KeyBind
            );
        Config.Active.Fly.Enabled = Value;
        Config.Active.Fly.KeyBind = KeyBind;
    }

    private static void DrawSettings()
    {
        Menu.NewTitle("Speed Settings");

        // horizontal speed
        var horizontalSpeed = Menu.NewSlider("Horizontal Speed", Config.Active.Fly.HorizontalSpeed, 1, 50);
        Config.Active.Fly.HorizontalSpeed = horizontalSpeed;

        // vertical speed
        var verticalSpeed = Menu.NewSlider("Vertical Speed", Config.Active.Fly.VerticalSpeed, 1, 50);
        Config.Active.Fly.VerticalSpeed = verticalSpeed;
    }

    private static void DrawOther()
    {
        Menu.NewTitle("Other");

        // spin
        var spin = Menu.NewToggle("Spin", Config.Active.Fly.Spin);
        Config.Active.Fly.Spin = spin;
    }
}
