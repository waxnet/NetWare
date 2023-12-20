using UnityEngine;

namespace NetWare
{
    public class Movement : MonoBehaviour
    {
        public static void Execute()
        {
            PlayerController playerController = LocalPlayer.Get();

            if (playerController != null)
            {
                // speed
                if (Config.GetBool("movement.speed.speed"))
                {
                    playerController.MGOCBLHDAOP = Config.GetFloat("movement.speed.speedamount");
                } else {
                    playerController.MGOCBLHDAOP = 1;
                }

                // fly
                if (Config.GetBool("movement.fly.fly"))
                {
                    playerController.SetGodMode(true);
                } else {
                    playerController.SetGodMode(false);
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Speed");
            Config.SetBool(
                "movement.speed.speed",
                Menu.NewToggle(
                    Config.GetBool("movement.speed.speed"),
                    "Enabled"
                )
            );
            Config.SetFloat(
                "movement.speed.speedamount",
                Menu.NewSlider(
                    "Amount",
                    Config.GetFloat("movement.speed.speedamount"),
                    1,
                    10
                )
            );

            Menu.Separate();

            Menu.NewSection("Fly");
            Config.SetBool(
                "movement.fly.fly",
                Menu.NewToggle(
                    Config.GetBool("movement.fly.fly"),
                    "Enabled"
                )
            );

            Menu.End();
        }
    }
}
