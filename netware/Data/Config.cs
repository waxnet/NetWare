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
            ["combat.aimbot.fovsides"] = "50",
            ["combat.aimbot.smoothing"] = "5",
            ["combat.aimbot.fovcolor"] = "#FF4C4C",
            ["combat.aimbot.rainbowfov"] = "false",

            ["combat.silentaim.enabled"] = "false",
            ["combat.silentaim.aimbone"] = "Head",
            ["combat.silentaim.checkfov"] = "true",
            ["combat.silentaim.dynamicfov"] = "false",
            ["combat.silentaim.drawfov"] = "true",
            ["combat.silentaim.fovsize"] = "200",
            ["combat.silentaim.fovsides"] = "50",
            ["combat.silentaim.fovcolor"] = "#4C4CFF",
            ["combat.silentaim.rainbowfov"] = "false",

            ["combat.magicbullet.enabled"] = "false",
            ["combat.magicbullet.frequency"] = "10",
            ["combat.magicbullet.checkfov"] = "true",
            ["combat.magicbullet.dynamicfov"] = "false",
            ["combat.magicbullet.drawfov"] = "true",
            ["combat.magicbullet.fovsize"] = "200",
            ["combat.magicbullet.fovsides"] = "50",
            ["combat.magicbullet.fovcolor"] = "#9900FF",
            ["combat.magicbullet.rainbowfov"] = "false",

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
            ["visual.fovchanger.amount"] = "100",

            ["visual.speedgraph.enabled"] = "false",
            ["visual.speedgraph.color"] = "#FFFFFF",
            ["visual.speedgraph.colormode"] = "Normal",

            ["visual.crosshair.enabled"] = "false",
            ["visual.crosshair.dynamic"] = "false",
            ["visual.crosshair.betterscope"] = "false",
            ["visual.crosshair.color"] = "#FFFFFF",
            ["visual.crosshair.rainbow"] = "false",

            ["visual.camerasettings.enabled"] = "false",
            ["visual.camerasettings.x"] = "0.2",
            ["visual.camerasettings.y"] = "1.5",
            ["visual.camerasettings.z"] = "2.5",

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
            ["exploits.fun.levelchanger.amount"] = "100000",

            ["exploits.fun.buildinggun"] = "false",
            ["exploits.fun.teleportgun"] = "false",
            ["exploits.fun.explosiongun"] = "false",

            // settings
            ["settings.interface.watermark"] = "true",
            ["settings.interface.watermark.timetype"] = "Standard",

            ["settings.interface.featurelist"] = "false",
            ["settings.interface.featurelist.color"] = "#FFFFFF",
            ["settings.interface.featurelist.colormode"] = "Normal",

            ["settings.interface.menueffects"] = "false",
            ["settings.interface.menueffects.speed"] = "0.2",
            ["settings.interface.menueffects.spawndelaymin"] = "0.1",
            ["settings.interface.menueffects.spawndelaymax"] = "0.1",
            ["settings.interface.menueffects.color"] = "#FFFFFF",
            ["settings.interface.menueffects.colormode"] = "Normal",
        };
        public static Dictionary<string, string> toggles = new Dictionary<string, string>() // there are probably better ways to do this but idc
        {
            // combat
            ["combat.aimbot.enabled"] = "Aimbot",
            ["combat.silentaim.enabled"] = "Silent Aim",
            ["combat.magicbullet.enabled"] = "Magic Bullet",
            ["combat.weapons.norecoil"] = "No Recoil",
            ["combat.weapons.nospread"] = "No Spread",
            ["combat.weapons.infiniteammo"] = "Infinite Ammo",
            ["combat.weapons.rapidfire"] = "Rapid Fire",
            ["combat.weapons.infiniterange"] = "Infinite Range",

            // visual
            ["visual.esp.tracers"] = "Tracers",
            ["visual.esp.skeleton"] = "Skeleton",
            ["visual.esp.3dboxes"] = "3D Boxes",
            ["visual.esp.2dboxes"] = "2D Boxes",
            ["visual.esp.filledboxes"] = "Filled Boxes",
            ["visual.esp.info"] = "Info",
            ["visual.esp.nametags"] = "Nametags",

            ["visual.fovchanger.enabled"] = "FOV Changer",

            ["visual.speedgraph.enabled"] = "Speed Graph",

            ["visual.crosshair.enabled"] = "Crosshair",

            ["visual.camerasettings.enabled"] = "Camera Settings",

            // movement
            ["movement.speed.enabled"] = "Speed",

            ["movement.fly.enabled"] = "Fly",

            ["movement.bhop.enabled"] = "BHop",

            // exploits
            ["exploits.player.godmode"] = "Godmode",
            ["exploits.player.instantland"] = "Instant Land",
            ["exploits.player.infinitematerials"] = "Infinite Materials",
            ["exploits.player.antifreeze"] = "Anti Freeze",

            ["exploits.gameplay.autoplay"] = "Auto Play",

            ["exploits.game.buildingspam"] = "Building Spam",
            ["exploits.game.rigspam"] = "Rig Spam",
            ["exploits.game.instantbreak"] = "Instant Break",

            ["exploits.fun.levelchanger"] = "Level Changer",

            ["exploits.fun.buildinggun"] = "Building Gun",
            ["exploits.fun.teleportgun"] = "Teleport Gun",
            ["exploits.fun.explosiongun"] = "Explosion Gun",

            // settings
            ["settings.interface.watermark"] = "Watermark",

            ["settings.interface.featurelist"] = "Feature List",

            ["settings.interface.menueffects"] = "Menu Effects",
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
