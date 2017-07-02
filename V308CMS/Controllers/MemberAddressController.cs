using System.Linq;
using System.Web.Mvc;

namespace V308CMS.Controllers
{
    public class MemberAddressController : BaseController
    {

        public JsonResult AjaxAddress(int value)
        {
            var listDistrict = RegionService.GetListRegionByParentId(value);
            return Json(listDistrict.Select(item=>new
            {
                item.Id,
                item.Name
            }));

        }        
        //
        // GET: /Address/

        public ActionResult Index()
        {
            return View();
        }

    }
}
