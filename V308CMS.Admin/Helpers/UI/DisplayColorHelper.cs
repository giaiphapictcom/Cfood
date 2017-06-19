using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.UI
{
    public static class DisplayColorHelper
    {
        public static MvcHtmlString ToShowColor(this string code)
        {
            return  new MvcHtmlString( string.Format("<span class='btn-colorselector' style='background-color: {0};'></span>",code) );
        }
    }
}