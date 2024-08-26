using NetWare.Attributes;
using NetWare.Configuration;
using UnityEngine;

namespace NetWare.Modules.SettingsModules;

[NetWareComponent]
public class FPSCapper : MonoBehaviour
{
    public void Update()
    {
        Application.targetFrameRate = Config.Active.FpsCapper.Fps;
    }
}
