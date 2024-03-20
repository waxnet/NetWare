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
            return PlayerController.FACIEHFLDJP;
        }

        public static PlayerHealth GetHealth()
        {
            return Get()?.MGDFNIKPEDA;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
        }

        // controllers
        public static vThirdPersonController GetThirdPersonController()
        {
            return Get()?.FCJLEGCCGML;
        }

        public static WeaponsController GetWeaponsController()
        {
            return Get()?.LNCILGNMBAL;
        }

        public static WeaponModel GetWeaponModel()
        {
            return GetWeaponsController()?.LICGBKBBPBJ;
        }

        public static WeaponStats GetWeaponStats()
        {
            return (WeaponStats)(GetWeaponModel()?.IIADOJDLHCO);
        }

        // camera
        public static CameraManager GetCameraManager()
        {
            return CameraManager.IAOLLGGFCDC;
        }

        public static vThirdPersonCamera GetThirdPersonCamera()
        {
            return GetCameraManager()?.TPCamera;
        }

        // checks
        public static bool IsHoldingWeapon()
        {
            return (GetPlayerBuildingManager()?.state == CEFHGHANAKP.NONE) || (!GetPlayerBuildingManager().enabled);
        }

        public static bool IsAiming()
        {
            return (bool)(GetWeaponsController()?.LLCGNJLGOMJ);
        }

        // other
        public static Vector3? GetAimPosition()
        {
            return GetWeaponsController()?.DJNLBHOMKMN.point;
        }
    }
}
