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
                if (Config.Combat.SilentAim.enabled)
                {
                    PlayerController playerController = null;
                    if (Config.Combat.SilentAim.checkFov)
                    {
                        playerController = GetBestPlayerInFOV(Config.Combat.SilentAim.fovSize);
                    } else {
                        playerController = GetBestPlayer();
                    }

                    if (playerController != null)
                    {
                        LocalPlayer.GetLocalPlayerMainCamera().transform.LookAt(Players.GetHeadPosition(playerController));
                    }
                }

                // weapons
                if (Config.Combat.Weapons.noRecoil)
                {
                    LocalPlayer.GetLocalPlayerThirdPersonCamera().AddRecoil(Vector2.zero, 0, 0);
                }

                if (Config.Combat.Weapons.infiniteAmmo)
                {
                    WeaponsController weaponsController = LocalPlayer.GetLocalPlayerWeaponsController();

                    weaponsController.PFPIKMMEICB.SetCurrentAmmoAmount(999);
                    weaponsController.PFPIKMMEICB.SetCurrentMagazineAmount(999);
                }

                if (Config.Combat.Weapons.rapidFire)
                {
                    rapidFireTimer++;

                    if (rapidFireTimer > 3)
                    {
                        LocalPlayer.GetLocalPlayerWeaponsController().photonView.RPC(
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
            if (Config.Combat.SilentAim.enabled && Config.Combat.SilentAim.checkFov)
            {
                Render.DrawCircle(Color.white, Render.screenCenter, Config.Combat.SilentAim.fovSize);
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Silent Aim");
            Config.Combat.SilentAim.enabled = Menu.NewToggle(Config.Combat.SilentAim.enabled, "Enabled");
            Config.Combat.SilentAim.checkFov = Menu.NewToggle(Config.Combat.SilentAim.checkFov, "Check FOV");
            Config.Combat.SilentAim.fovSize = Menu.NewSlider("Silent Aim FOV", Config.Combat.SilentAim.fovSize, 50, 500);

            Menu.Separate();

            Menu.NewSection("Weapons");
            Config.Combat.Weapons.noRecoil = Menu.NewToggle(Config.Combat.Weapons.noRecoil, "No Recoil");
            Config.Combat.Weapons.infiniteAmmo = Menu.NewToggle(Config.Combat.Weapons.infiniteAmmo, "Infinite Ammo");
            Config.Combat.Weapons.rapidFire = Menu.NewToggle(Config.Combat.Weapons.rapidFire, "Rapid Fire");

            Menu.End();
        }

        // internal methods and variables
        private static int rapidFireTimer = 0;

        private static PlayerController GetBestPlayerInFOV(float fov)
        {
            Vector3 origin = new Vector3((Screen.width / 2), (Screen.height / 2));

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
                        float distance = (playerHeadScreenPosition - origin).magnitude;

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
            }
            else
            {
                return null;
            }
        }

        private static PlayerController GetBestPlayer()
        {
            Vector3 origin = LocalPlayer.GetLocalPlayer().FGBLDFEONKO;

            PlayerController bestPlayerController = null;
            float lastDistance = float.MaxValue;

            foreach (PlayerController playerController in Storage.players)
            {
                if (!playerController.IsMine() && Players.IsPlayerAlive(playerController) && Skeleton.HasSkeleton(playerController))
                {
                    Vector3 playerCenterWorldPosition = playerController.FGBLDFEONKO;
                    float distance = (playerCenterWorldPosition - origin).magnitude;

                    if (distance < lastDistance)
                    {
                        lastDistance = distance;
                        bestPlayerController = playerController;
                    }
                }
            }

            return bestPlayerController;
        }
    }
}
