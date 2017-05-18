using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Helpers
{
    public static class ListHelper
    {
        public static bool IsHasData<T>(this List<T> list)
        {
            return (list != null && list.Count > 0);

        }

    }
}