using NetWare.Helpers;
using UnityEngine;

namespace NetWare
{
    public static class Visual
    {
        public static void Execute()
        {
            // fov changer
            if (Config.GetBool("visual.fovchanger.enabled"))
            {
                VisualH.resetFOV = true;

                CameraManager cameraManager = LocalPlayer.GetCameraManager();

                if (cameraManager != null)
                {
                    cameraManager.ResetZoomStateInstant();
                    Camera.main.fieldOfView = Config.GetFloat("visual.fovchanger.fovchangeramount");
                }
            } else if (VisualH.resetFOV) {
                VisualH.resetFOV = false;
                Camera.main.fieldOfView = 60;
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
                "visual.fovchanger.fovchangeramount",
                Menu.NewSlider(
                    "Amount",
                    Config.GetFloat("visual.fovchanger.fovchangeramount"),
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
            Config.SetBool(
                "visual.crosshair.dynamic",
                Menu.NewToggle(
                    Config.GetBool("visual.crosshair.dynamic"),
                    "Dynamic"
                )
            );
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
