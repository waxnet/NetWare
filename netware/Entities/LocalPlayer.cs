namespace NetWare.Entities;

public static class LocalPlayer
{
    // other
    public static PlayerController Get()
    {
        try {
            return PlayerController.NALHMIPKGPO;
        } catch {
            return Resolver.GetInstance<PlayerController>();
        }
    }

    public static PlayerBuildingManager GetPlayerBuildingManager()
    {
        return Get()?.PlayerBuildingManager;
    }

    // camera
    public static CameraManager GetCameraManager()
    {
        try {
            return CameraManager.OEPCIBFBPLE;
        } catch {
            return Resolver.GetInstance<CameraManager>();
        }
    }

    public static vThirdPersonCamera GetThirdPersonCamera()
    {
        return GetCameraManager()?.TPCamera;
    }

    // checks
    public static bool IsHoldingWeapon()
    {
        return (GetPlayerBuildingManager()?.state.ToString() == "NONE") || (!GetPlayerBuildingManager().enabled);
    }
}
