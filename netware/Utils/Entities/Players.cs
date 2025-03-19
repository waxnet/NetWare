using UnityEngine;

namespace NetWare.Entities;

public static class Players
{
    public static Vector3 GetHeadPosition(PlayerController playerController) => GetBonePosition(playerController, HumanBodyBones.Head);

    public static Vector3 GetHipPosition(PlayerController playerController) => GetBonePosition(playerController, HumanBodyBones.Hips);

    public static Vector3 GetFeetPosition(PlayerController playerController)
    {
        var rightFoot = GetBonePosition(playerController, HumanBodyBones.RightFoot);
        var leftFoot = GetBonePosition(playerController, HumanBodyBones.LeftFoot);

        return Vector3.Lerp(rightFoot, leftFoot, .5f);
    }

    public static Vector3 GetBonePosition(PlayerController playerController, HumanBodyBones bone)
    {
        var playerAnimator = playerController.GetComponent<Animator>();
        return playerAnimator.GetBoneTransform(bone).transform.position;
    }

    public static bool IsPlayerTeammate(PlayerController playerController)
    {
        return Resolver.GetProperty<PlayerController, bool>(playerController, "IsTeammate");
    }

    public static bool IsPlayerBot(PlayerController playerController)
    {
        return Resolver.GetProperty<PlayerController, bool>(playerController, "IsBot");
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
        return GetHealth(playerController)?.KillerId is null;
    }

    public static InGamePlayerInfo GetPlayerInfo(PlayerController playerController)
    {
        return Resolver.GetProperty<PlayerController, InGamePlayerInfo>(playerController, "PlayerInfo");
    }

    public static string GetPlayerName(PlayerController playerController)
    {
        try {
            return GetPlayerInfo(playerController).Nickname;
        } catch {
            return "Player";
        }
    }

    public static PlayerHealth GetHealth(PlayerController playerController)
    {
        return Resolver.GetProperty<PlayerController, PlayerHealth>(playerController, "Health");
    }
}
