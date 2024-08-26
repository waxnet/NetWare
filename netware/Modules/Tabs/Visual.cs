using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Modules.MenuTabs;
using NetWare.UI;

namespace NetWare.Modules.Tabs;

[MenuTab("Visual", 1)]
public sealed class Visual : MenuTab
{
    public Visual() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();
        DrawNameTags();
        DrawSkeleton();
        DrawCameraSettings();

        Menu.Separate();
        DrawTracers();
        DrawBoxes();
        DrawFovValues();

        Menu.End();
    }

    #region Name Tags
    private static void DrawNameTags()
    {
        Menu.NewSection("Nametags");

        DrawNameTagsToggle();
        DrawNameTagsColors();
    }
    private static void DrawNameTagsToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.NameTags.Enabled,
            Config.Active.NameTags.KeyBind
            );

        Config.Active.NameTags.Enabled = enabled.Value;
        Config.Active.NameTags.KeyBind = enabled.KeyBind;
    }
    private static void DrawNameTagsColors()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.NameTags.TeamColor);
        Config.Active.NameTags.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.NameTags.EnemyColor);
        Config.Active.NameTags.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.NameTags.BotColor);
        Config.Active.NameTags.BotColor = botColor;

        // background color
        var bgColor = Menu.NewTextField("Background Color", Config.Active.NameTags.BackgroundColor);
        Config.Active.NameTags.BackgroundColor = bgColor;
    }
    #endregion

    #region Skeleton
    private static void DrawSkeleton()
    {
        Menu.NewSection("Skeleton");

        DrawSkeletonToggle();
        DrawSkeletonColors();
    }
    private static void DrawSkeletonToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.Skeleton.Enabled,
            Config.Active.Skeleton.KeyBind
            );

        Config.Active.Skeleton.Enabled = enabled.Value;
        Config.Active.Skeleton.KeyBind = enabled.KeyBind;
    }
    private static void DrawSkeletonColors()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.Skeleton.TeamColor);
        Config.Active.Skeleton.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.Skeleton.EnemyColor);
        Config.Active.Skeleton.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.Skeleton.BotColor);
        Config.Active.Skeleton.BotColor = botColor;
    }
    #endregion

    #region Camera Settings
    private static void DrawCameraSettings()
    {
        Menu.NewSection("Camera Settings");

        DrawCameraSettingsToggle();
        DrawCameraSettingsOffsets();
    }
    private static void DrawCameraSettingsToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.CameraSettings.Enabled,
            Config.Active.CameraSettings.KeyBind
            );

        Config.Active.CameraSettings.Enabled = enabled.Value;
        Config.Active.CameraSettings.KeyBind = enabled.KeyBind;
    }
    private static void DrawCameraSettingsOffsets()
    {
        // x offset
        var xOffset = Menu.NewSlider("X Offset", Config.Active.CameraSettings.OffsetX, -10, 10);
        Config.Active.CameraSettings.OffsetX = xOffset;

        // y offset
        var yOffset = Menu.NewSlider("Y Offset", Config.Active.CameraSettings.OffsetY, -10, 10);
        Config.Active.CameraSettings.OffsetY = yOffset;

        // z offset
        var zOffset = Menu.NewSlider("Z Offset", Config.Active.CameraSettings.OffsetZ, -10, 10);
        Config.Active.CameraSettings.OffsetZ = zOffset;

        // reset
        Menu.NewButton("Reset", () => Data.ResetZoom = true);
    }
    #endregion

    #region Tracers
    private static void DrawTracers()
    {
        Menu.NewSection("Tracers");

        DrawTracersToggle();
        DrawTracersColor();
    }
    private static void DrawTracersToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.Tracers.Enabled,
            Config.Active.Tracers.KeyBind
            );

        Config.Active.Tracers.Enabled = enabled.Value;
        Config.Active.Tracers.KeyBind = enabled.KeyBind;
    }
    private static void DrawTracersColor()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.Tracers.TeamColor);
        Config.Active.Tracers.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.Tracers.EnemyColor);
        Config.Active.Tracers.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.Tracers.BotColor);
        Config.Active.Tracers.BotColor = botColor;
    }
    #endregion

    #region Boxes
    private static void DrawBoxes()
    {
        Menu.NewSection("Boxes");

        DrawBoxesToggle();
        DrawBoxesColor();
    }
    private static void DrawBoxesToggle()
    {
        // enabled
        var enabled = Menu.NewToggle(
            "Enabled",
            Config.Active.Boxes.Enabled,
            Config.Active.Boxes.KeyBind
            );

        Config.Active.Boxes.Enabled = enabled.Value;
        Config.Active.Boxes.KeyBind = enabled.KeyBind;
    }
    private static void DrawBoxesColor()
    {
        Menu.NewTitle("Colors");

        // team color
        var teamColor = Menu.NewTextField("Team Color", Config.Active.Boxes.TeamColor);
        Config.Active.Boxes.TeamColor = teamColor;

        // enemy color
        var enemyColor = Menu.NewTextField("Enemy Color", Config.Active.Boxes.EnemyColor);
        Config.Active.Boxes.EnemyColor = enemyColor;

        // bot color
        var botColor = Menu.NewTextField("Bot Color", Config.Active.Boxes.BotColor);
        Config.Active.Boxes.BotColor = botColor;
    }
    #endregion

    #region Fov Changer
    private static void DrawFovChanger()
    {
        Menu.NewSection("FOV Changer");

        DrawFovChangerToggle();
    }
    private static void DrawFovChangerToggle()
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
    #endregion
}
