using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;
using NetWare.Extensions;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.VisualModules;

[NetWareComponent]
public sealed class Tracers : MonoBehaviour
{
    public void OnGUI()
    {
        if (!Config.Active.Tracers.Enabled || !PhotonNetwork.InRoom)
            return;

        foreach (var player in NetWare.Data.Storage.Players)
        {
            if (!player.IsValid())
                continue;

            var screenPosition = Position.ToScreen(Players.GetHipPosition(player));
            
            var color = Colors.HexToRGB(Config.Active.Tracers.EnemyColor);

            if (player.IsTeammate())
                color = Colors.HexToRGB(Config.Active.Tracers.TeamColor);
            else if (player.IsBot())
                color = Colors.HexToRGB(Config.Active.Tracers.BotColor);

            if (!Position.IsBehindCamera(screenPosition))
                Render.DrawLine(
                    color,
                    Render.screenCenterBottom,
                    screenPosition
                );
        }
    }
}
