using NetWare.Attributes;

namespace NetWare.Enums;

public enum AimMode : byte
{
    [StringReinterpretation("Legit")]
    Legit,

    [StringReinterpretation("Lock")]
    Lock
}
