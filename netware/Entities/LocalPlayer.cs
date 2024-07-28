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
            return PlayerController.NALHMIPKGPO;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.LFMCIILGNAJ;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.HBPLNFBJBCI;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.NFICAPHFDOF;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.JDBCOHFFLEI;
        }

        public static WeaponStats GetWeaponStats()
        {
            return (WeaponStats)(GetWeaponModel()?.MHDDCENIHGH);
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.OEPCIBFBPLE;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool IsHoldingWeapon()
        {
            return (GetPlayerBuildingManager()?.state == IFLOFNCLLMO.NONE) || (!GetPlayerBuildingManager().enabled);
        }

        public static bool IsAiming()
        {
            return (bool)(GetWeaponsController()?.IAMJLKHMPKN);
        }

        // other
        public static Vector3? GetAimPosition()
        {
            return GetWeaponsController()?.NLDPALOLINP.point;
        }
    }
}
