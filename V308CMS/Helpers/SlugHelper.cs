using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using V308CMS.Common;

namespace V308CMS.Helpers
{
    public static class SlugHelper
    {
        public static string ToSlug(this string title)
        {
            return Ultility.URITitle(title);
        }
    }
}