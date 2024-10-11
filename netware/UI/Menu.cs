using NetWare.Storage;
using NetWare.UI.Styles;
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

    // cache system
    private static readonly CacheStorage<GUIStyle> _styleCache = new();

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

        var menuStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(title, "window"), () => WindowStyles.Create(Version, Color));

        // draw
        WindowRect = GUI.Window(0, WindowRect, windowFunction, title, menuStyle);
    }
    public static void NewSection(string text)
    {
        if (_isSectionOpen)
            GUILayout.EndVertical();

        // styles
        var titleText = new GUIContent(text);

        var titleStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "sectionTitle"), SectionStyles.CreateTitle);
        var sectionStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "section"), SectionStyles.Create);

        // draw
        GUILayout.BeginVertical(GUIContent.none, sectionStyle, GUILayout.Width((WindowRect.width / 2) - 12));
        GUILayout.Space(-8);
        GUILayout.Label(titleText, titleStyle, GUILayout.Width(titleStyle.CalcSize(titleText).x + 20));

        _isSectionOpen = true;
    }
    public static void NewButton(string text, Action callback)
    {
        // style
        var buttonStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "button"), ButtonStyles.Create);

        // draw and call if pressed
        if (GUILayout.Button(text, buttonStyle))
            callback();
    }
    public static (bool Value, KeyCode? KeyBind) NewToggle(string text, bool value, KeyCode? keyBind)
    {
        // value
        var newKeybind = keyBind;

        // styles
        var toggleStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "toggle"), () => ToggleStyles.Create(Color));

        var displayText = new GUIContent(text);
        var textStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "text"), ToggleStyles.CreateText);

        var valueContent = new GUIContent($"[ {(newKeybind is null ? "..." : newKeybind.Value.ConvertToString().ToUpper())} ]");
        var buttonStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "keybind"), ToggleStyles.CreateButton);

        // draw and return values
        GUILayout.BeginHorizontal();
        var newValue = GUILayout.Toggle(value, "", toggleStyle);

        GUILayout.BeginVertical(GUILayout.Height(toggleStyle.fixedHeight));
        GUILayout.FlexibleSpace();
        GUILayout.Space(2);
        GUILayout.Label(displayText, textStyle, GUILayout.Width(textStyle.CalcSize(displayText).x + 2));
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        // keybind logic
        if (GUILayout.Button(valueContent, buttonStyle, GUILayout.Width(buttonStyle.CalcSize(valueContent).x + 10)) || newKeybind is null)
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
        // styles
        var toggleStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "toggle"), () => ToggleStyles.Create(Color));

        var displayText = new GUIContent(text);
        var textStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "text"), ToggleStyles.CreateText);

        // draw and return values
        GUILayout.BeginHorizontal();
        var newValue = GUILayout.Toggle(value, "", toggleStyle);

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
        var areaStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, minimum, maximum, "area"), SliderStyles.CreateArea);
        var sliderStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, minimum, maximum, "slider"), SliderStyles.CreateHorizontal);
        var thumbStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, minimum, maximum, "thumb"), SliderStyles.CreateThumb);

        var textContent = new GUIContent(text);
        var textStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, minimum, maximum, "text"), SliderStyles.CreateLabel);

        var valueContent = new GUIContent(value.ToString("F1"));
        var valueStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, minimum, maximum, "value"), SliderStyles.CreateValueLabel);

        // draw
        GUILayout.BeginVertical(areaStyle);
        GUILayout.BeginHorizontal();
        GUILayout.Label(textContent, textStyle);
        GUILayout.Label(valueContent, valueStyle);
        GUILayout.EndHorizontal();
        GUILayout.Space(-2);
        var newValue = (float)Math.Round(GUILayout.HorizontalSlider(value, minimum, maximum, sliderStyle, thumbStyle), 1);
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
        var areaStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(title, "area"), LabelStyles.CreateArea);
        var titleStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(title, "title"), LabelStyles.CreateTitle);
        var textFieldStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(title, "textField"), LabelStyles.Create);

        // draw
        GUILayout.BeginVertical(areaStyle);
        GUILayout.Label(title, titleStyle);
        GUILayout.Space(-4);

        var newValue = GUILayout.TextField(value, textFieldStyle);

        GUILayout.EndVertical();

        // return value
        return newValue;
    }
    public static void NewTitle(string text)
    {
        // style
        GUIStyle labelStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(text, "title"), TitleStyles.Create);

        // draw
        var textSize = labelStyle.CalcSize(new GUIContent(text)).x;
        var dashSize = labelStyle.CalcSize(new GUIContent("-")).x;
        var sectionSize = (WindowRect.width / 2) - 12;

        var dashMultiplier = (int)((sectionSize - textSize - (dashSize * 4)) / dashSize) / 2;

        var separators = string.Concat(Enumerable.Repeat("-", dashMultiplier));

        GUILayout.Label($"<b>{separators} {text} {separators}</b>", labelStyle);
    }
    public static string NewDropdown(string title, string identifier, string currentValue, IEnumerable<string> values)
    {
        // styles
        var areaStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(title, "area"), DropdownStyles.CreateArea);
        var titleStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(title, "title"), DropdownStyles.CreateLabel);
        var buttonStyle = _styleCache.GetOrCreate(CacheStorage.CreateCacheKey(title, "button"), DropdownStyles.CreateButton);

        // draw
        GUILayout.BeginVertical(areaStyle);
        GUILayout.Label(title, titleStyle);
        GUILayout.Space(-4);

        if (_currentDropDown != identifier && GUILayout.Button(currentValue, buttonStyle))
            _currentDropDown = identifier;

        if (_currentDropDown == identifier)
        {
            GUILayout.BeginVertical(areaStyle);

            foreach (var value in values)
                if (GUILayout.Button(value, buttonStyle))
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
