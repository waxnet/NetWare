using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public static class Movement
    {
        public static void Execute()
        {
            vThirdPersonController thirdPersonController = LocalPlayer.GetThirdPersonController();
            PlayerController playerController = LocalPlayer.Get();

            if (playerController != null && thirdPersonController != null)
            {
                // speed
                if (Config.GetBool("movement.speed.enabled")) {
                    playerController.ENPBPEPJODF = Config.GetFloat("movement.speed.amount", 5);
                } else {
                    playerController.ENPBPEPJODF = 1;
                }

                // fly
                thirdPersonController.SetGodMode(Config.GetBool("movement.fly.enabled"));

                // bhop
                if (Config.GetBool("movement.bhop.enabled") && Input.GetKey(KeyCode.Space))
                    thirdPersonController.Jump();
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
            Config.SetString(
                "movement.speed.keybind",
                Menu.NewKeybind(
                    Config.GetString("movement.speed.keybind")
                )
            );
            Menu.NewTitle("Settings");
            Config.SetFloat(
                "movement.speed.amount",
                Menu.NewSlider(
                    "Amount",
                    Config.GetFloat("movement.speed.amount", 5),
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
            Config.SetString(
                "movement.fly.keybind",
                Menu.NewKeybind(
                    Config.GetString("movement.fly.keybind")
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
            Config.SetString(
                "movement.bhop.keybind",
                Menu.NewKeybind(
                    Config.GetString("movement.bhop.keybind")
                )
            );

            Menu.End();
        }
    }
}
