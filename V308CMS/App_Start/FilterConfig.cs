using System.Web;
using System.Web.Mvc;

namespace V308CMS
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