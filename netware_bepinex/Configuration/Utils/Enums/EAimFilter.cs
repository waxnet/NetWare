using NetWare.Attributes;

namespace NetWare.Enums;

public enum EAimFilter : byte
{
    [StringReinterpretation("FOV")]
    Fov,

    [StringReinterpretation("FOV & Closest")]
    FovAndClosest
}
