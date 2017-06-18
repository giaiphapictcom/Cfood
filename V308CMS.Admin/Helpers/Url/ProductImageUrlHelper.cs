using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductImageUrlHelper
    {
        public static string ProductImageEditUrl(this UrlHelper helper, object routeValue = null, string controller = "productimage",
       string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductImageDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "productimage",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}