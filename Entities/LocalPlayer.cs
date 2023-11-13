using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.PAGMKIHEDAK;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.KIKIGFOCCEM;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.BNKJJLPJDBC;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.PBFKLLIHCOA;
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.ACMIJJJBFPF;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // other
        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }
    }
}
