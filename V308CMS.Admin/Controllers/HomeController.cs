using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using V308CMS.Common;

namespace V308CMS.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View("Index");
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        [ActionName("Login")]
        public JsonResult OnLogin(string pUserName, string pPassWord)
        {                       
            var mEtLogin = AccountService.CheckDangNhap(pUserName, pPassWord);

            if (mEtLogin.code == 1)
            {
                //SET session cho UserId
                Session["UserId"] = mEtLogin.Admin.ID;
                Session["UserName"] = mEtLogin.Admin.UserName;
                Session["Role"] = mEtLogin.Admin.Role;
                Session["Admin"] = mEtLogin.Admin;
                //Thuc hien Authen cho User.
                FormsAuthentication.SetAuthCookie(pUserName, true);
                return Json(new { code = 1, message = "Đăng ký thành công. Tài khoản là : " + pUserName + "." });
            }
            else
            {
                return Json(new { code = 0, message = mEtLogin.message });
            }

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