using NetWare.Attributes;
using UnityEngine;

namespace NetWare.Enums;

public enum AimBone : byte
{
    [StringReinterpretation("Head")]
    Head = HumanBodyBones.Head,

    [StringReinterpretation("Neck")]
    Neck = HumanBodyBones.Neck,

    [StringReinterpretation("Upper Chest")]
    UpperChest = HumanBodyBones.UpperChest,

    [StringReinterpretation("Hips")]
    Hips = HumanBodyBones.Hips
}
