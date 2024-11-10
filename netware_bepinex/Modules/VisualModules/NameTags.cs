using NetWare.Configuration;
using NetWare.Entities;
using NetWare.Extensions;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.VisualModules;

public sealed class NameTags : MonoBehaviour
{
    public void OnGUI()
    {
        if (!Config.Active.NameTags.Enabled || !PhotonNetwork.InRoom)
            return;

        foreach (var player in NetWare.Data.Storage.Players)
        {
            if (!player.IsValid())
                continue;

            var headWorld = Players.GetHeadPosition(player);
            headWorld.y += .22f;
            var headScreen = Position.ToScreen(headWorld);

            if (Position.IsBehindCamera(headScreen))
                continue;

            // get name
            var name = "Player";
            if (Players.IsPlayerBot(player))
                name += " (BOT)";

            // data
            var nameContent = new GUIContent(name);
            var textStyle = GUI.skin.label;
            var nameSize = textStyle.CalcSize(nameContent);

            textStyle.normal.textColor = Colors.HexToRGB(Config.Active.NameTags.EnemyColor);

            if (player.IsTeammate())
                textStyle.normal.textColor = Colors.HexToRGB(Config.Active.NameTags.TeamColor);
            else if (player.IsBot())
                textStyle.normal.textColor = Colors.HexToRGB(Config.Active.NameTags.BotColor);

            // draw name box
            float boxX = headScreen.x;
            float boxY = headScreen.y - nameSize.y + 2;
            float boxSizeX = nameSize.x + 10;
            float boxSizeY = nameSize.y + 5;

            Render.DrawBox(
                Colors.HexToRGB(Config.Active.NameTags.BackgroundColor),
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
                    headScreen.x - (nameSize.x / 2),
                    headScreen.y - (nameSize.y * 2) + 7,
                    nameSize.x,
                    nameSize.y * 1.5f
                ),
                nameContent,
                textStyle
            );
        }
    }
}
