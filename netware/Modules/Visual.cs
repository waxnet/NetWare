using NetWare.Helpers;
using UnityEngine;

namespace NetWare
{
    public static class Visual
    {
        public static void Execute()
        {
            // reset zoom state if needed
            if (Config.GetBool("visual.fovchanger.enabled") || Config.GetBool("visual.camerasettings.enabled"))
            {
                LocalPlayer.GetCameraManager()?.ResetZoomStateInstant();
            }

            // fov changer
            if (Config.GetBool("visual.fovchanger.enabled"))
            {
                VisualH.resetFOV = true;
                Camera.main.fieldOfView = Config.GetFloat("visual.fovchanger.amount");
            } else if (VisualH.resetFOV) {
                VisualH.resetFOV = false;
                Camera.main.fieldOfView = 60;
            }

            // camera settings
            if (Config.GetBool("visual.camerasettings.enabled"))
            {
                vThirdPersonCamera thirdPersonCamera = LocalPlayer.GetThirdPersonCamera();

                if (thirdPersonCamera != null)
                {
                    thirdPersonCamera.rightOffset = Config.GetFloat("visual.camerasettings.x");
                    thirdPersonCamera.SetHeight(Config.GetFloat("visual.camerasettings.y"));
                    thirdPersonCamera.defaultDistance = Config.GetFloat("visual.camerasettings.z");
                }
            }
        }

        public static void Draw()
        {
            // esp
            if (
                Config.GetBool("visual.esp.tracers") ||
                Config.GetBool("visual.esp.skeleton") ||
                Config.GetBool("visual.esp.3dboxes") ||
                Config.GetBool("visual.esp.2dboxes") ||
                Config.GetBool("visual.esp.filledboxes") ||
                Config.GetBool("visual.esp.info") ||
                Config.GetBool("visual.esp.nametags")
            )
            {
                foreach (PlayerController playerController in Storage.players)
                {
                    if (Players.IsPlayerValid(playerController))
                    {
                        // get background color
                        Color backgroundColor = Colors.HexToRGB(Config.GetString("visual.esp.backgroundcolor"));

                        // tracers
                        if (Config.GetBool("visual.esp.tracers"))
                        {
                            VisualH.DrawPlayerTracer(playerController);
                        }

                        // skeleton
                        if (Config.GetBool("visual.esp.skeleton"))
                        {
                            VisualH.DrawPlayerSkeleton(playerController);
                        }

                        // 3d, 2d and filled boxes
                        if (Config.GetBool("visual.esp.3dboxes"))
                        {
                            VisualH.DrawPlayer3DBox(playerController);
                        }
                        if (Config.GetBool("visual.esp.2dboxes"))
                        {
                            VisualH.DrawPlayer2DBox(playerController);
                        }
                        if (Config.GetBool("visual.esp.filledboxes"))
                        {
                            VisualH.DrawPlayerFilledBox(playerController);
                        }

                        // info
                        if (Config.GetBool("visual.esp.info"))
                        {
                            VisualH.DrawPlayerInfo(playerController, backgroundColor);
                        }

                        // nametags
                        if (Config.GetBool("visual.esp.nametags"))
                        {
                            VisualH.DrawPlayerNametag(playerController, backgroundColor);
                        }
                    }
                }
            }

            // speed graph
            if (Config.GetBool("visual.speedgraph.enabled"))
                VisualH.DrawSpeedGraph();

            // crosshair
            if (Config.GetBool("visual.crosshair.enabled"))
                VisualH.DrawCrosshair();
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("ESP");
            Config.SetBool(
                "visual.esp.tracers",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.tracers"),
                    "Tracers"
                )
            );
            Config.SetBool(
                "visual.esp.skeleton",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.skeleton"),
                    "Skeleton"
                )
            );
            Config.SetBool(
                "visual.esp.3dboxes",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.3dboxes"),
                    "3D Boxes"
                )
            );
            Config.SetBool(
                "visual.esp.2dboxes",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.2dboxes"),
                    "2D Boxes"
                )
            );
            Config.SetBool(
                "visual.esp.filledboxes",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.filledboxes"),
                    "Filled Boxes"
                )
            );
            Config.SetBool(
                "visual.esp.info",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.info"),
                    "Info"
                )
            );
            Config.SetBool(
                "visual.esp.nametags",
                Menu.NewToggle(
                    Config.GetBool("visual.esp.nametags"),
                    "Nametags"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "visual.esp.backgroundcolor",
                Menu.NewTextField(
                    "Background Color",
                    Config.GetString("visual.esp.backgroundcolor").ToUpper()
                )
            );
            Config.SetString(
                "visual.esp.teammatecolor",
                Menu.NewTextField(
                    "Teammate Color",
                    Config.GetString("visual.esp.teammatecolor").ToUpper()
                )
            );
            Config.SetString(
                "visual.esp.enemycolor",
                Menu.NewTextField(
                    "Enemy Color",
                    Config.GetString("visual.esp.enemycolor").ToUpper()
                )
            );
            Config.SetString(
                "visual.esp.botcolor",
                Menu.NewTextField(
                    "Bot Color",
                    Config.GetString("visual.esp.botcolor").ToUpper()
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
            Menu.NewTitle("Settings");
            Config.SetFloat(
                "visual.camerasettings.x",
                Menu.NewSlider(
                    "X Offset",
                    Config.GetFloat("visual.camerasettings.x"),
                    -5,
                    5
                )
            );
            Config.SetFloat(
                "visual.camerasettings.y",
                Menu.NewSlider(
                    "Y Offset",
                    Config.GetFloat("visual.camerasettings.y"),
                    -5,
                    5
                )
            );
            Config.SetFloat(
                "visual.camerasettings.z",
                Menu.NewSlider(
                    "Z Offset",
                    Config.GetFloat("visual.camerasettings.z"),
                    -5,
                    5
                )
            );

            Menu.Separate();

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
                    Config.GetFloat("visual.fovchanger.amount"),
                    20,
                    150
                )
            );

            Menu.NewSection("Speed Graph");
            Config.SetBool(
                "visual.speedgraph.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.speedgraph.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "visual.speedgraph.color",
                Menu.NewTextField(
                    "Line Color",
                    Config.GetString("visual.speedgraph.color").ToUpper()
                )
            );
            Config.SetString(
                "visual.speedgraph.colormode",
                Menu.NewList(
                    "Line Color Mode",
                    Config.GetString("visual.speedgraph.colormode"),
                    new string[] { "Normal", "Rainbow", "Rainbow Wave" }
                )
            );

            Menu.NewSection("Crosshair");
            Config.SetBool(
                "visual.crosshair.enabled",
                Menu.NewToggle(
                    Config.GetBool("visual.crosshair.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Settings");
            Config.SetBool(
                "visual.crosshair.dynamic",
                Menu.NewToggle(
                    Config.GetBool("visual.crosshair.dynamic"),
                    "Dynamic"
                )
            );
            Config.SetBool(
                "visual.crosshair.betterscope",
                Menu.NewToggle(
                    Config.GetBool("visual.crosshair.betterscope"),
                    "Better Scope"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "visual.crosshair.color",
                Menu.NewTextField(
                    "Line Color",
                    Config.GetString("visual.crosshair.color").ToUpper()
                )
            );
            Config.SetBool(
                "visual.crosshair.rainbow",
                Menu.NewToggle(
                    Config.GetBool("visual.crosshair.rainbow"),
                    "Rainbow"
                )
            );

            Menu.End();
        }
    }
}
