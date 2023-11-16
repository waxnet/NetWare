using Photon.Pun;
using UnityEngine;

namespace NetWare
{
    public class Combat : MonoBehaviour
    {
        public static void Execute()
        {
            if (Input.GetMouseButton(0))
            {
                // silent aim
                if (Config.GetBool("combat.silentaim.enabled"))
                {
                    PlayerController playerController;

                    if (Config.GetBool("combat.silentaim.checkfov"))
                    {
                        if (Config.GetBool("combat.silentaim.dynamicfov"))
                        {
                            playerController = GetBestPlayerInFOV(Camera.main.fieldOfView + 80);
                        } else {
                            playerController = GetBestPlayerInFOV(Config.GetFloat("combat.silentaim.fovsize"));
                        }
                    } else {
                        playerController = GetBestPlayer();
                    }

                    if (playerController != null)
                    {
                        Camera.main.transform.LookAt(Players.GetHeadPosition(playerController));
                    }
                }

                // weapons
                if (Config.GetBool("combat.weapons.norecoil"))
                {
                    LocalPlayer.GetThirdPersonCamera()?.AddRecoil(Vector2.zero, 0, 0);
                }

                if (Config.GetBool("combat.weapons.infiniteammo"))
                {
                    WeaponsController weaponsController = LocalPlayer.GetWeaponsController();

                    weaponsController?.CKDFKAJOAGF?.SetCurrentAmmoAmount(999);
                    weaponsController?.CKDFKAJOAGF?.SetCurrentMagazineAmount(999);
                }

                if (Config.GetBool("combat.weapons.rapidfire"))
                {
                    rapidFireTimer++;

                    if (rapidFireTimer > 3)
                    {
                        WeaponsController weaponsController = LocalPlayer.GetWeaponsController();

                        weaponsController?.photonView?.RPC(
                            "FireWeaponRemote",
                            RpcTarget.All,
                            new object[] {
                                null,
                                true,
                                1
                            }
                        );

                        rapidFireTimer = 0;
                    }
                }
            }
        }

        public static void Draw()
        {
            if (Config.GetBool("combat.silentaim.enabled") && Config.GetBool("combat.silentaim.checkfov") && Config.GetBool("combat.silentaim.drawfov"))
            {
                if (Config.GetBool("combat.silentaim.dynamicfov"))
                {
                    Render.DrawCircle(Color.white, Render.screenCenter, Camera.main.fieldOfView + 80);
                } else {
                    Render.DrawCircle(Color.white, Render.screenCenter, Config.GetFloat("combat.silentaim.fovsize"));
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Silent Aim");
            Config.SetBool(
                "combat.silentaim.enabled",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.enabled"),
                    "Enabled"
                )
            );
            Config.SetBool(
                "combat.silentaim.drawfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.drawfov"),
                    "Draw FOV"
                )
            );
            Config.SetBool(
                "combat.silentaim.checkfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.checkfov"),
                    "Check FOV"
                )
            );
            Config.SetBool(
                "combat.silentaim.dynamicfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.dynamicfov"),
                    "Dynamic FOV"
                )
            );
            Config.SetFloat(
                "combat.silentaim.fovsize",
                Menu.NewSlider(
                    "FOV Size",
                    Config.GetFloat("combat.silentaim.fovsize"),
                    25,
                    500
                )
            );

            Menu.Separate();

            Menu.NewSection("Weapons");
            Config.SetBool(
                "combat.weapons.norecoil",
                Menu.NewToggle(
                    Config.GetBool("combat.weapons.norecoil"),
                    "No Recoil"
                )
            );
            Config.SetBool(
                "combat.weapons.infiniteammo",
                Menu.NewToggle(
                    Config.GetBool("combat.weapons.infiniteammo"),
                    "Infinite Ammo"
                )
            );
            Config.SetBool(
                "combat.weapons.rapidfire",
                Menu.NewToggle(
                    Config.GetBool("combat.weapons.rapidfire"),
                    "Rapid Fire"
                )
            );

            Menu.End();
        }

        // internal methods and variables
        private static int rapidFireTimer = 0;

        private static PlayerController GetBestPlayerInFOV(float fov)
        {
            PlayerController bestPlayerController = null;
            float lastDistance = float.MaxValue;

            foreach (PlayerController playerController in Storage.players)
            {
                if (!playerController.IsMine() && Players.IsPlayerAlive(playerController) && Skeleton.HasSkeleton(playerController))
                {
                    Vector3 playerHeadWorldPosition = Players.GetHeadPosition(playerController);
                    Vector3 playerHeadScreenPosition = Position.ToScreen(playerHeadWorldPosition);

                    if (Position.IsOnScreen(playerHeadScreenPosition))
                    {
                        float distance = new Vector2(
                            playerHeadScreenPosition.x - Render.screenCenter.x,
                            playerHeadScreenPosition.y - Render.screenCenter.y
                        ).magnitude;

                        if (distance < lastDistance)
                        {
                            lastDistance = distance;
                            bestPlayerController = playerController;
                        }
                    }
                }
            }

            if (lastDistance <= fov)
            {
                return bestPlayerController;
            } else {
                return null;
            }
        }

        private static PlayerController GetBestPlayer()
        {
            PlayerController localPlayer = LocalPlayer.Get();

            PlayerController bestPlayerController = null;
            float lastDistance = float.MaxValue;

            if (localPlayer != null)
            {
                Vector3 origin = localPlayer.GLBFEGDMAPI;

                foreach (PlayerController playerController in Storage.players)
                {
                    if (!playerController.IsMine() && Players.IsPlayerAlive(playerController) && Skeleton.HasSkeleton(playerController))
                    {
                        Vector3 playerCenterWorldPosition = playerController.GLBFEGDMAPI;
                        float distance = (playerCenterWorldPosition - origin).magnitude;

                        if (distance < lastDistance)
                        {
                            lastDistance = distance;
                            bestPlayerController = playerController;
                        }
                    }
                }
            }

            return bestPlayerController;
        }
    }
}
