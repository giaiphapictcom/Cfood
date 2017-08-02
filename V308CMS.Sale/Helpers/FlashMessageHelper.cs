
using System.Web.Mvc;

namespace V308CMS.Sale.Helpers
{
    public static class FlashMessageHelper
    {
        public static string GetFlashMessage(this ViewContext  context,string key="Message")
        {
            return context.TempData[key] != null ? context.TempData[key].ToString() : "";
        }

        public static string GetFlashError(this ViewContext context, string key = "Error")
        {
            return context.TempData[key] != null ? context.TempData[key].ToString() : "";
        }
    }
}