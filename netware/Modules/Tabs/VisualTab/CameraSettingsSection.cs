using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.VisualTab;

public static class CameraSettingsSection
{
    public static void Draw()
    {
        Menu.NewSection("Camera Settings");
        DrawToggle();
        DrawOffsets();
    }

    private static void DrawToggle()
    {
        // enabled
        var enabled = Menu.NewToggle("Enabled",  Config.Active.CameraSettings.Enabled);
        Config.Active.CameraSettings.Enabled = enabled;
    }
    private static void DrawOffsets()
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
    }
}
