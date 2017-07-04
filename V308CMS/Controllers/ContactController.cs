using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using V308CMS.Helpers;
using V308CMS.Models;

namespace V308CMS.Controllers
{
    public class ContactController : BaseController
    {
       
        public ActionResult Index()
        {
            var model = new ContactModels();
            if (AuthenticationHelper.IsAuthenticated)
            {
                var userInfo = AccountService.GetByUserId(User.UserId);
                if (userInfo != null)
                {
                    model.Email = userInfo.Email;
                    model.Phone = userInfo.Phone;
                    model.Name = userInfo.FullName;
                }
            }
            return View("Contact", model);
        }
        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult OnIndex(ContactModels contact)
        {
            if(ModelState.IsValid)
            {
                ContactService.Insert(contact.Name, contact.Email, contact.Phone, contact.Message, DateTime.Now);
                ViewBag.Message = "Cảm ơn bạn đã gửi thông tin liên hệ cho chúng tôi. Chúng tôi sẽ liên hệ ngay với bạn.";
            }
            return View("Contact", contact);
        }

    }
}
