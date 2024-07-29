using System;
using System.Linq;
using UnityEngine;

namespace NetWare
{
    public static class Menu
    {
        public static Rect windowRect;
        public static bool displayWindow = true;

        public static string[] tabs = { "Combat", "Visual", "Movement", "Exploits", "Settings" };
        public static int currentTab = 0;

        public static Vector2 tabScrollPosition = Vector2.zero;

        private static bool isSectionOpen = false;

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

        public static void NewSection(string text)
        {
            if (isSectionOpen)
                GUILayout.EndVertical();

            GUILayout.BeginVertical(new GUIContent(), "Box", GUILayout.Width((windowRect.width / 2) - 12));
            GUILayout.Box("<b>" + text + "</b>");
            isSectionOpen = true;
        }

        public static void NewButton(string text, Action callback)
        {
            // make button and execute callback if pressed
            if (GUILayout.Button(text))
                callback();
        }

        public static bool NewToggle(bool value, string text)
        {
            // set toggle style
            GUIStyle toggleStyle = new GUIStyle("Box") { fontSize = 12 };
            if (value) {
                toggleStyle.normal.textColor = new Color(0, 1, 0);
            } else {
                toggleStyle.normal.textColor = new Color(1, 0, 0);
            }

            // make toggle and return new value
            return GUILayout.Toggle(value, text, toggleStyle);
        }

        public static float NewSlider(string text, float value, float minimum, float maximum)
        {
            GUILayout.BeginVertical(new GUIContent(), "Box");

            // slider title and title style
            GUIStyle labelStyle = new GUIStyle("Label") { fontSize = 12, alignment = TextAnchor.MiddleCenter };
            GUILayout.Label(text + " : " + value.ToString(), labelStyle);

            // create slider and return new value
            float newValue = (float)Math.Round(GUILayout.HorizontalSlider(value, minimum, maximum), 1);

            GUILayout.EndVertical();

            if (newValue < minimum) {
                return minimum;
            } else if (newValue > maximum) {
                return maximum;
            }
            return newValue;
        }

        public static string NewTextField(string title, string value)
        {
            GUILayout.BeginVertical(new GUIContent(), "Box");

            // text field title and title style
            GUIStyle labelStyle = new GUIStyle("Label") { fontSize = 12, alignment = TextAnchor.MiddleCenter };
            GUILayout.Label(title, labelStyle);

            // set text field style, create textfield and return new value
            GUIStyle textFieldStyle = new GUIStyle("Box") { fontSize = 12 };
            string newValue = GUILayout.TextField(value, textFieldStyle);

            GUILayout.EndVertical();

            return newValue;
        }

        public static void NewTitle(string text)
        {
            // title style
            GUIStyle labelStyle = new GUIStyle("Label") { fontSize = 12, alignment = TextAnchor.MiddleCenter };

            // get amount of dashes to put for better separation (pretty random code but it works ig)
            float textSize = labelStyle.CalcSize(new GUIContent(text)).x;
            float dashSize = labelStyle.CalcSize(new GUIContent("-")).x;
            float sectionSize = ((windowRect.width / 2) - 12);

            int dashMultiplier = (int)((
                (
                    (sectionSize - textSize) - (dashSize * 4)
                ) / dashSize) / 2
            );

            string separators = string.Concat(Enumerable.Repeat("-", dashMultiplier));

            // create title
            GUILayout.Label("<b>" + separators + " " + text + " " + separators + "</b>", labelStyle);
        }

        public static string NewList(string title, string currentValue, string[] values)
        {
            GUILayout.BeginVertical(new GUIContent(), "Box");

            // list title and title style
            GUIStyle labelStyle = new GUIStyle("Label") { fontSize = 12, alignment = TextAnchor.MiddleCenter };
            GUILayout.Label(title, labelStyle);

            // get current value index
            int currentValueIndex = Array.IndexOf(values, currentValue);

            // set new value
            string newValue = currentValue;

            if (GUILayout.Button(currentValue))
            {
                int newIndex = (currentValueIndex + 1);

                if (newIndex >= values.Length) {
                    newValue = values[0];
                } else {
                    newValue = values[newIndex];
                }
            }

            GUILayout.EndVertical();

            return newValue;
        }

        public static string NewKeybind(string value)
        {
            string newValue = value;

            // set new value
            if (GUILayout.Button(newValue) || newValue == "...")
            {
                newValue = "...";
                if (!Input.anyKey)
                    return newValue;

                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        newValue = keyCode.ToString();
                        if (keyCode == KeyCode.Escape)
                            newValue = "None";
                        break;
                    }
                }
            }

            return newValue;
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
