using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetWare
{
    public static class Resolver
    {
        // flags
        private static BindingFlags staticFlags = (
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.NonPublic
        );

        private static BindingFlags nonStaticFlags = (
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic
        );

        // cache system
        private static Dictionary<string, PropertyInfo> instanceCache = new Dictionary<string, PropertyInfo>();
        private static Dictionary<string, MethodInfo> propertyCache = new Dictionary<string, MethodInfo>();
        
        private static string GetCacheKey(params string[] values)
        {
            return string.Join("_", values);
        }

        // methods
        public static T GetInstance<T>() where T : class
        {
            // class type
            Type type = typeof(T);

            // check cache
            string cacheKey = GetCacheKey($"{type.Name}_instance");
            if (instanceCache.TryGetValue(cacheKey, out PropertyInfo cacheProperty))
                return (T)cacheProperty.GetValue(null);

            // resolve class instance property
            PropertyInfo instance = type.GetProperty("Instance", staticFlags);

            if (instance == null)
                foreach (PropertyInfo property in type.GetProperties(staticFlags))
                {
                    MethodInfo getMethod = property.GetGetMethod(true);

                    if (getMethod != null && getMethod.ReturnType == type)
                    {
                        instance = property;
                        break;
                    }
                }

            // invoke getter
            instanceCache[cacheKey] = instance;
            return (T)instance.GetValue(null);
        }

        public static TProperty GetProperty<T, TProperty>(T instance, string getterName) where T : class
        {
            // class type
            Type type = typeof(T);

            // check cache
            string cacheKey = GetCacheKey($"{type.Name}_{getterName}");
            if (propertyCache.TryGetValue(cacheKey, out MethodInfo cacheMethod))
                return (TProperty)cacheMethod.Invoke(instance, null);

            // resolve getter by name
            MethodInfo getter = type.GetMethod($"get_{getterName}", nonStaticFlags);
            if (getter == null)
                return default;

            // invoke getter and add to cache
            propertyCache[cacheKey] = getter;
            return (TProperty)getter.Invoke(instance, null);
        }
    }
}
