using System;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using V308CMS.Common;

namespace V308CMS.Social
{
    /// <summary>
    /// Dang nhap su dung tai khoan googgle
    /// 07/10/2017
    /// </summary>
   
    public sealed class GoogleplusService: ISocialService
    {
        public GoogleplusService(string googleAppId, string googleAppSecret)
        {
            GoogleAppId = googleAppId;
            GoogleAppSecret = googleAppSecret;
        }

        public string GoogleAppId { get; set; }
        public string GoogleAppSecret { get; set; }
        private static string AuthenBaseUrl = "https://accounts.google.com/o/oauth2/";
      
        public string GetLoginUrl(string callBackUrl="/", string scope= "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email", string responseType= "code")
        {         
            return $"{AuthenBaseUrl}auth?client_id={GoogleAppId}" +
              $"&redirect_uri={callBackUrl}" +
              $"&scope={scope} " +
              $"&response_type={responseType}";
        }

        public string GetAccessToken(string code, string callBackUrl)
        {           
            try{
                var getTokenUrl = $"code={code}&client_id={GoogleAppId}&client_secret={GoogleAppSecret}&redirect_uri={callBackUrl}&grant_type=authorization_code";
                var getTokenRequest = new RequestHelper {RequestType = RequestType.Post};
                var getTokenResponse = getTokenRequest.SendRequest($"{AuthenBaseUrl}token", getTokenUrl);

                var tokenResponse = new JavaScriptSerializer().Deserialize(getTokenResponse, typeof (TokenResponse)) as TokenResponse;
                if (tokenResponse != null) return tokenResponse.access_token; return "";

            }catch (Exception ex){Debug.WriteLine($"Get access token fail. Error : {ex.Message}");return "";}
        }

        public  SocialUser GetUserProfile(string accessToken)
        {
            SocialUser result =  null;
            try
            {
                var getUserProfileUrl = $"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={accessToken}";
                var getUserProfileService = new RequestHelper { RequestType = RequestType.Get};
                var getUserProfileResponse = getUserProfileService.Send(getUserProfileUrl);
                var userProfile = JObject.Parse(getUserProfileResponse);
                result = new SocialUser
                {
                    UserId = userProfile["id"].ToString().Replace("\"", ""),
                    FirstName = userProfile["given_name"].ToString().Replace("\"", ""),
                    LastName = userProfile["family_name"].ToString().Replace("\"", ""),
                    Email = userProfile["email"].ToString().Replace("\"", ""),
                    Avatar = userProfile["picture"].ToString().Replace("\"", ""),
                    FullName = userProfile["name"].ToString().Replace("\"", ""),
                };
                return result;
            }
            catch (Exception ex) { Debug.WriteLine($"Get user profile fail. Error : {ex.Message}"); return result; }

        }
    }
}