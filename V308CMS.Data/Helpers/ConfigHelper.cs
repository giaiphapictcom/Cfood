using V308CMS.Common;

namespace V308CMS.Data.Helpers
{
    public static class ConfigHelper
    {
        public static string GmailSmtpServer
        {
            get { return Configs.GetItemConfig("host"); }
        }
        public static string GMailUserName {
            get { return Configs.GetItemConfig("userName"); }
        }

        public static string GMailPassword
        {
            get { return Configs.GetItemConfig("password"); }
        }
        public static string GMailPort
        {
            get { return Configs.GetItemConfig("port"); }
        }

        public static string WebDomain
        {
            get { return Configs.GetItemConfig("WebDomain"); }
        }
        public static string Domain
        {
            get { return Configs.GetItemConfig("domain"); }
        }
    }
}