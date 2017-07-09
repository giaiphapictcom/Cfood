using V308CMS.Common;

namespace V308CMS.Helpers
{
    public static class ConfigHelper
    {
        public static string GmailSmtpServer => Configs.GetItemConfig("host");
        public static string GMailUserName => Configs.GetItemConfig("userName");

        public static string GMailPassword => Configs.GetItemConfig("password");
        public static string GMailPort => Configs.GetItemConfig("port");

        public static string WebDomain => Configs.GetItemConfig("WebDomain");

        public static string Domain => Configs.GetItemConfig("domain");

        public static bool ShowPageProfile => Configs.GetItemConfig("ShowpageProfile") == "1";
        public static string FacebookAppId => Configs.GetItemConfig("FacebookAppId");
        public static string FacebookAppSecret => Configs.GetItemConfig("FacebookAppSecret");

        public static string GoogleAppId => Configs.GetItemConfig("GoogleAppId");
        public static string GoogleAppSecret => Configs.GetItemConfig("GoogleAppSecret");
        public static string SiteConfigName => Configs.GetItemConfig("SiteConfigName");
        public static string ResourceDomain => Configs.GetItemConfig("ResourceDomain");
        


    }
}