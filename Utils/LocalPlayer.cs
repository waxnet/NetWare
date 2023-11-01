using Invector.CharacterController;
using JustPlay.Equipment;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
    {
        // other
        public static PlayerController GetLocalPlayer()
        {
            PlayerController localPlayerController = null;

            foreach (PlayerController playerController in Storage.players)
            {
                if (playerController.IsMine() && Players.IsPlayerAlive(playerController))
                {
                    localPlayerController = playerController;
                    break;
                }
            }

            return localPlayerController;
        }

        public static PlayerHealth GetLocalPlayerHealth()
        {
            return GetLocalPlayer().ABDABPEKBFM;
        }

        public static WeaponBaseData GetLocalPlayerWeaponBaseData()
        {
            return GetLocalPlayerWeaponsController().PFPIKMMEICB.EIJLJMOPIJH;
        }

        // controllers
        public static vThirdPersonController GetLocalPlayerThirdPersonController()
        {
            return GetLocalPlayer().KBAOKNMAPBD;
        }

        public static WeaponsController GetLocalPlayerWeaponsController()
        {
            return GetLocalPlayer().AIACBMLLLFE;
        }

        public static vThirdPersonCamera GetLocalPlayerThirdPersonCamera()
        {
            return FindObjectOfType<vThirdPersonCamera>();
        }

        // camera
        public static Camera GetLocalPlayerMainCamera()
        {
            return Camera.main;
        }

        public static CameraManager GetLocalPlayerCameraManager()
        {
            return FindObjectOfType<CameraManager>();
        }
    }
}
