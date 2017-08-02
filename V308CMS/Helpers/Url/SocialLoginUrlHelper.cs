using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class SocialLoginUrlHelper
    {
        public static string SocialLoginFacebookUrl(this UrlHelper helper, string returnUrl = "", string controller = "sociallogin", string action = "loginfacebook")
        {
            return string.IsNullOrWhiteSpace(returnUrl)? helper.Action(action, controller): helper.Action(action, controller, new {returnUrl });
        }
        public static string SocialLoginGoogleUrl(this UrlHelper helper,  string returnUrl = "", string controller = "sociallogin", string action = "logingoogle")
        {
            return string.IsNullOrWhiteSpace(returnUrl) ? helper.Action(action, controller) : helper.Action(action, controller, new { returnUrl });
        }
    }
}