using Photon.Pun;
using UnityEngine;

namespace NetWare.Modules
{
    public class Aimbot : MonoBehaviour
    {
        public void Update()
        {
            if (!PhotonNetwork.InRoom || (!(Input.GetMouseButton(1) && Config.GetBool("combat.aimbot.enabled"))))
                return;

            // get target
            PlayerController target = null;
            float lastDistance = Config.GetFloat("combat.aimbot.distance", 500);

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
            if (Config.GetBool("combat.aimbot.dynamicfov"))
                fov = (UnityEngine.Camera.main.fieldOfView + 60);
            else if (Config.GetBool("combat.aimbot.checkfov"))
                fov = Config.GetInt("combat.aimbot.fovsize");

            if (lastDistance > fov || target == null)
                return;

            // data
            Vector3 playerWorldPosition = Players.GetBonePosition(
                target,
                NetWare.Skeleton.GetBoneFromString(Config.GetString("combat.aimbot.aimbone"))
            );
            Vector3 playerScreenPosition = Position.ToScreen(playerWorldPosition);
            string aimbotAimMode = Config.GetString("combat.aimbot.aimmode");

            // aim at player
            if (aimbotAimMode == "Legit" && Position.IsOnScreen(playerScreenPosition))
            {
                if (Config.GetBool("combat.aimbot.usesensitivity"))
                    Mouse.MoveTo(
                        playerScreenPosition,
                        Config.GetInt("combat.aimbot.smoothing"),
                        SettingsPanel.SensitivityX,
                        SettingsPanel.SensitivityY
                    );
                else
                    Mouse.MoveTo(
                        playerScreenPosition,
                        Config.GetInt("combat.aimbot.smoothing")
                    );
            }

            if (aimbotAimMode == "Lock")
            {
                vThirdPersonCamera camera = LocalPlayer.GetThirdPersonCamera();

                // rotations
                Quaternion startRotation = camera.transform.rotation;
                camera.transform.LookAt(playerWorldPosition);
                Quaternion endRotation = camera.transform.rotation;
                
                camera.transform.rotation = startRotation;

                // set rotation
                UnityEngine.Camera.main.transform.rotation = endRotation;
                camera.transform.rotation = endRotation;
                camera.SetRotation(endRotation.eulerAngles);
            }
        }

        public void OnGUI()
        {
            if (Config.GetBool("combat.aimbot.enabled") && Config.GetBool("combat.aimbot.checkfov") && Config.GetBool("combat.aimbot.drawfov"))
            {
                Color fovColor = Colors.HexToRGB(Config.GetString("combat.aimbot.fovcolor"));
                if (Config.GetBool("combat.aimbot.rainbowfov"))
                    fovColor = Colors.GetRainbow();

                if (Config.GetBool("combat.aimbot.dynamicfov"))
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        (UnityEngine.Camera.main.fieldOfView + 60),
                        Config.GetInt("combat.aimbot.fovsides")
                    );
                else
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        Config.GetInt("combat.aimbot.fovsize"),
                        Config.GetInt("combat.aimbot.fovsides")
                    );
            }
        }
    }
}
