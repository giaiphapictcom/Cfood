using Google.Apis.Auth.OAuth2.Mvc;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {

        protected override FlowMetadata FlowData => new GoogleAuth();
    }
}
