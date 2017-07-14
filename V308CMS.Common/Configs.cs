using System;
using System.Configuration;

namespace V308CMS.Common
{
    public static class Configs
    {

        /// <summary>
        /// Lấy an item config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetItemConfig(string key)
        {
            try
            {
                var value = (ConfigurationManager.AppSettings[key]?? string.Empty);
                if (!string.IsNullOrWhiteSpace(value))
                    value = value.Trim();
                return value;
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }

        public static string GetAppConfig(string key)
        {
            try
            {
                var value = (ConfigurationManager.AppSettings[key] ?? string.Empty);
                if (!string.IsNullOrWhiteSpace(value))
                    value = value.Trim();
                return value;
            }
            catch (Exception)
            {

                return string.Empty;
            }

        }
    }
}
