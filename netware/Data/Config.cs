using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetWare
{
    public static class Config
    {
        private static readonly string configFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/NetWare/configs";
        public static List<string> configList = new List<string>();

        private static Dictionary<string, string> config = new Dictionary<string, string>()
        {
            // combat
            ["combat.aimbot.enabled"] = "false",
            ["combat.aimbot.aimbone"] = "Head",
            ["combat.aimbot.aimmode"] = "Camera",
            ["combat.aimbot.checkfov"] = "true",
            ["combat.aimbot.dynamicfov"] = "false",
            ["combat.aimbot.drawfov"] = "true",
            ["combat.aimbot.fovsize"] = "200",
            ["combat.aimbot.smoothing"] = "5",
            ["combat.aimbot.fovcolor"] = "#FF4C4C",

            ["combat.silentaim.enabled"] = "false",
            ["combat.silentaim.aimbone"] = "Head",
            ["combat.silentaim.checkfov"] = "true",
            ["combat.silentaim.dynamicfov"] = "false",
            ["combat.silentaim.drawfov"] = "true",
            ["combat.silentaim.fovsize"] = "200",
            ["combat.silentaim.fovcolor"] = "#4C4CFF",

            ["combat.weapons.norecoil"] = "false",
            ["combat.weapons.nospread"] = "false",
            ["combat.weapons.infiniteammo"] = "false",
            ["combat.weapons.rapidfire"] = "false",
            ["combat.weapons.infiniterange"] = "false",

            // visual
            ["visual.esp.tracers"] = "false",
            ["visual.esp.skeleton"] = "false",
            ["visual.esp.3dboxes"] = "false",
            ["visual.esp.2dboxes"] = "false",
            ["visual.esp.filledboxes"] = "false",
            ["visual.esp.info"] = "false",
            ["visual.esp.nametags"] = "false",
            ["visual.esp.backgroundcolor"] = "#000000",
            ["visual.esp.teammatecolor"] = "#00FF00",
            ["visual.esp.enemycolor"] = "#FF0000",
            ["visual.esp.botcolor"] = "#FFFFFF",

            ["visual.fovchanger.enabled"] = "false",
            ["visual.fovchanger.fovchangeramount"] = "100",

            // movement
            ["movement.speed.enabled"] = "false",
            ["movement.speed.amount"] = "5",

            ["movement.fly.enabled"] = "false",

            ["movement.bhop.enabled"] = "false",

            // exploits
            ["exploits.player.godmode"] = "false",
            ["exploits.player.instantland"] = "false",
            ["exploits.player.infinitematerials"] = "false",
            ["exploits.player.antifreeze"] = "false",

            ["exploits.gameplay.autoplay"] = "false",

            ["exploits.game.buildingspam"] = "false",
            ["exploits.game.rigspam"] = "false",
            ["exploits.game.instantbreak"] = "false",

            ["exploits.fun.levelchanger"] = "false",
            ["exploits.fun.levelchangeramount"] = "100000",

            ["exploits.fun.buildinggun"] = "false",
            ["exploits.fun.teleportgun"] = "false",
            ["exploits.fun.explosiongun"] = "false",

            // settings
            ["settings.interface.watermark"] = "true",
            ["settings.interface.timetype"] = "Standard",
        };

        // setup
        public static void Setup()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            foreach (string file in Directory.GetFiles(configFolder))
            {
                string fileName = file.Split(new string[] { "\\" }, StringSplitOptions.None).Last().Replace(".nwc", "");

                configList.Add(fileName);
            }
        }

        // load, save and delete
        public static void Load(string configName)
        {
            string configPath = Path.Combine(configFolder, configName + ".nwc");

            if (File.Exists(configPath))
            {
                string[] packedContent = File.ReadAllText(configPath).Split(new string[] { "\n" }, StringSplitOptions.None);

                foreach (string packedData in packedContent)
                {
                    string[] unpackedData = packedData.Split(new string[] { " " }, StringSplitOptions.None);

                    if (unpackedData.Length == 2)
                    {
                        config[unpackedData[0]] = unpackedData[1];
                    }
                }
            }
        }
        public static void Save(string configName)
        {
            string configPath = Path.Combine(configFolder, configName + ".nwc");
            string packedConfig = "";

            foreach (KeyValuePair<string, string> data in config)
            {
                packedConfig += (data.Key + " " + data.Value + "\n");
            }

            File.WriteAllText(configPath, packedConfig);

            if (!configList.Contains(configName))
            {
                configList.Add(configName);
            }
        }
        public static void Delete(string configName)
        {
            string configPath = Path.Combine(configFolder, configName + ".nwc");

            File.Delete(configPath);

            if (configList.Contains(configName))
            {
                configList.Remove(configName);
            }
        }

        // booleans
        public static bool GetBool(string key)
        {
            return bool.Parse(config[key]);
        }
        public static void SetBool(string key, bool value)
        {
            config[key] = value.ToString();
        }

        // floats
        public static float GetFloat(string key)
        {
            return (float)Math.Round(float.Parse(config[key]), 1);
        }
        public static void SetFloat(string key, float value)
        {
            config[key] = value.ToString();
        }

        // integers
        public static int GetInt(string key)
        {
            return int.Parse(config[key]);
        }
        public static void SetInt(string key, int value)
        {
            config[key] = value.ToString();
        }

        // strings
        public static string GetString(string key)
        {
            return config[key];
        }
        public static void SetString(string key, string value)
        {
            config[key] = value;
        }
    }
}
