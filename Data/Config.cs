using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetWare
{
    public class Config
    {
        private static readonly string configFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/NetWare/configs";
        public static List<string> configList = new List<string>();
        
        private static Dictionary<string, string> config = new Dictionary<string, string>()
        {
            // combat
            ["combat.softaim.enabled"] = "false",
            ["combat.softaim.checkfov"] = "true",
            ["combat.softaim.dynamicfov"] = "false",
            ["combat.softaim.drawfov"] = "true",
            ["combat.softaim.fovsize"] = "200",
            ["combat.softaim.smoothing"] = "5",

            ["combat.silentaim.enabled"] = "false",
            ["combat.silentaim.checkfov"] = "true",
            ["combat.silentaim.dynamicfov"] = "false",
            ["combat.silentaim.drawfov"] = "true",
            ["combat.silentaim.fovsize"] = "200",

            ["combat.weapons.norecoil"] = "false",
            ["combat.weapons.infiniteammo"] = "false",
            ["combat.weapons.rapidfire"] = "false",

            // visual
            ["visual.esp.tracers"] = "false",
            ["visual.esp.skeleton"] = "false",
            ["visual.esp.boxes"] = "false",
            ["visual.esp.info"] = "false",
            ["visual.esp.nametags"] = "false",

            ["visual.camera.customfov"] = "false",
            ["visual.camera.customfovamount"] = "100",

            // movement
            ["movement.speed.speed"] = "false",
            ["movement.speed.speedamount"] = "5",

            ["movement.fly.fly"] = "false",
            ["movement.fly.helicopter"] = "false",

            // exploits
            ["exploits.player.godmode"] = "false",
            ["exploits.player.instantland"] = "false",
            ["exploits.player.infinitematerials"] = "false",

            ["exploits.other.autoplay"] = "false",

            ["exploits.world.buildingspam"] = "false",
            ["exploits.world.rigspam"] = "false",
            ["exploits.world.instantbreak"] = "false",
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

        // integers
        public static int GetInt(string key)
        {
            return int.Parse(config[key]);
        }

        public static void SetInt(string key, int value)
        {
            config[key] = value.ToString();
        }
    }
}
