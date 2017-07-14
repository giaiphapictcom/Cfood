using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class VoucherUrlHelper
    {
        public static string VoucherIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "Voucher",
           string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }


        public static string VoucherCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "Voucher",
           string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }



        public static string VoucherEditUrl(this UrlHelper helper, object routeValue = null, string controller = "Voucher",
            string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string VoucherDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "Voucher",
           string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string VoucherChangeStateUrl(this UrlHelper helper, object routeValue = null, string controller = "Voucher",
          string action = "changestate")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}
