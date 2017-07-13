using System;
using System.Diagnostics;
using Facebook;

namespace V308CMS.Socail
{
    public sealed class FacebookService : ISocialService
    {
        public string FacebookAppId { get; set; }
        public string FacebookAppSecret { get; set; }
        private readonly FacebookClient _fb;

        public FacebookService(string facebookAppId, string facebookAppSecret)
        {
            FacebookAppId = facebookAppId;
            FacebookAppSecret = facebookAppSecret;
            _fb = new FacebookClient();
        }

        public string GetLoginUrl(string callBackUrl= "/", string scope="email", string responseType= "code")
        {           
            var loginUrl = _fb.GetLoginUrl(new
            {
                client_id = FacebookAppId,
                client_secret = FacebookAppSecret,
                redirect_uri = callBackUrl,
                response_type = responseType,
                scope
            });
            return loginUrl.AbsoluteUri;
        }

        public string GetAccessToken(string code, string callBackUrl)
        {
            try
            {
                dynamic result = _fb.Post("oauth/access_token", new
                {
                    client_id = FacebookAppId,
                    client_secret = FacebookAppSecret,
                    redirect_uri = callBackUrl,
                    code
                });

                return result.access_token;
           }catch (Exception ex){Debug.WriteLine($"Get access token fail. Error : {ex.Message}");return "";}

        }

        public SocialUser GetUserProfile(string accessToken)
        {
            SocialUser result = null;
            try
            {
                _fb.AccessToken = accessToken;              
                dynamic me = _fb.Get("me?fields=first_name,last_name,id,email,picture");
                result = new SocialUser
                {
                    UserId = me.id,
                    Email = me.email,
                    Avatar = me.picture.data.url,
                    FirstName = me.first_name,
                    LastName = me.last_name,
                   
                };
                result.FullName = $"{result.FirstName} {result.LastName}";
                return result;
            }catch (Exception ex) { Debug.WriteLine($"Get user profile fail. Error : {ex.Message}"); return result; }
        }
    }
}
