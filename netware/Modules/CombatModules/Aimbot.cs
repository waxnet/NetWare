using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;
using NetWare.Enums;
using NetWare.Extensions;
using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.CombatModules;

[NetWareComponent]
public sealed class Aimbot : MonoBehaviour
{
    public void Update()
    {
        if (
            !Config.Active.Aimbot.Enabled ||
            !PhotonNetwork.InRoom ||
            LocalPlayer.Get() is null ||
            !Input.GetMouseButton(1)
            ) return;

        // get target
        PlayerController target = null;

        var lastDistanceScreen = float.MaxValue;
        var lastDistanceWorld = float.MaxValue;

        var fovSize = Config.Active.Aimbot.CheckFov ? Config.Active.Aimbot.FovSize : Screen.width;
        var targetFov = Config.Active.Aimbot.DynamicFov ? Camera.main.fieldOfView + 60 : fovSize;

        foreach (var player in NetWare.Data.Storage.Players)
        {
            if (player.IsTeammate() || !player.IsValid())
                continue;

            var localPlayer = LocalPlayer.Get();
            var playerWorld = Players.GetHipPosition(player);
            var playerScreen = Position.ToScreen(playerWorld);

            var worldDistance = (Players.GetHipPosition(localPlayer) - playerWorld).magnitude;

            if (worldDistance > Config.Active.Aimbot.Distance.Maximum || worldDistance < Config.Active.Aimbot.Distance.Minimum)
                continue;

            var screenDistance = new Vector2(
                    playerScreen.x - Render.screenCenter.x,
                    playerScreen.y - Render.screenCenter.y
                ).magnitude;

            if (!Position.IsOnScreen(playerScreen) || screenDistance > targetFov)
                continue;

            if ((Config.Active.Aimbot.Filter == AimFilter.FovAndClosest && worldDistance < lastDistanceWorld) || screenDistance < lastDistanceScreen)
            {
                lastDistanceWorld = worldDistance;
                target = player;
            }
        }

        if (target is null)
            return;

        // data
        var playerWorldPosition = Players.GetBonePosition(target, (HumanBodyBones)Config.Active.Aimbot.Bone);
        var playerScreenPosition = Position.ToScreen(playerWorldPosition);

        // aim at player
        if (Config.Active.Aimbot.Mode == AimMode.Legit && Position.IsOnScreen(playerScreenPosition))
        {
            Mouse.MoveTo(
                playerScreenPosition,
                Config.Active.Aimbot.Smoothing,
                Config.Active.Aimbot.UseSensitivity ? SettingsPanel.SensitivityX : 1,
                Config.Active.Aimbot.UseSensitivity ? SettingsPanel.SensitivityY : 1
            );
        }
        else if (Config.Active.Aimbot.Mode == AimMode.Lock)
        {
            var camera = LocalPlayer.GetThirdPersonCamera();

            // rotations
            var startRotation = camera.transform.rotation;
            camera.transform.LookAt(playerWorldPosition);
            var endRotation = camera.transform.rotation;

            camera.transform.rotation = startRotation;

            // set rotation
            Camera.main.transform.rotation = endRotation;
            camera.transform.rotation = endRotation;
            camera.SetRotation(endRotation.eulerAngles);
        }
    }

    public void OnGUI()
    {
        if (!Config.Active.Aimbot.Enabled || !Config.Active.Aimbot.CheckFov || !Config.Active.Aimbot.DrawFov)
            return;

        var fovColor = Colors.HexToRGB(Config.Active.Aimbot.FovColor);

        if (Config.Active.Aimbot.RainbowFov)
            fovColor = Colors.GetRainbow();

        if (Config.Active.Aimbot.DynamicFov)
        {
            Render.DrawCircle(
                fovColor,
                Render.screenCenter,
                Camera.main.fieldOfView + 60,
                Config.Active.Aimbot.FovSides
            );
        }
        else
        {
            Render.DrawCircle(
                fovColor,
                Render.screenCenter,
                Config.Active.Aimbot.FovSize,
                Config.Active.Aimbot.FovSides
            );
        }
    }
}
