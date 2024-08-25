namespace NetWare.Modules
{
    public static class Combat
    {
        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Aimbot");
            void aimbotToggle()
            {
                (bool, string) data = Menu.NewToggle(
                    Config.GetBool("combat.aimbot.enabled"),
                    "Enabled",
                    Config.GetString("combat.aimbot.keybind")
                );

                Config.SetBool("combat.aimbot.enabled", data.Item1);
                Config.SetString("combat.aimbot.keybind", data.Item2);
            } aimbotToggle();
            Menu.NewTitle("Targeting");
            Config.SetString(
                "combat.aimbot.aimbone",
                Menu.NewDropdown(
                    "Aim Bone", "aimbotaimbone",
                    Config.GetString("combat.aimbot.aimbone"),
                    new string[] { "Head", "Neck", "UpperChest", "Hips" }
                )
            );
            Config.SetString(
                "combat.aimbot.aimmode",
                Menu.NewDropdown(
                    "Aim Mode", "aimbotaimmode",
                    Config.GetString("combat.aimbot.aimmode"),
                    new string[] { "Legit", "Lock" }
                )
            );
            Config.SetString(
                "combat.aimbot.filterby",
                Menu.NewDropdown(
                    "Filter By", "aimbotfilterby",
                    Config.GetString("combat.aimbot.filterby"),
                    new string[] { "FOV", "FOV & Closest" }
                )
            );
            Config.SetFloat(
                "combat.aimbot.maxdistance",
                Menu.NewSlider(
                    "Max Distance",
                    Config.GetFloat("combat.aimbot.maxdistance", 500),
                    Config.GetFloat("combat.aimbot.mindistance", 0),
                    1000
                )
            );
            Config.SetFloat(
                "combat.aimbot.mindistance",
                Menu.NewSlider(
                    "Min Distance",
                    Config.GetFloat("combat.aimbot.mindistance", 0),
                    0,
                    Config.GetFloat("combat.aimbot.maxdistance", 500)
                )
            );
            Menu.NewTitle("Smoothing");
            Config.SetFloat(
                "combat.aimbot.smoothing",
                Menu.NewSlider(
                    "Smoothing Amount",
                    Config.GetFloat("combat.aimbot.smoothing", 5),
                    1,
                    10
                )
            );
            Config.SetBool(
                "combat.aimbot.usesensitivity",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.usesensitivity"),
                    "Use Sensitivity"
                )
            );
            Menu.NewTitle("FOV Settings");
            Config.SetBool(
                "combat.aimbot.checkfov",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.checkfov"),
                    "Check FOV"
                )
            );
            Config.SetBool(
                "combat.aimbot.drawfov",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.drawfov"),
                    "Draw FOV"
                )
            );
            Config.SetBool(
                "combat.aimbot.dynamicfov",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.dynamicfov"),
                    "Dynamic FOV"
                )
            );
            Config.SetInt(
                "combat.aimbot.fovsize",
                (int)Menu.NewSlider(
                    "FOV Size",
                    Config.GetInt("combat.aimbot.fovsize"),
                    10,
                    500
                )
            );
            Config.SetInt(
                "combat.aimbot.fovsides",
                (int)Menu.NewSlider(
                    "FOV Sides",
                    Config.GetInt("combat.aimbot.fovsides"),
                    3,
                    80
                )
            );
            Config.SetString(
                "combat.aimbot.fovcolor",
                Menu.NewTextField(
                    "FOV Color",
                    Config.GetString("combat.aimbot.fovcolor").ToUpper()
                )
            );
            Config.SetBool(
                "combat.aimbot.rainbowfov",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.rainbowfov"),
                    "Rainbow FOV"
                )
            );

            Menu.Separate();

            Menu.NewSection("Silent Aim");
            void silentAimToggle()
            {
                (bool, string) data = Menu.NewToggle(
                    Config.GetBool("combat.silentaim.enabled"),
                    "Enabled",
                    Config.GetString("combat.silentaim.keybind")
                );

                Config.SetBool("combat.silentaim.enabled", data.Item1);
                Config.SetString("combat.silentaim.keybind", data.Item2);
            } silentAimToggle();
            Menu.NewTitle("Targeting");
            Config.SetString(
                "combat.silentaim.aimbone",
                Menu.NewDropdown(
                    "Aim Bone", "silentaimaimbone",
                    Config.GetString("combat.silentaim.aimbone"),
                    new string[] { "Head", "Neck", "UpperChest", "Hips" }
                )
            );
            Config.SetString(
                "combat.silentaim.filterby",
                Menu.NewDropdown(
                    "Filter By", "silentaimfilterby",
                    Config.GetString("combat.silentaim.filterby"),
                    new string[] { "FOV", "FOV & Closest" }
                )
            );
            Config.SetFloat(
                "combat.silentaim.maxdistance",
                Menu.NewSlider(
                    "Max Distance",
                    Config.GetFloat("combat.silentaim.maxdistance", 500),
                    Config.GetFloat("combat.silentaim.mindistance", 0),
                    1000
                )
            );
            Config.SetFloat(
                "combat.silentaim.mindistance",
                Menu.NewSlider(
                    "Min Distance",
                    Config.GetFloat("combat.silentaim.mindistance", 0),
                    0,
                    Config.GetFloat("combat.silentaim.maxdistance", 500)
                )
            );
            Menu.NewTitle("FOV Settings");
            Config.SetBool(
                "combat.silentaim.checkfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.checkfov"),
                    "Check FOV"
                )
            );
            Config.SetBool(
                "combat.silentaim.drawfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.drawfov"),
                    "Draw FOV"
                )
            );
            Config.SetBool(
                "combat.silentaim.dynamicfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.dynamicfov"),
                    "Dynamic FOV"
                )
            );
            Config.SetInt(
                "combat.silentaim.fovsize",
                (int)Menu.NewSlider(
                    "FOV Size",
                    Config.GetInt("combat.silentaim.fovsize"),
                    10,
                    500
                )
            );
            Config.SetInt(
                "combat.silentaim.fovsides",
                (int)Menu.NewSlider(
                    "FOV Sides",
                    Config.GetInt("combat.silentaim.fovsides"),
                    3,
                    80
                )
            );
            Config.SetString(
                "combat.silentaim.fovcolor",
                Menu.NewTextField(
                    "FOV Color",
                    Config.GetString("combat.silentaim.fovcolor").ToUpper()
                )
            );
            Config.SetBool(
                "combat.silentaim.rainbowfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.rainbowfov"),
                    "Rainbow FOV"
                )
            );

            Menu.End();
        }
    }
}
