using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProfileUrlHelper
    {
        public static string ProfileChangePasswordUrl(this UrlHelper helper, object routeValue = null,
            string controller = "profile",
            string action = "changepassword")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProfileUpdateProfileUrl(this UrlHelper helper, object routeValue = null,
           string controller = "profile",
           string action = "updateprofile")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}