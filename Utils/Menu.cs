using System;
using UnityEngine;

namespace NetWare
{
    public class Menu
    {
        public static Rect windowRect;
        public static bool displayWindow = true;

        public static string[] tabs = { "Combat", "Visual", "Movement", "Exploits", "Settings" };
        public static int currentTab = 0;

        public static bool isSectionOpen = false;

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
            {
                GUILayout.EndVertical();
            }

            GUILayout.BeginVertical(new GUIContent(), "Box", GUILayout.Width((windowRect.width / 2) - 12));
            GUILayout.Box("<b>" + text + "</b>");
            isSectionOpen = true;
        }

        public static void NewButton(string text, Action callback)
        {
            // make button and execute callback if pressed
            if (GUILayout.Button(text))
            {
                callback();
            }
        }

        public static bool NewToggle(bool value, string text)
        {
            // set toggle style
            GUIStyle toggleStyle = new GUIStyle("Box") { fontSize = 12 };
            if (value)
            {
                toggleStyle.normal.textColor = new Color(0, 1, 0);
            } else {
                toggleStyle.normal.textColor = new Color(1, 0, 0);
            }

            // make toggle and return new value
            return GUILayout.Toggle(value, text, toggleStyle);
        }

        public static int NewSlider(string text, int value, float minimum, float maximum)
        {
            // slider title and title style
            GUIStyle labelStyle = new GUIStyle("Box") { fontSize = 12 };
            GUILayout.Label(text + " : " + value.ToString(), labelStyle);

            // create slider and return new value
            return (int)Math.Round(GUILayout.HorizontalSlider(value, minimum, maximum));
        }

        public static string NewTextField(string text)
        {
            // set text field style
            GUIStyle textFieldStyle = new GUIStyle("Box") { fontSize = 12 };

            // create textfield and return new value
            return GUILayout.TextField(text, textFieldStyle);
        }
    }
}
