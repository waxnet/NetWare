﻿using UnityEngine;

namespace NetWare
{
    public static class Colors
    {
        public static Color GetRainbow(float offset = 0)
        {
            float time = ((Time.time + offset) * 2);

            float red = ((Mathf.Sin(time) * .5f) + .5f);
            float green = ((Mathf.Sin(time + 2) * .5f) + .5f);
            float blue = ((Mathf.Sin(time + 4) * .5f) + .5f);

            return new Color(red, green, blue);
        }

        public static Color HexToRGBAA(string hexColor)
        {
            if (hexColor.Length != 7)
                return Color.white;

            if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
                return color;
            return Color.white;
        }

        public static Color HexToRGB(string hexColor)
        {
            if (hexColor.Length != 7)
                return Color.white;

            if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
                return color;
            return Color.white;
        }

        public static Color GetPlayerTeamColor(PlayerController playerController)
        {
            Color color = HexToRGB(Config.GetString("visual.esp.enemycolor"));

            if (Players.IsPlayerBot(playerController)) {
                color = HexToRGB(Config.GetString("visual.esp.botcolor"));
            } else if (Players.IsPlayerTeammate(playerController)) {
                color = HexToRGB(Config.GetString("visual.esp.teammatecolor"));
            }

            return color;
        }
    }
}
