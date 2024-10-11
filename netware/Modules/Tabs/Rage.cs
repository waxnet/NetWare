using NetWare.Attributes;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.RageTab;
using NetWare.UI;

namespace NetWare.Modules.Tabs;

[MenuTab("Rage", 1)]
public sealed class Rage : MenuTab
{
    public Rage() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();

        MagicBulletSection.Draw();

        Menu.End();
    }
}
