using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class AsyncUrlHelper
    {
        public static string CountAsyncUrl(this UrlHelper helper, object routeValue = null, string controller = "async",
            string action = "countasync")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string LoadLastestOrderAsyncUrl(this UrlHelper helper, object routeValue = null, string controller = "async",
            string action = "loadlastestorderasync")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string LoadLastestUserAsyncUrl(this UrlHelper helper, object routeValue = null, string controller = "async",
            string action = "loadlastestuserasync")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string LoadLastestContactAsyncUrl(this UrlHelper helper, object routeValue = null, string controller = "async",
            string action = "loadlastestcontactasync")
        {
            return helper.Action(action, controller, routeValue);
        }


    }
}