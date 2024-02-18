using UnityEngine;

namespace NetWare
{
    public class Loader
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
