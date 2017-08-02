using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using V308CMS.Admin.Models;

namespace V308CMS.Admin.Helpers
{
    public class AuthenticationHelper
    {
        private const int UserTimeExpires = 10;
        public static void SignIn(MyUser data, bool remember = false)
        {
            
            var userDataString = JsonConvert.SerializeObject(data);

            //var authCookie = FormsAuthentication.GetAuthCookie(data.UserName, remember);
            //var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            //var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
            //                                              ticket.Expiration, ticket.IsPersistent, userDataString);
            //authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            //authCookie.Expires = DateTime.Now.AddDays(UserTimeExpires);


            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     data.UserName,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(UserTimeExpires),
                     remember,
                     userDataString,
                     FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

      
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            var authenCookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty)
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            HttpContext.Current.Response.Cookies.Add(authenCookie);
            var sessionCookie = new HttpCookie("ASP.NET_SessionId", "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };

            HttpContext.Current.Response.Cookies.Add(sessionCookie);
        }
        public static bool IsAuthenticate
        {
            get { return HttpContext.Current.User.Identity.IsAuthenticated; }
        }
        public static string CurrentUser
        {
            get
            {
                if (IsAuthenticate)
                {
                    return HttpContext.Current.User.Identity.Name;

                }
                return string.Empty;

            }
        }

    }
}