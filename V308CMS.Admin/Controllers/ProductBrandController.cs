using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Admin.Models.UI;
using V308CMS.Common;
using V308CMS.Data.Enum;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Thương hiệu sản phẩm")]
    public class ProductBrandController : BaseController
    {
        //
        // GET: /ProductBrand/

        [NonAction]
        private List<MutilCategoryItem> GetListMutilCategoryItem()
        {
            return ProductTypeService.GetAll().Select
              (
                  cate => new MutilCategoryItem
                  {
                      Name = cate.Name,
                      Id = cate.ID,
                      ParentId = cate.Parent
                  }
              ).ToList();
        }       
        [CheckPermission(0, "Danh sách")]
        public ActionResult Index()
        {
            var data = ProductBrandService.GetListWithProductType();
            return View("Index", data);
        }        
        [CheckPermission(1, "Thêm mới")]
        public ActionResult Create()
        {
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            ViewBag.ListCategory = GetListMutilCategoryItem();
            var brand = new ProductBrandModels();
            return View("Create", brand);
        }
        [HttpPost]
        [CheckPermission(1, "Thêm mới")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnCreate(ProductBrandModels brand)
        {
            if (ModelState.IsValid)
            {
                brand.ImageUrl = brand.Image != null ?
                    brand.Image.Upload() :
                    brand.ImageUrl;
                var result = ProductBrandService.Insert
                    (
                        brand.Name,brand.CategoryId,
                        brand.ImageUrl, brand.Status
                    );
                if (result == Result.Exists)
                {
                    ModelState.AddModelError("", $"Tên thương hiệu '{brand.Name}' đã tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                    ViewBag.ListCategory = GetListMutilCategoryItem();
                    return View("Create", brand);
                }
                SetFlashMessage($"Thêm thương hiệu '{brand.Name}' thành công.");
                if (brand.SaveList)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                ViewBag.ListCategory = GetListMutilCategoryItem();
                ModelState.Clear();
                return View("Create", brand.ResetValue());
            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            ViewBag.ListCategory = GetListMutilCategoryItem();
            return View("Create", brand);
        }
        [CheckPermission(2, "Sửa")]       
        public ActionResult Edit(int id)
        {
            var brand = ProductBrandService.Find(id);
            if (brand == null)
            {

                return RedirectToAction("Index");

            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            ViewBag.ListCategory = GetListMutilCategoryItem();
            var data = new ProductBrandModels
            {
                Id = brand.id,
                CategoryId = brand.category_default,
                ImageUrl = brand.image,
                Name = brand.name,
                Status = (byte) brand.status
            };
            return View("Edit", data);
        }
        [HttpPost]
        [CheckPermission(2, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]        
        public ActionResult OnEdit(ProductBrandModels brand)
        {
            if (ModelState.IsValid)
            {               
                brand.ImageUrl = brand.Image != null? 
                    brand.Image.Upload(): 
                    brand.ImageUrl.ToImageOriginalPath();
                var result = ProductBrandService.Update(
                    brand.Id,
                    brand.Name,
                    brand.CategoryId,
                    brand.ImageUrl,
                    brand.Status);
                if (result == Result.NotExists)
                {
                    ModelState.AddModelError("", "Thương hiệu không tồn tại trên hệ thống.");
                    ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                    ViewBag.ListCategory = GetListMutilCategoryItem();
                    return View("Edit", brand);
                }
                SetFlashMessage($"Cập nhật thương hiệu '{brand.Name}' thành công.");
                if (brand.SaveList)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
                ViewBag.ListCategory = GetListMutilCategoryItem();
                return View("Edit", brand);
            }
            ViewBag.ListState = DataHelper.ListEnumType<StateEnum>();
            ViewBag.ListCategory = GetListMutilCategoryItem();
            return View("Edit", brand);

        }
        [HttpPost]
        [CheckPermission(3, "Xóa")]      
        [ActionName("Delete")]
        public ActionResult OnDelete(int id)
        {
            var result = ProductBrandService.Delete(id);
            SetFlashMessage(result == Result.Ok ?
                "Xóa thương hiệu thành công." :
                "Thương hiệu không tồn tại trên hệ thống.");
            return RedirectToAction("Index");
        }

    }
}
