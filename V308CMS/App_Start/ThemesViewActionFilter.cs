using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            SiteRepository config = new SiteRepository(mEntities);
            dynamic viewBag = filterContext.Controller.ViewBag;

            try {
                viewBag.domain = Theme.domain;
                viewBag.ThemesPath = "/Content/themes/" + Theme.domain;
                viewBag.MoneyShort = "vnđ";
                var configs = new[]
                {
                    "site-name",
                    "hotline",
                    "company-fullname",
                    "company-footer-contact",
                    "company-header-address",
                    "facebook-page",
                    "gplus",
                    "zalo",
                    "youtube-channel",
                    "product-view-text",
                    "home-text-alias",
                    "subscribe-news"
                };
              
                var siteConfigs = config.LoadSiteConfig(
                    configs
                  );
                if (siteConfigs.Any())
                {
                    viewBag.SiteName = config.ReadSiteConfig(siteConfigs, "site-name");
                    viewBag.Hotline = config.ReadSiteConfig(siteConfigs, "hotline");
                    viewBag.CompanyFullname = config.ReadSiteConfig(siteConfigs, "company-fullname");
                    viewBag.FooterCompanyContact = config.ReadSiteConfig(siteConfigs,"company-footer-contact");
                    viewBag.CompanyHeaderAddress = config.ReadSiteConfig(siteConfigs,"company-header-address");
                    viewBag.FacebookPage = config.ReadSiteConfig(siteConfigs,"facebook-page");
                    viewBag.GPlus = config.ReadSiteConfig(siteConfigs,"gplus");
                    viewBag.Zalo = config.ReadSiteConfig(siteConfigs,"zalo");
                    viewBag.Youtube = config.ReadSiteConfig(siteConfigs,"youtube-channel");
                    viewBag.ProductViewText = config.ReadSiteConfig(siteConfigs,"product-view-text");
                    viewBag.HomeAliasText = config.ReadSiteConfig(siteConfigs, "home-text-alias");
                    viewBag.SubscribeNews = config.ReadSiteConfig(siteConfigs, "subscribe-news");
                }
                
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {
                mEntities.Dispose();
                config.Dispose();
            }
            

        }
    }
}