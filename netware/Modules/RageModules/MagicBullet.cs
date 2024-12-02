using NetWare.Attributes;
using NetWare.Configuration;
using NetWare.Entities;
using NetWare.Enums;
using NetWare.Extensions;

using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules.RageModules;

[NetWareComponent]
public sealed class MagicBullet : MonoBehaviour
{
    public void Update()
    {
        Data.MagicBulletFrameSkips++;
        if (
            Data.MagicBulletFrameSkips <= Config.Active.MagicBullet.FrameSkips ||
            !Config.Active.MagicBullet.Enabled ||
            !PhotonNetwork.InRoom ||
            !Input.GetMouseButton(0) ||
            LocalPlayer.Get() is null
            )
        {
            return;
        } else if (Data.ResetMagicBullet) {
            PlayerController localPlayer = LocalPlayer.Get();
            if (localPlayer is null)
                return;

            // get and check aim position and weapon model
            Vector3? aimPosition_ = LocalPlayer.GetAimPosition();
            WeaponModel weaponModel_ = LocalPlayer.GetWeaponModel();

            if (aimPosition_ is not null && weaponModel_ is not null)
            {
                // get weapon fire origin transform
                Transform weaponFireOrigin = (Transform)Access.GetValue(weaponModel_, "_weaponFireOrigin");

                // edit weapon fire origin position
                weaponFireOrigin.position = Players.GetHeadPosition(localPlayer);

                // set new weapon fire origin transform
                Access.SetValue(weaponModel_, "_weaponFireOrigin", weaponFireOrigin);
            }
            Data.ResetMagicBullet = false;
        }
        Data.MagicBulletFrameSkips = 0;
        Data.ResetMagicBullet = true;

        // get target
        PlayerController target = null;

        var lastDistanceScreen = float.MaxValue;
        var lastDistanceWorld = float.MaxValue;

        var fovSize = Config.Active.MagicBullet.DynamicFov ?
            (Camera.main.fieldOfView + 60) :
            (
                Config.Active.MagicBullet.CheckFov ? Config.Active.MagicBullet.FovSize : Screen.width
            );

        foreach (var player in NetWare.Data.Storage.Players)
        {
            if (player.IsTeammate() || !player.IsValid())
                continue;

            var localPlayer = LocalPlayer.Get();
            var playerWorld = Players.GetHipPosition(player);
            var playerScreen = Position.ToScreen(playerWorld);

            var worldDistance = (Players.GetHipPosition(localPlayer) - playerWorld).magnitude;
            if (worldDistance > Config.Active.MagicBullet.Distance.Maximum || worldDistance < Config.Active.MagicBullet.Distance.Minimum)
                continue;

            var screenDistance = new Vector2(
                    playerScreen.x - Render.screenCenter.x,
                    playerScreen.y - Render.screenCenter.y
                ).magnitude;
            if (!Position.IsOnScreen(playerScreen) || screenDistance > fovSize)
                continue;

            if (Config.Active.MagicBullet.Filter == EAimFilter.FovAndClosest && worldDistance < lastDistanceWorld)
            {
                lastDistanceWorld = worldDistance;
                target = player;
            } else if (Config.Active.MagicBullet.Filter == EAimFilter.Fov && screenDistance < lastDistanceScreen) {
                lastDistanceScreen = screenDistance;
                target = player;
            }
        }
        if (target is null)
            return;

        // get and check aim position and weapon model
        Vector3? aimPosition = LocalPlayer.GetAimPosition();
        WeaponModel weaponModel = LocalPlayer.GetWeaponModel();

        if (aimPosition != null && weaponModel != null)
        {
            // get weapon fire origin transform
            Transform weaponFireOrigin = (Transform)Access.GetValue(weaponModel, "_weaponFireOrigin");

            // edit weapon fire origin position
            Vector3 playerPosition = Players.GetHipPosition(target);
            Vector3 direction = (playerPosition - (Vector3)aimPosition).normalized;
            weaponFireOrigin.position = (playerPosition + direction);

            // set new weapon fire origin transform
            Access.SetValue(weaponModel, "_weaponFireOrigin", weaponFireOrigin);
        }
    }

    public void OnGUI()
    {
        if (!Config.Active.MagicBullet.Enabled || !Config.Active.MagicBullet.CheckFov || !Config.Active.MagicBullet.DrawFov)
            return;

        var fovColor = Colors.HexToRGB(Config.Active.MagicBullet.FovColor);
        if (Config.Active.MagicBullet.RainbowFov)
            fovColor = Colors.GetRainbow();

        if (Config.Active.MagicBullet.DynamicFov)
            Render.DrawCircle(
                fovColor,
                Render.screenCenter,
                Camera.main.fieldOfView + 60,
                Config.Active.MagicBullet.FovSides
            );
        else
            Render.DrawCircle(
                fovColor,
                Render.screenCenter,
                Config.Active.MagicBullet.FovSize,
                Config.Active.MagicBullet.FovSides
            );
    }
}
