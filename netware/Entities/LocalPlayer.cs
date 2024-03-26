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
            return PlayerController.BFLGFELENMF;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.KEPLIKHCFNG;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.KNIHOFNCMCE;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.EHGPGJGFIOL;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.NCCDAMICFME;
        }

        public static WeaponStats GetWeaponStats()
        {
            return (WeaponStats)(GetWeaponModel()?.IEIDNJPAHEB);
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.ALADDIOCPKF;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool IsHoldingWeapon()
        {
            return (GetPlayerBuildingManager()?.state == CLLGBGCNKHC.NONE) || (!GetPlayerBuildingManager().enabled);
        }

        public static bool IsAiming()
        {
            return (bool)(GetWeaponsController()?.ENNCKHFABDM);
        }

        // other
        public static Vector3? GetAimPosition()
        {
            return GetWeaponsController()?.HMMBACDKGFM.point;
        }
    }
}
