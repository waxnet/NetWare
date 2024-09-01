using NetWare.Attributes;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.SettingsTab;
using NetWare.UI;

namespace NetWare.Modules.Tabs;

[Attributes.MenuTabAttribute("Settings", 2)]
public sealed class Settings : MenuTabs.MenuTab
{
    public Settings() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();
        ConfigManagerSection.Draw();
        WatermarkSection.Draw();
        GameplaySection.Draw();

        Menu.Separate();
        ConfigSelectionSection.Draw();

        Menu.End();
    }
}
