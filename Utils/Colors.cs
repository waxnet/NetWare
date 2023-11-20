using UnityEngine;

namespace NetWare
{
    public class Colors : MonoBehaviour
    {
        public static Color HexToRGB(string hexColor)
        {
            if (hexColor.Length != 7)
            {
                return Color.white;
            }

            if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
            {
                return color;
            }
            return Color.white;
        }
    }
}
