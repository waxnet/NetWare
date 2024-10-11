using NetWare.Configuration;
using NetWare.Extensions;
using NetWare.Modules.Tabs.Enums;
using NetWare.UI;

namespace NetWare.Modules.Tabs.RageTab;

public static class MagicBulletSection
{
    public static void Draw()
    {
        Menu.NewSection("Magic Bullet");
        DrawToggle();
        DrawTargeting();
        DrawFovSettings();
    }

    private static void DrawToggle()
    {
        var (Value, KeyBind) = Menu.NewToggle(
                "Enabled",
                Config.Active.MagicBullet.Enabled,
                Config.Active.MagicBullet.KeyBind
            );
        Config.Active.MagicBullet.Enabled = Value;
        Config.Active.MagicBullet.KeyBind = KeyBind;
    }
    private static void DrawTargeting()
    {
        Menu.NewTitle("Targeting");

        // aim filter
        var aimFilter = Menu.NewDropdown("Filter By", "MagicBulletAimFilter", Config.Active.MagicBullet.Filter.ConvertToString(), Aim.AimFilters.Keys);
        Config.Active.MagicBullet.Filter = Aim.AimFilters[aimFilter];

        // max distance
        var maxDistance = Menu.NewSlider("Max Distance", Config.Active.MagicBullet.Distance.Maximum, Config.Active.MagicBullet.Distance.Minimum, 1000);
        Config.Active.MagicBullet.Distance.Maximum = maxDistance;

        // min distance
        var minDistance = Menu.NewSlider("Min Distance", Config.Active.MagicBullet.Distance.Minimum, 0, Config.Active.MagicBullet.Distance.Maximum);
        Config.Active.MagicBullet.Distance.Minimum = minDistance;

        // frame skips
        var frameSkips = Menu.NewSlider("Frame Skips", Config.Active.MagicBullet.FrameSkips, 0, 10);
        Config.Active.MagicBullet.FrameSkips = frameSkips;
    }
    private static void DrawFovSettings()
    {
        Menu.NewTitle("FOV Settings");

        // check fov
        var checkFov = Menu.NewToggle("Check FOV", Config.Active.MagicBullet.CheckFov);
        Config.Active.MagicBullet.CheckFov = checkFov;

        // draw fov
        var drawFov = Menu.NewToggle("Draw FOV", Config.Active.MagicBullet.DrawFov);
        Config.Active.MagicBullet.DrawFov = drawFov;

        // dynamic fov
        var dynamicFov = Menu.NewToggle("Dynamic FOV", Config.Active.MagicBullet.DynamicFov);
        Config.Active.MagicBullet.DynamicFov = dynamicFov;

        // fov size
        var fovSize = Menu.NewSlider("FOV Size", Config.Active.MagicBullet.FovSize, 10, 800);
        Config.Active.MagicBullet.FovSize = (int)fovSize;

        // fov sides
        var fovSides = Menu.NewSlider("FOV Sides", Config.Active.MagicBullet.FovSides, 3, 80);
        Config.Active.MagicBullet.FovSides = (int)fovSides;

        // fov color
        var fovColor = Menu.NewTextField("FOV Color", Config.Active.MagicBullet.FovColor);
        Config.Active.MagicBullet.FovColor = fovColor;

        // rainbow fov
        var rainbowFov = Menu.NewToggle("Rainbow FOV", Config.Active.MagicBullet.RainbowFov);
        Config.Active.MagicBullet.RainbowFov = rainbowFov;
    }
}
