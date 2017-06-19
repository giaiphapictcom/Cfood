using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ContactUrlHelper
    {
        public static string ContactIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "contact",
            string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
       
        public static string ContactEditUrl(this UrlHelper helper, object routeValue = null, string controller = "contact",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ContactDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "contact",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}