using System.Collections.Generic;

namespace V308CMS.Common
{
    public static class ListHelper
    {
        public static bool IsHasData<T>(this List<T> list)
        {
            return (list != null && list.Count > 0);

        }

    }
}