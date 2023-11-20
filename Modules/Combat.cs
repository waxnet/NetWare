using Photon.Pun;
using System;
using UnityEngine;

namespace NetWare
{
    public class Combat : MonoBehaviour
    {
        public static void Execute()
        {
            if (Input.GetMouseButton(1))
            {
                // soft aim
                if (Config.GetBool("combat.softaim.enabled"))
                {
                    PlayerController playerController;

                    if (Config.GetBool("combat.softaim.checkfov"))
                    {
                        if (Config.GetBool("combat.softaim.dynamicfov"))
                        {
                            playerController = GetBestPlayerInFOV(Camera.main.fieldOfView + 80);
                        } else {
                            playerController = GetBestPlayerInFOV(Config.GetFloat("combat.softaim.fovsize"));
                        }
                    } else {
                        playerController = GetBestPlayerInFOV(Screen.width);
                    }

                    if (playerController != null)
                    {
                        Vector3 playerScreenPosition = Position.ToScreen(Players.GetHeadPosition(playerController));

                        if (Position.IsOnScreen(playerScreenPosition))
                        {
                            Mouse.MoveTo(playerScreenPosition, (int)Math.Round(Config.GetFloat("combat.softaim.smoothing"), 0));
                        }
                    }
                }
            }

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
                    WeaponModel weaponModel = LocalPlayer.GetWeaponModel();

                    weaponModel?.SetCurrentAmmoAmount(999);
                    weaponModel?.SetCurrentMagazineAmount(999);
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
            // soft aim
            if (Config.GetBool("combat.softaim.enabled") && Config.GetBool("combat.softaim.checkfov") && Config.GetBool("combat.softaim.drawfov"))
            {
                Color fovColor = Colors.HexToRGB(Config.GetString("combat.softaim.fovcolor"));

                if (Config.GetBool("combat.softaim.dynamicfov"))
                {
                    Render.DrawCircle(fovColor, Render.screenCenter, Camera.main.fieldOfView + 80);
                } else {
                    Render.DrawCircle(fovColor, Render.screenCenter, Config.GetFloat("combat.softaim.fovsize"));
                }
            }

            // silent aim
            if (Config.GetBool("combat.silentaim.enabled") && Config.GetBool("combat.silentaim.checkfov") && Config.GetBool("combat.silentaim.drawfov"))
            {
                Color fovColor = Colors.HexToRGB(Config.GetString("combat.silentaim.fovcolor"));

                if (Config.GetBool("combat.silentaim.dynamicfov"))
                {
                    Render.DrawCircle(fovColor, Render.screenCenter, Camera.main.fieldOfView + 80);
                } else {
                    Render.DrawCircle(fovColor, Render.screenCenter, Config.GetFloat("combat.silentaim.fovsize"));
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Soft Aim");
            Config.SetBool(
                "combat.softaim.enabled",
                Menu.NewToggle(
                    Config.GetBool("combat.softaim.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("FOV Settings");
            Config.SetBool(
                "combat.softaim.checkfov",
                Menu.NewToggle(
                    Config.GetBool("combat.softaim.checkfov"),
                    "Check FOV"
                )
            );
            Config.SetBool(
                "combat.softaim.drawfov",
                Menu.NewToggle(
                    Config.GetBool("combat.softaim.drawfov"),
                    "Draw FOV"
                )
            );
            Config.SetBool(
                "combat.softaim.dynamicfov",
                Menu.NewToggle(
                    Config.GetBool("combat.softaim.dynamicfov"),
                    "Dynamic FOV"
                )
            );
            Config.SetFloat(
                "combat.softaim.fovsize",
                Menu.NewSlider(
                    "FOV Size",
                    Config.GetFloat("combat.softaim.fovsize"),
                    10,
                    500
                )
            );
            Menu.NewTitle("Smoothing");
            Config.SetFloat(
                "combat.softaim.smoothing",
                Menu.NewSlider(
                    "Smoothing",
                    Config.GetFloat("combat.softaim.smoothing"),
                    5,
                    10
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "combat.softaim.fovcolor",
                Menu.NewTextField(
                    "FOV Color",
                    Config.GetString("combat.softaim.fovcolor").ToUpper()
                )
            );

            Menu.Separate();

            Menu.NewSection("Silent Aim");
            Config.SetBool(
                "combat.silentaim.enabled",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("FOV Settings");
            Config.SetBool(
                "combat.silentaim.checkfov",
                Menu.NewToggle(
                    Config.GetBool("combat.silentaim.checkfov"),
                    "Check FOV"
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
                    10,
                    500
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "combat.silentaim.fovcolor",
                Menu.NewTextField(
                    "FOV Color",
                    Config.GetString("combat.silentaim.fovcolor").ToUpper()
                )
            );

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
                if (!Players.IsPlayerTeammate(playerController) && Players.IsPlayerValid(playerController))
                {
                    Vector3 playerHeadWorldPosition = Players.GetHeadPosition(playerController);
                    Vector3 playerHeadScreenPosition = Position.ToScreen(playerHeadWorldPosition);

                    if (Position.IsOnScreen(playerHeadScreenPosition))
                    {
                        float screenDistance = new Vector2(
                            playerHeadScreenPosition.x - Render.screenCenter.x,
                            playerHeadScreenPosition.y - Render.screenCenter.y
                        ).magnitude;

                        if (screenDistance < lastDistance)
                        {
                            lastDistance = screenDistance;
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
                Vector3 origin = localPlayer.IMCKFPJJOFH;

                foreach (PlayerController playerController in Storage.players)
                {
                    if (!Players.IsPlayerTeammate(playerController) && Players.IsPlayerValid(playerController))
                    {
                        float distance = (playerController.IMCKFPJJOFH - origin).magnitude;

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
