using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace V308CMS.Helpers
{

    public class AuthenticationHelper
    {      
        private const int UserTimeExpires = 10;
        public static void SignIn(int userId,string userName,string displayName, string avatar, string affilateId, int affilateAmount, bool remember = false)
        {

            var userDataString = JsonConvert.SerializeObject(new MyUser
            {
                UserId = userId,
                UserName = userName,
                AffilateId = affilateId,
                AffilateAmount =  affilateAmount
            });

            var authCookie = FormsAuthentication.GetAuthCookie(displayName, remember);
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                                                          ticket.Expiration, ticket.IsPersistent, userDataString);
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            authCookie.Expires = DateTime.Now.AddDays(UserTimeExpires);
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

        public static bool IsAuthenticated => HttpContext.Current.User.Identity.IsAuthenticated;

        public static string AuthenticateName => IsAuthenticated ? HttpContext.Current.User.Identity.Name : "Tài khoản";
    }
}