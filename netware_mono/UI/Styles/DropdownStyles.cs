using UnityEngine;

namespace NetWare.UI.Styles;

public static class DropdownStyles
{
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

    public static GUIStyle CreateLabel()
    {
        return new GUIStyle("Label")
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 13,
        };
    }
    
    public static GUIStyle CreateButton()
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
