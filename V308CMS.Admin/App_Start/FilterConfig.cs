using System.Web;
using System.Web.Mvc;
using V308CMS.Admin.Middlewares;


namespace V308CMS.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new CheckPermissionMiddleware());

        }
    }
}