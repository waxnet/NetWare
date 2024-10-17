using Invector.CharacterController;

using UnityEngine;

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

    public static PlayerHealth GetHealth()
    {
        try {
            return Get().LFMCIILGNAJ;
        } catch {
            return Resolver.GetProperty<PlayerController, PlayerHealth>(Get(), "Health");
        }
    }

    public static vThirdPersonController GetThirdPersonController()
    {
        try {
            return Get().HBPLNFBJBCI;
        } catch {
            return Resolver.GetProperty<PlayerController, vThirdPersonController>(Get(), "ThirdPersonController");
        }
    }

    public static PlayerBuildingManager GetPlayerBuildingManager()
    {
        return PlayerBuildingManager.Instance;
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

    // weapons
    public static WeaponsController GetWeaponsController()
    {
        try {
            return Get()?.NFICAPHFDOF;
        } catch {
            return Resolver.GetProperty<PlayerController, WeaponsController>(Get(), "Weapons");
        }
    }

    public static WeaponModel GetWeaponModel()
    {
        try {
            return GetWeaponsController()?.JDBCOHFFLEI;
        } catch {
            return Resolver.GetProperty<WeaponsController, WeaponModel>(GetWeaponsController(), "CurrentWeapon");
        }
    }

    public static Vector3? GetAimPosition()
    {
        try {
            return GetWeaponsController()?.NLDPALOLINP.point;
        } catch {
            return Resolver.GetProperty<WeaponsController, RaycastHit>(GetWeaponsController(), "AimPointHit").point;
        }
    }

    // checks
    public static bool IsHoldingWeapon()
    {
        return (GetPlayerBuildingManager()?.state.ToString() == "NONE") || (!GetPlayerBuildingManager().enabled);
    }
}
