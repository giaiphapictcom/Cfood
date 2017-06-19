﻿using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class NewsCategoryUrlHelper
    {
        public static string NewsCategoryIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string NewsCategoryCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }


        public static string NewsCategoryEditUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
            string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string NewsCategoryDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
           string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
         public static string NewsCategoryChangeStateUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
           string action = "changestate")
         {
            return helper.Action(action, controller, routeValue);
        }

    }
}