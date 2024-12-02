using NetWare.Attributes;
using UnityEngine;

namespace NetWare.Configuration.Configs.Rage;

public sealed class SpeedModifierConfig : IBindable
{
    // toggle
    [ConfigProperty] public bool Enabled { get; set; } = false;
    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;

    // settings
    [ConfigProperty] public float Multiplier { get; set; } = 15;
}
