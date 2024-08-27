using NetWare.Attributes;
using UnityEngine;

namespace NetWare.Configuration.Subtypes.Settings;

public sealed class FpsCapperConfig
{
    public string FpsRaw { get; set; } = Application.targetFrameRate.ToString();

    [ConfigProperty] public int Fps { get; set; } = Application.targetFrameRate;
}
