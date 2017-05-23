﻿using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class AccountUrlHelper
    {
        public static string AccountLoginUrl(this UrlHelper helper, string controller = "account", string action = "login")
        {
            return helper.Action(action, controller);

        }
        public static string AccounRegisterUrl(this UrlHelper helper, string controller = "account", string action = "register")
        {
            return helper.Action(action, controller);

        }

        public static string AccountForgotPasswordUrl(this UrlHelper helper, string controller = "account", string action = "forgotgassword")
        {
            return helper.Action(action, controller);
        }

        public static string AccountActiveUrl(this UrlHelper helper, string controller = "account", string action = "active")
        {
            return helper.Action(action, controller);
        }

        public static string AccountGetTokenUrl(this UrlHelper helper, string controller = "account", string action = "gettoken")
        {
            return helper.Action(action, controller);
        }
        public static string AccountWishlistUrl(this UrlHelper helper, string controller = "account", string action = "wishlist")
        {
            return helper.Action(action, controller);
        }
        public static string AccountLogoutUrl(this UrlHelper helper, string controller = "account", string action = "logout")
        {
            return helper.Action(action, controller);

        }
        public static string AccountCheckEmail(this UrlHelper helper, string controller = "account", string action = "checkemail")
        {
            return helper.Action(action, controller);

        }
        public static string AccountChangePasswordUrl(this UrlHelper helper, string controller = "account", string action = "changepassword")
        {
            return helper.Action(action, controller);

        }

    }

}