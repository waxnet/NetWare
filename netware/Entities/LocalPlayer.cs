using Invector.CharacterController;
using UnityEngine;

namespace NetWare
{
    public class LocalPlayer : MonoBehaviour
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
        public static bool CanShoot()
        {
            return GetPlayerBuildingManager()?.state == CEFHGHANAKP.NONE;
        }
    }
}
