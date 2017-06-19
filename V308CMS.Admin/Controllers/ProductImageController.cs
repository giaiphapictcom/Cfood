using System;
using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;
using V308CMS.Common;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Ảnh sản phẩm")]    
    public class ProductImageController : BaseController
    {            
        [CheckPermission(0, "Sửa")]
        public ActionResult Edit(int id, int productId)
        {
            var productImage = ProductImageService.Find(id);
            if (productImage == null)
            {
                return RedirectToAction("Edit", "Product", new
                {
                    id = productId
                });
            }
            return View("Edit", productImage.CloneTo<ProductImageModels>());
        }
        [HttpPost]
        [CheckPermission(0, "Sửa")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]       
        public ActionResult OnEdit(ProductImageModels productImage)
        {
            var productImageUpdate = ProductImageService.Find(productImage.Id);
            if (productImageUpdate == null)
            {
                return RedirectToAction("Edit", "Product", new
                {
                    id = productImage.ProductId
                });
            }
            productImageUpdate.Name = productImage.Image != null ?
                  productImage.Image.Upload() :
                  productImage.ImageUrl.ToImageOriginalPath();

            productImageUpdate.ProductID = productImage.ProductId;
            productImageUpdate.Title = productImage.Title;
            productImageUpdate.Number = Convert.ToInt32(productImage.Number);
            var result = ProductImageService.Update(productImageUpdate);
            if (result == Result.NotExists)
            {
                ModelState.AddModelError("", "Id không tồn tại trên hệ thống.");                
                return View("Edit", productImage);
            }
            SetFlashMessage($"Cập nhật ảnh '{productImageUpdate.Title}' thành công.");
            if (productImage.SaveList)
            {
                return RedirectToAction("Edit", "Product", new
                {
                    id = productImage.ProductId
                });

            }         
            return View("Edit", productImage);
        }
        [HttpPost]
        [CheckPermission(1, "Xóa")]
        [ActionName("Delete")]       
        public JsonResult OnDelete(int id)
        {
            var result = ProductImageService.Delete(id);
            return Json(new { code = result });
        }
    }
}
