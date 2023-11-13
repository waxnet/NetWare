using System.Reflection;

namespace NetWare
{
    public class Access
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
    }
}
