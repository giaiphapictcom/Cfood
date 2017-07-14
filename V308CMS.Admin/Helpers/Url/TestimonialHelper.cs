using System.Web.Mvc;
using System.Web;

namespace V308CMS.Admin.Helpers.Url
{
    
    public static class TestimonialHelper
    {
        public static string TestimonialIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "testimonial",
          string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string TestimonialCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "testimonial",
           string action = "create")
        {

            
            return helper.Action(action, controller, routeValue);
        }

        public static string TestimonialChangeStatusUrl(this UrlHelper helper, object routeValue = null, string controller = "testimonial",
        string action = "changestatus")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string TestimonialEditUrl(this UrlHelper helper, object routeValue = null, string controller = "testimonial",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string TestimonialDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "testimonial",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
    }
}