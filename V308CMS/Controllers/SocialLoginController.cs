using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Facebook;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Plus.v1;
using Google.Apis.Services;
using V308CMS.Common;
using V308CMS.Helpers;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class SocialLoginController : BaseController
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url)
                {
                    Query = null,
                    Fragment = null,
                    Path = Url.Action("FacebookCallback")
                };
                return uriBuilder.Uri;
            }
        }
        /// <summary>
        /// Link dang nhap facebook
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult Facebook(string returnUrl="")
        {
            var fb = new FacebookClient();           
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigHelper.FacebookAppId,
                client_secret = ConfigHelper.FacebookAppSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            //Ghi nhan CallbackUrl can redirect toi sau khi dang nhap thanh cong
            ReturnUrl = returnUrl;                 
            return Redirect(loginUrl.AbsoluteUri);
        }
        /// <summary>
        /// Action duoc goi lai khi dang nhap facebook
        /// Thay doi trong App facebook MpStart
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigHelper.FacebookAppId,
                client_secret = ConfigHelper.FacebookAppSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                fb.AccessToken = accessToken;
                //Lay cac thong tin ten, facebook_id, email dang ky, avatar
                dynamic me = fb.Get("me?fields=name,id,email,picture");
                string email = me.email;
                string userId = me.id;
                string fullName = me.name;
                string avatar = me.picture.data.url;
                             
                var userIdResult = AddUser(userId, email, fullName, avatar);
                AuthenticationHelper.SignIn(userIdResult, userId, fullName, avatar);
                //Redirect toi ReturnUrl sau khi dang nhap thanh cong neu co
                //Nguoc lai redirect ve trang chu             
                return RedirectToUrl(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public async Task<ActionResult> Google(CancellationToken cancelToken, string returnUrl = "")
        {
            var result = await new AuthorizationCodeMvcApp(this, new GoogleAuth()).AuthorizeAsync(cancelToken);

            if (result.Credential == null)
                return new RedirectResult(result.RedirectUri);

            var plusService = new PlusService(new BaseClientService.Initializer
            {
                HttpClientInitializer = result.Credential,
                ApplicationName = "MpStart"
            });

            //get the user basic information
            var me = plusService.People.Get("me").Execute();
            var fullName = me.Name.GivenName + " " +  me.Name.FamilyName;
            var email = me.Emails.ElementAt(0).Value;        
            var userId = me.Id;
            var avatar = me.Image.Url;
            var displayName = me.DisplayName;           
            var userIdResult  = AddUser(userId, email, fullName, avatar);
            AuthenticationHelper.SignIn(userIdResult, userId, displayName, avatar);           
            return RedirectToUrl(returnUrl);
        }
        [NonAction]
        private int AddUser(string userId, string email, string fullName, string avatar)
        {
            var userIdResult = AccountService.Insert(userId, email, StringHelper.GenerateString(6), StringHelper.GenerateString(4), fullName, avatar);
            return int.Parse(userIdResult);
        }

    }
}
