using UnityEngine;

namespace NetWare.UI.Styles;

public static class TitleStyles
{
    public static GUIStyle Create()
    {
        return new GUIStyle("Label")
        {
            normal = {
                textColor = Color.gray,
            },

            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 13,
        };
    }
}
