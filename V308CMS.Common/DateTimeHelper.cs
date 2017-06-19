using System;

namespace V308CMS.Common
{
    public static class DateTimeHelper
    {
        public static string ToDdmmyyyyHhm(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("dd/MM/yyyy hh:mm") : "";


        }
        public static string ToDdmmyyyy(this DateTime? dateTime)
        {
            return dateTime.HasValue ? InternalToDdmmyyyy(dateTime.Value) : "";

        }
        public static string ToDdmmyyyy(this DateTime dateTime)
        {
            return InternalToDdmmyyyy(dateTime);

        }
        public static string ToDdmmyy(this DateTime dateTime)
        {
            return InternalToDdmmyy(dateTime);

        }
        private static string InternalToDdmmyy(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yy");
        }
        private static string InternalToDdmmyyyy(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}