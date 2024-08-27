using System;

namespace NetWare.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ConfigPropertyAttribute : Attribute
{
    public ConfigPropertyAttribute(string name = null)
    {
        Name = name;
    }

    public string Name { get; }
}