using NetWare.Configuration;
using NetWare.UI;
using UnityEngine;

namespace NetWare.Modules.Tabs.SettingsTab;

public static class ConfigManagerSection
{
    public static void Draw()
    {
        Menu.NewSection("Config Manager");

        // config name
        Data.SelectedConfig = Menu.NewTextField("Config Name", Data.SelectedConfig);

        // buttons
        Menu.NewButton("Load", () =>
        {
            if (!string.IsNullOrEmpty(Data.SelectedConfig))
                Config.Load(Data.SelectedConfig);
        });

        Menu.NewButton("Save", () => Config.Save(Data.SelectedConfig));
        Menu.NewButton("Delete", () => Config.Delete(Data.SelectedConfig));

        GUILayout.Space(4);
        Menu.NewButton("Open Configs Folder", Config.OpenConfigFolder);
    }
}
