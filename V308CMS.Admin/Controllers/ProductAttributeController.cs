using System.Web.Mvc;
using V308CMS.Admin.Attributes;

namespace V308CMS.Admin.Controllers
{
    [Authorize]
    [CheckGroupPermission(true, "Thuộc tính sản phẩm")]
    public class ProductAttributeController : BaseController
    {
        //
        // GET: /ProductAttribute/
        [HttpPost]
        [CheckPermission(0, "Xóa")]
        [ActionName("Delete")]        
        public JsonResult OnDelete(int id)
        {
           var result = ProductAttributeService.Delete(id);
            return Json(new {code = result});

        }

    }
}
