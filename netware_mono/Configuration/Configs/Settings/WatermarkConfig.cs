using NetWare.Attributes;
using NetWare.Enums;

namespace NetWare.Configuration.Configs.Settings;

public sealed class WatermarkConfig
{
    [ConfigProperty] public bool Enabled { get; set; } = true;

    [ConfigProperty] public ETimeType TimeType { get; set; } = ETimeType.Standard;
}
