using NetWare.Attributes;

namespace NetWare.Enums;

public enum ETimeType : byte
{
    [StringReinterpretation("Standard")]
    Standard,

    [StringReinterpretation("Military")]
    Military
}
