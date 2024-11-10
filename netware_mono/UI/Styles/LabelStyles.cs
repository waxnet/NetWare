using UnityEngine;

namespace NetWare.UI.Styles;

public static class LabelStyles
{
    public static GUIStyle Create()
    {
        return new GUIStyle("Label")
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
        };
    }

    public static GUIStyle CreateArea()
    {
        return new GUIStyle("Box")
        {
            normal = {
                background = Texture.NewBorder(.2f, .2f, .2f, .075f, .075f, .075f),
            },

            border = new RectOffset(1, 1, 1, 1),
        };
    }

    public static GUIStyle CreateTitle()
    {
        return new GUIStyle("Label")
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 13,
        };
    }

    public static GUIStyle CreateWatermark()
    {
        return new GUIStyle("Label")
        {
            wordWrap = false,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
        };
    }

    public static GUIStyle CreateNameTag()
    {
        return new GUIStyle("Label")
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 12,
        };
    }

    public static GUIStyle CreateVersion()
    {
        return new GUIStyle("Box")
        {
            normal = {
                background = Texture.NewTransparent(),
            },

            fontStyle = FontStyle.BoldAndItalic,
            fontSize = 10,
        };
    }
}
