using System;
using UnityEngine;
using NetWare.Helpers;

namespace NetWare
{
    public class Settings : MonoBehaviour
    {
        public static void Draw()
        {
            // watermark
            if (Config.GetBool("settings.interface.watermark"))
            {
                // get current time
                string currentTime = DateTime.Now.ToString("hh:mm:ss tt");
                if (Config.GetString("settings.interface.timetype") == "Military")
                {
                    currentTime = DateTime.Now.ToString("HH:mm:ss");
                }

                // get current fps
                if (SettingsH.fpsCounterTimer >= 20)
                {
                    float fpsNumber = (1 / Time.smoothDeltaTime);
                    if (fpsNumber > 2000) {
                        SettingsH.fpsCounterContent = "Unknown";
                    } else {
                        SettingsH.fpsCounterContent = fpsNumber.ToString("#,##0");
                    }

                    SettingsH.fpsCounterTimer = 0;
                }
                SettingsH.fpsCounterTimer++;

                // watermark content and style
                GUIContent titleContent = new GUIContent("<b>Net<color=red>Ware</color> v1.9 | " + currentTime + " | " + SettingsH.fpsCounterContent + " FPS</b>");
                GUIStyle titleStyle = new GUIStyle("Label")
                {
                    wordWrap = false,
                    alignment = TextAnchor.MiddleCenter
                };

                Vector2 titleSize = titleStyle.CalcSize(titleContent);

                // watermark background box data
                float backgroundWidth = (titleSize.x + 20);
                float backgroundHeight = 30;

                Vector2 boxPosition = new Vector2(
                    ((backgroundWidth / 2) + 20),
                    ((backgroundHeight / 2) + 20)
                );

                // draw watermark box
                Render.DrawBox(
                    Color.black,
                    boxPosition,
                    backgroundWidth,
                    backgroundHeight
                );

                // draw title
                Rect titleRect = new Rect(
                    (boxPosition.x - (backgroundWidth / 2)),
                    (boxPosition.y - (backgroundHeight / 2)),
                    backgroundWidth,
                    backgroundHeight
                );
                GUI.Label(titleRect, titleContent, titleStyle);

                // draw rainbow line
                Vector3 lineOrigin = boxPosition;
                lineOrigin.x -= ((backgroundWidth / 2) - 1);
                lineOrigin.y -= (backgroundHeight / 2);

                float offset = 0;
                for (int counter = 0; counter < backgroundWidth; counter++)
                {
                    if ((counter % 2) == 0)
                    {
                        offset += .01f;
                    }

                    Vector3 lineDestination = (lineOrigin + new Vector3(0, 2, 0));
                    Render.DrawLine(
                        Colors.GetRainbow(offset),
                        lineOrigin,
                        lineDestination
                    );
                    lineOrigin.x += 1;
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Config Manager");
            SettingsH.textFieldContent = Menu.NewTextField("Config Name", SettingsH.textFieldContent);
            Menu.NewButton("Save", SettingsH.Save);
            Menu.NewButton("Delete", SettingsH.Delete);

            Menu.NewSection("Interface");
            Config.SetBool(
                "settings.interface.watermark",
                Menu.NewToggle(
                    Config.GetBool("settings.interface.watermark"),
                    "Watermark"
                )
            );
            Menu.NewTitle("Settings");
            Config.SetString(
                "settings.interface.timetype",
                Menu.NewList(
                    "Time Type",
                    Config.GetString("settings.interface.timetype"),
                    new string[] { "Standard", "Military" }
                )
            );

            Menu.Separate();

            Menu.NewSection("Config Loader");
            foreach (string config in Config.configList)
            {
                void Load()
                {
                    Config.Load(config);
                }

                Menu.NewButton(config, Load);
            }

            Menu.End();
        }
    }
}
