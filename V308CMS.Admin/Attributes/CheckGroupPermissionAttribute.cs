using System;

namespace V308CMS.Admin.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class CheckGroupPermissionAttribute : Attribute
    {
        public bool HasCheckPermission { get; set; }
        public string Description { get; set; }
      
        public CheckGroupPermissionAttribute(bool hasCheckPermission, string description ="")
        {
            HasCheckPermission = hasCheckPermission;
            Description = description;
        }
    }
}