﻿using NetWare.Attributes;

namespace NetWare.Configuration.Configs.Rage;

public sealed class FireRateModifierConfig
{
    // toggle
    [ConfigProperty] public bool Enabled { get; set; } = false;

    // settings
    [ConfigProperty] public float Multiplier { get; set; } = 25;
}
