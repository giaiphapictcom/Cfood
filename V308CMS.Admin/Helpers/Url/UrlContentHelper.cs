using System;
using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class UrlContentHelper
    {
        public static string CreateNewImageUrl(this UrlHelper helper)
        {
            return "/Content/Images/them-moi.png";
        }

        public static string ToImageUrl(this string path)
        {
            return !string.IsNullOrWhiteSpace(path)?
                $"{ConfigHelper.ImageDomain}/{path}": 
                $"{ConfigHelper.ImageDomain}/no-image.jpg";
        }

        public static string ToImageOriginalPath(this string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (path.IndexOf("no-image.jpg", StringComparison.Ordinal)>0)
                {
                    path = "";
                }
                path = path.Replace(ConfigHelper.ImageDomain, "");
            }
            return path;
        }


        public static string ReplaceImageDomain(this string path)
        {
            return !string.IsNullOrWhiteSpace(path)  && path.IndexOf(ConfigHelper.ImageDomain, StringComparison.Ordinal)>0? 
                path.Replace(ConfigHelper.ImageDomain,""): 
                path;
        }
    }
}