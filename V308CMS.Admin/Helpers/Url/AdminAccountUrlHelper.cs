using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class AdminAccountUrlHelper
    {
        public static string AdminAccountIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountEditUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountInfoUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
          string action = "info")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountChangePasswordUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
         string action = "changepassword")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountCheckUserNameUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
        string action = "checkusername")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string AdminAccountChangeStatusUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
        string action = "changestatus")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string AdminAccountDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "adminaccount",
        string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}