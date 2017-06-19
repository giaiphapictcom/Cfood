using System.ComponentModel;
using System.Web.Mvc;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [Description("Ưu đãi")]
    public class VoucherController : BaseController
    {
        //
        // GET: /Voucher/

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

        public ActionResult Edit()
        {
            return View("Edit");
        }

        public ActionResult OnEdit()
        {
            return View("Edit");
        }

        public ActionResult OnDelete()
        {
            return Content("Ok");
        }

    }
}
