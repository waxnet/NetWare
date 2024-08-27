using NetWare.Attributes;
using UnityEngine;

namespace NetWare.Configuration.Subtypes.Visual;

public sealed class FovChangerConfig : IBindable
{
    [ConfigProperty] public bool Enabled { get; set; } = false;

    [ConfigProperty] public float Amount { get; set; } = 100;

    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;

}
