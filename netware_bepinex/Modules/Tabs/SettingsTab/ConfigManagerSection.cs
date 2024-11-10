using NetWare.Configuration;
using NetWare.UI;
using NetWare.Utils;

namespace NetWare.Modules.Tabs.SettingsTab;

public static class ConfigManagerSection
{
    public static void Draw()
    {
        Menu.NewSection("Config Manager");
        Data.SelectedConfig = Menu.NewTextField("Config Name", Data.SelectedConfig);
        Menu.NewButton("Load", () => Config.Load(Data.SelectedConfig));
        Menu.NewButton("Save", () => Config.Save(Data.SelectedConfig));
        Menu.NewButton("Delete", () => Config.Delete(Data.SelectedConfig));
        Menu.NewButton("Open Configs Folder", () => SystemUtils.OpenFolder(Config.ConfigFolder));
    }
}
