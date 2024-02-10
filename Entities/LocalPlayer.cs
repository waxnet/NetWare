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
            return PlayerController.LKIGDIGGPAD;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.DNBIIJJDBLN;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.EHCIACFKDOK;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.DNLMMIKCGGE;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.EPKJMHJNGIB;
        }

        public static WeaponStats? GetWeaponStats()
        {
            return GetWeaponModel()?.HMHEAENHHCF;
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.NKEFBCLBLKI;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool CanShoot()
        {
            return GetPlayerBuildingManager()?.state == HLOAPFHPADJ.NONE;
        }
    }
}
