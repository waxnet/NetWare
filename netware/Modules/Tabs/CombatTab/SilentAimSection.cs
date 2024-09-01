using NetWare.Configuration;
using NetWare.Extensions;
using NetWare.UI;

namespace NetWare.Modules.Tabs.CombatTab;

public static class SilentAimSection
{
    public static void Draw()
    {
        Menu.NewSection("Silent Aim");
        DrawToggle();
        DrawTargeting();
        DrawFovSettings();
    }

    private static void DrawToggle()
    {
        var (Value, KeyBind) = Menu.NewToggle(
                "Enabled",
                Config.Active.SilentAim.Enabled,
                Config.Active.SilentAim.KeyBind
            );
        Config.Active.SilentAim.Enabled = Value;
        Config.Active.SilentAim.KeyBind = KeyBind;
    }
    private static void DrawTargeting()
    {
        Menu.NewTitle("Targeting");

        // aim bone
        var aimBone = Menu.NewDropdown("Aim Bone", "SilentAimAimBone", Config.Active.SilentAim.Bone.ConvertToString(), Combat.AimBones.Keys);
        Config.Active.SilentAim.Bone = Combat.AimBones[aimBone];

        // aim filter
        var aimFilter = Menu.NewDropdown("Filter By", "SilentAimAimFilter", Config.Active.SilentAim.Filter.ConvertToString(), Combat.AimFilters.Keys);
        Config.Active.SilentAim.Filter = Combat.AimFilters[aimFilter];

        // max distance
        var maxDistance = Menu.NewSlider("Max Distance", Config.Active.SilentAim.Distance.Maximum, Config.Active.SilentAim.Distance.Minimum, 1000);
        Config.Active.SilentAim.Distance.Maximum = maxDistance;

        // min distance
        var minDistance = Menu.NewSlider("Min Distance", Config.Active.SilentAim.Distance.Minimum, 0, Config.Active.SilentAim.Distance.Maximum);
        Config.Active.SilentAim.Distance.Minimum = minDistance;
    }
    private static void DrawFovSettings()
    {
        Menu.NewTitle("FOV Settings");

        // check fov
        var checkFov = Menu.NewToggle("Check FOV", Config.Active.SilentAim.CheckFov);
        Config.Active.SilentAim.CheckFov = checkFov;

        // draw fov
        var drawFov = Menu.NewToggle("Draw FOV", Config.Active.SilentAim.DrawFov);
        Config.Active.SilentAim.DrawFov = drawFov;

        // dynamic fov
        var dynamicFov = Menu.NewToggle("Dynamic FOV", Config.Active.SilentAim.DynamicFov);
        Config.Active.SilentAim.DynamicFov = dynamicFov;

        // fov size
        var fovSize = Menu.NewSlider("FOV Size", Config.Active.SilentAim.FovSize, 10, 800);
        Config.Active.SilentAim.FovSize = (int)fovSize;

        // fov sides
        var fovSides = Menu.NewSlider("FOV Sides", Config.Active.SilentAim.FovSides, 3, 80);
        Config.Active.SilentAim.FovSides = (int)fovSides;

        // fov color
        var fovColor = Menu.NewTextField("FOV Color", Config.Active.SilentAim.FovColor);
        Config.Active.SilentAim.FovColor = fovColor;

        // rainbow fov
        var rainbowFov = Menu.NewToggle("Rainbow FOV", Config.Active.SilentAim.RainbowFov);
        Config.Active.SilentAim.RainbowFov = rainbowFov;
    }
}
