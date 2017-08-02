using System;

namespace V308CMS.Common
{
    public static class DateTimeHelper
    {
        public static string ToDdMmHhmm(this DateTime? dateTime)
        {
            return dateTime?.ToString("dd/MM hh:mm") ?? "";


        }
        public static string ToDdmmyyyyHhm(this DateTime? dateTime)
        {
            return dateTime?.ToString("dd/MM/yyyy hh:mm") ?? "";


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

        public static long toUnixTime(this DateTime dateTime)
        {
            //DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //return (long)(dateTime - sTime).TotalSeconds;

            return (long)Math.Truncate((dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }
    }
}