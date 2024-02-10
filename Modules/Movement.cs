using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public class Movement : MonoBehaviour
    {
        public static void Execute()
        {
            vThirdPersonController thirdPersonController = LocalPlayer.GetThirdPersonController();
            PlayerController playerController = LocalPlayer.Get();

            if (playerController != null && thirdPersonController != null)
            {
                // speed
                if (Config.GetBool("movement.speed.enabled"))
                {
                    playerController.BICJMAJGJJC = Config.GetFloat("movement.speed.amount");
                } else {
                    playerController.BICJMAJGJJC = 1;
                }

                // fly
                thirdPersonController.SetGodMode(Config.GetBool("movement.fly.enabled"));

                // bhop
                if (Config.GetBool("movement.bhop.enabled") && Input.GetKey(KeyCode.Space))
                {
                    thirdPersonController.Jump();
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
