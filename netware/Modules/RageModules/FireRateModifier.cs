using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;

using UnityEngine;
using Photon.Pun;

namespace NetWare.Modules.RageModules;

[NetWareComponent]
public sealed class FireRateModifier : MonoBehaviour
{
    public void Update()
    {
        if (
            !Config.Active.FireRateModifier.Enabled ||
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
            "FireRateMultiplier",
            Config.Active.FireRateModifier.Multiplier
        );
    }
}
