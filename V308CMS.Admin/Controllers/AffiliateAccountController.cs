using System;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Data;


namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Tài khoản Affiliate")]
    public class AffiliateAccountController : BaseController
    {
        //
        // GET: /AffiliateAccount/

        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            return View("Index", UserService.GetList(-1, Data.Helpers.Site.affiliate));
        }

    }
}
