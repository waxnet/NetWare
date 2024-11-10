using NetWare.Modules.MenuTabs;
using NetWare.Extensions;

using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace NetWare.UI;

public static class Menu
{
    private static readonly KeyCode[] _keyCodes = EnumExtension.GetValues<KeyCode>();

    private static bool _isSectionOpen = false;
    private static string _currentDropDown = "";

    public static Color Color { get; set; } = Color.white;
    public static string Version { get; set; }

    public static Rect WindowRect { get; set; }
    public static bool Enabled { get; set; } = true;

    public static IMenuTab[] Tabs { get; set; }
    public static IMenuTab CurrentTab { get; set; }
    public static Vector2 TabScrollPosition { get; set; } = Vector2.zero;

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

        _isSectionOpen = false;
    }
    public static void Separate()
    {
        GUILayout.EndVertical();
        GUILayout.EndVertical();
        GUILayout.BeginVertical();

        _isSectionOpen = false;
    }

    public static void Window(GUI.WindowFunction windowFunction, string title)
    {
        if (!Enabled)
            return;

        // draw
        WindowRect = GUI.Window(0, WindowRect, windowFunction, title, "Box");
    }
    public static void NewSection(string text)
    {
        if (_isSectionOpen)
            GUILayout.EndVertical();

        GUILayout.BeginVertical(new GUIContent(), "Box", GUILayout.Width((WindowRect.width / 2) - 12));
        GUILayout.Box($"<b>{text}</b>");

        _isSectionOpen = true;
    }
    public static void NewButton(string text, Action callback)
    {
        // draw and call if pressed
        if (GUILayout.Button(text))
            callback();
    }
    public static (bool Value, KeyCode? KeyBind) NewToggle(string text, bool value, KeyCode? keyBind)
    {
        // value
        var newKeybind = keyBind;

        // style
        GUIStyle toggleStyle = new("Box")
        {
            normal = {
                textColor = (value ? Color.green : Color.red)
            },
            fontSize = 13
        };

        var valueContent = new GUIContent($"[ {(newKeybind is null ? "..." : newKeybind.Value.ConvertToString().ToUpper())} ]");

        // draw and return values
        GUILayout.BeginHorizontal();
        var newValue = GUILayout.Toggle(value, text, toggleStyle);

        // keybind logic
        if (GUILayout.Button(valueContent, "Box", GUILayout.Width(GUI.skin.box.CalcSize(valueContent).x + 10)) || newKeybind is null)
        {
            if (!Input.anyKey)
            {
                GUILayout.EndHorizontal();
                return (newValue, null);
            }

            foreach (KeyCode keyCode in _keyCodes)
                if (Input.GetKeyDown(keyCode))
                {
                    newKeybind = keyCode;

                    if (keyCode == KeyCode.Escape)
                        newKeybind = KeyCode.None;

                    break;
                }
        }

        GUILayout.EndHorizontal();
        return (newValue, newKeybind);
    }
    public static bool NewToggle(string text, bool value)
    {
        // style
        GUIStyle toggleStyle = new("Box")
        {
            normal = {
                textColor = (value ? Color.green : Color.red)
            },
            fontSize = 13
        };

        // draw and return values
        var newValue = GUILayout.Toggle(value, text, toggleStyle);

        return newValue;
    }
    public static float NewSlider(string text, float value, float minimum, float maximum)
    {
        // label style
        GUIStyle labelStyle = new("Label") { fontSize = 12, alignment = TextAnchor.MiddleCenter };

        // draw
        GUILayout.BeginVertical("Box");
        GUILayout.BeginHorizontal();
        GUILayout.Label(text, labelStyle);
        GUILayout.Label(value.ToString("F1"), labelStyle);
        GUILayout.EndHorizontal();
        GUILayout.Space(-2);
        var newValue = (float)Math.Round(GUILayout.HorizontalSlider(value, minimum, maximum), 1);
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
        // style
        GUIStyle labelStyle = new("Label") { fontSize = 12, alignment = TextAnchor.MiddleCenter };
        GUIStyle textFieldStyle = new("Box") { fontSize = 12 };

        // draw
        GUILayout.BeginVertical("Box");
        GUILayout.Label(title, labelStyle);
        GUILayout.Space(-4);

        var newValue = GUILayout.TextField(value, textFieldStyle);

        GUILayout.EndVertical();

        // return value
        return newValue;
    }
    public static void NewTitle(string text)
    {
        // title style
        GUIStyle labelStyle = new("Label")
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleCenter
        };

        // draw
        var textSize = labelStyle.CalcSize(new GUIContent(text)).x;
        var dashSize = labelStyle.CalcSize(new GUIContent("-")).x;
        var sectionSize = (WindowRect.width / 2) - 14;

        var dashMultiplier = (int)((sectionSize - textSize - (dashSize * 4)) / dashSize) / 2;

        var separators = string.Concat(Enumerable.Repeat("-", dashMultiplier));

        GUILayout.Label($"<b>{separators} {text} {separators}</b>", labelStyle);
    }
    public static string NewDropdown(string title, string identifier, string currentValue, IEnumerable<string> values)
    {
        // style
        GUIStyle labelStyle = new("Label") { fontSize = 12, alignment = TextAnchor.MiddleCenter };

        // draw
        GUILayout.BeginVertical("Box");
        GUILayout.Label(title, labelStyle);
        GUILayout.Space(-4);

        if (_currentDropDown != identifier && GUILayout.Button(currentValue, "Button"))
            _currentDropDown = identifier;

        if (_currentDropDown == identifier)
        {
            GUILayout.BeginVertical("Box");

            foreach (var value in values)
                if (GUILayout.Button(value, "Button"))
                {
                    currentValue = value;
                    _currentDropDown = "";
                }

            GUILayout.EndVertical();
        }

        GUILayout.EndVertical();

        // return value
        return currentValue;
    }
}
