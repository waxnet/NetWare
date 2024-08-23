using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace NetWare
{
    public static class Menu
    {
        public static Color color = Color.white;
        public static string version;

        public static Rect windowRect;
        public static bool displayWindow = true;

        public static string[] tabs;
        public static int currentTab = 0;

        public static Vector2 tabScrollPosition = Vector2.zero;

        private static bool isSectionOpen = false;

        private static string currentDropdown = "";

        // cache system
        private static Dictionary<string, GUIStyle> cache = new Dictionary<string, GUIStyle>();

        public static string GetCacheKey(params string[] values)
        {
            return string.Join("_", values);
        }

        public static GUIStyle GetOrCreateStyle(string key, Func<GUIStyle> createStyle)
        {
            if (cache.TryGetValue(key, out GUIStyle cachedStyle))
                return cachedStyle;

            GUIStyle newStyle = createStyle();

            cache[key] = newStyle;
            return newStyle;
        }

        // methods
        public static void Begin()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
        }
        public static void End()
        {
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            isSectionOpen = false;
        }
        public static void Separate()
        {
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            isSectionOpen = false;
        }

        public static void Window(GUI.WindowFunction windowFunction, string title)
        {
            if (displayWindow)
            {
                // styles
                GUIStyle menuStyle = GetOrCreateStyle(GetCacheKey($"{title}_window"), () => new GUIStyle("Box")
                {
                    normal = {
                        background = Texture.NewBorder(color.r, color.g, color.b, .075f, .075f, .075f),
                    },
                    fontStyle = FontStyle.BoldAndItalic,
                    fontSize = 22,
                    border = new RectOffset(1, 1, 1, 1),
                    contentOffset = ((version == null) ? new Vector2(10, 6) : Vector2.zero),
                    alignment = TextAnchor.UpperLeft,
                });

                // draw
                windowRect = GUI.Window(0, windowRect, windowFunction, title, menuStyle);
            }
        }
        public static void NewSection(string text)
        {
            if (isSectionOpen)
                GUILayout.EndVertical();

            // styles
            GUIContent titleText = new GUIContent(text);
            GUIStyle titleStyle = GetOrCreateStyle(GetCacheKey($"{text}_sectiontitle"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.New(.075f, .075f, .075f),
                },
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            GUIStyle sectionStyle = GetOrCreateStyle(GetCacheKey($"{text}_section"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.NewBorder(.5f, .5f, .5f, .075f, .075f, .075f),
                },
                border = new RectOffset(1, 1, 1, 1),
            });

            // draw
            GUILayout.BeginVertical(GUIContent.none, sectionStyle, GUILayout.Width((windowRect.width / 2) - 12));
            GUILayout.Space(-8);
            GUILayout.Label(titleText, titleStyle, GUILayout.Width(titleStyle.CalcSize(titleText).x + 20));
            isSectionOpen = true;
        }
        public static void NewButton(string text, Action callback)
        {
            // style
            GUIStyle buttonStyle = GetOrCreateStyle(GetCacheKey($"{text}_button"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.NewBorder(0, 0, 0, .1f, .1f, .1f),
                },
                hover = {
                    background = Texture.NewBorder(0, 0, 0, .12f, .12f, .12f),
                    textColor = Color.white,
                },
                border = new RectOffset(1, 1, 1, 1),
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            // draw and call if pressed
            if (GUILayout.Button(text, buttonStyle))
                callback();
        }
        public static (bool, string) NewToggle(bool value, string text, string keybind)
        {
            // value
            string newKeybind = keybind.ToUpper();

            // styles
            GUIStyle toggleStyle = GetOrCreateStyle(GetCacheKey($"{text}_toggle"), () => new GUIStyle("Box")
            {
                onNormal = {
                    background = Texture.NewBorder(0, 0, 0, color.r, color.g, color.b),
                },
                normal = {
                    background = Texture.NewBorder(0, 0, 0, .15f, .15f, .15f),
                },
                border = new RectOffset(1, 1, 1, 1),
                fixedHeight = 18,
                fixedWidth = 18,
            });

            GUIContent displayText = new GUIContent(text);
            GUIStyle textStyle = GetOrCreateStyle(GetCacheKey($"{text}_text"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.New(.075f, .075f, .075f),
                },
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            GUIContent valueContent = new GUIContent($"[ {newKeybind} ]");
            GUIStyle buttonStyle = GetOrCreateStyle(GetCacheKey($"{text}_keybind"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.New(.075f, .075f, .075f),
                },
                hover = {
                    background = Texture.New(.075f, .075f, .075f),
                    textColor = Color.white,
                },
                alignment = TextAnchor.MiddleRight,
                fontStyle = FontStyle.Bold,
                fontSize = 12,
            });

            // draw and return values
            GUILayout.BeginHorizontal();
            bool newValue = GUILayout.Toggle(value, "", toggleStyle);

            GUILayout.BeginVertical(GUILayout.Height(toggleStyle.fixedHeight));
            GUILayout.FlexibleSpace();
            GUILayout.Space(2);
            GUILayout.Label(displayText, textStyle, GUILayout.Width(textStyle.CalcSize(displayText).x + 2));
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();

            // keybind logic
            if (newKeybind != "")
                if (GUILayout.Button(valueContent, buttonStyle, GUILayout.Width(buttonStyle.CalcSize(valueContent).x + 10)) || newKeybind == "...")
                {
                    newKeybind = "...";
                    if (!Input.anyKey)
                    {
                        GUILayout.EndHorizontal();
                        return (newValue, newKeybind);
                    }

                    foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                        if (Input.GetKeyDown(keyCode))
                        {
                            newKeybind = keyCode.ToString();
                            if (keyCode == KeyCode.Escape)
                                newKeybind = "None";
                            break;
                        }
                }

            GUILayout.EndHorizontal();

            return (newValue, newKeybind);
        }
        public static bool NewToggle(bool value, string text)
        {
            // styles
            GUIStyle toggleStyle = GetOrCreateStyle(GetCacheKey($"{text}_toggle"), () => new GUIStyle("Box")
            {
                onNormal = {
                    background = Texture.NewBorder(0, 0, 0, color.r, color.g, color.b),
                },
                normal = {
                    background = Texture.NewBorder(0, 0, 0, .15f, .15f, .15f),
                },
                border = new RectOffset(1, 1, 1, 1),
                fixedHeight = 18,
                fixedWidth = 18,
            });

            GUIContent displayText = new GUIContent(text);
            GUIStyle textStyle = GetOrCreateStyle(GetCacheKey($"{text}_text"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.New(.075f, .075f, .075f),
                },
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            // draw and return values
            GUILayout.BeginHorizontal();
            bool newValue = GUILayout.Toggle(value, "", toggleStyle);

            GUILayout.BeginVertical(GUILayout.Height(toggleStyle.fixedHeight));
            GUILayout.FlexibleSpace();
            GUILayout.Space(2);
            GUILayout.Label(displayText, textStyle, GUILayout.Width(textStyle.CalcSize(displayText).x + 2));
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            return newValue;
        }
        public static float NewSlider(string text, float value, float minimum, float maximum)
        {
            // styles
            GUIStyle areaStyle = GetOrCreateStyle(GetCacheKey($"{text}{minimum}{maximum}_area"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.NewBorder(.2f, .2f, .2f, .075f, .075f, .075f),
                },
                border = new RectOffset(1, 1, 1, 1),
            });

            GUIStyle sliderStyle = GetOrCreateStyle(GetCacheKey($"{text}{minimum}{maximum}_slider"), () => new GUIStyle("HorizontalSlider")
            {
                normal = {
                    background = Texture.NewBorder(0, 0, 0, .15f, .15f, .15f),
                },
                fixedHeight = 10,
                border = new RectOffset(1, 1, 1, 1),
            });

            GUIStyle thumbStyle = GetOrCreateStyle(GetCacheKey($"{text}{minimum}{maximum}_thumb"), () => new GUIStyle("HorizontalSliderThumb")
            {
                normal = {
                    background = Texture.NewBorder(0, 0, 0, .3f, .3f, .3f),
                },
                active = {
                    background = Texture.NewBorder(0, 0, 0, .4f, .4f, .4f),
                },
                hover = {
                    background = Texture.NewBorder(0, 0, 0, .5f, .5f, .5f),
                },
                fixedHeight = 10,
                fixedWidth = 12,
                border = new RectOffset(1, 1, 1, 1),
            });

            GUIContent textContent = new GUIContent(text);
            GUIStyle textStyle = GetOrCreateStyle(GetCacheKey($"{text}{minimum}{maximum}_text"), () => new GUIStyle("Label")
            {
                fontStyle = FontStyle.Bold,
                fontSize = 13,
                alignment = TextAnchor.MiddleLeft,
                contentOffset = new Vector2(10, 0),
            });

            GUIContent valueContent = new GUIContent(value.ToString("F1"));
            GUIStyle valueStyle = GetOrCreateStyle(GetCacheKey($"{text}{minimum}{maximum}_value"), () => new GUIStyle("Label")
            {
                fontStyle = FontStyle.Bold,
                fontSize = 13,
                alignment = TextAnchor.MiddleRight,
                contentOffset = new Vector2(-10, 0),
            });

            // draw
            GUILayout.BeginVertical(areaStyle);
            GUILayout.BeginHorizontal();
            GUILayout.Label(textContent, textStyle);
            GUILayout.Label(valueContent, valueStyle);
            GUILayout.EndHorizontal();
            GUILayout.Space(-2);
            float newValue = (float)Math.Round(GUILayout.HorizontalSlider(value, minimum, maximum, sliderStyle, thumbStyle), 1);
            GUILayout.EndVertical();

            // return value
            if (newValue < minimum)
                return minimum;
            else if (newValue > maximum)
                return maximum;
            return newValue;
        }
        public static string NewTextField(string title, string value)
        {
            // styles
            GUIStyle areaStyle = GetOrCreateStyle(GetCacheKey($"{title}_area"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.NewBorder(.2f, .2f, .2f, .075f, .075f, .075f),
                },
                border = new RectOffset(1, 1, 1, 1),
            });

            GUIStyle titleStyle = GetOrCreateStyle(GetCacheKey($"{title}_title"), () => new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            GUIStyle textFieldStyle = GetOrCreateStyle(GetCacheKey($"{title}_textfield"), () => new GUIStyle("Label")
            {
                normal = {
                    background = Texture.NewBorder(0, 0, 0, .1f, .1f, .1f),
                    textColor = Color.gray,
                },
                focused = {
                    background = Texture.NewBorder(0, 0, 0, .12f, .12f, .12f),
                    textColor = Color.white,
                },
                border = new RectOffset(1, 1, 1, 1),
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 12,
            });

            // draw
            GUILayout.BeginVertical(areaStyle);
            GUILayout.Label(title, titleStyle);
            GUILayout.Space(-4);

            string newValue = GUILayout.TextField(value, textFieldStyle);

            GUILayout.EndVertical();

            // return value
            return newValue;
        }
        public static void NewTitle(string text)
        {
            // style
            GUIStyle labelStyle = GetOrCreateStyle(GetCacheKey($"{text}_label"), () => new GUIStyle("Label")
            {
                normal = {
                    textColor = Color.gray,
                },
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            // draw
            float textSize = labelStyle.CalcSize(new GUIContent(text)).x;
            float dashSize = labelStyle.CalcSize(new GUIContent("-")).x;
            float sectionSize = ((windowRect.width / 2) - 12);

            int dashMultiplier = (int)((
                (
                    (sectionSize - textSize) - (dashSize * 4)
                ) / dashSize) / 2
            );

            string separators = string.Concat(Enumerable.Repeat("-", dashMultiplier));

            GUILayout.Label($"<b>{separators} {text} {separators}</b>", labelStyle);
        }
        public static string NewDropdown(string title, string currentValue, string[] values)
        {
            // styles
            GUIStyle areaStyle = GetOrCreateStyle(GetCacheKey($"{title}_area"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.NewBorder(.2f, .2f, .2f, .075f, .075f, .075f),
                },
                border = new RectOffset(1, 1, 1, 1),
            });

            GUIStyle titleStyle = GetOrCreateStyle(GetCacheKey($"{title}_title"), () => new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            GUIStyle buttonStyle = GetOrCreateStyle(GetCacheKey($"{title}_button"), () => new GUIStyle("Box")
            {
                normal = {
                    background = Texture.NewBorder(0, 0, 0, .1f, .1f, .1f),
                },
                hover = {
                    background = Texture.NewBorder(0, 0, 0, .12f, .12f, .12f),
                    textColor = Color.white,
                },
                border = new RectOffset(1, 1, 1, 1),
                fontStyle = FontStyle.Bold,
                fontSize = 13,
            });

            // draw
            GUILayout.BeginVertical(areaStyle);
            GUILayout.Label(title, titleStyle);
            GUILayout.Space(-4);

            if (currentDropdown != title && GUILayout.Button(currentValue, buttonStyle))
                currentDropdown = title;

            if (currentDropdown == title)
            {
                GUILayout.BeginVertical(areaStyle);
                for (int i = 0; i < values.Length; i++)
                    if (GUILayout.Button(values[i], buttonStyle))
                    {
                        currentValue = values[i];
                        currentDropdown = "";
                    }
                GUILayout.EndVertical();
            }
            GUILayout.EndVertical();

            // return value
            return currentValue;
        }

        // other
        public static void CheckKeybinds()
        {
            if (!Input.anyKey)
                return;

            foreach (string key in Config.config.Keys)
            {
                if (key.Split('.').Last() != "keybind")
                    continue;

                string keybindString = Config.GetString(key);
                KeyCode keybindKeycode = (KeyCode)Enum.Parse(typeof(KeyCode), keybindString);
                string toggleKey = key.Replace("keybind", "enabled");

                if (keybindString != "..." && keybindString != "None")
                    if (Input.GetKeyDown(keybindKeycode))
                        Config.SetBool(toggleKey, !Config.GetBool(toggleKey));
            }
        }
    }
}
