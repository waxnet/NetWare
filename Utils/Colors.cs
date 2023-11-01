using UnityEngine;

namespace NetWare
{
    public class Colors
    {
        public static Color GetRainbow()
        {
            return Color.HSVToRGB(Time.time % 1f, 1f, 1f);
        }
    }
}
