using NetWare.Attributes;
using NetWare.Enums;
using NetWare.Models;
using UnityEngine;

namespace NetWare.Configuration.Subtypes.Combat;

public sealed class SilentAimConfig : IBindable
{
    [ConfigProperty] public bool Enabled { get; set; } = false;
    [ConfigProperty] public bool CheckFov { get; set; } = true;
    [ConfigProperty] public bool DrawFov { get; set; } = true;
    [ConfigProperty] public bool DynamicFov { get; set; } = false;
    [ConfigProperty] public bool RainbowFov { get; set; } = false;

    [ConfigProperty] public int FovSize { get; set; } = 200;
    [ConfigProperty] public int FovSides { get; set; } = 50;

    [ConfigProperty] public string FovColor { get; set; } = "#4C4CFF";

    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;
    [ConfigProperty] public AimBone Bone { get; set; } = AimBone.Head;
    [ConfigProperty] public AimFilter Filter { get; set; } = AimFilter.Fov;

    [ConfigProperty] public Range<float> Distance { get; set; } = new(0, 500);
}
