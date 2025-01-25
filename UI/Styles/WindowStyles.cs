using UnityEngine;

namespace NetWare.UI.Styles;

public static class WindowStyles
{
    public static GUIStyle Create(string version, Color backgroundColor)
    {
        return new GUIStyle("Box")
        {
            normal = {
                background = Texture.NewBorder(backgroundColor.r, backgroundColor.g, backgroundColor.b, .075f, .075f, .075f),
            },

            fontStyle = FontStyle.BoldAndItalic,
            fontSize = 22,
            border = new RectOffset(1, 1, 1, 1),
            contentOffset = version is null ? new Vector2(10, 6) : Vector2.zero,
            alignment = TextAnchor.UpperLeft
        };
    }
}
