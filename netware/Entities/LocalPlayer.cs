using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.IAEJEMLCGGC;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.OLIMGMBHJAP;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.NELAOBLMMDB;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.MNFJLBOKBOG;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.JICDADILBPO;
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.GDJIEOJCGNC;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool CanShoot()
        {
            return GetPlayerBuildingManager()?.state == HFGPGEMALHH.NONE;
        }
    }
}
