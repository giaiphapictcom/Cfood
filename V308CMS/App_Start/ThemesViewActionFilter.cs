using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Data;
namespace V308CMS.App_Start
{
    public class ThemesViewActionFilter : ActionFilterAttribute
    {
        static V308CMSEntities mEntities = new V308CMSEntities();
        SiteRepository config = new SiteRepository(mEntities);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            dynamic ViewBag = filterContext.Controller.ViewBag;

            ViewBag.domain = Theme.domain;
            ViewBag.ThemesPath = "/Content/themes/" + Theme.domain;
            ViewBag.MoneyShort = "vnđ";

            ViewBag.SiteName = config.SiteConfig("site-name");
            ViewBag.Hotline = config.SiteConfig("hotline");
            ViewBag.CompanyFullname = config.SiteConfig("company-fullname");
            ViewBag.FooterCompanyContact = config.SiteConfig("company-footer-contact");
            ViewBag.CompanyHeaderAddress = config.SiteConfig("company-header-address");
            string fbPage = config.SiteConfig("facebook-page");
            fbPage = "Ketnoikhoinghiepviet";
            //fbPage = "SieuThiTienIchJ2";
            ViewBag.FacebookPage = fbPage;

        }
    }
}