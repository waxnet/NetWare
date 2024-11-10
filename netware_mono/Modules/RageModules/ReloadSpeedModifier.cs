using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;

using UnityEngine;
using Photon.Pun;

namespace NetWare.Modules.RageModules;

[NetWareComponent]
public sealed class ReloadSpeedModifier : MonoBehaviour
{
    public void Update()
    {
        if (
            !Config.Active.ReloadSpeedModifier.Enabled ||
            !PhotonNetwork.InRoom ||
            LocalPlayer.Get() is null
            )
            return;

        // set multiplier
        WeaponsController weaponsController = LocalPlayer.GetWeaponsController();
        if (weaponsController is null)
            return;

        Resolver.SetProperty(
            weaponsController,
            "ReloadSpeedMultiplier",
            Config.Active.ReloadSpeedModifier.Multiplier
        );
    }
}
