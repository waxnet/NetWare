using System.Runtime.InteropServices;
using UnityEngine;

namespace NetWare
{
    public static class Mouse
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void MoveTo(Vector3 position, int lerp = 1)
        {
            position.x = ((position.x - Render.screenCenter.x) / lerp);
            position.y = ((position.y - Render.screenCenter.y) / lerp);

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
