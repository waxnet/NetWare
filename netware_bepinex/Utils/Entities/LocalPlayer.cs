namespace NetWare.Entities;

public static class LocalPlayer
{
    // other
    public static PlayerController Get()
    {
        return Resolver.GetInstance<PlayerController>();
    }

    public static PlayerBuildingManager GetPlayerBuildingManager()
    {
        return PlayerBuildingManager.Instance;
    }

    // camera
    public static CameraManager GetCameraManager()
    {
        return Resolver.GetInstance<CameraManager>();
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
