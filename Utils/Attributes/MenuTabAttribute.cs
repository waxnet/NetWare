using System;

namespace NetWare.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MenuTabAttribute(string tabName, int order = 0) : NetWareComponent
{
    public string TabName { get; } = tabName;
    public int Order { get; } = order;
}
