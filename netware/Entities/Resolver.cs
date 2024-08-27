using NetWare.Storage;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NetWare.Entities;

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

    // cache system
    private static readonly CacheStorage<Delegate> _instanceCache = new();
    private static readonly CacheStorage<Delegate> _propertyCache = new();

    public static T GetInstance<T>() where T : class
    {
        var type = typeof(T);
        var cacheKey = CacheStorage.CreateCacheKey(type.Name, "instance");
        var func = (Func<T>)_instanceCache.GetOrCreate(cacheKey, () => CreateInstance<T>(type));

        return func is null ? default : func.Invoke();
    }

    private static Delegate CreateInstance<T>(Type type) where T : class
    {
        // resolve class instance property
        var instanceProperty = type.GetProperty("Instance", _staticFlags);

        if (instanceProperty is null)
        {
            foreach (var property in type.GetProperties(_staticFlags))
            {
                var getMethod = property.GetGetMethod(true);

                if (getMethod is not null && getMethod.ReturnType == type)
                {
                    instanceProperty = property;
                    break;
                }
            }
        }

        // build expression and add to cache
        return Expression.Lambda<Func<T>>(Expression.Property(null, instanceProperty)).Compile();
    }

    public static TProperty GetProperty<T, TProperty>(T instance, string getterName) where T : class
    {
        var type = typeof(T);
        var cacheKey = CacheStorage.CreateCacheKey(type.Name, getterName);
        var func = (Func<T, TProperty>)_propertyCache.GetOrCreate(cacheKey, () => CreateProperty<T, TProperty>(type, getterName));

        return func is null ? default : func.Invoke(instance);
    }

    private static Delegate CreateProperty<T, TProperty>(Type type, string getterName)
    {
        // resolve getter by name
        var getterMethod = type.GetMethod($"get_{getterName}", _nonStaticFlags);
        if (getterMethod is null)
            return default;

        // build expresssion and add to cache
        var expressionParameter = Expression.Parameter(typeof(T), "instance");
        var propertyAccess = Expression.Property(expressionParameter, getterMethod);
        var expression = Expression.Lambda<Func<T, TProperty>>(propertyAccess, expressionParameter).Compile();

        return expression;
    }
}
