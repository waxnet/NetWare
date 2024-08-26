using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Enums;
using NetWare.Extensions;
using NetWare.Modules.MenuTabs;
using NetWare.UI;
using System.Collections.Generic;
using System.Linq;

namespace NetWare.Modules.Tabs;

[MenuTab("Combat")]
public sealed class Combat : MenuTab
{
    private static readonly Dictionary<string, AimBone> _aimBones = EnumExtensions.GetValues<AimBone>()
        .ToDictionary(x => x.ConvertToString());

    private static readonly Dictionary<string, AimMode> _aimModes = EnumExtensions.GetValues<AimMode>()
        .ToDictionary(x => x.ConvertToString());

    private static readonly Dictionary<string, AimFilter> _aimFilters = EnumExtensions.GetValues<AimFilter>()
        .ToDictionary(x => x.ConvertToString());

    public Combat() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();
        DrawAimbot();

        Menu.Separate();
        DrawSilentAim();

        Menu.End();
    }

    #region Aimbot
    private static void DrawAimbot()
    {
        Menu.NewSection("Aimbot");

        DrawAimbotToggle();
        DrawAimbotTargeting();
        DrawAimbotSmoothing();
        DrawAimbotFovSettings();
    }
    private static void DrawAimbotToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
                "Enabled",
                Config.Active.Aimbot.Enabled,
                Config.Active.Aimbot.KeyBind
            );

        Config.Active.Aimbot.Enabled = enabled.Value;
        Config.Active.Aimbot.KeyBind = enabled.KeyBind;
    }
    private static void DrawAimbotTargeting()
    {
        Menu.NewTitle("Targeting");

        // aim bone
        var aimBone = Menu.NewDropdown("Aim Bone", "AimBotAimBone", Config.Active.Aimbot.Bone.ConvertToString(), _aimBones.Keys);
        Config.Active.Aimbot.Bone = _aimBones[aimBone];

        // aim mode
        var aimMode = Menu.NewDropdown("Aim Mode", "AimBotAimMode", Config.Active.Aimbot.Mode.ConvertToString(), _aimModes.Keys);
        Config.Active.Aimbot.Mode = _aimModes[aimMode];

        // aim filter
        var aimFilter = Menu.NewDropdown("Filter By", "AimBotAimFilter", Config.Active.Aimbot.Filter.ConvertToString(), _aimFilters.Keys);
        Config.Active.Aimbot.Filter = _aimFilters[aimFilter];

        // max distance
        var maxDistance = Menu.NewSlider("Max Distance", Config.Active.Aimbot.Distance.Maximum, Config.Active.Aimbot.Distance.Minimum, 1000);
        Config.Active.Aimbot.Distance.Maximum = maxDistance;

        // min distance
        var minDistance = Menu.NewSlider("Min Distance", Config.Active.Aimbot.Distance.Minimum, 0, Config.Active.Aimbot.Distance.Maximum);
        Config.Active.Aimbot.Distance.Minimum = minDistance;
    }
    private static void DrawAimbotSmoothing()
    {
        Menu.NewTitle("Smoothing");

        var smoothing = Menu.NewSlider("Smoothing", Config.Active.Aimbot.Smoothing, 0.1f, 10);
        Config.Active.Aimbot.Smoothing = smoothing;

        // use sensitivity
        var useSensitivity = Menu.NewToggle("Use Sensitivity", Config.Active.Aimbot.UseSensitivity);
        Config.Active.Aimbot.UseSensitivity = useSensitivity;
    }
    private static void DrawAimbotFovSettings()
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
    #endregion

    #region Silent Aim
    private static void DrawSilentAim()
    {
        Menu.NewSection("Silent Aim");

        DrawSilentAimToggle();
        DrawSilentAimTargeting();
        DrawSilentAimFovSettings();
    }
    private static void DrawSilentAimToggle()
    {
        var enabled = Menu.NewToggle(
                "Enabled",
                Config.Active.SilentAim.Enabled,
                Config.Active.SilentAim.KeyBind
            );

        Config.Active.SilentAim.Enabled = enabled.Value;
        Config.Active.SilentAim.KeyBind = enabled.KeyBind;
    }
    private static void DrawSilentAimTargeting()
    {
        Menu.NewTitle("Targeting");

        // aim bone
        var aimBone = Menu.NewDropdown("Aim Bone", "AimBotAimBone", Config.Active.SilentAim.Bone.ConvertToString(), _aimBones.Keys);
        Config.Active.SilentAim.Bone = _aimBones[aimBone];

        // aim filter
        var aimFilter = Menu.NewDropdown("Filter By", "AimBotAimFilter", Config.Active.SilentAim.Filter.ConvertToString(), _aimFilters.Keys);
        Config.Active.SilentAim.Filter = _aimFilters[aimFilter];

        // max distance
        var maxDistance = Menu.NewSlider("Max Distance", Config.Active.SilentAim.Distance.Maximum, Config.Active.SilentAim.Distance.Minimum, 1000);
        Config.Active.SilentAim.Distance.Maximum = maxDistance;

        // min distance
        var minDistance = Menu.NewSlider("Min Distance", Config.Active.SilentAim.Distance.Minimum, 0, Config.Active.SilentAim.Distance.Maximum);
        Config.Active.SilentAim.Distance.Minimum = minDistance;
    }
    private static void DrawSilentAimFovSettings()
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
    #endregion
}
