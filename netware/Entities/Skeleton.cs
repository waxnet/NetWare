using System.Collections.Generic;
using System;
using UnityEngine;

namespace NetWare.Entities;

public static class Skeleton
{
    // body parts
    public static HumanBodyBones[] Spine { get; } = {
        HumanBodyBones.Hips,
        HumanBodyBones.UpperChest,
        HumanBodyBones.Neck,
        HumanBodyBones.Head
    };

    public static HumanBodyBones[] Arms { get; } = {
        HumanBodyBones.RightHand,
        HumanBodyBones.RightLowerArm,
        HumanBodyBones.RightUpperArm,
        HumanBodyBones.UpperChest,
        HumanBodyBones.LeftUpperArm,
        HumanBodyBones.LeftLowerArm,
        HumanBodyBones.LeftHand
    };

    public static HumanBodyBones[] Legs { get; } = {
        HumanBodyBones.RightFoot,
        HumanBodyBones.RightLowerLeg,
        HumanBodyBones.RightUpperLeg,
        HumanBodyBones.Hips,
        HumanBodyBones.LeftUpperLeg,
        HumanBodyBones.LeftLowerLeg,
        HumanBodyBones.LeftFoot
    };

    // full body
    public static HumanBodyBones[] Body { get; } = {
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

        foreach (HumanBodyBones bone in Body)
            if (playerAnimator?.GetBoneTransform(bone) == null)
            {
                hasSkeleton = false;
                break;
            }

        return hasSkeleton;
    }
}
