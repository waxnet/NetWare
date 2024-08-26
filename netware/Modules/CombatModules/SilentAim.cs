using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;
using NetWare.Enums;
using NetWare.Extensions;
using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.CombatModules;

[NetWareComponent]
public class SilentAim : MonoBehaviour
{
    public void Update()
    {
        if (
            !Config.Active.SilentAim.Enabled ||
            !PhotonNetwork.InRoom ||
            LocalPlayer.Get() == null ||
            !LocalPlayer.IsHoldingWeapon() ||
            !Input.GetMouseButton(0)
            ) return;
        
        // get target
        PlayerController target = null;

        float lastDistanceScreen = float.MaxValue;
        float lastDistanceWorld = float.MaxValue;

        var fovSize = Config.Active.SilentAim.CheckFov ? Config.Active.SilentAim.FovSize : Screen.width;
        var targetFov = Config.Active.SilentAim.DynamicFov ? Camera.main.fieldOfView + 60 : fovSize;

        foreach (var player in NetWare.Data.Storage.Players)
        {
            if (player.IsTeammate() || !player.IsValid())
                continue;

            var localPlayer = LocalPlayer.Get();
            var playerWorld = Players.GetHipPosition(player);
            var playerScreen = Position.ToScreen(playerWorld);

            var worldDistance = (Players.GetHipPosition(localPlayer) - playerWorld).magnitude;

            if (worldDistance > Config.Active.SilentAim.Distance.Maximum || worldDistance < Config.Active.SilentAim.Distance.Minimum)
                continue;

            var screenDistance = new Vector2(
                    playerScreen.x - Render.screenCenter.x,
                    playerScreen.y - Render.screenCenter.y
                ).magnitude;

            if (!Position.IsOnScreen(playerScreen) || screenDistance > targetFov)
                continue;

            if ((Config.Active.SilentAim.Filter == AimFilter.FovAndClosest && worldDistance < lastDistanceWorld) || screenDistance < lastDistanceScreen)
            {
                lastDistanceWorld = worldDistance;
                target = player;
            }
        }

        if (target is null)
            return;

        // set rotation
        Camera.main.transform.LookAt(
            Players.GetBonePosition(
                target,
                (HumanBodyBones)Config.Active.SilentAim.Bone
            )
        );
    }

    public void OnGUI()
    {
        if (!Config.Active.SilentAim.Enabled || !Config.Active.SilentAim.CheckFov || !Config.Active.SilentAim.DrawFov)
            return;

        var fovColor = Colors.HexToRGB(Config.Active.SilentAim.FovColor);

        if (Config.Active.SilentAim.RainbowFov)
            fovColor = Colors.GetRainbow();

        if (Config.Active.SilentAim.DynamicFov)
        {
            Render.DrawCircle(
                fovColor,
                Render.screenCenter,
                Camera.main.fieldOfView + 60,
                Config.Active.SilentAim.FovSides
            );
        }
        else
        {
            Render.DrawCircle(
                fovColor,
                Render.screenCenter,
                Config.Active.SilentAim.FovSize,
                Config.Active.SilentAim.FovSides
            );
        }
    }
}
