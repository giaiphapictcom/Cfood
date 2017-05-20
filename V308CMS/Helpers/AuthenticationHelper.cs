﻿using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using V308CMS.Models;

namespace V308CMS.Helpers
{
    public class AuthenticationHelper
    {
        private const int UserTimeExpires = 10;
        public static void SignIn(LoginModels data, bool remember = false)
        {
            var userDataString = JsonConvert.SerializeObject(data);

            var authCookie = FormsAuthentication.GetAuthCookie(data.Email, remember);
            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                                                          ticket.Expiration, ticket.IsPersistent, userDataString);
            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            authCookie.Expires = DateTime.Now.AddDays(UserTimeExpires);
            HttpContext.Current.Response.Cookies.Add(authCookie);

            SetCurrentUser(data.Email);

        }

        private static void SetCurrentUser(string userName)
        {
            HttpContext.Current.Session["UserName"] = userName;


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
            get
            {
                return (
                     HttpContext.Current.User.Identity.IsAuthenticated ||
                     HttpContext.Current.Session["UserName"] != null
                   );
            }
        }
        public static string CurrentUser
        {
            get
            {
                if (IsAuthenticate)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        return HttpContext.Current.User.Identity.Name;
                    }
                    if (HttpContext.Current.Session["UserName"] != null)
                    {
                        return HttpContext.Current.Session["UserName"].ToString();
                    }

                }
                return string.Empty;

            }
        }

    }
}