using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductOrderUrlHelper
    {
        public static string ProductOrderIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "order",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderUpdateDetailUrl(this UrlHelper helper, object routeValue = null, string controller = "order",
            string action = "updatedetail")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderEditUrl(this UrlHelper helper, object routeValue = null, string controller = "order",
           string action = "detail")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "order",
          string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderChangeStatusUrl(this UrlHelper helper, object routeValue = null, string controller = "order",
           string action = "changestatus")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductOrderCancelUrl(this UrlHelper helper, object routeValue = null, string controller = "order",
           string action = "cancelorder")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}