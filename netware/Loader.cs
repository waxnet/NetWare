using UnityEngine;

namespace NetWare
{
    public static class Loader
    {
        public static void Load()
        {
            instance = new GameObject();
            instance.AddComponent<Main>();
            Object.DontDestroyOnLoad(instance);
        }

        private static GameObject instance;
    }
}
