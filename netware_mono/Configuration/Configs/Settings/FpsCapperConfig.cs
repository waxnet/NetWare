using NetWare.Attributes;

using UnityEngine;

namespace NetWare.Configuration.Configs.Settings;

public sealed class FpsCapperConfig
{
    public string FpsRaw { get; set; } = Application.targetFrameRate.ToString();

    [ConfigProperty] public int Fps { get; set; } = Application.targetFrameRate;
}
