using NetWare.Attributes;
using NetWare.Enums;
using UnityEngine;

namespace NetWare.Configuration.Subtypes.Settings;

public sealed class WatermarkConfig : IBindable
{
    [ConfigProperty] public bool Enabled { get; set; } = true;
    [ConfigProperty] public TimeType TimeType { get; set; } = TimeType.Standard;

    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;
}
