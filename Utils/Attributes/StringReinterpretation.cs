using System;

namespace NetWare.Attributes;

[AttributeUsage(AttributeTargets.All)]
public sealed class StringReinterpretation(string value) : Attribute
{
    public string Value { get; set; } = value;
}
