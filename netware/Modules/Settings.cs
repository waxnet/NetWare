using System;
using UnityEngine;
using NetWare.Helpers;
using System.Linq;

namespace NetWare
{
    public static class Settings
    {
        public static void Draw()
        {
            // watermark
            if (Config.GetBool("settings.interface.watermark"))
            {
                // get current time
                string currentTime = DateTime.Now.ToString("hh:mm:ss tt");
                if (Config.GetString("settings.interface.watermark.timetype") == "Military")
                    currentTime = DateTime.Now.ToString("HH:mm:ss");

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
                GUIContent titleContent = new GUIContent("<b>Net<color=red>Ware</color> v1.9.8.1 | " + currentTime + " | " + SettingsH.fpsCounterContent + " FPS</b>");
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
                        offset += .01f;

                    Vector3 lineDestination = (lineOrigin + new Vector3(0, 2, 0));
                    Render.DrawLine(
                        Colors.GetRainbow(offset),
                        lineOrigin,
                        lineDestination
                    );
                    lineOrigin.x += 1;
                }
            }

            // feature list
            if (Config.GetBool("settings.interface.featurelist"))
            {
                // text data
                string colorMode = Config.GetString("settings.interface.featurelist.colormode");
                GUIStyle style = new GUIStyle
                {
                    fontStyle = FontStyle.Bold
                };
                style.normal.textColor = Colors.HexToRGB(Config.GetString("settings.interface.featurelist.color"));

                // lists
                string[] features = Config.toggles.Keys.ToArray();

                string[] sortedList = features
                    .Select(feature => (Key: feature, Text: Config.toggles[feature]))
                    .OrderByDescending(group => style.CalcSize(new GUIContent(group.Text)).x)
                    .Select(group => group.Key).ToArray();

                // display list
                float rainbowOffset = 0;
                int yPosition = 0;

                foreach (string feature in sortedList)
                {
                    // check if feature is enabled
                    if (!Config.GetBool(feature))
                        continue;

                    // text
                    GUIContent text = new GUIContent(Config.toggles[feature]);

                    // data
                    Vector2 textSize = style.CalcSize(text);

                    int boxWidth = (int)(textSize.x + 20);
                    int boxHeight = 30;

                    Vector2 boxPosition = new Vector3((Screen.width - (boxWidth / 2)), (yPosition + (boxHeight / 2)));

                    // draw box
                    Render.DrawBox(
                        Color.black,
                        boxPosition,
                        boxWidth,
                        boxHeight
                    );

                    // draw text
                    if (colorMode == "Rainbow") {
                        style.normal.textColor = Colors.GetRainbow();
                    } else if (colorMode == "Rainbow Wave") {
                        style.normal.textColor = Colors.GetRainbow(rainbowOffset);
                    }
                    
                    GUI.Label(
                        new Rect(
                            (boxPosition.x - (textSize.x / 2)),
                            (boxPosition.y - (textSize.y / 2)),
                            textSize.x,
                            textSize.y
                        ),
                        text,
                        style
                    );

                    // increase y position
                    rainbowOffset -= .05f;
                    yPosition += boxHeight;
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

            Menu.NewSection("Watermark");
            Config.SetBool(
                "settings.interface.watermark",
                Menu.NewToggle(
                    Config.GetBool("settings.interface.watermark"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Settings");
            Config.SetString(
                "settings.interface.watermark.timetype",
                Menu.NewList(
                    "Time Type",
                    Config.GetString("settings.interface.watermark.timetype"),
                    new string[] { "Standard", "Military" }
                )
            );

            Menu.NewSection("Feature List");
            Config.SetBool(
                "settings.interface.featurelist",
                Menu.NewToggle(
                    Config.GetBool("settings.interface.featurelist"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "settings.interface.featurelist.color",
                Menu.NewTextField(
                    "Text Color",
                    Config.GetString("settings.interface.featurelist.color").ToUpper()
                )
            );
            Config.SetString(
                "settings.interface.featurelist.colormode",
                Menu.NewList(
                    "Text Color Mode",
                    Config.GetString("settings.interface.featurelist.colormode"),
                    new string[] { "Normal", "Rainbow", "Rainbow Wave" }
                )
            );

            Menu.NewSection("Menu Effects");
            Config.SetBool(
                "settings.interface.menueffects",
                Menu.NewToggle(
                    Config.GetBool("settings.interface.menueffects"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Settings");
            Config.SetFloat(
                "settings.interface.menueffects.speed",
                Menu.NewSlider(
                    "Speed",
                    Config.GetFloat("settings.interface.menueffects.speed", .2f),
                    .1f,
                    1
                )
            );
            Config.SetFloat(
                "settings.interface.menueffects.spawndelaymin",
                Menu.NewSlider(
                    "Spawn Delay Min",
                    Config.GetFloat("settings.interface.menueffects.spawndelaymin", .1f),
                    0,
                    Config.GetFloat("settings.interface.menueffects.spawndelaymax", .1f)
                )
            );
            Config.SetFloat(
                "settings.interface.menueffects.spawndelaymax",
                Menu.NewSlider(
                    "Spawn Delay Max",
                    Config.GetFloat("settings.interface.menueffects.spawndelaymax", .1f),
                    Config.GetFloat("settings.interface.menueffects.spawndelaymin", .1f),
                    1
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "settings.interface.menueffects.color",
                Menu.NewTextField(
                    "Color",
                    Config.GetString("settings.interface.menueffects.color").ToUpper()
                )
            );
            Config.SetString(
                "settings.interface.menueffects.colormode",
                Menu.NewList(
                    "Color Mode",
                    Config.GetString("settings.interface.menueffects.colormode"),
                    new string[] { "Normal", "Rainbow", "Confetti" }
                )
            );

            Menu.Separate();

            Menu.NewSection("Config Loader");
            foreach (string config in Config.configList)
            {
                Menu.NewButton(config, () => { Config.Load(config); });
            }

            Menu.End();
        }
    }
}
