using System;
using System.Reflection;

namespace V308CMS.Common
{
    public static class ObjectHelper
    {
        public static T ResetValue<T>(this T objectItem)
        {
            Type type = objectItem.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo t in properties)
                t.SetValue(objectItem, null);
            return objectItem;
        }
    }
}