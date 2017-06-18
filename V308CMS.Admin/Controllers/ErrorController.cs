using System.Web.Mvc;

namespace V308CMS.Admin.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

    }
}
