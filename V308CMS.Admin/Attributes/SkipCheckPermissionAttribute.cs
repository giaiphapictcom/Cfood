using System;

namespace V308CMS.Admin.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SkipCheckPermissionAttribute: Attribute
    {
        public bool SkipCheckPermission { get; set; }
        public SkipCheckPermissionAttribute(bool isSkip =true)
        {
            SkipCheckPermission = isSkip;
        }
    }
}