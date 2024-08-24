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
            try {
                return playerController.JDJGEBHGLMI;
            } catch {
                return Resolver.GetProperty<PlayerController, bool>(playerController, "IsTeammate");
            }
        }

        public static bool IsPlayerBot(PlayerController playerController)
        {
            try {
                return playerController.OAHKGPBBLCP;
            } catch {
                return Resolver.GetProperty<PlayerController, bool>(playerController, "IsBot");
            }
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

        public static BABFPNELLFA GetPlayerInfo(PlayerController playerController)
        {
            try {
                return playerController.HGJCDHLIOII;
            } catch {
                return null;
            }
        }

        public static string GetPlayerName(PlayerController playerController)
        {
            try {
                return GetPlayerInfo(playerController).GCCNGNHHLMC;
            } catch {
                return "Player";
            }
        }

        public static PlayerHealth GetHealth(PlayerController playerController)
        {
            try {
                return playerController.LFMCIILGNAJ;
            } catch {
                return Resolver.GetProperty<PlayerController, PlayerHealth>(playerController, "Health");
            }
        }
    }
}
