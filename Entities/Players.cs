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

        public static Color GetPlayerTeamColor(PlayerController playerController)
        {
            Color color = Color.red;
            if (playerController?.FJLDBODHGKB ?? true)
            {
                color = Color.white;
            } else if (playerController?.FDBLGDDCJED ?? true) {
                color = Color.green;
            }

            return color;
        }

        public static bool IsPlayerAlive(PlayerController playerController)
        {
            return playerController?.MLCGAAINICC?.KillerId == null;
        }
    }
}
