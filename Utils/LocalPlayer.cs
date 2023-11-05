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
            return PlayerController.LFNGIIPNIDN;
        }

        public static PlayerHealth GetLocalPlayerHealth()
        {
            return GetLocalPlayer()?.ABDABPEKBFM;
        }

        // controllers
        public static vThirdPersonController GetLocalPlayerThirdPersonController()
        {
            return GetLocalPlayer()?.KBAOKNMAPBD;
        }

        public static WeaponsController GetLocalPlayerWeaponsController()
        {
            return GetLocalPlayer()?.AIACBMLLLFE;
        }

        public static vThirdPersonCamera GetLocalPlayerThirdPersonCamera()
        {
            return FindObjectOfType<vThirdPersonCamera>();
        }

        // camera
        public static CameraManager GetLocalPlayerCameraManager()
        {
            return CameraManager.NFLLAGMKOCA;
        }
    }
}
