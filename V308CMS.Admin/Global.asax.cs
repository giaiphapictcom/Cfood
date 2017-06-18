using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;

namespace V308CMS.Admin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
                 
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
                        RoleId = userData.RoleId
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