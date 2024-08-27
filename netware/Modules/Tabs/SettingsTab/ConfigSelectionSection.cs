using NetWare.Configuration;
using NetWare.UI;

namespace NetWare.Modules.Tabs.SettingsTab;

public static class ConfigSelectionSection
{
    public static void Draw()
    {
        Menu.NewSection("Config Selection");

        foreach (string config in Config.ConfigNames)
            Menu.NewButton(config, () => Data.SelectedConfig = config);
    }
}
