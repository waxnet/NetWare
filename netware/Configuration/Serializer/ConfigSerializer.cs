using NetWare.Attributes;

using System;
using System.Collections;
using System.Reflection;
using System.Text;

public static class ConfigSerializer
{
    // serializer
    public static string Serialize(object config)
    {
        if (config is null)
            return string.Empty;

        var stringBuilder = new StringBuilder();
        SerializeObject(stringBuilder, config);
        return stringBuilder.ToString();
    }
    private static void SerializeObject(StringBuilder stringBuilder, object config, string prefix = "")
    {
        // object data
        var configType = config.GetType();
        var configMembers = configType.GetMembers(BindingFlags.Public | BindingFlags.Instance);

        foreach (var member in configMembers)
        {
            // get and check member type
            bool isField = member.MemberType == MemberTypes.Field;
            bool isProperty = member.MemberType == MemberTypes.Property;
            if (!(isField || isProperty))
                continue;

            // get attribute
            var attribute = member.GetCustomAttribute<ConfigProperty>();
            if (attribute is null)
                continue;

            // member data
            string memberName = prefix + member.Name;
            object memberValue = isField ? ((FieldInfo)member).GetValue(config) : ((PropertyInfo)member).GetValue(config);

            // serialize
            if (memberValue is null)
                stringBuilder.AppendLine($"{memberName}=null");

            else if (IsSimpleType(memberValue.GetType()))
                stringBuilder.AppendLine($"{memberName}={memberValue}");

            else if (memberValue is IEnumerable enumerable) {
                int index = 0;
                
                foreach (object item in enumerable)
                {
                    string itemPrefix = $"{memberName}[{index}].";
                    SerializeObject(stringBuilder, item, itemPrefix);

                    index++;
                }
            }
            
            else
                SerializeObject(stringBuilder, memberValue, memberName + ".");
        }
    }

    // checks
    private static bool IsSimpleType(Type type)
    {
        return
            type.IsPrimitive ||
            type.IsEnum ||
            type == typeof(string) ||
            type == typeof(decimal);
    }
}
