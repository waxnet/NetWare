using NetWare.Configuration;
using UnityEngine;

namespace NetWare.Modules.SettingsModules;

public sealed class FPSCapper : MonoBehaviour
{
    public void Update()
    {
        Application.targetFrameRate = Config.Active.FpsCapper.Fps;
    }
}
