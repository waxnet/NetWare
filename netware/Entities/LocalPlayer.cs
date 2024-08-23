namespace NetWare
{
    public static class LocalPlayer
    {
        // other
        public static PlayerController Get()
        {
            return PlayerController.NALHMIPKGPO;
        }

        public static PlayerBuildingManager GetPlayerBuildingManager()
        {
            return Get()?.PlayerBuildingManager;
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
    }
}
