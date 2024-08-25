using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class Nametags : MonoBehaviour
    {
        public void OnGUI()
        {
            if (!PhotonNetwork.InRoom || !Config.GetBool("visual.nametags.enabled"))
                return;

            foreach (var player in Storage.players)
            {
                if (!Players.IsPlayerValid(player))
                    continue;

                // get and check position
                Vector3 headWorld = Players.GetHeadPosition(player);
                headWorld.y += .22f;
                Vector3 headScreen = Position.ToScreen(headWorld);

                if (Position.IsBehindCamera(headScreen))
                    continue;

                // get name
                string name = Players.GetPlayerName(player);
                if (Players.IsPlayerBot(player))
                    name += " (BOT)";

                // data
                var nameContent = new GUIContent(name);
                var nameStyle = new GUIStyle("Label")
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 12,
                };
                Vector2 nameSize = nameStyle.CalcSize(nameContent);

                if (Players.IsPlayerTeammate(player))
                    nameStyle.normal.textColor = Colors.HexToRGB(Config.GetString("visual.nametags.team"));
                else if (Players.IsPlayerBot(player))
                    nameStyle.normal.textColor = Colors.HexToRGB(Config.GetString("visual.nametags.bot"));
                else
                    nameStyle.normal.textColor = Colors.HexToRGB(Config.GetString("visual.nametags.enemy"));

                // draw name box
                float boxX = headScreen.x;
                float boxY = ((headScreen.y - nameSize.y) + 2);
                float boxSizeX = (nameSize.x + 10);
                float boxSizeY = (nameSize.y + 5);

                Render.DrawBox(
                    Colors.HexToRGB(Config.GetString("visual.nametags.background")),
                    new Vector2(
                        boxX,
                        boxY
                    ),
                    boxSizeX,
                    boxSizeY
                );

                // draw name
                GUI.Label(
                    new Rect(
                        (headScreen.x - (nameSize.x / 2)),
                        (headScreen.y - (nameSize.y * 2) + 7),
                        nameSize.x,
                        (nameSize.y * 1.5f)
                    ),
                    nameContent,
                    nameStyle
                );
            }
        }
    }
}
