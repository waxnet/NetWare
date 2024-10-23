using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.RageModules;

[NetWareComponent]
public sealed class SpeedModifier : MonoBehaviour
{
    public void Update()
    {
        var localPlayer = LocalPlayer.Get();

        if (
            !Config.Active.SpeedModifier.Enabled ||
            !PhotonNetwork.InRoom ||
            localPlayer is null
            )
            return;

        // set multiplier
        Resolver.SetProperty(
            localPlayer,
            "PlayerSpeedMultiplier",
            Config.Active.SpeedModifier.Multiplier
        );
    }
}
