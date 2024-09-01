using NetWare.Attributes;

namespace NetWare.Configuration.Configs.Visual;

public sealed class CameraSettingsConfig
{
    [ConfigProperty] public bool Enabled { get; set; } = false;

    [ConfigProperty] public float OffsetX { get; set; } = 0.2f;
    [ConfigProperty] public float OffsetY { get; set; } = 1.5f;
    [ConfigProperty] public float OffsetZ { get; set; } = 2.5f;
}
