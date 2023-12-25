using System;
using System.Reflection;
using UnityEngine;

namespace NetWare
{
    public class Players : MonoBehaviour
    {
        public static Vector3 GetHeadPosition(PlayerController playerController)
        {
            Animator playerAnimator = playerController.GetComponent<Animator>();

            return playerAnimator.GetBoneTransform(HumanBodyBones.Head).transform.position;
        }
        
        public static Vector3 GetHipPosition(PlayerController playerController)
        {
            Animator playerAnimator = playerController.GetComponent<Animator>();

            return playerAnimator.GetBoneTransform(HumanBodyBones.Hips).transform.position;
        }

        public static Vector3 GetFeetPosition(PlayerController playerController)
        {
            Animator playerAnimator = playerController.GetComponent<Animator>();

            Vector3 rightFoot = playerAnimator.GetBoneTransform(HumanBodyBones.RightFoot).transform.position;
            Vector3 leftFoot = playerAnimator.GetBoneTransform(HumanBodyBones.LeftFoot).transform.position;

            return Vector3.Lerp(rightFoot, leftFoot, .5f);
        }

        public static Vector3 GetBonePosition(PlayerController playerController, HumanBodyBones bone)
        {
            Animator playerAnimator = playerController.GetComponent<Animator>();

            return playerAnimator.GetBoneTransform(bone).transform.position;
        }

        public static bool IsPlayerTeammate(PlayerController playerController)
        {
            return playerController?.IFAAIDGNPOL ?? true;
        }

        public static bool IsPlayerValid(PlayerController playerController)
        {
            return !playerController.IsMine() && IsPlayerAlive(playerController) && Skeleton.HasSkeleton(playerController);
        }

        public static bool IsPlayerAlive(PlayerController playerController)
        {
            return playerController?.PAEKEKFLHOK?.KillerId == null;
        }

        public static EOLCHOFICLK GetPlayerInfo(PlayerController playerController)
        {
            return playerController?.PBIJHIGLJEM;
        }

        public static string GetPlayerName(PlayerController playerController)
        {
            return GetPlayerInfo(playerController)?.JBNPFKLFFCH;
        }

        public static int? GetPlayerRankXP(PlayerController playerController)
        {
            return GetPlayerInfo(playerController)?.PDNGCJKAPNC;
        }

        public static double GetPlayerDistance(PlayerController playerController)
        {
            return Math.Round((LocalPlayer.Get().transform.position - playerController.transform.position).magnitude, 1);
        }

        public static PlayerHealth GetHealth(PlayerController playerController)
        {
            return playerController?.PAEKEKFLHOK;
        }
    }
}
