using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;

using UnityEngine;
using Photon.Pun;

namespace NetWare.Modules.LegitModules;

[NetWareComponent]
public sealed class RecoilModifier : MonoBehaviour
{
    public void Update()
    {
        if (
            !Config.Active.RecoilModifier.Enabled ||
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
            "RecoilMultiplier",
            Config.Active.RecoilModifier.Multiplier
        );
    }
}
