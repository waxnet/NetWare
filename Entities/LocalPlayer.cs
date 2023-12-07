using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.IKIFDINMMKC;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.MECEACOMEMD;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.JCEELPIJOML;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.FFFJCDBAEBO;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.PIIMNHFPCBF;
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.OJICDNBLPIC;
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
