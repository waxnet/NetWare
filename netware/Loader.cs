using NetWare.Attributes;
using NetWare.Utils;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NetWare;

public static class Loader
{
    private static GameObject _instance;

    public static void Load()
    {
        // cheat instance
        _instance = new GameObject();

        // components
        var componentTypes = SourceUtils
            .GetTypesWithAttribute<NetWareComponentAttribute>()
            .Select(x => x.Type); // this is NOT an extra allocation, IEnumerable is differed

        foreach (var componentType in componentTypes)
            _instance.AddComponent(componentType);

        // settings
        Object.DontDestroyOnLoad(_instance);
    }

    public static void Unload()
    {
        Object.DestroyImmediate(_instance);
    }
}

public static class Native
{
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    public static void MessageBox(string msg) => MessageBox(IntPtr.Zero, msg, null, 0);
}