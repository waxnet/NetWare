using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.HGPJOFAPIBH;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.PAEKEKFLHOK;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.OEGGIHFLNAN;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.CIMFGBELHOH;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.MMFGMHOCMEP;
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.LIPLNDMKLDB;
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
