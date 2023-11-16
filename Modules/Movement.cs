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
                    playerController.KHHDLLIIKOE = Config.GetInt("movement.speed.speedamount");
                } else {
                    playerController.KHHDLLIIKOE = 1;
                }

                // fly
                if (Config.GetBool("movement.fly.fly"))
                {
                    playerController.SetGodMode(true);

                    if (Config.GetBool("movement.fly.helicopter"))
                    {
                        playerController.gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    }
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
            Config.SetInt(
                "movement.speed.speedamount",
                Menu.NewSlider(
                    "Amount",
                    Config.GetInt("movement.speed.speedamount"),
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
            Config.SetBool(
                "movement.fly.helicopter",
                Menu.NewToggle(
                    Config.GetBool("movement.fly.helicopter"),
                    "Helicopter"
                )
            );

            Menu.End();
        }
    }
}
