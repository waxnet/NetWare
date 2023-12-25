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

        public static HumanBodyBones[] arms = {
            HumanBodyBones.RightHand,
            HumanBodyBones.RightLowerArm,
            HumanBodyBones.RightUpperArm,
            HumanBodyBones.RightShoulder,
            HumanBodyBones.UpperChest,
            HumanBodyBones.LeftShoulder,
            HumanBodyBones.LeftUpperArm,
            HumanBodyBones.LeftLowerArm,
            HumanBodyBones.LeftHand
        };

        public static HumanBodyBones[] legs = {
            HumanBodyBones.RightFoot,
            HumanBodyBones.RightLowerLeg,
            HumanBodyBones.RightUpperLeg,
            HumanBodyBones.Hips,
            HumanBodyBones.LeftUpperLeg,
            HumanBodyBones.LeftLowerLeg,
            HumanBodyBones.LeftFoot
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

        public static HumanBodyBones GetBoneFromString(string boneName)
        {
            HumanBodyBones bone;

            switch (boneName)
            {
                // spine
                case "Head":
                    bone = HumanBodyBones.Head;
                    break;
                case "UpperChest":
                    bone = HumanBodyBones.UpperChest;
                    break;
                case "Hips":
                    bone = HumanBodyBones.Hips;
                    break;

                // arms
                case "RightShoulder":
                    bone = HumanBodyBones.RightShoulder;
                    break;
                case "RightUpperArm":
                    bone = HumanBodyBones.RightUpperArm;
                    break;
                case "RightLowerArm":
                    bone = HumanBodyBones.RightLowerArm;
                    break;
                case "RightHand":
                    bone = HumanBodyBones.RightHand;
                    break;
                case "LeftShoulder":
                    bone = HumanBodyBones.LeftShoulder;
                    break;
                case "LeftUpperArm":
                    bone = HumanBodyBones.LeftUpperArm;
                    break;
                case "LeftLowerArm":
                    bone = HumanBodyBones.LeftLowerArm;
                    break;
                case "LeftHand":
                    bone = HumanBodyBones.LeftHand;
                    break;

                // legs
                case "RightUpperLeg":
                    bone = HumanBodyBones.RightUpperLeg;
                    break;
                case "RightLowerLeg":
                    bone = HumanBodyBones.RightLowerLeg;
                    break;
                case "RightFoot":
                    bone = HumanBodyBones.RightFoot;
                    break;
                case "LeftUpperLeg":
                    bone = HumanBodyBones.LeftUpperLeg;
                    break;
                case "LeftLowerLeg":
                    bone = HumanBodyBones.LeftLowerLeg;
                    break;
                case "LeftFoot":
                    bone = HumanBodyBones.LeftFoot;
                    break;

                // default bone
                default:
                    bone = HumanBodyBones.Head;
                    break;
            }

            return bone;
        }
    }
}
