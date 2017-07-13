using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Controllers
{
    public class PopupController : Controller
    {
        //
        // GET: /Popup/

        public PartialViewResult Login()
        {
            return PartialView("Login");
        }

        public PartialViewResult Register()
        {
            return PartialView("Register");
        }
        public PartialViewResult ForgotPassword()
        {
            return PartialView("ForgotPassword");
        }
    }
}
