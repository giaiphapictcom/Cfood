using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;

namespace V308CMS.Admin.Controllers
{   
    [Authorize] 
    [CheckGroupPermission(true, "Liên hệ")]
    public class ContactController : BaseController
    {        
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {          
            return View("Index", ContactService.GetAll());
        }        
        [CheckPermission(1, "Sửa")]
        public ActionResult Edit(int id)
        {
            var contact = ContactService.Find(id);
            if (contact == null)
            {
                return RedirectToAction("Index");
            }
                                 
            return View("Edit", contact.CloneTo<ContactModels>());

        }
        [HttpPost]
        [CheckPermission(1, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
       
        public ActionResult OnEdit(ContactModels contact)
        {
            if (ModelState.IsValid)
            {
                var result = ContactService.Update
                    (
                        contact.Id,
                        contact.FullName,
                        contact.Email,
                        contact.Phone,
                        contact.Message,
                        contact.CreatedDate
                    );
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Thông tin liên hệ không tồn tại trên hệ thống.");                   
                    return View("Edit", contact);
                }
                SetFlashMessage($"Cập nhật Thông tin liên hệ '{contact.FullName}' thành công.");
                if (contact.SaveList)
                {
                    return RedirectToAction("Index");
                }             
                return View("Edit", contact);
            }           
            return View("Edit", contact);

        }        
        [HttpPost]
        [CheckPermission(2, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ContactService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa thông tin liên hệ thành công." :
                "Thông tin liên hệ không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }
    }
}
