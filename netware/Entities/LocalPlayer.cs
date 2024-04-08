using Invector.CharacterController;
using JustPlay.Equipment;
using UnityEngine;

namespace NetWare
{
    public static class LocalPlayer
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.JBJEHPHAKAJ;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.ODHIJKBAEEJ;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.EFCIDEPGEAD;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.MOIPCKAHEFH;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.BIINMCJEAAN;
        }

        public static WeaponStats GetWeaponStats()
        {
            return (WeaponStats)(GetWeaponModel()?.GCHLINKEEJD);
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.FJEIAIJOKFI;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool IsHoldingWeapon()
        {
            return (GetPlayerBuildingManager()?.state == PMEDKDGHIBA.NONE) || (!GetPlayerBuildingManager().enabled);
        }

        public static bool IsAiming()
        {
            return (bool)(GetWeaponsController()?.HBKJIEMKEFF);
        }

        // other
        public static Vector3? GetAimPosition()
        {
            return GetWeaponsController()?.LBLMGIICGCD.point;
        }
    }
}
