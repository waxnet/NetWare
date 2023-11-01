using UnityEngine;

namespace NetWare
{
    public class Skeleton
    {
        // body parts
        public static HumanBodyBones[] spine = {
            HumanBodyBones.Hips,
            HumanBodyBones.UpperChest,
            HumanBodyBones.Head
        };

        public static HumanBodyBones[] rightArm = {
            HumanBodyBones.UpperChest,
            HumanBodyBones.RightShoulder,
            HumanBodyBones.RightUpperArm,
            HumanBodyBones.RightLowerArm,
            HumanBodyBones.RightHand
        };

        public static HumanBodyBones[] leftArm = {
            HumanBodyBones.UpperChest,
            HumanBodyBones.LeftShoulder,
            HumanBodyBones.LeftUpperArm,
            HumanBodyBones.LeftLowerArm,
            HumanBodyBones.LeftHand
        };

        public static HumanBodyBones[] rightLeg = {
            HumanBodyBones.RightFoot,
            HumanBodyBones.RightLowerLeg,
            HumanBodyBones.RightUpperLeg,
            HumanBodyBones.Hips
        };

        public static HumanBodyBones[] leftLeg = {
            HumanBodyBones.LeftFoot,
            HumanBodyBones.LeftLowerLeg,
            HumanBodyBones.LeftUpperLeg,
            HumanBodyBones.Hips
        };

        // full body
        public static HumanBodyBones[] body = {
            HumanBodyBones.Head,
            HumanBodyBones.UpperChest,
            HumanBodyBones.Hips,

            HumanBodyBones.RightShoulder,
            HumanBodyBones.RightUpperArm,
            HumanBodyBones.RightLowerArm,
            HumanBodyBones.RightHand,

            HumanBodyBones.LeftShoulder,
            HumanBodyBones.LeftUpperArm,
            HumanBodyBones.LeftLowerArm,
            HumanBodyBones.LeftHand,

            HumanBodyBones.RightFoot,
            HumanBodyBones.RightLowerLeg,
            HumanBodyBones.RightUpperLeg,

            HumanBodyBones.LeftFoot,
            HumanBodyBones.LeftLowerLeg,
            HumanBodyBones.LeftUpperLeg,
    };

        // methods
        public static bool HasSkeleton(PlayerController playerController)
        {
            Animator playerAnimator = playerController.GetComponent<Animator>();
            bool hasSkeleton = true;

            foreach (HumanBodyBones bone in body)
            {
                if (playerAnimator.GetBoneTransform(bone) == null)
                {
                    hasSkeleton = false;
                    break;
                }
            }

            return hasSkeleton;
        }
    }
}
