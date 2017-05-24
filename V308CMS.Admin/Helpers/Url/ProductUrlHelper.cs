using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductUrlHelper
    {
        public static string ProductIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
          string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductEditUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
           string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}