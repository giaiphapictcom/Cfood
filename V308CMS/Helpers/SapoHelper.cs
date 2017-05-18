using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Helpers
{
    public static class SapoHelper
    {
        public static string ToSmartSapo(this string title)
        {
            return string.IsNullOrWhiteSpace(title)
                ? ""
                : (title.Length > 150 ? title.Substring(0, 150) + "..." : title);

        }

    }
}