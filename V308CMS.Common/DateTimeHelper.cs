using System;

namespace V308CMS.Common
{
    public static class DateTimeHelper
    {
        public static string ToDdmmyyyyHhm(this DateTime? dateTime)
        {
            return dateTime.HasValue? dateTime.Value.ToString("dd/MM/yyyy hh:mm") : "";


        }
    }
}