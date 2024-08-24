using UnityEngine;

namespace NetWare.Modules
{
    public class FPSCapper : MonoBehaviour
    {
        public void Update()
        {
            Application.targetFrameRate = Config.GetInt("settings.fpscapper.fps");
        }
    }
}
