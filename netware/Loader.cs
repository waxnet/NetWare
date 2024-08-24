using UnityEngine;

namespace NetWare
{
    public static class Loader
    {
        public static void Load()
        {
            // cheat instance
            instance = new GameObject();

            // main class
            instance.AddComponent<Main>();

            // modules
            instance.AddComponent<Modules.Aimbot>();
            instance.AddComponent<Modules.SilentAim>();

            instance.AddComponent<Modules.Nametags>();
            instance.AddComponent<Modules.Boxes>();
            instance.AddComponent<Modules.Skeleton>();
            instance.AddComponent<Modules.Tracers>();
            instance.AddComponent<Modules.Camera>();

            instance.AddComponent<Modules.Watermark>();
            instance.AddComponent<Modules.FPSCapper>();

            instance.AddComponent<MenuChecks>();

            // settings
            Object.DontDestroyOnLoad(instance);
        }

        private static GameObject instance;
    }
}