using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class Tracers : MonoBehaviour
    {
        public void OnGUI()
        {
            if (!PhotonNetwork.InRoom || !Config.GetBool("visual.tracers.enabled"))
                return;

            foreach (var player in Storage.players)
            {
                if (!Players.IsPlayerValid(player))
                    continue;

                Vector3 screenPosition = Position.ToScreen(Players.GetHipPosition(player));

                var color = Colors.HexToRGB(Config.GetString("visual.skeleton.enemy"));
                if (Players.IsPlayerTeammate(player))
                    color = Colors.HexToRGB(Config.GetString("visual.skeleton.team"));
                else if (Players.IsPlayerBot(player))
                    color = Colors.HexToRGB(Config.GetString("visual.skeleton.bot"));

                if (!Position.IsBehindCamera(screenPosition))
                    Render.DrawLine(
                        color,
                        Render.screenCenterBottom,
                        screenPosition
                    );
            }
        }
    }
}
