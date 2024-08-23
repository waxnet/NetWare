namespace NetWare.Modules
{
    public static class Visual
    {
        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Nametags");
            Config.SetBool(
                "visual.nametags.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.nametags.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "visual.nametags.team",
                Menu.NewTextField(
                    "Team Color",
                    Config.GetString("visual.nametags.team").ToUpper()
                )
            );
            Config.SetString(
                "visual.nametags.enemy",
                Menu.NewTextField(
                    "Enemy Color",
                    Config.GetString("visual.nametags.enemy").ToUpper()
                )
            );
            Config.SetString(
                "visual.nametags.bot",
                Menu.NewTextField(
                    "Bot Color",
                    Config.GetString("visual.nametags.bot").ToUpper()
                )
            );
            Config.SetString(
                "visual.nametags.background",
                Menu.NewTextField(
                    "Background",
                    Config.GetString("visual.nametags.background").ToUpper()
                )
            );

            Menu.NewSection("Skeleton");
            Config.SetBool(
                "visual.skeleton.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.skeleton.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "visual.skeleton.team",
                Menu.NewTextField(
                    "Team Color",
                    Config.GetString("visual.skeleton.team").ToUpper()
                )
            );
            Config.SetString(
                "visual.skeleton.enemy",
                Menu.NewTextField(
                    "Enemy Color",
                    Config.GetString("visual.skeleton.enemy").ToUpper()
                )
            );
            Config.SetString(
                "visual.skeleton.bot",
                Menu.NewTextField(
                    "Bot Color",
                    Config.GetString("visual.skeleton.bot").ToUpper()
                )
            );

            Menu.NewSection("Camera Settings");
            Config.SetBool(
                "visual.camerasettings.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.camerasettings.enabled"),
                    "Enabled"
                )
            );
            Config.SetFloat(
                "visual.camerasettings.x",
                Menu.NewSlider(
                    "X Offset",
                    Config.GetFloat("visual.camerasettings.x", .2f),
                    -5,
                    5
                )
            );
            Config.SetFloat(
                "visual.camerasettings.y",
                Menu.NewSlider(
                    "Y Offset",
                    Config.GetFloat("visual.camerasettings.y", 1.5f),
                    -5,
                    5
                )
            );
            Config.SetFloat(
                "visual.camerasettings.z",
                Menu.NewSlider(
                    "Z Offset",
                    Config.GetFloat("visual.camerasettings.z", 2.5f),
                    -5,
                    5
                )
            );

            Menu.Separate();

            Menu.NewSection("Tracers");
            Config.SetBool(
                "visual.tracers.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.tracers.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "visual.tracers.team",
                Menu.NewTextField(
                    "Team Color",
                    Config.GetString("visual.tracers.team").ToUpper()
                )
            );
            Config.SetString(
                "visual.tracers.enemy",
                Menu.NewTextField(
                    "Enemy Color",
                    Config.GetString("visual.tracers.enemy").ToUpper()
                )
            );
            Config.SetString(
                "visual.tracers.bot",
                Menu.NewTextField(
                    "Bot Color",
                    Config.GetString("visual.tracers.bot").ToUpper()
                )
            );

            Menu.NewSection("Boxes");
            Config.SetBool(
                "visual.boxes.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.boxes.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "visual.boxes.team",
                Menu.NewTextField(
                    "Team Color",
                    Config.GetString("visual.boxes.team").ToUpper()
                )
            );
            Config.SetString(
                "visual.boxes.enemy",
                Menu.NewTextField(
                    "Enemy Color",
                    Config.GetString("visual.boxes.enemy").ToUpper()
                )
            );
            Config.SetString(
                "visual.boxes.bot",
                Menu.NewTextField(
                    "Bot Color",
                    Config.GetString("visual.boxes.bot").ToUpper()
                )
            );

            Menu.NewSection("FOV Changer");
            Config.SetBool(
                "visual.fovchanger.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.fovchanger.enabled"),
                    "Enabled"
                )
            );
            Config.SetFloat(
                "visual.fovchanger.amount",
                Menu.NewSlider(
                    "Amount",
                    Config.GetFloat("visual.fovchanger.amount", 100),
                    20,
                    150
                )
            );

            Menu.End();
        }
    }
}
