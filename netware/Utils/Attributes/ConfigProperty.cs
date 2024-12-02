using System;

namespace NetWare.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ConfigProperty(string name = null) : Attribute
{
    public string Name { get; } = name;
}