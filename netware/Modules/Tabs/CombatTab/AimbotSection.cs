using NetWare.Configuration;
using NetWare.Extensions;
using NetWare.UI;

namespace NetWare.Modules.Tabs.CombatTab;

public static class AimbotSection
{
    public static void Draw()
    {
        Menu.NewSection("Aimbot");
        DrawToggle();
        DrawTargeting();
        DrawSmoothing();
        DrawFovSettings();
    }

    private static void DrawToggle()
    {
        // enabled
        var (Value, KeyBind) = Menu.NewToggle(
                "Enabled",
                Config.Active.Aimbot.Enabled,
                Config.Active.Aimbot.KeyBind
            );
        Config.Active.Aimbot.Enabled = Value;
        Config.Active.Aimbot.KeyBind = KeyBind;
    }
    private static void DrawTargeting()
    {
        Menu.NewTitle("Targeting");

        // aim bone
        var aimBone = Menu.NewDropdown("Aim Bone", "AimBotAimBone", Config.Active.Aimbot.Bone.ConvertToString(), Combat.AimBones.Keys);
        Config.Active.Aimbot.Bone = Combat.AimBones[aimBone];

        // aim mode
        var aimMode = Menu.NewDropdown("Aim Mode", "AimBotAimMode", Config.Active.Aimbot.Mode.ConvertToString(), Combat.AimModes.Keys);
        Config.Active.Aimbot.Mode = Combat.AimModes[aimMode];

        // aim filter
        var aimFilter = Menu.NewDropdown("Filter By", "AimBotAimFilter", Config.Active.Aimbot.Filter.ConvertToString(), Combat.AimFilters.Keys);
        Config.Active.Aimbot.Filter = Combat.AimFilters[aimFilter];

        // max distance
        var maxDistance = Menu.NewSlider("Max Distance", Config.Active.Aimbot.Distance.Maximum, Config.Active.Aimbot.Distance.Minimum, 1000);
        Config.Active.Aimbot.Distance.Maximum = maxDistance;

        // min distance
        var minDistance = Menu.NewSlider("Min Distance", Config.Active.Aimbot.Distance.Minimum, 0, Config.Active.Aimbot.Distance.Maximum);
        Config.Active.Aimbot.Distance.Minimum = minDistance;
    }
    private static void DrawSmoothing()
    {
        Menu.NewTitle("Smoothing");

        var smoothing = Menu.NewSlider("Smoothing", Config.Active.Aimbot.Smoothing, 2, 10);
        Config.Active.Aimbot.Smoothing = smoothing;

        // use sensitivity
        var useSensitivity = Menu.NewToggle("Use Sensitivity", Config.Active.Aimbot.UseSensitivity);
        Config.Active.Aimbot.UseSensitivity = useSensitivity;
    }
    private static void DrawFovSettings()
    {
        Menu.NewTitle("FOV Settings");

        // check fov
        var checkFov = Menu.NewToggle("Check FOV", Config.Active.Aimbot.CheckFov);
        Config.Active.Aimbot.CheckFov = checkFov;

        // draw fov
        var drawFov = Menu.NewToggle("Draw FOV", Config.Active.Aimbot.DrawFov);
        Config.Active.Aimbot.DrawFov = drawFov;

        // dynamic fov
        var dynamicFov = Menu.NewToggle("Dynamic FOV", Config.Active.Aimbot.DynamicFov);
        Config.Active.Aimbot.DynamicFov = dynamicFov;

        // fov size
        var fovSize = Menu.NewSlider("FOV Size", Config.Active.Aimbot.FovSize, 10, 800);
        Config.Active.Aimbot.FovSize = (int)fovSize;

        // fov sides
        var fovSides = Menu.NewSlider("FOV Sides", Config.Active.Aimbot.FovSides, 3, 80);
        Config.Active.Aimbot.FovSides = (int)fovSides;

        // fov color
        var fovColor = Menu.NewTextField("FOV Color", Config.Active.Aimbot.FovColor);
        Config.Active.Aimbot.FovColor = fovColor;

        // rainbow fov
        var rainbowFov = Menu.NewToggle("Rainbow FOV", Config.Active.Aimbot.RainbowFov);
        Config.Active.Aimbot.RainbowFov = rainbowFov;
    }
}
