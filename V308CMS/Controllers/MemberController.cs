using System;
using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Helpers;
using V308CMS.Helpers.Url;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    
    public class MemberController : BaseController
    {

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
                if (result == "not_active")
                {
                    var activeAccountUrl = $"{ConfigHelper.WebDomain}account/active";
                    ViewBag.Error =
                        $"Tài khoản của bạn chưa được kích hoạt, click vào <a href='{activeAccountUrl}' title='Kích hoạt tài khoản' style='color: #007FF0'> đây</a> để kích hoạt tài khoản của bạn.";

                    login.Password = "";
                    return View("Member.Login", login);
                }
                var userDetail = result.Split(new[]{"|"}, StringSplitOptions.RemoveEmptyEntries);
                var userId = int.Parse(userDetail[0]);
                var userAvatar = userDetail.Length>1? userDetail[1]:"";
                AuthenticationHelper.SignIn(userId, login.Email, login.Email, userAvatar);
                return RedirectToUrl(ReturnUrl);
            }
            return View("Member.Login", login);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            AuthenticationHelper.SignOut();
            return Redirect("/");

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
        [ValidateAntiForgeryToken]
        [ActionName("Register")]
        public ActionResult OnRegister(RegisterModels register)
        {
            if (ModelState.IsValid)
            {
                var salt = StringHelper.GenerateString(6);
                var token = getToken(register.Email);
                var tokenExpireDate = DateTime.Now.AddDays(1);

                var result = AccountService.Insert(register.Email, register.Password, salt, token, tokenExpireDate);
                if (result == "exist")
                {
                    register.ResetPasswordValue();
                    ViewBag.Error = "Địa chỉ Email này đã được sử dụng để đăng ký, vui lòng sử dụng tên tài khoản khác.";
                    return View("Member.Register", register);
                }
                else
                {
                    var activeAccountUrl = $"{ConfigHelper.WebDomain}account/active";
                    var body =
                        string.Format(
                            "Cảm ơn bạn đã đăng ký tài khoản trên hệ thống của {0}. Mã kích hoạt tài khoản của bạn là {1}. Click vào <a style='color: #007FF0' href='{2}' title='Kích hoạt tài khoản'> đây</a> để kích hoạt tài khoản của bạn.",
                            ViewBag.SiteName, token, activeAccountUrl);
                    InternalSendEmail(register.Email, "Đăng ký tài khoản", body);
                    return RedirectToAction("Active");
                }

            }
            register.ResetPasswordValue();
            return View("Member.Register", register);

        }

        public ActionResult Active()
        {
            return View("Member.Active");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Active")]
        public ActionResult OnActive(ActiveModels active)
        {
            if (ModelState.IsValid)
            {
                var activeResult = AccountService.Active(active.Token);
                if (activeResult == "invalid")
                {
                    ModelState.AddModelError("Token", "Mã kích hoạt tài khoản không đúng.");
                    return View("Member.Active", active);
                }
                if (activeResult == "expire")
                {
                    var getTokenUrl = $"{ConfigHelper.WebDomain}account/gettoken";
                    ModelState.AddModelError("Token",
                        $"Mã kích hoạt tài khoản đã hết hạn. Click vào <a style='color: #007FF0' href='{getTokenUrl}' title='Kích hoạt tài khoản'> đây</a> để lấy mã kích hoạt. ");
                    return View("Member.Active", active);
                }
                return RedirectToAction("Login");
            }
            return View("Member.Active", active);
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
        [ValidateAntiForgeryToken]
        [ActionName("ForgotPassword")]
        public ActionResult OnForgotPassword(AccountForgotPasswordModels forgot)
        {
            var forgotPasswordToken = getToken(forgot.Email);
            var result = AccountService.RequestForgotPassword(forgot.Email, forgotPasswordToken, DateTime.Now.AddDays(1));
            if (result == "invalid")
            {
                ModelState.AddModelError("Email", "Địa chỉ Email không đúng.");
                return View("Member.ForgotPassword", forgot);

            }
            var emailSender = new EmailSender(
                  ConfigHelper.GMailUserName,
                  ConfigHelper.GMailPassword,
                  ConfigHelper.GmailSmtpServer,
                  ConfigHelper.GMailPort
                 );
            var changePasswordUrl = $"{ConfigHelper.WebDomain}account/changepassword";
            var body =
                $"Mật khẩu mới của bạn là : {result}. Click vào <a href='{changePasswordUrl}' style='color: #007FF0' title='Đổi mật khẩu'> đây</a> đề thay đổi mật khẩu của bạn.";
            emailSender.SendMail(ConfigHelper.GMailUserName, forgot.Email,
                "Cấp mật khẩu mới", body);
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

