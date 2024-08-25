using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class Boxes : MonoBehaviour
    {
        public void OnGUI()
        {
            if (!PhotonNetwork.InRoom || !Config.GetBool("visual.boxes.enabled"))
                return;

            foreach (var player in Storage.players)
            {
                if (!Players.IsPlayerValid(player))
                    continue;

                // get color
                var color = Colors.HexToRGB(Config.GetString("visual.boxes.enemy"));
                if (Players.IsPlayerTeammate(player))
                    color = Colors.HexToRGB(Config.GetString("visual.boxes.team"));
                else if (Players.IsPlayerBot(player))
                    color = Colors.HexToRGB(Config.GetString("visual.boxes.bot"));

                // get head and feet world position
                Vector3 headWorld = Players.GetHeadPosition(player);
                Vector3 feetWorld = Players.GetFeetPosition(player);

                if (headWorld.y > feetWorld.y) {
                    headWorld.y += .22f;
                    feetWorld.y -= .2f;
                } else {
                    headWorld.y -= .22f;
                    feetWorld.y += .2f;
                }

                // get positions
                Vector3 headScreen = Position.ToScreen(headWorld);
                Vector3 feetScreen = Position.ToScreen(feetWorld);

                // check positions
                if (!Position.IsBehindCamera(headScreen) && !Position.IsBehindCamera(feetScreen))
                {
                    // get box position
                    float boxX = headScreen.x + (feetScreen.x - headScreen.x) * .5f;
                    float boxY = headScreen.y + (feetScreen.y - headScreen.y) * .5f;

                    // box size
                    float boxHeightA = (Mathf.Abs(headScreen.x - feetScreen.x) / 2);
                    float boxHeightB = (Mathf.Abs(headScreen.y - feetScreen.y) / 2);

                    float boxWidth = (boxHeightB / 1.5f);
                    float boxHeight = boxHeightB;

                    if (boxHeightA > boxWidth)
                    {
                        boxWidth = boxHeightA;
                        boxHeight = (boxHeightA / 1.5f);
                    }

                    // corners
                    Vector3 topLeft = new Vector3(boxX - boxWidth, boxY - boxHeight, 0);
                    Vector3 topRight = new Vector3(boxX + boxWidth, boxY - boxHeight, 0);

                    Vector3 bottomLeft = new Vector3(boxX - boxWidth, boxY + boxHeight, 0);
                    Vector3 bottomRight = new Vector3(boxX + boxWidth, boxY + boxHeight, 0);

                    // draw box
                    Render.DrawLine(color, topLeft, topRight);
                    Render.DrawLine(color, topRight, bottomRight);
                    Render.DrawLine(color, bottomRight, bottomLeft);
                    Render.DrawLine(color, bottomLeft, topLeft);
                }
            }
        }
    }
}
