using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Models;
using V308CMS.Common;
using V308CMS.Data.Models;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Quốc gia")]
    public class CountryController:BaseController
    {      
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {            
            return View("Index", CountryService.GetList());
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            return View("Create", new CountryModels());
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]               
        public ActionResult OnCreate(CountryModels country)
        {
            if (ModelState.IsValid)
            {
                var result = CountryService.Insert(country.CloneTo<Country>());
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Quốc gia {country.Name} đã tồn tại trên hệ thống.");
                    return View("Create", country);
                }
                SetFlashMessage($"Thêm Quốc gia '{country.Name}' thành công.");
                if (country.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ModelState.Clear();
                return View("Create", country.ResetValue());

            }
            return View("Create", country);
        }        
        [CheckPermission(2, "Sửa")]
        public ActionResult Edit(int id)
        {
            var country = CountryService.Find(id);
            if (country == null)
            {
                SetFlashMessage($"Không tìm thấy Quốc gia cần cập nhật.");
                return RedirectToAction("Index");

            }
            return View("Edit", country.CloneTo<CountryModels>());

        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
       
        public ActionResult OnEdit(CountryModels country)
        {
            if (ModelState.IsValid)
            {
                var result = CountryService.Update(country.CloneTo<Country>());
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");
                    return View("Edit", country);
                }

                SetFlashMessage($"Cập nhật Quốc gia '{country.Name}' thành công.");
                if (country.SaveList)
                {
                    return RedirectToAction("Index");
                }
                return View("Edit", country);
            }
            return View("Edit", country);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = CountryService.Delete(id);
            SetFlashMessage(result == Result.Ok ? "Xóa Quốc gia thành công." : "Quốc gia không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }


    }
}