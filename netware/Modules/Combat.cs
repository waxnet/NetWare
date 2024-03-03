using Photon.Pun;
using UnityEngine;
using NetWare.Helpers;
using JustPlay.Equipment;
using System.Reflection;

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
                            Mouse.MoveTo(playerScreenPosition, Config.GetInt("combat.aimbot.smoothing"));
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
                                (Config.GetFloat("combat.aimbot.smoothing") / 10)
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
                if (Config.GetBool("combat.silentaim.enabled") && LocalPlayer.IsHoldingWeapon())
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

                // magic bullet
                if (CombatH.magicBulletSkips >= Config.GetInt("combat.magicbullet.frequency"))
                {
                    if (LocalPlayer.IsHoldingWeapon())
                    {
                        if (Config.GetBool("combat.magicbullet.enabled"))
                        {
                            CombatH.magicBulletReset = true;

                            PlayerController playerController;

                            // get best player
                            if (Config.GetBool("combat.magicbullet.checkfov"))
                            {
                                if (Config.GetBool("combat.magicbullet.dynamicfov"))
                                {
                                    playerController = CombatH.GetBestPlayerInFOV(Camera.main.fieldOfView + 80);
                                } else {
                                    playerController = CombatH.GetBestPlayerInFOV(Config.GetFloat("combat.magicbullet.fovsize"));
                                }
                            } else {
                                playerController = CombatH.GetBestPlayer();
                            }

                            if (playerController != null)
                            {
                                // get and check aim position and weapon model
                                Vector3? aimPosition = LocalPlayer.GetAimPosition();
                                WeaponModel weaponModel = LocalPlayer.GetWeaponModel();

                                if (aimPosition != null && weaponModel != null)
                                {
                                    // get weapon fire origin transform
                                    Transform weaponFireOrigin = (Transform)Access.GetValue(weaponModel, "_weaponFireOrigin");

                                    // edit weapon fire origin position
                                    Vector3 playerPosition = Players.GetHipPosition(playerController);
                                    Vector3 direction = (playerPosition - (Vector3)aimPosition).normalized;
                                    weaponFireOrigin.position = (playerPosition + direction);

                                    // set new weapon fire origin transform
                                    Access.SetValue(weaponModel, "_weaponFireOrigin", weaponFireOrigin);
                                }
                            }
                        } else if (CombatH.magicBulletReset) {
                            CombatH.magicBulletReset = false;
                            CombatH.ResetMagicBullet();
                        }
                    }

                    CombatH.magicBulletSkips = 0;
                }
                CombatH.magicBulletSkips++;

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

            if (CombatH.weaponStatsTimer >= 10)
            {
                WeaponModel weaponModel = LocalPlayer.GetWeaponModel();
                if (weaponModel == null)
                    return;
                WeaponStats stats = weaponModel.IIADOJDLHCO;

                bool editWeaponStats = false;

                if (Config.GetBool("combat.weapons.nospread"))
                {
                    stats.StatsForLevel.SpreadSettings.IncreasePerShot = 0f;
                    stats.StatsForLevel.SpreadSettings.AimingSpread = 0f;
                    stats.StatsForLevel.SpreadSettings.DefaultSpread = 0f;
                    editWeaponStats = true;
                }

                if (Config.GetBool("combat.weapons.infiniterange"))
                {
                    stats.Range = 999;
                    editWeaponStats = true;
                }

                if (editWeaponStats)
                {
                    PropertyInfo property = typeof(WeaponModel).GetProperty("IIADOJDLHCO");
                    property.DeclaringType.GetProperty("IIADOJDLHCO");
                    property.GetSetMethod(true).Invoke(weaponModel, new object[] { stats });
                }

                CombatH.weaponStatsTimer = 0;
            }
            CombatH.weaponStatsTimer++;
        }

        public static void Draw()
        {
            // aimbot
            if (Config.GetBool("combat.aimbot.enabled") && Config.GetBool("combat.aimbot.checkfov") && Config.GetBool("combat.aimbot.drawfov"))
            {
                string fovSelectedColor = Config.GetString("combat.aimbot.fovcolor");
                Color fovColor = Colors.HexToRGB(fovSelectedColor);
                if (fovSelectedColor == "RGB")
                    fovColor = Colors.GetRainbow();

                if (Config.GetBool("combat.aimbot.dynamicfov")) {
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        (Camera.main.fieldOfView + 80),
                        Config.GetInt("combat.aimbot.fovsides")
                    );
                } else {
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        Config.GetInt("combat.aimbot.fovsize"),
                        Config.GetInt("combat.aimbot.fovsides")
                    );
                }
            }

            // silent aim
            if (Config.GetBool("combat.silentaim.enabled") && Config.GetBool("combat.silentaim.checkfov") && Config.GetBool("combat.silentaim.drawfov"))
            {
                string fovSelectedColor = Config.GetString("combat.silentaim.fovcolor");
                Color fovColor = Colors.HexToRGB(fovSelectedColor);
                if (fovSelectedColor == "RGB")
                    fovColor = Colors.GetRainbow();

                if (Config.GetBool("combat.silentaim.dynamicfov")) {
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        (Camera.main.fieldOfView + 80),
                        Config.GetInt("combat.silentaim.fovsides")
                    );
                } else {
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        Config.GetInt("combat.silentaim.fovsize"),
                        Config.GetInt("combat.silentaim.fovsides")
                    );
                }
            }

            // magic bullet
            if (Config.GetBool("combat.magicbullet.enabled") && Config.GetBool("combat.magicbullet.checkfov") && Config.GetBool("combat.magicbullet.drawfov"))
            {
                string fovSelectedColor = Config.GetString("combat.magicbullet.fovcolor");
                Color fovColor = Colors.HexToRGB(fovSelectedColor);
                if (fovSelectedColor == "RGB")
                    fovColor = Colors.GetRainbow();

                if (Config.GetBool("combat.magicbullet.dynamicfov"))
                {
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        (Camera.main.fieldOfView + 80),
                        Config.GetInt("combat.magicbullet.fovsides")
                    );
                } else {
                    Render.DrawCircle(
                        fovColor,
                        Render.screenCenter,
                        Config.GetInt("combat.magicbullet.fovsize"),
                        Config.GetInt("combat.magicbullet.fovsides")
                    );
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
                    new string[] { "Camera", "Mouse" }
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
            Config.SetInt(
                "combat.aimbot.fovsize",
                (int)Menu.NewSlider(
                    "FOV Size",
                    Config.GetInt("combat.aimbot.fovsize"),
                    10,
                    500
                )
            );
            Config.SetInt(
                "combat.aimbot.fovsides",
                (int)Menu.NewSlider(
                    "FOV Sides",
                    Config.GetInt("combat.aimbot.fovsides"),
                    3,
                    80
                )
            );
            Menu.NewTitle("Smoothing");
            Config.SetFloat(
                "combat.aimbot.smoothing",
                Menu.NewSlider(
                    "Smoothing",
                    Config.GetFloat("combat.aimbot.smoothing"),
                    1,
                    10
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

            Menu.NewSection("Magic Bullet");
            Config.SetBool(
                "combat.magicbullet.enabled",
                Menu.NewToggle(
                    Config.GetBool("combat.magicbullet.enabled"),
                    "Enabled"
                )
            );
            Menu.NewTitle("Targeting");
            Config.SetInt(
                "combat.magicbullet.frequency",
                (int)Menu.NewSlider(
                    "Frequency",
                    Config.GetInt("combat.magicbullet.frequency"),
                    1,
                    100
                )
            );
            Menu.NewTitle("FOV Settings");
            Config.SetBool(
                "combat.magicbullet.checkfov",
                Menu.NewToggle(
                    Config.GetBool("combat.magicbullet.checkfov"),
                    "Check FOV"
                )
            );
            Config.SetBool(
                "combat.magicbullet.drawfov",
                Menu.NewToggle(
                    Config.GetBool("combat.magicbullet.drawfov"),
                    "Draw FOV"
                )
            );
            Config.SetBool(
                "combat.magicbullet.dynamicfov",
                Menu.NewToggle(
                    Config.GetBool("combat.magicbullet.dynamicfov"),
                    "Dynamic FOV"
                )
            );
            Config.SetInt(
                "combat.magicbullet.fovsize",
                (int)Menu.NewSlider(
                    "FOV Size",
                    Config.GetInt("combat.magicbullet.fovsize"),
                    10,
                    500
                )
            );
            Config.SetInt(
                "combat.magicbullet.fovsides",
                (int)Menu.NewSlider(
                    "FOV Sides",
                    Config.GetInt("combat.magicbullet.fovsides"),
                    3,
                    80
                )
            );
            Menu.NewTitle("Colors");
            Config.SetString(
                "combat.magicbullet.fovcolor",
                Menu.NewTextField(
                    "FOV Color",
                    Config.GetString("combat.magicbullet.fovcolor").ToUpper()
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
            Config.SetInt(
                "combat.silentaim.fovsize",
                (int)Menu.NewSlider(
                    "FOV Size",
                    Config.GetInt("combat.silentaim.fovsize"),
                    10,
                    500
                )
            );
            Config.SetInt(
                "combat.silentaim.fovsides",
                (int)Menu.NewSlider(
                    "FOV Sides",
                    Config.GetInt("combat.silentaim.fovsides"),
                    3,
                    80
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
                "combat.weapons.nospread",
                Menu.NewToggle(
                    Config.GetBool("combat.weapons.nospread"),
                    "No Spread"
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
            Config.SetBool(
                "combat.weapons.infiniterange",
                Menu.NewToggle(
                    Config.GetBool("combat.weapons.infiniterange"),
                    "Infinite Range"
                )
            );

            Menu.End();
        }
    }
}
