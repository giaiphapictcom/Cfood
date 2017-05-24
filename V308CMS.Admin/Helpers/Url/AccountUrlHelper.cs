using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class AccountUrlHelper
    {
        public static string AccountIndexUrl(this UrlHelper helper, object routeValue = null,string controller = "account",
            string action = "index")
        {
          return  helper.Action(action, controller, routeValue);
        }
        public static string AccountInfoUrl(this UrlHelper helper, object routeValue = null, string controller = "account",
            string action = "info")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}