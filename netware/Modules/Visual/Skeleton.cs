using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class Skeleton : MonoBehaviour
    {
        public void OnGUI()
        {
            if (!PhotonNetwork.InRoom || !Config.GetBool("visual.skeleton.enabled"))
                return;

            foreach (PlayerController player in Storage.players)
            {
                if (!Players.IsPlayerValid(player))
                    continue;

                // player data
                Animator animator = player.GetComponent<Animator>();
                if (animator == null)
                    continue;

                Color color = Colors.HexToRGB(Config.GetString("visual.skeleton.enemy"));
                if (Players.IsPlayerTeammate(player))
                    color = Colors.HexToRGB(Config.GetString("visual.skeleton.team"));
                else if (Players.IsPlayerBot(player))
                    color = Colors.HexToRGB(Config.GetString("visual.skeleton.bot"));

                // bones
                HumanBodyBones[] spine = NetWare.Skeleton.spine;
                HumanBodyBones[] arms = NetWare.Skeleton.arms;
                HumanBodyBones[] legs = NetWare.Skeleton.legs;

                // spine
                for (int index = 0; (index + 1) != spine.Length; index++)
                {
                    HumanBodyBones originBone = spine[index];
                    HumanBodyBones destinationBone = spine[index + 1];

                    Vector3 originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

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
                for (int index = 0; (index + 1) != arms.Length; index++)
                {
                    HumanBodyBones originBone = arms[index];
                    HumanBodyBones destinationBone = arms[index + 1];

                    Vector3 originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                        Render.DrawLine(color, originPosition, destinationPosition);
                }

                // legs
                for (int index = 0; (index + 1) != legs.Length; index++)
                {
                    HumanBodyBones originBone = legs[index];
                    HumanBodyBones destinationBone = legs[index + 1];

                    Vector3 originPosition = Position.ToScreen(animator.GetBoneTransform(originBone).transform.position);
                    Vector3 destinationPosition = Position.ToScreen(animator.GetBoneTransform(destinationBone).transform.position);

                    if (!Position.IsBehindCamera(originPosition) && !Position.IsBehindCamera(destinationPosition))
                        Render.DrawLine(color, originPosition, destinationPosition);
                }
            }
        }
    }
}
