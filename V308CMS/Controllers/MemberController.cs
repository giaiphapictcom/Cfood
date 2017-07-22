using System;
using System.Collections.Generic;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Helpers;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    
    public class MemberController : BaseController
    {
        public MemberController(){
            VisisterRepo.UpdateView();
        }
        
        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            var result = AccountService.CheckEmail(email);
            return Json(result);
        }
      
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (AuthenticationHelper.IsAuthenticated)
            {
                return RedirectToUrl(returnUrl);

            }
            ReturnUrl = returnUrl;
            return View("Member.Login", new LoginModels() );
        }
        [HttpPost]
        [ActionName("AjaxLogin")]
        public JsonResult OnAjaxLogin(string email="", string password="")
        {          
            var checkLoginResult = AccountService.CheckAccount(email, password);
            if (checkLoginResult == "invalid")
            {              
                return Json(new { code = 0, message = "Tên tài khoản hoặc Mật khẩu không chính xác."});
            }
           
            InternalLoginSuccess(checkLoginResult, email);         
            return Json(new { code = 1, message = "Đăng nhập thành công. Đang chuyển về trang chủ..." });
        }

        private void InternalLoginSuccess(string checkLoginResult, string email, bool remember = false)
        {
            var userDetail = checkLoginResult.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            var userId = int.Parse(userDetail[0]);
            var userAvatar = userDetail.Length > 1 ? userDetail[1] : "";
            var userAffilate = AffilateUserService.GetByUserId(userId);
            string affilateId = "";
            int affilateAmount = 0;
            if (userAffilate != null)
            {
                affilateId = userAffilate.AffilateId;
                affilateAmount = userAffilate.Amount;
            }
            
            AuthenticationHelper.SignIn(userId, email, email, userAvatar, affilateId, affilateAmount, remember);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Login")]
        public ActionResult OnLogin(LoginModels login)
        {
            if (ModelState.IsValid)
            {
                var result = AccountService.CheckAccount(login.Email, login.Password);
                if (result == "invalid")
                {
                    ViewBag.Error = "Tên tài khoản hoặc Mật khẩu không chính xác.";
                    login.Password = "";
                    return View("Member.Login", login);
                }
                InternalLoginSuccess(result, login.Email);
                return RedirectToUrl(ReturnUrl);
            }
            return View("Member.Login", login);
        }
        [Authorize]
        public ActionResult LogOut(string returnUrl)
        {
            AuthenticationHelper.SignOut();
            return RedirectToUrl(returnUrl);

        }
        public ActionResult Register()
        {
            return View("Member.Register", new RegisterModels());
        }

        private string getToken(string email, bool forForgotPassword = false)
        {
            return forForgotPassword ? EncryptionMD5.ToMd5($"{email}|{DateTime.Now.Ticks}|forgot-die") : EncryptionMD5.ToMd5(
                $"{email}|{DateTime.Now.Ticks}");
        }
        [HttpPost]
        [ActionName("RegisterAjax")]
        public JsonResult OnRegisterAjax(string email="", string password="", string confirmPassword="", string captcha="")
        {
            var validationResult = new Dictionary<string,string>();           
            if (!email.IsEmailAddress())
            {               
                return Json(new{ code = 0, message = "Địa chỉ email không hợp lê." });             

            }
            if (!password.IsPasswordValid())
            {
                return Json(new { code = 0,message = "Mật khẩu ít nhât 6 ký tự." });               
            }
            if (password != confirmPassword)
            {
                return Json(new { code = 0,  message = "Mật khẩu xác nhận không đúng." });              
            }
            if (captcha != Session["RegisterCaptcha"].ToString())
            {
                return Json(new { code = 0, message = "Mã xác thực không đúng." });               
            }           
            var result = InternalRegister(email, password);
            if (result == "exist")
            {
                return Json(new { code = 0,message = "Email đã được sử dụng để đăng ký tài khoản." });
            }
            return Json(new { code = 1, message = "Đăng ký tài khoản thành công." });

        }
        [NonAction]
        private string InternalRegister(string email, string password)
        {
            var salt = StringHelper.GenerateString(6);
            var token = getToken(email);
            var tokenExpireDate = DateTime.Now.AddDays(1);

            return AccountService.Insert(email, password, salt, token, tokenExpireDate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public ActionResult OnRegister(RegisterModels register)
        {
            if (ModelState.IsValid)
            {               
                if (register.Captcha != Session["RegisterCaptcha"].ToString())
                {
                    ModelState.AddModelError("Captcha","Mã xác thực không đúng.");
                    register.ResetPasswordValue();
                    return View("Member.Register", register);
                }              
                var result = InternalRegister(register.Email, register.Password);
                if (result == "exist")
                {
                    register.ResetPasswordValue();
                    ViewBag.Error = "Địa chỉ Email này đã được sử dụng để đăng ký, vui lòng sử dụng tên tài khoản khác.";
                    return View("Member.Register", register);
                }
                return RedirectToAction("Login", "Member");
            }
            register.ResetPasswordValue();
            return View("Member.Register", register);

        }

        public ActionResult Active()
        {
            return View("Member.Active");
        }       
        public ActionResult GetToken()
        {
            return View("Member.GetToken", new GetTokenModels());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GetToken")]
        public ActionResult OnGetToken(GetTokenModels token)
        {
            if (ModelState.IsValid)
            {
                var result = AccountService.UpdateToken(token.Email, getToken(token.Email), DateTime.Now.AddDays(1));
                if (result == "invalid")
                {
                    ModelState.AddModelError("Email", "Địa chỉ Email không đúng.");
                    return View("Member.GetToken", token);
                }
                if (result == "active")
                {
                    var loginAccountUrl = $"{ConfigHelper.WebDomain}account/active";
                    ModelState.AddModelError("Email",
                        $"Tài khoản của bạn đã được kích hoạt rôì. Click vào <a href='{loginAccountUrl}' style='color: #007FF0' title='Đăng nhập'> đây</a> để đăng nhập.");
                    return View("Member.GetToken", token);
                }

                var activeAccountUrl = $"{ConfigHelper.WebDomain}account/active";
                var body =
                    $"Mã kích hoạt tài khoản của bạn là {token}. Click vào <a href='{activeAccountUrl}' style='color: #007FF0' title='Kích hoạt tài khoản'> đây</a> để kích hoạt tài khoản của bạn.";
                InternalSendEmail(token.Email, "Lấy mã kích hoạt tài khoản", body);
                return RedirectToAction("Active");

            }

            return View("Member.GetToken", token);
        }
        [HttpPost]
        [ActionName("ForgotPasswordAjax")]
        public JsonResult OnForgotPasswordAjax(string email="")
        {
            var forgotPasswordToken = getToken(email);
            var newPassword = AccountService.RequestForgotPassword(email, forgotPasswordToken, DateTime.Now.AddDays(1));
            if (newPassword == "invalid")
            {
                return Json(new {code = 0, message = "Địa chỉ email không đúng."});
            }
            InternalForgotPasswordSuccess(email, newPassword);
            return Json(new {code =1, message = "Vui lòng kiểm tra hòm thư của bạn để biết mật khẩu mới của bạn."});
         
        }

        private void InternalForgotPasswordSuccess(string email,string newPassword)
        {
            var emailSender = new EmailSender(
                ConfigHelper.GMailUserName,
                ConfigHelper.GMailPassword,
                ConfigHelper.GmailSmtpServer,
                ConfigHelper.GMailPort
               );
            var changePasswordUrl = $"{ConfigHelper.WebDomain}account/changepassword";
            var body =
                $"Mật khẩu mới của bạn là : {newPassword}. Click vào <a href='{changePasswordUrl}' style='color: #007FF0' title='Đổi mật khẩu'> đây</a> đề thay đổi mật khẩu của bạn.";
                 emailSender.SendMail(ConfigHelper.GMailUserName, email,
                "Cấp mật khẩu mới", body);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ForgotPassword")]
        public ActionResult OnForgotPassword(AccountForgotPasswordModels forgot)
        {
            var forgotPasswordToken = getToken(forgot.Email);
            var newPassword = AccountService.RequestForgotPassword(forgot.Email, forgotPasswordToken, DateTime.Now.AddDays(1));
            if (newPassword == "invalid")
            {
                ModelState.AddModelError("Email", "Địa chỉ Email không đúng.");
                return View("Member.ForgotPassword", forgot);
            }
            InternalForgotPasswordSuccess(forgot.Email, newPassword);         
            return RedirectToAction("ChangePassword", new { token = forgotPasswordToken });
        }
        public ActionResult ChangePassword(string token)
        {
            var result = AccountService.CheckForgotPasswordToken(token);
            if (result == "invalid")
            {
                ViewBag.Error =
                    $"Mã xác thực không đúng. Click vào <a href='{$"{ConfigHelper.Domain}account/forgotpassword"}' style='color: #007FF0' title='Quên mật khẩu'> đây</a> để gửi yêu cầu cấp mật khẩu mới.";
                return View("Member.InvalidToken");
            }
            if (result == "expire")
            {
                ViewBag.Error =
                    $"Mã xác thực hết hạn. Click vào <a href='{$"{ConfigHelper.Domain}account/forgotpassword"}' style='color: #007FF0' title='Quên mật khẩu'> đây</a> để gửi yêu cầu cấp mật khẩu mới.";
                return View("Member.InvalidToken");
            }

            return View("Member.ChangePassword", new ChangePasswordModels { Token = token });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("HandleChangePassword")]
        public ActionResult OnChangePassword(ChangePasswordModels account)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(account.Token))
                {
                    ModelState.AddModelError("", "Mã xác thực lấy lại mật khẩu trống.");
                    account.OldPassword = "";
                    account.NewPassword = "";
                    account.ConfirmPassword = "";
                    return View("Member.ChangePassword", account);
                }
                var result = AccountService.ChangePassword(account.Token, account.OldPassword,
                    account.NewPassword);
                if (result == "invalid")
                {
                    ModelState.AddModelError("", "Email đăng ký tài khoản không đúng.");
                    account.OldPassword = "";
                    account.NewPassword = "";
                    account.ConfirmPassword = "";
                    return View("Member.ChangePassword", account);
                }
                if (result == "current_wrong")
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không đúng.");
                    account.OldPassword = "";
                    account.NewPassword = "";
                    account.ConfirmPassword = "";
                    return View("Member.ChangePassword", account);
                }
                return Redirect("/");
            }
            return View("Member.ChangePassword", account);
        }

        public ActionResult Profiles()
        {
            return View("Member.Profile");
        }

        public ActionResult OnUpdateProfiles()
        {
            return Content("ok");

        }

        private string InternalSendEmail(string to, string subject, string body)
        {
            var emailSender = new EmailSender(
                 ConfigHelper.GMailUserName,
                 ConfigHelper.GMailPassword,
                 ConfigHelper.GmailSmtpServer,
                 ConfigHelper.GMailPort
                );
            return emailSender.SendMail(ConfigHelper.GMailUserName, to,
                 subject, body);
        }


    }
}

