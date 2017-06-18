using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductStoreUrlHelper
    {
        public static string ProductStoreIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "productstore",
         string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductStoreCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "productstore",
         string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductStoreEditUrl(this UrlHelper helper, object routeValue = null, string controller = "productstore",
         string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductStoreDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "productstore",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }

}