using System;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Data;

namespace V308CMS
{
    public class ThemesViewActionFilter : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //VisisterRepository visister = new VisisterRepository(mEntities);
            //visister.update();

            dynamic viewBag = filterContext.Controller.ViewBag;

            viewBag.domain = Theme.domain;
            viewBag.ThemesPath = "/Content/themes/" + Theme.domain;
            viewBag.MoneyShort = "vnđ";
        }
    }
}