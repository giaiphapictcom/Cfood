using System.Web;
using System.Web.Mvc;

namespace V308CMS.Sale
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new V308CMS.App_Start.ThemesViewActionFilter());
        }
    }
}