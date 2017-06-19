using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class ContactUrlHelper
    {
        public static string ContactIndexUrl(this UrlHelper helper, string controller = "contact", string action = "index")
        {
            return helper.Action(action, controller);
        }
    }
}