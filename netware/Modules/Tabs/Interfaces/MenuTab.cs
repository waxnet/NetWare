using NetWare.Attributes;

using System.Reflection;

namespace NetWare.Modules.MenuTabs;

public abstract class MenuTab : IMenuTab
{
    private readonly string _tabName;

    protected MenuTab()
    {
        _tabName = GetType()
            .GetCustomAttribute<Attributes.MenuTabAttribute>()
            ?.TabName;
    }

    public virtual string TabName => _tabName;
    public abstract void Tab();
}
