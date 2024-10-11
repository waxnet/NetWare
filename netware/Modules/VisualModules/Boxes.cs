using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;
using NetWare.Extensions;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.VisualModules;

[NetWareComponent]
public sealed class Boxes : MonoBehaviour
{
    public void OnGUI()
    {
        if (!Config.Active.Boxes.Enabled || !PhotonNetwork.InRoom)
            return;

        foreach (var player in NetWare.Data.Storage.Players)
        {
            if (!player.IsValid())
                continue;

            // get color
            var color = Colors.HexToRGB(Config.Active.Boxes.EnemyColor);

            if (player.IsTeammate())
                color = Colors.HexToRGB(Config.Active.Boxes.TeamColor);
            else if (player.IsBot())
                color = Colors.HexToRGB(Config.Active.Boxes.BotColor);

            // get head and feet world position
            var headWorld = Players.GetHeadPosition(player);
            var feetWorld = Players.GetFeetPosition(player);

            if (headWorld.y > feetWorld.y) {
                headWorld.y += .22f;
                feetWorld.y -= .2f;
            } else {
                headWorld.y -= .22f;
                feetWorld.y += .2f;
            }

            // get positions
            var headScreen = Position.ToScreen(headWorld);
            var feetScreen = Position.ToScreen(feetWorld);

            // check positions
            if (!Position.IsBehindCamera(headScreen) && !Position.IsBehindCamera(feetScreen))
            {
                // get box position
                float boxX = headScreen.x + (feetScreen.x - headScreen.x) * .5f;
                float boxY = headScreen.y + (feetScreen.y - headScreen.y) * .5f;

                // box size
                float boxHeightA = Mathf.Abs(headScreen.x - feetScreen.x) / 2;
                float boxHeightB = Mathf.Abs(headScreen.y - feetScreen.y) / 2;

                float boxWidth = boxHeightB / 1.5f;
                float boxHeight = boxHeightB;

                if (boxHeightA > boxWidth)
                {
                    boxWidth = boxHeightA;
                    boxHeight = boxHeightA / 1.5f;
                }

                // corners
                var topLeft = new Vector3(boxX - boxWidth, boxY - boxHeight, 0);
                var topRight = new Vector3(boxX + boxWidth, boxY - boxHeight, 0);

                var bottomLeft = new Vector3(boxX - boxWidth, boxY + boxHeight, 0);
                var bottomRight = new Vector3(boxX + boxWidth, boxY + boxHeight, 0);

                // draw box
                Render.DrawLine(color, topLeft, topRight);
                Render.DrawLine(color, topRight, bottomRight);
                Render.DrawLine(color, bottomRight, bottomLeft);
                Render.DrawLine(color, bottomLeft, topLeft);
            }
        }
    }
}
