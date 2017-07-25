using System.Web.Http;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using StackExchange.Profiling;
using V308CMS.Helpers;
using V308CMS.Models;

namespace V308CMS
{   
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SiteConfig.RegisterConfigs();
            AuthConfig.RegisterAuth();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MpStartViewEngine());
        }
        protected void Application_BeginRequest()
        {
            if (ConfigHelper.ShowPageProfile)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            if (ConfigHelper.ShowPageProfile)
            {
                MiniProfiler.Stop();
            }
        }
        private void SetCustomUser()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            if (authTicket != null)
            {
                var userData = JsonConvert.DeserializeObject<MyUser>(authTicket.UserData);
                if (userData != null)
                {
                    var principal = new CustomPrincipal(authTicket.Name)
                    {
                        UserId = userData.UserId,
                        UserName = userData.UserName,
                        Avatar = userData.Avatar,
                        Affilate = userData.Affilate

                    };                   
                    HttpContext.Current.User = principal;
                }
            }
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            SetCustomUser();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            SetCustomUser();

        }
    }
}