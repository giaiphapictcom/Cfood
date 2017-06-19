using System;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Helpers
{
    public static class EnumTypeHelper
    {
        public static string ToEmailTypeText(this byte type)
        {
            return DataHelper.GetStringEnum<EmailType>(type);

        }
     
    }
}