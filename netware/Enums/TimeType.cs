using NetWare.Attributes;

namespace NetWare.Enums;

public enum TimeType : byte
{
    [StringReinterpretation("Standard")]
    Standard,

    [StringReinterpretation("Military")]
    Military
}
