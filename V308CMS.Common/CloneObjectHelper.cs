using System;
using System.ComponentModel;
using System.Linq;

namespace V308CMS.Common
{
    public static class CloneObjectHelper
    {
        public static TConvert CloneTo<TConvert>(this object entity, string[] skipProperties = null) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name.ToLower() == property.Name.ToLower());
                var isSkipProperty = ((skipProperties != null) && skipProperties.Contains(property.Name)) == false;
                if (convertProperty != null && isSkipProperty)
                {
                    var value = entityProperty.GetValue(entity);
                    var ConvertType = convertProperty.PropertyType;
                    var TypeCurrent = entityProperty.PropertyType;
                    if (ConvertType == TypeCurrent) {
                        convertProperty.SetValue(convert, Convert.ChangeType(value, ConvertType));
                    }
                    
                }
            }

            return convert;
        }
    }
}