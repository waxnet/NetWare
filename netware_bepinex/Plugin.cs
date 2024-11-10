using NetWare.Modules.LegitModules;
using NetWare.Modules.VisualModules;
using NetWare.UI;

using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;

using UnityEngine;

namespace NetWare;

[BepInPlugin("waxnet.netware", "NetWare", "0.0.0")]
public class Plugin : BasePlugin
{
    private GameObject _instance;

    public override void Load()
    {
        // cheat instance
        _instance = new();

        // register types
        ClassInjector.RegisterTypeInIl2Cpp<Main>();

        ClassInjector.RegisterTypeInIl2Cpp<Aimbot>();
        ClassInjector.RegisterTypeInIl2Cpp<SilentAim>();

        ClassInjector.RegisterTypeInIl2Cpp<NameTags>();
        ClassInjector.RegisterTypeInIl2Cpp<Boxes>();
        ClassInjector.RegisterTypeInIl2Cpp<Skeleton>();
        ClassInjector.RegisterTypeInIl2Cpp<Tracers>();
        ClassInjector.RegisterTypeInIl2Cpp<Modules.VisualModules.Camera>();

        ClassInjector.RegisterTypeInIl2Cpp<MenuChecks>();

        // add types
        _instance.AddComponent<Main>();

        _instance.AddComponent<Aimbot>();
        _instance.AddComponent<SilentAim>();

        _instance.AddComponent<NameTags>();
        _instance.AddComponent<Boxes>();
        _instance.AddComponent<Skeleton>();
        _instance.AddComponent<Tracers>();
        _instance.AddComponent<Modules.VisualModules.Camera>();

        _instance.AddComponent<MenuChecks>();

        // settings
        _instance.gameObject.hideFlags = HideFlags.HideAndDontSave | HideFlags.HideInInspector;
        _instance.hideFlags = HideFlags.HideAndDontSave | HideFlags.HideInInspector;

        Object.DontDestroyOnLoad(_instance);
    }
}
