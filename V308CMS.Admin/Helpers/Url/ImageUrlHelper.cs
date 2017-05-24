using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ImageUrlHelper
    {
        public static string ImageIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "image",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ImageCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "image",
            string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}