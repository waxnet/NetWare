using UnityEngine;

namespace NetWare.Helpers
{
    public static class CombatH
    {
        // values
        public static int weaponStatsTimer = 0;
        public static int magicBulletSkips = 0;
        public static bool magicBulletReset = false;

        // magic bullet
        public static void ResetMagicBullet()
        {
            PlayerController localPlayer = LocalPlayer.Get();

            if (localPlayer != null)
            {
                // get and check aim position and weapon model
                Vector3? aimPosition = LocalPlayer.GetAimPosition();
                WeaponModel weaponModel = LocalPlayer.GetWeaponModel();

                if (aimPosition != null && weaponModel != null)
                {
                    // get weapon fire origin transform
                    Transform weaponFireOrigin = (Transform)Access.GetValue(weaponModel, "_weaponFireOrigin");

                    // edit weapon fire origin position
                    weaponFireOrigin.position = Players.GetHeadPosition(localPlayer);

                    // set new weapon fire origin transform
                    Access.SetValue(weaponModel, "_weaponFireOrigin", weaponFireOrigin);
                }
            }
        }

        // other
        public static PlayerController GetBestPlayerInFOV(float fov)
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

            if (lastDistance <= fov) {
                return bestPlayerController;
            } else {
                return null;
            }
        }
        public static PlayerController GetBestPlayer()
        {
            PlayerController localPlayer = LocalPlayer.Get();

            PlayerController bestPlayerController = null;
            float lastDistance = float.MaxValue;

            if (localPlayer != null)
            {
                Vector3 origin = Players.GetHipPosition(localPlayer);

                foreach (PlayerController playerController in Storage.players)
                {
                    if (!Players.IsPlayerTeammate(playerController) && Players.IsPlayerValid(playerController))
                    {
                        float distance = (Players.GetHipPosition(playerController) - origin).magnitude;

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
