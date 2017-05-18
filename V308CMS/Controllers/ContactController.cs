using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class ContactController : BaseController
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View(FindView("Contact"));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult HandleIndex(ContactModels contact)
        {
            if(ModelState.IsValid){

            }
            return Content("ok");
        }

    }
}
