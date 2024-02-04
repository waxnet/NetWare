using Invector.CharacterController;
using JustPlay.Equipment;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.JGCPJEAMFIG;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.MLMEBEKJKAE;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.KNCPCFDCFCD;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.DCINIDFEEJF;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.FFJBKALIODH;
        }

        public static WeaponStats? GetWeaponStats()
        {
            return GetWeaponModel()?.BJEJCJJBMCC;
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.CPBDFOPICLP;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool CanShoot()
        {
            return GetPlayerBuildingManager()?.state == GOBILIEJCKA.NONE;
        }
    }
}
