﻿using System;
using UnityEngine;

namespace NetWare
{
    public static class Players
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
            return playerController?.DJLPPEAIEPN ?? true;
        }

        public static bool IsPlayerBot(PlayerController playerController)
        {
            return playerController?.GOMDJKNPEME ?? true;
        }
        
        public static bool IsPlayerValid(PlayerController playerController)
        {
            try {
                return !playerController.IsMine() && IsPlayerAlive(playerController) && Skeleton.HasSkeleton(playerController);
            } catch {
                return false;
            }
        }

        public static bool IsPlayerAlive(PlayerController playerController)
        {
            return GetHealth(playerController)?.KillerId == null;
        }

        public static BNCCCAFHJBP GetPlayerInfo(PlayerController playerController)
        {
            return playerController?.ADOEBLGNHKP;
        }

        public static string GetPlayerName(PlayerController playerController)
        {
            return GetPlayerInfo(playerController)?.CDFCGBAEILF;
        }

        public static int? GetPlayerRankXP(PlayerController playerController)
        {
            return GetPlayerInfo(playerController)?.OIDEKJLLEAF;
        }

        public static double GetPlayerDistance(PlayerController playerController)
        {
            return Math.Round((LocalPlayer.Get().transform.position - playerController.transform.position).magnitude, 1);
        }

        public static PlayerHealth GetHealth(PlayerController playerController)
        {
            return playerController?.MGDFNIKPEDA;
        }
    }
}
