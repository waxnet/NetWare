using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetWare.Utils;

public static class SourceUtils
{
    private static Type[] _assemblyTypes;

    public static IEnumerable<(Type Type, TAttribute Attribute)> GetTypesWithAttribute<TAttribute>() where TAttribute : Attribute
    {
        Init();

        return _assemblyTypes
            .Where(x => x.GetCustomAttribute<TAttribute>() is not null)
            .Select(x => (x, x.GetCustomAttribute<TAttribute>()));
    }

    public static IEnumerable<T> CreateInstancesAs<T>(IEnumerable<Type> types)
    {
        Init();

        List<T> instances = new(types.Count());
        foreach (var type in types)
        {
            if (!typeof(T).IsAssignableFrom(type))
                throw new ArgumentException("Type is not assignable from <T>");

            instances.Add((T)Activator.CreateInstance(type));
        }

        return instances;
    }

    private static void Init()
    {
        _assemblyTypes ??= Assembly
            .GetExecutingAssembly()
            .GetTypes();
    }
}
