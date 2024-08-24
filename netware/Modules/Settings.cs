using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public static class Settings
    {
        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Config Manager");
            Data.configName = Menu.NewTextField("Config Name", Data.configName);
            Menu.NewButton("Save", () => Config.Save(Data.configName));
            Menu.NewButton("Delete", () => Config.Delete(Data.configName));

            Menu.NewSection("Watermark");
            Config.SetBool(
                "settings.watermark.enabled",
                Menu.NewToggle(
                    Config.GetBool("settings.watermark.enabled"),
                    "Enabled"
                )
            );
            Config.SetString(
                "settings.watermark.timetype",
                Menu.NewDropdown(
                    "Time Type",
                    Config.GetString("settings.watermark.timetype"),
                    new string[] { "Standard", "Military" }
                )
            );

            Menu.NewSection("Gameplay");
            Config.SetString(
                "settings.fpscapper.fps",
                Menu.NewTextField(
                    "FPS Cap",
                    Config.GetString("settings.fpscapper.fps")
                )
            );
            Menu.NewButton("Leave Game", () => PhotonNetwork.Disconnect());

            Menu.Separate();

            Menu.NewSection("Config Loader");
            foreach (string config in Config.configList)
                Menu.NewButton(config, () => Config.Load(config));

            Menu.End();
        }
    }
}
