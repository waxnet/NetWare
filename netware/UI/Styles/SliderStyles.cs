using UnityEngine;

namespace NetWare.UI.Styles;

public static class SliderStyles
{
    public static GUIStyle CreateHorizontal()
    {
        return new GUIStyle("HorizontalSlider")
        {
            normal = {
                background = Texture.NewBorder(0, 0, 0, .15f, .15f, .15f),
            },

            fixedHeight = 10,
            border = new RectOffset(1, 1, 1, 1),
        };
    }

    public static GUIStyle CreateThumb()
    {
        return new GUIStyle("HorizontalSliderThumb")
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
        };
    }

    public static GUIStyle CreateValueLabel()
    {
        return new GUIStyle("Label")
        {
            fontStyle = FontStyle.Bold,
            fontSize = 13,
            alignment = TextAnchor.MiddleRight,
            contentOffset = new Vector2(-10, 0),
        };
    }

    public static GUIStyle CreateLabel()
    {
        return new GUIStyle("Label")
        {
            fontStyle = FontStyle.Bold,
            fontSize = 13,
            alignment = TextAnchor.MiddleLeft,
            contentOffset = new Vector2(10, 0),
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
}
