using UnityEngine;

namespace NetWare.UI.Styles;

public static class ToggleStyles
{
    public static GUIStyle Create(Color backgroundColor)
    {
        return new GUIStyle("Box")
        {
            onNormal =
            {
                background = Texture.NewBorder(0, 0, 0, backgroundColor.r, backgroundColor.g, backgroundColor.b),
            },

            normal =
            {
                background = Texture.NewBorder(0, 0, 0, .15f, .15f, .15f),
            },

            border = new RectOffset(1, 1, 1, 1),
            fixedHeight = 18,
            fixedWidth = 18,
        };
    }

    public static GUIStyle CreateText()
    {
        return new GUIStyle("Box")
        {
            normal = {
                background = Texture.New(.075f, .075f, .075f),
            },

            fontStyle = FontStyle.Bold,
            fontSize = 13,
        };
    }

    public static GUIStyle CreateButton()
    {
        return new GUIStyle("Box")
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
        };
    }

    public static GUIStyle CreateTab(bool isSelected)
    {
        return new GUIStyle("Box")
        {
            normal = {
                background = Texture.New(.075f, .075f, .075f),
                textColor = (isSelected ? Color.white : Color.gray),
            },

            hover = {
                background = Texture.New(.075f, .075f, .075f),
                textColor = (isSelected ? Color.white : Color.gray),
            },

            fontStyle = FontStyle.Bold,
            fontSize = 12,
        };
    }
}
