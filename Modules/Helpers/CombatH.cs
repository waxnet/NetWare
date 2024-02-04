using UnityEngine;

namespace NetWare.Helpers
{
    public class CombatH
    {
        // values
        public static int rapidFireTimer = 0;

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

            if (lastDistance <= fov)
            {
                return bestPlayerController;
            }
            else
            {
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
                Vector3 origin = localPlayer.PFCNHGBHDLO;

                foreach (PlayerController playerController in Storage.players)
                {
                    if (!Players.IsPlayerTeammate(playerController) && Players.IsPlayerValid(playerController))
                    {
                        float distance = (playerController.PFCNHGBHDLO - origin).magnitude;

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
