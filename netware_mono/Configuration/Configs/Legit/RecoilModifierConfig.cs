using NetWare.Attributes;

namespace NetWare.Configuration.Configs.Legit;

public sealed class RecoilModifierConfig
{
    // toggle
    [ConfigProperty] public bool Enabled { get; set; } = false;

    // settings
    [ConfigProperty] public float Multiplier { get; set; } = .5f;
}
