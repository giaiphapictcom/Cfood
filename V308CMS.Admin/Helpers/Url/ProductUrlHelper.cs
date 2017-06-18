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
        public static string ProductDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
          string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductDeleteAllUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
          string action = "deleteall")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string ProductHideAllUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
         string action = "hideall")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductChangeStatusUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
         string action = "changestatus")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductUpdateOrderUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
         string action = "updateproductorder")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductUpdateQuantityUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
        string action = "UpdateProductQuantity")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductUpdatePriceUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
        string action = "updateprice")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string ProductUpdateNppUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
        string action = "updatenpp")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string ProductUpdateCodeUrl(this UrlHelper helper, object routeValue = null, string controller = "product",
        string action = "updatecode")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}