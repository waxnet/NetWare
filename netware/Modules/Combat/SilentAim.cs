using JustPlay.FTUE;
using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class SilentAim : MonoBehaviour
    {
        public void Update()
        {
            if (
                !PhotonNetwork.InRoom ||
                !Config.GetBool("combat.silentaim.enabled") ||
                !Input.GetMouseButton(0) ||
                LocalPlayer.Get() == null ||
                !LocalPlayer.IsHoldingWeapon()
                ) return;
            
            // get target
            PlayerController target = null;

            float lastDistanceScreen = float.MaxValue;
            float lastDistanceWorld = float.MaxValue;

            float fov = Config.GetBool("combat.silentaim.dynamicfov") ? (UnityEngine.Camera.main.fieldOfView + 60) : (
                    Config.GetBool("combat.silentaim.checkfov") ? Config.GetInt("combat.silentaim.fovsize") : Screen.width
                );

            foreach (PlayerController player in Storage.players)
                if (!Players.IsPlayerTeammate(player) && Players.IsPlayerValid(player))
                {
                    // player data
                    PlayerController localPlayer = LocalPlayer.Get();
                    
                    Vector3 playerWorld = Players.GetHipPosition(player);
                    Vector3 playerScreen = Position.ToScreen(playerWorld);

                    // distances
                    float worldDistance = (Players.GetHipPosition(localPlayer) - playerWorld).magnitude;
                    if (worldDistance > Config.GetFloat("combat.silentaim.maxdistance", 500))
                        continue;

                    float screenDistance = new Vector2(
                        playerScreen.x - Render.screenCenter.x,
                        playerScreen.y - Render.screenCenter.y
                    ).magnitude;
                    if (!Position.IsOnScreen(playerScreen) || screenDistance > fov)
                        continue;

                    // target selection
                    if (Config.GetString("combat.silentaim.filterby") == "FOV & Closest") {
                        if (worldDistance < lastDistanceWorld)
                        {
                            lastDistanceWorld = worldDistance;
                            target = player;
                        }
                    } else if (screenDistance < lastDistanceScreen) {
                        lastDistanceScreen = screenDistance;
                        target = player;
                    }
                }
            if (target == null)
                return;

            // set rotation
            UnityEngine.Camera.main.transform.LookAt(
                Players.GetBonePosition(
                    target,
                    NetWare.Skeleton.GetBoneFromString(
                        Config.GetString("combat.silentaim.aimbone")
                    )
                )
            );
        }

        public void OnGUI()
        {
            if (Config.GetBool("combat.silentaim.enabled") && Config.GetBool("combat.silentaim.checkfov") && Config.GetBool("combat.silentaim.drawfov"))
            {
                Color fovColor = Colors.HexToRGB(Config.GetString("combat.silentaim.fovcolor"));
                if (Config.GetBool("combat.silentaim.rainbowfov"))
                    fovColor = Colors.GetRainbow();

                if (Config.GetBool("combat.silentaim.dynamicfov"))
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        (UnityEngine.Camera.main.fieldOfView + 60),
                        Config.GetInt("combat.silentaim.fovsides")
                    );
                else
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        Config.GetInt("combat.silentaim.fovsize"),
                        Config.GetInt("combat.silentaim.fovsides")
                    );
            }
        }
    }
}
