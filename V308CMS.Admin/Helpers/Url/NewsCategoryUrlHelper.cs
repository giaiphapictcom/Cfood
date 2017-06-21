using System.Web.Mvc;
using System.Web;
namespace V308CMS.Admin.Helpers.Url
{
    public static class NewsCategoryUrlHelper
    {
        public static string NewsCategoryIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string AffiliateCategoryIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
           string action = "affiliatecategory")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string NewsCategoryCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "newscategory",
           string action = "create")
        {
            switch (HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString())
            {
                case "affiliatecategory":
                    action = "affiliateCreate";
                    break;
            }
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