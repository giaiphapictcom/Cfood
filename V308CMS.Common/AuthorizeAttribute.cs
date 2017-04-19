using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace V308CMS.Common
{

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(
                            AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || filterContext.HttpContext.Session["UserId"] == null || filterContext.HttpContext.Session["Role"] == null || filterContext.HttpContext.Session["Admin"] == null)
            {
                string loginUrl = "/Admin/Login";
                filterContext.Result = new RedirectResult(loginUrl);
            }
        }
    }

    public class CustomAuthorize2Attribute : AuthorizeAttribute
    {
        public override void OnAuthorization(
                            AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string loginUrl = "/";
                filterContext.Result = new RedirectResult(loginUrl);
            }
        }
    }
}