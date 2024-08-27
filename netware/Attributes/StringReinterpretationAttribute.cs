using System;

namespace NetWare.Attributes;

[AttributeUsage(AttributeTargets.All)]
public sealed class StringReinterpretationAttribute : Attribute
{
    public StringReinterpretationAttribute(string value)
    {
        Value = value;
    }

    public string Value { get; set; }
}
