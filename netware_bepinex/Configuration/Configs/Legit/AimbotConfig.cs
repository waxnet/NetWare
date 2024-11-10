using NetWare.Attributes;
using NetWare.Enums;
using NetWare.Models;

using UnityEngine;

namespace NetWare.Configuration.Configs.Legit;

public sealed class AimbotConfig : IBindable
{
    // toggle
    [ConfigProperty] public bool Enabled { get; set; } = false;
    [ConfigProperty] public KeyCode? KeyBind { get; set; } = KeyCode.None;

    // targeting
    [ConfigProperty] public EAimBone Bone { get; set; } = EAimBone.Head;
    [ConfigProperty] public EAimMode Mode { get; set; } = EAimMode.Legit;
    [ConfigProperty] public EAimFilter Filter { get; set; } = EAimFilter.Fov;
    [ConfigProperty] public Range<float> Distance { get; set; } = new(0, 500);

    // smoothing
    [ConfigProperty] public float Smoothing { get; set; } = 4;
    [ConfigProperty] public bool UseSensitivity { get; set; } = true;

    // fov settings
    [ConfigProperty] public bool CheckFov { get; set; } = true;
    [ConfigProperty] public bool DrawFov { get; set; } = true;
    [ConfigProperty] public bool DynamicFov { get; set; } = false;
    [ConfigProperty] public int FovSize { get; set; } = 200;
    [ConfigProperty] public int FovSides { get; set; } = 50;
    [ConfigProperty] public string FovColor { get; set; } = "#FF4C4C";
}
