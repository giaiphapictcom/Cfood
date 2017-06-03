using System.Web.Mvc;

namespace V308CMS.Helpers
{
    public static class CartUrlHelper
    {
        public static string AddToCartUrl(this UrlHelper helper, string controller ="cart", string action= "add")
        {
            return helper.Action(action, controller);

        }
        public static string GetCartUrl(this UrlHelper helper, string controller = "cart", string action = "index")
        {
            return helper.Action(action, controller);
        }

        public static string ViewCartUrl(this UrlHelper helper, string controller = "cart",
            string action = "viewcart")
        {
            return helper.Action(action, controller);
        }
        public static string CheckoutCartUrl(this UrlHelper helper, string controller = "cart",
            string action = "checkout")
        {
            return helper.Action(action, controller);
        }
        public static string UpdateCartUrl(this UrlHelper helper, string controller = "cart",
            string action = "updatecart")
        {
            return helper.Action(action, controller);
        }

        public static string SendOrderCartUrl(this UrlHelper helper, string controller = "cart",
            string action = "sendorder")
        {
            return helper.Action(action, controller);
        }
    }
}