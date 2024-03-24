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
            return PlayerController.EDDEFNBPDPF;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.BOPJGJLLKAK;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.NKNCIFGBIGP;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.ANHKGOHBCLO;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.GKNADIJBIPC;
        }

        public static WeaponStats GetWeaponStats()
        {
            return (WeaponStats)(GetWeaponModel()?.APPPJGCCEBG);
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.OOAIICFKEKI;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool IsHoldingWeapon()
        {
            return (GetPlayerBuildingManager()?.state == LJCLAABHFLF.NONE) || (!GetPlayerBuildingManager().enabled);
        }

        public static bool IsAiming()
        {
            return (bool)(GetWeaponsController()?.NOLHAAGPGIJ);
        }

        // other
        public static Vector3? GetAimPosition()
        {
            return GetWeaponsController()?.MLKOPKJNBPH.point;
        }
    }
}
