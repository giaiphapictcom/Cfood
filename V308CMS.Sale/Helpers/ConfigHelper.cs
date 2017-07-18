using System;
using V308CMS.Common;
using V308CMS.Data;

namespace V308CMS.Sale.Helpers
{
    public static class ConfigHelper
    {
        

        public static string ImageDomain = Configs.GetItemConfig("ImageDomain");
        public static string UploadFolder = Configs.GetItemConfig("UploadFolder");
        public static int MaxImageSize = Convert.ToInt32(Configs.GetItemConfig("MaxImageSize"));
        public static string VirtualUploadPath = Configs.GetItemConfig("VirtualUploadPath");
        public static string SkipCheckPermission = "SkipCheckPermission";
        public static string ProductCode = "MP";

        public static string SiteAffiliate = "affiliate";
        public static string FacebookAppId = Configs.GetAppConfig("FacebookAppId");

        public static string FacebookAppSecret = Configs.GetItemConfig("FacebookAppSecret");
        public static string FacebookLoginCallback = Configs.GetItemConfig("FacebookLoginCallback");
        public static string GoogleAppId = Configs.GetItemConfig("GoogleAppId");
        public static string GoogleAppSecret = Configs.GetItemConfig("GoogleAppSecret");
        public static string GoogleLoginCallback = Configs.GetItemConfig("GoogleLoginCallback");
        public static string RecaptchaSecretKey = "";
        public static string RecaptchaSitekey = "";
        







    }
}