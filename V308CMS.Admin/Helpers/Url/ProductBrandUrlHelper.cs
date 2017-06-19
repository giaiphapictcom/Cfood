using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductBrandUrlHelper
    {
        public static string ProductBrandIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "productbrand",
             string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductBrandCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "productbrand",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductBrandEditUrl(this UrlHelper helper, object routeValue = null, string controller = "productbrand",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductBrandDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "productbrand",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}