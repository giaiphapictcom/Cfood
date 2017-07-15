using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class FacebookPluginUrlHelper
    {
        public static string FacebookShareUrl(this UrlHelper helper, string url)
        {
            return $"https://www.facebook.com/sharer/sharer.php?u={url}&display=popup&ref=plugin&src=like&kid_directed_site=0&app_id={ConfigHelper.FacebookAppId}";
        }


    }
}