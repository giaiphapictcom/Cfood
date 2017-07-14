using System;
using V308CMS.Common;

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



    }
}