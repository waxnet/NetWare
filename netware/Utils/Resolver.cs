using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        private static Dictionary<string, Delegate> instanceCache = new Dictionary<string, Delegate>();
        private static Dictionary<string, Delegate> propertyCache = new Dictionary<string, Delegate>();

        // methods
        public static T GetInstance<T>() where T : class
        {
            // class type
            Type type = typeof(T);

            // check cache
            string cacheKey = $"{type.Name}_instance";
            if (instanceCache.TryGetValue(cacheKey, out var cacheDelegate))
                return ((Func<T>)cacheDelegate)();

            // resolve class instance property
            var instanceProperty = type.GetProperty("Instance", staticFlags);

            if (instanceProperty == null)
                foreach (var property in type.GetProperties(staticFlags))
                {
                    var getMethod = property.GetGetMethod(true);

                    if (getMethod != null && getMethod.ReturnType == type)
                    {
                        instanceProperty = property;
                        break;
                    }
                }

            // build expression and add to cache
            var expression = Expression.Lambda<Func<T>>(Expression.Property(null, instanceProperty)).Compile();

            instanceCache[cacheKey] = expression;

            // invoke expression
            return expression();
        }

        public static TProperty GetProperty<T, TProperty>(T instance, string getterName) where T : class
        {
            // class type
            Type type = typeof(T);

            // check cache
            string cacheKey = $"{type.Name}_{getterName}";
            if (propertyCache.TryGetValue(cacheKey, out var cacheDelegate))
                return ((Func<T, TProperty>)cacheDelegate)(instance);

            // resolve getter by name
            var getterMethod = type.GetMethod($"get_{getterName}", nonStaticFlags);
            if (getterMethod == null)
                return default;

            // build expresssion and add to cache
            var expressionParameter = Expression.Parameter(typeof(T), "instance");
            var propertyAccess = Expression.Property(expressionParameter, getterMethod);
            var expression = Expression.Lambda<Func<T, TProperty>>(propertyAccess, expressionParameter).Compile();

            propertyCache[cacheKey] = expression;

            // invoke expression
            return expression(instance);
        }
    }
}
