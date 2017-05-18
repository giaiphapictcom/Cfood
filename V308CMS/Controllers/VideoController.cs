using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Controllers
{
    public class VideoController : Controller
    {
        //
        // GET: /Video/

        public ActionResult Index(int page =1)
        {
            return Content("ok");
        }

        public ActionResult Detail(int id )
        {
            return Content("ok");
        }

    }
}
