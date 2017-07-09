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
        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            SiteRepository config = new SiteRepository();
            dynamic ViewBag = filterContext.Controller.ViewBag;

            ViewBag.linkDomain = System.Configuration.ConfigurationManager.AppSettings["link_domain"];
            ViewBag.productLink = System.Configuration.ConfigurationManager.AppSettings["product_link"];
            try {
                ViewBag.domain = Theme.domain;
                ViewBag.ThemesPath = "/Content/themes/" + Theme.domain;
                ViewBag.MoneyShort = "vnđ";

                ViewBag.SiteName = config.SiteConfig("site-name");
                ViewBag.Hotline = config.SiteConfig("hotline");
                ViewBag.CompanyFullname = config.SiteConfig("company-fullname");
                ViewBag.FooterCompanyContact = config.SiteConfig("company-footer-contact");
                ViewBag.CompanyHeaderAddress = config.SiteConfig("company-header-address");
                ViewBag.FacebookPage = config.SiteConfig("affiliate-facebook-page");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                mEntities.Dispose();
                //config.Dispose();
            }
            

        }
    }
}