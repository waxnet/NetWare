using NetWare.Attributes;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.MiscTab;
using NetWare.UI;

namespace NetWare.Modules.Tabs;

[MenuTab("Misc", 3)]
public sealed class Misc : MenuTab
{
    public Misc() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();

        LockerSection.Draw();

        Menu.End();
    }
}
