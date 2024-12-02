using NetWare.Attributes;
using UnityEngine;

namespace NetWare.Configuration.Configs.Rage;

public sealed class FlyConfig : IBindable
{
    // toggle
    [ConfigProperty] public bool Enabled { get; set; } = false;
    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;

    // speed
    [ConfigProperty] public float HorizontalSpeed { get; set; } = 25;
    [ConfigProperty] public float VerticalSpeed { get; set; } = 25;

    // spin
    [ConfigProperty] public bool Spin { get; set; } = false;
}
