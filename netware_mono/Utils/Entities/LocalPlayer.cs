using Invector.CharacterController;

using UnityEngine;

namespace NetWare.Entities;

public static class LocalPlayer
{
    // other
    public static PlayerController Get()
    {
        return Resolver.GetInstance<PlayerController>();
    }

    public static PlayerHealth GetHealth()
    {
        return Resolver.GetProperty<PlayerController, PlayerHealth>(Get(), "Health");
    }

    public static vThirdPersonController GetThirdPersonController()
    {
        return Resolver.GetProperty<PlayerController, vThirdPersonController>(Get(), "ThirdPersonController");
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

    // weapons
    public static WeaponsController GetWeaponsController()
    {
        return Resolver.GetProperty<PlayerController, WeaponsController>(Get(), "Weapons");
    }

    public static WeaponModel GetWeaponModel()
    {
        return Resolver.GetProperty<WeaponsController, WeaponModel>(GetWeaponsController(), "CurrentWeapon");
    }

    public static Vector3? GetAimPosition()
    {
        return Resolver.GetProperty<WeaponsController, RaycastHit>(GetWeaponsController(), "AimPointHit").point;
    }

    // checks
    public static bool IsHoldingWeapon()
    {
        return (GetPlayerBuildingManager()?.state.ToString() == "NONE") || (!GetPlayerBuildingManager().enabled);
    }
}
