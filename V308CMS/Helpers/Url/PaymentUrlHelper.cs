using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class PaymentUrlHelper
    {
        public static string PaymentIndexUrl(this UrlHelper helper, string controller = "payment",
         string action = "index")
        {
            return helper.Action(action, controller);
        }

        public static string PaymentSuccessUrl(this UrlHelper helper, string controller = "payment",
         string action = "success")
        {
            return helper.Action(action, controller);
        }

        public static string PaymentUseVoucherUrl(this UrlHelper helper, string controller = "payment",
        string action = "usevoucher")
        {
            return helper.Action(action, controller);
        }

        public static string PaymentUseAffilateUrl(this UrlHelper helper, string controller = "payment",
        string action = "useaffilate")
        {
            return helper.Action(action, controller);
        }
    }
}