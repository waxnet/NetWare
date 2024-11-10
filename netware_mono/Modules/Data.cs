using UnityEngine;

namespace NetWare.Modules;

public static class Data
{
    public static bool ResetFov { get; set; }
    public static bool ResetZoom { get; set; }
    public static string SelectedConfig { get; set; }
    public static int MagicBulletFrameSkips { get; set; }
    public static bool ResetMagicBullet { get; set; }
    public static Vector3 playerPosition { get; set; } = default;
}
