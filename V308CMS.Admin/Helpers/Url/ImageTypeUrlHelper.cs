using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ImageTypeUrlHelper
    {
        public static string ImageTypeIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "imagetype",
          string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ImageTypeCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "imagetype",
         string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}