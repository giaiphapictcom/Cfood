using System.Web.Mvc;

namespace V308CMS.Helpers.Url
{
    public static class CaptchaUrlHelper
    {
        public static string RegisterCaptchaUrl(this UrlHelper helper, string controllerName= "Captcha", string actionName = "RegisterCaptcha")
        {
            return helper.Action(actionName, controllerName);

        }
    }
}