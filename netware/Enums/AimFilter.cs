using NetWare.Attributes;

namespace NetWare.Enums;

public enum AimFilter : byte
{
    [StringReinterpretation("FOV")]
    Fov,

    [StringReinterpretation("FOV & Closest")]
    FovAndClosest
}
