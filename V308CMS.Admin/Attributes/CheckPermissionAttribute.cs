using System;

namespace V308CMS.Admin.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CheckPermissionAttribute : Attribute
    {
        public CheckPermissionAttribute(int index, string name)
        {
            Index = index;
            Name = name;

        }
       public int Index { get; set; }
       public string Name { get; set; }

    }
}