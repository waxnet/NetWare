using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;
using NetWare.Extensions;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.VisualModules;

[NetWareComponent]
public sealed class Skeleton : MonoBehaviour
{
    public void OnGUI()
    {
        if (!Config.Active.Skeleton.Enabled || !PhotonNetwork.InRoom)
            return;

        foreach (var player in NetWare.Data.Storage.Players)
        {
            if (!player.IsValid())
                continue;

            // player data
            var animator = player.GetComponent<Animator>();
            if (animator is null)
                continue;

            var color = Colors.HexToRGB(Config.Active.Skeleton.EnemyColor);
            if (Players.IsPlayerTeammate(player))
                color = Colors.HexToRGB(Config.Active.Skeleton.TeamColor);
            else if (Players.IsPlayerBot(player))
                color = Colors.HexToRGB(Config.Active.Skeleton.BotColor);

            // bones
            var spine = Entities.Skeleton.Spine;
            var arms = Entities.Skeleton.Arms;
            var legs = Entities.Skeleton.Legs;

            // spine
            for (int index = 0; index < spine.Length - 1; index++)
            {
                var originBone = spine[index];
                var destinationBone = spine[index + 1];

                var originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                var destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

                if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                {
                    if ((index + 2) == spine.Length)
                    {
                        originPosition.z = 0;
                        destinationPosition.z = 0;
                        float circleRadius = (originPosition - destinationPosition).magnitude;

                        Render.DrawCircle(color, destinationPosition, circleRadius);
                        continue;
                    }

                    Render.DrawLine(color, originPosition, destinationPosition);
                }
            }

            // arms
            for (int index = 0; index < arms.Length - 1; index++)
            {
                var originBone = arms[index];
                var destinationBone = arms[index + 1];

                var originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                var destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

                if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                    Render.DrawLine(color, originPosition, destinationPosition);
            }

            // legs
            for (int index = 0; index < legs.Length - 1; index++)
            {
                var originBone = legs[index];
                var destinationBone = legs[index + 1];

                var originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                var destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

                if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                    Render.DrawLine(color, originPosition, destinationPosition);
            }
        }
    }
}
