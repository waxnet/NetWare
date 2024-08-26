using NetWare.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ConfigDeserializer
{
    public static T Deserialize<T>(string data) where T : new()
    {
        var obj = new T();
        var lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var parsedData = lines.ToDictionary(
            line => line.Substring(0, line.IndexOf('=')),
            line => line.Substring(line.IndexOf('=') + 1)
        );

        DeserializeObject(parsedData, obj);
        return obj;
    }

    private static void DeserializeObject(Dictionary<string, string> data, object obj, string prefix = "")
    {
        var type = obj.GetType();
        var members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);

        foreach (var member in members)
        {
            var isField = member.MemberType == MemberTypes.Field;
            var isProperty = member.MemberType == MemberTypes.Property;

            if (!(isField || isProperty))
            {
                continue;
            }

            var attr = member.GetCustomAttribute<ConfigPropertyAttribute>();
            if (attr == null)
            {
                continue;
            }

            var memberName = prefix + member.Name;
            var memberType = isField ? ((FieldInfo)member).FieldType : ((PropertyInfo)member).PropertyType;

            if (IsSimpleType(memberType))
            {
                if (data.TryGetValue(memberName, out var value))
                {
                    var convertedValue = ConvertValue(value, memberType);
                    if (isField)
                    {
                        ((FieldInfo)member).SetValue(obj, convertedValue);
                    }
                    else
                    {
                        ((PropertyInfo)member).SetValue(obj, convertedValue);
                    }
                }
            }
            else if (IsNullableType(memberType))
            {
                if (data.TryGetValue(memberName, out var value))
                {
                    var underlyingType = Nullable.GetUnderlyingType(memberType);
                    var convertedValue = value == "null" ? null : ConvertValue(value, underlyingType);
                    if (isField)
                    {
                        ((FieldInfo)member).SetValue(obj, convertedValue);
                    }
                    else
                    {
                        ((PropertyInfo)member).SetValue(obj, convertedValue);
                    }
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(memberType))
            {
                var listType = memberType.GetGenericArguments()[0];
                var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listType));

                int index = 0;
                while (true)
                {
                    var itemPrefix = $"{memberName}[{index}].";
                    if (!data.Keys.Any(k => k.StartsWith(itemPrefix)))
                    {
                        break;
                    }

                    var nestedObject = Activator.CreateInstance(listType);
                    DeserializeObject(data, nestedObject, itemPrefix);
                    list.Add(nestedObject);
                    index++;
                }

                if (isField)
                {
                    ((FieldInfo)member).SetValue(obj, list);
                }
                else
                {
                    ((PropertyInfo)member).SetValue(obj, list);
                }
            }
            else
            {
                var nestedObject = Activator.CreateInstance(memberType);
                DeserializeObject(data, nestedObject, memberName + ".");
                if (isField)
                {
                    ((FieldInfo)member).SetValue(obj, nestedObject);
                }
                else
                {
                    ((PropertyInfo)member).SetValue(obj, nestedObject);
                }
            }
        }
    }

    private static bool IsNullableType(Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }

    private static object ConvertValue(string value, Type type)
    {
        if (value == "null")
        {
            return null;
        }

        if (type.IsEnum)
        {
            return Enum.Parse(type, value);
        }

        return Convert.ChangeType(value, type ?? type);
    }

    private static bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal);
    }
}
