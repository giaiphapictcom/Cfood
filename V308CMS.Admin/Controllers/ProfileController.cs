using System.Web.Mvc;
using V308CMS.Admin.Models;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        //
        // GET: /UserProfile/

        public ActionResult Index()
        {
            return View("Index", new ProfileViewModels());
        }

        public ActionResult OnChangePassword()
        {
            return Content("ok");
        }
        public ActionResult OnEdit()
        {
            return Content("ok");
        }


    }
}
