﻿using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class UserUrlHelper
    {
        public static string UserIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "user",
             string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string UserCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "user",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string UserEditUrl(this UrlHelper helper, object routeValue = null, string controller = "user",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string UserDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "user",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string UserChangeStatusUrl(this UrlHelper helper, object routeValue = null, string controller = "user",
         string action = "changestatus")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string UserChangePasswordUrl(this UrlHelper helper, object routeValue = null, string controller = "user",
        string action = "changepassword")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string UserCheckEmailUrl(this UrlHelper helper, object routeValue = null, string controller = "user",
        string action = "checkemail")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}