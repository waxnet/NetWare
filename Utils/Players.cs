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

        public static Color GetPlayerTeamColor(PlayerController playerController)
        {
            Color color = Color.red;
            if (playerController?.EBPEIGIEEIF ?? true)
            {
                color = Color.gray;
            } else if (playerController?.BODBNDGJCLF ?? true)
            {
                color = Color.green;
            }

            return color;
        }

        public static bool IsPlayerAlive(PlayerController playerController)
        {
            return playerController?.ABDABPEKBFM?.KillerId == null;
        }
    }
}
