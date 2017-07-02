using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V308CMS.Helpers;

namespace V308CMS.Controllers
{
   
    public class MemberWishListController : BaseController
    {
        //
        // GET: /WishList/
        [Authorize]
        public ActionResult Index()
        {
            var wishlist = ProductWishlistService.GetListWishlist(User.UserId);
            var listProductWishlist = ProductsService.GetListProductWishlist(wishlist);
            return View("Wishlist.Index", listProductWishlist);
        }

        [HttpPost]
        public JsonResult AddToWishList(int id)
        {
            if (!AuthenticationHelper.IsAuthenticated)
            {
                return Json(new {code = 0, message = "Bạn cần đăng nhập để thực hiện chức năng này."});
            }
            var result = ProductWishlistService.AddItemToWishlist(id, User.UserId);
            if (result == "exist")
            {
                return Json(new { code = 0, message = "Sản phẩm đã có trong danh sách yêu thích." });
            }
            return Json(new { code = 1, message = "Thêm sản phẩm vào danh sách yêu thích thành công." });
        }

        public JsonResult RemoveItemFromWishList(int id)
        {
            if (!AuthenticationHelper.IsAuthenticated)
            {
                return Json(new {code = "require_login", message = "Bạn cần đăng nhập để thực hiện chức năng này."});
            }
            var result = ProductWishlistService.RemoveItemFromWishlist(id, User.UserId);
            if (result == "userid_invalid")
            {
                return Json(new { code = 0, message = "Tên tài khoản không chính xác." });
            }
            if (result == "not_exist")
            {
                return Json(new { code = 0, message = "Danh sách sản phẩm yêu thích trống." });
            }
            if (result == "invalid")
            {
                return Json(new { code = 0, message = "Sản phẩm không thuộc trong danh sách yêu thích." });
            }
            return Json(new { code = result, message = "Xóa sản phẩm khỏi danh sách yêu thích thành công." });
        }


    }
}
