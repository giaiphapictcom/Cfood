using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class MemberAddressUrlHelper
    {
        public static string MemberAddressAjaxAddressUrl(this UrlHelper helper,string controller = "memberaddress",
           string action = "ajaxaddress")
        {
            return helper.Action(action, controller);
        }

    }
}