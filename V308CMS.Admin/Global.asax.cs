using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;

namespace V308CMS.Admin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
                 
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (authCookie == null) return;
            //var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            //if (authTicket != null)
            //{
            //    var serializeModel = JsonConvert.DeserializeObject<MyUser>(authTicket.UserData);
            //    if (serializeModel != null)
            //    {
            //        var newUser = new CustomPrincipal
            //        {
            //            UserId = serializeModel.UserId,
            //            UserName = serializeModel.UserName,
            //            Role = serializeModel.Role,
            //            Admin = serializeModel.Admin
            //        };
            //        HttpContext.Current.User = newUser;
            //    }
                
              
            //}
        }
    }
}