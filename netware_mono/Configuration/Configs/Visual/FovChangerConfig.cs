using NetWare.Attributes;

namespace NetWare.Configuration.Configs.Visual;

public sealed class FovChangerConfig
{
    [ConfigProperty] public bool Enabled { get; set; } = false;

    [ConfigProperty] public float Amount { get; set; } = 100;

}
