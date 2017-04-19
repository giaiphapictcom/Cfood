using System.Web;
using System.Web.Mvc;

namespace V308CMS.Revenue
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}