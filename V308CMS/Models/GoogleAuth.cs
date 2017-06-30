using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Plus.v1;
using Google.Apis.Util.Store;
using V308CMS.Helpers;

namespace V308CMS.Models
{
    public class GoogleAuth: FlowMetadata
    {

        private static readonly IAuthorizationCodeFlow flow =
         new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
         {
             ClientSecrets = new ClientSecrets
             {
                 ClientId = ConfigHelper.GoogleAppId,
                 ClientSecret = ConfigHelper.GoogleAppSecret
             },
             Scopes = new[] {
                PlusService.Scope.UserinfoEmail,
                PlusService.Scope.UserinfoProfile
             },
             DataStore = new FileDataStore("Google.Apis.Sample.MVC")
         });

        public override string GetUserId(Controller controller)
        {
            return "user1";
        }

        public override IAuthorizationCodeFlow Flow
        {
            get { return flow; }
        }
    }
}