using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Helpers
{
    public static class DateHelper
    {
        public static string ToDdmmyyyyHhm(this DateTime? dateTime)
        {
            return dateTime.HasValue? dateTime.Value.ToString("dd/MM/yyyy hh:mm") : "";


        }
    }
}