using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V308CMS.Sale.Controllers
{
    public class PartnerController : Controller
    {
        //
        // GET: /Partner/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }



        #region LinkForm Action
        
        [HttpGet]
        public ActionResult Links()
        {
            return View();
        }

        [HttpPost, ActionName("Links")]
        public ActionResult LinksPost()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LinkForm()
        {
            return View();
        }

        [HttpPost, ActionName("LinkForm")]
        public ActionResult LinkFormPost()
        {
            return View();
        }
        #endregion

        #region Products

        [HttpGet]
        public ActionResult Products()
        {
            return View();
        }

        [HttpPost, ActionName("Products")]
        public ActionResult ProductsPost()
        {
            return View();
        }
        #endregion


    }
}
