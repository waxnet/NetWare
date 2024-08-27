using System.Runtime.InteropServices;
using UnityEngine;

namespace NetWare
{
    public static class Mouse
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void MoveTo(Vector3 position, float lerp, float xSensitivity, float ySensitivity)
        {
            position.x = (position.x - Render.screenCenter.x) / lerp * xSensitivity;
            position.y = (position.y - Render.screenCenter.y) / lerp * ySensitivity;

            mouse_event(
                0x0001,
                (int)position.x,
                (int)position.y,
                0,
                0
            );
        }
    }
}
