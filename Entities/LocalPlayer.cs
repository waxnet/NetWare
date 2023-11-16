using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.PFMGMMBMDMO;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.MLCGAAINICC;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.HNADBHINEPA;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.AJEAHIPKNPG;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.CAJMLMBFGKK;
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.KODEGOFIJIC;
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
