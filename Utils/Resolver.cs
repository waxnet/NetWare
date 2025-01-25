﻿using NetWare.Storage;

using System;
using System.Linq;
using System.Linq.Expressions;
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

    // cache system
    private static readonly CacheStorage<Delegate> _instanceCache = new();
    private static readonly CacheStorage<Delegate> _propertyCache = new();

    // methods
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
            foreach (var property in type.GetProperties(_staticFlags))
            {
                var getMethod = property.GetGetMethod(true);

                if (getMethod is not null && getMethod.ReturnType == type)
                {
                    instanceProperty = property;
                    break;
                }
            }

        // build expression and add to cache
        return Expression.Lambda<Func<T>>(Expression.Property(null, instanceProperty)).Compile();
    }

    public static TProperty GetProperty<T, TProperty>(T instance, string getterName) where T : class
    {
        var type = typeof(T);
        var cacheKey = CacheStorage.CreateCacheKey(type.Name, getterName);
        var func = (Func<T, TProperty>)_propertyCache.GetOrCreate(cacheKey, () => CreatePropertyGetter<T, TProperty>(type, getterName));

        return func is null ? default : func.Invoke(instance);
    }
    private static Delegate CreatePropertyGetter<T, TProperty>(Type type, string getterName)
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

    public static void SetProperty<T, TProperty>(T instance, string name, TProperty value) where T : class
    {
        var type = typeof(T);
        var cacheKey = CacheStorage.CreateCacheKey(type.Name, name);
        var func = (Action<T, TProperty>)_propertyCache.GetOrCreate(cacheKey, () => CreatePropertySetter<T, TProperty>(type, name));

        func?.Invoke(instance, value);
    }
    private static Delegate CreatePropertySetter<T, TProperty>(Type type, string name)
    {
        // try searching for the setter method directly
        var setterMethod = type.GetMethod($"set_{name}", _nonStaticFlags);

        if (setterMethod is null) {
            // resolve the getter method to find the property
            var getterMethod = type.GetMethod($"get_{name}", _nonStaticFlags);
            if (getterMethod is null)
                return default;

            // get and check the property from the getter method
            var property = type.GetProperties(_nonStaticFlags)
                .FirstOrDefault(p => p.GetGetMethod(true) == getterMethod);
            if (property is null)
                return default;

            // find the setter, even if obfuscated or private
            setterMethod = property.GetSetMethod(true);
            if (setterMethod is null)
                return default;
        }

        // build expression and add to cache
        var instanceParameter = Expression.Parameter(typeof(T), "instance");
        var valueParameter = Expression.Parameter(typeof(TProperty), "value");
        var propertyAccess = Expression.Call(instanceParameter, setterMethod, valueParameter);
        var expression = Expression.Lambda<Action<T, TProperty>>(propertyAccess, instanceParameter, valueParameter).Compile();

        return expression;
    }
}
