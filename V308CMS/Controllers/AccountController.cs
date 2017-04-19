using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.Text;
using V308CMS.Common;
using V308CMS.Data;
using System.Web.Security;
namespace V308CMS.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult ProfileUser()
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            MarketRepository marketRepository = new MarketRepository(mEntities);
            AccountRepository accountRepository = new AccountRepository(mEntities);
            HeaderPage mHeaderPage = new HeaderPage();
            //List<Market> mList;
            Account mAccount;
            try
            {
                //Check xem dang nhap chưa thi hien thi menu khac
                // && Session["ShopCart"] != null
                if (HttpContext.User.Identity.IsAuthenticated == true && Session["UserId"] != null)
                {
                    //lay thong tin chi tiet user
                    mAccount = accountRepository.LayTinTheoId(Int32.Parse(Session["UserId"].ToString()));
                    mHeaderPage.IsAuthenticated = true;
                    mHeaderPage.Account = mAccount;
                }
                return View(mHeaderPage);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Content("<dx></dx>");
            }
            finally
            {
                mEntities.Dispose();
                marketRepository.Dispose();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckDangNhap(string pUserName, string pPassWord)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ETLogin mETLogin = null;
            try
            {
                mETLogin = accountRepository.CheckDangNhapHome(pUserName, pPassWord);
                if (mETLogin.code == 1)
                {
                    //SET session cho UserId
                    Session["UserId"] = mETLogin.Account.ID;
                    Session["UserName"] = mETLogin.Account.UserName;
                    if (Session["ShopCart"] == null)
                        Session["ShopCart"] = new ShopCart();                  
                    //Thuc hien Authen cho User.
                    FormsAuthentication.SetAuthCookie(pUserName, true);
                    return Json(new { code = 1, message = "Đăng nhập thành công. Tài khoản là : " + pUserName + "." });
                }
                else
                {
                    return Json(new { code = 0, message = mETLogin.message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        [HttpPost]
        public JsonResult CheckDangKy(string pUserName, string pPassWord, string pPassWord2, string pEmail, string pFullName, string pMobile)
        {
            V308CMSEntities mEntities = new V308CMSEntities();
            AccountRepository accountRepository = new AccountRepository(mEntities);
            ETLogin mETLogin = null;
            try
            {
                mETLogin = accountRepository.CheckDangKyHome(pUserName, pPassWord, pPassWord2, pEmail, pFullName, pMobile);
                if (mETLogin.code == 1)
                {
                    //SET session cho UserId
                    Session["UserId"] = mETLogin.Account.ID;
                    Session["UserName"] = mETLogin.Account.UserName;
                    if (Session["ShopCart"] == null)
                        Session["ShopCart"] = new ShopCart();      
                    //Thuc hien Authen cho User.
                    FormsAuthentication.SetAuthCookie(pUserName, true);
                    return Json(new { code = 1, message = "Đăng ký thành công. Tài khoản là : " + pUserName + "." });
                }
                else
                {
                    return Json(new { code = 0, message = mETLogin.message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, message = "Có lỗi xảy ra. Vui lòng thử lại." });
            }
            finally
            {
                mEntities.Dispose();
                accountRepository.Dispose();
            }

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            Session["ShopCart"] = null;
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            return Redirect("/");
        }

    }
}
