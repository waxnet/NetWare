using System;
using System.Reflection;

namespace NetWare
{
    public static class Access
    {
        public static void SetValue<T>(T instance, string field, object newValue)
        {
            if (instance != null)
            {
                FieldInfo[] fieldInfos = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    if (fieldInfo.Name == field)
                    {
                        fieldInfo.SetValue(instance, newValue);
                        break;
                    }
                }
            }
        }

        public static object GetValue<T>(T instance, string field)
        {
            FieldInfo[] fieldInfos = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.Name == field)
                {
                    return fieldInfo.GetValue(instance);
                }
            }
            return null;
        }
    }
}
