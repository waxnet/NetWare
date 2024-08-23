using UnityEngine;

namespace NetWare.Modules
{
    public class Tracers : MonoBehaviour
    {
        public void OnGUI()
        {
            if (!Config.GetBool("visual.tracers.enabled"))
                return;

            foreach (PlayerController player in Storage.players)
            {
                if (!Players.IsPlayerValid(player))
                    continue;

                Vector3 screenPosition = Position.ToScreen(Players.GetHipPosition(player));
                
                Color color = Colors.HexToRGB(Config.GetString("visual.skeleton.enemy"));
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
