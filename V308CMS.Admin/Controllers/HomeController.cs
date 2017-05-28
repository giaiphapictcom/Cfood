using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;

namespace V308CMS.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        
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
                var mEtLogin = AccountService.CheckDangNhap(login.Username, login.Password);

                if (mEtLogin.code == 1){
                    
                    AuthenticationHelper.SignIn(new MyUser
                    {
                        UserId = mEtLogin.Admin.ID,
                        UserName = mEtLogin.Admin.UserName,
                        Role = mEtLogin.Admin.Role ?? 0,
                        Admin = mEtLogin.Admin
                    });
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Sai tên tài khoản hoặc mật khẩu.");
            }
            return View("Login",login);
  
        }
        [CustomAuthorize]
        public ActionResult ChucNang()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie1);
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "") {Expires = DateTime.Now.AddYears(-1)};
            Response.Cookies.Add(cookie2);
            return RedirectToAction("Login");
        }
    }
}