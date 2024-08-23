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

        public static Dictionary<string, string> config = new Dictionary<string, string>()
        {
            // combat
            ["combat.aimbot.enabled"] = "false",
            ["combat.aimbot.keybind"] = "None",
            ["combat.aimbot.aimbone"] = "Head",
            ["combat.aimbot.aimmode"] = "Legit",
            ["combat.aimbot.smoothing"] = "5",
            ["combat.aimbot.distance"] = "500",
            ["combat.aimbot.checkfov"] = "true",
            ["combat.aimbot.drawfov"] = "true",
            ["combat.aimbot.dynamicfov"] = "false",
            ["combat.aimbot.fovsize"] = "200",
            ["combat.aimbot.fovsides"] = "50",
            ["combat.aimbot.fovcolor"] = "#FF4C4C",
            ["combat.aimbot.rainbowfov"] = "false",

            ["combat.silentaim.enabled"] = "false",
            ["combat.silentaim.keybind"] = "None",
            ["combat.silentaim.aimbone"] = "Head",
            ["combat.silentaim.distance"] = "500",
            ["combat.silentaim.checkfov"] = "true",
            ["combat.silentaim.drawfov"] = "true",
            ["combat.silentaim.dynamicfov"] = "false",
            ["combat.silentaim.fovsize"] = "200",
            ["combat.silentaim.fovsides"] = "50",
            ["combat.silentaim.fovcolor"] = "#4C4CFF",
            ["combat.silentaim.rainbowfov"] = "false",

            // visual
            ["visual.nametags.enabled"] = "false",
            ["visual.nametags.team"] = "#00FF00",
            ["visual.nametags.enemy"] = "#FF0000",
            ["visual.nametags.bot"] = "#FFFFFF",
            ["visual.nametags.background"] = "#000000",

            ["visual.boxes.enabled"] = "false",
            ["visual.boxes.team"] = "#00FF00",
            ["visual.boxes.enemy"] = "#FF0000",
            ["visual.boxes.bot"] = "#FFFFFF",

            ["visual.skeleton.enabled"] = "false",
            ["visual.skeleton.team"] = "#00FF00",
            ["visual.skeleton.enemy"] = "#FF0000",
            ["visual.skeleton.bot"] = "#FFFFFF",

            ["visual.tracers.enabled"] = "false",
            ["visual.tracers.team"] = "#00FF00",
            ["visual.tracers.enemy"] = "#FF0000",
            ["visual.tracers.bot"] = "#FFFFFF",

            ["visual.fovchanger.enabled"] = "false",
            ["visual.fovchanger.amount"] = "100",

            ["visual.camerasettings.enabled"] = "false",
            ["visual.camerasettings.x"] = "0.2",
            ["visual.camerasettings.y"] = "1.5",
            ["visual.camerasettings.z"] = "2.5",

            // settings
            ["settings.watermark.enabled"] = "true",
            ["settings.watermark.timetype"] = "Standard",
        };

        // setup
        public static void Setup()
        {
            if (!Directory.Exists(configFolder))
                Directory.CreateDirectory(configFolder);

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

            if (!File.Exists(configPath))
                return;

            string[] packedContent = File.ReadAllText(configPath).Split(new string[] { "\n" }, StringSplitOptions.None);

            foreach (string packedData in packedContent)
            {
                string[] unpackedData = packedData.Split(new string[] { " " }, StringSplitOptions.None);

                if (unpackedData.Length == 2)
                    config[unpackedData[0]] = unpackedData[1];
            }
        }
        public static void Save(string configName)
        {
            string configPath = Path.Combine(configFolder, configName + ".nwc");
            string packedConfig = "";

            foreach (KeyValuePair<string, string> data in config)
                packedConfig += (data.Key + " " + data.Value + "\n");

            File.WriteAllText(configPath, packedConfig);

            if (!configList.Contains(configName))
                configList.Add(configName);
        }
        public static void Delete(string configName)
        {
            string configPath = Path.Combine(configFolder, configName + ".nwc");

            File.Delete(configPath);

            if (configList.Contains(configName))
                configList.Remove(configName);
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
        public static float GetFloat(string key, float safetyValue)
        {
            if (float.TryParse(config[key], out float value))
                return (float)Math.Round(value, 1);
            return safetyValue;
        }
        public static void SetFloat(string key, float value)
        {
            config[key] = value.ToString();
        }

        // integers
        public static int GetInt(string key)
        {
            if (int.TryParse(config[key], out int intValue))
                return intValue;

            if (double.TryParse(config[key].Replace(",", "."), out double doubleValue))
                return (int)Math.Round(doubleValue);

            return 10;
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
