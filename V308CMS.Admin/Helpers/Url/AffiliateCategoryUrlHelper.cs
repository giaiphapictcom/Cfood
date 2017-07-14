using System.Web.Mvc;
using System.Web;
namespace V308CMS.Admin.Helpers.Url
{
    public static class AffiliateCategoryUrlHelper
    {
        public static string CategoryIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "AffiliateCategory",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }


        public static string CategoryCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "AffiliateCategory",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }

        

        public static string CategoryEditUrl(this UrlHelper helper, object routeValue = null, string controller = "AffiliateCategory",
            string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string CategoryDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "AffiliateCategory",
           string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
         public static string CategoryChangeStateUrl(this UrlHelper helper, object routeValue = null, string controller = "AffiliateCategory",
           string action = "changestate")
         {
            return helper.Action(action, controller, routeValue);
        }

    }
}