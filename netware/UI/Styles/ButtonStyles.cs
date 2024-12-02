using UnityEngine;

namespace NetWare.UI.Styles;

public static class ButtonStyles
{
    public static GUIStyle Create()
    {
        return new GUIStyle("Box")
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
        };
    }
}
