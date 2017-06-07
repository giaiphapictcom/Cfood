﻿using System;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Helpers;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    
    public class MemberController : BaseController
    {
        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            var result = AccountService.CheckEmail(email);
            return Json(result) ;

        }
        private ActionResult RedirectToUrl(string url, string defaultUrl ="/")
        {
            if (url.IsLocalUrl())
            {
                return Redirect(url);
            }
            return Redirect("/");

        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (AuthenticationHelper.IsAuthenticate) {
                return RedirectToUrl(returnUrl);

            }            
            return View("Account.Login", new LoginModels
            {
                ReturnUrl = returnUrl
            });
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Login")]        
        public ActionResult HandleLogin(LoginModels login)
        {
            if (ModelState.IsValid)
            {
                var checkLogin = AccountService.CheckAccount(login.Email, login.Password);
                if (checkLogin == "invalid")
                {
                    ViewBag.Error = "Tên tài khoản hoặc Mật khẩu không chính xác.";               
                    login.Password = "";
                    return View("Account.Login", login);
                } 
                 if (checkLogin == "not_active")
                {
                    var activeAccountUrl = string.Format("{0}account/active", ConfigHelper.WebDomain);
                    ViewBag.Error = string.Format(
                        "Tài khoản của bạn chưa được kích hoạt, click vào <a href='{0}' title='Kích hoạt tài khoản' style='color: #007FF0'> đây</a> để kích hoạt tài khoản của bạn.",
                        activeAccountUrl);
                 
                    login.Password = "";
                    return View("Account.Login", login);
                }
                AuthenticationHelper.SignIn(login);
                return RedirectToUrl(login.ReturnUrl);


            }           
            return View("Account.Login", login);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            AuthenticationHelper.SignOut();
            return Redirect("/");
           
        }
        public ActionResult Register()
        {           
            return View("Account.Register", new RegisterModels());
        }

        private string getToken(string email, bool forForgotPassword = false)
        {
           return forForgotPassword? EncryptionMD5.ToMd5(string.Format("{0}|{1}|forgot-die", email, DateTime.Now.Ticks)):EncryptionMD5.ToMd5(string.Format("{0}|{1}", email, DateTime.Now.Ticks));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public ActionResult HandleRegister(RegisterModels register)
        {
            if (ModelState.IsValid)
            {
                var salt = StringHelper.GenerateString(6);
                var token = getToken(register.Email);
                var tokenExpireDate = DateTime.Now.AddDays(1);
                
                var result = AccountService.Insert(register.Email, register.Password, salt, token, tokenExpireDate);
                if (result == "exist"){
                    register.ResetPasswordValue();
                    ViewBag.Error = "Địa chỉ Email này đã được sử dụng để đăng ký, vui lòng sử dụng tên tài khoản khác.";                  
                    return View("Account.Register", register);
                }
                else{
                    var activeAccountUrl = string.Format("{0}account/active", ConfigHelper.WebDomain);
                    var body =
                        string.Format(
                            "Cảm ơn bạn đã đăng ký tài khoản trên hệ thống của {0}. Mã kích hoạt tài khoản của bạn là {1}. Click vào <a style='color: #007FF0' href='{2}' title='Kích hoạt tài khoản'> đây</a> để kích hoạt tài khoản của bạn.",
                            ViewBag.SiteName, token, activeAccountUrl);
                    InternalSendEmail(register.Email, "Đăng ký tài khoản", body);
                    return RedirectToAction("Active");
                }
             
            }
            register.ResetPasswordValue();         
            return View("Account.Register", register);

        }
        
        public ActionResult Active()
        {           
            return View("Account.Active");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Active")]
        public ActionResult HandleActive(ActiveModels active)
        {
            if (ModelState.IsValid)
            {
                var activeResult = AccountService.Active(active.Token);
                if (activeResult == "invalid"){
                   ModelState.AddModelError("Token","Mã kích hoạt tài khoản không đúng.");
                   return View("Account.Active", active);
                }
                if (activeResult == "expire"){
                    var getTokenUrl = string.Format("{0}account/gettoken", ConfigHelper.WebDomain);
                    ModelState.AddModelError("Token", string.Format("Mã kích hoạt tài khoản đã hết hạn. Click vào <a style='color: #007FF0' href='{0}' title='Kích hoạt tài khoản'> đây</a> để lấy mã kích hoạt. ", getTokenUrl) );
                    return View("Account.Active", active);
                }
                return RedirectToAction("Login");
            }            
            return View("Account.Active", active);
        }

        public ActionResult GetToken()
        {
            return View("Account.GetToken", new GetTokenModels());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GetToken")]
        public ActionResult HandleGetToken(GetTokenModels token)
        {
            if (ModelState.IsValid)
            {
                var result = AccountService.UpdateToken(token.Email, getToken(token.Email), DateTime.Now.AddDays(1));
                if (result == "invalid")
                {
                    ModelState.AddModelError("Email", "Địa chỉ Email không đúng.");
                    return View("Account.GetToken", token);
                }
                if (result == "active")
                {
                    var loginAccountUrl = string.Format("{0}account/active", ConfigHelper.WebDomain);
                    ModelState.AddModelError("Email", string.Format("Tài khoản của bạn đã được kích hoạt rôì. Click vào <a href='{0}' style='color: #007FF0' title='Đăng nhập'> đây</a> để đăng nhập.", loginAccountUrl));
                    return View("Account.GetToken", token);
                }
            
                var activeAccountUrl = string.Format("{0}account/active", ConfigHelper.WebDomain);
                var body =
                    string.Format(
                        "Mã kích hoạt tài khoản của bạn là {0}. Click vào <a href='{1}' style='color: #007FF0' title='Kích hoạt tài khoản'> đây</a> để kích hoạt tài khoản của bạn.",
                        token, activeAccountUrl);
                InternalSendEmail(token.Email, "Lấy mã kích hoạt tài khoản", body);
                return RedirectToAction("Active");
            
            }

            return View("Account.GetToken", token);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ForgotPassword")]
        public ActionResult HandleForgotPassword(AccountForgotPasswordModels forgot)
        {
            var forgotPasswordToken = getToken(forgot.Email);
            var result = AccountService.RequestForgotPassword(forgot.Email, forgotPasswordToken, DateTime.Now.AddDays(1));
            if (result == "invalid")
            {
                ModelState.AddModelError("Email", "Địa chỉ Email không đúng.");
                return View("Account.ForgotPassword", forgot);

            }
            var emailSender = new EmailSender(
                  ConfigHelper.GMailUserName,
                  ConfigHelper.GMailPassword,
                  ConfigHelper.GmailSmtpServer,
                  ConfigHelper.GMailPort
                 );
            var changePasswordUrl = string.Format("{0}account/changepassword", ConfigHelper.WebDomain);
            var body =string.Format(
                    "Mật khẩu mới của bạn là : {0}. Click vào <a href='{1}' style='color: #007FF0' title='Đổi mật khẩu'> đây</a> đề thay đổi mật khẩu của bạn.",
                    result, changePasswordUrl);
            emailSender.SendMail(ConfigHelper.GMailUserName, forgot.Email,
                "Cấp mật khẩu mới", body);
            return RedirectToAction("ChangePassword", new {token = forgotPasswordToken });
        }
        public ActionResult ChangePassword(string token)
        {
            var result = AccountService.CheckForgotPasswordToken(token);
            if (result == "invalid")
            {
                ViewBag.Error = string.Format("Mã xác thực không đúng. Click vào <a href='{0}' style='color: #007FF0' title='Quên mật khẩu'> đây</a> để gửi yêu cầu cấp mật khẩu mới.", string.Format("{0}account/forgotpassword", ConfigHelper.Domain)) ;
                return View("Account.InvalidToken");
            }
            if (result == "expire")
            {
                ViewBag.Error =string.Format("Mã xác thực hết hạn. Click vào <a href='{0}' style='color: #007FF0' title='Quên mật khẩu'> đây</a> để gửi yêu cầu cấp mật khẩu mới.", string.Format("{0}account/forgotpassword", ConfigHelper.Domain)) ;
                return View("Account.InvalidToken");
            }           

            return View("Account.ChangePassword", new ChangePasswordModels {Token = token});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("HandleChangePassword")]
        public ActionResult HandleChangePassword(ChangePasswordModels account)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(account.Token)){
                    ModelState.AddModelError("", "Mã xác thực lấy lại mật khẩu trống.");
                    account.OldPassword = "";
                    account.NewPassword = "";
                    account.ConfirmPassword = "";
                    return View("Account.ChangePassword", account);
                }
                var result = AccountService.ChangePassword(account.Token, account.OldPassword,
                    account.NewPassword);
                if (result == "invalid")
                {
                    ModelState.AddModelError("", "Email đăng ký tài khoản không đúng.");
                    account.OldPassword = "";
                    account.NewPassword = "";
                    account.ConfirmPassword = "";
                    return View("Account.ChangePassword", account);
                }
                if (result == "current_wrong")
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không đúng.");
                    account.OldPassword = "";
                    account.NewPassword = "";
                    account.ConfirmPassword = "";
                    return View("Account.ChangePassword", account);
                }
                return Redirect("/");
            }
            return View("Account.ChangePassword", account);
        }
       
        public ActionResult Profiles()
        {
            return View("Account.Profile");
        }

        public ActionResult HandleUpdateProfiles()
        {
            return Content("ok");

        }

        private string InternalSendEmail(string to,string subject,string body)
        {
            var emailSender = new EmailSender(
                 ConfigHelper.GMailUserName,
                 ConfigHelper.GMailPassword,
                 ConfigHelper.GmailSmtpServer,
                 ConfigHelper.GMailPort
                );
          return  emailSender.SendMail(ConfigHelper.GMailUserName, to,
               subject, body);
        }


    }
}

