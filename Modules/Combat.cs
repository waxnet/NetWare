using Photon.Pun;
using UnityEngine;
using NetWare.Helpers;

namespace NetWare
{
    public class Combat : MonoBehaviour
    {
        public static void Execute()
        {
            if (Input.GetMouseButton(1))
            {
                // aimbot
                if (Config.GetBool("combat.aimbot.enabled"))
                {
                    PlayerController playerController;

                    // get best player
                    if (Config.GetBool("combat.aimbot.checkfov"))
                    {
                        if (Config.GetBool("combat.aimbot.dynamicfov"))
                        {
                            playerController = CombatH.GetBestPlayerInFOV(Camera.main.fieldOfView + 80);
                        } else {
                            playerController = CombatH.GetBestPlayerInFOV(Config.GetFloat("combat.aimbot.fovsize"));
                        }
                    } else {
                        playerController = CombatH.GetBestPlayerInFOV(Screen.width);
                    }

                    if (playerController != null)
                    {
                        // get player positions
                        Vector3 playerWorldPosition = Players.GetBonePosition(
                            playerController,
                            Skeleton.GetBoneFromString(Config.GetString("combat.aimbot.aimbone"))
                        );
                        Vector3 playerScreenPosition = Position.ToScreen(playerWorldPosition);

                        // get aimbot aim mode
                        string aimbotAimMode = Config.GetString("combat.aimbot.aimmode");

                        // aim at player
                        if (aimbotAimMode == "Mouse" && Position.IsOnScreen(playerScreenPosition)) {
                            Mouse.MoveTo(playerScreenPosition, (Config.GetFloat("combat.aimbot.smoothing") * 10));
                        }

                        if (aimbotAimMode == "Camera") {
                            vThirdPersonCamera camera = LocalPlayer.GetThirdPersonCamera();

                            // get rotations
                            Quaternion startRotation = camera.transform.rotation;
                            camera.transform.LookAt(playerWorldPosition);
                            Quaternion endRotation = camera.transform.rotation;

                            // reset camera rotation
                            camera.transform.rotation = startRotation;

                            // get new rotation
                            Quaternion newRotation = Quaternion.Lerp(
                                startRotation,
                                endRotation,
                                Config.GetFloat("combat.aimbot.smoothing")
                            );

                            // set camera rotation
                            Camera.main.transform.rotation = newRotation;
                            camera.transform.rotation = newRotation;
                            camera.SetRotation(newRotation.eulerAngles);
                        }
                    }
                }
            }

            if (Input.GetMouseButton(0))
            {
                // silent aim
                if (Config.GetBool("combat.silentaim.enabled") && LocalPlayer.CanShoot())
                {
                    PlayerController playerController;

                    // get best player
                    if (Config.GetBool("combat.silentaim.checkfov"))
                    {
                        if (Config.GetBool("combat.silentaim.dynamicfov"))
                        {
                            playerController = CombatH.GetBestPlayerInFOV(Camera.main.fieldOfView + 80);
                        } else {
                            playerController = CombatH.GetBestPlayerInFOV(Config.GetFloat("combat.silentaim.fovsize"));
                        }
                    } else {
                        playerController = CombatH.GetBestPlayer();
                    }

                    if (playerController != null)
                    {
                        // make main camera aim at player
                        Camera.main.transform.LookAt(
                            Players.GetBonePosition(
                                playerController,
                                Skeleton.GetBoneFromString(Config.GetString("combat.silentaim.aimbone"))
                            )
                        );
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
                    CombatH.rapidFireTimer++;

                    if (CombatH.rapidFireTimer > 3)
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

                        CombatH.rapidFireTimer = 0;
                    }
                }
            }
        }

        public static void Draw()
        {
            // aimbot
            if (Config.GetBool("combat.aimbot.enabled") && Config.GetBool("combat.aimbot.checkfov") && Config.GetBool("combat.aimbot.drawfov"))
            {
                Color fovColor = Colors.HexToRGB(Config.GetString("combat.aimbot.fovcolor"));

                if (Config.GetBool("combat.aimbot.dynamicfov"))
                {
                    Render.DrawCircle(fovColor, Render.screenCenter, Camera.main.fieldOfView + 80);
                } else {
                    Render.DrawCircle(fovColor, Render.screenCenter, Config.GetFloat("combat.aimbot.fovsize"));
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

            Menu.NewSection("Aimbot");
            Config.SetBool(
                "combat.aimbot.enabled",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Targeting");
            Config.SetString(
                "combat.aimbot.aimbone",
                Menu.NewList(
                    "Aim Bone",
                    Config.GetString("combat.aimbot.aimbone"),
                    new string[] { "Head", "Hips" }
                )
            );
            Config.SetString(
                "combat.aimbot.aimmode",
                Menu.NewList(
                    "Aim Mode",
                    Config.GetString("combat.aimbot.aimmode"),
                    new string[] { "Mouse", "Camera" }
                )
            );
            Menu.NewTitle("FOV Settings");
            Config.SetBool(
                "combat.aimbot.checkfov",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.checkfov"),
                    "Check FOV"
                )
            );
            Config.SetBool(
                "combat.aimbot.drawfov",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.drawfov"),
                    "Draw FOV"
                )
            );
            Config.SetBool(
                "combat.aimbot.dynamicfov",
                Menu.NewToggle(
                    Config.GetBool("combat.aimbot.dynamicfov"),
                    "Dynamic FOV"
                )
            );
            Config.SetFloat(
                "combat.aimbot.fovsize",
                Menu.NewSlider(
                    "FOV Size",
                    Config.GetFloat("combat.aimbot.fovsize"),
                    10,
                    500
                )
            );
            Menu.NewTitle("Smoothing");
            Config.SetFloat(
                "combat.aimbot.smoothing",
                Menu.NewSlider(
                    "Smoothing",
                    Config.GetFloat("combat.aimbot.smoothing"),
                    0,
                    1
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "combat.aimbot.fovcolor",
                Menu.NewTextField(
                    "FOV Color",
                    Config.GetString("combat.aimbot.fovcolor").ToUpper()
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
            Menu.NewTitle("Targeting");
            Config.SetString(
                "combat.silentaim.aimbone",
                Menu.NewList(
                    "Aim Bone",
                    Config.GetString("combat.silentaim.aimbone"),
                    new string[] { "Head", "Hips" }
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
    }
}
