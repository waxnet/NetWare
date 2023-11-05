using UnityEngine;

namespace NetWare
{
    public class Movement : MonoBehaviour
    {
        public static void Execute()
        {
            PlayerController playerController = LocalPlayer.GetLocalPlayer();

            if (playerController != null)
            {
                // speed
                if (Config.Movement.Speed.speed)
                {
                    playerController.NGABAFFHJBE = Config.Movement.Speed.speedAmount;
                } else
                {
                    playerController.NGABAFFHJBE = 1;
                }

                // fly
                if (Config.Movement.Fly.fly)
                {
                    playerController.SetGodMode(true);

                    if (Config.Movement.Fly.helicopter)
                    {
                        playerController.gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    }
                } else
                {
                    playerController.SetGodMode(false);
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Speed");
            Config.Movement.Speed.speed = Menu.NewToggle(Config.Movement.Speed.speed, "Enabled");
            Config.Movement.Speed.speedAmount = Menu.NewSlider("Amount", Config.Movement.Speed.speedAmount, 1, 10);
            
            Menu.Separate();

            Menu.NewSection("Fly");
            Config.Movement.Fly.fly = Menu.NewToggle(Config.Movement.Fly.fly, "Enabled");
            Config.Movement.Fly.helicopter = Menu.NewToggle(Config.Movement.Fly.helicopter, "Helicopter");

            Menu.End();
        }
    }
}
