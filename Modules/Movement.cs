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
                if (Config.GetBool("movement.speed.enabled"))
                {
                    playerController.MGOCBLHDAOP = Config.GetFloat("movement.speed.amount");
                } else {
                    playerController.MGOCBLHDAOP = 1;
                }

                // fly
                playerController.SetGodMode(Config.GetBool("movement.fly.enabled"));

                // bhop
                if (Config.GetBool("movement.bhop.enabled") && Input.GetKey(KeyCode.Space))
                {
                    LocalPlayer.GetThirdPersonController()?.Jump();
                }
            }
        }

        public static void Tab()
        {
            Menu.Begin();

            Menu.NewSection("Speed");
            Config.SetBool(
                "movement.speed.enabled",
                Menu.NewToggle(
                    Config.GetBool("movement.speed.enabled"),
                    "Enabled"
                )
            );
            Config.SetFloat(
                "movement.speed.amount",
                Menu.NewSlider(
                    "Amount",
                    Config.GetFloat("movement.speed.amount"),
                    1,
                    10
                )
            );

            Menu.Separate();

            Menu.NewSection("Fly");
            Config.SetBool(
                "movement.fly.enabled",
                Menu.NewToggle(
                    Config.GetBool("movement.fly.enabled"),
                    "Enabled"
                )
            );

            Menu.NewSection("BHop");
            Config.SetBool(
                "movement.bhop.enabled",
                Menu.NewToggle(
                    Config.GetBool("movement.bhop.enabled"),
                    "Enabled"
                )
            );

            Menu.End();
        }
    }
}
