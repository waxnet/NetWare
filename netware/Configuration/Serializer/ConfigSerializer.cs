using NetWare.Attributes;
using System;
using System.Collections;
using System.Reflection;
using System.Text;

public static class ConfigSerializer
{
    public static string Serialize(object obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }

        var sb = new StringBuilder();
        SerializeObject(sb, obj);
        return sb.ToString();
    }

    private static void SerializeObject(StringBuilder sb, object obj, string prefix = "")
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
            var memberValue = isField ? ((FieldInfo)member).GetValue(obj) : ((PropertyInfo)member).GetValue(obj);

            if (memberValue == null)
            {
                sb.AppendLine($"{memberName}=null");
            }
            else if (IsSimpleType(memberValue.GetType()))
            {
                sb.AppendLine($"{memberName}={memberValue}");
            }
            else if (memberValue is IEnumerable enumerable)
            {
                int index = 0;
                foreach (var item in enumerable)
                {
                    var itemPrefix = $"{memberName}[{index}].";
                    SerializeObject(sb, item, itemPrefix);
                    index++;
                }
            }
            else
            {
                SerializeObject(sb, memberValue, memberName + ".");
            }
        }
    }

    private static bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal);
    }
}
