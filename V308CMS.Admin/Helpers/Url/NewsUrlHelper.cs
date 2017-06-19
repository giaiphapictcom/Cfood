using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class NewsUrlHelper
    {
        public static string NewsIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string NewsCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string NewsEditUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
        string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string NewsDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "news",
        string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }


    }
}