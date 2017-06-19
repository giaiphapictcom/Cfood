using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class CountryUrlHelper
    {
        public static string CountryIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "country",
             string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string CountryCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "country",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string CountryEditUrl(this UrlHelper helper, object routeValue = null, string controller = "country",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string CountryDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "country",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}