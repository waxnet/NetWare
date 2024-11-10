using System.Reflection;

namespace NetWare;

public static class Resolver
{
    // flags
    private const BindingFlags _staticFlags =
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.NonPublic;

    private const BindingFlags _nonStaticFlags =
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic;

    // methods
    public static T GetInstance<T>() where T : class
    {
        var type = typeof(T);

        // resolve class instance property
        var instanceProperty = type.GetProperty("Instance", _staticFlags);

        if (instanceProperty is null)
            foreach (var property in type.GetProperties(_staticFlags))
            {
                var getMethod = property.GetGetMethod(true);

                if (getMethod is not null && getMethod.ReturnType == type)
                {
                    instanceProperty = property;
                    break;
                }
            }

        return instanceProperty?.GetValue(null) as T;
    }

    public static TProperty GetProperty<T, TProperty>(T instance, string getterName) where T : class
    {
        var type = typeof(T);

        // resolve getter by name
        var getterMethod = type.GetMethod($"get_{getterName}", _nonStaticFlags);
        if (getterMethod is null)
            return default;

        // call getter and return value
        return (TProperty)getterMethod.Invoke(instance, null);
    }
}
