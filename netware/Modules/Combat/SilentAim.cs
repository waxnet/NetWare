using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class SilentAim : MonoBehaviour
    {
        public void Update()
        {
            if (!PhotonNetwork.InRoom || (!(Input.GetMouseButton(0) && Config.GetBool("combat.silentaim.enabled") && LocalPlayer.IsHoldingWeapon())))
                return;
            
            // get target
            PlayerController target = null;
            float lastDistance = Config.GetFloat("combat.silentaim.distance", 500);

            foreach (PlayerController player in Storage.players)
                if (!Players.IsPlayerTeammate(player) && Players.IsPlayerValid(player))
                {
                    Vector3 playerWorld = Players.GetHeadPosition(player);
                    Vector3 playerScreen = Position.ToScreen(playerWorld);

                    if (Position.IsOnScreen(playerScreen))
                    {
                        float screenDistance = new Vector2(
                            playerScreen.x - Render.screenCenter.x,
                            playerScreen.y - Render.screenCenter.y
                        ).magnitude;

                        if (screenDistance < lastDistance)
                        {
                            lastDistance = screenDistance;
                            target = player;
                        }
                    }
                }

            float fov = Screen.width;
            if (Config.GetBool("combat.silentaim.dynamicfov"))
                fov = (UnityEngine.Camera.main.fieldOfView + 60);
            else if (Config.GetBool("combat.silentaim.checkfov"))
                fov = Config.GetInt("combat.silentaim.fovsize");

            if (lastDistance > fov || target == null)
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
