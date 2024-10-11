using NetWare.Attributes;
using NetWare.Utils;

using System.Linq;
using UnityEngine;

namespace NetWare;

public static class Loader
{
    private static GameObject _instance;

    public static void Load()
    {
        // cheat instance
        _instance = new();

        // components
        var componentTypes = SourceUtils
            .GetTypesWithAttribute<NetWareComponent>()
            .Select(x => x.Type);

        foreach (var componentType in componentTypes)
            _instance.AddComponent(componentType);

        // settings
        Object.DontDestroyOnLoad(_instance);
    }
}
