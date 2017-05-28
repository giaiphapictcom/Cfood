using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Admin.Helpers
{
    public static class ContentHelper
    {
        public static string ToTitle(this string title, int limit =50, string limitText = "...")
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return "";
            }
            return title.Length > limit ? title.Substring(0, limit) + limitText : title;


        }
    }
}