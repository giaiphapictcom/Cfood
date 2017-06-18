using System.Web.Mvc;
using V308CMS.Admin.Attributes;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(false, "Gửi Email")]
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult OnCreate()
        {
            return View("Create");
        }

        public ActionResult Detail(int id)
        {
            return Content("ok");
        }

        public ActionResult OnDelete(int id)
        {
            return Content("ok");
        }


    }
}
