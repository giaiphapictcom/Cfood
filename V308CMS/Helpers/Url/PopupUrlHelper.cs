using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class PopupUrlHelper
    {
        public static string PopupLoginUrl(this UrlHelper helper, string controller = "popup",
        string action = "login")
        {
            return helper.Action(action, controller);
        }
        public static string PopupRegisterUrl(this UrlHelper helper, string controller = "popup",
        string action = "register")
        {
            return helper.Action(action, controller);
        }

        public static string PopupForgotPasswordUrl(this UrlHelper helper, string controller = "popup",
       string action = "forgotpassword")
        {
            return helper.Action(action, controller);
        }

        public static string LoginAndBuyUrl(this UrlHelper helper, string controller = "popup",
        string action = "loginandbuy")
        {
            return helper.Action(action, controller);
        }

    }
}