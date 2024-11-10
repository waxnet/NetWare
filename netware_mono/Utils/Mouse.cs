using System.Runtime.InteropServices;
using UnityEngine;

namespace NetWare;

public static class Mouse
{
    // imports
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

    // methods
    public static void MoveTo(Vector3 position, float lerp, float xSensitivity, float ySensitivity)
    {
        // calculate positions
        position.x = (position.x - Render.screenCenter.x) / lerp * xSensitivity;
        position.y = (position.y - Render.screenCenter.y) / lerp * ySensitivity;

        // move mouse
        mouse_event(
            0x0001,
            (int)position.x,
            (int)position.y,
            0,
            0
        );
    }
}
