using System;

namespace NetWare.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class MenuTabAttribute : NetWareComponentAttribute
{
    public MenuTabAttribute(string tabName, int order = 0)
    {
        TabName = tabName;
        Order = order;
    }

    public string TabName { get; }
    public int Order { get; }
}
