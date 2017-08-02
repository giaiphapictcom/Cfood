using System.Web.Mvc;
using V308CMS.Common;
using V308CMS.Sale.Helpers;
using V308CMS.Social;

namespace V308CMS.Sale.Controllers
{
    public class SocialLoginController : BaseController
    {       
        /// <summary>
        /// Link dang nhap facebook
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult LoginFacebook(string returnUrl="")
        {
            //Ghi nhan CallbackUrl can redirect toi sau khi dang nhap thanh cong
            ReturnUrl = returnUrl;                 
            return Redirect(FacebookService.GetLoginUrl(ConfigHelper.FacebookLoginCallback));
        }
        [NonAction]
        private void SignIn(SocialUser user)
        {
            var userIdResult = AddUser(user.UserId, user.Email, user.FullName, user.Avatar);
            SetFlashMessage("Đăng nhập thành công.");
            Session["UserId"] = userIdResult;
            Session["UserName"] = user.FullName;
            Session["Role"] = 0;
            Session["Account"] = user.FullName;
            System.Web.Security.FormsAuthentication.SetAuthCookie(user.Email, true);
            //AuthenticationHelper.SignIn(userIdResult, user.UserId, user.FullName, user.Avatar);
        }

        public ActionResult FacebookCallback(string code)
        {
            var accessToken = FacebookService.GetAccessToken(code, ConfigHelper.FacebookLoginCallback);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return RedirectToAction("LoginFail", new { provider = "facebook" });
            }

            var facebookUser = FacebookService.GetUserProfile(accessToken);
            if (facebookUser == null)
            {
                return RedirectToAction("LoginFail", new { provider = "facebook" });
            }
            SignIn(facebookUser); 
            return RedirectToUrl(ReturnUrl);
        }    
        [NonAction]
        private int AddUser(string userId, string email, string fullName, string avatar)
        {
            
            var userIdResult = AccountService.Insert(userId, email, StringHelper.GenerateString(6), StringHelper.GenerateString(4), fullName, avatar, Data.Helpers.Site.affiliate);
            return int.Parse(userIdResult);
        }

        public ActionResult LoginGoogle(string returnUrl="")
        {
            ReturnUrl = returnUrl;        
            return Redirect(GoogleplusService.GetLoginUrl(ConfigHelper.GoogleLoginCallback));
        }

        public ActionResult LoginFail(string provider)
        {
            return View("LoginFail",(object)provider);
        }
        public ActionResult GoogleCallback(string returnUrl = "")
        {
            var code = Request.QueryString["code"];
            if (string.IsNullOrWhiteSpace(code))
            {
                return RedirectToAction("LoginFail", new { provider  ="google"});
            }
            var accessToken = GoogleplusService.GetAccessToken(code, ConfigHelper.GoogleLoginCallback);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return RedirectToAction("LoginFail", new { provider = "google" });
            }
            var googleUser = GoogleplusService.GetUserProfile(accessToken);
            if (googleUser == null)
            {
                return RedirectToAction("LoginFail", new { provider = "google" });
            }
            SignIn(googleUser);
            return RedirectToUrl(ReturnUrl);
        }
    }
}
