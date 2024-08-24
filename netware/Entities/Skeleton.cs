using System.Collections.Generic;
using System;
using UnityEngine;

namespace NetWare
{
    public static class Skeleton
    {
        // body parts
        public static HumanBodyBones[] spine = {
            HumanBodyBones.Hips,
            HumanBodyBones.UpperChest,
            HumanBodyBones.Neck,
            HumanBodyBones.Head
        };

        public static HumanBodyBones[] arms = {
            HumanBodyBones.RightHand,
            HumanBodyBones.RightLowerArm,
            HumanBodyBones.RightUpperArm,
            HumanBodyBones.UpperChest,
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
            HumanBodyBones.Neck,
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
            Animator playerAnimator = playerController?.GetComponent<Animator>();
            bool hasSkeleton = true;

            foreach (HumanBodyBones bone in body)
                if (playerAnimator?.GetBoneTransform(bone) == null)
                {
                    hasSkeleton = false;
                    break;
                }

            return hasSkeleton;
        }

        public static HumanBodyBones GetBoneFromString(string boneName)
        {
            return new Dictionary<string, HumanBodyBones>(StringComparer.OrdinalIgnoreCase)
            {
                { "Head", HumanBodyBones.Head },
                { "Neck", HumanBodyBones.Neck },
                { "UpperChest", HumanBodyBones.UpperChest },
                { "Hips", HumanBodyBones.Hips },
                { "RightShoulder", HumanBodyBones.RightShoulder },
                { "RightUpperArm", HumanBodyBones.RightUpperArm },
                { "RightLowerArm", HumanBodyBones.RightLowerArm },
                { "RightHand", HumanBodyBones.RightHand },
                { "LeftShoulder", HumanBodyBones.LeftShoulder },
                { "LeftUpperArm", HumanBodyBones.LeftUpperArm },
                { "LeftLowerArm", HumanBodyBones.LeftLowerArm },
                { "LeftHand", HumanBodyBones.LeftHand },
                { "RightUpperLeg", HumanBodyBones.RightUpperLeg },
                { "RightLowerLeg", HumanBodyBones.RightLowerLeg },
                { "RightFoot", HumanBodyBones.RightFoot },
                { "LeftUpperLeg", HumanBodyBones.LeftUpperLeg },
                { "LeftLowerLeg", HumanBodyBones.LeftLowerLeg },
                { "LeftFoot", HumanBodyBones.LeftFoot }
            
            }.TryGetValue(boneName, out HumanBodyBones bone) ? bone : HumanBodyBones.Head;
        }
    }
}
