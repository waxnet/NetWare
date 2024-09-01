using NetWare.Attributes;
using NetWare.Enums;
using NetWare.Models;

using UnityEngine;

namespace NetWare.Configuration.Configs.Combat;

public sealed class SilentAimConfig : IBindable
{
    // toggle
    [ConfigProperty] public bool Enabled { get; set; } = false;
    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;

    // targeting
    [ConfigProperty] public EAimBone Bone { get; set; } = EAimBone.Head;
    [ConfigProperty] public EAimFilter Filter { get; set; } = EAimFilter.Fov;
    [ConfigProperty] public Range<float> Distance { get; set; } = new(0, 500);

    // fov settings
    [ConfigProperty] public bool CheckFov { get; set; } = true;
    [ConfigProperty] public bool DrawFov { get; set; } = true;
    [ConfigProperty] public bool DynamicFov { get; set; } = false;
    [ConfigProperty] public int FovSize { get; set; } = 200;
    [ConfigProperty] public int FovSides { get; set; } = 50;
    [ConfigProperty] public string FovColor { get; set; } = "#4C4CFF";
    [ConfigProperty] public bool RainbowFov { get; set; } = false;

}
