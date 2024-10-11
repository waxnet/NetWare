using NetWare.Attributes;

namespace NetWare.Enums;

public enum EAimMode : byte
{
    [StringReinterpretation("Legit")]
    Legit,

    [StringReinterpretation("Lock")]
    Lock
}
