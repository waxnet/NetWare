using NetWare.Attributes;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.LegitTab;
using NetWare.UI;

namespace NetWare.Modules.Tabs;

[MenuTab("Legit")]
public sealed class Legit : MenuTab
{
    public Legit() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();
        AimbotSection.Draw();

        Menu.Separate();
        SilentAimSection.Draw();

        Menu.End();
    }
}
