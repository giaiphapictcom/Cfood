using System.Web.Mvc;
using V308CMS.Admin.Attributes;
using V308CMS.Admin.Helpers.Url;
using V308CMS.Admin.Models;

namespace V308CMS.Admin.Controllers
{
    [CheckGroupPermission(false)]
    public class AsyncController : BaseController
    {
        //
        // GET: /Async/

        public PartialViewResult CountAsync(byte type)
        {
            var countBox = new CountItems();
            switch (type)
            {
                case (byte)CountBoxType.CountOrder:
                    countBox.Name = "Đơn Hàng";
                    countBox.Total = 150;
                    countBox.IconClass = "fa-shopping-cart";
                    countBox.Url = "";
                    break;
                case (byte)CountBoxType.CountUser:
                    countBox.Name = "Khách hàng";
                    countBox.Total = UserService.Count();
                    countBox.IconClass = "fa-user";
                    countBox.Url = UserUrlHelper.UserIndexUrl();
                    break;
                case (byte)CountBoxType.CountContact:
                    countBox.Name = "Liên hệ";
                    countBox.Total = ContactService.Count();
                    countBox.IconClass = "fa-book";
                    countBox.Url = ContactUrlHelper.ContactIndexUrl();
                    break;
                case (byte)CountBoxType.CountProduct:
                    countBox.Name = "Sản phẩm";
                    countBox.Total = ProductService.Count();
                    countBox.IconClass = "fa-product-hunt";
                    countBox.Url = ProductUrlHelper.ProductIndexUrl();
                    break;
            }
            return PartialView("_CountBox", countBox);
        }
        public PartialViewResult LoadLastestOrderAsync()
        {
            return PartialView("_LastestOrder",OrderService.Take());
        }

        public PartialViewResult LoadLastestUserAsync()
        {
            return PartialView("_LastestUser", UserService.Take());
        }
        public PartialViewResult LoadLastestContactAsync()
        {
            return PartialView("_LastestContact", ContactService.Take());
        }


    }
}
