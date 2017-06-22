using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;

namespace V308CMS.Admin.Controllers
{

    [CheckGroupPermission(false)]
    public class HomeController : BaseController
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {


            return View("IndexV2");
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login", new LoginModels());
        }
        [HttpPost]
        [ActionName("Login")]
        public ActionResult OnLogin(LoginModels login)
        {
            if (ModelState.IsValid)
            {
                var result = AdminAccountService.CheckAccount(login.Username, login.Password);

                if (result == null)
                {
                    ModelState.AddModelError("", "Sai tên tài khoản hoặc mật khẩu.");
                    return View("Login", login);

                }
                var myUser = new MyUser
                {
                    UserName = login.Username,
                    Avatar = result.Avatar,
                    UserId = result.ID,
                    RoleId = result.Role ?? 0
                };
                AuthenticationHelper.SignIn(myUser, login.Remember);
                return RedirectToAction("Index");
            }
            return View("Login", login);

        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie1);
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie2);
            return RedirectToAction("Login");
        }


    }
}