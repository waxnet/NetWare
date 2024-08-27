using NetWare.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace NetWare.Extensions;

public static class EnumExtensions
{
    public static string ConvertToString<T>(this T value) where T : Enum
    {
        var stringReinterpretation = GetAttribute<T, StringReinterpretationAttribute>(value);
        return stringReinterpretation?.Value ?? Enum.GetName(typeof(T), value);
    }

    public static string[] GetNames<T>() where T : Enum
    {
        return Enum.GetNames(typeof(T));
    }

    public static T[] GetValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).OfType<T>().ToArray();
    }

    public static TAttribute GetAttribute<T, TAttribute>(this T value)
        where T : Enum
        where TAttribute : Attribute
    {
        var type = typeof(T);
        var fieldInfo = type.GetField(value.ToString());

        if (fieldInfo is null)
            return null;

        return fieldInfo.GetCustomAttribute<TAttribute>();
    }
}
